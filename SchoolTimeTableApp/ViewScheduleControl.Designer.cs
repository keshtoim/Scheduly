namespace testing
{
    partial class ViewScheduleControl
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
            this.panelFilters      = new System.Windows.Forms.Panel();
            this.labelClass        = new System.Windows.Forms.Label();
            this.comboClass        = new System.Windows.Forms.ComboBox();
            this.labelTeacher      = new System.Windows.Forms.Label();
            this.comboTeacher      = new System.Windows.Forms.ComboBox();
            this.labelDay          = new System.Windows.Forms.Label();
            this.comboDayFilter    = new System.Windows.Forms.ComboBox();
            this.buttonApplyFilter = new System.Windows.Forms.Button();
            this.buttonResetFilter = new System.Windows.Forms.Button();
            this.buttonExport      = new System.Windows.Forms.Button();
            this.panelSearch       = new System.Windows.Forms.Panel();
            this.labelSearch       = new System.Windows.Forms.Label();
            this.textSearch        = new System.Windows.Forms.TextBox();
            this.buttonSearch      = new System.Windows.Forms.Button();
            this.labelSearchHint   = new System.Windows.Forms.Label();
            this.labelLevel        = new System.Windows.Forms.Label();
            this.comboLevel        = new System.Windows.Forms.ComboBox();
            this.dataGrid          = new System.Windows.Forms.DataGridView();
            this.panelFilters.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFilters — верхняя строка фильтров
            // 
            this.panelFilters.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelFilters.Controls.Add(this.labelClass);
            this.panelFilters.Controls.Add(this.comboClass);
            this.panelFilters.Controls.Add(this.labelTeacher);
            this.panelFilters.Controls.Add(this.comboTeacher);
            this.panelFilters.Controls.Add(this.labelDay);
            this.panelFilters.Controls.Add(this.comboDayFilter);
            this.panelFilters.Controls.Add(this.labelLevel);
            this.panelFilters.Controls.Add(this.comboLevel);
            this.panelFilters.Controls.Add(this.buttonApplyFilter);
            this.panelFilters.Controls.Add(this.buttonResetFilter);
            this.panelFilters.Controls.Add(this.buttonExport);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 0);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(1192, 46);
            this.panelFilters.TabIndex = 0;
            // labelClass
            this.labelClass.AutoSize = true;
            this.labelClass.Location = new System.Drawing.Point(8, 14);
            this.labelClass.Name = "labelClass"; this.labelClass.TabIndex = 0;
            this.labelClass.Text = "Класс:";
            // comboClass
            this.comboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClass.FormattingEnabled = true;
            this.comboClass.Location = new System.Drawing.Point(58, 10);
            this.comboClass.Name = "comboClass";
            this.comboClass.Size = new System.Drawing.Size(130, 21);
            this.comboClass.TabIndex = 1;
            // labelTeacher
            this.labelTeacher.AutoSize = true;
            this.labelTeacher.Location = new System.Drawing.Point(198, 14);
            this.labelTeacher.Name = "labelTeacher"; this.labelTeacher.TabIndex = 2;
            this.labelTeacher.Text = "Учитель:";
            // comboTeacher
            this.comboTeacher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTeacher.FormattingEnabled = true;
            this.comboTeacher.Location = new System.Drawing.Point(256, 10);
            this.comboTeacher.Name = "comboTeacher";
            this.comboTeacher.Size = new System.Drawing.Size(185, 21);
            this.comboTeacher.TabIndex = 3;
            // labelDay
            this.labelDay.AutoSize = true;
            this.labelDay.Location = new System.Drawing.Point(452, 14);
            this.labelDay.Name = "labelDay"; this.labelDay.TabIndex = 4;
            this.labelDay.Text = "День:";
            // comboDayFilter
            this.comboDayFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDayFilter.FormattingEnabled = true;
            this.comboDayFilter.Items.AddRange(new object[] {
                "Все дни","Понедельник","Вторник","Среда","Четверг","Пятница"});
            this.comboDayFilter.Location = new System.Drawing.Point(492, 10);
            this.comboDayFilter.Name = "comboDayFilter";
            this.comboDayFilter.SelectedIndex = 0;
            this.comboDayFilter.Size = new System.Drawing.Size(115, 21);
            this.comboDayFilter.TabIndex = 5;
            // labelLevel
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(618, 14);
            this.labelLevel.Name = "labelLevel"; this.labelLevel.TabIndex = 6;
            this.labelLevel.Text = "Ступень:";
            // comboLevel
            this.comboLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLevel.FormattingEnabled = true;
            this.comboLevel.Location = new System.Drawing.Point(668, 10);
            this.comboLevel.Name = "comboLevel";
            this.comboLevel.Size = new System.Drawing.Size(170, 21);
            this.comboLevel.TabIndex = 7;
            // buttonApplyFilter
            this.buttonApplyFilter.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApplyFilter.ForeColor = System.Drawing.Color.White;
            this.buttonApplyFilter.Location = new System.Drawing.Point(850, 9);
            this.buttonApplyFilter.Name = "buttonApplyFilter";
            this.buttonApplyFilter.Size = new System.Drawing.Size(90, 26);
            this.buttonApplyFilter.TabIndex = 8;
            this.buttonApplyFilter.Text = "Применить";
            this.buttonApplyFilter.UseVisualStyleBackColor = false;
            this.buttonApplyFilter.Click += new System.EventHandler(this.buttonApplyFilter_Click);
            // buttonResetFilter
            this.buttonResetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonResetFilter.Location = new System.Drawing.Point(948, 9);
            this.buttonResetFilter.Name = "buttonResetFilter";
            this.buttonResetFilter.Size = new System.Drawing.Size(80, 26);
            this.buttonResetFilter.TabIndex = 9;
            this.buttonResetFilter.Text = "Сбросить";
            this.buttonResetFilter.UseVisualStyleBackColor = true;
            this.buttonResetFilter.Click += new System.EventHandler(this.buttonResetFilter_Click);
            // buttonExport
            this.buttonExport.BackColor = System.Drawing.Color.SeaGreen;
            this.buttonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExport.ForeColor = System.Drawing.Color.White;
            this.buttonExport.Location = new System.Drawing.Point(1036, 9);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(148, 26);
            this.buttonExport.TabIndex = 10;
            this.buttonExport.Text = "⬇ Экспорт в Excel";
            this.buttonExport.UseVisualStyleBackColor = false;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // panelSearch — вторая строка с поиском
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(240, 246, 255);
            this.panelSearch.Controls.Add(this.labelSearch);
            this.panelSearch.Controls.Add(this.textSearch);
            this.panelSearch.Controls.Add(this.buttonSearch);
            this.panelSearch.Controls.Add(this.labelSearchHint);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 46);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1192, 36);
            this.panelSearch.TabIndex = 1;
            // labelSearch
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(8, 10);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.TabIndex = 0;
            this.labelSearch.Text = "🔍 Поиск:";
            // textSearch
            this.textSearch.Location = new System.Drawing.Point(72, 7);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(340, 20);
            this.textSearch.TabIndex = 1;
            this.textSearch.TextChanged += new System.EventHandler(this.textSearch_TextChanged);
            this.textSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textSearch_KeyDown);
            // buttonSearch
            this.buttonSearch.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.ForeColor = System.Drawing.Color.White;
            this.buttonSearch.Location  = new System.Drawing.Point(420, 5);
            this.buttonSearch.Name      = "buttonSearch";
            this.buttonSearch.Size      = new System.Drawing.Size(80, 24);
            this.buttonSearch.TabIndex  = 2;
            this.buttonSearch.Text      = "Найти";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // labelSearchHint
            this.labelSearchHint.AutoSize = true;
            this.labelSearchHint.ForeColor = System.Drawing.Color.SeaGreen;
            this.labelSearchHint.Location = new System.Drawing.Point(510, 10);
            this.labelSearchHint.Name = "labelSearchHint";
            this.labelSearchHint.TabIndex = 3;
            this.labelSearchHint.Text = "";
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Location = new System.Drawing.Point(0, 82);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersWidth = 4;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.RowTemplate.Height = 52;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGrid.Size = new System.Drawing.Size(1192, 602);
            this.dataGrid.TabIndex = 2;
            this.dataGrid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dataGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SteelBlue;
            this.dataGrid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dataGrid.EnableHeadersVisualStyles = false;
            // 
            // ViewScheduleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelFilters);
            this.Name = "ViewScheduleControl";
            this.Size = new System.Drawing.Size(1192, 684);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel      panelFilters;
        private System.Windows.Forms.Label      labelClass;
        private System.Windows.Forms.ComboBox   comboClass;
        private System.Windows.Forms.Label      labelTeacher;
        private System.Windows.Forms.ComboBox   comboTeacher;
        private System.Windows.Forms.Label      labelDay;
        private System.Windows.Forms.ComboBox   comboDayFilter;
        private System.Windows.Forms.Label      labelLevel;
        private System.Windows.Forms.ComboBox   comboLevel;
        private System.Windows.Forms.Button     buttonApplyFilter;
        private System.Windows.Forms.Button     buttonResetFilter;
        private System.Windows.Forms.Button     buttonExport;
        private System.Windows.Forms.Panel      panelSearch;
        private System.Windows.Forms.Label      labelSearch;
        private System.Windows.Forms.TextBox    textSearch;
        private System.Windows.Forms.Button     buttonSearch;
        private System.Windows.Forms.Label      labelSearchHint;
        private System.Windows.Forms.DataGridView dataGrid;
    }
}
