namespace DGestion
{
    partial class frmListaPrecioCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaPrecioCompra));
            this.lvwCliente = new System.Windows.Forms.ListView();
            this.IdVendedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Descripción = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Precio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CodProveed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RazonSocial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CodArticulo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Artículo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.cboBuscaArticulo = new System.Windows.Forms.ComboBox();
            this.gpoCliente = new System.Windows.Forms.GroupBox();
            this.dtpFechaFactu = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCodRubroArt = new System.Windows.Forms.TextBox();
            this.txtCodFamiliaArt = new System.Windows.Forms.TextBox();
            this.btnRubro = new System.Windows.Forms.Button();
            this.cboRubroArt = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnFamilia = new System.Windows.Forms.Button();
            this.cmbDescripcionFamiliaArt = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btcCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tlsBarArticulo.SuspendLayout();
            this.gpoCliente.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwCliente
            // 
            this.lvwCliente.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwCliente.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwCliente.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IdVendedor,
            this.Descripción,
            this.Precio,
            this.CodProveed,
            this.RazonSocial,
            this.CodArticulo,
            this.Artículo});
            this.lvwCliente.FullRowSelect = true;
            this.lvwCliente.GridLines = true;
            this.lvwCliente.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwCliente.HideSelection = false;
            this.lvwCliente.Location = new System.Drawing.Point(12, 68);
            this.lvwCliente.MultiSelect = false;
            this.lvwCliente.Name = "lvwCliente";
            this.lvwCliente.Size = new System.Drawing.Size(832, 371);
            this.lvwCliente.TabIndex = 53;
            this.lvwCliente.UseCompatibleStateImageBehavior = false;
            this.lvwCliente.View = System.Windows.Forms.View.Details;
            // 
            // IdVendedor
            // 
            this.IdVendedor.Text = "id";
            this.IdVendedor.Width = 0;
            // 
            // Descripción
            // 
            this.Descripción.Text = "Descripción";
            this.Descripción.Width = 150;
            // 
            // Precio
            // 
            this.Precio.Text = "Precio";
            this.Precio.Width = 90;
            // 
            // CodProveed
            // 
            this.CodProveed.Text = "Cód. Proveed";
            this.CodProveed.Width = 90;
            // 
            // RazonSocial
            // 
            this.RazonSocial.Text = "Razon Social";
            this.RazonSocial.Width = 250;
            // 
            // CodArticulo
            // 
            this.CodArticulo.Text = "CodArticulo";
            this.CodArticulo.Width = 0;
            // 
            // Artículo
            // 
            this.Artículo.Text = "Artículo";
            this.Artículo.Width = 220;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.tlsBarArticulo);
            this.groupBox1.Controls.Add(this.txtBuscarArticulo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboBuscaArticulo);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(832, 50);
            this.groupBox1.TabIndex = 52;
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
            this.tlsBarArticulo.Location = new System.Drawing.Point(670, 16);
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
            this.tsBtnNuevo.Text = "Nuevo Artículo";
            // 
            // tsBtnModificar
            // 
            this.tsBtnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnModificar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnModificar.Image")));
            this.tsBtnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnModificar.Name = "tsBtnModificar";
            this.tsBtnModificar.Size = new System.Drawing.Size(28, 28);
            this.tsBtnModificar.Text = "Modificar Item";
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
            // cboBuscaArticulo
            // 
            this.cboBuscaArticulo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboBuscaArticulo.FormattingEnabled = true;
            this.cboBuscaArticulo.Items.AddRange(new object[] {
            "Cód. Artículo",
            "Descripción"});
            this.cboBuscaArticulo.Location = new System.Drawing.Point(78, 19);
            this.cboBuscaArticulo.Name = "cboBuscaArticulo";
            this.cboBuscaArticulo.Size = new System.Drawing.Size(120, 21);
            this.cboBuscaArticulo.TabIndex = 0;
            // 
            // gpoCliente
            // 
            this.gpoCliente.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpoCliente.Controls.Add(this.dtpFechaFactu);
            this.gpoCliente.Controls.Add(this.label5);
            this.gpoCliente.Controls.Add(this.txtCodRubroArt);
            this.gpoCliente.Controls.Add(this.txtCodFamiliaArt);
            this.gpoCliente.Controls.Add(this.btnRubro);
            this.gpoCliente.Controls.Add(this.cboRubroArt);
            this.gpoCliente.Controls.Add(this.label7);
            this.gpoCliente.Controls.Add(this.btnFamilia);
            this.gpoCliente.Controls.Add(this.cmbDescripcionFamiliaArt);
            this.gpoCliente.Controls.Add(this.label8);
            this.gpoCliente.Controls.Add(this.btnEliminar);
            this.gpoCliente.Controls.Add(this.btnModificar);
            this.gpoCliente.Controls.Add(this.btcCerrar);
            this.gpoCliente.Controls.Add(this.btnGuardar);
            this.gpoCliente.Controls.Add(this.textBox4);
            this.gpoCliente.Controls.Add(this.label4);
            this.gpoCliente.Controls.Add(this.textBox3);
            this.gpoCliente.Controls.Add(this.label3);
            this.gpoCliente.Location = new System.Drawing.Point(12, 445);
            this.gpoCliente.Name = "gpoCliente";
            this.gpoCliente.Size = new System.Drawing.Size(832, 129);
            this.gpoCliente.TabIndex = 51;
            this.gpoCliente.TabStop = false;
            // 
            // dtpFechaFactu
            // 
            this.dtpFechaFactu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFactu.Location = new System.Drawing.Point(438, 22);
            this.dtpFechaFactu.Name = "dtpFechaFactu";
            this.dtpFechaFactu.Size = new System.Drawing.Size(136, 20);
            this.dtpFechaFactu.TabIndex = 71;
            this.dtpFechaFactu.Value = new System.DateTime(2015, 3, 16, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(392, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 72;
            this.label5.Text = "Fecha:";
            // 
            // txtCodRubroArt
            // 
            this.txtCodRubroArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodRubroArt.Location = new System.Drawing.Point(96, 49);
            this.txtCodRubroArt.Name = "txtCodRubroArt";
            this.txtCodRubroArt.Size = new System.Drawing.Size(50, 20);
            this.txtCodRubroArt.TabIndex = 45;
            // 
            // txtCodFamiliaArt
            // 
            this.txtCodFamiliaArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodFamiliaArt.Location = new System.Drawing.Point(96, 23);
            this.txtCodFamiliaArt.Name = "txtCodFamiliaArt";
            this.txtCodFamiliaArt.Size = new System.Drawing.Size(50, 20);
            this.txtCodFamiliaArt.TabIndex = 42;
            // 
            // btnRubro
            // 
            this.btnRubro.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRubro.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRubro.Image = ((System.Drawing.Image)(resources.GetObject("btnRubro.Image")));
            this.btnRubro.Location = new System.Drawing.Point(332, 46);
            this.btnRubro.Name = "btnRubro";
            this.btnRubro.Size = new System.Drawing.Size(30, 25);
            this.btnRubro.TabIndex = 47;
            this.btnRubro.UseVisualStyleBackColor = false;
            // 
            // cboRubroArt
            // 
            this.cboRubroArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboRubroArt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboRubroArt.FormattingEnabled = true;
            this.cboRubroArt.Location = new System.Drawing.Point(152, 48);
            this.cboRubroArt.Name = "cboRubroArt";
            this.cboRubroArt.Size = new System.Drawing.Size(174, 21);
            this.cboRubroArt.TabIndex = 46;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 49;
            this.label7.Text = "Cód Artículo:";
            // 
            // btnFamilia
            // 
            this.btnFamilia.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFamilia.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnFamilia.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFamilia.Image = ((System.Drawing.Image)(resources.GetObject("btnFamilia.Image")));
            this.btnFamilia.Location = new System.Drawing.Point(332, 18);
            this.btnFamilia.Name = "btnFamilia";
            this.btnFamilia.Size = new System.Drawing.Size(30, 25);
            this.btnFamilia.TabIndex = 44;
            this.btnFamilia.UseVisualStyleBackColor = false;
            this.btnFamilia.Click += new System.EventHandler(this.btnFamilia_Click);
            // 
            // cmbDescripcionFamiliaArt
            // 
            this.cmbDescripcionFamiliaArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cmbDescripcionFamiliaArt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbDescripcionFamiliaArt.FormattingEnabled = true;
            this.cmbDescripcionFamiliaArt.Location = new System.Drawing.Point(152, 22);
            this.cmbDescripcionFamiliaArt.Name = "cmbDescripcionFamiliaArt";
            this.cmbDescripcionFamiliaArt.Size = new System.Drawing.Size(174, 21);
            this.cmbDescripcionFamiliaArt.TabIndex = 43;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 48;
            this.label8.Text = "Cod. Proveedor:";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEliminar.Enabled = false;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(650, 98);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(85, 25);
            this.btnEliminar.TabIndex = 30;
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
            this.btnModificar.Location = new System.Drawing.Point(559, 98);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(85, 25);
            this.btnModificar.TabIndex = 29;
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
            this.btcCerrar.Location = new System.Drawing.Point(741, 98);
            this.btcCerrar.Name = "btcCerrar";
            this.btcCerrar.Size = new System.Drawing.Size(85, 25);
            this.btcCerrar.TabIndex = 31;
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
            this.btnGuardar.Location = new System.Drawing.Point(468, 98);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 25);
            this.btnGuardar.TabIndex = 28;
            this.btnGuardar.Text = "   Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.textBox4.Location = new System.Drawing.Point(438, 50);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(284, 20);
            this.textBox4.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(375, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Condición:";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.textBox3.Location = new System.Drawing.Point(636, 22);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(86, 20);
            this.textBox3.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(590, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Precio:";
            // 
            // frmListaPrecioCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(855, 586);
            this.Controls.Add(this.lvwCliente);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpoCliente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmListaPrecioCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Precio de Proveedores";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tlsBarArticulo.ResumeLayout(false);
            this.tlsBarArticulo.PerformLayout();
            this.gpoCliente.ResumeLayout(false);
            this.gpoCliente.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwCliente;
        private System.Windows.Forms.ColumnHeader IdVendedor;
        private System.Windows.Forms.ColumnHeader Descripción;
        private System.Windows.Forms.ColumnHeader Precio;
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
        private System.Windows.Forms.ComboBox cboBuscaArticulo;
        private System.Windows.Forms.GroupBox gpoCliente;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btcCerrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodRubroArt;
        private System.Windows.Forms.TextBox txtCodFamiliaArt;
        private System.Windows.Forms.Button btnRubro;
        private System.Windows.Forms.ComboBox cboRubroArt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnFamilia;
        private System.Windows.Forms.ComboBox cmbDescripcionFamiliaArt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpFechaFactu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader CodProveed;
        private System.Windows.Forms.ColumnHeader RazonSocial;
        private System.Windows.Forms.ColumnHeader CodArticulo;
        private System.Windows.Forms.ColumnHeader Artículo;
    }
}