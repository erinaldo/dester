namespace DGestion
{
    partial class frmNotaPedido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotaPedido));
            this.gpoDetalleNota = new System.Windows.Forms.GroupBox();
            this.btnCerrarDetalle = new System.Windows.Forms.Button();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.lvwDetalleNotaPedido = new System.Windows.Forms.ListView();
            this.IdArticulo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Codigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Artículos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CantArtPedida = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PrecioUnitario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Subtotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Desc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Iva = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Importe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Existencia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CantArtRestante = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdDNota = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.iva1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.iva2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Entregar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Faltante = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Remitido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Reasignado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnQuitaArt = new System.Windows.Forms.Button();
            this.btnAgregaArt = new System.Windows.Forms.Button();
            this.txtCantRestante = new System.Windows.Forms.TextBox();
            this.lblCantRestante = new System.Windows.Forms.Label();
            this.txtCantPedida = new System.Windows.Forms.TextBox();
            this.lblCantPedida = new System.Windows.Forms.Label();
            this.cboArticulo = new System.Windows.Forms.ComboBox();
            this.btnBuscaArticulo = new System.Windows.Forms.Button();
            this.lblCodArt = new System.Windows.Forms.Label();
            this.txtCantArticulo = new System.Windows.Forms.TextBox();
            this.lblCantActual = new System.Windows.Forms.Label();
            this.txtCodigoArticulo = new System.Windows.Forms.TextBox();
            this.gpNP = new System.Windows.Forms.GroupBox();
            this.tlsBarArticulo = new System.Windows.Forms.ToolStrip();
            this.tsBtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsBtnModificar = new System.Windows.Forms.ToolStripButton();
            this.tsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsRemito = new System.Windows.Forms.ToolStripButton();
            this.Referencias = new System.Windows.Forms.ToolStripButton();
            this.tsRacionalizador = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnReporte = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnReporteGenerico = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.txtBuscarArticulo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboBuscaNotaPedido = new System.Windows.Forms.ComboBox();
            this.gbLeyenda = new System.Windows.Forms.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.btnCerrarLeyenda = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gpNotaPedido = new System.Windows.Forms.GroupBox();
            this.lvwNotaPedido = new System.Windows.Forms.ListView();
            this.IDInterno = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Suc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NumeroNotaPedido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Fecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdCliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RazonSocialCliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Basico = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Descuento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Iva105 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Iva21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Observaciones = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdVendedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TotalCompletada = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gpoNotaDePedido = new System.Windows.Forms.GroupBox();
            this.btnRemito = new System.Windows.Forms.Button();
            this.gpDetalleNP = new System.Windows.Forms.GroupBox();
            this.cboListaCliente = new System.Windows.Forms.ComboBox();
            this.txtCodListaCliente = new System.Windows.Forms.TextBox();
            this.btnListaPrecioCliente = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboVendedor = new System.Windows.Forms.ComboBox();
            this.txtCodVendedor = new System.Windows.Forms.TextBox();
            this.btnVendedor = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.txtCodCliente = new System.Windows.Forms.TextBox();
            this.btnCliente = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.txtImpuesto2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtObservacionNotaPedido = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtImpuesto1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTotalFactur = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtSubTotal = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCuit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIva = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gpIdentificacion = new System.Windows.Forms.GroupBox();
            this.label33 = new System.Windows.Forms.Label();
            this.txtNroNPedido = new System.Windows.Forms.TextBox();
            this.cboNroSucursal = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaFactu = new System.Windows.Forms.DateTimePicker();
            this.txtNroNotaPedido = new System.Windows.Forms.TextBox();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNroInternoNota = new System.Windows.Forms.TextBox();
            this.lblNroInterno = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnActicarDetallesNP = new System.Windows.Forms.Button();
            this.gpbRacionalizador = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pboxArtPedido = new System.Windows.Forms.PictureBox();
            this.lblExistenciaArtic = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblCantClientePedido = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblTotalPedidoArticulo = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.cboDescArticulo = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtCantTotalArticulo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCerrarRacionalizador = new System.Windows.Forms.Button();
            this.btnBuscaArtNPCliente = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnQuitaCantidad = new System.Windows.Forms.Button();
            this.txtProporcionManualPedido = new System.Windows.Forms.TextBox();
            this.btnAgregaCantidad = new System.Windows.Forms.Button();
            this.txtCodArtic = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvwPedidoClientes = new System.Windows.Forms.ListView();
            this.NP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdCliente_ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RazonSocial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FechaNota = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CantidadPedida = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CantLimiteEntregar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CantReasignada = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gpoDetalleNota.SuspendLayout();
            this.gpNP.SuspendLayout();
            this.tlsBarArticulo.SuspendLayout();
            this.gbLeyenda.SuspendLayout();
            this.gpNotaPedido.SuspendLayout();
            this.gpoNotaDePedido.SuspendLayout();
            this.gpDetalleNP.SuspendLayout();
            this.gpIdentificacion.SuspendLayout();
            this.gpbRacionalizador.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxArtPedido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpoDetalleNota
            // 
            this.gpoDetalleNota.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpoDetalleNota.Controls.Add(this.btnCerrarDetalle);
            this.gpoDetalleNota.Controls.Add(this.lblPrecio);
            this.gpoDetalleNota.Controls.Add(this.txtPrecio);
            this.gpoDetalleNota.Controls.Add(this.lvwDetalleNotaPedido);
            this.gpoDetalleNota.Controls.Add(this.btnQuitaArt);
            this.gpoDetalleNota.Controls.Add(this.btnAgregaArt);
            this.gpoDetalleNota.Controls.Add(this.txtCantRestante);
            this.gpoDetalleNota.Controls.Add(this.lblCantRestante);
            this.gpoDetalleNota.Controls.Add(this.txtCantPedida);
            this.gpoDetalleNota.Controls.Add(this.lblCantPedida);
            this.gpoDetalleNota.Controls.Add(this.cboArticulo);
            this.gpoDetalleNota.Controls.Add(this.btnBuscaArticulo);
            this.gpoDetalleNota.Controls.Add(this.lblCodArt);
            this.gpoDetalleNota.Controls.Add(this.txtCantArticulo);
            this.gpoDetalleNota.Controls.Add(this.lblCantActual);
            this.gpoDetalleNota.Controls.Add(this.txtCodigoArticulo);
            this.gpoDetalleNota.Location = new System.Drawing.Point(12, 334);
            this.gpoDetalleNota.Name = "gpoDetalleNota";
            this.gpoDetalleNota.Size = new System.Drawing.Size(951, 324);
            this.gpoDetalleNota.TabIndex = 18;
            this.gpoDetalleNota.TabStop = false;
            this.gpoDetalleNota.Text = "Detalle Nota de Pedido";
            // 
            // btnCerrarDetalle
            // 
            this.btnCerrarDetalle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCerrarDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrarDetalle.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarDetalle.Image")));
            this.btnCerrarDetalle.Location = new System.Drawing.Point(931, 304);
            this.btnCerrarDetalle.Name = "btnCerrarDetalle";
            this.btnCerrarDetalle.Size = new System.Drawing.Size(20, 20);
            this.btnCerrarDetalle.TabIndex = 35;
            this.btnCerrarDetalle.TabStop = false;
            this.btnCerrarDetalle.UseVisualStyleBackColor = false;
            this.btnCerrarDetalle.Click += new System.EventHandler(this.btnCerrarDetalle_Click);
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Location = new System.Drawing.Point(605, 297);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(40, 13);
            this.lblPrecio.TabIndex = 160;
            this.lblPrecio.Text = "Precio:";
            // 
            // txtPrecio
            // 
            this.txtPrecio.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtPrecio.Location = new System.Drawing.Point(651, 293);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.ReadOnly = true;
            this.txtPrecio.Size = new System.Drawing.Size(55, 20);
            this.txtPrecio.TabIndex = 23;
            this.txtPrecio.TabStop = false;
            this.txtPrecio.Text = "$ 0,00";
            // 
            // lvwDetalleNotaPedido
            // 
            this.lvwDetalleNotaPedido.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwDetalleNotaPedido.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwDetalleNotaPedido.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IdArticulo,
            this.Codigo,
            this.Artículos,
            this.CantArtPedida,
            this.PrecioUnitario,
            this.Subtotal,
            this.Desc,
            this.Iva,
            this.Importe,
            this.Existencia,
            this.CantArtRestante,
            this.IdDNota,
            this.iva1,
            this.iva2,
            this.Entregar,
            this.Faltante,
            this.Remitido,
            this.Reasignado});
            this.lvwDetalleNotaPedido.FullRowSelect = true;
            this.lvwDetalleNotaPedido.GridLines = true;
            this.lvwDetalleNotaPedido.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwDetalleNotaPedido.HideSelection = false;
            this.lvwDetalleNotaPedido.LargeImageList = this.imageList1;
            this.lvwDetalleNotaPedido.Location = new System.Drawing.Point(6, 19);
            this.lvwDetalleNotaPedido.Name = "lvwDetalleNotaPedido";
            this.lvwDetalleNotaPedido.Size = new System.Drawing.Size(939, 239);
            this.lvwDetalleNotaPedido.SmallImageList = this.imageList1;
            this.lvwDetalleNotaPedido.TabIndex = 19;
            this.lvwDetalleNotaPedido.UseCompatibleStateImageBehavior = false;
            this.lvwDetalleNotaPedido.View = System.Windows.Forms.View.Details;
            this.lvwDetalleNotaPedido.SelectedIndexChanged += new System.EventHandler(this.lvwDetalleNotaPedido_SelectedIndexChanged);
            // 
            // IdArticulo
            // 
            this.IdArticulo.Text = "-";
            this.IdArticulo.Width = 28;
            // 
            // Codigo
            // 
            this.Codigo.Text = "Código";
            this.Codigo.Width = 75;
            // 
            // Artículos
            // 
            this.Artículos.Text = "Artículos";
            this.Artículos.Width = 214;
            // 
            // CantArtPedida
            // 
            this.CantArtPedida.Text = "Pedido";
            this.CantArtPedida.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CantArtPedida.Width = 55;
            // 
            // PrecioUnitario
            // 
            this.PrecioUnitario.Text = "Precio Unit.";
            this.PrecioUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PrecioUnitario.Width = 70;
            // 
            // Subtotal
            // 
            this.Subtotal.Text = "Subtotal";
            this.Subtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Subtotal.Width = 0;
            // 
            // Desc
            // 
            this.Desc.Text = "Desc";
            this.Desc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Desc.Width = 50;
            // 
            // Iva
            // 
            this.Iva.Text = "Iva";
            this.Iva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Iva.Width = 0;
            // 
            // Importe
            // 
            this.Importe.Text = "Importe";
            this.Importe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Importe.Width = 70;
            // 
            // Existencia
            // 
            this.Existencia.Text = "Existencia";
            this.Existencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Existencia.Width = 65;
            // 
            // CantArtRestante
            // 
            this.CantArtRestante.Text = "Cant. Rest.";
            this.CantArtRestante.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CantArtRestante.Width = 0;
            // 
            // IdDNota
            // 
            this.IdDNota.Text = "IdDNota";
            this.IdDNota.Width = 0;
            // 
            // iva1
            // 
            this.iva1.Text = "iva1";
            this.iva1.Width = 0;
            // 
            // iva2
            // 
            this.iva2.Text = "iva2";
            this.iva2.Width = 0;
            // 
            // Entregar
            // 
            this.Entregar.Text = "Pendiente Entrega";
            this.Entregar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Entregar.Width = 106;
            // 
            // Faltante
            // 
            this.Faltante.Text = "Faltante";
            this.Faltante.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Remitido
            // 
            this.Remitido.Text = "Remito";
            this.Remitido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Remitido.Width = 50;
            // 
            // Reasignado
            // 
            this.Reasignado.Text = "Reasig.";
            this.Reasignado.Width = 50;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Box_Green.ico");
            this.imageList1.Images.SetKeyName(1, "Box_Red.ico");
            this.imageList1.Images.SetKeyName(2, "Box_Orange.ico");
            this.imageList1.Images.SetKeyName(3, "System Box Full.ico");
            this.imageList1.Images.SetKeyName(4, "System Box Empty.ico");
            this.imageList1.Images.SetKeyName(5, "Folder documents1.ico");
            this.imageList1.Images.SetKeyName(6, "Knob Cancel.ico");
            this.imageList1.Images.SetKeyName(7, "Knob Valid Green.ico");
            this.imageList1.Images.SetKeyName(8, "Circle_Yellow.ico");
            this.imageList1.Images.SetKeyName(9, "Box_Yellow.ico");
            this.imageList1.Images.SetKeyName(10, "Circle_Green.ico");
            this.imageList1.Images.SetKeyName(11, "Knob Orange.ico");
            this.imageList1.Images.SetKeyName(12, "Circle_Red.ico");
            this.imageList1.Images.SetKeyName(13, "Knob Red.ico");
            this.imageList1.Images.SetKeyName(14, "Knob Green.ico");
            this.imageList1.Images.SetKeyName(15, "Circle_Blue.ico");
            this.imageList1.Images.SetKeyName(16, "Circle_Grey.ico");
            this.imageList1.Images.SetKeyName(17, "Circle_Orange.ico");
            this.imageList1.Images.SetKeyName(18, "Box_Blue.ico");
            this.imageList1.Images.SetKeyName(19, "Box_Grey.ico");
            this.imageList1.Images.SetKeyName(20, "Clock4.ico");
            this.imageList1.Images.SetKeyName(21, "Balance.ico");
            // 
            // btnQuitaArt
            // 
            this.btnQuitaArt.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnQuitaArt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnQuitaArt.Image = ((System.Drawing.Image)(resources.GetObject("btnQuitaArt.Image")));
            this.btnQuitaArt.Location = new System.Drawing.Point(915, 264);
            this.btnQuitaArt.Name = "btnQuitaArt";
            this.btnQuitaArt.Size = new System.Drawing.Size(30, 25);
            this.btnQuitaArt.TabIndex = 25;
            this.btnQuitaArt.UseVisualStyleBackColor = false;
            this.btnQuitaArt.Click += new System.EventHandler(this.btnQuitaArt_Click);
            // 
            // btnAgregaArt
            // 
            this.btnAgregaArt.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAgregaArt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregaArt.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregaArt.Image")));
            this.btnAgregaArt.Location = new System.Drawing.Point(880, 264);
            this.btnAgregaArt.Name = "btnAgregaArt";
            this.btnAgregaArt.Size = new System.Drawing.Size(30, 25);
            this.btnAgregaArt.TabIndex = 24;
            this.btnAgregaArt.UseVisualStyleBackColor = false;
            this.btnAgregaArt.Click += new System.EventHandler(this.btnAgregaArt_Click);
            this.btnAgregaArt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnAgregaArt_KeyPress);
            // 
            // txtCantRestante
            // 
            this.txtCantRestante.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtCantRestante.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantRestante.Location = new System.Drawing.Point(786, 293);
            this.txtCantRestante.Name = "txtCantRestante";
            this.txtCantRestante.ReadOnly = true;
            this.txtCantRestante.Size = new System.Drawing.Size(55, 20);
            this.txtCantRestante.TabIndex = 25;
            this.txtCantRestante.TabStop = false;
            // 
            // lblCantRestante
            // 
            this.lblCantRestante.AutoSize = true;
            this.lblCantRestante.Location = new System.Drawing.Point(717, 296);
            this.lblCantRestante.Name = "lblCantRestante";
            this.lblCantRestante.Size = new System.Drawing.Size(63, 13);
            this.lblCantRestante.TabIndex = 152;
            this.lblCantRestante.Text = "Cant. Rest.:";
            // 
            // txtCantPedida
            // 
            this.txtCantPedida.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCantPedida.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantPedida.Location = new System.Drawing.Point(651, 267);
            this.txtCantPedida.Name = "txtCantPedida";
            this.txtCantPedida.Size = new System.Drawing.Size(55, 20);
            this.txtCantPedida.TabIndex = 23;
            this.txtCantPedida.TextChanged += new System.EventHandler(this.txtCantPedida_TextChanged);
            this.txtCantPedida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantPedida_KeyPress);
            // 
            // lblCantPedida
            // 
            this.lblCantPedida.AutoSize = true;
            this.lblCantPedida.Location = new System.Drawing.Point(574, 270);
            this.lblCantPedida.Name = "lblCantPedida";
            this.lblCantPedida.Size = new System.Drawing.Size(71, 13);
            this.lblCantPedida.TabIndex = 150;
            this.lblCantPedida.Text = "Cant. Pedida:";
            // 
            // cboArticulo
            // 
            this.cboArticulo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cboArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboArticulo.FormattingEnabled = true;
            this.cboArticulo.Location = new System.Drawing.Point(169, 267);
            this.cboArticulo.Name = "cboArticulo";
            this.cboArticulo.Size = new System.Drawing.Size(363, 21);
            this.cboArticulo.TabIndex = 21;
            this.cboArticulo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboArticulo_KeyPress);
            // 
            // btnBuscaArticulo
            // 
            this.btnBuscaArticulo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBuscaArticulo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBuscaArticulo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuscaArticulo.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscaArticulo.Image")));
            this.btnBuscaArticulo.Location = new System.Drawing.Point(538, 264);
            this.btnBuscaArticulo.Name = "btnBuscaArticulo";
            this.btnBuscaArticulo.Size = new System.Drawing.Size(30, 25);
            this.btnBuscaArticulo.TabIndex = 22;
            this.btnBuscaArticulo.UseVisualStyleBackColor = false;
            this.btnBuscaArticulo.Click += new System.EventHandler(this.btnArticulo_Click);
            // 
            // lblCodArt
            // 
            this.lblCodArt.AutoSize = true;
            this.lblCodArt.Location = new System.Drawing.Point(6, 270);
            this.lblCodArt.Name = "lblCodArt";
            this.lblCodArt.Size = new System.Drawing.Size(72, 13);
            this.lblCodArt.TabIndex = 146;
            this.lblCodArt.Text = "Cod. Artículo:";
            // 
            // txtCantArticulo
            // 
            this.txtCantArticulo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtCantArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantArticulo.Location = new System.Drawing.Point(786, 267);
            this.txtCantArticulo.Name = "txtCantArticulo";
            this.txtCantArticulo.ReadOnly = true;
            this.txtCantArticulo.Size = new System.Drawing.Size(55, 20);
            this.txtCantArticulo.TabIndex = 24;
            this.txtCantArticulo.TabStop = false;
            // 
            // lblCantActual
            // 
            this.lblCantActual.AutoSize = true;
            this.lblCantActual.Location = new System.Drawing.Point(712, 270);
            this.lblCantActual.Name = "lblCantActual";
            this.lblCantActual.Size = new System.Drawing.Size(68, 13);
            this.lblCantActual.TabIndex = 141;
            this.lblCantActual.Text = "Cant. Actual:";
            // 
            // txtCodigoArticulo
            // 
            this.txtCodigoArticulo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCodigoArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoArticulo.Location = new System.Drawing.Point(84, 267);
            this.txtCodigoArticulo.Name = "txtCodigoArticulo";
            this.txtCodigoArticulo.Size = new System.Drawing.Size(79, 20);
            this.txtCodigoArticulo.TabIndex = 20;
            this.txtCodigoArticulo.TextChanged += new System.EventHandler(this.txtCodigoArticulo_TextChanged);
            this.txtCodigoArticulo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoArticulo_KeyPress);
            // 
            // gpNP
            // 
            this.gpNP.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpNP.Controls.Add(this.tlsBarArticulo);
            this.gpNP.Controls.Add(this.txtBuscarArticulo);
            this.gpNP.Controls.Add(this.label3);
            this.gpNP.Controls.Add(this.cboBuscaNotaPedido);
            this.gpNP.Location = new System.Drawing.Point(12, 12);
            this.gpNP.Name = "gpNP";
            this.gpNP.Size = new System.Drawing.Size(954, 50);
            this.gpNP.TabIndex = 0;
            this.gpNP.TabStop = false;
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
            this.tsBtnNuevo,
            this.tsBtnModificar,
            this.tsBtnBuscar,
            this.toolStripSeparator4,
            this.tsRemito,
            this.Referencias,
            this.tsRacionalizador,
            this.toolStripSeparator2,
            this.tsBtnReporte,
            this.toolStripSeparator1,
            this.tsbtnReporteGenerico,
            this.toolStripSeparator5,
            this.tsBtnSalir,
            this.toolStripSeparator3});
            this.tlsBarArticulo.Location = new System.Drawing.Point(670, 16);
            this.tlsBarArticulo.Name = "tlsBarArticulo";
            this.tlsBarArticulo.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tlsBarArticulo.Size = new System.Drawing.Size(285, 31);
            this.tlsBarArticulo.Stretch = true;
            this.tlsBarArticulo.TabIndex = 3;
            this.tlsBarArticulo.Text = "tlsBarArticulo";
            // 
            // tsBtnNuevo
            // 
            this.tsBtnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnNuevo.Image")));
            this.tsBtnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnNuevo.Name = "tsBtnNuevo";
            this.tsBtnNuevo.Size = new System.Drawing.Size(28, 28);
            this.tsBtnNuevo.Text = "Nuevo Artículo";
            this.tsBtnNuevo.ToolTipText = "Nueva Nota de Pedido";
            this.tsBtnNuevo.Click += new System.EventHandler(this.tsBtnNuevo_Click);
            // 
            // tsBtnModificar
            // 
            this.tsBtnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnModificar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnModificar.Image")));
            this.tsBtnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnModificar.Name = "tsBtnModificar";
            this.tsBtnModificar.Size = new System.Drawing.Size(28, 28);
            this.tsBtnModificar.Text = "Modificar Item";
            this.tsBtnModificar.ToolTipText = "Modificar Nota de Pedido";
            this.tsBtnModificar.Click += new System.EventHandler(this.tsBtnModificar_Click);
            // 
            // tsBtnBuscar
            // 
            this.tsBtnBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnBuscar.Image")));
            this.tsBtnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnBuscar.Name = "tsBtnBuscar";
            this.tsBtnBuscar.Size = new System.Drawing.Size(28, 28);
            this.tsBtnBuscar.Text = "Buscar Item";
            this.tsBtnBuscar.ToolTipText = "Buscar Nota de Pedido";
            this.tsBtnBuscar.Click += new System.EventHandler(this.tsBtnBuscar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // tsRemito
            // 
            this.tsRemito.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsRemito.Image = ((System.Drawing.Image)(resources.GetObject("tsRemito.Image")));
            this.tsRemito.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRemito.Name = "tsRemito";
            this.tsRemito.Size = new System.Drawing.Size(28, 28);
            this.tsRemito.Text = "tsRemito";
            this.tsRemito.ToolTipText = "Remito";
            this.tsRemito.Click += new System.EventHandler(this.tsRemito_Click);
            // 
            // Referencias
            // 
            this.Referencias.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Referencias.Image = ((System.Drawing.Image)(resources.GetObject("Referencias.Image")));
            this.Referencias.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Referencias.Name = "Referencias";
            this.Referencias.Size = new System.Drawing.Size(28, 28);
            this.Referencias.Text = "tsReferencia";
            this.Referencias.ToolTipText = "Referencias de Estados";
            this.Referencias.Click += new System.EventHandler(this.Referencias_Click);
            // 
            // tsRacionalizador
            // 
            this.tsRacionalizador.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsRacionalizador.Image = ((System.Drawing.Image)(resources.GetObject("tsRacionalizador.Image")));
            this.tsRacionalizador.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRacionalizador.Name = "tsRacionalizador";
            this.tsRacionalizador.Size = new System.Drawing.Size(28, 28);
            this.tsRacionalizador.Text = "Racionalizar Existencias";
            this.tsRacionalizador.ToolTipText = "Racionalizar Existencia";
            this.tsRacionalizador.Click += new System.EventHandler(this.tsRacionalizador_Click);
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
            this.tsBtnReporte.Text = "Visualizar Nota de Pedido";
            this.tsBtnReporte.Click += new System.EventHandler(this.tsBtnReporte_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbtnReporteGenerico
            // 
            this.tsbtnReporteGenerico.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnReporteGenerico.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnReporteGenerico.Image")));
            this.tsbtnReporteGenerico.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnReporteGenerico.Name = "tsbtnReporteGenerico";
            this.tsbtnReporteGenerico.Size = new System.Drawing.Size(28, 28);
            this.tsbtnReporteGenerico.Text = "Reportes";
            this.tsbtnReporteGenerico.Click += new System.EventHandler(this.tsbtnReporteGenerico_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
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
            this.txtBuscarArticulo.Location = new System.Drawing.Point(265, 19);
            this.txtBuscarArticulo.Name = "txtBuscarArticulo";
            this.txtBuscarArticulo.Size = new System.Drawing.Size(338, 20);
            this.txtBuscarArticulo.TabIndex = 1;
            this.txtBuscarArticulo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarArticulo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Buscar por:";
            // 
            // cboBuscaNotaPedido
            // 
            this.cboBuscaNotaPedido.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboBuscaNotaPedido.FormattingEnabled = true;
            this.cboBuscaNotaPedido.Items.AddRange(new object[] {
            "Num Comprobante",
            "Fecha Comprobante",
            "Razón Social Cliente"});
            this.cboBuscaNotaPedido.Location = new System.Drawing.Point(78, 19);
            this.cboBuscaNotaPedido.Name = "cboBuscaNotaPedido";
            this.cboBuscaNotaPedido.Size = new System.Drawing.Size(181, 21);
            this.cboBuscaNotaPedido.TabIndex = 0;
            this.cboBuscaNotaPedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboBuscaNotaPedido_KeyPress);
            // 
            // gbLeyenda
            // 
            this.gbLeyenda.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gbLeyenda.Controls.Add(this.label31);
            this.gbLeyenda.Controls.Add(this.label32);
            this.gbLeyenda.Controls.Add(this.label29);
            this.gbLeyenda.Controls.Add(this.label30);
            this.gbLeyenda.Controls.Add(this.label27);
            this.gbLeyenda.Controls.Add(this.label28);
            this.gbLeyenda.Controls.Add(this.label25);
            this.gbLeyenda.Controls.Add(this.label26);
            this.gbLeyenda.Controls.Add(this.btnCerrarLeyenda);
            this.gbLeyenda.Controls.Add(this.label23);
            this.gbLeyenda.Controls.Add(this.label24);
            this.gbLeyenda.Controls.Add(this.label22);
            this.gbLeyenda.Controls.Add(this.label19);
            this.gbLeyenda.Location = new System.Drawing.Point(764, 7);
            this.gbLeyenda.Name = "gbLeyenda";
            this.gbLeyenda.Size = new System.Drawing.Size(203, 172);
            this.gbLeyenda.TabIndex = 155;
            this.gbLeyenda.TabStop = false;
            this.gbLeyenda.Text = "Referencias de Estado";
            this.gbLeyenda.Visible = false;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(64, 118);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(113, 13);
            this.label31.TabIndex = 161;
            this.label31.Text = "Mercadería Pendiente";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label32.Location = new System.Drawing.Point(11, 118);
            this.label32.Margin = new System.Windows.Forms.Padding(0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(47, 13);
            this.label32.TabIndex = 160;
            this.label32.Text = "          ";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(64, 99);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(95, 13);
            this.label29.TabIndex = 159;
            this.label29.Text = "No Existe faltantes";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Green;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label30.Location = new System.Drawing.Point(12, 99);
            this.label30.Margin = new System.Windows.Forms.Padding(0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(46, 13);
            this.label30.TabIndex = 158;
            this.label30.Text = "    0    ";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(64, 81);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(106, 13);
            this.label27.TabIndex = 157;
            this.label27.Text = "Mercadería Remitida";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Green;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label28.Location = new System.Drawing.Point(11, 81);
            this.label28.Margin = new System.Windows.Forms.Padding(0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(47, 13);
            this.label28.TabIndex = 156;
            this.label28.Text = "   SI    ";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(64, 62);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(134, 13);
            this.label25.TabIndex = 155;
            this.label25.Text = "No Remitido Sin Existencia";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Red;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label26.Location = new System.Drawing.Point(13, 62);
            this.label26.Margin = new System.Windows.Forms.Padding(0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(45, 13);
            this.label26.TabIndex = 154;
            this.label26.Text = "  S/E  ";
            // 
            // btnCerrarLeyenda
            // 
            this.btnCerrarLeyenda.BackColor = System.Drawing.SystemColors.Control;
            this.btnCerrarLeyenda.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarLeyenda.Image")));
            this.btnCerrarLeyenda.Location = new System.Drawing.Point(177, 138);
            this.btnCerrarLeyenda.Name = "btnCerrarLeyenda";
            this.btnCerrarLeyenda.Size = new System.Drawing.Size(20, 20);
            this.btnCerrarLeyenda.TabIndex = 153;
            this.btnCerrarLeyenda.TabStop = false;
            this.btnCerrarLeyenda.UseVisualStyleBackColor = false;
            this.btnCerrarLeyenda.Click += new System.EventHandler(this.btnCerrarLeyenda_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(64, 45);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(116, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "Faltante de Existencias";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Coral;
            this.label24.Location = new System.Drawing.Point(12, 45);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(46, 13);
            this.label24.TabIndex = 2;
            this.label24.Text = "             ";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(64, 27);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(73, 13);
            this.label22.TabIndex = 1;
            this.label22.Text = "Sin Existencia";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(12, 27);
            this.label19.Margin = new System.Windows.Forms.Padding(0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(46, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "             ";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // gpNotaPedido
            // 
            this.gpNotaPedido.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpNotaPedido.Controls.Add(this.lvwNotaPedido);
            this.gpNotaPedido.Location = new System.Drawing.Point(12, 68);
            this.gpNotaPedido.Name = "gpNotaPedido";
            this.gpNotaPedido.Size = new System.Drawing.Size(214, 260);
            this.gpNotaPedido.TabIndex = 1;
            this.gpNotaPedido.TabStop = false;
            this.gpNotaPedido.Text = "Nota de Pedido";
            // 
            // lvwNotaPedido
            // 
            this.lvwNotaPedido.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwNotaPedido.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwNotaPedido.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IDInterno,
            this.Suc,
            this.NumeroNotaPedido,
            this.Fecha,
            this.IdCliente,
            this.RazonSocialCliente,
            this.Basico,
            this.Descuento,
            this.Iva105,
            this.Iva21,
            this.Total,
            this.Observaciones,
            this.IdVendedor,
            this.TotalCompletada});
            this.lvwNotaPedido.FullRowSelect = true;
            this.lvwNotaPedido.GridLines = true;
            this.lvwNotaPedido.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwNotaPedido.HideSelection = false;
            this.lvwNotaPedido.LargeImageList = this.imageList1;
            this.lvwNotaPedido.Location = new System.Drawing.Point(6, 19);
            this.lvwNotaPedido.MultiSelect = false;
            this.lvwNotaPedido.Name = "lvwNotaPedido";
            this.lvwNotaPedido.Size = new System.Drawing.Size(202, 235);
            this.lvwNotaPedido.SmallImageList = this.imageList1;
            this.lvwNotaPedido.TabIndex = 2;
            this.lvwNotaPedido.UseCompatibleStateImageBehavior = false;
            this.lvwNotaPedido.View = System.Windows.Forms.View.Details;
            this.lvwNotaPedido.SelectedIndexChanged += new System.EventHandler(this.lvwNotaPedido_SelectedIndexChanged);
            this.lvwNotaPedido.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwNotaPedido_MouseDoubleClick);
            // 
            // IDInterno
            // 
            this.IDInterno.Text = "-";
            this.IDInterno.Width = 32;
            // 
            // Suc
            // 
            this.Suc.Text = "Suc";
            // 
            // NumeroNotaPedido
            // 
            this.NumeroNotaPedido.Text = "Nro NP";
            this.NumeroNotaPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumeroNotaPedido.Width = 75;
            // 
            // Fecha
            // 
            this.Fecha.Text = "Fecha";
            this.Fecha.Width = 135;
            // 
            // IdCliente
            // 
            this.IdCliente.Text = "IdCliente";
            this.IdCliente.Width = 0;
            // 
            // RazonSocialCliente
            // 
            this.RazonSocialCliente.Text = "Razon Social";
            this.RazonSocialCliente.Width = 335;
            // 
            // Basico
            // 
            this.Basico.Text = "Básico";
            this.Basico.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Basico.Width = 0;
            // 
            // Descuento
            // 
            this.Descuento.Text = "Desc";
            this.Descuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Descuento.Width = 0;
            // 
            // Iva105
            // 
            this.Iva105.Text = "Total Iva 10,5";
            this.Iva105.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Iva105.Width = 0;
            // 
            // Iva21
            // 
            this.Iva21.Text = "Total Iva 21";
            this.Iva21.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Iva21.Width = 0;
            // 
            // Total
            // 
            this.Total.Text = "Total";
            this.Total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Total.Width = 75;
            // 
            // Observaciones
            // 
            this.Observaciones.Text = "Observaciones";
            this.Observaciones.Width = 135;
            // 
            // IdVendedor
            // 
            this.IdVendedor.Text = "IdVendedor";
            this.IdVendedor.Width = 0;
            // 
            // TotalCompletada
            // 
            this.TotalCompletada.Text = "Nivel Entrega";
            this.TotalCompletada.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TotalCompletada.Width = 90;
            // 
            // gpoNotaDePedido
            // 
            this.gpoNotaDePedido.Controls.Add(this.btnRemito);
            this.gpoNotaDePedido.Controls.Add(this.gpDetalleNP);
            this.gpoNotaDePedido.Controls.Add(this.gpIdentificacion);
            this.gpoNotaDePedido.Controls.Add(this.btnEliminar);
            this.gpoNotaDePedido.Controls.Add(this.btnModificar);
            this.gpoNotaDePedido.Controls.Add(this.btnCerrar);
            this.gpoNotaDePedido.Location = new System.Drawing.Point(232, 68);
            this.gpoNotaDePedido.Name = "gpoNotaDePedido";
            this.gpoNotaDePedido.Size = new System.Drawing.Size(731, 260);
            this.gpoNotaDePedido.TabIndex = 3;
            this.gpoNotaDePedido.TabStop = false;
            // 
            // btnRemito
            // 
            this.btnRemito.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRemito.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemito.Image = ((System.Drawing.Image)(resources.GetObject("btnRemito.Image")));
            this.btnRemito.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemito.Location = new System.Drawing.Point(349, 229);
            this.btnRemito.Name = "btnRemito";
            this.btnRemito.Size = new System.Drawing.Size(85, 25);
            this.btnRemito.TabIndex = 40;
            this.btnRemito.TabStop = false;
            this.btnRemito.Text = "   Remito";
            this.btnRemito.UseVisualStyleBackColor = false;
            this.btnRemito.Visible = false;
            this.btnRemito.Click += new System.EventHandler(this.Remito_Click);
            // 
            // gpDetalleNP
            // 
            this.gpDetalleNP.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpDetalleNP.Controls.Add(this.cboListaCliente);
            this.gpDetalleNP.Controls.Add(this.txtCodListaCliente);
            this.gpDetalleNP.Controls.Add(this.btnListaPrecioCliente);
            this.gpDetalleNP.Controls.Add(this.label1);
            this.gpDetalleNP.Controls.Add(this.cboVendedor);
            this.gpDetalleNP.Controls.Add(this.txtCodVendedor);
            this.gpDetalleNP.Controls.Add(this.btnVendedor);
            this.gpDetalleNP.Controls.Add(this.label14);
            this.gpDetalleNP.Controls.Add(this.cboCliente);
            this.gpDetalleNP.Controls.Add(this.txtCodCliente);
            this.gpDetalleNP.Controls.Add(this.btnCliente);
            this.gpDetalleNP.Controls.Add(this.label16);
            this.gpDetalleNP.Controls.Add(this.txtImpuesto2);
            this.gpDetalleNP.Controls.Add(this.label13);
            this.gpDetalleNP.Controls.Add(this.txtDescuento);
            this.gpDetalleNP.Controls.Add(this.label12);
            this.gpDetalleNP.Controls.Add(this.txtObservacionNotaPedido);
            this.gpDetalleNP.Controls.Add(this.label10);
            this.gpDetalleNP.Controls.Add(this.txtImpuesto1);
            this.gpDetalleNP.Controls.Add(this.label11);
            this.gpDetalleNP.Controls.Add(this.txtTotalFactur);
            this.gpDetalleNP.Controls.Add(this.label17);
            this.gpDetalleNP.Controls.Add(this.txtSubTotal);
            this.gpDetalleNP.Controls.Add(this.label15);
            this.gpDetalleNP.Controls.Add(this.txtCuit);
            this.gpDetalleNP.Controls.Add(this.label7);
            this.gpDetalleNP.Controls.Add(this.txtIva);
            this.gpDetalleNP.Controls.Add(this.label6);
            this.gpDetalleNP.Location = new System.Drawing.Point(6, 66);
            this.gpDetalleNP.Name = "gpDetalleNP";
            this.gpDetalleNP.Size = new System.Drawing.Size(718, 157);
            this.gpDetalleNP.TabIndex = 7;
            this.gpDetalleNP.TabStop = false;
            this.gpDetalleNP.Text = "Datos de Factura";
            // 
            // cboListaCliente
            // 
            this.cboListaCliente.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboListaCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboListaCliente.FormattingEnabled = true;
            this.cboListaCliente.Location = new System.Drawing.Point(493, 50);
            this.cboListaCliente.Name = "cboListaCliente";
            this.cboListaCliente.Size = new System.Drawing.Size(172, 21);
            this.cboListaCliente.TabIndex = 15;
            this.cboListaCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboListaCliente_KeyPress);
            // 
            // txtCodListaCliente
            // 
            this.txtCodListaCliente.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodListaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodListaCliente.Location = new System.Drawing.Point(437, 51);
            this.txtCodListaCliente.Name = "txtCodListaCliente";
            this.txtCodListaCliente.Size = new System.Drawing.Size(50, 20);
            this.txtCodListaCliente.TabIndex = 14;
            this.txtCodListaCliente.TextChanged += new System.EventHandler(this.txtCodListaCliente_TextChanged);
            this.txtCodListaCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodListaCliente_KeyPress);
            // 
            // btnListaPrecioCliente
            // 
            this.btnListaPrecioCliente.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnListaPrecioCliente.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnListaPrecioCliente.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnListaPrecioCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnListaPrecioCliente.Image")));
            this.btnListaPrecioCliente.Location = new System.Drawing.Point(671, 48);
            this.btnListaPrecioCliente.Name = "btnListaPrecioCliente";
            this.btnListaPrecioCliente.Size = new System.Drawing.Size(30, 25);
            this.btnListaPrecioCliente.TabIndex = 16;
            this.btnListaPrecioCliente.UseVisualStyleBackColor = false;
            this.btnListaPrecioCliente.Click += new System.EventHandler(this.btnListaPrecioCliente_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(341, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 1020;
            this.label1.Text = "Cod. Lista Precio:";
            // 
            // cboVendedor
            // 
            this.cboVendedor.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboVendedor.FormattingEnabled = true;
            this.cboVendedor.Location = new System.Drawing.Point(493, 21);
            this.cboVendedor.Name = "cboVendedor";
            this.cboVendedor.Size = new System.Drawing.Size(172, 21);
            this.cboVendedor.TabIndex = 12;
            this.cboVendedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboVendedor_KeyPress);
            // 
            // txtCodVendedor
            // 
            this.txtCodVendedor.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodVendedor.Location = new System.Drawing.Point(437, 22);
            this.txtCodVendedor.Name = "txtCodVendedor";
            this.txtCodVendedor.Size = new System.Drawing.Size(50, 20);
            this.txtCodVendedor.TabIndex = 11;
            this.txtCodVendedor.TextChanged += new System.EventHandler(this.txtCodVendedor_TextChanged);
            this.txtCodVendedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodVendedor_KeyPress);
            // 
            // btnVendedor
            // 
            this.btnVendedor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnVendedor.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnVendedor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVendedor.Image = ((System.Drawing.Image)(resources.GetObject("btnVendedor.Image")));
            this.btnVendedor.Location = new System.Drawing.Point(671, 19);
            this.btnVendedor.Name = "btnVendedor";
            this.btnVendedor.Size = new System.Drawing.Size(30, 25);
            this.btnVendedor.TabIndex = 13;
            this.btnVendedor.UseVisualStyleBackColor = false;
            this.btnVendedor.Click += new System.EventHandler(this.btnVendedor_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(350, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 13);
            this.label14.TabIndex = 1016;
            this.label14.Text = "Cod. Vendedor:";
            // 
            // cboCliente
            // 
            this.cboCliente.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboCliente.FormattingEnabled = true;
            this.cboCliente.Location = new System.Drawing.Point(144, 21);
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(150, 21);
            this.cboCliente.TabIndex = 9;
            this.cboCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboCliente_KeyPress);
            // 
            // txtCodCliente
            // 
            this.txtCodCliente.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodCliente.Location = new System.Drawing.Point(88, 21);
            this.txtCodCliente.Name = "txtCodCliente";
            this.txtCodCliente.Size = new System.Drawing.Size(50, 20);
            this.txtCodCliente.TabIndex = 8;
            this.txtCodCliente.TextChanged += new System.EventHandler(this.txtCodCliente_TextChanged);
            this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodCliente_KeyPress);
            // 
            // btnCliente
            // 
            this.btnCliente.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCliente.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnCliente.Image")));
            this.btnCliente.Location = new System.Drawing.Point(300, 20);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(30, 25);
            this.btnCliente.TabIndex = 10;
            this.btnCliente.UseVisualStyleBackColor = false;
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 13);
            this.label16.TabIndex = 1012;
            this.label16.Text = "Cod. Cliente:";
            // 
            // txtImpuesto2
            // 
            this.txtImpuesto2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtImpuesto2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImpuesto2.Location = new System.Drawing.Point(489, 114);
            this.txtImpuesto2.Name = "txtImpuesto2";
            this.txtImpuesto2.Size = new System.Drawing.Size(75, 20);
            this.txtImpuesto2.TabIndex = 30;
            this.txtImpuesto2.TabStop = false;
            this.txtImpuesto2.Text = "$ 0,00";
            this.txtImpuesto2.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(453, 117);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 13);
            this.label13.TabIndex = 108;
            this.label13.Text = "21%:";
            this.label13.Visible = false;
            // 
            // txtDescuento
            // 
            this.txtDescuento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescuento.Location = new System.Drawing.Point(220, 114);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Size = new System.Drawing.Size(75, 20);
            this.txtDescuento.TabIndex = 30;
            this.txtDescuento.TabStop = false;
            this.txtDescuento.Text = "$ 0,00";
            this.txtDescuento.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(176, 117);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 106;
            this.label12.Text = "Desc.:";
            this.label12.Visible = false;
            // 
            // txtObservacionNotaPedido
            // 
            this.txtObservacionNotaPedido.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtObservacionNotaPedido.Location = new System.Drawing.Point(88, 77);
            this.txtObservacionNotaPedido.Name = "txtObservacionNotaPedido";
            this.txtObservacionNotaPedido.Size = new System.Drawing.Size(613, 20);
            this.txtObservacionNotaPedido.TabIndex = 17;
            this.txtObservacionNotaPedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtObservacionNotaPedido_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 104;
            this.label10.Text = "Observación:";
            // 
            // txtImpuesto1
            // 
            this.txtImpuesto1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtImpuesto1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImpuesto1.Location = new System.Drawing.Point(356, 114);
            this.txtImpuesto1.Name = "txtImpuesto1";
            this.txtImpuesto1.Size = new System.Drawing.Size(75, 20);
            this.txtImpuesto1.TabIndex = 30;
            this.txtImpuesto1.TabStop = false;
            this.txtImpuesto1.Text = "$ 0,00";
            this.txtImpuesto1.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(311, 117);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 13);
            this.label11.TabIndex = 101;
            this.label11.Text = "10,5%:";
            this.label11.Visible = false;
            // 
            // txtTotalFactur
            // 
            this.txtTotalFactur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtTotalFactur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalFactur.Location = new System.Drawing.Point(626, 114);
            this.txtTotalFactur.Name = "txtTotalFactur";
            this.txtTotalFactur.Size = new System.Drawing.Size(75, 20);
            this.txtTotalFactur.TabIndex = 36;
            this.txtTotalFactur.TabStop = false;
            this.txtTotalFactur.Text = "$ 0,00";
            this.txtTotalFactur.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(586, 117);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(34, 13);
            this.label17.TabIndex = 98;
            this.label17.Text = "Total:";
            this.label17.Visible = false;
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubTotal.Location = new System.Drawing.Point(88, 114);
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.ReadOnly = true;
            this.txtSubTotal.Size = new System.Drawing.Size(75, 20);
            this.txtSubTotal.TabIndex = 30;
            this.txtSubTotal.TabStop = false;
            this.txtSubTotal.Text = "$ 0,00";
            this.txtSubTotal.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(29, 117);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 13);
            this.label15.TabIndex = 96;
            this.label15.Text = "SubTotal:";
            this.label15.Visible = false;
            // 
            // txtCuit
            // 
            this.txtCuit.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtCuit.Location = new System.Drawing.Point(245, 51);
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.ReadOnly = true;
            this.txtCuit.Size = new System.Drawing.Size(85, 20);
            this.txtCuit.TabIndex = 30;
            this.txtCuit.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(204, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 86;
            this.label7.Text = "CUIT:";
            // 
            // txtIva
            // 
            this.txtIva.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtIva.Location = new System.Drawing.Point(88, 51);
            this.txtIva.Name = "txtIva";
            this.txtIva.ReadOnly = true;
            this.txtIva.Size = new System.Drawing.Size(110, 20);
            this.txtIva.TabIndex = 30;
            this.txtIva.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 84;
            this.label6.Text = "Iva:";
            // 
            // gpIdentificacion
            // 
            this.gpIdentificacion.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpIdentificacion.Controls.Add(this.label33);
            this.gpIdentificacion.Controls.Add(this.txtNroNPedido);
            this.gpIdentificacion.Controls.Add(this.cboNroSucursal);
            this.gpIdentificacion.Controls.Add(this.label4);
            this.gpIdentificacion.Controls.Add(this.dtpFechaFactu);
            this.gpIdentificacion.Controls.Add(this.txtNroNotaPedido);
            this.gpIdentificacion.Controls.Add(this.lblSucursal);
            this.gpIdentificacion.Controls.Add(this.label5);
            this.gpIdentificacion.Controls.Add(this.txtNroInternoNota);
            this.gpIdentificacion.Controls.Add(this.lblNroInterno);
            this.gpIdentificacion.Location = new System.Drawing.Point(6, 10);
            this.gpIdentificacion.Name = "gpIdentificacion";
            this.gpIdentificacion.Size = new System.Drawing.Size(720, 50);
            this.gpIdentificacion.TabIndex = 4;
            this.gpIdentificacion.TabStop = false;
            this.gpIdentificacion.Text = "Identificación";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(217, 22);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(89, 13);
            this.label33.TabIndex = 802;
            this.label33.Text = "Nro Nota Pedido:";
            // 
            // txtNroNPedido
            // 
            this.txtNroNPedido.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNroNPedido.Location = new System.Drawing.Point(312, 19);
            this.txtNroNPedido.Name = "txtNroNPedido";
            this.txtNroNPedido.Size = new System.Drawing.Size(99, 20);
            this.txtNroNPedido.TabIndex = 801;
            // 
            // cboNroSucursal
            // 
            this.cboNroSucursal.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboNroSucursal.Enabled = false;
            this.cboNroSucursal.FormattingEnabled = true;
            this.cboNroSucursal.Location = new System.Drawing.Point(142, 18);
            this.cboNroSucursal.Name = "cboNroSucursal";
            this.cboNroSucursal.Size = new System.Drawing.Size(69, 21);
            this.cboNroSucursal.TabIndex = 5;
            this.cboNroSucursal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboNroSucursal_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 136;
            this.label4.Text = "Nro N.P.:";
            this.label4.Visible = false;
            // 
            // dtpFechaFactu
            // 
            this.dtpFechaFactu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFactu.Location = new System.Drawing.Point(504, 19);
            this.dtpFechaFactu.Name = "dtpFechaFactu";
            this.dtpFechaFactu.Size = new System.Drawing.Size(120, 20);
            this.dtpFechaFactu.TabIndex = 6;
            this.dtpFechaFactu.Value = new System.DateTime(2015, 3, 16, 0, 0, 0, 0);
            this.dtpFechaFactu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaFactu_KeyPress);
            // 
            // txtNroNotaPedido
            // 
            this.txtNroNotaPedido.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtNroNotaPedido.Location = new System.Drawing.Point(645, 25);
            this.txtNroNotaPedido.Name = "txtNroNotaPedido";
            this.txtNroNotaPedido.ReadOnly = true;
            this.txtNroNotaPedido.Size = new System.Drawing.Size(69, 20);
            this.txtNroNotaPedido.TabIndex = 800;
            this.txtNroNotaPedido.TabStop = false;
            this.txtNroNotaPedido.Visible = false;
            // 
            // lblSucursal
            // 
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Location = new System.Drawing.Point(85, 22);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(51, 13);
            this.lblSucursal.TabIndex = 68;
            this.lblSucursal.Text = "Sucursal:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(422, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 134;
            this.label5.Text = "Fecha Pedido:";
            // 
            // txtNroInternoNota
            // 
            this.txtNroInternoNota.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtNroInternoNota.Location = new System.Drawing.Point(63, 18);
            this.txtNroInternoNota.Name = "txtNroInternoNota";
            this.txtNroInternoNota.ReadOnly = true;
            this.txtNroInternoNota.Size = new System.Drawing.Size(16, 20);
            this.txtNroInternoNota.TabIndex = 50;
            this.txtNroInternoNota.TabStop = false;
            this.txtNroInternoNota.Text = "0";
            this.txtNroInternoNota.Visible = false;
            // 
            // lblNroInterno
            // 
            this.lblNroInterno.AutoSize = true;
            this.lblNroInterno.Location = new System.Drawing.Point(656, 9);
            this.lblNroInterno.Name = "lblNroInterno";
            this.lblNroInterno.Size = new System.Drawing.Size(58, 13);
            this.lblNroInterno.TabIndex = 66;
            this.lblNroInterno.Text = "Id. Interno:";
            this.lblNroInterno.Visible = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(531, 229);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(85, 25);
            this.btnEliminar.TabIndex = 38;
            this.btnEliminar.TabStop = false;
            this.btnEliminar.Text = "   Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.Location = new System.Drawing.Point(440, 229);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(85, 25);
            this.btnModificar.TabIndex = 37;
            this.btnModificar.TabStop = false;
            this.btnModificar.Text = "   Actualizar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrar.Location = new System.Drawing.Point(622, 229);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(85, 25);
            this.btnCerrar.TabIndex = 39;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Text = "   Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnActicarDetallesNP
            // 
            this.btnActicarDetallesNP.BackColor = System.Drawing.SystemColors.Control;
            this.btnActicarDetallesNP.Image = ((System.Drawing.Image)(resources.GetObject("btnActicarDetallesNP.Image")));
            this.btnActicarDetallesNP.Location = new System.Drawing.Point(946, 643);
            this.btnActicarDetallesNP.Name = "btnActicarDetallesNP";
            this.btnActicarDetallesNP.Size = new System.Drawing.Size(20, 20);
            this.btnActicarDetallesNP.TabIndex = 152;
            this.btnActicarDetallesNP.TabStop = false;
            this.btnActicarDetallesNP.UseVisualStyleBackColor = false;
            this.btnActicarDetallesNP.Visible = false;
            this.btnActicarDetallesNP.Click += new System.EventHandler(this.btnActicarDetallesNP_Click);
            // 
            // gpbRacionalizador
            // 
            this.gpbRacionalizador.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gpbRacionalizador.Controls.Add(this.groupBox1);
            this.gpbRacionalizador.Controls.Add(this.cboDescArticulo);
            this.gpbRacionalizador.Controls.Add(this.pictureBox1);
            this.gpbRacionalizador.Controls.Add(this.txtCantTotalArticulo);
            this.gpbRacionalizador.Controls.Add(this.label9);
            this.gpbRacionalizador.Controls.Add(this.btnCerrarRacionalizador);
            this.gpbRacionalizador.Controls.Add(this.btnBuscaArtNPCliente);
            this.gpbRacionalizador.Controls.Add(this.groupBox3);
            this.gpbRacionalizador.Controls.Add(this.txtCodArtic);
            this.gpbRacionalizador.Controls.Add(this.label2);
            this.gpbRacionalizador.Controls.Add(this.groupBox2);
            this.gpbRacionalizador.Location = new System.Drawing.Point(12, 12);
            this.gpbRacionalizador.Name = "gpbRacionalizador";
            this.gpbRacionalizador.Size = new System.Drawing.Size(954, 651);
            this.gpbRacionalizador.TabIndex = 154;
            this.gpbRacionalizador.TabStop = false;
            this.gpbRacionalizador.Text = "Racionalizador de Existencia por Cliente/Producto";
            this.gpbRacionalizador.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.pboxArtPedido);
            this.groupBox1.Controls.Add(this.lblExistenciaArtic);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.lblCantClientePedido);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.lblTotalPedidoArticulo);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(538, 478);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 78);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cuantificadores";
            // 
            // pboxArtPedido
            // 
            this.pboxArtPedido.Image = ((System.Drawing.Image)(resources.GetObject("pboxArtPedido.Image")));
            this.pboxArtPedido.Location = new System.Drawing.Point(174, 19);
            this.pboxArtPedido.Name = "pboxArtPedido";
            this.pboxArtPedido.Size = new System.Drawing.Size(48, 48);
            this.pboxArtPedido.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pboxArtPedido.TabIndex = 38;
            this.pboxArtPedido.TabStop = false;
            // 
            // lblExistenciaArtic
            // 
            this.lblExistenciaArtic.AutoSize = true;
            this.lblExistenciaArtic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExistenciaArtic.Location = new System.Drawing.Point(119, 56);
            this.lblExistenciaArtic.Name = "lblExistenciaArtic";
            this.lblExistenciaArtic.Size = new System.Drawing.Size(14, 15);
            this.lblExistenciaArtic.TabIndex = 5;
            this.lblExistenciaArtic.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(49, 58);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(58, 13);
            this.label21.TabIndex = 4;
            this.label21.Text = "Existencia:";
            // 
            // lblCantClientePedido
            // 
            this.lblCantClientePedido.AutoSize = true;
            this.lblCantClientePedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantClientePedido.Location = new System.Drawing.Point(119, 23);
            this.lblCantClientePedido.Name = "lblCantClientePedido";
            this.lblCantClientePedido.Size = new System.Drawing.Size(14, 15);
            this.lblCantClientePedido.TabIndex = 3;
            this.lblCantClientePedido.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(37, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 13);
            this.label20.TabIndex = 2;
            this.label20.Text = "Cant. Cliente:";
            // 
            // lblTotalPedidoArticulo
            // 
            this.lblTotalPedidoArticulo.AutoSize = true;
            this.lblTotalPedidoArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPedidoArticulo.Location = new System.Drawing.Point(119, 40);
            this.lblTotalPedidoArticulo.Name = "lblTotalPedidoArticulo";
            this.lblTotalPedidoArticulo.Size = new System.Drawing.Size(14, 15);
            this.lblTotalPedidoArticulo.TabIndex = 1;
            this.lblTotalPedidoArticulo.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(9, 42);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(98, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Total Cant. Pedida:";
            // 
            // cboDescArticulo
            // 
            this.cboDescArticulo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboDescArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboDescArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDescArticulo.FormattingEnabled = true;
            this.cboDescArticulo.Location = new System.Drawing.Point(306, 21);
            this.cboDescArticulo.Name = "cboDescArticulo";
            this.cboDescArticulo.Size = new System.Drawing.Size(297, 21);
            this.cboDescArticulo.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(797, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            // 
            // txtCantTotalArticulo
            // 
            this.txtCantTotalArticulo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCantTotalArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantTotalArticulo.Location = new System.Drawing.Point(727, 21);
            this.txtCantTotalArticulo.Name = "txtCantTotalArticulo";
            this.txtCantTotalArticulo.ReadOnly = true;
            this.txtCantTotalArticulo.Size = new System.Drawing.Size(64, 22);
            this.txtCantTotalArticulo.TabIndex = 34;
            this.txtCantTotalArticulo.TabStop = false;
            this.txtCantTotalArticulo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(645, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Cantidad Total";
            // 
            // btnCerrarRacionalizador
            // 
            this.btnCerrarRacionalizador.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrarRacionalizador.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarRacionalizador.Image")));
            this.btnCerrarRacionalizador.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrarRacionalizador.Location = new System.Drawing.Point(860, 560);
            this.btnCerrarRacionalizador.Name = "btnCerrarRacionalizador";
            this.btnCerrarRacionalizador.Size = new System.Drawing.Size(85, 25);
            this.btnCerrarRacionalizador.TabIndex = 32;
            this.btnCerrarRacionalizador.Text = "   Cerrar";
            this.btnCerrarRacionalizador.UseVisualStyleBackColor = true;
            this.btnCerrarRacionalizador.Click += new System.EventHandler(this.btnCerrarRacionalizador_Click);
            // 
            // btnBuscaArtNPCliente
            // 
            this.btnBuscaArtNPCliente.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBuscaArtNPCliente.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuscaArtNPCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscaArtNPCliente.Image")));
            this.btnBuscaArtNPCliente.Location = new System.Drawing.Point(609, 18);
            this.btnBuscaArtNPCliente.Name = "btnBuscaArtNPCliente";
            this.btnBuscaArtNPCliente.Size = new System.Drawing.Size(30, 25);
            this.btnBuscaArtNPCliente.TabIndex = 3;
            this.btnBuscaArtNPCliente.UseVisualStyleBackColor = true;
            this.btnBuscaArtNPCliente.Click += new System.EventHandler(this.btnBuscaArtNPCliente_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox3.Controls.Add(this.pictureBox2);
            this.groupBox3.Controls.Add(this.btnAsignar);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.btnQuitaCantidad);
            this.groupBox3.Controls.Add(this.txtProporcionManualPedido);
            this.groupBox3.Controls.Add(this.btnAgregaCantidad);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(186, 478);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(346, 79);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Regulador de Cantidad";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(281, 19);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 39;
            this.pictureBox2.TabStop = false;
            // 
            // btnAsignar
            // 
            this.btnAsignar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAsignar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAsignar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignar.Image = ((System.Drawing.Image)(resources.GetObject("btnAsignar.Image")));
            this.btnAsignar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAsignar.Location = new System.Drawing.Point(155, 42);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(99, 25);
            this.btnAsignar.TabIndex = 7;
            this.btnAsignar.Text = "   Asignar";
            this.btnAsignar.UseVisualStyleBackColor = false;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(203, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Programador de Cantidades Reasignadas";
            // 
            // btnQuitaCantidad
            // 
            this.btnQuitaCantidad.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnQuitaCantidad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnQuitaCantidad.Image = ((System.Drawing.Image)(resources.GetObject("btnQuitaCantidad.Image")));
            this.btnQuitaCantidad.Location = new System.Drawing.Point(109, 41);
            this.btnQuitaCantidad.Name = "btnQuitaCantidad";
            this.btnQuitaCantidad.Size = new System.Drawing.Size(35, 25);
            this.btnQuitaCantidad.TabIndex = 6;
            this.btnQuitaCantidad.UseVisualStyleBackColor = false;
            this.btnQuitaCantidad.Click += new System.EventHandler(this.btnQuitaCantidad_Click);
            // 
            // txtProporcionManualPedido
            // 
            this.txtProporcionManualPedido.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtProporcionManualPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProporcionManualPedido.Location = new System.Drawing.Point(50, 44);
            this.txtProporcionManualPedido.Name = "txtProporcionManualPedido";
            this.txtProporcionManualPedido.ReadOnly = true;
            this.txtProporcionManualPedido.Size = new System.Drawing.Size(53, 20);
            this.txtProporcionManualPedido.TabIndex = 5;
            this.txtProporcionManualPedido.Text = "0";
            this.txtProporcionManualPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtProporcionManualPedido.TextChanged += new System.EventHandler(this.txtProporcionManualPedido_TextChanged);
            this.txtProporcionManualPedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProporcionManualPedido_KeyPress);
            // 
            // btnAgregaCantidad
            // 
            this.btnAgregaCantidad.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAgregaCantidad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregaCantidad.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregaCantidad.Image")));
            this.btnAgregaCantidad.Location = new System.Drawing.Point(9, 41);
            this.btnAgregaCantidad.Name = "btnAgregaCantidad";
            this.btnAgregaCantidad.Size = new System.Drawing.Size(35, 25);
            this.btnAgregaCantidad.TabIndex = 4;
            this.btnAgregaCantidad.UseVisualStyleBackColor = false;
            this.btnAgregaCantidad.Click += new System.EventHandler(this.btnAgregaCantidad_Click);
            // 
            // txtCodArtic
            // 
            this.txtCodArtic.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodArtic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodArtic.Location = new System.Drawing.Point(217, 21);
            this.txtCodArtic.Name = "txtCodArtic";
            this.txtCodArtic.Size = new System.Drawing.Size(83, 20);
            this.txtCodArtic.TabIndex = 1;
            this.txtCodArtic.TextChanged += new System.EventHandler(this.txtCodArtic_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cód. Artículo";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox2.Controls.Add(this.lvwPedidoClientes);
            this.groupBox2.Location = new System.Drawing.Point(65, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(826, 425);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pedidos por Cliente";
            // 
            // lvwPedidoClientes
            // 
            this.lvwPedidoClientes.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwPedidoClientes.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwPedidoClientes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NP,
            this.IdCliente_,
            this.RazonSocial,
            this.FechaNota,
            this.CantidadPedida,
            this.CantLimiteEntregar,
            this.CantReasignada});
            this.lvwPedidoClientes.FullRowSelect = true;
            this.lvwPedidoClientes.GridLines = true;
            this.lvwPedidoClientes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwPedidoClientes.HideSelection = false;
            this.lvwPedidoClientes.LargeImageList = this.imageList1;
            this.lvwPedidoClientes.Location = new System.Drawing.Point(6, 19);
            this.lvwPedidoClientes.MultiSelect = false;
            this.lvwPedidoClientes.Name = "lvwPedidoClientes";
            this.lvwPedidoClientes.Size = new System.Drawing.Size(814, 400);
            this.lvwPedidoClientes.SmallImageList = this.imageList1;
            this.lvwPedidoClientes.TabIndex = 4;
            this.lvwPedidoClientes.TabStop = false;
            this.lvwPedidoClientes.UseCompatibleStateImageBehavior = false;
            this.lvwPedidoClientes.View = System.Windows.Forms.View.Details;
            this.lvwPedidoClientes.SelectedIndexChanged += new System.EventHandler(this.lvwPedidoClientes_SelectedIndexChanged);
            // 
            // NP
            // 
            this.NP.Text = "-";
            this.NP.Width = 28;
            // 
            // IdCliente_
            // 
            this.IdCliente_.Text = "IdCliente";
            this.IdCliente_.Width = 0;
            // 
            // RazonSocial
            // 
            this.RazonSocial.Text = "Razón Social";
            this.RazonSocial.Width = 375;
            // 
            // FechaNota
            // 
            this.FechaNota.Text = "Fecha Pedido";
            this.FechaNota.Width = 85;
            // 
            // CantidadPedida
            // 
            this.CantidadPedida.Text = "Cant. Pedida";
            this.CantidadPedida.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CantidadPedida.Width = 80;
            // 
            // CantLimiteEntregar
            // 
            this.CantLimiteEntregar.Text = "Lim. Entrega";
            this.CantLimiteEntregar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CantLimiteEntregar.Width = 75;
            // 
            // CantReasignada
            // 
            this.CantReasignada.Text = "Reasignada";
            this.CantReasignada.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CantReasignada.Width = 80;
            // 
            // frmNotaPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(973, 670);
            this.Controls.Add(this.gbLeyenda);
            this.Controls.Add(this.gpbRacionalizador);
            this.Controls.Add(this.btnActicarDetallesNP);
            this.Controls.Add(this.gpNotaPedido);
            this.Controls.Add(this.gpoNotaDePedido);
            this.Controls.Add(this.gpNP);
            this.Controls.Add(this.gpoDetalleNota);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmNotaPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nota de Pedido";
            this.Load += new System.EventHandler(this.frmPedidoNota_Load);
            this.gpoDetalleNota.ResumeLayout(false);
            this.gpoDetalleNota.PerformLayout();
            this.gpNP.ResumeLayout(false);
            this.gpNP.PerformLayout();
            this.tlsBarArticulo.ResumeLayout(false);
            this.tlsBarArticulo.PerformLayout();
            this.gbLeyenda.ResumeLayout(false);
            this.gbLeyenda.PerformLayout();
            this.gpNotaPedido.ResumeLayout(false);
            this.gpoNotaDePedido.ResumeLayout(false);
            this.gpDetalleNP.ResumeLayout(false);
            this.gpDetalleNP.PerformLayout();
            this.gpIdentificacion.ResumeLayout(false);
            this.gpIdentificacion.PerformLayout();
            this.gpbRacionalizador.ResumeLayout(false);
            this.gpbRacionalizador.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxArtPedido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpoDetalleNota;
        private System.Windows.Forms.TextBox txtCantArticulo;
        private System.Windows.Forms.Label lblCantActual;
        private System.Windows.Forms.TextBox txtCodigoArticulo;
        private System.Windows.Forms.ComboBox cboArticulo;
        private System.Windows.Forms.Button btnBuscaArticulo;
        private System.Windows.Forms.Label lblCodArt;
        private System.Windows.Forms.GroupBox gpNP;
        private System.Windows.Forms.ToolStrip tlsBarArticulo;
        private System.Windows.Forms.ToolStripButton tsBtnNuevo;
        private System.Windows.Forms.ToolStripButton tsBtnModificar;
        private System.Windows.Forms.ToolStripButton tsBtnBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnReporte;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TextBox txtBuscarArticulo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboBuscaNotaPedido;
        private System.Windows.Forms.ToolStripButton tsbtnReporteGenerico;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtCantPedida;
        private System.Windows.Forms.Label lblCantPedida;
        private System.Windows.Forms.TextBox txtCantRestante;
        private System.Windows.Forms.Label lblCantRestante;
        private System.Windows.Forms.Button btnQuitaArt;
        private System.Windows.Forms.Button btnAgregaArt;
        private System.Windows.Forms.ListView lvwDetalleNotaPedido;
        private System.Windows.Forms.ColumnHeader IdArticulo;
        private System.Windows.Forms.ColumnHeader Codigo;
        private System.Windows.Forms.ColumnHeader Artículos;
        private System.Windows.Forms.ColumnHeader Existencia;
        private System.Windows.Forms.ColumnHeader PrecioUnitario;
        private System.Windows.Forms.ColumnHeader Subtotal;
        private System.Windows.Forms.ColumnHeader Desc;
        private System.Windows.Forms.ColumnHeader Iva;
        private System.Windows.Forms.ColumnHeader Importe;
        private System.Windows.Forms.ColumnHeader IdDNota;
        private System.Windows.Forms.ColumnHeader iva1;
        private System.Windows.Forms.ColumnHeader iva2;
        private System.Windows.Forms.ColumnHeader CantArtPedida;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox gpNotaPedido;
        private System.Windows.Forms.ListView lvwNotaPedido;
        private System.Windows.Forms.ColumnHeader IDInterno;
        private System.Windows.Forms.ColumnHeader Basico;
        private System.Windows.Forms.ColumnHeader Descuento;
        private System.Windows.Forms.ColumnHeader Iva105;
        private System.Windows.Forms.ColumnHeader Iva21;
        private System.Windows.Forms.ColumnHeader Total;
        private System.Windows.Forms.ColumnHeader Observaciones;
        private System.Windows.Forms.GroupBox gpoNotaDePedido;
        private System.Windows.Forms.GroupBox gpDetalleNP;
        private System.Windows.Forms.ComboBox cboVendedor;
        private System.Windows.Forms.TextBox txtCodVendedor;
        private System.Windows.Forms.Button btnVendedor;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.TextBox txtCodCliente;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtImpuesto2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtObservacionNotaPedido;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtImpuesto1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTotalFactur;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtSubTotal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIva;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gpIdentificacion;
        private System.Windows.Forms.DateTimePicker dtpFechaFactu;
        private System.Windows.Forms.TextBox txtNroInternoNota;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.Label lblNroInterno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.ColumnHeader IdCliente;
        private System.Windows.Forms.ColumnHeader RazonSocialCliente;
        private System.Windows.Forms.ColumnHeader CantArtRestante;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNroNotaPedido;
        private System.Windows.Forms.ComboBox cboNroSucursal;
        private System.Windows.Forms.ColumnHeader Fecha;
        private System.Windows.Forms.ColumnHeader IdVendedor;
        private System.Windows.Forms.ComboBox cboListaCliente;
        private System.Windows.Forms.TextBox txtCodListaCliente;
        private System.Windows.Forms.Button btnListaPrecioCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCerrarDetalle;
        private System.Windows.Forms.ColumnHeader Faltante;
        private System.Windows.Forms.ColumnHeader Remitido;
        private System.Windows.Forms.ColumnHeader TotalCompletada;
        private System.Windows.Forms.Button btnActicarDetallesNP;
        private System.Windows.Forms.ColumnHeader Entregar;
        private System.Windows.Forms.GroupBox gpbRacionalizador;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAgregaCantidad;
        private System.Windows.Forms.TextBox txtCodArtic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvwPedidoClientes;
        private System.Windows.Forms.ColumnHeader NP;
        private System.Windows.Forms.ColumnHeader IdCliente_;
        private System.Windows.Forms.ColumnHeader RazonSocial;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnQuitaCantidad;
        private System.Windows.Forms.TextBox txtProporcionManualPedido;
        private System.Windows.Forms.Button btnBuscaArtNPCliente;
        private System.Windows.Forms.ToolStripButton tsRacionalizador;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Button btnCerrarRacionalizador;
        private System.Windows.Forms.ColumnHeader CantidadPedida;
        private System.Windows.Forms.TextBox txtCantTotalArticulo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ColumnHeader CantReasignada;
        private System.Windows.Forms.ComboBox cboDescArticulo;
        private System.Windows.Forms.ColumnHeader FechaNota;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTotalPedidoArticulo;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblCantClientePedido;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblExistenciaArtic;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.PictureBox pboxArtPedido;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ColumnHeader Reasignado;
        private System.Windows.Forms.Button btnRemito;
        private System.Windows.Forms.ColumnHeader CantLimiteEntregar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton Referencias;
        private System.Windows.Forms.GroupBox gbLeyenda;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnCerrarLeyenda;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ToolStripButton tsRemito;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txtNroNPedido;
        private System.Windows.Forms.ColumnHeader NumeroNotaPedido;
        private System.Windows.Forms.ColumnHeader Suc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}