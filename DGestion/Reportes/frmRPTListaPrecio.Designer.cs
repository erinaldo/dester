namespace DGestion.Reportes
{
    partial class frmRPTListaPrecio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRPTListaPrecio));
            this.listaPrecioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dGestionDTGeneral = new DGestion.DGestionDTGeneral();
            this.rptConsultaListaPrecio = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tsMenuReporte = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsCboSelect = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.txBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.listaPrecioTableAdapter = new DGestion.DGestionDTGeneralTableAdapters.ListaPrecioTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.listaPrecioBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGestionDTGeneral)).BeginInit();
            this.tsMenuReporte.SuspendLayout();
            this.SuspendLayout();
            // 
            // listaPrecioBindingSource
            // 
            this.listaPrecioBindingSource.DataMember = "ListaPrecio";
            this.listaPrecioBindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // dGestionDTGeneral
            // 
            this.dGestionDTGeneral.DataSetName = "DGestionDTGeneral";
            this.dGestionDTGeneral.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rptConsultaListaPrecio
            // 
            this.rptConsultaListaPrecio.AutoSize = true;
            this.rptConsultaListaPrecio.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsConsultaListaPrecio";
            reportDataSource1.Value = this.listaPrecioBindingSource;
            this.rptConsultaListaPrecio.LocalReport.DataSources.Add(reportDataSource1);
            this.rptConsultaListaPrecio.LocalReport.ReportEmbeddedResource = "DGestion.Reportes.rptListaPrecio.rdlc";
            this.rptConsultaListaPrecio.Location = new System.Drawing.Point(0, 25);
            this.rptConsultaListaPrecio.Margin = new System.Windows.Forms.Padding(1);
            this.rptConsultaListaPrecio.Name = "rptConsultaListaPrecio";
            this.rptConsultaListaPrecio.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.rptConsultaListaPrecio.Size = new System.Drawing.Size(931, 762);
            this.rptConsultaListaPrecio.TabIndex = 148;
            this.rptConsultaListaPrecio.Visible = false;
            this.rptConsultaListaPrecio.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // tsMenuReporte
            // 
            this.tsMenuReporte.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenuReporte.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tsCboSelect,
            this.toolStripSeparator4,
            this.txBtnBuscar});
            this.tsMenuReporte.Location = new System.Drawing.Point(0, 0);
            this.tsMenuReporte.Name = "tsMenuReporte";
            this.tsMenuReporte.Size = new System.Drawing.Size(931, 25);
            this.tsMenuReporte.Stretch = true;
            this.tsMenuReporte.TabIndex = 149;
            this.tsMenuReporte.Text = "Menu Reporte";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(82, 22);
            this.toolStripLabel2.Text = "Tipo de Lista: ";
            // 
            // tsCboSelect
            // 
            this.tsCboSelect.AutoCompleteCustomSource.AddRange(new string[] {
            "LISTA GENERAL",
            "LISTA ESPECIAL",
            "LISTA PUBLICO",
            "LISTA CONTADO",
            "LISTA CON RECARGO"});
            this.tsCboSelect.BackColor = System.Drawing.SystemColors.Info;
            this.tsCboSelect.Items.AddRange(new object[] {
            "LISTA GENERAL",
            "LISTA ESPECIAL",
            "LISTA PUBLICO",
            "LISTA CONTADO",
            "LISTA CON RECARGO"});
            this.tsCboSelect.Name = "tsCboSelect";
            this.tsCboSelect.Size = new System.Drawing.Size(200, 25);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // txBtnBuscar
            // 
            this.txBtnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("txBtnBuscar.Image")));
            this.txBtnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.txBtnBuscar.Name = "txBtnBuscar";
            this.txBtnBuscar.Size = new System.Drawing.Size(62, 22);
            this.txBtnBuscar.Text = "Buscar";
            this.txBtnBuscar.Click += new System.EventHandler(this.txBtnBuscar_Click);
            // 
            // listaPrecioTableAdapter
            // 
            this.listaPrecioTableAdapter.ClearBeforeFill = true;
            // 
            // frmRPTListaPrecio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 787);
            this.Controls.Add(this.rptConsultaListaPrecio);
            this.Controls.Add(this.tsMenuReporte);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmRPTListaPrecio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte lista de Precios";
            this.Load += new System.EventHandler(this.frmRPTListaPrecio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listaPrecioBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGestionDTGeneral)).EndInit();
            this.tsMenuReporte.ResumeLayout(false);
            this.tsMenuReporte.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rptConsultaListaPrecio;
        private System.Windows.Forms.ToolStrip tsMenuReporte;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tsCboSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton txBtnBuscar;
        private System.Windows.Forms.BindingSource listaPrecioBindingSource;
        private DGestionDTGeneral dGestionDTGeneral;
        private DGestionDTGeneralTableAdapters.ListaPrecioTableAdapter listaPrecioTableAdapter;
    }
}