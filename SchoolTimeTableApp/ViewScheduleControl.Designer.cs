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
            this.panelFilters = new System.Windows.Forms.Panel();
            this.labelClass = new System.Windows.Forms.Label();
            this.comboClass = new System.Windows.Forms.ComboBox();
            this.labelTeacher = new System.Windows.Forms.Label();
            this.comboTeacher = new System.Windows.Forms.ComboBox();
            this.labelDay = new System.Windows.Forms.Label();
            this.comboDayFilter = new System.Windows.Forms.ComboBox();
            this.buttonApplyFilter = new System.Windows.Forms.Button();
            this.buttonResetFilter = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFilters
            // 
            this.panelFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.panelFilters.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelFilters.Controls.Add(this.labelClass);
            this.panelFilters.Controls.Add(this.comboClass);
            this.panelFilters.Controls.Add(this.labelTeacher);
            this.panelFilters.Controls.Add(this.comboTeacher);
            this.panelFilters.Controls.Add(this.labelDay);
            this.panelFilters.Controls.Add(this.comboDayFilter);
            this.panelFilters.Controls.Add(this.buttonApplyFilter);
            this.panelFilters.Controls.Add(this.buttonResetFilter);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 0);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(1192, 46);
            this.panelFilters.TabIndex = 0;
            // labelClass
            this.labelClass.AutoSize = true;
            this.labelClass.Location = new System.Drawing.Point(8, 14);
            this.labelClass.Name = "labelClass";
            this.labelClass.TabIndex = 0;
            this.labelClass.Text = "Класс:";
            // comboClass
            this.comboClass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClass.FormattingEnabled = true;
            this.comboClass.Location = new System.Drawing.Point(60, 10);
            this.comboClass.Name = "comboClass";
            this.comboClass.Size = new System.Drawing.Size(140, 21);
            this.comboClass.TabIndex = 1;
            // labelTeacher
            this.labelTeacher.AutoSize = true;
            this.labelTeacher.Location = new System.Drawing.Point(212, 14);
            this.labelTeacher.Name = "labelTeacher";
            this.labelTeacher.TabIndex = 2;
            this.labelTeacher.Text = "Учитель:";
            // comboTeacher
            this.comboTeacher.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboTeacher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTeacher.FormattingEnabled = true;
            this.comboTeacher.Location = new System.Drawing.Point(272, 10);
            this.comboTeacher.Name = "comboTeacher";
            this.comboTeacher.Size = new System.Drawing.Size(200, 21);
            this.comboTeacher.TabIndex = 3;
            // labelDay
            this.labelDay.AutoSize = true;
            this.labelDay.Location = new System.Drawing.Point(484, 14);
            this.labelDay.Name = "labelDay";
            this.labelDay.TabIndex = 4;
            this.labelDay.Text = "День:";
            // comboDayFilter
            this.comboDayFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboDayFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDayFilter.FormattingEnabled = true;
            this.comboDayFilter.Items.AddRange(new object[] {
                "Все дни","Понедельник","Вторник","Среда","Четверг","Пятница"});
            this.comboDayFilter.Location = new System.Drawing.Point(528, 10);
            this.comboDayFilter.Name = "comboDayFilter";
            this.comboDayFilter.SelectedIndex = 0;
            this.comboDayFilter.Size = new System.Drawing.Size(150, 21);
            this.comboDayFilter.TabIndex = 5;
            // buttonApplyFilter
            this.buttonApplyFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonApplyFilter.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApplyFilter.ForeColor = System.Drawing.Color.White;
            this.buttonApplyFilter.Location = new System.Drawing.Point(694, 9);
            this.buttonApplyFilter.Name = "buttonApplyFilter";
            this.buttonApplyFilter.Size = new System.Drawing.Size(110, 26);
            this.buttonApplyFilter.TabIndex = 6;
            this.buttonApplyFilter.Text = "Применить";
            this.buttonApplyFilter.UseVisualStyleBackColor = false;
            this.buttonApplyFilter.Click += new System.EventHandler(this.buttonApplyFilter_Click);
            // buttonResetFilter
            this.buttonResetFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonResetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonResetFilter.Location = new System.Drawing.Point(812, 9);
            this.buttonResetFilter.Name = "buttonResetFilter";
            this.buttonResetFilter.Size = new System.Drawing.Size(110, 26);
            this.buttonResetFilter.TabIndex = 7;
            this.buttonResetFilter.Text = "Сбросить";
            this.buttonResetFilter.UseVisualStyleBackColor = true;
            this.buttonResetFilter.Click += new System.EventHandler(this.buttonResetFilter_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Location = new System.Drawing.Point(0, 46);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersWidth = 40;
            this.dataGrid.RowTemplate.Height = 52;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGrid.Size = new System.Drawing.Size(1192, 638);
            this.dataGrid.TabIndex = 1;
            // 
            // ViewScheduleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.panelFilters);
            this.Name = "ViewScheduleControl";
            this.Size = new System.Drawing.Size(1192, 684);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Label labelClass;
        private System.Windows.Forms.ComboBox comboClass;
        private System.Windows.Forms.Label labelTeacher;
        private System.Windows.Forms.ComboBox comboTeacher;
        private System.Windows.Forms.Label labelDay;
        private System.Windows.Forms.ComboBox comboDayFilter;
        private System.Windows.Forms.Button buttonApplyFilter;
        private System.Windows.Forms.Button buttonResetFilter;
        private System.Windows.Forms.DataGridView dataGrid;
    }
}
