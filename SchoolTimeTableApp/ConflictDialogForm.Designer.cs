namespace testing
{
    partial class ConflictDialogForm
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
            this.pictureBoxIcon    = new System.Windows.Forms.PictureBox();
            this.labelDescription  = new System.Windows.Forms.Label();
            this.buttonEditThis    = new System.Windows.Forms.Button();
            this.buttonEditOther   = new System.Windows.Forms.Button();
            this.buttonCancel      = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // pictureBoxIcon
            this.pictureBoxIcon.Image    = System.Drawing.SystemIcons.Warning.ToBitmap();
            this.pictureBoxIcon.Location = new System.Drawing.Point(12, 16);
            this.pictureBoxIcon.Name     = "pictureBoxIcon";
            this.pictureBoxIcon.Size     = new System.Drawing.Size(32, 32);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxIcon.TabIndex = 0;
            this.pictureBoxIcon.TabStop  = false;
            // labelDescription
            this.labelDescription.AutoSize  = false;
            this.labelDescription.Location  = new System.Drawing.Point(54, 12);
            this.labelDescription.Name      = "labelDescription";
            this.labelDescription.Size      = new System.Drawing.Size(330, 80);
            this.labelDescription.TabIndex  = 1;
            this.labelDescription.Text      = "Обнаружен конфликт.";
            // buttonEditThis
            this.buttonEditThis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditThis.Location  = new System.Drawing.Point(12, 106);
            this.buttonEditThis.Name      = "buttonEditThis";
            this.buttonEditThis.Size      = new System.Drawing.Size(180, 28);
            this.buttonEditThis.TabIndex  = 2;
            this.buttonEditThis.Text      = "Изменить эту запись";
            this.buttonEditThis.UseVisualStyleBackColor = true;
            this.buttonEditThis.Click += new System.EventHandler(this.buttonEditThis_Click);
            // buttonEditOther
            this.buttonEditOther.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditOther.Location  = new System.Drawing.Point(12, 142);
            this.buttonEditOther.Name      = "buttonEditOther";
            this.buttonEditOther.Size      = new System.Drawing.Size(180, 28);
            this.buttonEditOther.TabIndex  = 3;
            this.buttonEditOther.Text      = "Изменить другую запись";
            this.buttonEditOther.UseVisualStyleBackColor = true;
            this.buttonEditOther.Click += new System.EventHandler(this.buttonEditOther_Click);
            // buttonCancel
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location  = new System.Drawing.Point(12, 178);
            this.buttonCancel.Name      = "buttonCancel";
            this.buttonCancel.Size      = new System.Drawing.Size(180, 28);
            this.buttonCancel.TabIndex  = 4;
            this.buttonCancel.Text      = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // ConflictDialogForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(396, 220);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.buttonEditThis);
            this.Controls.Add(this.buttonEditOther);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "ConflictDialogForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Конфликт расписания";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Label      labelDescription;
        private System.Windows.Forms.Button     buttonEditThis;
        private System.Windows.Forms.Button     buttonEditOther;
        private System.Windows.Forms.Button     buttonCancel;
    }
}
