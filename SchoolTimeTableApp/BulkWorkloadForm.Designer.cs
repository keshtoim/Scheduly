namespace testing
{
    partial class BulkWorkloadForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.panelTop           = new System.Windows.Forms.Panel();
            this.labelTeacher       = new System.Windows.Forms.Label();
            this.comboTeacher       = new System.Windows.Forms.ComboBox();
            this.labelSubject       = new System.Windows.Forms.Label();
            this.comboSubject       = new System.Windows.Forms.ComboBox();
            this.labelHours         = new System.Windows.Forms.Label();
            this.textHours          = new System.Windows.Forms.TextBox();
            this.labelSubgroup      = new System.Windows.Forms.Label();
            this.comboSubgroup      = new System.Windows.Forms.ComboBox();
            this.labelClassesTitle  = new System.Windows.Forms.Label();
            this.checkedListClasses = new System.Windows.Forms.CheckedListBox();
            this.panelBottom        = new System.Windows.Forms.Panel();
            this.labelGrade         = new System.Windows.Forms.Label();
            this.comboGrade         = new System.Windows.Forms.ComboBox();
            this.buttonSelectAll    = new System.Windows.Forms.Button();
            this.buttonClearAll     = new System.Windows.Forms.Button();
            this.labelStatus        = new System.Windows.Forms.Label();
            this.buttonAdd          = new System.Windows.Forms.Button();
            this.buttonClose        = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // panelTop — поля ввода
            this.panelTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTop.Controls.Add(this.labelTeacher);
            this.panelTop.Controls.Add(this.comboTeacher);
            this.panelTop.Controls.Add(this.labelSubject);
            this.panelTop.Controls.Add(this.comboSubject);
            this.panelTop.Controls.Add(this.labelHours);
            this.panelTop.Controls.Add(this.textHours);
            this.panelTop.Controls.Add(this.labelSubgroup);
            this.panelTop.Controls.Add(this.comboSubgroup);
            this.panelTop.Dock     = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Name     = "panelTop";
            this.panelTop.Size     = new System.Drawing.Size(560, 106);
            this.panelTop.TabIndex = 0;
            // labelTeacher
            this.labelTeacher.AutoSize = true;
            this.labelTeacher.Location = new System.Drawing.Point(8, 14);
            this.labelTeacher.Name     = "labelTeacher";
            this.labelTeacher.Text     = "Учитель:";
            // comboTeacher
            this.comboTeacher.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTeacher.FormattingEnabled = true;
            this.comboTeacher.Location          = new System.Drawing.Point(72, 10);
            this.comboTeacher.Name              = "comboTeacher";
            this.comboTeacher.Size              = new System.Drawing.Size(468, 21);
            this.comboTeacher.TabIndex          = 0;
            // labelSubject
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(8, 46);
            this.labelSubject.Name     = "labelSubject";
            this.labelSubject.Text     = "Предмет:";
            // comboSubject
            this.comboSubject.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSubject.FormattingEnabled = true;
            this.comboSubject.Location          = new System.Drawing.Point(72, 42);
            this.comboSubject.Name              = "comboSubject";
            this.comboSubject.Size              = new System.Drawing.Size(468, 21);
            this.comboSubject.TabIndex          = 1;
            this.comboSubject.SelectedIndexChanged += new System.EventHandler(this.comboSubject_SelectedIndexChanged);
            // labelHours
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(8, 80);
            this.labelHours.Name     = "labelHours";
            this.labelHours.Text     = "Часов/нед:";
            // textHours
            this.textHours.Location = new System.Drawing.Point(84, 76);
            this.textHours.Name     = "textHours";
            this.textHours.Size     = new System.Drawing.Size(48, 20);
            this.textHours.TabIndex = 2;
            this.textHours.Text     = "2";
            // labelSubgroup
            this.labelSubgroup.AutoSize = true;
            this.labelSubgroup.Location = new System.Drawing.Point(146, 80);
            this.labelSubgroup.Name     = "labelSubgroup";
            this.labelSubgroup.Text     = "Подгруппа:";
            // comboSubgroup
            this.comboSubgroup.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSubgroup.FormattingEnabled = true;
            this.comboSubgroup.Location          = new System.Drawing.Point(226, 76);
            this.comboSubgroup.Name              = "comboSubgroup";
            this.comboSubgroup.Size              = new System.Drawing.Size(150, 21);
            this.comboSubgroup.TabIndex          = 3;
            // labelClassesTitle
            this.labelClassesTitle.AutoSize  = true;
            this.labelClassesTitle.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelClassesTitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelClassesTitle.Location  = new System.Drawing.Point(8, 114);
            this.labelClassesTitle.Name      = "labelClassesTitle";
            this.labelClassesTitle.Text      = "Классы (выберите один или несколько):";
            // checkedListClasses
            this.checkedListClasses.CheckOnClick    = true;
            this.checkedListClasses.FormattingEnabled = true;
            this.checkedListClasses.Location        = new System.Drawing.Point(8, 134);
            this.checkedListClasses.Name            = "checkedListClasses";
            this.checkedListClasses.Size            = new System.Drawing.Size(544, 238);
            this.checkedListClasses.TabIndex        = 4;
            this.checkedListClasses.MultiColumn     = true;
            this.checkedListClasses.ColumnWidth     = 80;
            // panelBottom — кнопки управления и добавления
            this.panelBottom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelBottom.Controls.Add(this.labelGrade);
            this.panelBottom.Controls.Add(this.comboGrade);
            this.panelBottom.Controls.Add(this.buttonSelectAll);
            this.panelBottom.Controls.Add(this.buttonClearAll);
            this.panelBottom.Controls.Add(this.labelStatus);
            this.panelBottom.Controls.Add(this.buttonAdd);
            this.panelBottom.Controls.Add(this.buttonClose);
            this.panelBottom.Dock     = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Name     = "panelBottom";
            this.panelBottom.Size     = new System.Drawing.Size(560, 88);
            this.panelBottom.TabIndex = 1;
            // labelGrade
            this.labelGrade.AutoSize = true;
            this.labelGrade.Location = new System.Drawing.Point(8, 10);
            this.labelGrade.Name     = "labelGrade";
            this.labelGrade.Text     = "Параллель:";
            // comboGrade
            this.comboGrade.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGrade.FormattingEnabled = true;
            this.comboGrade.Location          = new System.Drawing.Point(80, 6);
            this.comboGrade.Name              = "comboGrade";
            this.comboGrade.Size              = new System.Drawing.Size(100, 21);
            this.comboGrade.TabIndex          = 5;
            this.comboGrade.SelectedIndexChanged += new System.EventHandler(this.comboGrade_SelectedIndexChanged);
            // buttonSelectAll
            this.buttonSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelectAll.Location  = new System.Drawing.Point(190, 5);
            this.buttonSelectAll.Name      = "buttonSelectAll";
            this.buttonSelectAll.Size      = new System.Drawing.Size(120, 24);
            this.buttonSelectAll.TabIndex  = 6;
            this.buttonSelectAll.Text      = "Выбрать все";
            this.buttonSelectAll.Click    += new System.EventHandler(this.buttonSelectAll_Click);
            // buttonClearAll
            this.buttonClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClearAll.Location  = new System.Drawing.Point(318, 5);
            this.buttonClearAll.Name      = "buttonClearAll";
            this.buttonClearAll.Size      = new System.Drawing.Size(120, 24);
            this.buttonClearAll.TabIndex  = 7;
            this.buttonClearAll.Text      = "Снять все";
            this.buttonClearAll.Click    += new System.EventHandler(this.buttonClearAll_Click);
            // labelStatus
            this.labelStatus.AutoSize = false;
            this.labelStatus.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelStatus.Location = new System.Drawing.Point(8, 40);
            this.labelStatus.Name     = "labelStatus";
            this.labelStatus.Size     = new System.Drawing.Size(340, 38);
            this.labelStatus.Text     = "";
            // buttonAdd
            this.buttonAdd.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.Location  = new System.Drawing.Point(356, 40);
            this.buttonAdd.Name      = "buttonAdd";
            this.buttonAdd.Size      = new System.Drawing.Size(100, 36);
            this.buttonAdd.TabIndex  = 8;
            this.buttonAdd.Text      = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click    += new System.EventHandler(this.buttonAdd_Click);
            // buttonClose
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location  = new System.Drawing.Point(464, 40);
            this.buttonClose.Name      = "buttonClose";
            this.buttonClose.Size      = new System.Drawing.Size(84, 36);
            this.buttonClose.TabIndex  = 9;
            this.buttonClose.Text      = "Закрыть";
            this.buttonClose.Click    += new System.EventHandler(this.buttonClose_Click);
            // BulkWorkloadForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(560, 480);
            this.Controls.Add(this.checkedListClasses);
            this.Controls.Add(this.labelClassesTitle);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "BulkWorkloadForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Массовое заполнение нагрузки";
            this.Load           += new System.EventHandler(this.BulkWorkloadForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Panel           panelTop;
        private System.Windows.Forms.Label           labelTeacher;
        private System.Windows.Forms.ComboBox        comboTeacher;
        private System.Windows.Forms.Label           labelSubject;
        private System.Windows.Forms.ComboBox        comboSubject;
        private System.Windows.Forms.Label           labelHours;
        private System.Windows.Forms.TextBox         textHours;
        private System.Windows.Forms.Label           labelSubgroup;
        private System.Windows.Forms.ComboBox        comboSubgroup;
        private System.Windows.Forms.Label           labelClassesTitle;
        private System.Windows.Forms.CheckedListBox  checkedListClasses;
        private System.Windows.Forms.Panel           panelBottom;
        private System.Windows.Forms.Label           labelGrade;
        private System.Windows.Forms.ComboBox        comboGrade;
        private System.Windows.Forms.Button          buttonSelectAll;
        private System.Windows.Forms.Button          buttonClearAll;
        private System.Windows.Forms.Label           labelStatus;
        private System.Windows.Forms.Button          buttonAdd;
        private System.Windows.Forms.Button          buttonClose;
    }
}
