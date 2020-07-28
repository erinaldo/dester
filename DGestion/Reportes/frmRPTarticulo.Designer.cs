namespace DGestion.Reportes
{
    partial class frmRPTarticulo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRPTarticulo));
            this.ArticuloBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.articuloDataSet = new DGestion.ArticuloDataSet();
            this.rptVisorArticulo = new Microsoft.Reporting.WinForms.ReportViewer();
            this.articuloTableAdapter = new DGestion.ArticuloDataSetTableAdapters.ArticuloTableAdapter();
            this.tableAdapterManager = new DGestion.ArticuloDataSetTableAdapters.TableAdapterManager();
            this.articuloDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tsMenuReporte = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsCboSelect = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsTXTcodArticulo = new System.Windows.Forms.ToolStripTextBox();
            this.txBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnVerTodo = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.ArticuloBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.articuloDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.articuloDataSetBindingSource)).BeginInit();
            this.tsMenuReporte.SuspendLayout();
            this.SuspendLayout();
            // 
            // ArticuloBindingSource
            // 
            this.ArticuloBindingSource.DataMember = "Articulo";
            this.ArticuloBindingSource.DataSource = this.articuloDataSet;
            // 
            // articuloDataSet
            // 
            this.articuloDataSet.DataSetName = "ArticuloDataSet";
            this.articuloDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rptVisorArticulo
            // 
            this.rptVisorArticulo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ArticuloBindingSource;
            this.rptVisorArticulo.LocalReport.DataSources.Add(reportDataSource1);
            this.rptVisorArticulo.LocalReport.ReportEmbeddedResource = "DGestion.Reportes.rptArticulos.rdlc";
            this.rptVisorArticulo.Location = new System.Drawing.Point(0, 25);
            this.rptVisorArticulo.Name = "rptVisorArticulo";
            this.rptVisorArticulo.Size = new System.Drawing.Size(864, 711);
            this.rptVisorArticulo.TabIndex = 0;
            this.rptVisorArticulo.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            this.rptVisorArticulo.Load += new System.EventHandler(this.rptVisorArticulo_Load);
            // 
            // articuloTableAdapter
            // 
            this.articuloTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.ArticuloTableAdapter = this.articuloTableAdapter;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.TipoArticuloTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = DGestion.ArticuloDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // articuloDataSetBindingSource
            // 
            this.articuloDataSetBindingSource.DataSource = this.articuloDataSet;
            this.articuloDataSetBindingSource.Position = 0;
            // 
            // tsMenuReporte
            // 
            this.tsMenuReporte.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMenuReporte.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tsCboSelect,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.tsTXTcodArticulo,
            this.txBtnBuscar,
            this.toolStripSeparator1,
            this.tsBtnVerTodo});
            this.tsMenuReporte.Location = new System.Drawing.Point(0, 0);
            this.tsMenuReporte.Name = "tsMenuReporte";
            this.tsMenuReporte.Size = new System.Drawing.Size(864, 25);
            this.tsMenuReporte.Stretch = true;
            this.tsMenuReporte.TabIndex = 1;
            this.tsMenuReporte.Text = "Menu Reporte";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(87, 22);
            this.toolStripLabel2.Text = "Tipo Consulta: ";
            // 
            // tsCboSelect
            // 
            this.tsCboSelect.BackColor = System.Drawing.SystemColors.Info;
            this.tsCboSelect.Items.AddRange(new object[] {
            "Articulos",
            "Tipo de Art.",
            "Rubro de Art.",
            "Familia de Art."});
            this.tsCboSelect.Name = "tsCboSelect";
            this.tsCboSelect.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel1.Text = "Cod. Articulo:";
            // 
            // tsTXTcodArticulo
            // 
            this.tsTXTcodArticulo.BackColor = System.Drawing.SystemColors.Info;
            this.tsTXTcodArticulo.Name = "tsTXTcodArticulo";
            this.tsTXTcodArticulo.Size = new System.Drawing.Size(60, 25);
            this.tsTXTcodArticulo.ToolTipText = "Código Artículo";
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnVerTodo
            // 
            this.tsBtnVerTodo.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnVerTodo.Image")));
            this.tsBtnVerTodo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnVerTodo.Name = "tsBtnVerTodo";
            this.tsBtnVerTodo.Size = new System.Drawing.Size(73, 22);
            this.tsBtnVerTodo.Text = "Ver Todo";
            this.tsBtnVerTodo.Click += new System.EventHandler(this.tsBtnVerTodo_Click);
            // 
            // frmRPTarticulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 736);
            this.Controls.Add(this.tsMenuReporte);
            this.Controls.Add(this.rptVisorArticulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "frmRPTarticulo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Artículos";
            this.Load += new System.EventHandler(this.frmRPTarticulo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ArticuloBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.articuloDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.articuloDataSetBindingSource)).EndInit();
            this.tsMenuReporte.ResumeLayout(false);
            this.tsMenuReporte.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource ArticuloBindingSource;
        private ArticuloDataSet articuloDataSet;
        private System.Windows.Forms.BindingSource articuloDataSetBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer rptVisorArticulo;
        private ArticuloDataSetTableAdapters.ArticuloTableAdapter articuloTableAdapter;
        private ArticuloDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.ToolStrip tsMenuReporte;
        private System.Windows.Forms.ToolStripTextBox tsTXTcodArticulo;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton txBtnBuscar;
        private System.Windows.Forms.ToolStripButton tsBtnVerTodo;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tsCboSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}