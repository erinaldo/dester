namespace DGestion
{
    partial class frmEmpresa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmpresa));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.GpbEmpresa = new System.Windows.Forms.GroupBox();
            this.gbPtoVta = new System.Windows.Forms.GroupBox();
            this.txtDirFiscal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.txtPunto = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.cboPtoVta = new System.Windows.Forms.ComboBox();
            this.gbEmpresa = new System.Windows.Forms.GroupBox();
            this.tlsBarArticulo = new System.Windows.Forms.ToolStrip();
            this.tsBtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsBtnModificar = new System.Windows.Forms.ToolStripButton();
            this.tsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnReporte = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.txtBuscarArticulo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboFormaPago = new System.Windows.Forms.ComboBox();
            this.gpoCliente = new System.Windows.Forms.GroupBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCuit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtIdEmpresa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lvwEmpresaLogin = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RazonSocial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CUIT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DomicilioFiscal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GpbEmpresa.SuspendLayout();
            this.gbPtoVta.SuspendLayout();
            this.gbEmpresa.SuspendLayout();
            this.tlsBarArticulo.SuspendLayout();
            this.gpoCliente.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "accept.ico");
            this.imageList1.Images.SetKeyName(1, "Action button stop.ico");
            this.imageList1.Images.SetKeyName(2, "warning.ico");
            // 
            // GpbEmpresa
            // 
            this.GpbEmpresa.Controls.Add(this.gbPtoVta);
            this.GpbEmpresa.Controls.Add(this.gbEmpresa);
            this.GpbEmpresa.Controls.Add(this.gpoCliente);
            this.GpbEmpresa.Controls.Add(this.lvwEmpresaLogin);
            this.GpbEmpresa.Location = new System.Drawing.Point(12, 12);
            this.GpbEmpresa.Name = "GpbEmpresa";
            this.GpbEmpresa.Size = new System.Drawing.Size(602, 490);
            this.GpbEmpresa.TabIndex = 63;
            this.GpbEmpresa.TabStop = false;
            this.GpbEmpresa.Text = "Empresa";
            // 
            // gbPtoVta
            // 
            this.gbPtoVta.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gbPtoVta.Controls.Add(this.txtDirFiscal);
            this.gbPtoVta.Controls.Add(this.label4);
            this.gbPtoVta.Controls.Add(this.label8);
            this.gbPtoVta.Controls.Add(this.btnVolver);
            this.gbPtoVta.Controls.Add(this.txtPunto);
            this.gbPtoVta.Controls.Add(this.btnAceptar);
            this.gbPtoVta.Controls.Add(this.cboPtoVta);
            this.gbPtoVta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPtoVta.Location = new System.Drawing.Point(29, 160);
            this.gbPtoVta.Name = "gbPtoVta";
            this.gbPtoVta.Size = new System.Drawing.Size(541, 133);
            this.gbPtoVta.TabIndex = 66;
            this.gbPtoVta.TabStop = false;
            this.gbPtoVta.Visible = false;
            // 
            // txtDirFiscal
            // 
            this.txtDirFiscal.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtDirFiscal.Location = new System.Drawing.Point(244, 50);
            this.txtDirFiscal.Name = "txtDirFiscal";
            this.txtDirFiscal.Size = new System.Drawing.Size(261, 20);
            this.txtDirFiscal.TabIndex = 65;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(182, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 66;
            this.label4.Text = "Dir. Fiscal:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(139, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(166, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Seleccionar Punto de Venta";
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Image = ((System.Drawing.Image)(resources.GetObject("btnVolver.Image")));
            this.btnVolver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVolver.Location = new System.Drawing.Point(430, 104);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 6;
            this.btnVolver.Text = "    &Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // txtPunto
            // 
            this.txtPunto.AutoSize = true;
            this.txtPunto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPunto.Location = new System.Drawing.Point(37, 53);
            this.txtPunto.Name = "txtPunto";
            this.txtPunto.Size = new System.Drawing.Size(54, 13);
            this.txtPunto.TabIndex = 5;
            this.txtPunto.Text = "Pto.  Vta.:";
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(40, 104);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "    &Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // cboPtoVta
            // 
            this.cboPtoVta.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboPtoVta.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboPtoVta.FormattingEnabled = true;
            this.cboPtoVta.Location = new System.Drawing.Point(97, 50);
            this.cboPtoVta.Name = "cboPtoVta";
            this.cboPtoVta.Size = new System.Drawing.Size(79, 21);
            this.cboPtoVta.TabIndex = 3;
            this.cboPtoVta.SelectedIndexChanged += new System.EventHandler(this.cboPtoVta_SelectedIndexChanged);
            // 
            // gbEmpresa
            // 
            this.gbEmpresa.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gbEmpresa.Controls.Add(this.tlsBarArticulo);
            this.gbEmpresa.Controls.Add(this.txtBuscarArticulo);
            this.gbEmpresa.Controls.Add(this.label5);
            this.gbEmpresa.Controls.Add(this.cboFormaPago);
            this.gbEmpresa.Location = new System.Drawing.Point(6, 19);
            this.gbEmpresa.Name = "gbEmpresa";
            this.gbEmpresa.Size = new System.Drawing.Size(583, 50);
            this.gbEmpresa.TabIndex = 65;
            this.gbEmpresa.TabStop = false;
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
            this.toolStripSeparator2,
            this.tsBtnReporte,
            this.toolStripSeparator1,
            this.tsBtnSalir,
            this.toolStripSeparator3});
            this.tlsBarArticulo.Location = new System.Drawing.Point(420, 16);
            this.tlsBarArticulo.Name = "tlsBarArticulo";
            this.tlsBarArticulo.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tlsBarArticulo.Size = new System.Drawing.Size(161, 31);
            this.tlsBarArticulo.Stretch = true;
            this.tlsBarArticulo.TabIndex = 3;
            this.tlsBarArticulo.Text = "tlsBarArticulo";
            // 
            // tsBtnNuevo
            // 
            this.tsBtnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnNuevo.Enabled = false;
            this.tsBtnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnNuevo.Image")));
            this.tsBtnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnNuevo.Name = "tsBtnNuevo";
            this.tsBtnNuevo.Size = new System.Drawing.Size(28, 28);
            this.tsBtnNuevo.Text = "Nueva Forma de Pago";
            this.tsBtnNuevo.Click += new System.EventHandler(this.tsBtnNuevo_Click);
            // 
            // tsBtnModificar
            // 
            this.tsBtnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnModificar.Enabled = false;
            this.tsBtnModificar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnModificar.Image")));
            this.tsBtnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnModificar.Name = "tsBtnModificar";
            this.tsBtnModificar.Size = new System.Drawing.Size(28, 28);
            this.tsBtnModificar.Text = "Modificar Item";
            this.tsBtnModificar.Click += new System.EventHandler(this.tsBtnModificar_Click);
            // 
            // tsBtnBuscar
            // 
            this.tsBtnBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnBuscar.Enabled = false;
            this.tsBtnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnBuscar.Image")));
            this.tsBtnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnBuscar.Name = "tsBtnBuscar";
            this.tsBtnBuscar.Size = new System.Drawing.Size(28, 28);
            this.tsBtnBuscar.Text = "Buscar Item";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsBtnReporte
            // 
            this.tsBtnReporte.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnReporte.Enabled = false;
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
            this.tsBtnSalir.Text = "Salir de Forma de Pago";
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
            this.txtBuscarArticulo.Location = new System.Drawing.Point(217, 19);
            this.txtBuscarArticulo.Name = "txtBuscarArticulo";
            this.txtBuscarArticulo.Size = new System.Drawing.Size(150, 20);
            this.txtBuscarArticulo.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Buscar por:";
            // 
            // cboFormaPago
            // 
            this.cboFormaPago.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cboFormaPago.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboFormaPago.FormattingEnabled = true;
            this.cboFormaPago.Items.AddRange(new object[] {
            "ID Empresa",
            "Razón Social"});
            this.cboFormaPago.Location = new System.Drawing.Point(91, 19);
            this.cboFormaPago.Name = "cboFormaPago";
            this.cboFormaPago.Size = new System.Drawing.Size(120, 21);
            this.cboFormaPago.TabIndex = 0;
            // 
            // gpoCliente
            // 
            this.gpoCliente.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpoCliente.Controls.Add(this.txtEmail);
            this.gpoCliente.Controls.Add(this.label7);
            this.gpoCliente.Controls.Add(this.txtRazonSocial);
            this.gpoCliente.Controls.Add(this.label3);
            this.gpoCliente.Controls.Add(this.txtCuit);
            this.gpoCliente.Controls.Add(this.label1);
            this.gpoCliente.Controls.Add(this.btnActualizar);
            this.gpoCliente.Controls.Add(this.btnCerrar);
            this.gpoCliente.Controls.Add(this.btnGuardar);
            this.gpoCliente.Controls.Add(this.txtIdEmpresa);
            this.gpoCliente.Controls.Add(this.label2);
            this.gpoCliente.Location = new System.Drawing.Point(6, 359);
            this.gpoCliente.Name = "gpoCliente";
            this.gpoCliente.Size = new System.Drawing.Size(583, 119);
            this.gpoCliente.TabIndex = 64;
            this.gpoCliente.TabStop = false;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtEmail.Location = new System.Drawing.Point(301, 45);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(97, 20);
            this.txtEmail.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(256, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 64;
            this.label7.Text = "E-Mail:";
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtRazonSocial.Location = new System.Drawing.Point(301, 19);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(269, 20);
            this.txtRazonSocial.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(222, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "Razón Social:";
            // 
            // txtCuit
            // 
            this.txtCuit.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCuit.Location = new System.Drawing.Point(91, 45);
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.Size = new System.Drawing.Size(97, 20);
            this.txtCuit.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "CUIT:";
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActualizar.Location = new System.Drawing.Point(394, 80);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(85, 25);
            this.btnActualizar.TabIndex = 54;
            this.btnActualizar.Text = "   Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrar.Location = new System.Drawing.Point(485, 80);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(85, 25);
            this.btnCerrar.TabIndex = 53;
            this.btnCerrar.Text = "  Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(303, 80);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 25);
            this.btnGuardar.TabIndex = 51;
            this.btnGuardar.Text = "   Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtIdEmpresa
            // 
            this.txtIdEmpresa.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtIdEmpresa.Location = new System.Drawing.Point(91, 19);
            this.txtIdEmpresa.Name = "txtIdEmpresa";
            this.txtIdEmpresa.ReadOnly = true;
            this.txtIdEmpresa.Size = new System.Drawing.Size(97, 20);
            this.txtIdEmpresa.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "ID Empresa:";
            // 
            // lvwEmpresaLogin
            // 
            this.lvwEmpresaLogin.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwEmpresaLogin.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwEmpresaLogin.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.RazonSocial,
            this.CUIT,
            this.DomicilioFiscal});
            this.lvwEmpresaLogin.FullRowSelect = true;
            this.lvwEmpresaLogin.GridLines = true;
            this.lvwEmpresaLogin.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwEmpresaLogin.HideSelection = false;
            this.lvwEmpresaLogin.Location = new System.Drawing.Point(6, 75);
            this.lvwEmpresaLogin.MultiSelect = false;
            this.lvwEmpresaLogin.Name = "lvwEmpresaLogin";
            this.lvwEmpresaLogin.Size = new System.Drawing.Size(583, 278);
            this.lvwEmpresaLogin.TabIndex = 63;
            this.lvwEmpresaLogin.UseCompatibleStateImageBehavior = false;
            this.lvwEmpresaLogin.View = System.Windows.Forms.View.Details;
            this.lvwEmpresaLogin.SelectedIndexChanged += new System.EventHandler(this.lvwEmpresaLogin_SelectedIndexChanged);
            this.lvwEmpresaLogin.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwEmpresaLogin_MouseClick);
            // 
            // Id
            // 
            this.Id.Text = "-";
            this.Id.Width = 26;
            // 
            // RazonSocial
            // 
            this.RazonSocial.Text = "Razón Social";
            this.RazonSocial.Width = 200;
            // 
            // CUIT
            // 
            this.CUIT.Text = "CUIT";
            this.CUIT.Width = 110;
            // 
            // DomicilioFiscal
            // 
            this.DomicilioFiscal.Text = "Domicilio Fiscal";
            this.DomicilioFiscal.Width = 0;
            // 
            // frmEmpresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(624, 509);
            this.Controls.Add(this.GpbEmpresa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmpresa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elección de Empresa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEmpresa_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEmpresa_FormClosed);
            this.Load += new System.EventHandler(this.frmEmpresa_Load);
            this.GpbEmpresa.ResumeLayout(false);
            this.gbPtoVta.ResumeLayout(false);
            this.gbPtoVta.PerformLayout();
            this.gbEmpresa.ResumeLayout(false);
            this.gbEmpresa.PerformLayout();
            this.tlsBarArticulo.ResumeLayout(false);
            this.tlsBarArticulo.PerformLayout();
            this.gpoCliente.ResumeLayout(false);
            this.gpoCliente.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox GpbEmpresa;
        private System.Windows.Forms.GroupBox gbPtoVta;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label txtPunto;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.ComboBox cboPtoVta;
        private System.Windows.Forms.GroupBox gbEmpresa;
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboFormaPago;
        private System.Windows.Forms.GroupBox gpoCliente;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtIdEmpresa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvwEmpresaLogin;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader RazonSocial;
        private System.Windows.Forms.ColumnHeader CUIT;
        private System.Windows.Forms.ColumnHeader DomicilioFiscal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDirFiscal;
        private System.Windows.Forms.Label label4;
    }
}