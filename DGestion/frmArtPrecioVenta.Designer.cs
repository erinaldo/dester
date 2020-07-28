namespace DGestion
{
    partial class frmArtPrecioVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArtPrecioVenta));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboBuscarArt = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsBtnModificar = new System.Windows.Forms.ToolStripButton();
            this.tsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.tsBtnListaPrecio = new System.Windows.Forms.ToolStripButton();
            this.tsBtnRecargarListas = new System.Windows.Forms.ToolStripButton();
            this.tsBtnVincularSinFechaActualizacion = new System.Windows.Forms.ToolStripButton();
            this.tsBtnVincular = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnBuscaLista = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.txtConsulta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboBuscar = new System.Windows.Forms.ComboBox();
            this.gpoCliente = new System.Windows.Forms.GroupBox();
            this.txtDescripcionArticulo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btcCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtDescripListaPrecio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbListaPrecio = new System.Windows.Forms.GroupBox();
            this.txtFlete = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lvwListaPrecioVenta = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Descripcion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PrecioCostoLista = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PrecioVenta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PrecioConIva = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UltimaActualización = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnSalir = new System.Windows.Forms.Button();
            this.txtPorcentaje = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombreLista = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodLista = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gpoCliente.SuspendLayout();
            this.gbListaPrecio.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cboBuscarArt);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.txtConsulta);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboBuscar);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(829, 49);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(246, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Buscar:";
            // 
            // cboBuscarArt
            // 
            this.cboBuscarArt.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboBuscarArt.FormattingEnabled = true;
            this.cboBuscarArt.Items.AddRange(new object[] {
            "Descripción",
            "Cód. Artículo"});
            this.cboBuscarArt.Location = new System.Drawing.Point(295, 16);
            this.cboBuscarArt.Name = "cboBuscarArt";
            this.cboBuscarArt.Size = new System.Drawing.Size(115, 21);
            this.cboBuscarArt.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnNuevo,
            this.tsBtnModificar,
            this.tsBtnBuscar,
            this.tsBtnListaPrecio,
            this.tsBtnRecargarListas,
            this.tsBtnVincularSinFechaActualizacion,
            this.tsBtnVincular,
            this.toolStripSeparator1,
            this.tsBtnBuscaLista,
            this.toolStripSeparator3,
            this.tsBtnSalir,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(556, 15);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(273, 31);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnNuevo
            // 
            this.tsBtnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnNuevo.Enabled = false;
            this.tsBtnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnNuevo.Image")));
            this.tsBtnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnNuevo.Name = "tsBtnNuevo";
            this.tsBtnNuevo.Size = new System.Drawing.Size(28, 28);
            this.tsBtnNuevo.Text = "Nuevo Artículo";
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
            this.tsBtnModificar.ToolTipText = "Modificar Datos";
            this.tsBtnModificar.Click += new System.EventHandler(this.tsBtnModificar_Click);
            // 
            // tsBtnBuscar
            // 
            this.tsBtnBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnBuscar.Image")));
            this.tsBtnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnBuscar.Name = "tsBtnBuscar";
            this.tsBtnBuscar.Size = new System.Drawing.Size(28, 28);
            this.tsBtnBuscar.Text = "Buscar Items";
            this.tsBtnBuscar.Click += new System.EventHandler(this.tsBtnBuscar_Click);
            // 
            // tsBtnListaPrecio
            // 
            this.tsBtnListaPrecio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnListaPrecio.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnListaPrecio.Image")));
            this.tsBtnListaPrecio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnListaPrecio.Name = "tsBtnListaPrecio";
            this.tsBtnListaPrecio.Size = new System.Drawing.Size(28, 28);
            this.tsBtnListaPrecio.Text = "Administrar Listas de Precios";
            this.tsBtnListaPrecio.ToolTipText = "Administrar Listas de Precios";
            this.tsBtnListaPrecio.Click += new System.EventHandler(this.tsBtnListaPrecio_Click);
            // 
            // tsBtnRecargarListas
            // 
            this.tsBtnRecargarListas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnRecargarListas.Enabled = false;
            this.tsBtnRecargarListas.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnRecargarListas.Image")));
            this.tsBtnRecargarListas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRecargarListas.Name = "tsBtnRecargarListas";
            this.tsBtnRecargarListas.Size = new System.Drawing.Size(28, 28);
            this.tsBtnRecargarListas.Text = "Cargar nuevos artículos ingresados en listas de precios";
            this.tsBtnRecargarListas.ToolTipText = "Cargar nuevos artículos en listas de precios";
            this.tsBtnRecargarListas.Click += new System.EventHandler(this.tsBtnRecargarListas_Click);
            // 
            // tsBtnVincularSinFechaActualizacion
            // 
            this.tsBtnVincularSinFechaActualizacion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnVincularSinFechaActualizacion.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnVincularSinFechaActualizacion.Image")));
            this.tsBtnVincularSinFechaActualizacion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnVincularSinFechaActualizacion.Name = "tsBtnVincularSinFechaActualizacion";
            this.tsBtnVincularSinFechaActualizacion.Size = new System.Drawing.Size(28, 28);
            this.tsBtnVincularSinFechaActualizacion.Text = "Actualizar articulos precios/porcentajes en lista publico";
            this.tsBtnVincularSinFechaActualizacion.Click += new System.EventHandler(this.tsBtnVincularSinFechaActualizacion_Click);
            // 
            // tsBtnVincular
            // 
            this.tsBtnVincular.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnVincular.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnVincular.Image")));
            this.tsBtnVincular.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnVincular.Name = "tsBtnVincular";
            this.tsBtnVincular.Size = new System.Drawing.Size(28, 28);
            this.tsBtnVincular.Text = "Vincular artículos y porcentajes a lista de precio";
            this.tsBtnVincular.ToolTipText = "Vincular artículo y porcentaje a lista seleccionada";
            this.tsBtnVincular.Click += new System.EventHandler(this.tsBtnVincular_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsBtnBuscaLista
            // 
            this.tsBtnBuscaLista.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnBuscaLista.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnBuscaLista.Image")));
            this.tsBtnBuscaLista.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnBuscaLista.Name = "tsBtnBuscaLista";
            this.tsBtnBuscaLista.Size = new System.Drawing.Size(28, 28);
            this.tsBtnBuscaLista.Text = "Reportes";
            this.tsBtnBuscaLista.Click += new System.EventHandler(this.tsBtnBuscaLista_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // tsBtnSalir
            // 
            this.tsBtnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSalir.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSalir.Image")));
            this.tsBtnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSalir.Name = "tsBtnSalir";
            this.tsBtnSalir.Size = new System.Drawing.Size(28, 28);
            this.tsBtnSalir.Text = "Salir";
            this.tsBtnSalir.Click += new System.EventHandler(this.tsBtnSalir_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // txtConsulta
            // 
            this.txtConsulta.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtConsulta.Location = new System.Drawing.Point(416, 17);
            this.txtConsulta.Name = "txtConsulta";
            this.txtConsulta.Size = new System.Drawing.Size(134, 20);
            this.txtConsulta.TabIndex = 2;
            this.txtConsulta.TextChanged += new System.EventHandler(this.txtConsulta_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Listas:";
            // 
            // cboBuscar
            // 
            this.cboBuscar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBuscar.ForeColor = System.Drawing.Color.Navy;
            this.cboBuscar.FormattingEnabled = true;
            this.cboBuscar.Items.AddRange(new object[] {
            "\"-\""});
            this.cboBuscar.Location = new System.Drawing.Point(49, 13);
            this.cboBuscar.Name = "cboBuscar";
            this.cboBuscar.Size = new System.Drawing.Size(187, 24);
            this.cboBuscar.TabIndex = 0;
            this.cboBuscar.SelectedIndexChanged += new System.EventHandler(this.cboBuscar_SelectedIndexChanged);
            this.cboBuscar.TextChanged += new System.EventHandler(this.cboBuscar_TextChanged);
            this.cboBuscar.Enter += new System.EventHandler(this.cboBuscar_Enter);
            // 
            // gpoCliente
            // 
            this.gpoCliente.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpoCliente.Controls.Add(this.txtDescripcionArticulo);
            this.gpoCliente.Controls.Add(this.label8);
            this.gpoCliente.Controls.Add(this.txtCodigo);
            this.gpoCliente.Controls.Add(this.label1);
            this.gpoCliente.Controls.Add(this.btnEliminar);
            this.gpoCliente.Controls.Add(this.btnModificar);
            this.gpoCliente.Controls.Add(this.btcCerrar);
            this.gpoCliente.Controls.Add(this.btnGuardar);
            this.gpoCliente.Controls.Add(this.txtDescripListaPrecio);
            this.gpoCliente.Controls.Add(this.label4);
            this.gpoCliente.Location = new System.Drawing.Point(12, 482);
            this.gpoCliente.Name = "gpoCliente";
            this.gpoCliente.Size = new System.Drawing.Size(829, 93);
            this.gpoCliente.TabIndex = 47;
            this.gpoCliente.TabStop = false;
            // 
            // txtDescripcionArticulo
            // 
            this.txtDescripcionArticulo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtDescripcionArticulo.Location = new System.Drawing.Point(207, 19);
            this.txtDescripcionArticulo.Name = "txtDescripcionArticulo";
            this.txtDescripcionArticulo.ReadOnly = true;
            this.txtDescripcionArticulo.Size = new System.Drawing.Size(456, 20);
            this.txtDescripcionArticulo.TabIndex = 33;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(135, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Descripción:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtCodigo.Location = new System.Drawing.Point(55, 19);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(70, 20);
            this.txtCodigo.TabIndex = 7;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Código:";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(647, 62);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(85, 25);
            this.btnEliminar.TabIndex = 11;
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
            this.btnModificar.Location = new System.Drawing.Point(556, 62);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(85, 25);
            this.btnModificar.TabIndex = 10;
            this.btnModificar.Text = "   Actualizar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btcCerrar
            // 
            this.btcCerrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btcCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btcCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btcCerrar.Image")));
            this.btcCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btcCerrar.Location = new System.Drawing.Point(738, 62);
            this.btcCerrar.Name = "btcCerrar";
            this.btcCerrar.Size = new System.Drawing.Size(85, 25);
            this.btcCerrar.TabIndex = 12;
            this.btcCerrar.Text = "   Cerrar";
            this.btcCerrar.UseVisualStyleBackColor = false;
            this.btcCerrar.Click += new System.EventHandler(this.btcCerrar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(465, 62);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 25);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "   Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtDescripListaPrecio
            // 
            this.txtDescripListaPrecio.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtDescripListaPrecio.Location = new System.Drawing.Point(745, 19);
            this.txtDescripListaPrecio.Name = "txtDescripListaPrecio";
            this.txtDescripListaPrecio.Size = new System.Drawing.Size(78, 20);
            this.txtDescripListaPrecio.TabIndex = 8;
            this.txtDescripListaPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripListaPrecio_KeyPress);
            this.txtDescripListaPrecio.Leave += new System.EventHandler(this.txtDescripListaPrecio_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(669, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Precio Costo:";
            // 
            // gbListaPrecio
            // 
            this.gbListaPrecio.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gbListaPrecio.Controls.Add(this.txtFlete);
            this.gbListaPrecio.Controls.Add(this.label9);
            this.gbListaPrecio.Controls.Add(this.lvwListaPrecioVenta);
            this.gbListaPrecio.Controls.Add(this.btnSalir);
            this.gbListaPrecio.Controls.Add(this.txtPorcentaje);
            this.gbListaPrecio.Controls.Add(this.label6);
            this.gbListaPrecio.Controls.Add(this.txtNombreLista);
            this.gbListaPrecio.Controls.Add(this.label2);
            this.gbListaPrecio.Controls.Add(this.txtCodLista);
            this.gbListaPrecio.Controls.Add(this.label5);
            this.gbListaPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbListaPrecio.Location = new System.Drawing.Point(12, 67);
            this.gbListaPrecio.Name = "gbListaPrecio";
            this.gbListaPrecio.Size = new System.Drawing.Size(829, 409);
            this.gbListaPrecio.TabIndex = 48;
            this.gbListaPrecio.TabStop = false;
            this.gbListaPrecio.Text = "Artículos en Lista de precio a la Venta";
            // 
            // txtFlete
            // 
            this.txtFlete.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtFlete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFlete.Location = new System.Drawing.Point(621, 23);
            this.txtFlete.Name = "txtFlete";
            this.txtFlete.ReadOnly = true;
            this.txtFlete.Size = new System.Drawing.Size(67, 20);
            this.txtFlete.TabIndex = 59;
            this.txtFlete.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(571, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 60;
            this.label9.Text = "% Flete:";
            this.label9.Visible = false;
            // 
            // lvwListaPrecioVenta
            // 
            this.lvwListaPrecioVenta.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwListaPrecioVenta.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwListaPrecioVenta.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.Descripcion,
            this.PrecioCostoLista,
            this.PrecioVenta,
            this.PrecioConIva,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader1,
            this.columnHeader5,
            this.columnHeader2,
            this.columnHeader6,
            this.UltimaActualización});
            this.lvwListaPrecioVenta.FullRowSelect = true;
            this.lvwListaPrecioVenta.GridLines = true;
            this.lvwListaPrecioVenta.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwListaPrecioVenta.HideSelection = false;
            this.lvwListaPrecioVenta.LargeImageList = this.imageList1;
            this.lvwListaPrecioVenta.Location = new System.Drawing.Point(6, 49);
            this.lvwListaPrecioVenta.MultiSelect = false;
            this.lvwListaPrecioVenta.Name = "lvwListaPrecioVenta";
            this.lvwListaPrecioVenta.Size = new System.Drawing.Size(817, 354);
            this.lvwListaPrecioVenta.SmallImageList = this.imageList1;
            this.lvwListaPrecioVenta.TabIndex = 58;
            this.lvwListaPrecioVenta.UseCompatibleStateImageBehavior = false;
            this.lvwListaPrecioVenta.View = System.Windows.Forms.View.Details;
            this.lvwListaPrecioVenta.SelectedIndexChanged += new System.EventHandler(this.lvwListaPrecioVenta_SelectedIndexChanged);
            // 
            // Id
            // 
            this.Id.Text = "Código";
            this.Id.Width = 110;
            // 
            // Descripcion
            // 
            this.Descripcion.Text = "Descripcion";
            this.Descripcion.Width = 320;
            // 
            // PrecioCostoLista
            // 
            this.PrecioCostoLista.Text = "Precio Costo Lista";
            this.PrecioCostoLista.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PrecioCostoLista.Width = 105;
            // 
            // PrecioVenta
            // 
            this.PrecioVenta.Text = "Precio Venta s/iva";
            this.PrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PrecioVenta.Width = 110;
            // 
            // PrecioConIva
            // 
            this.PrecioConIva.Text = "Precio Venta c/Iva";
            this.PrecioConIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PrecioConIva.Width = 110;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 0;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 0;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 0;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 0;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Width = 0;
            // 
            // UltimaActualización
            // 
            this.UltimaActualización.Text = "Actualizado";
            this.UltimaActualización.Width = 93;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Action ok.ico");
            this.imageList1.Images.SetKeyName(1, "warning.ico");
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(762, 393);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(10, 10);
            this.btnSalir.TabIndex = 49;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentaje.Location = new System.Drawing.Point(498, 23);
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.ReadOnly = true;
            this.txtPorcentaje.Size = new System.Drawing.Size(67, 20);
            this.txtPorcentaje.TabIndex = 5;
            this.txtPorcentaje.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(443, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 56;
            this.label6.Text = "% Venta:";
            this.label6.Visible = false;
            // 
            // txtNombreLista
            // 
            this.txtNombreLista.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtNombreLista.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreLista.Location = new System.Drawing.Point(320, 23);
            this.txtNombreLista.Name = "txtNombreLista";
            this.txtNombreLista.ReadOnly = true;
            this.txtNombreLista.Size = new System.Drawing.Size(117, 20);
            this.txtNombreLista.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(242, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "Nombre Lista:";
            // 
            // txtCodLista
            // 
            this.txtCodLista.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtCodLista.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodLista.Location = new System.Drawing.Point(170, 23);
            this.txtCodLista.Name = "txtCodLista";
            this.txtCodLista.ReadOnly = true;
            this.txtCodLista.Size = new System.Drawing.Size(66, 20);
            this.txtCodLista.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(110, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 51;
            this.label5.Text = "Cód Lista:";
            // 
            // frmArtPrecioVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(853, 587);
            this.Controls.Add(this.gbListaPrecio);
            this.Controls.Add(this.gpoCliente);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmArtPrecioVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Precio de Venta de Artículos";
            this.Load += new System.EventHandler(this.frmArtPrecioVenta_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gpoCliente.ResumeLayout(false);
            this.gpoCliente.PerformLayout();
            this.gbListaPrecio.ResumeLayout(false);
            this.gbListaPrecio.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnNuevo;
        private System.Windows.Forms.ToolStripButton tsBtnModificar;
        private System.Windows.Forms.ToolStripButton tsBtnBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TextBox txtConsulta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboBuscar;
        private System.Windows.Forms.GroupBox gpoCliente;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btcCerrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtDescripListaPrecio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbListaPrecio;
        private System.Windows.Forms.TextBox txtPorcentaje;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNombreLista;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodLista;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton tsBtnVincular;
        private System.Windows.Forms.ToolStripButton tsBtnListaPrecio;
        private System.Windows.Forms.ToolStripButton tsBtnRecargarListas;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboBuscarArt;
        private System.Windows.Forms.TextBox txtDescripcionArticulo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView lvwListaPrecioVenta;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader Descripcion;
        private System.Windows.Forms.ColumnHeader PrecioVenta;
        private System.Windows.Forms.ColumnHeader PrecioCostoLista;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.TextBox txtFlete;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader UltimaActualización;
        private System.Windows.Forms.ColumnHeader PrecioConIva;
        private System.Windows.Forms.ToolStripButton tsBtnVincularSinFechaActualizacion;
        private System.Windows.Forms.ToolStripButton tsBtnBuscaLista;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}