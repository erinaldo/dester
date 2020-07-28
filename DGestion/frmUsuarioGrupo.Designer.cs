namespace DGestion
{
    partial class frmUsuarioGrupo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsuarioGrupo));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tlsBarArticulo = new System.Windows.Forms.ToolStrip();
            this.tsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.txtBuscarArticulo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBuscaPersonal = new System.Windows.Forms.ComboBox();
            this.lvwPersonal = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NombreApellido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TipoPersonal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Usuario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IDTIPOPERSONAL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnActualizarDatos = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRepetirContraseña = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnIncluirAlGrupo = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cboGrupoUsuario = new System.Windows.Forms.ComboBox();
            this.btcCerrar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tlsBarArticulo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox1.Size = new System.Drawing.Size(816, 50);
            this.groupBox1.TabIndex = 34;
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
            this.tsBtnBuscar,
            this.toolStripSeparator2,
            this.tsBtnSalir,
            this.toolStripSeparator3});
            this.tlsBarArticulo.Location = new System.Drawing.Point(740, 12);
            this.tlsBarArticulo.Name = "tlsBarArticulo";
            this.tlsBarArticulo.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tlsBarArticulo.Size = new System.Drawing.Size(71, 31);
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
            this.txtBuscarArticulo.Size = new System.Drawing.Size(150, 20);
            this.txtBuscarArticulo.TabIndex = 1;
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
            "Nombre o Apellido",
            "Nombre de Usuario"});
            this.cboBuscaPersonal.Location = new System.Drawing.Point(78, 19);
            this.cboBuscaPersonal.Name = "cboBuscaPersonal";
            this.cboBuscaPersonal.Size = new System.Drawing.Size(120, 21);
            this.cboBuscaPersonal.TabIndex = 0;
            // 
            // lvwPersonal
            // 
            this.lvwPersonal.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwPersonal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.NombreApellido,
            this.TipoPersonal,
            this.Usuario,
            this.IDTIPOPERSONAL});
            this.lvwPersonal.FullRowSelect = true;
            this.lvwPersonal.GridLines = true;
            this.lvwPersonal.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwPersonal.HideSelection = false;
            this.lvwPersonal.LargeImageList = this.imageList1;
            this.lvwPersonal.Location = new System.Drawing.Point(12, 68);
            this.lvwPersonal.MultiSelect = false;
            this.lvwPersonal.Name = "lvwPersonal";
            this.lvwPersonal.Size = new System.Drawing.Size(478, 480);
            this.lvwPersonal.SmallImageList = this.imageList1;
            this.lvwPersonal.TabIndex = 35;
            this.lvwPersonal.UseCompatibleStateImageBehavior = false;
            this.lvwPersonal.View = System.Windows.Forms.View.Details;
            this.lvwPersonal.SelectedIndexChanged += new System.EventHandler(this.lvwPersonal_SelectedIndexChanged);
            // 
            // Id
            // 
            this.Id.Text = "-";
            this.Id.Width = 23;
            // 
            // NombreApellido
            // 
            this.NombreApellido.Text = "Nombre y Apellido";
            this.NombreApellido.Width = 200;
            // 
            // TipoPersonal
            // 
            this.TipoPersonal.Text = "Grupo";
            this.TipoPersonal.Width = 120;
            // 
            // Usuario
            // 
            this.Usuario.Text = "Usuario";
            this.Usuario.Width = 110;
            // 
            // IDTIPOPERSONAL
            // 
            this.IDTIPOPERSONAL.Text = "IDTIPOPERSONAL";
            this.IDTIPOPERSONAL.Width = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "man black.ico");
            this.imageList1.Images.SetKeyName(1, "man.ico");
            this.imageList1.Images.SetKeyName(2, "tester.ico");
            this.imageList1.Images.SetKeyName(3, "woman.ico");
            this.imageList1.Images.SetKeyName(4, "user.ico");
            this.imageList1.Images.SetKeyName(5, "privacy.ico");
            this.imageList1.Images.SetKeyName(6, "Sign Alert.ico");
            this.imageList1.Images.SetKeyName(7, "man key.ico");
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(496, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 456);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configurar Grupo Nombre de Usuario y Contraseña";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox5.Controls.Add(this.btnActualizarDatos);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.txtRepetirContraseña);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.txtContraseña);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.txtNombreUsuario);
            this.groupBox5.Location = new System.Drawing.Point(6, 33);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(320, 145);
            this.groupBox5.TabIndex = 40;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Nombre de Usuario y Contraseña";
            // 
            // btnActualizarDatos
            // 
            this.btnActualizarDatos.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnActualizarDatos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActualizarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizarDatos.Image")));
            this.btnActualizarDatos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActualizarDatos.Location = new System.Drawing.Point(224, 112);
            this.btnActualizarDatos.Name = "btnActualizarDatos";
            this.btnActualizarDatos.Size = new System.Drawing.Size(85, 25);
            this.btnActualizarDatos.TabIndex = 46;
            this.btnActualizarDatos.Text = "  Actualizar";
            this.btnActualizarDatos.UseVisualStyleBackColor = false;
            this.btnActualizarDatos.Click += new System.EventHandler(this.btnGuardarDatos_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Repetir Contraseña";
            // 
            // txtRepetirContraseña
            // 
            this.txtRepetirContraseña.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtRepetirContraseña.Location = new System.Drawing.Point(112, 86);
            this.txtRepetirContraseña.Name = "txtRepetirContraseña";
            this.txtRepetirContraseña.PasswordChar = '*';
            this.txtRepetirContraseña.Size = new System.Drawing.Size(197, 20);
            this.txtRepetirContraseña.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Contraseña Usuario";
            // 
            // txtContraseña
            // 
            this.txtContraseña.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtContraseña.Location = new System.Drawing.Point(112, 60);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.PasswordChar = '*';
            this.txtContraseña.Size = new System.Drawing.Size(197, 20);
            this.txtContraseña.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Nombre Usuario";
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNombreUsuario.Location = new System.Drawing.Point(112, 34);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(197, 20);
            this.txtNombreUsuario.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox3.Controls.Add(this.btnIncluirAlGrupo);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cboGrupoUsuario);
            this.groupBox3.Location = new System.Drawing.Point(6, 184);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(320, 95);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Grupo de Usuario";
            // 
            // btnIncluirAlGrupo
            // 
            this.btnIncluirAlGrupo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnIncluirAlGrupo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIncluirAlGrupo.Image = ((System.Drawing.Image)(resources.GetObject("btnIncluirAlGrupo.Image")));
            this.btnIncluirAlGrupo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIncluirAlGrupo.Location = new System.Drawing.Point(159, 61);
            this.btnIncluirAlGrupo.Name = "btnIncluirAlGrupo";
            this.btnIncluirAlGrupo.Size = new System.Drawing.Size(150, 25);
            this.btnIncluirAlGrupo.TabIndex = 45;
            this.btnIncluirAlGrupo.Text = "     Incluir Usuario al Grupo";
            this.btnIncluirAlGrupo.UseVisualStyleBackColor = false;
            this.btnIncluirAlGrupo.Click += new System.EventHandler(this.btnIncluirAlGrupo_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Grupo del Usuario";
            // 
            // cboGrupoUsuario
            // 
            this.cboGrupoUsuario.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboGrupoUsuario.FormattingEnabled = true;
            this.cboGrupoUsuario.Location = new System.Drawing.Point(112, 34);
            this.cboGrupoUsuario.Name = "cboGrupoUsuario";
            this.cboGrupoUsuario.Size = new System.Drawing.Size(197, 21);
            this.cboGrupoUsuario.TabIndex = 1;
            // 
            // btcCerrar
            // 
            this.btcCerrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btcCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btcCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btcCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btcCerrar.Image")));
            this.btcCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btcCerrar.Location = new System.Drawing.Point(743, 530);
            this.btcCerrar.Name = "btcCerrar";
            this.btcCerrar.Size = new System.Drawing.Size(85, 25);
            this.btcCerrar.TabIndex = 47;
            this.btcCerrar.Text = "   Cerrar";
            this.btcCerrar.UseVisualStyleBackColor = false;
            this.btcCerrar.Click += new System.EventHandler(this.btcCerrar_Click);
            // 
            // frmUsuarioGrupo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.btcCerrar;
            this.ClientSize = new System.Drawing.Size(840, 560);
            this.Controls.Add(this.btcCerrar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lvwPersonal);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmUsuarioGrupo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración de Usuarios y Grupos";
            this.Load += new System.EventHandler(this.frmUsuarioGrupo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tlsBarArticulo.ResumeLayout(false);
            this.tlsBarArticulo.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip tlsBarArticulo;
        private System.Windows.Forms.ToolStripButton tsBtnBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TextBox txtBuscarArticulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBuscaPersonal;
        private System.Windows.Forms.ListView lvwPersonal;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader NombreApellido;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboGrupoUsuario;
        private System.Windows.Forms.Button btnIncluirAlGrupo;
        private System.Windows.Forms.Button btcCerrar;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRepetirContraseña;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.ColumnHeader Usuario;
        private System.Windows.Forms.Button btnActualizarDatos;
        private System.Windows.Forms.ColumnHeader IDTIPOPERSONAL;
        private System.Windows.Forms.ColumnHeader TipoPersonal;
    }
}