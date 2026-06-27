namespace testing
{
    partial class ComposeScheduleControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.splitLeft = new System.Windows.Forms.SplitContainer();
            this.labelClassesTitle = new System.Windows.Forms.Label();
            this.listBoxClasses = new System.Windows.Forms.ListBox();
            this.panelLegend = new System.Windows.Forms.Panel();
            this.labelLegendTitle = new System.Windows.Forms.Label();
            this.picConflict = new System.Windows.Forms.PictureBox();
            this.labelLegendConflict = new System.Windows.Forms.Label();
            this.picNormal = new System.Windows.Forms.PictureBox();
            this.labelLegendNormal = new System.Windows.Forms.Label();
            this.picEmpty = new System.Windows.Forms.PictureBox();
            this.labelLegendEmpty = new System.Windows.Forms.Label();
            this.splitCenter = new System.Windows.Forms.SplitContainer();
            this.panelWeekSwitch = new System.Windows.Forms.Panel();
            this.labelWeekSwitchTitle = new System.Windows.Forms.Label();
            this.radioWeekViewAll = new System.Windows.Forms.RadioButton();
            this.radioWeekViewOdd = new System.Windows.Forms.RadioButton();
            this.radioWeekViewEven = new System.Windows.Forms.RadioButton();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.splitRight = new System.Windows.Forms.SplitContainer();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.labelInfoTitle = new System.Windows.Forms.Label();
            this.labelInfoSubject = new System.Windows.Forms.Label();
            this.labelInfoSubjectVal = new System.Windows.Forms.Label();
            this.labelInfoTeacher = new System.Windows.Forms.Label();
            this.labelInfoTeacherVal = new System.Windows.Forms.Label();
            this.labelInfoRoom = new System.Windows.Forms.Label();
            this.labelInfoRoomVal = new System.Windows.Forms.Label();
            this.labelInfoDay = new System.Windows.Forms.Label();
            this.labelInfoDayVal = new System.Windows.Forms.Label();
            this.labelInfoLesson = new System.Windows.Forms.Label();
            this.labelInfoLessonVal = new System.Windows.Forms.Label();
            this.panelWarnings = new System.Windows.Forms.Panel();
            this.labelWarningsTitle = new System.Windows.Forms.Label();
            this.listBoxWarnings = new System.Windows.Forms.ListBox();

            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitLeft)).BeginInit();
            this.splitLeft.Panel1.SuspendLayout();
            this.splitLeft.Panel2.SuspendLayout();
            this.splitLeft.SuspendLayout();
            this.panelLegend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConflict)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNormal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmpty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitCenter)).BeginInit();
            this.splitCenter.Panel1.SuspendLayout();
            this.splitCenter.Panel2.SuspendLayout();
            this.splitCenter.SuspendLayout();
            this.panelWeekSwitch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).BeginInit();
            this.splitRight.Panel1.SuspendLayout();
            this.splitRight.Panel2.SuspendLayout();
            this.splitRight.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.panelWarnings.SuspendLayout();
            this.SuspendLayout();

            // splitMain — left panel vs center+right
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            this.splitMain.Panel1MinSize = 160;
            this.splitMain.Panel2MinSize = 400;
            this.splitMain.Size = new System.Drawing.Size(1192, 684);
            this.splitMain.SplitterDistance = 180;
            this.splitMain.TabIndex = 0;
            this.splitMain.Panel1.Controls.Add(this.splitLeft);
            this.splitMain.Panel2.Controls.Add(this.splitCenter);

            // splitLeft — classes list top, legend bottom
            this.splitLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLeft.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitLeft.Location = new System.Drawing.Point(0, 0);
            this.splitLeft.Name = "splitLeft";
            this.splitLeft.Panel1MinSize = 200;
            this.splitLeft.Panel2MinSize = 120;
            this.splitLeft.Size = new System.Drawing.Size(177, 684);
            this.splitLeft.SplitterDistance = 560;
            this.splitLeft.TabIndex = 0;
            this.splitLeft.Panel1.Controls.Add(this.listBoxClasses);
            this.splitLeft.Panel1.Controls.Add(this.labelClassesTitle);
            this.splitLeft.Panel2.Controls.Add(this.panelLegend);

            // labelClassesTitle
            this.labelClassesTitle.AutoSize = false;
            this.labelClassesTitle.BackColor = System.Drawing.Color.SteelBlue;
            this.labelClassesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelClassesTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelClassesTitle.ForeColor = System.Drawing.Color.White;
            this.labelClassesTitle.Location = new System.Drawing.Point(0, 0);
            this.labelClassesTitle.Name = "labelClassesTitle";
            this.labelClassesTitle.Size = new System.Drawing.Size(177, 28);
            this.labelClassesTitle.TabIndex = 0;
            this.labelClassesTitle.Text = "  Классы";
            this.labelClassesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // listBoxClasses
            this.listBoxClasses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxClasses.FormattingEnabled = true;
            this.listBoxClasses.ItemHeight = 18;
            this.listBoxClasses.Location = new System.Drawing.Point(0, 28);
            this.listBoxClasses.Name = "listBoxClasses";
            this.listBoxClasses.Size = new System.Drawing.Size(177, 532);
            this.listBoxClasses.TabIndex = 1;
            this.listBoxClasses.SelectedIndexChanged += new System.EventHandler(this.listBoxClasses_SelectedIndexChanged);

            // panelLegend
            this.panelLegend.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelLegend.Controls.Add(this.labelLegendTitle);
            this.panelLegend.Controls.Add(this.picConflict);
            this.panelLegend.Controls.Add(this.labelLegendConflict);
            this.panelLegend.Controls.Add(this.picNormal);
            this.panelLegend.Controls.Add(this.labelLegendNormal);
            this.panelLegend.Controls.Add(this.picEmpty);
            this.panelLegend.Controls.Add(this.labelLegendEmpty);
            this.panelLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLegend.Location = new System.Drawing.Point(0, 0);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(177, 120);
            this.panelLegend.TabIndex = 0;

            // labelLegendTitle
            this.labelLegendTitle.AutoSize = false;
            this.labelLegendTitle.BackColor = System.Drawing.Color.SteelBlue;
            this.labelLegendTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelLegendTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelLegendTitle.ForeColor = System.Drawing.Color.White;
            this.labelLegendTitle.Location = new System.Drawing.Point(0, 0);
            this.labelLegendTitle.Name = "labelLegendTitle";
            this.labelLegendTitle.Size = new System.Drawing.Size(177, 28);
            this.labelLegendTitle.TabIndex = 0;
            this.labelLegendTitle.Text = "  Легенда";
            this.labelLegendTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // picConflict
            this.picConflict.BackColor = System.Drawing.Color.MistyRose;
            this.picConflict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picConflict.Location = new System.Drawing.Point(8, 36);
            this.picConflict.Name = "picConflict";
            this.picConflict.Size = new System.Drawing.Size(16, 16);
            this.picConflict.TabIndex = 1;
            this.picConflict.TabStop = false;

            // labelLegendConflict
            this.labelLegendConflict.AutoSize = true;
            this.labelLegendConflict.Location = new System.Drawing.Point(28, 38);
            this.labelLegendConflict.Name = "labelLegendConflict";
            this.labelLegendConflict.TabIndex = 2;
            this.labelLegendConflict.Text = "Конфликт";

            // picNormal
            this.picNormal.BackColor = System.Drawing.Color.White;
            this.picNormal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picNormal.Location = new System.Drawing.Point(8, 60);
            this.picNormal.Name = "picNormal";
            this.picNormal.Size = new System.Drawing.Size(16, 16);
            this.picNormal.TabIndex = 3;
            this.picNormal.TabStop = false;

            // labelLegendNormal
            this.labelLegendNormal.AutoSize = true;
            this.labelLegendNormal.Location = new System.Drawing.Point(28, 62);
            this.labelLegendNormal.Name = "labelLegendNormal";
            this.labelLegendNormal.TabIndex = 4;
            this.labelLegendNormal.Text = "Урок назначен";

            // picEmpty
            this.picEmpty.BackColor = System.Drawing.Color.LightGray;
            this.picEmpty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEmpty.Location = new System.Drawing.Point(8, 84);
            this.picEmpty.Name = "picEmpty";
            this.picEmpty.Size = new System.Drawing.Size(16, 16);
            this.picEmpty.TabIndex = 5;
            this.picEmpty.TabStop = false;

            // labelLegendEmpty
            this.labelLegendEmpty.AutoSize = true;
            this.labelLegendEmpty.Location = new System.Drawing.Point(28, 86);
            this.labelLegendEmpty.Name = "labelLegendEmpty";
            this.labelLegendEmpty.TabIndex = 6;
            this.labelLegendEmpty.Text = "Свободно (+)";

            // splitCenter — grid left, right panel right
            this.splitCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCenter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitCenter.Location = new System.Drawing.Point(0, 0);
            this.splitCenter.Name = "splitCenter";
            this.splitCenter.Panel1MinSize = 300;
            this.splitCenter.Panel2MinSize = 220;
            this.splitCenter.Size = new System.Drawing.Size(1009, 684);
            this.splitCenter.SplitterDistance = 780;
            this.splitCenter.TabIndex = 0;
            this.splitCenter.Panel1.Controls.Add(this.dataGrid);
            this.splitCenter.Panel1.Controls.Add(this.panelWeekSwitch);
            this.splitCenter.Panel2.Controls.Add(this.splitRight);

            // panelWeekSwitch — переключатель отображения по неделям
            this.panelWeekSwitch.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            this.panelWeekSwitch.Controls.Add(this.labelWeekSwitchTitle);
            this.panelWeekSwitch.Controls.Add(this.radioWeekViewAll);
            this.panelWeekSwitch.Controls.Add(this.radioWeekViewOdd);
            this.panelWeekSwitch.Controls.Add(this.radioWeekViewEven);
            this.panelWeekSwitch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelWeekSwitch.Name = "panelWeekSwitch";
            this.panelWeekSwitch.Size = new System.Drawing.Size(780, 32);
            this.panelWeekSwitch.TabIndex = 1;
            // labelWeekSwitchTitle
            this.labelWeekSwitchTitle.AutoSize = true;
            this.labelWeekSwitchTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelWeekSwitchTitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelWeekSwitchTitle.Location = new System.Drawing.Point(6, 8);
            this.labelWeekSwitchTitle.Name = "labelWeekSwitchTitle";
            this.labelWeekSwitchTitle.Text = "Показать:";
            // radioWeekViewAll
            this.radioWeekViewAll.AutoSize = true;
            this.radioWeekViewAll.Checked = true;
            this.radioWeekViewAll.Location = new System.Drawing.Point(80, 7);
            this.radioWeekViewAll.Name = "radioWeekViewAll";
            this.radioWeekViewAll.TabIndex = 0;
            this.radioWeekViewAll.TabStop = true;
            this.radioWeekViewAll.Text = "Обе недели";
            this.radioWeekViewAll.CheckedChanged += new System.EventHandler(this.radioWeekView_CheckedChanged);
            // radioWeekViewOdd
            this.radioWeekViewOdd.AutoSize = true;
            this.radioWeekViewOdd.Location = new System.Drawing.Point(190, 7);
            this.radioWeekViewOdd.Name = "radioWeekViewOdd";
            this.radioWeekViewOdd.TabIndex = 1;
            this.radioWeekViewOdd.Text = "Нечётная неделя";
            this.radioWeekViewOdd.CheckedChanged += new System.EventHandler(this.radioWeekView_CheckedChanged);
            // radioWeekViewEven
            this.radioWeekViewEven.AutoSize = true;
            this.radioWeekViewEven.Location = new System.Drawing.Point(330, 7);
            this.radioWeekViewEven.Name = "radioWeekViewEven";
            this.radioWeekViewEven.TabIndex = 2;
            this.radioWeekViewEven.Text = "Чётная неделя";
            this.radioWeekViewEven.CheckedChanged += new System.EventHandler(this.radioWeekView_CheckedChanged);

            // dataGrid
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Location = new System.Drawing.Point(0, 0);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidth = 40;
            this.dataGrid.RowTemplate.Height = 52;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGrid.Size = new System.Drawing.Size(780, 684);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.CellClick    += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellClick);
            this.dataGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGrid_CellPainting);

            // splitRight — info top, warnings bottom
            this.splitRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitRight.Location = new System.Drawing.Point(0, 0);
            this.splitRight.Name = "splitRight";
            this.splitRight.Panel1MinSize = 200;
            this.splitRight.Panel2MinSize = 100;
            this.splitRight.Size = new System.Drawing.Size(225, 684);
            this.splitRight.SplitterDistance = 260;
            this.splitRight.TabIndex = 0;
            this.splitRight.Panel1.Controls.Add(this.panelInfo);
            this.splitRight.Panel2.Controls.Add(this.panelWarnings);

            // panelInfo
            this.panelInfo.BackColor = System.Drawing.Color.White;
            this.panelInfo.Controls.Add(this.labelInfoTitle);
            this.panelInfo.Controls.Add(this.labelInfoSubject);
            this.panelInfo.Controls.Add(this.labelInfoSubjectVal);
            this.panelInfo.Controls.Add(this.labelInfoTeacher);
            this.panelInfo.Controls.Add(this.labelInfoTeacherVal);
            this.panelInfo.Controls.Add(this.labelInfoRoom);
            this.panelInfo.Controls.Add(this.labelInfoRoomVal);
            this.panelInfo.Controls.Add(this.labelInfoDay);
            this.panelInfo.Controls.Add(this.labelInfoDayVal);
            this.panelInfo.Controls.Add(this.labelInfoLesson);
            this.panelInfo.Controls.Add(this.labelInfoLessonVal);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfo.Location = new System.Drawing.Point(0, 0);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(225, 260);
            this.panelInfo.TabIndex = 0;

            // labelInfoTitle
            this.labelInfoTitle.AutoSize = false;
            this.labelInfoTitle.BackColor = System.Drawing.Color.SteelBlue;
            this.labelInfoTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelInfoTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelInfoTitle.ForeColor = System.Drawing.Color.White;
            this.labelInfoTitle.Location = new System.Drawing.Point(0, 0);
            this.labelInfoTitle.Name = "labelInfoTitle";
            this.labelInfoTitle.Size = new System.Drawing.Size(225, 28);
            this.labelInfoTitle.TabIndex = 0;
            this.labelInfoTitle.Text = "  Выбранный урок";
            this.labelInfoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // labelInfoDay
            this.labelInfoDay.AutoSize = true;
            this.labelInfoDay.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.labelInfoDay.ForeColor = System.Drawing.Color.Gray;
            this.labelInfoDay.Location = new System.Drawing.Point(8, 36);
            this.labelInfoDay.Name = "labelInfoDay";
            this.labelInfoDay.TabIndex = 1;
            this.labelInfoDay.Text = "ДЕНЬ";
            // labelInfoDayVal
            this.labelInfoDayVal.AutoSize = false;
            this.labelInfoDayVal.Location = new System.Drawing.Point(8, 50);
            this.labelInfoDayVal.Name = "labelInfoDayVal";
            this.labelInfoDayVal.Size = new System.Drawing.Size(209, 18);
            this.labelInfoDayVal.TabIndex = 2;
            this.labelInfoDayVal.Text = "—";

            // labelInfoLesson
            this.labelInfoLesson.AutoSize = true;
            this.labelInfoLesson.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.labelInfoLesson.ForeColor = System.Drawing.Color.Gray;
            this.labelInfoLesson.Location = new System.Drawing.Point(8, 72);
            this.labelInfoLesson.Name = "labelInfoLesson";
            this.labelInfoLesson.TabIndex = 3;
            this.labelInfoLesson.Text = "УРОК";
            // labelInfoLessonVal
            this.labelInfoLessonVal.AutoSize = false;
            this.labelInfoLessonVal.Location = new System.Drawing.Point(8, 86);
            this.labelInfoLessonVal.Name = "labelInfoLessonVal";
            this.labelInfoLessonVal.Size = new System.Drawing.Size(209, 18);
            this.labelInfoLessonVal.TabIndex = 4;
            this.labelInfoLessonVal.Text = "—";

            // labelInfoSubject
            this.labelInfoSubject.AutoSize = true;
            this.labelInfoSubject.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.labelInfoSubject.ForeColor = System.Drawing.Color.Gray;
            this.labelInfoSubject.Location = new System.Drawing.Point(8, 108);
            this.labelInfoSubject.Name = "labelInfoSubject";
            this.labelInfoSubject.TabIndex = 5;
            this.labelInfoSubject.Text = "ПРЕДМЕТ";
            // labelInfoSubjectVal
            this.labelInfoSubjectVal.AutoSize = false;
            this.labelInfoSubjectVal.Location = new System.Drawing.Point(8, 122);
            this.labelInfoSubjectVal.Name = "labelInfoSubjectVal";
            this.labelInfoSubjectVal.Size = new System.Drawing.Size(209, 18);
            this.labelInfoSubjectVal.TabIndex = 6;
            this.labelInfoSubjectVal.Text = "—";

            // labelInfoTeacher
            this.labelInfoTeacher.AutoSize = true;
            this.labelInfoTeacher.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.labelInfoTeacher.ForeColor = System.Drawing.Color.Gray;
            this.labelInfoTeacher.Location = new System.Drawing.Point(8, 144);
            this.labelInfoTeacher.Name = "labelInfoTeacher";
            this.labelInfoTeacher.TabIndex = 7;
            this.labelInfoTeacher.Text = "УЧИТЕЛЬ";
            // labelInfoTeacherVal
            this.labelInfoTeacherVal.AutoSize = false;
            this.labelInfoTeacherVal.Location = new System.Drawing.Point(8, 158);
            this.labelInfoTeacherVal.Name = "labelInfoTeacherVal";
            this.labelInfoTeacherVal.Size = new System.Drawing.Size(209, 18);
            this.labelInfoTeacherVal.TabIndex = 8;
            this.labelInfoTeacherVal.Text = "—";

            // labelInfoRoom
            this.labelInfoRoom.AutoSize = true;
            this.labelInfoRoom.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.labelInfoRoom.ForeColor = System.Drawing.Color.Gray;
            this.labelInfoRoom.Location = new System.Drawing.Point(8, 180);
            this.labelInfoRoom.Name = "labelInfoRoom";
            this.labelInfoRoom.TabIndex = 9;
            this.labelInfoRoom.Text = "КАБИНЕТ";
            // labelInfoRoomVal
            this.labelInfoRoomVal.AutoSize = false;
            this.labelInfoRoomVal.Location = new System.Drawing.Point(8, 194);
            this.labelInfoRoomVal.Name = "labelInfoRoomVal";
            this.labelInfoRoomVal.Size = new System.Drawing.Size(209, 18);
            this.labelInfoRoomVal.TabIndex = 10;
            this.labelInfoRoomVal.Text = "—";

            // panelWarnings
            this.panelWarnings.BackColor = System.Drawing.Color.White;
            this.panelWarnings.Controls.Add(this.listBoxWarnings);
            this.panelWarnings.Controls.Add(this.labelWarningsTitle);
            this.panelWarnings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWarnings.Location = new System.Drawing.Point(0, 0);
            this.panelWarnings.Name = "panelWarnings";
            this.panelWarnings.Size = new System.Drawing.Size(225, 420);
            this.panelWarnings.TabIndex = 0;

            // labelWarningsTitle
            this.labelWarningsTitle.AutoSize = false;
            this.labelWarningsTitle.BackColor = System.Drawing.Color.Crimson;
            this.labelWarningsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelWarningsTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelWarningsTitle.ForeColor = System.Drawing.Color.White;
            this.labelWarningsTitle.Location = new System.Drawing.Point(0, 0);
            this.labelWarningsTitle.Name = "labelWarningsTitle";
            this.labelWarningsTitle.Size = new System.Drawing.Size(225, 28);
            this.labelWarningsTitle.TabIndex = 0;
            this.labelWarningsTitle.Text = "  ⚠ Конфликты";
            this.labelWarningsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // listBoxWarnings
            this.listBoxWarnings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxWarnings.FormattingEnabled = true;
            this.listBoxWarnings.ItemHeight = 15;
            this.listBoxWarnings.Location = new System.Drawing.Point(0, 28);
            this.listBoxWarnings.Name = "listBoxWarnings";
            this.listBoxWarnings.Size = new System.Drawing.Size(225, 392);
            this.listBoxWarnings.TabIndex = 1;

            // ComposeScheduleControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Name = "ComposeScheduleControl";
            this.Size = new System.Drawing.Size(1192, 684);

            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitLeft.Panel1.ResumeLayout(false);
            this.splitLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitLeft)).EndInit();
            this.splitLeft.ResumeLayout(false);
            this.panelLegend.ResumeLayout(false);
            this.panelLegend.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConflict)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNormal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmpty)).EndInit();
            this.panelWeekSwitch.ResumeLayout(false);
            this.panelWeekSwitch.PerformLayout();
            this.splitCenter.Panel1.ResumeLayout(false);
            this.splitCenter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCenter)).EndInit();
            this.splitCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.splitRight.Panel1.ResumeLayout(false);
            this.splitRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).EndInit();
            this.splitRight.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.panelWarnings.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.SplitContainer splitLeft;
        private System.Windows.Forms.Label labelClassesTitle;
        private System.Windows.Forms.ListBox listBoxClasses;
        private System.Windows.Forms.Panel panelLegend;
        private System.Windows.Forms.Label labelLegendTitle;
        private System.Windows.Forms.PictureBox picConflict;
        private System.Windows.Forms.Label labelLegendConflict;
        private System.Windows.Forms.PictureBox picNormal;
        private System.Windows.Forms.Label labelLegendNormal;
        private System.Windows.Forms.PictureBox picEmpty;
        private System.Windows.Forms.Label labelLegendEmpty;
        private System.Windows.Forms.SplitContainer splitCenter;
        private System.Windows.Forms.Panel panelWeekSwitch;
        private System.Windows.Forms.Label labelWeekSwitchTitle;
        private System.Windows.Forms.RadioButton radioWeekViewAll;
        private System.Windows.Forms.RadioButton radioWeekViewOdd;
        private System.Windows.Forms.RadioButton radioWeekViewEven;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.SplitContainer splitRight;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label labelInfoTitle;
        private System.Windows.Forms.Label labelInfoSubject;
        private System.Windows.Forms.Label labelInfoSubjectVal;
        private System.Windows.Forms.Label labelInfoTeacher;
        private System.Windows.Forms.Label labelInfoTeacherVal;
        private System.Windows.Forms.Label labelInfoRoom;
        private System.Windows.Forms.Label labelInfoRoomVal;
        private System.Windows.Forms.Label labelInfoDay;
        private System.Windows.Forms.Label labelInfoDayVal;
        private System.Windows.Forms.Label labelInfoLesson;
        private System.Windows.Forms.Label labelInfoLessonVal;
        private System.Windows.Forms.Panel panelWarnings;
        private System.Windows.Forms.Label labelWarningsTitle;
        private System.Windows.Forms.ListBox listBoxWarnings;
    }
}
