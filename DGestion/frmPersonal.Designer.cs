namespace DGestion
{
    partial class frmPersonal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPersonal));
            this.gpoCliente = new System.Windows.Forms.GroupBox();
            this.cboPersonal = new System.Windows.Forms.ComboBox();
            this.cboEstadoCivil = new System.Windows.Forms.ComboBox();
            this.dtpFechaVencimientoCartilla = new System.Windows.Forms.DateTimePicker();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.Legajo = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLegajo = new System.Windows.Forms.TextBox();
            this.dtpFechaNacConyuge = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.btnTPerso = new System.Windows.Forms.Button();
            this.txtCodTipoPersonal = new System.Windows.Forms.TextBox();
            this.txtCodEstadoCivil = new System.Windows.Forms.TextBox();
            this.btnEstadoCivil = new System.Windows.Forms.Button();
            this.btnBajaPersonal = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btcCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.txtNombreConyuge = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNumCartillaSanitaria = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTelCelular = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTelFijo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDomicilio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombreApellido = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboBuscaPersonal = new System.Windows.Forms.ComboBox();
            this.lvwPersonal = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NLegajo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NDNI = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NombreApellido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Domicilio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Telefonos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FechaNacimiento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Estado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnSalir = new System.Windows.Forms.Button();
            this.gpoCliente.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tlsBarArticulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpoCliente
            // 
            this.gpoCliente.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpoCliente.Controls.Add(this.cboPersonal);
            this.gpoCliente.Controls.Add(this.cboEstadoCivil);
            this.gpoCliente.Controls.Add(this.dtpFechaVencimientoCartilla);
            this.gpoCliente.Controls.Add(this.txtDNI);
            this.gpoCliente.Controls.Add(this.Legajo);
            this.gpoCliente.Controls.Add(this.label12);
            this.gpoCliente.Controls.Add(this.txtLegajo);
            this.gpoCliente.Controls.Add(this.dtpFechaNacConyuge);
            this.gpoCliente.Controls.Add(this.dtpFechaNacimiento);
            this.gpoCliente.Controls.Add(this.btnTPerso);
            this.gpoCliente.Controls.Add(this.txtCodTipoPersonal);
            this.gpoCliente.Controls.Add(this.txtCodEstadoCivil);
            this.gpoCliente.Controls.Add(this.btnEstadoCivil);
            this.gpoCliente.Controls.Add(this.btnBajaPersonal);
            this.gpoCliente.Controls.Add(this.btnModificar);
            this.gpoCliente.Controls.Add(this.btcCerrar);
            this.gpoCliente.Controls.Add(this.btnGuardar);
            this.gpoCliente.Controls.Add(this.txtObservaciones);
            this.gpoCliente.Controls.Add(this.txtNombreConyuge);
            this.gpoCliente.Controls.Add(this.label10);
            this.gpoCliente.Controls.Add(this.label9);
            this.gpoCliente.Controls.Add(this.label16);
            this.gpoCliente.Controls.Add(this.label13);
            this.gpoCliente.Controls.Add(this.label11);
            this.gpoCliente.Controls.Add(this.txtNumCartillaSanitaria);
            this.gpoCliente.Controls.Add(this.label8);
            this.gpoCliente.Controls.Add(this.label7);
            this.gpoCliente.Controls.Add(this.label6);
            this.gpoCliente.Controls.Add(this.txtTelCelular);
            this.gpoCliente.Controls.Add(this.label5);
            this.gpoCliente.Controls.Add(this.txtTelFijo);
            this.gpoCliente.Controls.Add(this.label4);
            this.gpoCliente.Controls.Add(this.txtDomicilio);
            this.gpoCliente.Controls.Add(this.label3);
            this.gpoCliente.Controls.Add(this.txtNombreApellido);
            this.gpoCliente.Controls.Add(this.label2);
            this.gpoCliente.Location = new System.Drawing.Point(12, 325);
            this.gpoCliente.Name = "gpoCliente";
            this.gpoCliente.Size = new System.Drawing.Size(859, 241);
            this.gpoCliente.TabIndex = 27;
            this.gpoCliente.TabStop = false;
            // 
            // cboPersonal
            // 
            this.cboPersonal.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboPersonal.FormattingEnabled = true;
            this.cboPersonal.Location = new System.Drawing.Point(667, 149);
            this.cboPersonal.Name = "cboPersonal";
            this.cboPersonal.Size = new System.Drawing.Size(121, 21);
            this.cboPersonal.TabIndex = 16;
            this.cboPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboPersonal_KeyPress);
            // 
            // cboEstadoCivil
            // 
            this.cboEstadoCivil.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboEstadoCivil.FormattingEnabled = true;
            this.cboEstadoCivil.Location = new System.Drawing.Point(667, 17);
            this.cboEstadoCivil.Name = "cboEstadoCivil";
            this.cboEstadoCivil.Size = new System.Drawing.Size(121, 21);
            this.cboEstadoCivil.TabIndex = 9;
            this.cboEstadoCivil.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboEstadoCivil_KeyPress);
            // 
            // dtpFechaVencimientoCartilla
            // 
            this.dtpFechaVencimientoCartilla.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaVencimientoCartilla.Location = new System.Drawing.Point(611, 72);
            this.dtpFechaVencimientoCartilla.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpFechaVencimientoCartilla.Name = "dtpFechaVencimientoCartilla";
            this.dtpFechaVencimientoCartilla.Size = new System.Drawing.Size(147, 20);
            this.dtpFechaVencimientoCartilla.TabIndex = 12;
            this.dtpFechaVencimientoCartilla.Value = new System.DateTime(1995, 2, 1, 0, 0, 0, 0);
            this.dtpFechaVencimientoCartilla.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaVencimientoCartilla_KeyPress);
            // 
            // txtDNI
            // 
            this.txtDNI.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtDNI.Location = new System.Drawing.Point(141, 45);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(86, 20);
            this.txtDNI.TabIndex = 2;
            this.txtDNI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDNI_KeyPress);
            // 
            // Legajo
            // 
            this.Legajo.AutoSize = true;
            this.Legajo.Location = new System.Drawing.Point(106, 48);
            this.Legajo.Name = "Legajo";
            this.Legajo.Size = new System.Drawing.Size(29, 13);
            this.Legajo.TabIndex = 63;
            this.Legajo.Text = "DNI:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(93, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 13);
            this.label12.TabIndex = 57;
            this.label12.Text = "Legajo:";
            // 
            // txtLegajo
            // 
            this.txtLegajo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtLegajo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLegajo.Location = new System.Drawing.Point(141, 19);
            this.txtLegajo.Name = "txtLegajo";
            this.txtLegajo.Size = new System.Drawing.Size(86, 20);
            this.txtLegajo.TabIndex = 3;
            this.txtLegajo.TabStop = false;
            // 
            // dtpFechaNacConyuge
            // 
            this.dtpFechaNacConyuge.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaNacConyuge.Location = new System.Drawing.Point(611, 124);
            this.dtpFechaNacConyuge.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpFechaNacConyuge.Name = "dtpFechaNacConyuge";
            this.dtpFechaNacConyuge.Size = new System.Drawing.Size(147, 20);
            this.dtpFechaNacConyuge.TabIndex = 14;
            this.dtpFechaNacConyuge.Value = new System.DateTime(1995, 2, 1, 0, 0, 0, 0);
            this.dtpFechaNacConyuge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaNacConyuge_KeyPress);
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(141, 175);
            this.dtpFechaNacimiento.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(117, 20);
            this.dtpFechaNacimiento.TabIndex = 7;
            this.dtpFechaNacimiento.Value = new System.DateTime(1995, 2, 1, 0, 0, 0, 0);
            this.dtpFechaNacimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFechaNacimiento_KeyPress);
            // 
            // btnTPerso
            // 
            this.btnTPerso.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTPerso.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTPerso.Image = ((System.Drawing.Image)(resources.GetObject("btnTPerso.Image")));
            this.btnTPerso.Location = new System.Drawing.Point(794, 147);
            this.btnTPerso.Name = "btnTPerso";
            this.btnTPerso.Size = new System.Drawing.Size(30, 25);
            this.btnTPerso.TabIndex = 17;
            this.btnTPerso.UseVisualStyleBackColor = false;
            this.btnTPerso.Click += new System.EventHandler(this.btnTPerso_Click);
            this.btnTPerso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnTPerso_KeyPress);
            // 
            // txtCodTipoPersonal
            // 
            this.txtCodTipoPersonal.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodTipoPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodTipoPersonal.Location = new System.Drawing.Point(611, 149);
            this.txtCodTipoPersonal.Name = "txtCodTipoPersonal";
            this.txtCodTipoPersonal.Size = new System.Drawing.Size(50, 20);
            this.txtCodTipoPersonal.TabIndex = 15;
            this.txtCodTipoPersonal.TextChanged += new System.EventHandler(this.txtCodTipoPersonal_TextChanged);
            this.txtCodTipoPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodTipoPersonal_KeyPress);
            // 
            // txtCodEstadoCivil
            // 
            this.txtCodEstadoCivil.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodEstadoCivil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodEstadoCivil.Location = new System.Drawing.Point(611, 17);
            this.txtCodEstadoCivil.Name = "txtCodEstadoCivil";
            this.txtCodEstadoCivil.Size = new System.Drawing.Size(50, 20);
            this.txtCodEstadoCivil.TabIndex = 8;
            this.txtCodEstadoCivil.TextChanged += new System.EventHandler(this.txtCodEstadoCivil_TextChanged);
            this.txtCodEstadoCivil.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodEstadoCivil_KeyPress);
            // 
            // btnEstadoCivil
            // 
            this.btnEstadoCivil.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEstadoCivil.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEstadoCivil.Image = ((System.Drawing.Image)(resources.GetObject("btnEstadoCivil.Image")));
            this.btnEstadoCivil.Location = new System.Drawing.Point(794, 15);
            this.btnEstadoCivil.Name = "btnEstadoCivil";
            this.btnEstadoCivil.Size = new System.Drawing.Size(30, 25);
            this.btnEstadoCivil.TabIndex = 10;
            this.btnEstadoCivil.UseVisualStyleBackColor = false;
            this.btnEstadoCivil.Click += new System.EventHandler(this.btnEstadoCivil_Click);
            this.btnEstadoCivil.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnEstadoCivil_KeyPress);
            // 
            // btnBajaPersonal
            // 
            this.btnBajaPersonal.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBajaPersonal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBajaPersonal.Image = ((System.Drawing.Image)(resources.GetObject("btnBajaPersonal.Image")));
            this.btnBajaPersonal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBajaPersonal.Location = new System.Drawing.Point(648, 210);
            this.btnBajaPersonal.Name = "btnBajaPersonal";
            this.btnBajaPersonal.Size = new System.Drawing.Size(85, 25);
            this.btnBajaPersonal.TabIndex = 21;
            this.btnBajaPersonal.Text = "   Dar Baja";
            this.btnBajaPersonal.UseVisualStyleBackColor = false;
            this.btnBajaPersonal.Click += new System.EventHandler(this.btnBajaPersonal_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.Location = new System.Drawing.Point(557, 210);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(85, 25);
            this.btnModificar.TabIndex = 20;
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
            this.btcCerrar.Location = new System.Drawing.Point(739, 210);
            this.btcCerrar.Name = "btcCerrar";
            this.btcCerrar.Size = new System.Drawing.Size(85, 25);
            this.btcCerrar.TabIndex = 22;
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
            this.btnGuardar.Location = new System.Drawing.Point(466, 210);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 25);
            this.btnGuardar.TabIndex = 19;
            this.btnGuardar.Text = "   Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.btnGuardar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnGuardar_KeyPress);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtObservaciones.Location = new System.Drawing.Point(611, 175);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(213, 20);
            this.txtObservaciones.TabIndex = 18;
            this.txtObservaciones.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtObservaciones_KeyPress);
            // 
            // txtNombreConyuge
            // 
            this.txtNombreConyuge.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNombreConyuge.Location = new System.Drawing.Point(611, 98);
            this.txtNombreConyuge.Name = "txtNombreConyuge";
            this.txtNombreConyuge.Size = new System.Drawing.Size(213, 20);
            this.txtNombreConyuge.TabIndex = 13;
            this.txtNombreConyuge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreConyuge_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(496, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 13);
            this.label10.TabIndex = 38;
            this.label10.Text = "Nombre del Cónyuge:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(537, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Vencimiento:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(524, 178);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(81, 13);
            this.label16.TabIndex = 34;
            this.label16.Text = "Observaciones:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(505, 152);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "Cod. Tipo Personal:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(494, 127);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Fecha Nac. Cónyuge:";
            // 
            // txtNumCartillaSanitaria
            // 
            this.txtNumCartillaSanitaria.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNumCartillaSanitaria.Location = new System.Drawing.Point(611, 46);
            this.txtNumCartillaSanitaria.Name = "txtNumCartillaSanitaria";
            this.txtNumCartillaSanitaria.Size = new System.Drawing.Size(213, 20);
            this.txtNumCartillaSanitaria.TabIndex = 11;
            this.txtNumCartillaSanitaria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumCartillaSanitaria_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(505, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "N° Cartilla Sanitaria:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(515, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Cod. Estado Civil:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Fecha Nacimiento:";
            // 
            // txtTelCelular
            // 
            this.txtTelCelular.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTelCelular.Location = new System.Drawing.Point(141, 149);
            this.txtTelCelular.Name = "txtTelCelular";
            this.txtTelCelular.Size = new System.Drawing.Size(117, 20);
            this.txtTelCelular.TabIndex = 6;
            this.txtTelCelular.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelCelular_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Teléfono Celular";
            // 
            // txtTelFijo
            // 
            this.txtTelFijo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTelFijo.Location = new System.Drawing.Point(141, 123);
            this.txtTelFijo.Name = "txtTelFijo";
            this.txtTelFijo.Size = new System.Drawing.Size(117, 20);
            this.txtTelFijo.TabIndex = 5;
            this.txtTelFijo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelFijo_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Teléfono fijo:";
            // 
            // txtDomicilio
            // 
            this.txtDomicilio.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtDomicilio.Location = new System.Drawing.Point(141, 97);
            this.txtDomicilio.Name = "txtDomicilio";
            this.txtDomicilio.Size = new System.Drawing.Size(213, 20);
            this.txtDomicilio.TabIndex = 4;
            this.txtDomicilio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDomicilio_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(83, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Domicilio:";
            // 
            // txtNombreApellido
            // 
            this.txtNombreApellido.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNombreApellido.Location = new System.Drawing.Point(141, 71);
            this.txtNombreApellido.Name = "txtNombreApellido";
            this.txtNombreApellido.Size = new System.Drawing.Size(213, 20);
            this.txtNombreApellido.TabIndex = 3;
            this.txtNombreApellido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreApellido_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Nombre y Apellido:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.tlsBarArticulo);
            this.groupBox1.Controls.Add(this.txtBuscarArticulo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboBuscaPersonal);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(859, 50);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
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
            this.tlsBarArticulo.Location = new System.Drawing.Point(695, 12);
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
            this.tsBtnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnNuevo.Image")));
            this.tsBtnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnNuevo.Name = "tsBtnNuevo";
            this.tsBtnNuevo.Size = new System.Drawing.Size(28, 28);
            this.tsBtnNuevo.Text = "Nueva Persona";
            this.tsBtnNuevo.Click += new System.EventHandler(this.tsBtnNuevo_Click);
            // 
            // tsBtnModificar
            // 
            this.tsBtnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnModificar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnModificar.Image")));
            this.tsBtnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnModificar.Name = "tsBtnModificar";
            this.tsBtnModificar.Size = new System.Drawing.Size(28, 28);
            this.tsBtnModificar.Text = "Modificar Datos";
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
            this.tsBtnBuscar.Text = "Buscar Datos";
            this.tsBtnBuscar.Click += new System.EventHandler(this.tsBtnBuscar_Click);
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
            this.tsBtnSalir.Text = "Salir de Gestión del Personal";
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
            this.txtBuscarArticulo.TabIndex = 1;
            this.txtBuscarArticulo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarArticulo_KeyPress);
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
            // cboBuscaPersonal
            // 
            this.cboBuscaPersonal.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboBuscaPersonal.FormattingEnabled = true;
            this.cboBuscaPersonal.Items.AddRange(new object[] {
            "Código",
            "Nombre"});
            this.cboBuscaPersonal.Location = new System.Drawing.Point(78, 19);
            this.cboBuscaPersonal.Name = "cboBuscaPersonal";
            this.cboBuscaPersonal.Size = new System.Drawing.Size(120, 21);
            this.cboBuscaPersonal.TabIndex = 0;
            this.cboBuscaPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboBuscaPersonal_KeyPress);
            // 
            // lvwPersonal
            // 
            this.lvwPersonal.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwPersonal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.NLegajo,
            this.NDNI,
            this.NombreApellido,
            this.Domicilio,
            this.Telefonos,
            this.FechaNacimiento,
            this.Estado});
            this.lvwPersonal.FullRowSelect = true;
            this.lvwPersonal.GridLines = true;
            this.lvwPersonal.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwPersonal.HideSelection = false;
            this.lvwPersonal.LargeImageList = this.imageList1;
            this.lvwPersonal.Location = new System.Drawing.Point(12, 68);
            this.lvwPersonal.MultiSelect = false;
            this.lvwPersonal.Name = "lvwPersonal";
            this.lvwPersonal.Size = new System.Drawing.Size(859, 251);
            this.lvwPersonal.SmallImageList = this.imageList1;
            this.lvwPersonal.TabIndex = 2;
            this.lvwPersonal.UseCompatibleStateImageBehavior = false;
            this.lvwPersonal.View = System.Windows.Forms.View.Details;
            this.lvwPersonal.SelectedIndexChanged += new System.EventHandler(this.lvwPersonal_SelectedIndexChanged);
            this.lvwPersonal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvwPersonal_KeyPress);
            this.lvwPersonal.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwPersonal_MouseDoubleClick);
            // 
            // Id
            // 
            this.Id.Text = "-";
            this.Id.Width = 25;
            // 
            // NLegajo
            // 
            this.NLegajo.Text = "Legajo";
            this.NLegajo.Width = 80;
            // 
            // NDNI
            // 
            this.NDNI.Text = "DNI";
            this.NDNI.Width = 80;
            // 
            // NombreApellido
            // 
            this.NombreApellido.Text = "Nombre y Apellido";
            this.NombreApellido.Width = 200;
            // 
            // Domicilio
            // 
            this.Domicilio.Text = "Domicilio";
            this.Domicilio.Width = 195;
            // 
            // Telefonos
            // 
            this.Telefonos.Text = "Teléfonos";
            this.Telefonos.Width = 84;
            // 
            // FechaNacimiento
            // 
            this.FechaNacimiento.Text = "Fecha Nac.";
            this.FechaNacimiento.Width = 80;
            // 
            // Estado
            // 
            this.Estado.Text = "Estado";
            this.Estado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Estado.Width = 80;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Kdm user male.ico");
            this.imageList1.Images.SetKeyName(1, "Action ok.ico");
            this.imageList1.Images.SetKeyName(2, "Action delete.ico");
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(801, 309);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(10, 10);
            this.btnSalir.TabIndex = 84;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmPersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(883, 578);
            this.Controls.Add(this.lvwPersonal);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpoCliente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmPersonal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión del Personal";
            this.Load += new System.EventHandler(this.frmPersonal_Load);
            this.gpoCliente.ResumeLayout(false);
            this.gpoCliente.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tlsBarArticulo.ResumeLayout(false);
            this.tlsBarArticulo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpoCliente;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.TextBox txtNombreConyuge;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNumCartillaSanitaria;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTelCelular;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTelFijo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDomicilio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombreApellido;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBajaPersonal;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btcCerrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEstadoCivil;
        private System.Windows.Forms.Button btnTPerso;
        private System.Windows.Forms.TextBox txtCodTipoPersonal;
        private System.Windows.Forms.TextBox txtCodEstadoCivil;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBuscaPersonal;
        private System.Windows.Forms.ListView lvwPersonal;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.DateTimePicker dtpFechaNacConyuge;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader NombreApellido;
        private System.Windows.Forms.ColumnHeader Domicilio;
        private System.Windows.Forms.ColumnHeader Telefonos;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader FechaNacimiento;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtLegajo;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label Legajo;
        private System.Windows.Forms.DateTimePicker dtpFechaVencimientoCartilla;
        private System.Windows.Forms.ComboBox cboPersonal;
        private System.Windows.Forms.ComboBox cboEstadoCivil;
        private System.Windows.Forms.ColumnHeader NLegajo;
        private System.Windows.Forms.ColumnHeader NDNI;
        private System.Windows.Forms.ColumnHeader Estado;
    }
}