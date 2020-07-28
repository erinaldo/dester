namespace DGestion
{
    partial class frmArticuloEstadoStock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArticuloEstadoStock));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.pbxEstado = new System.Windows.Forms.PictureBox();
            this.tlsBarArticulo = new System.Windows.Forms.ToolStrip();
            this.tsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.tsBtnActualizaStock = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnReporte = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.txtBuscarArticulo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBuscaArticulo = new System.Windows.Forms.ComboBox();
            this.lvwEstadosExistencia = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Codigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Descripción = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CantidadActual = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CantMínima = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CantRep = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Situación = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnSalir = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxEstado)).BeginInit();
            this.tlsBarArticulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.lblEstado);
            this.groupBox1.Controls.Add(this.pbxEstado);
            this.groupBox1.Controls.Add(this.tlsBarArticulo);
            this.groupBox1.Controls.Add(this.txtBuscarArticulo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboBuscaArticulo);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(848, 50);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(382, 23);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(19, 13);
            this.lblEstado.TabIndex = 14;
            this.lblEstado.Text = "__";
            this.lblEstado.Visible = false;
            // 
            // pbxEstado
            // 
            this.pbxEstado.Image = ((System.Drawing.Image)(resources.GetObject("pbxEstado.Image")));
            this.pbxEstado.InitialImage = null;
            this.pbxEstado.Location = new System.Drawing.Point(360, 21);
            this.pbxEstado.Name = "pbxEstado";
            this.pbxEstado.Size = new System.Drawing.Size(16, 16);
            this.pbxEstado.TabIndex = 13;
            this.pbxEstado.TabStop = false;
            this.pbxEstado.Visible = false;
            // 
            // tlsBarArticulo
            // 
            this.tlsBarArticulo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tlsBarArticulo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsBarArticulo.Dock = System.Windows.Forms.DockStyle.None;
            this.tlsBarArticulo.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsBarArticulo.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tlsBarArticulo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tlsBarArticulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnBuscar,
            this.tsBtnActualizaStock,
            this.toolStripSeparator2,
            this.tsBtnReporte,
            this.toolStripSeparator1,
            this.tsBtnSalir,
            this.toolStripSeparator3});
            this.tlsBarArticulo.Location = new System.Drawing.Point(715, 16);
            this.tlsBarArticulo.Name = "tlsBarArticulo";
            this.tlsBarArticulo.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tlsBarArticulo.Size = new System.Drawing.Size(133, 31);
            this.tlsBarArticulo.Stretch = true;
            this.tlsBarArticulo.TabIndex = 3;
            this.tlsBarArticulo.Text = "tlsBarArticulo";
            // 
            // tsBtnBuscar
            // 
            this.tsBtnBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnBuscar.Image")));
            this.tsBtnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnBuscar.Name = "tsBtnBuscar";
            this.tsBtnBuscar.Size = new System.Drawing.Size(28, 28);
            this.tsBtnBuscar.Text = "Buscar Item";
            this.tsBtnBuscar.Click += new System.EventHandler(this.tsBtnBuscar_Click);
            // 
            // tsBtnActualizaStock
            // 
            this.tsBtnActualizaStock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnActualizaStock.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnActualizaStock.Image")));
            this.tsBtnActualizaStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnActualizaStock.Name = "tsBtnActualizaStock";
            this.tsBtnActualizaStock.Size = new System.Drawing.Size(28, 28);
            this.tsBtnActualizaStock.Text = "Actualizar Estados";
            this.tsBtnActualizaStock.Click += new System.EventHandler(this.tsBtnActualizaStock_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsBtnReporte
            // 
            this.tsBtnReporte.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnReporte.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnReporte.Image")));
            this.tsBtnReporte.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnReporte.Name = "tsBtnReporte";
            this.tsBtnReporte.Size = new System.Drawing.Size(28, 28);
            this.tsBtnReporte.Text = "Visualizar Reportes";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsBtnSalir
            // 
            this.tsBtnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSalir.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSalir.Image")));
            this.tsBtnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSalir.Name = "tsBtnSalir";
            this.tsBtnSalir.Size = new System.Drawing.Size(28, 28);
            this.tsBtnSalir.Text = "Salir del Módulo Artículos";
            this.tsBtnSalir.Click += new System.EventHandler(this.tsBtnSalir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // txtBuscarArticulo
            // 
            this.txtBuscarArticulo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtBuscarArticulo.Location = new System.Drawing.Point(204, 19);
            this.txtBuscarArticulo.Name = "txtBuscarArticulo";
            this.txtBuscarArticulo.Size = new System.Drawing.Size(150, 20);
            this.txtBuscarArticulo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Buscar por:";
            // 
            // cboBuscaArticulo
            // 
            this.cboBuscaArticulo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cboBuscaArticulo.FormattingEnabled = true;
            this.cboBuscaArticulo.Items.AddRange(new object[] {
            "Cód. Artículo",
            "Descripción"});
            this.cboBuscaArticulo.Location = new System.Drawing.Point(78, 19);
            this.cboBuscaArticulo.Name = "cboBuscaArticulo";
            this.cboBuscaArticulo.Size = new System.Drawing.Size(120, 21);
            this.cboBuscaArticulo.TabIndex = 1;
            // 
            // lvwEstadosExistencia
            // 
            this.lvwEstadosExistencia.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwEstadosExistencia.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lvwEstadosExistencia.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Codigo,
            this.Descripción,
            this.CantidadActual,
            this.CantMínima,
            this.CantRep,
            this.Situación});
            this.lvwEstadosExistencia.FullRowSelect = true;
            this.lvwEstadosExistencia.GridLines = true;
            this.lvwEstadosExistencia.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwEstadosExistencia.HideSelection = false;
            this.lvwEstadosExistencia.LargeImageList = this.imageList1;
            this.lvwEstadosExistencia.Location = new System.Drawing.Point(12, 63);
            this.lvwEstadosExistencia.MultiSelect = false;
            this.lvwEstadosExistencia.Name = "lvwEstadosExistencia";
            this.lvwEstadosExistencia.Size = new System.Drawing.Size(850, 622);
            this.lvwEstadosExistencia.SmallImageList = this.imageList1;
            this.lvwEstadosExistencia.TabIndex = 33;
            this.lvwEstadosExistencia.UseCompatibleStateImageBehavior = false;
            this.lvwEstadosExistencia.View = System.Windows.Forms.View.Details;
            this.lvwEstadosExistencia.SelectedIndexChanged += new System.EventHandler(this.lvwEstadosExistencia_SelectedIndexChanged);
            // 
            // ID
            // 
            this.ID.Text = "Id";
            this.ID.Width = 30;
            // 
            // Codigo
            // 
            this.Codigo.Text = "Codigo";
            this.Codigo.Width = 81;
            // 
            // Descripción
            // 
            this.Descripción.Text = "Descripción";
            this.Descripción.Width = 500;
            // 
            // CantidadActual
            // 
            this.CantidadActual.Text = "Cant. Actual";
            this.CantidadActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CantidadActual.Width = 76;
            // 
            // CantMínima
            // 
            this.CantMínima.Text = "Cant. Mín.";
            this.CantMínima.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CantMínima.Width = 64;
            // 
            // CantRep
            // 
            this.CantRep.Text = "Cant. Rep,";
            this.CantRep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CantRep.Width = 68;
            // 
            // Situación
            // 
            this.Situación.Text = "Situación";
            this.Situación.Width = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Network Status High connection.ico");
            this.imageList1.Images.SetKeyName(1, "Network Status No connection.ico");
            this.imageList1.Images.SetKeyName(2, "Network Status Low connection.ico");
            this.imageList1.Images.SetKeyName(3, "Network Status Medium connection.ico");
            this.imageList1.Images.SetKeyName(4, "status battery 100.ico");
            this.imageList1.Images.SetKeyName(5, "Status battery low.ico");
            this.imageList1.Images.SetKeyName(6, "Status battery caution.ico");
            this.imageList1.Images.SetKeyName(7, "Status battery 060.ico");
            this.imageList1.Images.SetKeyName(8, "Status battery missing.ico");
            this.imageList1.Images.SetKeyName(9, "battery 3.ico");
            this.imageList1.Images.SetKeyName(10, "battery 1.ico");
            this.imageList1.Images.SetKeyName(11, "battery 2.ico");
            this.imageList1.Images.SetKeyName(12, "battery half.ico");
            this.imageList1.Images.SetKeyName(13, "System Box Full.ico");
            this.imageList1.Images.SetKeyName(14, "System Box Empty.ico");
            this.imageList1.Images.SetKeyName(15, "error.ico");
            this.imageList1.Images.SetKeyName(16, "Action_flag.ico");
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(12, 607);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(10, 10);
            this.btnSalir.TabIndex = 46;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmArticuloEstadoStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(874, 697);
            this.Controls.Add(this.lvwEstadosExistencia);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmArticuloEstadoStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Estado de Existencia por Artículos ";
            this.Load += new System.EventHandler(this.frmArticuloStockInsuf_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxEstado)).EndInit();
            this.tlsBarArticulo.ResumeLayout(false);
            this.tlsBarArticulo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip tlsBarArticulo;
        private System.Windows.Forms.ToolStripButton tsBtnBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnReporte;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TextBox txtBuscarArticulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBuscaArticulo;
        private System.Windows.Forms.ListView lvwEstadosExistencia;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Codigo;
        private System.Windows.Forms.ColumnHeader Descripción;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader Situación;
        private System.Windows.Forms.ToolStripButton tsBtnActualizaStock;
        private System.Windows.Forms.ColumnHeader CantidadActual;
        private System.Windows.Forms.ColumnHeader CantMínima;
        private System.Windows.Forms.ColumnHeader CantRep;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.PictureBox pbxEstado;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Timer timer1;
    }
}