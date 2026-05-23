using System;
using System.Data;
using ClosedXML.Excel;

namespace testing
{
    public static class ScheduleExporter
    {
        private static readonly XLColor HeaderBg   = XLColor.FromHtml("#2E75B6");
        private static readonly XLColor HeaderFg   = XLColor.White;
        private static readonly XLColor ConflictBg = XLColor.FromHtml("#FFE0E0");
        private static readonly XLColor ConflictFg = XLColor.FromHtml("#C00000");
        private static readonly XLColor EvenRowBg  = XLColor.FromHtml("#F8FCFF");
        private static readonly XLColor ClassHdrBg = XLColor.FromHtml("#E6F0FA");

        private static readonly string[] DayNames =
            { "", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };

        public static void Export(string filePath)
        {
            using (var wb = new XLWorkbook())
            {
                DataTable classes = DbHelper.Query(
                    "SELECT ID_класса AS class_id, " +
                    "CAST(pc.Параллель AS NVARCHAR) + lc.Буква AS class_name " +
                    "FROM Classes cl " +
                    "JOIN ParallelClass pc ON cl.ID_параллели_класса = pc.ID_параллели_класса " +
                    "JOIN LetterClass lc   ON cl.ID_буквы_класса     = lc.ID_буквы_класса " +
                    "ORDER BY pc.Параллель, lc.Буква");

                // Конфликты из представления
                DataTable conflicts = DbHelper.Query(
                    "SELECT DISTINCT ID_дня_недели, Номер_урока FROM vw_Conflicts");

                var summary = wb.Worksheets.Add("Всё расписание");
                BuildSummarySheet(summary, classes, conflicts);

                foreach (DataRow cls in classes.Rows)
                {
                    int    cid   = Convert.ToInt32(cls["class_id"]);
                    string cname = cls["class_name"].ToString().Trim();
                    var    ws    = wb.Worksheets.Add(cname);
                    BuildClassSheet(ws, cid, cname, conflicts);
                }

                wb.SaveAs(filePath);
            }
        }

        private static void BuildSummarySheet(IXLWorksheet ws,
            DataTable classes, DataTable conflicts)
        {
            ws.Cell(1, 1).Value = "Школьное расписание";
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 1).Style.Font.FontSize = 16;
            ws.Cell(1, 1).Style.Font.FontColor = HeaderBg;
            ws.Range(1, 1, 1, 7).Merge();
            ws.Cell(2, 1).Value = "Сформировано: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            ws.Range(2, 1, 2, 7).Merge();

            int hr = 4;
            string[] hdrs = { "Класс", "Урок", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
            for (int i = 0; i < hdrs.Length; i++)
            {
                var cell = ws.Cell(hr, i + 1);
                cell.Value = hdrs[i];
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontColor = HeaderFg;
                cell.Style.Fill.BackgroundColor = HeaderBg;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            ws.Row(hr).Height = 20;

            int curRow = hr + 1;
            foreach (DataRow cls in classes.Rows)
            {
                int    cid   = Convert.ToInt32(cls["class_id"]);
                string cname = cls["class_name"].ToString().Trim();
                int    cstart = curRow;
                bool   odd   = (classes.Rows.IndexOf(cls) % 2 == 0);
                XLColor rowBg = odd ? XLColor.White : EvenRowBg;

                DataTable dt = DbHelper.Query(
                    "SELECT * FROM vw_Schedule WHERE ID_класса = @cid",
                    p => p.AddWithValue("@cid", cid));

                for (int l = 1; l <= 8; l++)
                {
                    ws.Cell(curRow, 1).Value = l == 1 ? cname : "";
                    ws.Cell(curRow, 2).Value = l;
                    ws.Cell(curRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    for (int d = 3; d <= 7; d++)
                    {
                        ws.Cell(curRow, d).Style.Fill.BackgroundColor = rowBg;
                        ws.Cell(curRow, d).Style.Alignment.WrapText   = true;
                        ws.Cell(curRow, d).Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        if (Convert.ToInt32(row["Номер_урока"]) != l) continue;
                        int day  = Convert.ToInt32(row["ID_дня_недели"]);
                        var cell = ws.Cell(curRow, day + 2);
                        cell.Value = string.Format("{0}\n{1}\nКаб.{2}",
                            row["Предмет"], row["ФИО_учителя"], row["Кабинет"]);
                        if (IsConflict(conflicts, day, l))
                        { cell.Style.Fill.BackgroundColor = ConflictBg;
                          cell.Style.Font.FontColor       = ConflictFg; }
                    }
                    ws.Row(curRow).Height = 42;
                    curRow++;
                }

                var cr = ws.Range(cstart, 1, curRow - 1, 1);
                cr.Merge();
                cr.Style.Font.Bold = true;
                cr.Style.Fill.BackgroundColor = ClassHdrBg;
                cr.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cr.Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
                cr.Style.Border.OutsideBorder  = XLBorderStyleValues.Medium;
            }

            ws.Column(1).Width = 8; ws.Column(2).Width = 6;
            for (int d = 3; d <= 7; d++) ws.Column(d).Width = 28;
            ws.SheetView.FreezeRows(hr);
        }

        private static void BuildClassSheet(IXLWorksheet ws, int classId,
            string className, DataTable conflicts)
        {
            ws.Cell(1, 1).Value = "Расписание класса " + className;
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 1).Style.Font.FontSize = 14;
            ws.Range(1, 1, 1, 6).Merge();

            ws.Cell(3, 1).Value = "Урок";
            for (int d = 1; d <= 5; d++) ws.Cell(3, d + 1).Value = DayNames[d];
            var hdr = ws.Range(3, 1, 3, 6);
            hdr.Style.Font.Bold = true;
            hdr.Style.Font.FontColor = HeaderFg;
            hdr.Style.Fill.BackgroundColor = HeaderBg;
            hdr.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(3).Height = 22;

            DataTable dt = DbHelper.Query(
                "SELECT * FROM vw_Schedule WHERE ID_класса = @cid",
                p => p.AddWithValue("@cid", classId));

            for (int l = 1; l <= 8; l++)
            {
                int ri = l + 3;
                ws.Cell(ri, 1).Value = l;
                ws.Cell(ri, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(ri, 1).Style.Font.Bold = true;
                ws.Cell(ri, 1).Style.Fill.BackgroundColor = ClassHdrBg;
                XLColor rowBg = l % 2 == 0 ? EvenRowBg : XLColor.White;
                for (int d = 2; d <= 6; d++)
                {
                    ws.Cell(ri, d).Style.Fill.BackgroundColor = rowBg;
                    ws.Cell(ri, d).Style.Alignment.WrapText   = true;
                    ws.Cell(ri, d).Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
                }
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row["Номер_урока"]) != l) continue;
                    int day  = Convert.ToInt32(row["ID_дня_недели"]);
                    var cell = ws.Cell(ri, day + 1);
                    cell.Value = string.Format("{0}\n{1}\nКаб. {2}",
                        row["Предмет"], row["ФИО_учителя"], row["Кабинет"]);
                    if (IsConflict(conflicts, day, l))
                    { cell.Style.Fill.BackgroundColor = ConflictBg;
                      cell.Style.Font.FontColor       = ConflictFg; }
                }
                ws.Row(ri).Height = 42;
            }

            ws.Range(3, 1, 11, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            ws.Column(1).Width = 6;
            for (int d = 2; d <= 6; d++) ws.Column(d).Width = 30;
            ws.SheetView.FreezeRows(3);
        }

        private static bool IsConflict(DataTable c, int day, int lesson)
        {
            foreach (DataRow r in c.Rows)
                if (Convert.ToInt32(r["ID_дня_недели"]) == day &&
                    Convert.ToInt32(r["Номер_урока"])   == lesson) return true;
            return false;
        }
    }
}
