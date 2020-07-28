namespace DGestion.Reportes
{
    partial class frmRTFactuConsulta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRTFactuConsulta));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.rptGralFactu1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dGestionDTGeneral = new DGestion.DGestionDTGeneral();
            this.rpt_GralFactu_1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rpt_GralFactu_3BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rpt_GralFactu_4BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rpt_Grafico_1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tsMenuReporte = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsCboSelect = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.txtcboEmpresa = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tstcboPtoVta = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsCboMes = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lblAnio = new System.Windows.Forms.ToolStripLabel();
            this.tstxtAño = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.rptConsultaFacturacion_1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rptConsultaFacturacion_2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rptConsultaFacturacion_3 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rpt_GralFactu_1TableAdapter = new DGestion.DGestionDTGeneralTableAdapters.rpt_GralFactu_1TableAdapter();
            this.rptGralFactu2BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rpt_GralFactu_2TableAdapter = new DGestion.DGestionDTGeneralTableAdapters.rpt_GralFactu_2TableAdapter();
            this.rptGralFactu3BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rpt_GralFactu_3TableAdapter = new DGestion.DGestionDTGeneralTableAdapters.rpt_GralFactu_3TableAdapter();
            this.rptConsultaFacturacion_4 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rptGralFactu4BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rpt_GralFactu_4TableAdapter = new DGestion.DGestionDTGeneralTableAdapters.rpt_GralFactu_4TableAdapter();
            this.rptGrafico_1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rptGrafico1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rpt_Grafico_1TableAdapter = new DGestion.DGestionDTGeneralTableAdapters.rpt_Grafico_1TableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.rptGralFactu1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGestionDTGeneral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpt_GralFactu_1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpt_GralFactu_3BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpt_GralFactu_4BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpt_Grafico_1BindingSource)).BeginInit();
            this.tsMenuReporte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rptGralFactu2BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptGralFactu3BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptGralFactu4BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptGrafico1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // rptGralFactu1BindingSource
            // 
            this.rptGralFactu1BindingSource.DataMember = "rpt_GralFactu_1";
            this.rptGralFactu1BindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // dGestionDTGeneral
            // 
            this.dGestionDTGeneral.DataSetName = "DGestionDTGeneral";
            this.dGestionDTGeneral.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rpt_GralFactu_1BindingSource
            // 
            this.rpt_GralFactu_1BindingSource.DataMember = "rpt_GralFactu_1";
            this.rpt_GralFactu_1BindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // rpt_GralFactu_3BindingSource
            // 
            this.rpt_GralFactu_3BindingSource.DataMember = "rpt_GralFactu_3";
            this.rpt_GralFactu_3BindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // rpt_GralFactu_4BindingSource
            // 
            this.rpt_GralFactu_4BindingSource.DataMember = "rpt_GralFactu_4";
            this.rpt_GralFactu_4BindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // rpt_Grafico_1BindingSource
            // 
            this.rpt_Grafico_1BindingSource.DataMember = "rpt_Grafico_1";
            this.rpt_Grafico_1BindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // tsMenuReporte
            // 
            this.tsMenuReporte.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenuReporte.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tsCboSelect,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.txtcboEmpresa,
            this.toolStripSeparator4,
            this.toolStripLabel4,
            this.tstcboPtoVta,
            this.toolStripSeparator5,
            this.toolStripLabel1,
            this.tsCboMes,
            this.toolStripSeparator3,
            this.lblAnio,
            this.tstxtAño,
            this.toolStripSeparator1,
            this.txBtnBuscar});
            this.tsMenuReporte.Location = new System.Drawing.Point(0, 0);
            this.tsMenuReporte.Name = "tsMenuReporte";
            this.tsMenuReporte.Size = new System.Drawing.Size(1029, 25);
            this.tsMenuReporte.Stretch = true;
            this.tsMenuReporte.TabIndex = 147;
            this.tsMenuReporte.Text = "Menu Reporte";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(87, 22);
            this.toolStripLabel2.Text = "Tipo Consulta: ";
            // 
            // tsCboSelect
            // 
            this.tsCboSelect.BackColor = System.Drawing.SystemColors.Info;
            this.tsCboSelect.Items.AddRange(new object[] {
            "Facturación Anual por Cliente",
            "Facturación Mensual Por Cliente",
            "Total Facturación Mensual y Anual x Cliente",
            "Total de Facturacion Anual x Empresa y Pto.Vta.",
            "------------------------------------",
            "Gráfico de Fact. Mensual x Empresa y Pto. Vta."});
            this.tsCboSelect.Name = "tsCboSelect";
            this.tsCboSelect.Size = new System.Drawing.Size(275, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(60, 22);
            this.toolStripLabel3.Text = "Empresa.:";
            // 
            // txtcboEmpresa
            // 
            this.txtcboEmpresa.BackColor = System.Drawing.SystemColors.Info;
            this.txtcboEmpresa.Items.AddRange(new object[] {
            "Dester Argentina S.A.",
            "D&A Group S.A.",
            "Todas las Empresas",
            "-"});
            this.txtcboEmpresa.Name = "txtcboEmpresa";
            this.txtcboEmpresa.Size = new System.Drawing.Size(150, 25);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(57, 22);
            this.toolStripLabel4.Text = "Pto. Vta.:";
            // 
            // tstcboPtoVta
            // 
            this.tstcboPtoVta.BackColor = System.Drawing.SystemColors.Info;
            this.tstcboPtoVta.Items.AddRange(new object[] {
            "0001",
            "0002",
            "0003",
            "0004",
            "-"});
            this.tstcboPtoVta.Name = "tstcboPtoVta";
            this.tstcboPtoVta.Size = new System.Drawing.Size(80, 25);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel1.Text = "Mes:";
            this.toolStripLabel1.Visible = false;
            // 
            // tsCboMes
            // 
            this.tsCboMes.AutoCompleteCustomSource.AddRange(new string[] {
            "Enero",
            "Febrero",
            "Marzo",
            "Abril",
            "Mayo",
            "Junio",
            "Julio",
            "Agosto",
            "Setiembre",
            "Octubre",
            "Noviembre",
            "Diciembre"});
            this.tsCboMes.BackColor = System.Drawing.SystemColors.Info;
            this.tsCboMes.Items.AddRange(new object[] {
            "Enero",
            "Febrero",
            "Marzo",
            "Abril",
            "Mayo",
            "Junio",
            "Julio",
            "Agosto",
            "Setiembre",
            "Octubre",
            "Noviembre",
            "Diciembre"});
            this.tsCboMes.Name = "tsCboMes";
            this.tsCboMes.Size = new System.Drawing.Size(80, 25);
            this.tsCboMes.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // lblAnio
            // 
            this.lblAnio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnio.Name = "lblAnio";
            this.lblAnio.Size = new System.Drawing.Size(29, 22);
            this.lblAnio.Text = "Año";
            // 
            // tstxtAño
            // 
            this.tstxtAño.BackColor = System.Drawing.SystemColors.Info;
            this.tstxtAño.Name = "tstxtAño";
            this.tstxtAño.Size = new System.Drawing.Size(45, 25);
            this.tstxtAño.Text = "2016";
            this.tstxtAño.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // txBtnBuscar
            // 
            this.txBtnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txBtnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("txBtnBuscar.Image")));
            this.txBtnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.txBtnBuscar.Name = "txBtnBuscar";
            this.txBtnBuscar.Size = new System.Drawing.Size(64, 22);
            this.txBtnBuscar.Text = "Buscar";
            this.txBtnBuscar.Click += new System.EventHandler(this.txBtnBuscar_Click);
            // 
            // rptConsultaFacturacion_1
            // 
            this.rptConsultaFacturacion_1.AutoSize = true;
            this.rptConsultaFacturacion_1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsFactuConsulta1";
            reportDataSource1.Value = this.rptGralFactu1BindingSource;
            this.rptConsultaFacturacion_1.LocalReport.DataSources.Add(reportDataSource1);
            this.rptConsultaFacturacion_1.LocalReport.ReportEmbeddedResource = "DGestion.Reportes.rptFactuConsultaGral_1.rdlc";
            this.rptConsultaFacturacion_1.Location = new System.Drawing.Point(0, 25);
            this.rptConsultaFacturacion_1.Margin = new System.Windows.Forms.Padding(1);
            this.rptConsultaFacturacion_1.Name = "rptConsultaFacturacion_1";
            this.rptConsultaFacturacion_1.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.rptConsultaFacturacion_1.Size = new System.Drawing.Size(1029, 759);
            this.rptConsultaFacturacion_1.TabIndex = 146;
            this.rptConsultaFacturacion_1.Visible = false;
            this.rptConsultaFacturacion_1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            this.rptConsultaFacturacion_1.Load += new System.EventHandler(this.rptConsultaFacturacion_Load);
            // 
            // rptConsultaFacturacion_2
            // 
            this.rptConsultaFacturacion_2.AutoSize = true;
            this.rptConsultaFacturacion_2.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "dsFactuConsulta2";
            reportDataSource2.Value = this.rpt_GralFactu_1BindingSource;
            this.rptConsultaFacturacion_2.LocalReport.DataSources.Add(reportDataSource2);
            this.rptConsultaFacturacion_2.LocalReport.ReportEmbeddedResource = "DGestion.Reportes.rptFactuConsultaGral_2.rdlc";
            this.rptConsultaFacturacion_2.Location = new System.Drawing.Point(0, 25);
            this.rptConsultaFacturacion_2.Margin = new System.Windows.Forms.Padding(1);
            this.rptConsultaFacturacion_2.Name = "rptConsultaFacturacion_2";
            this.rptConsultaFacturacion_2.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.rptConsultaFacturacion_2.Size = new System.Drawing.Size(1029, 759);
            this.rptConsultaFacturacion_2.TabIndex = 148;
            this.rptConsultaFacturacion_2.Visible = false;
            this.rptConsultaFacturacion_2.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // rptConsultaFacturacion_3
            // 
            this.rptConsultaFacturacion_3.AutoSize = true;
            this.rptConsultaFacturacion_3.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource3.Name = "dsTotalFactuCliente";
            reportDataSource3.Value = this.rpt_GralFactu_3BindingSource;
            this.rptConsultaFacturacion_3.LocalReport.DataSources.Add(reportDataSource3);
            this.rptConsultaFacturacion_3.LocalReport.ReportEmbeddedResource = "DGestion.Reportes.rptFactuConsultaGral_3.rdlc";
            this.rptConsultaFacturacion_3.Location = new System.Drawing.Point(0, 25);
            this.rptConsultaFacturacion_3.Margin = new System.Windows.Forms.Padding(1);
            this.rptConsultaFacturacion_3.Name = "rptConsultaFacturacion_3";
            this.rptConsultaFacturacion_3.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.rptConsultaFacturacion_3.Size = new System.Drawing.Size(1029, 759);
            this.rptConsultaFacturacion_3.TabIndex = 149;
            this.rptConsultaFacturacion_3.Visible = false;
            this.rptConsultaFacturacion_3.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // rpt_GralFactu_1TableAdapter
            // 
            this.rpt_GralFactu_1TableAdapter.ClearBeforeFill = true;
            // 
            // rptGralFactu2BindingSource
            // 
            this.rptGralFactu2BindingSource.DataMember = "rpt_GralFactu_2";
            this.rptGralFactu2BindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // rpt_GralFactu_2TableAdapter
            // 
            this.rpt_GralFactu_2TableAdapter.ClearBeforeFill = true;
            // 
            // rptGralFactu3BindingSource
            // 
            this.rptGralFactu3BindingSource.DataMember = "rpt_GralFactu_3";
            this.rptGralFactu3BindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // rpt_GralFactu_3TableAdapter
            // 
            this.rpt_GralFactu_3TableAdapter.ClearBeforeFill = true;
            // 
            // rptConsultaFacturacion_4
            // 
            this.rptConsultaFacturacion_4.AutoSize = true;
            this.rptConsultaFacturacion_4.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource4.Name = "dsTotaFactuAnualxEmpresaPtoVta";
            reportDataSource4.Value = this.rpt_GralFactu_4BindingSource;
            this.rptConsultaFacturacion_4.LocalReport.DataSources.Add(reportDataSource4);
            this.rptConsultaFacturacion_4.LocalReport.ReportEmbeddedResource = "DGestion.Reportes.rptFactuConsultaGral_4.rdlc";
            this.rptConsultaFacturacion_4.Location = new System.Drawing.Point(0, 25);
            this.rptConsultaFacturacion_4.Margin = new System.Windows.Forms.Padding(1);
            this.rptConsultaFacturacion_4.Name = "rptConsultaFacturacion_4";
            this.rptConsultaFacturacion_4.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.rptConsultaFacturacion_4.Size = new System.Drawing.Size(1029, 759);
            this.rptConsultaFacturacion_4.TabIndex = 150;
            this.rptConsultaFacturacion_4.Visible = false;
            this.rptConsultaFacturacion_4.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // rptGralFactu4BindingSource
            // 
            this.rptGralFactu4BindingSource.DataMember = "rpt_GralFactu_4";
            this.rptGralFactu4BindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // rpt_GralFactu_4TableAdapter
            // 
            this.rpt_GralFactu_4TableAdapter.ClearBeforeFill = true;
            // 
            // rptGrafico_1
            // 
            this.rptGrafico_1.AutoSize = true;
            this.rptGrafico_1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource5.Name = "dsGrafico1";
            reportDataSource5.Value = this.rpt_Grafico_1BindingSource;
            this.rptGrafico_1.LocalReport.DataSources.Add(reportDataSource5);
            this.rptGrafico_1.LocalReport.ReportEmbeddedResource = "DGestion.Reportes.rptGrafico_1.rdlc";
            this.rptGrafico_1.Location = new System.Drawing.Point(0, 25);
            this.rptGrafico_1.Margin = new System.Windows.Forms.Padding(1);
            this.rptGrafico_1.Name = "rptGrafico_1";
            this.rptGrafico_1.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.rptGrafico_1.Size = new System.Drawing.Size(1029, 759);
            this.rptGrafico_1.TabIndex = 151;
            this.rptGrafico_1.Visible = false;
            this.rptGrafico_1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // rptGrafico1BindingSource
            // 
            this.rptGrafico1BindingSource.DataMember = "rpt_Grafico_1";
            this.rptGrafico1BindingSource.DataSource = this.dGestionDTGeneral;
            // 
            // rpt_Grafico_1TableAdapter
            // 
            this.rpt_Grafico_1TableAdapter.ClearBeforeFill = true;
            // 
            // frmRTFactuConsulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 784);
            this.Controls.Add(this.rptGrafico_1);
            this.Controls.Add(this.rptConsultaFacturacion_4);
            this.Controls.Add(this.rptConsultaFacturacion_3);
            this.Controls.Add(this.rptConsultaFacturacion_2);
            this.Controls.Add(this.rptConsultaFacturacion_1);
            this.Controls.Add(this.tsMenuReporte);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmRTFactuConsulta";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consultas Generales de Facturación";
            this.Load += new System.EventHandler(this.frmRTFactuConsulta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rptGralFactu1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGestionDTGeneral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpt_GralFactu_1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpt_GralFactu_3BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpt_GralFactu_4BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpt_Grafico_1BindingSource)).EndInit();
            this.tsMenuReporte.ResumeLayout(false);
            this.tsMenuReporte.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rptGralFactu2BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptGralFactu3BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptGralFactu4BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptGrafico1BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip tsMenuReporte;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tsCboSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton txBtnBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox tstcboPtoVta;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox txtcboEmpresa;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel lblAnio;
        private System.Windows.Forms.ToolStripTextBox tstxtAño;
        private Microsoft.Reporting.WinForms.ReportViewer rptConsultaFacturacion_1;
        private System.Windows.Forms.BindingSource rptGralFactu1BindingSource;
        private DGestionDTGeneral dGestionDTGeneral;
        private DGestionDTGeneralTableAdapters.rpt_GralFactu_1TableAdapter rpt_GralFactu_1TableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer rptConsultaFacturacion_2;
        private System.Windows.Forms.BindingSource rpt_GralFactu_1BindingSource;
        private System.Windows.Forms.BindingSource rptGralFactu2BindingSource;
        private DGestionDTGeneralTableAdapters.rpt_GralFactu_2TableAdapter rpt_GralFactu_2TableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer rptConsultaFacturacion_3;
        private System.Windows.Forms.BindingSource rptGralFactu3BindingSource;
        private DGestionDTGeneralTableAdapters.rpt_GralFactu_3TableAdapter rpt_GralFactu_3TableAdapter;
        private System.Windows.Forms.BindingSource rpt_GralFactu_3BindingSource;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripComboBox tsCboMes;
        private System.Windows.Forms.BindingSource rpt_GralFactu_4BindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer rptConsultaFacturacion_4;
        private System.Windows.Forms.BindingSource rptGralFactu4BindingSource;
        private DGestionDTGeneralTableAdapters.rpt_GralFactu_4TableAdapter rpt_GralFactu_4TableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer rptGrafico_1;
        private System.Windows.Forms.BindingSource rpt_Grafico_1BindingSource;
        private System.Windows.Forms.BindingSource rptGrafico1BindingSource;
        private DGestionDTGeneralTableAdapters.rpt_Grafico_1TableAdapter rpt_Grafico_1TableAdapter;
    }
}