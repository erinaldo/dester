namespace DGestion
{
    partial class frmVendedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVendedor));
            this.lvwVendedor = new System.Windows.Forms.ListView();
            this.IdVendedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NombreApellido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Comisión = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Grupo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tlsBarArticulo = new System.Windows.Forms.ToolStrip();
            this.tsBtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsBtnModificar = new System.Windows.Forms.ToolStripButton();
            this.tsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.txtBuscarArticulo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBuscaVendedor = new System.Windows.Forms.ComboBox();
            this.gpoCliente = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.cboTipoPersonal = new System.Windows.Forms.ComboBox();
            this.txtCodTipoPersonal = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboPersonal = new System.Windows.Forms.ComboBox();
            this.txtCodPersonal = new System.Windows.Forms.TextBox();
            this.btnVendedor = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btcCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtComisionVendedor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.tlsBarArticulo.SuspendLayout();
            this.gpoCliente.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwVendedor
            // 
            this.lvwVendedor.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwVendedor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IdVendedor,
            this.NombreApellido,
            this.Comisión,
            this.Grupo});
            this.lvwVendedor.FullRowSelect = true;
            this.lvwVendedor.GridLines = true;
            this.lvwVendedor.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwVendedor.HideSelection = false;
            this.lvwVendedor.Location = new System.Drawing.Point(12, 68);
            this.lvwVendedor.MultiSelect = false;
            this.lvwVendedor.Name = "lvwVendedor";
            this.lvwVendedor.Size = new System.Drawing.Size(531, 271);
            this.lvwVendedor.TabIndex = 0;
            this.lvwVendedor.UseCompatibleStateImageBehavior = false;
            this.lvwVendedor.View = System.Windows.Forms.View.Details;
            this.lvwVendedor.SelectedIndexChanged += new System.EventHandler(this.lvwVendedor_SelectedIndexChanged);
            this.lvwVendedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvwVendedor_KeyPress);
            this.lvwVendedor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwVendedor_MouseDoubleClick);
            // 
            // IdVendedor
            // 
            this.IdVendedor.Text = "-";
            this.IdVendedor.Width = 25;
            // 
            // NombreApellido
            // 
            this.NombreApellido.Text = "Nombre y Apellido";
            this.NombreApellido.Width = 300;
            // 
            // Comisión
            // 
            this.Comisión.Text = "% Comisión";
            this.Comisión.Width = 80;
            // 
            // Grupo
            // 
            this.Grupo.Text = "Grupo";
            this.Grupo.Width = 90;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.tlsBarArticulo);
            this.groupBox1.Controls.Add(this.txtBuscarArticulo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboBuscaVendedor);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(532, 50);
            this.groupBox1.TabIndex = 1;
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
            this.tsBtnSalir,
            this.toolStripSeparator3});
            this.tlsBarArticulo.Location = new System.Drawing.Point(400, 12);
            this.tlsBarArticulo.Name = "tlsBarArticulo";
            this.tlsBarArticulo.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tlsBarArticulo.Size = new System.Drawing.Size(127, 31);
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
            this.tsBtnNuevo.Text = "Nuevo Vendedor";
            this.tsBtnNuevo.Click += new System.EventHandler(this.tsBtnNuevo_Click);
            // 
            // tsBtnModificar
            // 
            this.tsBtnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnModificar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnModificar.Image")));
            this.tsBtnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnModificar.Name = "tsBtnModificar";
            this.tsBtnModificar.Size = new System.Drawing.Size(28, 28);
            this.tsBtnModificar.Text = "Modificar Vendedor";
            this.tsBtnModificar.Click += new System.EventHandler(this.tsBtnModificar_Click);
            // 
            // tsBtnBuscar
            // 
            this.tsBtnBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnBuscar.Image")));
            this.tsBtnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnBuscar.Name = "tsBtnBuscar";
            this.tsBtnBuscar.Size = new System.Drawing.Size(28, 28);
            this.tsBtnBuscar.Text = "Buscar Vendedor";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
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
            this.txtBuscarArticulo.Size = new System.Drawing.Size(139, 20);
            this.txtBuscarArticulo.TabIndex = 3;
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
            // cboBuscaVendedor
            // 
            this.cboBuscaVendedor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboBuscaVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBuscaVendedor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboBuscaVendedor.FormattingEnabled = true;
            this.cboBuscaVendedor.Items.AddRange(new object[] {
            "Nombre o Apellido"});
            this.cboBuscaVendedor.Location = new System.Drawing.Point(78, 19);
            this.cboBuscaVendedor.Name = "cboBuscaVendedor";
            this.cboBuscaVendedor.Size = new System.Drawing.Size(120, 21);
            this.cboBuscaVendedor.TabIndex = 2;
            // 
            // gpoCliente
            // 
            this.gpoCliente.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpoCliente.Controls.Add(this.label3);
            this.gpoCliente.Controls.Add(this.btnEliminar);
            this.gpoCliente.Controls.Add(this.cboTipoPersonal);
            this.gpoCliente.Controls.Add(this.txtCodTipoPersonal);
            this.gpoCliente.Controls.Add(this.button1);
            this.gpoCliente.Controls.Add(this.label2);
            this.gpoCliente.Controls.Add(this.cboPersonal);
            this.gpoCliente.Controls.Add(this.txtCodPersonal);
            this.gpoCliente.Controls.Add(this.btnVendedor);
            this.gpoCliente.Controls.Add(this.label14);
            this.gpoCliente.Controls.Add(this.btnModificar);
            this.gpoCliente.Controls.Add(this.btcCerrar);
            this.gpoCliente.Controls.Add(this.btnGuardar);
            this.gpoCliente.Controls.Add(this.txtComisionVendedor);
            this.gpoCliente.Controls.Add(this.label6);
            this.gpoCliente.Location = new System.Drawing.Point(12, 345);
            this.gpoCliente.Name = "gpoCliente";
            this.gpoCliente.Size = new System.Drawing.Size(531, 144);
            this.gpoCliente.TabIndex = 48;
            this.gpoCliente.TabStop = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(419, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 75);
            this.label3.TabIndex = 149;
            this.label3.Text = "NOTA: EL personal asignado como vendedor debe estar incluido en un grupo de traba" +
    "jo.";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(349, 113);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(85, 25);
            this.btnEliminar.TabIndex = 13;
            this.btnEliminar.Text = "   Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // cboTipoPersonal
            // 
            this.cboTipoPersonal.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboTipoPersonal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboTipoPersonal.Enabled = false;
            this.cboTipoPersonal.FormattingEnabled = true;
            this.cboTipoPersonal.Items.AddRange(new object[] {
            "Vendedor"});
            this.cboTipoPersonal.Location = new System.Drawing.Point(183, 44);
            this.cboTipoPersonal.Name = "cboTipoPersonal";
            this.cboTipoPersonal.Size = new System.Drawing.Size(172, 21);
            this.cboTipoPersonal.TabIndex = 8;
            // 
            // txtCodTipoPersonal
            // 
            this.txtCodTipoPersonal.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodTipoPersonal.Enabled = false;
            this.txtCodTipoPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodTipoPersonal.Location = new System.Drawing.Point(127, 45);
            this.txtCodTipoPersonal.Name = "txtCodTipoPersonal";
            this.txtCodTipoPersonal.Size = new System.Drawing.Size(50, 20);
            this.txtCodTipoPersonal.TabIndex = 7;
            this.txtCodTipoPersonal.Text = "5";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(361, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 25);
            this.button1.TabIndex = 8;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 147;
            this.label2.Text = "Cod. TipoPersonal:";
            // 
            // cboPersonal
            // 
            this.cboPersonal.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboPersonal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboPersonal.FormattingEnabled = true;
            this.cboPersonal.Location = new System.Drawing.Point(183, 18);
            this.cboPersonal.Name = "cboPersonal";
            this.cboPersonal.Size = new System.Drawing.Size(172, 21);
            this.cboPersonal.TabIndex = 5;
            // 
            // txtCodPersonal
            // 
            this.txtCodPersonal.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodPersonal.Location = new System.Drawing.Point(127, 19);
            this.txtCodPersonal.Name = "txtCodPersonal";
            this.txtCodPersonal.Size = new System.Drawing.Size(50, 20);
            this.txtCodPersonal.TabIndex = 4;
            this.txtCodPersonal.TextChanged += new System.EventHandler(this.txtCodPersonal_TextChanged);
            // 
            // btnVendedor
            // 
            this.btnVendedor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnVendedor.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnVendedor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVendedor.Image = ((System.Drawing.Image)(resources.GetObject("btnVendedor.Image")));
            this.btnVendedor.Location = new System.Drawing.Point(361, 16);
            this.btnVendedor.Name = "btnVendedor";
            this.btnVendedor.Size = new System.Drawing.Size(30, 25);
            this.btnVendedor.TabIndex = 6;
            this.btnVendedor.UseVisualStyleBackColor = false;
            this.btnVendedor.Click += new System.EventHandler(this.btnVendedor_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(45, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 13);
            this.label14.TabIndex = 143;
            this.label14.Text = "Cod. Personal:";
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.Location = new System.Drawing.Point(258, 113);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(85, 25);
            this.btnModificar.TabIndex = 12;
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
            this.btcCerrar.Location = new System.Drawing.Point(440, 113);
            this.btcCerrar.Name = "btcCerrar";
            this.btcCerrar.Size = new System.Drawing.Size(85, 25);
            this.btcCerrar.TabIndex = 14;
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
            this.btnGuardar.Location = new System.Drawing.Point(167, 113);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 25);
            this.btnGuardar.TabIndex = 11;
            this.btnGuardar.Text = "   Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtComisionVendedor
            // 
            this.txtComisionVendedor.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtComisionVendedor.Location = new System.Drawing.Point(127, 71);
            this.txtComisionVendedor.Name = "txtComisionVendedor";
            this.txtComisionVendedor.Size = new System.Drawing.Size(50, 20);
            this.txtComisionVendedor.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "% Comisión de Venta: ";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Action share.ico");
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmVendedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(553, 499);
            this.Controls.Add(this.lvwVendedor);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpoCliente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmVendedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vendedor";
            this.Load += new System.EventHandler(this.frmVendedor_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tlsBarArticulo.ResumeLayout(false);
            this.tlsBarArticulo.PerformLayout();
            this.gpoCliente.ResumeLayout(false);
            this.gpoCliente.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwVendedor;
        private System.Windows.Forms.ColumnHeader IdVendedor;
        private System.Windows.Forms.ColumnHeader NombreApellido;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip tlsBarArticulo;
        private System.Windows.Forms.ToolStripButton tsBtnNuevo;
        private System.Windows.Forms.ToolStripButton tsBtnModificar;
        private System.Windows.Forms.ToolStripButton tsBtnBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TextBox txtBuscarArticulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBuscaVendedor;
        private System.Windows.Forms.GroupBox gpoCliente;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btcCerrar;
        private System.Windows.Forms.TextBox txtComisionVendedor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader Comisión;
        private System.Windows.Forms.ComboBox cboTipoPersonal;
        private System.Windows.Forms.TextBox txtCodTipoPersonal;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboPersonal;
        private System.Windows.Forms.TextBox txtCodPersonal;
        private System.Windows.Forms.Button btnVendedor;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ColumnHeader Grupo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label label3;
    }
}