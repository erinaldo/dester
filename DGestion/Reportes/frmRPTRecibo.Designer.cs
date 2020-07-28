namespace DGestion.Reportes
{
    partial class frmRPTRecibo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRPTRecibo));
            this.recibosRealizadosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dGestionDTGeneral = new DGestion.DGestionDTGeneral();
            this.rptRecibosRealizados = new Microsoft.Reporting.WinForms.ReportViewer();
            this.recibosRealizadosTableAdapter = new DGestion.DGestionDTGeneralTableAdapters.RecibosRealizadosTableAdapter();
            this.detalleReciboRealizadoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.detalleReciboRealizadoTableAdapter = new DGestion.DGestionDTGeneralTableAdapters.DetalleReciboRealizadoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.recibosRealizadosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGestionDTGeneral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detalleReciboRealizadoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // recibosRealizadosBindingSource
            // 
            this.recibosRealizadosBindingSource.DataMember = "RecibosRealizados";
            this.recibosRealizadosBindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // dGestionDTGeneral
            // 
            this.dGestionDTGeneral.DataSetName = "DGestionDTGeneral";
            this.dGestionDTGeneral.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rptRecibosRealizados
            // 
            this.rptRecibosRealizados.AutoSize = true;
            this.rptRecibosRealizados.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dRecibosRealizados";
            reportDataSource1.Value = this.recibosRealizadosBindingSource;
            reportDataSource2.Name = "dDetalleReciboRealizado";
            reportDataSource2.Value = this.detalleReciboRealizadoBindingSource;
            this.rptRecibosRealizados.LocalReport.DataSources.Add(reportDataSource1);
            this.rptRecibosRealizados.LocalReport.DataSources.Add(reportDataSource2);
            this.rptRecibosRealizados.LocalReport.ReportEmbeddedResource = "DGestion.Reportes.rptRecibo.rdlc";
            this.rptRecibosRealizados.Location = new System.Drawing.Point(0, 0);
            this.rptRecibosRealizados.Margin = new System.Windows.Forms.Padding(1);
            this.rptRecibosRealizados.Name = "rptRecibosRealizados";
            this.rptRecibosRealizados.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.rptRecibosRealizados.ServerReport.ReportServerUrl = new System.Uri("", System.UriKind.Relative);
            this.rptRecibosRealizados.Size = new System.Drawing.Size(982, 720);
            this.rptRecibosRealizados.TabIndex = 142;
            this.rptRecibosRealizados.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // recibosRealizadosTableAdapter
            // 
            this.recibosRealizadosTableAdapter.ClearBeforeFill = true;
            // 
            // detalleReciboRealizadoBindingSource
            // 
            this.detalleReciboRealizadoBindingSource.DataMember = "DetalleReciboRealizado";
            this.detalleReciboRealizadoBindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // detalleReciboRealizadoTableAdapter
            // 
            this.detalleReciboRealizadoTableAdapter.ClearBeforeFill = true;
            // 
            // frmRPTRecibo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 720);
            this.Controls.Add(this.rptRecibosRealizados);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRPTRecibo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Recibos";
            this.Load += new System.EventHandler(this.frmRPTRecibo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.recibosRealizadosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGestionDTGeneral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detalleReciboRealizadoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rptRecibosRealizados;
        private DGestionDTGeneral dGestionDTGeneral;
        private System.Windows.Forms.BindingSource recibosRealizadosBindingSource;
        private DGestionDTGeneralTableAdapters.RecibosRealizadosTableAdapter recibosRealizadosTableAdapter;
        private System.Windows.Forms.BindingSource detalleReciboRealizadoBindingSource;
        private DGestionDTGeneralTableAdapters.DetalleReciboRealizadoTableAdapter detalleReciboRealizadoTableAdapter;
    }
}