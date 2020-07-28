namespace DGestion.Reportes
{
    partial class frmRPTRemito
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRPTRemito));
            this.remitoClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dGestionDTGeneral = new DGestion.DGestionDTGeneral();
            this.detalleRemitoClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rptRemito = new Microsoft.Reporting.WinForms.ReportViewer();
            this.remitoClienteTableAdapter = new DGestion.DGestionDTGeneralTableAdapters.RemitoClienteTableAdapter();
            this.detalleRemitoClienteTableAdapter = new DGestion.DGestionDTGeneralTableAdapters.DetalleRemitoClienteTableAdapter();
            this.rptRemitoAPreimpreso = new Microsoft.Reporting.WinForms.ReportViewer();
            this.empresaActivaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.empresaActivaTableAdapter = new DGestion.DGestionDTGeneralTableAdapters.EmpresaActivaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.remitoClienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGestionDTGeneral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detalleRemitoClienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaActivaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // remitoClienteBindingSource
            // 
            this.remitoClienteBindingSource.DataMember = "RemitoCliente";
            this.remitoClienteBindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // dGestionDTGeneral
            // 
            this.dGestionDTGeneral.DataSetName = "DGestionDTGeneral";
            this.dGestionDTGeneral.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // detalleRemitoClienteBindingSource
            // 
            this.detalleRemitoClienteBindingSource.DataMember = "DetalleRemitoCliente";
            this.detalleRemitoClienteBindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // rptRemito
            // 
            this.rptRemito.AutoSize = true;
            this.rptRemito.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dRemitoCliente";
            reportDataSource1.Value = this.remitoClienteBindingSource;
            reportDataSource2.Name = "dDetalleRemitoCliente";
            reportDataSource2.Value = this.detalleRemitoClienteBindingSource;
            reportDataSource3.Name = "dEmpresaActiva";
            reportDataSource3.Value = this.empresaActivaBindingSource;
            this.rptRemito.LocalReport.DataSources.Add(reportDataSource1);
            this.rptRemito.LocalReport.DataSources.Add(reportDataSource2);
            this.rptRemito.LocalReport.DataSources.Add(reportDataSource3);
            this.rptRemito.LocalReport.ReportEmbeddedResource = "DGestion.Reportes.rptRemito.rdlc";
            this.rptRemito.Location = new System.Drawing.Point(0, 0);
            this.rptRemito.Margin = new System.Windows.Forms.Padding(1);
            this.rptRemito.Name = "rptRemito";
            this.rptRemito.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.rptRemito.Size = new System.Drawing.Size(984, 724);
            this.rptRemito.TabIndex = 142;
            this.rptRemito.Visible = false;
            this.rptRemito.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // remitoClienteTableAdapter
            // 
            this.remitoClienteTableAdapter.ClearBeforeFill = true;
            // 
            // detalleRemitoClienteTableAdapter
            // 
            this.detalleRemitoClienteTableAdapter.ClearBeforeFill = true;
            // 
            // rptRemitoAPreimpreso
            // 
            this.rptRemitoAPreimpreso.AutoSize = true;
            this.rptRemitoAPreimpreso.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource4.Name = "dRemitoCliente";
            reportDataSource4.Value = this.remitoClienteBindingSource;
            reportDataSource5.Name = "dDetalleRemitoCliente";
            reportDataSource5.Value = this.detalleRemitoClienteBindingSource;
            this.rptRemitoAPreimpreso.LocalReport.DataSources.Add(reportDataSource4);
            this.rptRemitoAPreimpreso.LocalReport.DataSources.Add(reportDataSource5);
            this.rptRemitoAPreimpreso.LocalReport.ReportEmbeddedResource = "DGestion.Reportes.rptRemitoAPreimpreso.rdlc";
            this.rptRemitoAPreimpreso.Location = new System.Drawing.Point(0, 0);
            this.rptRemitoAPreimpreso.Margin = new System.Windows.Forms.Padding(1);
            this.rptRemitoAPreimpreso.Name = "rptRemitoAPreimpreso";
            this.rptRemitoAPreimpreso.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.rptRemitoAPreimpreso.Size = new System.Drawing.Size(984, 724);
            this.rptRemitoAPreimpreso.TabIndex = 143;
            this.rptRemitoAPreimpreso.Visible = false;
            this.rptRemitoAPreimpreso.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // empresaActivaBindingSource
            // 
            this.empresaActivaBindingSource.DataMember = "EmpresaActiva";
            this.empresaActivaBindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // empresaActivaTableAdapter
            // 
            this.empresaActivaTableAdapter.ClearBeforeFill = true;
            // 
            // frmRPTRemito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 724);
            this.Controls.Add(this.rptRemitoAPreimpreso);
            this.Controls.Add(this.rptRemito);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "frmRPTRemito";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remito";
            this.Load += new System.EventHandler(this.frmRPTRemito_Load);
            ((System.ComponentModel.ISupportInitialize)(this.remitoClienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGestionDTGeneral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detalleRemitoClienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaActivaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rptRemito;
        private System.Windows.Forms.BindingSource remitoClienteBindingSource;
        private DGestionDTGeneral dGestionDTGeneral;
        private DGestionDTGeneralTableAdapters.RemitoClienteTableAdapter remitoClienteTableAdapter;
        private System.Windows.Forms.BindingSource detalleRemitoClienteBindingSource;
        private DGestionDTGeneralTableAdapters.DetalleRemitoClienteTableAdapter detalleRemitoClienteTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer rptRemitoAPreimpreso;
        private System.Windows.Forms.BindingSource empresaActivaBindingSource;
        private DGestionDTGeneralTableAdapters.EmpresaActivaTableAdapter empresaActivaTableAdapter;
    }
}