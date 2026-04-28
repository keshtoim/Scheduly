namespace testing
{
    partial class QuickAddForm
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
            this.panelFields  = new System.Windows.Forms.Panel();
            this.buttonOk     = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // panelFields
            this.panelFields.Location = new System.Drawing.Point(0, 0);
            this.panelFields.Name     = "panelFields";
            this.panelFields.Size     = new System.Drawing.Size(344, 120);
            this.panelFields.TabIndex = 0;
            // buttonOk
            this.buttonOk.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.ForeColor = System.Drawing.Color.White;
            this.buttonOk.Location  = new System.Drawing.Point(12, 130);
            this.buttonOk.Name      = "buttonOk";
            this.buttonOk.Size      = new System.Drawing.Size(150, 28);
            this.buttonOk.TabIndex  = 1;
            this.buttonOk.Text      = "Добавить";
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // buttonCancel
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location  = new System.Drawing.Point(176, 130);
            this.buttonCancel.Name      = "buttonCancel";
            this.buttonCancel.Size      = new System.Drawing.Size(150, 28);
            this.buttonCancel.TabIndex  = 2;
            this.buttonCancel.Text      = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // QuickAddForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(344, 170);
            this.Controls.Add(this.panelFields);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "QuickAddForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Добавить";
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel  panelFields;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}
