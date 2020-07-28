namespace DGestion
{
    partial class frmListaPrecioVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaPrecioVenta));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsBtnModificar = new System.Windows.Forms.ToolStripButton();
            this.tsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.txtConsulta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBuscar = new System.Windows.Forms.ComboBox();
            this.gpoCliente = new System.Windows.Forms.GroupBox();
            this.txtPorcentFlete = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPorcent = new System.Windows.Forms.TextBox();
            this.lblPorcent = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btcCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtDescripListaPrecio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lvwListaPrecioVenta = new System.Windows.Forms.ListView();
            this.IdListaVenta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Descripción = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Porcentaje2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Porcentaje1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.Orden = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtCriterio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gpoCliente.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.txtConsulta);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbBuscar);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(520, 49);
            this.groupBox1.TabIndex = 42;
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
            this.toolStrip1.Location = new System.Drawing.Point(355, 15);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(127, 31);
            this.toolStrip1.TabIndex = 3;
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
            // txtConsulta
            // 
            this.txtConsulta.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtConsulta.Location = new System.Drawing.Point(197, 16);
            this.txtConsulta.Name = "txtConsulta";
            this.txtConsulta.Size = new System.Drawing.Size(111, 20);
            this.txtConsulta.TabIndex = 1;
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
            // cmbBuscar
            // 
            this.cmbBuscar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cmbBuscar.FormattingEnabled = true;
            this.cmbBuscar.Items.AddRange(new object[] {
            "Cód. Lista Precio",
            "Descripción"});
            this.cmbBuscar.Location = new System.Drawing.Point(76, 16);
            this.cmbBuscar.Name = "cmbBuscar";
            this.cmbBuscar.Size = new System.Drawing.Size(115, 21);
            this.cmbBuscar.TabIndex = 0;
            // 
            // gpoCliente
            // 
            this.gpoCliente.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpoCliente.Controls.Add(this.txtCriterio);
            this.gpoCliente.Controls.Add(this.label5);
            this.gpoCliente.Controls.Add(this.txtPorcentFlete);
            this.gpoCliente.Controls.Add(this.label4);
            this.gpoCliente.Controls.Add(this.txtPorcent);
            this.gpoCliente.Controls.Add(this.lblPorcent);
            this.gpoCliente.Controls.Add(this.txtCodigo);
            this.gpoCliente.Controls.Add(this.label3);
            this.gpoCliente.Controls.Add(this.btnEliminar);
            this.gpoCliente.Controls.Add(this.btnModificar);
            this.gpoCliente.Controls.Add(this.btcCerrar);
            this.gpoCliente.Controls.Add(this.btnGuardar);
            this.gpoCliente.Controls.Add(this.txtDescripListaPrecio);
            this.gpoCliente.Controls.Add(this.label2);
            this.gpoCliente.Location = new System.Drawing.Point(11, 259);
            this.gpoCliente.Name = "gpoCliente";
            this.gpoCliente.Size = new System.Drawing.Size(521, 133);
            this.gpoCliente.TabIndex = 28;
            this.gpoCliente.TabStop = false;
            // 
            // txtPorcentFlete
            // 
            this.txtPorcentFlete.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtPorcentFlete.Location = new System.Drawing.Point(427, 44);
            this.txtPorcentFlete.Name = "txtPorcentFlete";
            this.txtPorcentFlete.Size = new System.Drawing.Size(53, 20);
            this.txtPorcentFlete.TabIndex = 35;
            this.txtPorcentFlete.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(377, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "% Flete:";
            // 
            // txtPorcent
            // 
            this.txtPorcent.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtPorcent.Location = new System.Drawing.Point(318, 44);
            this.txtPorcent.Name = "txtPorcent";
            this.txtPorcent.Size = new System.Drawing.Size(53, 20);
            this.txtPorcent.TabIndex = 5;
            this.txtPorcent.Text = "0";
            this.txtPorcent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorcent_KeyPress);
            this.txtPorcent.Leave += new System.EventHandler(this.txtPorcent_Leave);
            // 
            // lblPorcent
            // 
            this.lblPorcent.AutoSize = true;
            this.lblPorcent.Location = new System.Drawing.Point(263, 47);
            this.lblPorcent.Name = "lblPorcent";
            this.lblPorcent.Size = new System.Drawing.Size(49, 13);
            this.lblPorcent.TabIndex = 34;
            this.lblPorcent.Text = "% Venta:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtCodigo.Location = new System.Drawing.Point(78, 18);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(70, 20);
            this.txtCodigo.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Código:";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(304, 102);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(85, 25);
            this.btnEliminar.TabIndex = 8;
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
            this.btnModificar.Location = new System.Drawing.Point(213, 102);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(85, 25);
            this.btnModificar.TabIndex = 7;
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
            this.btcCerrar.Location = new System.Drawing.Point(395, 102);
            this.btcCerrar.Name = "btcCerrar";
            this.btcCerrar.Size = new System.Drawing.Size(85, 25);
            this.btcCerrar.TabIndex = 9;
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
            this.btnGuardar.Location = new System.Drawing.Point(122, 102);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 25);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "   Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtDescripListaPrecio
            // 
            this.txtDescripListaPrecio.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtDescripListaPrecio.Location = new System.Drawing.Point(78, 44);
            this.txtDescripListaPrecio.Name = "txtDescripListaPrecio";
            this.txtDescripListaPrecio.Size = new System.Drawing.Size(179, 20);
            this.txtDescripListaPrecio.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Descripción:";
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(11, 243);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(10, 10);
            this.btnSalir.TabIndex = 47;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lvwListaPrecioVenta
            // 
            this.lvwListaPrecioVenta.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwListaPrecioVenta.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwListaPrecioVenta.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IdListaVenta,
            this.Descripción,
            this.Porcentaje2,
            this.Porcentaje1,
            this.Orden});
            this.lvwListaPrecioVenta.FullRowSelect = true;
            this.lvwListaPrecioVenta.GridLines = true;
            this.lvwListaPrecioVenta.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwListaPrecioVenta.HideSelection = false;
            this.lvwListaPrecioVenta.LargeImageList = this.imageList1;
            this.lvwListaPrecioVenta.Location = new System.Drawing.Point(11, 58);
            this.lvwListaPrecioVenta.MultiSelect = false;
            this.lvwListaPrecioVenta.Name = "lvwListaPrecioVenta";
            this.lvwListaPrecioVenta.Size = new System.Drawing.Size(521, 195);
            this.lvwListaPrecioVenta.SmallImageList = this.imageList1;
            this.lvwListaPrecioVenta.TabIndex = 48;
            this.lvwListaPrecioVenta.UseCompatibleStateImageBehavior = false;
            this.lvwListaPrecioVenta.View = System.Windows.Forms.View.Details;
            this.lvwListaPrecioVenta.SelectedIndexChanged += new System.EventHandler(this.lvwListaPrecioVenta_SelectedIndexChanged);
            this.lvwListaPrecioVenta.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwListaPrecioVenta_MouseDoubleClick);
            // 
            // IdListaVenta
            // 
            this.IdListaVenta.Text = "-";
            this.IdListaVenta.Width = 25;
            // 
            // Descripción
            // 
            this.Descripción.Text = "Descripción";
            this.Descripción.Width = 180;
            // 
            // Porcentaje2
            // 
            this.Porcentaje2.Text = "% Aplicado a Flete";
            this.Porcentaje2.Width = 100;
            // 
            // Porcentaje1
            // 
            this.Porcentaje1.Text = "% Aplicado Vta.";
            this.Porcentaje1.Width = 100;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder full.ico");
            // 
            // Orden
            // 
            this.Orden.Text = "Orden";
            this.Orden.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Orden.Width = 80;
            // 
            // txtCriterio
            // 
            this.txtCriterio.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCriterio.Location = new System.Drawing.Point(77, 70);
            this.txtCriterio.Name = "txtCriterio";
            this.txtCriterio.Size = new System.Drawing.Size(53, 20);
            this.txtCriterio.TabIndex = 37;
            this.txtCriterio.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Orden:";
            // 
            // frmListaPrecioVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(544, 404);
            this.Controls.Add(this.lvwListaPrecioVenta);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.gpoCliente);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmListaPrecioVenta";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Precios de Venta";
            this.Load += new System.EventHandler(this.frmListaPrecio_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gpoCliente.ResumeLayout(false);
            this.gpoCliente.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBuscar;
        private System.Windows.Forms.GroupBox gpoCliente;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btcCerrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtDescripListaPrecio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPorcent;
        private System.Windows.Forms.Label lblPorcent;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ListView lvwListaPrecioVenta;
        private System.Windows.Forms.ColumnHeader IdListaVenta;
        private System.Windows.Forms.ColumnHeader Descripción;
        private System.Windows.Forms.ColumnHeader Porcentaje1;
        private System.Windows.Forms.ColumnHeader Porcentaje2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtPorcentFlete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader Orden;
        private System.Windows.Forms.TextBox txtCriterio;
        private System.Windows.Forms.Label label5;
    }
}