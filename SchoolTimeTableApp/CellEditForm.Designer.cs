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
            this.labelSlotTitle  = new System.Windows.Forms.Label();
            this.labelSlot       = new System.Windows.Forms.Label();
            this.labelSubject    = new System.Windows.Forms.Label();
            this.comboSubject    = new System.Windows.Forms.ComboBox();
            this.labelTeacher    = new System.Windows.Forms.Label();
            this.comboTeacher    = new System.Windows.Forms.ComboBox();
            this.labelClassroom  = new System.Windows.Forms.Label();
            this.comboClassroom  = new System.Windows.Forms.ComboBox();
            this.buttonSave      = new System.Windows.Forms.Button();
            this.buttonDelete    = new System.Windows.Forms.Button();
            this.buttonCancel    = new System.Windows.Forms.Button();
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
            this.labelSlot.Size      = new System.Drawing.Size(376, 26);
            this.labelSlot.TabIndex  = 1;
            this.labelSlot.Text      = "—";
            // labelSubject
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(12, 68);
            this.labelSubject.Name     = "labelSubject";
            this.labelSubject.TabIndex = 2;
            this.labelSubject.Text     = "Предмет:";
            // comboSubject
            this.comboSubject.Anchor          = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.comboSubject.DropDownStyle   = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSubject.FormattingEnabled = true;
            this.comboSubject.Location        = new System.Drawing.Point(12, 84);
            this.comboSubject.Name            = "comboSubject";
            this.comboSubject.Size            = new System.Drawing.Size(376, 21);
            this.comboSubject.TabIndex        = 3;
            // labelTeacher
            this.labelTeacher.AutoSize = true;
            this.labelTeacher.Location = new System.Drawing.Point(12, 118);
            this.labelTeacher.Name     = "labelTeacher";
            this.labelTeacher.TabIndex = 4;
            this.labelTeacher.Text     = "Учитель:";
            // comboTeacher
            this.comboTeacher.Anchor          = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.comboTeacher.DropDownStyle   = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTeacher.FormattingEnabled = true;
            this.comboTeacher.Location        = new System.Drawing.Point(12, 134);
            this.comboTeacher.Name            = "comboTeacher";
            this.comboTeacher.Size            = new System.Drawing.Size(376, 21);
            this.comboTeacher.TabIndex        = 5;
            // labelClassroom
            this.labelClassroom.AutoSize = true;
            this.labelClassroom.Location = new System.Drawing.Point(12, 168);
            this.labelClassroom.Name     = "labelClassroom";
            this.labelClassroom.TabIndex = 6;
            this.labelClassroom.Text     = "Кабинет:";
            // comboClassroom
            this.comboClassroom.Anchor          = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.comboClassroom.DropDownStyle   = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClassroom.FormattingEnabled = true;
            this.comboClassroom.Location        = new System.Drawing.Point(12, 184);
            this.comboClassroom.Name            = "comboClassroom";
            this.comboClassroom.Size            = new System.Drawing.Size(376, 21);
            this.comboClassroom.TabIndex        = 7;
            // buttonSave
            this.buttonSave.Anchor          = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.buttonSave.BackColor       = System.Drawing.Color.SteelBlue;
            this.buttonSave.FlatStyle       = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.ForeColor       = System.Drawing.Color.White;
            this.buttonSave.Location        = new System.Drawing.Point(12, 224);
            this.buttonSave.Name            = "buttonSave";
            this.buttonSave.Size            = new System.Drawing.Size(110, 28);
            this.buttonSave.TabIndex        = 8;
            this.buttonSave.Text            = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // buttonDelete
            this.buttonDelete.FlatStyle  = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.ForeColor  = System.Drawing.Color.Crimson;
            this.buttonDelete.Location   = new System.Drawing.Point(136, 224);
            this.buttonDelete.Name       = "buttonDelete";
            this.buttonDelete.Size       = new System.Drawing.Size(120, 28);
            this.buttonDelete.TabIndex   = 9;
            this.buttonDelete.Text       = "Удалить урок";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // buttonCancel
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location  = new System.Drawing.Point(278, 224);
            this.buttonCancel.Name      = "buttonCancel";
            this.buttonCancel.Size      = new System.Drawing.Size(110, 28);
            this.buttonCancel.TabIndex  = 10;
            this.buttonCancel.Text      = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // CellEditForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(400, 266);
            this.Controls.Add(this.labelSlotTitle);
            this.Controls.Add(this.labelSlot);
            this.Controls.Add(this.labelSubject);
            this.Controls.Add(this.comboSubject);
            this.Controls.Add(this.labelTeacher);
            this.Controls.Add(this.comboTeacher);
            this.Controls.Add(this.labelClassroom);
            this.Controls.Add(this.comboClassroom);
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
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label    labelSlotTitle;
        private System.Windows.Forms.Label    labelSlot;
        private System.Windows.Forms.Label    labelSubject;
        private System.Windows.Forms.ComboBox comboSubject;
        private System.Windows.Forms.Label    labelTeacher;
        private System.Windows.Forms.ComboBox comboTeacher;
        private System.Windows.Forms.Label    labelClassroom;
        private System.Windows.Forms.ComboBox comboClassroom;
        private System.Windows.Forms.Button   buttonSave;
        private System.Windows.Forms.Button   buttonDelete;
        private System.Windows.Forms.Button   buttonCancel;
    }
}
