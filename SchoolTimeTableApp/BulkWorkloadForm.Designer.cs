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
            this.groupStep1      = new System.Windows.Forms.GroupBox();
            this.labelTeacher    = new System.Windows.Forms.Label();
            this.comboTeacher    = new System.Windows.Forms.ComboBox();
            this.labelSubject    = new System.Windows.Forms.Label();
            this.comboSubject    = new System.Windows.Forms.ComboBox();
            this.labelHours      = new System.Windows.Forms.Label();
            this.textHours       = new System.Windows.Forms.TextBox();
            this.labelSubgroup   = new System.Windows.Forms.Label();
            this.comboSubgroup   = new System.Windows.Forms.ComboBox();
            this.groupStep2      = new System.Windows.Forms.GroupBox();
            this.labelGrade      = new System.Windows.Forms.Label();
            this.comboGrade      = new System.Windows.Forms.ComboBox();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.buttonClearAll  = new System.Windows.Forms.Button();
            this.checkedListClasses = new System.Windows.Forms.CheckedListBox();
            this.labelStatus     = new System.Windows.Forms.Label();
            this.buttonAdd       = new System.Windows.Forms.Button();
            this.buttonClose     = new System.Windows.Forms.Button();
            this.groupStep1.SuspendLayout();
            this.groupStep2.SuspendLayout();
            this.SuspendLayout();
            //
            // Шрифт формы (наследуется всеми дочерними элементами)
            //
            this.Font = new System.Drawing.Font("Segoe UI", 11F);
            //
            // groupStep1 — данные урока
            //
            this.groupStep1.Controls.Add(this.labelTeacher);
            this.groupStep1.Controls.Add(this.comboTeacher);
            this.groupStep1.Controls.Add(this.labelSubject);
            this.groupStep1.Controls.Add(this.comboSubject);
            this.groupStep1.Controls.Add(this.labelHours);
            this.groupStep1.Controls.Add(this.textHours);
            this.groupStep1.Controls.Add(this.labelSubgroup);
            this.groupStep1.Controls.Add(this.comboSubgroup);
            this.groupStep1.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupStep1.Location  = new System.Drawing.Point(8, 8);
            this.groupStep1.Name      = "groupStep1";
            this.groupStep1.Size      = new System.Drawing.Size(596, 172);
            this.groupStep1.TabIndex  = 0;
            this.groupStep1.TabStop   = false;
            this.groupStep1.Text      = "Шаг 1.  Данные урока";
            // labelTeacher
            this.labelTeacher.AutoSize = true;
            this.labelTeacher.ForeColor = System.Drawing.Color.Black;
            this.labelTeacher.Location = new System.Drawing.Point(12, 32);
            this.labelTeacher.Name     = "labelTeacher";
            this.labelTeacher.Text     = "Учитель:";
            // comboTeacher
            this.comboTeacher.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTeacher.FormattingEnabled = true;
            this.comboTeacher.Location          = new System.Drawing.Point(104, 26);
            this.comboTeacher.Name              = "comboTeacher";
            this.comboTeacher.Size              = new System.Drawing.Size(470, 29);
            this.comboTeacher.TabIndex          = 0;
            // labelSubject
            this.labelSubject.AutoSize = true;
            this.labelSubject.ForeColor = System.Drawing.Color.Black;
            this.labelSubject.Location = new System.Drawing.Point(12, 76);
            this.labelSubject.Name     = "labelSubject";
            this.labelSubject.Text     = "Предмет:";
            // comboSubject
            this.comboSubject.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSubject.FormattingEnabled = true;
            this.comboSubject.Location          = new System.Drawing.Point(104, 70);
            this.comboSubject.Name              = "comboSubject";
            this.comboSubject.Size              = new System.Drawing.Size(470, 29);
            this.comboSubject.TabIndex          = 1;
            this.comboSubject.SelectedIndexChanged += new System.EventHandler(this.comboSubject_SelectedIndexChanged);
            // labelHours
            this.labelHours.AutoSize = true;
            this.labelHours.ForeColor = System.Drawing.Color.Black;
            this.labelHours.Location = new System.Drawing.Point(12, 120);
            this.labelHours.Name     = "labelHours";
            this.labelHours.Text     = "Часов/нед:";
            // textHours
            this.textHours.Location = new System.Drawing.Point(118, 114);
            this.textHours.Name     = "textHours";
            this.textHours.Size     = new System.Drawing.Size(62, 29);
            this.textHours.TabIndex = 2;
            this.textHours.Text     = "2";
            // labelSubgroup
            this.labelSubgroup.AutoSize = true;
            this.labelSubgroup.ForeColor = System.Drawing.Color.Black;
            this.labelSubgroup.Location = new System.Drawing.Point(196, 120);
            this.labelSubgroup.Name     = "labelSubgroup";
            this.labelSubgroup.Text     = "Подгруппа:";
            // comboSubgroup
            this.comboSubgroup.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSubgroup.FormattingEnabled = true;
            this.comboSubgroup.Location          = new System.Drawing.Point(298, 114);
            this.comboSubgroup.Name              = "comboSubgroup";
            this.comboSubgroup.Size              = new System.Drawing.Size(200, 29);
            this.comboSubgroup.TabIndex          = 3;
            //
            // groupStep2 — выбор классов
            //
            this.groupStep2.Controls.Add(this.labelGrade);
            this.groupStep2.Controls.Add(this.comboGrade);
            this.groupStep2.Controls.Add(this.buttonSelectAll);
            this.groupStep2.Controls.Add(this.buttonClearAll);
            this.groupStep2.Controls.Add(this.checkedListClasses);
            this.groupStep2.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupStep2.Location  = new System.Drawing.Point(8, 186);
            this.groupStep2.Name      = "groupStep2";
            this.groupStep2.Size      = new System.Drawing.Size(596, 330);
            this.groupStep2.TabIndex  = 1;
            this.groupStep2.TabStop   = false;
            this.groupStep2.Text      = "Шаг 2.  Выберите классы";
            // labelGrade
            this.labelGrade.AutoSize = true;
            this.labelGrade.ForeColor = System.Drawing.Color.Black;
            this.labelGrade.Location = new System.Drawing.Point(12, 34);
            this.labelGrade.Name     = "labelGrade";
            this.labelGrade.Text     = "Параллель:";
            // comboGrade
            this.comboGrade.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGrade.FormattingEnabled = true;
            this.comboGrade.Location          = new System.Drawing.Point(120, 28);
            this.comboGrade.Name              = "comboGrade";
            this.comboGrade.Size              = new System.Drawing.Size(80, 29);
            this.comboGrade.TabIndex          = 4;
            this.comboGrade.SelectedIndexChanged += new System.EventHandler(this.comboGrade_SelectedIndexChanged);
            // buttonSelectAll
            this.buttonSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelectAll.ForeColor = System.Drawing.Color.Black;
            this.buttonSelectAll.Location  = new System.Drawing.Point(212, 26);
            this.buttonSelectAll.Name      = "buttonSelectAll";
            this.buttonSelectAll.Size      = new System.Drawing.Size(140, 32);
            this.buttonSelectAll.TabIndex  = 5;
            this.buttonSelectAll.Text      = "Выбрать все";
            this.buttonSelectAll.Click    += new System.EventHandler(this.buttonSelectAll_Click);
            // buttonClearAll
            this.buttonClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClearAll.ForeColor = System.Drawing.Color.Black;
            this.buttonClearAll.Location  = new System.Drawing.Point(360, 26);
            this.buttonClearAll.Name      = "buttonClearAll";
            this.buttonClearAll.Size      = new System.Drawing.Size(140, 32);
            this.buttonClearAll.TabIndex  = 6;
            this.buttonClearAll.Text      = "Снять все";
            this.buttonClearAll.Click    += new System.EventHandler(this.buttonClearAll_Click);
            // checkedListClasses
            this.checkedListClasses.BorderStyle       = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListClasses.CheckOnClick      = true;
            this.checkedListClasses.FormattingEnabled = true;
            this.checkedListClasses.Location          = new System.Drawing.Point(12, 68);
            this.checkedListClasses.MultiColumn       = true;
            this.checkedListClasses.ColumnWidth       = 110;
            this.checkedListClasses.Name              = "checkedListClasses";
            this.checkedListClasses.Size              = new System.Drawing.Size(566, 250);
            this.checkedListClasses.TabIndex          = 7;
            //
            // labelStatus
            //
            this.labelStatus.AutoSize  = false;
            this.labelStatus.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelStatus.Location  = new System.Drawing.Point(14, 524);
            this.labelStatus.Name      = "labelStatus";
            this.labelStatus.Size      = new System.Drawing.Size(592, 30);
            this.labelStatus.Text      = "";
            //
            // buttonAdd
            //
            this.buttonAdd.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font      = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location  = new System.Drawing.Point(14, 558);
            this.buttonAdd.Name      = "buttonAdd";
            this.buttonAdd.Size      = new System.Drawing.Size(380, 44);
            this.buttonAdd.TabIndex  = 8;
            this.buttonAdd.Text      = "+ Добавить для выбранных классов";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click    += new System.EventHandler(this.buttonAdd_Click);
            //
            // buttonClose
            //
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location  = new System.Drawing.Point(404, 558);
            this.buttonClose.Name      = "buttonClose";
            this.buttonClose.Size      = new System.Drawing.Size(200, 44);
            this.buttonClose.TabIndex  = 9;
            this.buttonClose.Text      = "Закрыть";
            this.buttonClose.Click    += new System.EventHandler(this.buttonClose_Click);
            //
            // BulkWorkloadForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(620, 614);
            this.Controls.Add(this.groupStep1);
            this.Controls.Add(this.groupStep2);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "BulkWorkloadForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Массовое заполнение нагрузки";
            this.Load           += new System.EventHandler(this.BulkWorkloadForm_Load);
            this.groupStep1.ResumeLayout(false);
            this.groupStep1.PerformLayout();
            this.groupStep2.ResumeLayout(false);
            this.groupStep2.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.GroupBox        groupStep1;
        private System.Windows.Forms.Label           labelTeacher;
        private System.Windows.Forms.ComboBox        comboTeacher;
        private System.Windows.Forms.Label           labelSubject;
        private System.Windows.Forms.ComboBox        comboSubject;
        private System.Windows.Forms.Label           labelHours;
        private System.Windows.Forms.TextBox         textHours;
        private System.Windows.Forms.Label           labelSubgroup;
        private System.Windows.Forms.ComboBox        comboSubgroup;
        private System.Windows.Forms.GroupBox        groupStep2;
        private System.Windows.Forms.Label           labelGrade;
        private System.Windows.Forms.ComboBox        comboGrade;
        private System.Windows.Forms.Button          buttonSelectAll;
        private System.Windows.Forms.Button          buttonClearAll;
        private System.Windows.Forms.CheckedListBox  checkedListClasses;
        private System.Windows.Forms.Label           labelStatus;
        private System.Windows.Forms.Button          buttonAdd;
        private System.Windows.Forms.Button          buttonClose;
    }
}
