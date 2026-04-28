namespace testing
{
    partial class WorkloadControl
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
            this.panelAdd      = new System.Windows.Forms.Panel();
            this.labelClass    = new System.Windows.Forms.Label();
            this.comboClass    = new System.Windows.Forms.ComboBox();
            this.labelSubject  = new System.Windows.Forms.Label();
            this.comboSubject  = new System.Windows.Forms.ComboBox();
            this.labelTeacher  = new System.Windows.Forms.Label();
            this.comboTeacher  = new System.Windows.Forms.ComboBox();
            this.labelHours    = new System.Windows.Forms.Label();
            this.textHours     = new System.Windows.Forms.TextBox();
            this.buttonAdd     = new System.Windows.Forms.Button();
            this.buttonDelete  = new System.Windows.Forms.Button();
            this.panelSearch   = new System.Windows.Forms.Panel();
            this.labelSearch   = new System.Windows.Forms.Label();
            this.textSearch    = new System.Windows.Forms.TextBox();
            this.labelSearchHint = new System.Windows.Forms.Label();
            this.dataGrid      = new System.Windows.Forms.DataGridView();
            this.panelAdd.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panelAdd
            // 
            this.panelAdd.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelAdd.Controls.Add(this.labelClass);
            this.panelAdd.Controls.Add(this.comboClass);
            this.panelAdd.Controls.Add(this.labelSubject);
            this.panelAdd.Controls.Add(this.comboSubject);
            this.panelAdd.Controls.Add(this.labelTeacher);
            this.panelAdd.Controls.Add(this.comboTeacher);
            this.panelAdd.Controls.Add(this.labelHours);
            this.panelAdd.Controls.Add(this.textHours);
            this.panelAdd.Controls.Add(this.buttonAdd);
            this.panelAdd.Controls.Add(this.buttonDelete);
            this.panelAdd.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAdd.Location = new System.Drawing.Point(0, 0);
            this.panelAdd.Name = "panelAdd";
            this.panelAdd.Size = new System.Drawing.Size(1192, 46);
            this.panelAdd.TabIndex = 0;
            // labelClass
            this.labelClass.AutoSize = true;
            this.labelClass.Location = new System.Drawing.Point(8, 14);
            this.labelClass.Name = "labelClass"; this.labelClass.TabIndex = 0;
            this.labelClass.Text = "Класс:";
            // comboClass
            this.comboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClass.FormattingEnabled = true;
            this.comboClass.Location = new System.Drawing.Point(56, 10);
            this.comboClass.Name = "comboClass";
            this.comboClass.Size = new System.Drawing.Size(120, 21);
            this.comboClass.TabIndex = 1;
            // labelSubject
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(186, 14);
            this.labelSubject.Name = "labelSubject"; this.labelSubject.TabIndex = 2;
            this.labelSubject.Text = "Предмет:";
            // comboSubject
            this.comboSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSubject.FormattingEnabled = true;
            this.comboSubject.Location = new System.Drawing.Point(248, 10);
            this.comboSubject.Name = "comboSubject";
            this.comboSubject.Size = new System.Drawing.Size(180, 21);
            this.comboSubject.TabIndex = 3;
            // labelTeacher
            this.labelTeacher.AutoSize = true;
            this.labelTeacher.Location = new System.Drawing.Point(438, 14);
            this.labelTeacher.Name = "labelTeacher"; this.labelTeacher.TabIndex = 4;
            this.labelTeacher.Text = "Учитель:";
            // comboTeacher
            this.comboTeacher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTeacher.FormattingEnabled = true;
            this.comboTeacher.Location = new System.Drawing.Point(496, 10);
            this.comboTeacher.Name = "comboTeacher";
            this.comboTeacher.Size = new System.Drawing.Size(200, 21);
            this.comboTeacher.TabIndex = 5;
            // labelHours
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(706, 14);
            this.labelHours.Name = "labelHours"; this.labelHours.TabIndex = 6;
            this.labelHours.Text = "Часов/нед:";
            // textHours
            this.textHours.Location = new System.Drawing.Point(776, 11);
            this.textHours.Name = "textHours";
            this.textHours.Size = new System.Drawing.Size(50, 20);
            this.textHours.TabIndex = 7;
            // buttonAdd
            this.buttonAdd.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(838, 9);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(100, 26);
            this.buttonAdd.TabIndex = 8;
            this.buttonAdd.Text = "+ Добавить";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // buttonDelete
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.ForeColor = System.Drawing.Color.Crimson;
            this.buttonDelete.Location = new System.Drawing.Point(946, 9);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(100, 26);
            this.buttonDelete.TabIndex = 9;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(240, 246, 255);
            this.panelSearch.Controls.Add(this.labelSearch);
            this.panelSearch.Controls.Add(this.textSearch);
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
            this.textSearch.Size = new System.Drawing.Size(400, 20);
            this.textSearch.TabIndex = 1;
            this.textSearch.TextChanged += new System.EventHandler(this.textSearch_TextChanged);
            // labelSearchHint
            this.labelSearchHint.AutoSize = true;
            this.labelSearchHint.Location = new System.Drawing.Point(482, 10);
            this.labelSearchHint.Name = "labelSearchHint";
            this.labelSearchHint.TabIndex = 2;
            this.labelSearchHint.Text = "";
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Location = new System.Drawing.Point(0, 82);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersWidth = 40;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.Size = new System.Drawing.Size(1192, 602);
            this.dataGrid.TabIndex = 2;
            // 
            // WorkloadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelAdd);
            this.Name = "WorkloadControl";
            this.Size = new System.Drawing.Size(1192, 684);
            this.panelAdd.ResumeLayout(false);
            this.panelAdd.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel      panelAdd;
        private System.Windows.Forms.Label      labelClass;
        private System.Windows.Forms.ComboBox   comboClass;
        private System.Windows.Forms.Label      labelSubject;
        private System.Windows.Forms.ComboBox   comboSubject;
        private System.Windows.Forms.Label      labelTeacher;
        private System.Windows.Forms.ComboBox   comboTeacher;
        private System.Windows.Forms.Label      labelHours;
        private System.Windows.Forms.TextBox    textHours;
        private System.Windows.Forms.Button     buttonAdd;
        private System.Windows.Forms.Button     buttonDelete;
        private System.Windows.Forms.Panel      panelSearch;
        private System.Windows.Forms.Label      labelSearch;
        private System.Windows.Forms.TextBox    textSearch;
        private System.Windows.Forms.Label      labelSearchHint;
        private System.Windows.Forms.DataGridView dataGrid;
    }
}
