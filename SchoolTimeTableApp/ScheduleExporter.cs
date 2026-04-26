using System;
using System.Data;
using System.IO;
using ClosedXML.Excel;

namespace testing
{
    /// <summary>
    /// Exports the full school schedule to a well-formatted Excel file.
    /// One sheet per class, plus a summary sheet.
    /// Requires NuGet package: ClosedXML
    /// </summary>
    public static class ScheduleExporter
    {
        private static readonly string[] DayNames =
            { "", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };

        private const int MAX_LESSONS = 8;

        // Colors
        private static readonly XLColor HeaderBg    = XLColor.FromHtml("#2E75B6");
        private static readonly XLColor HeaderFg    = XLColor.White;
        private static readonly XLColor DayBg       = XLColor.FromHtml("#D6E4F0");
        private static readonly XLColor ConflictBg  = XLColor.FromHtml("#FFE0E0");
        private static readonly XLColor ConflictFg  = XLColor.FromHtml("#C00000");
        private static readonly XLColor EvenRowBg   = XLColor.FromHtml("#F8FCFF");
        private static readonly XLColor ClassHeaderBg = XLColor.FromHtml("#E6F0FA");

        public static void Export(string filePath)
        {
            using (var wb = new XLWorkbook())
            {
                // Load all classes
                DataTable classes = DbHelper.Query(
                    "SELECT cl.class_id, CAST(cp.parallel AS NVARCHAR) + lc.letterClass AS class_name " +
                    "FROM Classes cl " +
                    "JOIN ClassParallel cp ON cl.id_parallel_class = cp.id_parallel_class " +
                    "JOIN LetterOfTheClass lc ON cl.id_letter_class = lc.id_letter_class " +
                    "ORDER BY cp.parallel, lc.letterClass");

                // Load all conflicts once
                DataTable conflicts = DbHelper.Query(
                    "SELECT DISTINCT s1.day_of_week, s1.lesson_number FROM Schedule s1 " +
                    "JOIN Workload w1 ON s1.workload_id = w1.workload_id " +
                    "JOIN Schedule s2 ON s1.day_of_week = s2.day_of_week " +
                    "  AND s1.lesson_number = s2.lesson_number AND s1.schedule_id <> s2.schedule_id " +
                    "JOIN Workload w2 ON s2.workload_id = w2.workload_id " +
                    "WHERE w1.teacher_id = w2.teacher_id OR s1.classroom_id = s2.classroom_id");

                // ── Summary sheet ────────────────────────────────────────────
                var summary = wb.Worksheets.Add("Всё расписание");
                BuildSummarySheet(summary, classes, conflicts);

                // ── One sheet per class ──────────────────────────────────────
                foreach (DataRow cls in classes.Rows)
                {
                    int    classId   = Convert.ToInt32(cls["class_id"]);
                    string className = cls["class_name"].ToString().Trim();
                    var    ws        = wb.Worksheets.Add(className);
                    BuildClassSheet(ws, classId, className, conflicts);
                }

                wb.SaveAs(filePath);
            }
        }

        // ── Summary: all classes, rows = class+lesson, cols = days ──────────

        private static void BuildSummarySheet(IXLWorksheet ws,
            DataTable classes, DataTable conflicts)
        {
            // Title
            ws.Cell(1, 1).Value = "Школьное расписание";
            ws.Cell(1, 1).Style.Font.Bold      = true;
            ws.Cell(1, 1).Style.Font.FontSize  = 16;
            ws.Cell(1, 1).Style.Font.FontColor = HeaderBg;
            ws.Range(1, 1, 1, 7).Merge();
            ws.Cell(2, 1).Value = "Сформировано: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            ws.Cell(2, 1).Style.Font.FontColor = XLColor.Gray;
            ws.Range(2, 1, 2, 7).Merge();

            // Column headers
            int headerRow = 4;
            string[] headers = { "Класс", "Урок", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cell(headerRow, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold           = true;
                cell.Style.Font.FontColor      = HeaderFg;
                cell.Style.Fill.BackgroundColor = HeaderBg;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }
            ws.Row(headerRow).Height = 20;

            int currentRow = headerRow + 1;

            foreach (DataRow cls in classes.Rows)
            {
                int    classId   = Convert.ToInt32(cls["class_id"]);
                string className = cls["class_name"].ToString().Trim();
                int    classStart = currentRow;

                DataTable dt = DbHelper.Query(
                    "SELECT s.day_of_week, s.lesson_number, sub.subject_name, " +
                    "t.name AS teacher_name, cr.room_number " +
                    "FROM Schedule s " +
                    "JOIN Workload w   ON s.workload_id  = w.workload_id " +
                    "JOIN Subjects sub ON w.subject_id   = sub.subject_id " +
                    "JOIN Teachers t   ON w.teacher_id   = t.teacher_id " +
                    "JOIN Classrooms cr ON s.classroom_id = cr.classroom_id " +
                    "WHERE w.class_id = @cid",
                    p => p.AddWithValue("@cid", classId));

                // Write MAX_LESSONS rows
                for (int l = 1; l <= MAX_LESSONS; l++)
                {
                    ws.Cell(currentRow, 1).Value = (l == 1) ? className : "";
                    ws.Cell(currentRow, 2).Value = l;
                    ws.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    bool even = (classes.Rows.IndexOf(cls) % 2 == 0);
                    XLColor rowBg = even ? XLColor.White : EvenRowBg;

                    for (int d = 1; d <= 5; d++)
                    {
                        var cell = ws.Cell(currentRow, d + 2);
                        cell.Style.Fill.BackgroundColor = rowBg;
                        cell.Style.Alignment.WrapText   = true;
                        cell.Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
                        cell.Style.Border.OutsideBorder = XLBorderStyleValues.Hair;
                    }

                    // Fill lesson data
                    foreach (DataRow row in dt.Rows)
                    {
                        if (Convert.ToInt32(row["lesson_number"]) != l) continue;
                        int day = Convert.ToInt32(row["day_of_week"]);
                        var cell = ws.Cell(currentRow, day + 2);
                        cell.Value = string.Format("{0}\n{1}\nКаб.{2}",
                            row["subject_name"], row["teacher_name"], row["room_number"]);

                        if (IsConflict(conflicts, day, l))
                        {
                            cell.Style.Fill.BackgroundColor = ConflictBg;
                            cell.Style.Font.FontColor       = ConflictFg;
                        }
                    }

                    ws.Row(currentRow).Height = 42;
                    currentRow++;
                }

                // Class name cell — bold, colored, merge vertically
                var classRange = ws.Range(classStart, 1, currentRow - 1, 1);
                classRange.Merge();
                classRange.Style.Font.Bold           = true;
                classRange.Style.Fill.BackgroundColor = ClassHeaderBg;
                classRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                classRange.Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
                classRange.Style.Border.OutsideBorder = XLBorderStyleValues.Medium;

                // Bottom border after each class
                ws.Range(currentRow - 1, 1, currentRow - 1, 7)
                  .Style.Border.BottomBorder = XLBorderStyleValues.Medium;
            }

            // Column widths
            ws.Column(1).Width = 8;
            ws.Column(2).Width = 6;
            for (int d = 3; d <= 7; d++)
                ws.Column(d).Width = 28;

            ws.SheetView.FreezeRows(headerRow);
        }

        // ── Per-class sheet ──────────────────────────────────────────────────

        private static void BuildClassSheet(IXLWorksheet ws, int classId,
            string className, DataTable conflicts)
        {
            // Title
            ws.Cell(1, 1).Value = "Расписание класса " + className;
            ws.Cell(1, 1).Style.Font.Bold     = true;
            ws.Cell(1, 1).Style.Font.FontSize = 14;
            ws.Cell(1, 1).Style.Font.FontColor = HeaderBg;
            ws.Range(1, 1, 1, 6).Merge();

            // Header row
            ws.Cell(3, 1).Value = "Урок";
            for (int d = 1; d <= 5; d++)
                ws.Cell(3, d + 1).Value = DayNames[d];

            var headerRange = ws.Range(3, 1, 3, 6);
            headerRange.Style.Font.Bold           = true;
            headerRange.Style.Font.FontColor      = HeaderFg;
            headerRange.Style.Fill.BackgroundColor = HeaderBg;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerRange.Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
            headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            ws.Row(3).Height = 22;

            DataTable dt = DbHelper.Query(
                "SELECT s.day_of_week, s.lesson_number, sub.subject_name, " +
                "t.name AS teacher_name, cr.room_number " +
                "FROM Schedule s " +
                "JOIN Workload w   ON s.workload_id  = w.workload_id " +
                "JOIN Subjects sub ON w.subject_id   = sub.subject_id " +
                "JOIN Teachers t   ON w.teacher_id   = t.teacher_id " +
                "JOIN Classrooms cr ON s.classroom_id = cr.classroom_id " +
                "WHERE w.class_id = @cid",
                p => p.AddWithValue("@cid", classId));

            for (int l = 1; l <= MAX_LESSONS; l++)
            {
                int rowIdx = l + 3;
                ws.Cell(rowIdx, 1).Value = l;
                ws.Cell(rowIdx, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(rowIdx, 1).Style.Font.Bold = true;
                ws.Cell(rowIdx, 1).Style.Fill.BackgroundColor = ClassHeaderBg;

                XLColor rowBg = (l % 2 == 0) ? EvenRowBg : XLColor.White;

                for (int d = 1; d <= 5; d++)
                {
                    var cell = ws.Cell(rowIdx, d + 1);
                    cell.Style.Fill.BackgroundColor = rowBg;
                    cell.Style.Alignment.WrapText   = true;
                    cell.Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Hair;
                }

                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row["lesson_number"]) != l) continue;
                    int day  = Convert.ToInt32(row["day_of_week"]);
                    var cell = ws.Cell(rowIdx, day + 1);
                    cell.Value = string.Format("{0}\n{1}\nКаб. {2}",
                        row["subject_name"], row["teacher_name"], row["room_number"]);

                    if (IsConflict(conflicts, day, l))
                    {
                        cell.Style.Fill.BackgroundColor = ConflictBg;
                        cell.Style.Font.FontColor       = ConflictFg;
                    }
                }

                ws.Row(rowIdx).Height = 42;
            }

            // Borders around full table
            ws.Range(3, 1, MAX_LESSONS + 3, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;

            // Column widths
            ws.Column(1).Width = 6;
            for (int d = 2; d <= 6; d++)
                ws.Column(d).Width = 30;

            ws.SheetView.FreezeRows(3);
        }

        private static bool IsConflict(DataTable conflicts, int day, int lesson)
        {
            foreach (DataRow cr in conflicts.Rows)
                if (Convert.ToInt32(cr["day_of_week"])   == day &&
                    Convert.ToInt32(cr["lesson_number"]) == lesson)
                    return true;
            return false;
        }
    }
}
