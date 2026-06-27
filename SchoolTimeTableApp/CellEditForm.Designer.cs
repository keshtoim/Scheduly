namespace testing
{
    partial class CellEditForm
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
            this.labelSlotTitle      = new System.Windows.Forms.Label();
            this.labelSlot           = new System.Windows.Forms.Label();
            this.labelSubject        = new System.Windows.Forms.Label();
            this.comboSubject        = new System.Windows.Forms.ComboBox();
            this.labelTeacher        = new System.Windows.Forms.Label();
            this.comboTeacher        = new System.Windows.Forms.ComboBox();
            this.buttonAddTeacher    = new System.Windows.Forms.Button();
            this.labelClassroom      = new System.Windows.Forms.Label();
            this.comboClassroom      = new System.Windows.Forms.ComboBox();
            this.buttonAddClassroom  = new System.Windows.Forms.Button();
            this.groupWeekParity     = new System.Windows.Forms.GroupBox();
            this.radioWeekAll        = new System.Windows.Forms.RadioButton();
            this.radioWeekOdd        = new System.Windows.Forms.RadioButton();
            this.radioWeekEven       = new System.Windows.Forms.RadioButton();
            this.buttonSave          = new System.Windows.Forms.Button();
            this.buttonDelete        = new System.Windows.Forms.Button();
            this.buttonCancel        = new System.Windows.Forms.Button();
            this.groupWeekParity.SuspendLayout();
            this.SuspendLayout();
            // labelSlotTitle
            this.labelSlotTitle.AutoSize  = true;
            this.labelSlotTitle.ForeColor = System.Drawing.Color.Gray;
            this.labelSlotTitle.Location  = new System.Drawing.Point(12, 14);
            this.labelSlotTitle.Name      = "labelSlotTitle";
            this.labelSlotTitle.TabIndex  = 0;
            this.labelSlotTitle.Text      = "Слот:";
            // labelSlot
            this.labelSlot.AutoSize  = false;
            this.labelSlot.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelSlot.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelSlot.Location  = new System.Drawing.Point(12, 30);
            this.labelSlot.Name      = "labelSlot";
            this.labelSlot.Size      = new System.Drawing.Size(420, 26);
            this.labelSlot.TabIndex  = 1;
            this.labelSlot.Text      = "—";
            // labelSubject
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(12, 68);
            this.labelSubject.Name     = "labelSubject";
            this.labelSubject.TabIndex = 2;
            this.labelSubject.Text     = "Предмет:";
            // comboSubject
            this.comboSubject.AutoCompleteMode   = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboSubject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboSubject.DropDownStyle      = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboSubject.FormattingEnabled  = true;
            this.comboSubject.Location           = new System.Drawing.Point(12, 84);
            this.comboSubject.Name               = "comboSubject";
            this.comboSubject.Size               = new System.Drawing.Size(420, 21);
            this.comboSubject.TabIndex           = 3;
            // labelTeacher
            this.labelTeacher.AutoSize = true;
            this.labelTeacher.Location = new System.Drawing.Point(12, 118);
            this.labelTeacher.Name     = "labelTeacher";
            this.labelTeacher.TabIndex = 4;
            this.labelTeacher.Text     = "Учитель:";
            // comboTeacher
            this.comboTeacher.AutoCompleteMode   = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboTeacher.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboTeacher.DropDownStyle      = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboTeacher.FormattingEnabled  = true;
            this.comboTeacher.Location           = new System.Drawing.Point(12, 134);
            this.comboTeacher.Name               = "comboTeacher";
            this.comboTeacher.Size               = new System.Drawing.Size(388, 21);
            this.comboTeacher.TabIndex           = 5;
            // buttonAddTeacher
            this.buttonAddTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddTeacher.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAddTeacher.ForeColor = System.Drawing.Color.SteelBlue;
            this.buttonAddTeacher.Location  = new System.Drawing.Point(406, 132);
            this.buttonAddTeacher.Name      = "buttonAddTeacher";
            this.buttonAddTeacher.Size      = new System.Drawing.Size(26, 24);
            this.buttonAddTeacher.TabIndex  = 6;
            this.buttonAddTeacher.Text      = "+";
            this.buttonAddTeacher.UseVisualStyleBackColor = true;
            this.buttonAddTeacher.Click += new System.EventHandler(this.buttonAddTeacher_Click);
            // labelClassroom
            this.labelClassroom.AutoSize = true;
            this.labelClassroom.Location = new System.Drawing.Point(12, 168);
            this.labelClassroom.Name     = "labelClassroom";
            this.labelClassroom.TabIndex = 7;
            this.labelClassroom.Text     = "Кабинет:";
            // comboClassroom
            this.comboClassroom.AutoCompleteMode   = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboClassroom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboClassroom.DropDownStyle      = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboClassroom.FormattingEnabled  = true;
            this.comboClassroom.Location           = new System.Drawing.Point(12, 184);
            this.comboClassroom.Name               = "comboClassroom";
            this.comboClassroom.Size               = new System.Drawing.Size(388, 21);
            this.comboClassroom.TabIndex           = 8;
            // buttonAddClassroom
            this.buttonAddClassroom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddClassroom.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAddClassroom.ForeColor = System.Drawing.Color.SteelBlue;
            this.buttonAddClassroom.Location  = new System.Drawing.Point(406, 182);
            this.buttonAddClassroom.Name      = "buttonAddClassroom";
            this.buttonAddClassroom.Size      = new System.Drawing.Size(26, 24);
            this.buttonAddClassroom.TabIndex  = 9;
            this.buttonAddClassroom.Text      = "+";
            this.buttonAddClassroom.UseVisualStyleBackColor = true;
            this.buttonAddClassroom.Click += new System.EventHandler(this.buttonAddClassroom_Click);
            // groupWeekParity — выбор чётности/нечётности недели
            this.groupWeekParity.Controls.Add(this.radioWeekAll);
            this.groupWeekParity.Controls.Add(this.radioWeekOdd);
            this.groupWeekParity.Controls.Add(this.radioWeekEven);
            this.groupWeekParity.Location = new System.Drawing.Point(12, 214);
            this.groupWeekParity.Name     = "groupWeekParity";
            this.groupWeekParity.Size     = new System.Drawing.Size(420, 48);
            this.groupWeekParity.TabIndex = 10;
            this.groupWeekParity.TabStop  = false;
            this.groupWeekParity.Text     = "Недели";
            // radioWeekAll — 0: каждую неделю
            this.radioWeekAll.AutoSize = true;
            this.radioWeekAll.Checked  = true;
            this.radioWeekAll.Location = new System.Drawing.Point(10, 20);
            this.radioWeekAll.Name     = "radioWeekAll";
            this.radioWeekAll.Size     = new System.Drawing.Size(120, 17);
            this.radioWeekAll.TabIndex = 0;
            this.radioWeekAll.TabStop  = true;
            this.radioWeekAll.Text     = "Каждую неделю";
            // radioWeekOdd — 1: только нечётные недели
            this.radioWeekOdd.AutoSize = true;
            this.radioWeekOdd.Location = new System.Drawing.Point(148, 20);
            this.radioWeekOdd.Name     = "radioWeekOdd";
            this.radioWeekOdd.Size     = new System.Drawing.Size(90, 17);
            this.radioWeekOdd.TabIndex = 1;
            this.radioWeekOdd.Text     = "Нечётные";
            // radioWeekEven — 2: только чётные недели
            this.radioWeekEven.AutoSize = true;
            this.radioWeekEven.Location = new System.Drawing.Point(256, 20);
            this.radioWeekEven.Name     = "radioWeekEven";
            this.radioWeekEven.Size     = new System.Drawing.Size(70, 17);
            this.radioWeekEven.TabIndex = 2;
            this.radioWeekEven.Text     = "Чётные";
            // buttonSave
            this.buttonSave.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location  = new System.Drawing.Point(12, 274);
            this.buttonSave.Name      = "buttonSave";
            this.buttonSave.Size      = new System.Drawing.Size(110, 28);
            this.buttonSave.TabIndex  = 11;
            this.buttonSave.Text      = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // buttonDelete
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.ForeColor = System.Drawing.Color.Crimson;
            this.buttonDelete.Location  = new System.Drawing.Point(136, 274);
            this.buttonDelete.Name      = "buttonDelete";
            this.buttonDelete.Size      = new System.Drawing.Size(120, 28);
            this.buttonDelete.TabIndex  = 12;
            this.buttonDelete.Text      = "Удалить урок";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // buttonCancel
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location  = new System.Drawing.Point(310, 274);
            this.buttonCancel.Name      = "buttonCancel";
            this.buttonCancel.Size      = new System.Drawing.Size(118, 28);
            this.buttonCancel.TabIndex  = 13;
            this.buttonCancel.Text      = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // CellEditForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(444, 314);
            this.Controls.Add(this.labelSlotTitle);
            this.Controls.Add(this.labelSlot);
            this.Controls.Add(this.labelSubject);
            this.Controls.Add(this.comboSubject);
            this.Controls.Add(this.labelTeacher);
            this.Controls.Add(this.comboTeacher);
            this.Controls.Add(this.buttonAddTeacher);
            this.Controls.Add(this.labelClassroom);
            this.Controls.Add(this.comboClassroom);
            this.Controls.Add(this.buttonAddClassroom);
            this.Controls.Add(this.groupWeekParity);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "CellEditForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Урок";
            this.Load += new System.EventHandler(this.CellEditForm_Load);
            this.groupWeekParity.ResumeLayout(false);
            this.groupWeekParity.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label       labelSlotTitle;
        private System.Windows.Forms.Label       labelSlot;
        private System.Windows.Forms.Label       labelSubject;
        private System.Windows.Forms.ComboBox    comboSubject;
        private System.Windows.Forms.Label       labelTeacher;
        private System.Windows.Forms.ComboBox    comboTeacher;
        private System.Windows.Forms.Button      buttonAddTeacher;
        private System.Windows.Forms.Label       labelClassroom;
        private System.Windows.Forms.ComboBox    comboClassroom;
        private System.Windows.Forms.Button      buttonAddClassroom;
        private System.Windows.Forms.GroupBox    groupWeekParity;
        private System.Windows.Forms.RadioButton radioWeekAll;
        private System.Windows.Forms.RadioButton radioWeekOdd;
        private System.Windows.Forms.RadioButton radioWeekEven;
        private System.Windows.Forms.Button      buttonSave;
        private System.Windows.Forms.Button      buttonDelete;
        private System.Windows.Forms.Button      buttonCancel;
    }
}
