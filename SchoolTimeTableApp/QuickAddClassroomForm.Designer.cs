namespace testing
{
    partial class QuickAddClassroomForm
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
            this.labelRoomNumber = new System.Windows.Forms.Label();
            this.txtRoomNumber   = new System.Windows.Forms.TextBox();
            this.labelCapacity   = new System.Windows.Forms.Label();
            this.txtCapacity     = new System.Windows.Forms.TextBox();
            this.labelType       = new System.Windows.Forms.Label();
            this.comboType       = new System.Windows.Forms.ComboBox();
            this.buttonOk        = new System.Windows.Forms.Button();
            this.buttonCancel    = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // labelRoomNumber
            this.labelRoomNumber.AutoSize = true;
            this.labelRoomNumber.Location = new System.Drawing.Point(12, 14);
            this.labelRoomNumber.Name     = "labelRoomNumber";
            this.labelRoomNumber.TabIndex = 0;
            this.labelRoomNumber.Text     = "Номер кабинета:";
            // txtRoomNumber
            this.txtRoomNumber.Location = new System.Drawing.Point(12, 30);
            this.txtRoomNumber.Name     = "txtRoomNumber";
            this.txtRoomNumber.Size     = new System.Drawing.Size(320, 20);
            this.txtRoomNumber.TabIndex = 1;
            // labelCapacity
            this.labelCapacity.AutoSize = true;
            this.labelCapacity.Location = new System.Drawing.Point(12, 62);
            this.labelCapacity.Name     = "labelCapacity";
            this.labelCapacity.TabIndex = 2;
            this.labelCapacity.Text     = "Вместимость:";
            // txtCapacity
            this.txtCapacity.Location = new System.Drawing.Point(12, 78);
            this.txtCapacity.Name     = "txtCapacity";
            this.txtCapacity.Size     = new System.Drawing.Size(320, 20);
            this.txtCapacity.TabIndex = 3;
            // labelType
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(12, 110);
            this.labelType.Name     = "labelType";
            this.labelType.TabIndex = 4;
            this.labelType.Text     = "Тип кабинета:";
            // comboType
            this.comboType.DropDownStyle    = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.FormattingEnabled = true;
            this.comboType.Location         = new System.Drawing.Point(12, 126);
            this.comboType.Name             = "comboType";
            this.comboType.Size             = new System.Drawing.Size(320, 21);
            this.comboType.TabIndex         = 5;
            // buttonOk
            this.buttonOk.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.ForeColor = System.Drawing.Color.White;
            this.buttonOk.Location  = new System.Drawing.Point(12, 164);
            this.buttonOk.Name      = "buttonOk";
            this.buttonOk.Size      = new System.Drawing.Size(150, 28);
            this.buttonOk.TabIndex  = 6;
            this.buttonOk.Text      = "Добавить";
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // buttonCancel
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location  = new System.Drawing.Point(176, 164);
            this.buttonCancel.Name      = "buttonCancel";
            this.buttonCancel.Size      = new System.Drawing.Size(150, 28);
            this.buttonCancel.TabIndex  = 7;
            this.buttonCancel.Text      = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // QuickAddClassroomForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(344, 206);
            this.Controls.Add(this.labelRoomNumber);
            this.Controls.Add(this.txtRoomNumber);
            this.Controls.Add(this.labelCapacity);
            this.Controls.Add(this.txtCapacity);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.comboType);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "QuickAddClassroomForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Новый кабинет";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label    labelRoomNumber;
        private System.Windows.Forms.TextBox  txtRoomNumber;
        private System.Windows.Forms.Label    labelCapacity;
        private System.Windows.Forms.TextBox  txtCapacity;
        private System.Windows.Forms.Label    labelType;
        private System.Windows.Forms.ComboBox comboType;
        private System.Windows.Forms.Button   buttonOk;
        private System.Windows.Forms.Button   buttonCancel;
    }
}
