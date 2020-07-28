namespace DGestion.Reportes
{
    partial class frmRPTNotaCredito
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRPTNotaCredito));
            this.rptNotaCreditoImpreso_B = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dGestionDTGeneral = new DGestion.DGestionDTGeneral();
            this.notaCreditoClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.notaCreditoClienteTableAdapter = new DGestion.DGestionDTGeneralTableAdapters.NotaCreditoClienteTableAdapter();
            this.detalleNotaCreditoClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.detalleNotaCreditoClienteTableAdapter = new DGestion.DGestionDTGeneralTableAdapters.DetalleNotaCreditoClienteTableAdapter();
            this.empresaActivaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.empresaActivaTableAdapter = new DGestion.DGestionDTGeneralTableAdapters.EmpresaActivaTableAdapter();
            this.rptNotaCreditoImpreso_A = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dGestionDTGeneral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notaCreditoClienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detalleNotaCreditoClienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaActivaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // rptNotaCreditoImpreso_B
            // 
            this.rptNotaCreditoImpreso_B.AutoSize = true;
            this.rptNotaCreditoImpreso_B.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dNotaCreditoCliente_B";
            reportDataSource1.Value = this.notaCreditoClienteBindingSource;
            reportDataSource2.Name = "dDetalleNotaCreditoCliente_B";
            reportDataSource2.Value = this.detalleNotaCreditoClienteBindingSource;
            reportDataSource3.Name = "dEmpresaActiva";
            reportDataSource3.Value = this.empresaActivaBindingSource;
            this.rptNotaCreditoImpreso_B.LocalReport.DataSources.Add(reportDataSource1);
            this.rptNotaCreditoImpreso_B.LocalReport.DataSources.Add(reportDataSource2);
            this.rptNotaCreditoImpreso_B.LocalReport.DataSources.Add(reportDataSource3);
            this.rptNotaCreditoImpreso_B.LocalReport.ReportEmbeddedResource = "DGestion.Reportes.rptNotaCreditoCliente_B.rdlc";
            this.rptNotaCreditoImpreso_B.Location = new System.Drawing.Point(0, 0);
            this.rptNotaCreditoImpreso_B.Margin = new System.Windows.Forms.Padding(1);
            this.rptNotaCreditoImpreso_B.Name = "rptNotaCreditoImpreso_B";
            this.rptNotaCreditoImpreso_B.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.rptNotaCreditoImpreso_B.Size = new System.Drawing.Size(913, 717);
            this.rptNotaCreditoImpreso_B.TabIndex = 144;
            this.rptNotaCreditoImpreso_B.Visible = false;
            this.rptNotaCreditoImpreso_B.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // dGestionDTGeneral
            // 
            this.dGestionDTGeneral.DataSetName = "DGestionDTGeneral";
            this.dGestionDTGeneral.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // notaCreditoClienteBindingSource
            // 
            this.notaCreditoClienteBindingSource.DataMember = "NotaCreditoCliente";
            this.notaCreditoClienteBindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // notaCreditoClienteTableAdapter
            // 
            this.notaCreditoClienteTableAdapter.ClearBeforeFill = true;
            // 
            // detalleNotaCreditoClienteBindingSource
            // 
            this.detalleNotaCreditoClienteBindingSource.DataMember = "DetalleNotaCreditoCliente";
            this.detalleNotaCreditoClienteBindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // detalleNotaCreditoClienteTableAdapter
            // 
            this.detalleNotaCreditoClienteTableAdapter.ClearBeforeFill = true;
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
            // rptNotaCreditoImpreso_A
            // 
            this.rptNotaCreditoImpreso_A.AutoSize = true;
            this.rptNotaCreditoImpreso_A.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource4.Name = "dNotaCreditoCliente_A";
            reportDataSource4.Value = this.notaCreditoClienteBindingSource;
            reportDataSource5.Name = "dDetalleNotaCreditoCliente_A";
            reportDataSource5.Value = this.detalleNotaCreditoClienteBindingSource;
            reportDataSource6.Name = "dEmpresaActiva";
            reportDataSource6.Value = this.empresaActivaBindingSource;
            this.rptNotaCreditoImpreso_A.LocalReport.DataSources.Add(reportDataSource4);
            this.rptNotaCreditoImpreso_A.LocalReport.DataSources.Add(reportDataSource5);
            this.rptNotaCreditoImpreso_A.LocalReport.DataSources.Add(reportDataSource6);
            this.rptNotaCreditoImpreso_A.LocalReport.ReportEmbeddedResource = "DGestion.Reportes.rptNotaCreditoCliente_A.rdlc";
            this.rptNotaCreditoImpreso_A.Location = new System.Drawing.Point(0, 0);
            this.rptNotaCreditoImpreso_A.Margin = new System.Windows.Forms.Padding(1);
            this.rptNotaCreditoImpreso_A.Name = "rptNotaCreditoImpreso_A";
            this.rptNotaCreditoImpreso_A.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.rptNotaCreditoImpreso_A.Size = new System.Drawing.Size(913, 717);
            this.rptNotaCreditoImpreso_A.TabIndex = 145;
            this.rptNotaCreditoImpreso_A.Visible = false;
            this.rptNotaCreditoImpreso_A.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // frmRPTNotaCredito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 717);
            this.Controls.Add(this.rptNotaCreditoImpreso_A);
            this.Controls.Add(this.rptNotaCreditoImpreso_B);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRPTNotaCredito";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nota de Crédito";
            this.Load += new System.EventHandler(this.frmRPTNotaCredito_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGestionDTGeneral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notaCreditoClienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detalleNotaCreditoClienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaActivaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rptNotaCreditoImpreso_B;
        private System.Windows.Forms.BindingSource notaCreditoClienteBindingSource;
        private DGestionDTGeneral dGestionDTGeneral;
        private System.Windows.Forms.BindingSource detalleNotaCreditoClienteBindingSource;
        private System.Windows.Forms.BindingSource empresaActivaBindingSource;
        private DGestionDTGeneralTableAdapters.NotaCreditoClienteTableAdapter notaCreditoClienteTableAdapter;
        private DGestionDTGeneralTableAdapters.DetalleNotaCreditoClienteTableAdapter detalleNotaCreditoClienteTableAdapter;
        private DGestionDTGeneralTableAdapters.EmpresaActivaTableAdapter empresaActivaTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer rptNotaCreditoImpreso_A;
    }
}