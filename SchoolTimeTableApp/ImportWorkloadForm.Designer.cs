namespace testing
{
    partial class ImportWorkloadForm
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
            this.panelTop      = new System.Windows.Forms.Panel();
            this.buttonOpen    = new System.Windows.Forms.Button();
            this.labelPath     = new System.Windows.Forms.Label();
            this.buttonTemplate = new System.Windows.Forms.Button();
            this.previewGrid   = new System.Windows.Forms.DataGridView();
            this.panelBottom   = new System.Windows.Forms.Panel();
            this.labelStatus   = new System.Windows.Forms.Label();
            this.buttonImport  = new System.Windows.Forms.Button();
            this.buttonClose   = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewGrid)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();

            // ── panelTop ──────────────────────────────────────────────────
            this.panelTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTop.Controls.Add(this.buttonOpen);
            this.panelTop.Controls.Add(this.labelPath);
            this.panelTop.Controls.Add(this.buttonTemplate);
            this.panelTop.Dock     = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Name     = "panelTop";
            this.panelTop.Size     = new System.Drawing.Size(940, 46);
            this.panelTop.TabIndex = 0;

            // buttonOpen
            this.buttonOpen.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpen.ForeColor = System.Drawing.Color.White;
            this.buttonOpen.Location  = new System.Drawing.Point(8, 9);
            this.buttonOpen.Name      = "buttonOpen";
            this.buttonOpen.Size      = new System.Drawing.Size(150, 26);
            this.buttonOpen.TabIndex  = 0;
            this.buttonOpen.Text      = "📂  Открыть .xlsx";
            this.buttonOpen.UseVisualStyleBackColor = false;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);

            // labelPath
            this.labelPath.AutoSize  = false;
            this.labelPath.ForeColor = System.Drawing.Color.Gray;
            this.labelPath.Location  = new System.Drawing.Point(168, 14);
            this.labelPath.Name      = "labelPath";
            this.labelPath.Size      = new System.Drawing.Size(560, 18);
            this.labelPath.Text      = "Файл не выбран";
            this.labelPath.TabIndex  = 1;

            // buttonTemplate
            this.buttonTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTemplate.ForeColor = System.Drawing.Color.SeaGreen;
            this.buttonTemplate.Location  = new System.Drawing.Point(738, 9);
            this.buttonTemplate.Name      = "buttonTemplate";
            this.buttonTemplate.Size      = new System.Drawing.Size(192, 26);
            this.buttonTemplate.TabIndex  = 2;
            this.buttonTemplate.Text      = "📋  Скачать шаблон (.xlsx)";
            this.buttonTemplate.Click += new System.EventHandler(this.buttonTemplate_Click);

            // ── previewGrid ───────────────────────────────────────────────
            this.previewGrid.AllowUserToAddRows    = false;
            this.previewGrid.AllowUserToDeleteRows = false;
            this.previewGrid.Dock            = System.Windows.Forms.DockStyle.Fill;
            this.previewGrid.Location        = new System.Drawing.Point(0, 46);
            this.previewGrid.Name            = "previewGrid";
            this.previewGrid.ReadOnly        = true;
            this.previewGrid.RowHeadersWidth = 30;
            this.previewGrid.SelectionMode   = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.previewGrid.TabIndex        = 1;

            // ── panelBottom ───────────────────────────────────────────────
            this.panelBottom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelBottom.Controls.Add(this.labelStatus);
            this.panelBottom.Controls.Add(this.buttonImport);
            this.panelBottom.Controls.Add(this.buttonClose);
            this.panelBottom.Dock     = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Name     = "panelBottom";
            this.panelBottom.Size     = new System.Drawing.Size(940, 46);
            this.panelBottom.TabIndex = 2;

            // labelStatus
            this.labelStatus.AutoSize  = false;
            this.labelStatus.ForeColor = System.Drawing.Color.Gray;
            this.labelStatus.Location  = new System.Drawing.Point(8, 14);
            this.labelStatus.Name      = "labelStatus";
            this.labelStatus.Size      = new System.Drawing.Size(500, 18);
            this.labelStatus.Text      = "Откройте .xlsx файл для предпросмотра";
            this.labelStatus.TabIndex  = 0;

            // buttonImport
            this.buttonImport.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonImport.Enabled   = false;
            this.buttonImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonImport.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonImport.ForeColor = System.Drawing.Color.White;
            this.buttonImport.Location  = new System.Drawing.Point(520, 9);
            this.buttonImport.Name      = "buttonImport";
            this.buttonImport.Size      = new System.Drawing.Size(300, 26);
            this.buttonImport.TabIndex  = 1;
            this.buttonImport.Text      = "▶  Нет строк для импорта";
            this.buttonImport.UseVisualStyleBackColor = false;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);

            // buttonClose
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location  = new System.Drawing.Point(830, 9);
            this.buttonClose.Name      = "buttonClose";
            this.buttonClose.Size      = new System.Drawing.Size(100, 26);
            this.buttonClose.TabIndex  = 2;
            this.buttonClose.Text      = "Закрыть";
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);

            // ── ImportWorkloadForm ─────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(940, 620);
            this.Controls.Add(this.previewGrid);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.MinimumSize     = new System.Drawing.Size(800, 500);
            this.Name            = "ImportWorkloadForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Импорт нагрузки из Excel";
            this.Load           += new System.EventHandler(this.ImportWorkloadForm_Load);

            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.previewGrid)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel         panelTop;
        private System.Windows.Forms.Button        buttonOpen;
        private System.Windows.Forms.Label         labelPath;
        private System.Windows.Forms.Button        buttonTemplate;
        private System.Windows.Forms.DataGridView  previewGrid;
        private System.Windows.Forms.Panel         panelBottom;
        private System.Windows.Forms.Label         labelStatus;
        private System.Windows.Forms.Button        buttonImport;
        private System.Windows.Forms.Button        buttonClose;
    }
}
