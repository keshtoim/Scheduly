namespace testing
{
    partial class ComposeScheduleControl
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.labelClassesTitle = new System.Windows.Forms.Label();
            this.listBoxClasses = new System.Windows.Forms.ListBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Panel1MinSize = 140;
            this.splitContainer.Panel2MinSize = 400;
            this.splitContainer.Size = new System.Drawing.Size(1192, 684);
            this.splitContainer.SplitterDistance = 160;
            this.splitContainer.TabIndex = 0;
            // Panel1 — class list
            this.splitContainer.Panel1.Controls.Add(this.listBoxClasses);
            this.splitContainer.Panel1.Controls.Add(this.labelClassesTitle);
            // Panel2 — schedule grid
            this.splitContainer.Panel2.Controls.Add(this.dataGrid);
            // 
            // labelClassesTitle
            // 
            this.labelClassesTitle.AutoSize = false;
            this.labelClassesTitle.BackColor = System.Drawing.Color.SteelBlue;
            this.labelClassesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelClassesTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelClassesTitle.ForeColor = System.Drawing.Color.White;
            this.labelClassesTitle.Location = new System.Drawing.Point(0, 0);
            this.labelClassesTitle.Name = "labelClassesTitle";
            this.labelClassesTitle.Size = new System.Drawing.Size(160, 28);
            this.labelClassesTitle.TabIndex = 0;
            this.labelClassesTitle.Text = "  Классы";
            this.labelClassesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxClasses
            // 
            this.listBoxClasses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxClasses.FormattingEnabled = true;
            this.listBoxClasses.ItemHeight = 18;
            this.listBoxClasses.Location = new System.Drawing.Point(0, 28);
            this.listBoxClasses.Name = "listBoxClasses";
            this.listBoxClasses.Size = new System.Drawing.Size(160, 656);
            this.listBoxClasses.TabIndex = 1;
            this.listBoxClasses.SelectedIndexChanged += new System.EventHandler(this.listBoxClasses_SelectedIndexChanged);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Location = new System.Drawing.Point(0, 0);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidth = 40;
            this.dataGrid.RowTemplate.Height = 52;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGrid.Size = new System.Drawing.Size(1028, 684);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellClick);
            // 
            // ComposeScheduleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "ComposeScheduleControl";
            this.Size = new System.Drawing.Size(1192, 684);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label labelClassesTitle;
        private System.Windows.Forms.ListBox listBoxClasses;
        private System.Windows.Forms.DataGridView dataGrid;
    }
}
