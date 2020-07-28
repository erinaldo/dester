namespace DGestion.Reportes
{
    partial class frmRPTNotaPedido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRPTNotaPedido));
            this.notaPedidoClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dGestionDTGeneral = new DGestion.DGestionDTGeneral();
            this.rptNotaPedido = new Microsoft.Reporting.WinForms.ReportViewer();
            this.notaPedidoClienteTableAdapter = new DGestion.DGestionDTGeneralTableAdapters.NotaPedidoClienteTableAdapter();
            this.empresaActivaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.empresaActivaTableAdapter = new DGestion.DGestionDTGeneralTableAdapters.EmpresaActivaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.notaPedidoClienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGestionDTGeneral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaActivaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // notaPedidoClienteBindingSource
            // 
            this.notaPedidoClienteBindingSource.DataMember = "NotaPedidoCliente";
            this.notaPedidoClienteBindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // dGestionDTGeneral
            // 
            this.dGestionDTGeneral.DataSetName = "DGestionDTGeneral";
            this.dGestionDTGeneral.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rptNotaPedido
            // 
            this.rptNotaPedido.AutoSize = true;
            this.rptNotaPedido.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dNotaPedidoCliente";
            reportDataSource1.Value = this.notaPedidoClienteBindingSource;
            reportDataSource2.Name = "dEmpresaActiva";
            reportDataSource2.Value = this.empresaActivaBindingSource;
            this.rptNotaPedido.LocalReport.DataSources.Add(reportDataSource1);
            this.rptNotaPedido.LocalReport.DataSources.Add(reportDataSource2);
            this.rptNotaPedido.LocalReport.ReportEmbeddedResource = "DGestion.Reportes.rptNotaPedido.rdlc";
            this.rptNotaPedido.Location = new System.Drawing.Point(0, 0);
            this.rptNotaPedido.Margin = new System.Windows.Forms.Padding(1);
            this.rptNotaPedido.Name = "rptNotaPedido";
            this.rptNotaPedido.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.rptNotaPedido.Size = new System.Drawing.Size(984, 724);
            this.rptNotaPedido.TabIndex = 143;
            this.rptNotaPedido.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // notaPedidoClienteTableAdapter
            // 
            this.notaPedidoClienteTableAdapter.ClearBeforeFill = true;
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
            // frmRPTNotaPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 724);
            this.Controls.Add(this.rptNotaPedido);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "frmRPTNotaPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nota de Pedido";
            this.Load += new System.EventHandler(this.frmRPTNotaPedido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.notaPedidoClienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGestionDTGeneral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaActivaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rptNotaPedido;
        private System.Windows.Forms.BindingSource notaPedidoClienteBindingSource;
        private DGestionDTGeneral dGestionDTGeneral;
        private DGestionDTGeneralTableAdapters.NotaPedidoClienteTableAdapter notaPedidoClienteTableAdapter;
        private System.Windows.Forms.BindingSource empresaActivaBindingSource;
        private DGestionDTGeneralTableAdapters.EmpresaActivaTableAdapter empresaActivaTableAdapter;
    }
}