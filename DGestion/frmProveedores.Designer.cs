namespace DGestion
{
    partial class frmProveedores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProveedores));
            this.gpoCliente = new System.Windows.Forms.GroupBox();
            this.cboRubro = new System.Windows.Forms.ComboBox();
            this.txtCodRubro = new System.Windows.Forms.TextBox();
            this.btnRubro = new System.Windows.Forms.Button();
            this.btnTipoIva = new System.Windows.Forms.Button();
            this.txtCodTipoIva = new System.Windows.Forms.TextBox();
            this.txtCodProvee = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btcCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtPersonaContact = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNroCuit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboTipoIva = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTelefonos = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDirComercial = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsBtnModificar = new System.Windows.Forms.ToolStripButton();
            this.tsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.txtConsultaProvee = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBuscar = new System.Windows.Forms.ComboBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RazonSocial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Domicilio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CUIT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwProveedores = new System.Windows.Forms.ListView();
            this.gpoCliente.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpoCliente
            // 
            this.gpoCliente.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpoCliente.Controls.Add(this.cboRubro);
            this.gpoCliente.Controls.Add(this.txtCodRubro);
            this.gpoCliente.Controls.Add(this.btnRubro);
            this.gpoCliente.Controls.Add(this.btnTipoIva);
            this.gpoCliente.Controls.Add(this.txtCodTipoIva);
            this.gpoCliente.Controls.Add(this.txtCodProvee);
            this.gpoCliente.Controls.Add(this.label4);
            this.gpoCliente.Controls.Add(this.btnEliminar);
            this.gpoCliente.Controls.Add(this.btnModificar);
            this.gpoCliente.Controls.Add(this.btcCerrar);
            this.gpoCliente.Controls.Add(this.btnGuardar);
            this.gpoCliente.Controls.Add(this.txtPersonaContact);
            this.gpoCliente.Controls.Add(this.label14);
            this.gpoCliente.Controls.Add(this.txtObservaciones);
            this.gpoCliente.Controls.Add(this.label11);
            this.gpoCliente.Controls.Add(this.label9);
            this.gpoCliente.Controls.Add(this.txtNroCuit);
            this.gpoCliente.Controls.Add(this.label8);
            this.gpoCliente.Controls.Add(this.cboTipoIva);
            this.gpoCliente.Controls.Add(this.label7);
            this.gpoCliente.Controls.Add(this.txtTelefonos);
            this.gpoCliente.Controls.Add(this.label5);
            this.gpoCliente.Controls.Add(this.txtDirComercial);
            this.gpoCliente.Controls.Add(this.label3);
            this.gpoCliente.Controls.Add(this.txtRazonSocial);
            this.gpoCliente.Controls.Add(this.label2);
            this.gpoCliente.Location = new System.Drawing.Point(12, 299);
            this.gpoCliente.Name = "gpoCliente";
            this.gpoCliente.Size = new System.Drawing.Size(791, 188);
            this.gpoCliente.TabIndex = 27;
            this.gpoCliente.TabStop = false;
            // 
            // cboRubro
            // 
            this.cboRubro.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboRubro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboRubro.FormattingEnabled = true;
            this.cboRubro.Location = new System.Drawing.Point(554, 67);
            this.cboRubro.Name = "cboRubro";
            this.cboRubro.Size = new System.Drawing.Size(184, 21);
            this.cboRubro.TabIndex = 37;
            // 
            // txtCodRubro
            // 
            this.txtCodRubro.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodRubro.Location = new System.Drawing.Point(504, 68);
            this.txtCodRubro.Name = "txtCodRubro";
            this.txtCodRubro.Size = new System.Drawing.Size(44, 20);
            this.txtCodRubro.TabIndex = 12;
            this.txtCodRubro.TextChanged += new System.EventHandler(this.txtCodRubro_TextChanged);
            // 
            // btnRubro
            // 
            this.btnRubro.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRubro.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRubro.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRubro.Image = ((System.Drawing.Image)(resources.GetObject("btnRubro.Image")));
            this.btnRubro.Location = new System.Drawing.Point(744, 65);
            this.btnRubro.Name = "btnRubro";
            this.btnRubro.Size = new System.Drawing.Size(30, 25);
            this.btnRubro.TabIndex = 14;
            this.btnRubro.UseVisualStyleBackColor = false;
            this.btnRubro.Click += new System.EventHandler(this.btnRubro_Click);
            // 
            // btnTipoIva
            // 
            this.btnTipoIva.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTipoIva.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnTipoIva.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTipoIva.Image = ((System.Drawing.Image)(resources.GetObject("btnTipoIva.Image")));
            this.btnTipoIva.Location = new System.Drawing.Point(355, 117);
            this.btnTipoIva.Name = "btnTipoIva";
            this.btnTipoIva.Size = new System.Drawing.Size(30, 25);
            this.btnTipoIva.TabIndex = 9;
            this.btnTipoIva.UseVisualStyleBackColor = false;
            this.btnTipoIva.Click += new System.EventHandler(this.btnTipoIva_Click);
            // 
            // txtCodTipoIva
            // 
            this.txtCodTipoIva.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodTipoIva.Location = new System.Drawing.Point(127, 120);
            this.txtCodTipoIva.Name = "txtCodTipoIva";
            this.txtCodTipoIva.Size = new System.Drawing.Size(44, 20);
            this.txtCodTipoIva.TabIndex = 7;
            this.txtCodTipoIva.TextChanged += new System.EventHandler(this.txtCodTipoIva_TextChanged);
            // 
            // txtCodProvee
            // 
            this.txtCodProvee.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtCodProvee.Location = new System.Drawing.Point(127, 16);
            this.txtCodProvee.Name = "txtCodProvee";
            this.txtCodProvee.ReadOnly = true;
            this.txtCodProvee.Size = new System.Drawing.Size(70, 20);
            this.txtCodProvee.TabIndex = 3;
            this.txtCodProvee.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(78, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Código:";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(598, 157);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(85, 25);
            this.btnEliminar.TabIndex = 18;
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
            this.btnModificar.Location = new System.Drawing.Point(507, 157);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(85, 25);
            this.btnModificar.TabIndex = 17;
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
            this.btcCerrar.Location = new System.Drawing.Point(689, 157);
            this.btcCerrar.Name = "btcCerrar";
            this.btcCerrar.Size = new System.Drawing.Size(85, 25);
            this.btcCerrar.TabIndex = 19;
            this.btcCerrar.Text = "   Cerrar";
            this.btcCerrar.UseVisualStyleBackColor = false;
            this.btcCerrar.Click += new System.EventHandler(this.btcCerrar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(416, 157);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 25);
            this.btnGuardar.TabIndex = 16;
            this.btnGuardar.Text = "   Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtPersonaContact
            // 
            this.txtPersonaContact.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtPersonaContact.Location = new System.Drawing.Point(504, 42);
            this.txtPersonaContact.Name = "txtPersonaContact";
            this.txtPersonaContact.Size = new System.Drawing.Size(270, 20);
            this.txtPersonaContact.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(445, 45);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Contacto:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtObservaciones.Location = new System.Drawing.Point(504, 94);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(270, 20);
            this.txtObservaciones.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(417, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Observaciones:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(423, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Código Rubro:";
            // 
            // txtNroCuit
            // 
            this.txtNroCuit.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNroCuit.Location = new System.Drawing.Point(504, 16);
            this.txtNroCuit.Name = "txtNroCuit";
            this.txtNroCuit.Size = new System.Drawing.Size(270, 20);
            this.txtNroCuit.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(413, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Nro. de C.U.I.T.:";
            // 
            // cboTipoIva
            // 
            this.cboTipoIva.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboTipoIva.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboTipoIva.FormattingEnabled = true;
            this.cboTipoIva.Location = new System.Drawing.Point(177, 119);
            this.cboTipoIva.Name = "cboTipoIva";
            this.cboTipoIva.Size = new System.Drawing.Size(172, 21);
            this.cboTipoIva.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Código Tipo Iva:";
            // 
            // txtTelefonos
            // 
            this.txtTelefonos.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTelefonos.Location = new System.Drawing.Point(127, 94);
            this.txtTelefonos.Name = "txtTelefonos";
            this.txtTelefonos.Size = new System.Drawing.Size(258, 20);
            this.txtTelefonos.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Teléfonos:";
            // 
            // txtDirComercial
            // 
            this.txtDirComercial.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtDirComercial.Location = new System.Drawing.Point(127, 68);
            this.txtDirComercial.Name = "txtDirComercial";
            this.txtDirComercial.Size = new System.Drawing.Size(258, 20);
            this.txtDirComercial.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Domicilio Comercial:";
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtRazonSocial.Location = new System.Drawing.Point(127, 42);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(258, 20);
            this.txtRazonSocial.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Razon Social:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.txtConsultaProvee);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboBuscar);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(791, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
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
            this.toolStripSeparator1,
            this.tsBtnSalir,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(660, 15);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(127, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnNuevo
            // 
            this.tsBtnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnNuevo.Image")));
            this.tsBtnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnNuevo.Name = "tsBtnNuevo";
            this.tsBtnNuevo.Size = new System.Drawing.Size(28, 28);
            this.tsBtnNuevo.Text = "Nuevo Artículo";
            this.tsBtnNuevo.ToolTipText = "Nuevo Proveedor";
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
            this.tsBtnSalir.Text = "Salir";
            this.tsBtnSalir.Click += new System.EventHandler(this.tsBtnSalir_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // txtConsultaProvee
            // 
            this.txtConsultaProvee.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtConsultaProvee.Location = new System.Drawing.Point(202, 16);
            this.txtConsultaProvee.Name = "txtConsultaProvee";
            this.txtConsultaProvee.Size = new System.Drawing.Size(150, 20);
            this.txtConsultaProvee.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Buscar por:";
            // 
            // cboBuscar
            // 
            this.cboBuscar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboBuscar.FormattingEnabled = true;
            this.cboBuscar.Items.AddRange(new object[] {
            "Código",
            "Razón Social"});
            this.cboBuscar.Location = new System.Drawing.Point(76, 16);
            this.cboBuscar.Name = "cboBuscar";
            this.cboBuscar.Size = new System.Drawing.Size(120, 21);
            this.cboBuscar.TabIndex = 1;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(12, 283);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(10, 10);
            this.btnSalir.TabIndex = 48;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "App login manager.ico");
            // 
            // Id
            // 
            this.Id.Text = "Id";
            this.Id.Width = 25;
            // 
            // RazonSocial
            // 
            this.RazonSocial.Text = "Razón Social";
            this.RazonSocial.Width = 220;
            // 
            // Domicilio
            // 
            this.Domicilio.Text = "Domicilio";
            this.Domicilio.Width = 280;
            // 
            // CUIT
            // 
            this.CUIT.Text = "CUIT";
            this.CUIT.Width = 120;
            // 
            // lvwProveedores
            // 
            this.lvwProveedores.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwProveedores.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwProveedores.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.RazonSocial,
            this.Domicilio,
            this.CUIT});
            this.lvwProveedores.FullRowSelect = true;
            this.lvwProveedores.GridLines = true;
            this.lvwProveedores.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwProveedores.HideSelection = false;
            this.lvwProveedores.LargeImageList = this.imageList1;
            this.lvwProveedores.Location = new System.Drawing.Point(12, 58);
            this.lvwProveedores.MultiSelect = false;
            this.lvwProveedores.Name = "lvwProveedores";
            this.lvwProveedores.Size = new System.Drawing.Size(791, 235);
            this.lvwProveedores.SmallImageList = this.imageList1;
            this.lvwProveedores.TabIndex = 0;
            this.lvwProveedores.UseCompatibleStateImageBehavior = false;
            this.lvwProveedores.View = System.Windows.Forms.View.Details;
            this.lvwProveedores.SelectedIndexChanged += new System.EventHandler(this.lvwProveedores_SelectedIndexChanged);
            this.lvwProveedores.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvwProveedores_KeyPress);
            this.lvwProveedores.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwProveedores_MouseDoubleClick);
            // 
            // frmProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(812, 499);
            this.Controls.Add(this.lvwProveedores);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpoCliente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmProveedores";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Proveedores";
            this.Load += new System.EventHandler(this.frmProveedores_Load);
            this.gpoCliente.ResumeLayout(false);
            this.gpoCliente.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpoCliente;
        private System.Windows.Forms.TextBox txtPersonaContact;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNroCuit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboTipoIva;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTelefonos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDirComercial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnNuevo;
        private System.Windows.Forms.ToolStripButton tsBtnModificar;
        private System.Windows.Forms.ToolStripButton tsBtnBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TextBox txtConsultaProvee;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBuscar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btcCerrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtCodProvee;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCodTipoIva;
        private System.Windows.Forms.Button btnTipoIva;
        private System.Windows.Forms.TextBox txtCodRubro;
        private System.Windows.Forms.Button btnRubro;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox cboRubro;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader RazonSocial;
        private System.Windows.Forms.ColumnHeader Domicilio;
        private System.Windows.Forms.ColumnHeader CUIT;
        private System.Windows.Forms.ListView lvwProveedores;
    }
}