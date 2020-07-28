namespace DGestion
{
    partial class frmSoporteTecnico
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSoporteTecnico));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gpCompraProveedor = new System.Windows.Forms.GroupBox();
            this.tlsBarArticulo = new System.Windows.Forms.ToolStrip();
            this.tsBtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnReporte = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.txtBuscarArticulo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboBuscaTiquetSoporte = new System.Windows.Forms.ComboBox();
            this.gpoCliente = new System.Windows.Forms.GroupBox();
            this.cboEstadoActual = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPrioridad = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dtpFechaFactu = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btcCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lvwSoporteTecnico = new System.Windows.Forms.ListView();
            this.NroTiquet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FechaHora = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Descripción = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Prioridad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Usuario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EstadoActual = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gpCompraProveedor.SuspendLayout();
            this.tlsBarArticulo.SuspendLayout();
            this.gpoCliente.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "insect blue.ico");
            this.imageList1.Images.SetKeyName(1, "insect.ico");
            this.imageList1.Images.SetKeyName(2, "Misc Bug.ico");
            this.imageList1.Images.SetKeyName(3, "system monitor1.ico");
            this.imageList1.Images.SetKeyName(4, "OK Shield.ico");
            // 
            // gpCompraProveedor
            // 
            this.gpCompraProveedor.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpCompraProveedor.Controls.Add(this.tlsBarArticulo);
            this.gpCompraProveedor.Controls.Add(this.txtBuscarArticulo);
            this.gpCompraProveedor.Controls.Add(this.label3);
            this.gpCompraProveedor.Controls.Add(this.cboBuscaTiquetSoporte);
            this.gpCompraProveedor.Location = new System.Drawing.Point(12, 12);
            this.gpCompraProveedor.Name = "gpCompraProveedor";
            this.gpCompraProveedor.Size = new System.Drawing.Size(996, 50);
            this.gpCompraProveedor.TabIndex = 152;
            this.gpCompraProveedor.TabStop = false;
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
            this.tsBtnBuscar,
            this.toolStripSeparator2,
            this.tsBtnReporte,
            this.toolStripSeparator1,
            this.tsBtnSalir,
            this.toolStripSeparator3});
            this.tlsBarArticulo.Location = new System.Drawing.Point(860, 16);
            this.tlsBarArticulo.Name = "tlsBarArticulo";
            this.tlsBarArticulo.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tlsBarArticulo.Size = new System.Drawing.Size(133, 31);
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
            this.tsBtnNuevo.Text = "Nueva Solicitud";
            this.tsBtnNuevo.Click += new System.EventHandler(this.tsBtnNuevo_Click);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Buscar por:";
            // 
            // cboBuscaTiquetSoporte
            // 
            this.cboBuscaTiquetSoporte.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboBuscaTiquetSoporte.FormattingEnabled = true;
            this.cboBuscaTiquetSoporte.Items.AddRange(new object[] {
            "Fecha Solicutud"});
            this.cboBuscaTiquetSoporte.Location = new System.Drawing.Point(78, 19);
            this.cboBuscaTiquetSoporte.Name = "cboBuscaTiquetSoporte";
            this.cboBuscaTiquetSoporte.Size = new System.Drawing.Size(120, 21);
            this.cboBuscaTiquetSoporte.TabIndex = 0;
            // 
            // gpoCliente
            // 
            this.gpoCliente.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpoCliente.Controls.Add(this.cboEstadoActual);
            this.gpoCliente.Controls.Add(this.label7);
            this.gpoCliente.Controls.Add(this.label6);
            this.gpoCliente.Controls.Add(this.txtObservaciones);
            this.gpoCliente.Controls.Add(this.label4);
            this.gpoCliente.Controls.Add(this.label1);
            this.gpoCliente.Controls.Add(this.cboPrioridad);
            this.gpoCliente.Controls.Add(this.label16);
            this.gpoCliente.Controls.Add(this.dtpFechaFactu);
            this.gpoCliente.Controls.Add(this.label5);
            this.gpoCliente.Controls.Add(this.txtDescripcion);
            this.gpoCliente.Controls.Add(this.label2);
            this.gpoCliente.Controls.Add(this.btnEliminar);
            this.gpoCliente.Controls.Add(this.btnModificar);
            this.gpoCliente.Controls.Add(this.btcCerrar);
            this.gpoCliente.Controls.Add(this.btnGuardar);
            this.gpoCliente.Location = new System.Drawing.Point(12, 481);
            this.gpoCliente.Name = "gpoCliente";
            this.gpoCliente.Size = new System.Drawing.Size(996, 213);
            this.gpoCliente.TabIndex = 151;
            this.gpoCliente.TabStop = false;
            // 
            // cboEstadoActual
            // 
            this.cboEstadoActual.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboEstadoActual.Enabled = false;
            this.cboEstadoActual.FormattingEnabled = true;
            this.cboEstadoActual.Items.AddRange(new object[] {
            "Pendiente",
            "Atendiendo",
            "Solucionado"});
            this.cboEstadoActual.Location = new System.Drawing.Point(103, 124);
            this.cboEstadoActual.Name = "cboEstadoActual";
            this.cboEstadoActual.Size = new System.Drawing.Size(100, 21);
            this.cboEstadoActual.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 142;
            this.label7.Text = "Requerimiento)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 141;
            this.label6.Text = "(Problema o";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtObservaciones.Location = new System.Drawing.Point(296, 124);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.ReadOnly = true;
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservaciones.Size = new System.Drawing.Size(694, 52);
            this.txtObservaciones.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(209, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 139;
            this.label4.Text = "Observaciones:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 137;
            this.label1.Text = "Estado Solicitud:";
            // 
            // cboPrioridad
            // 
            this.cboPrioridad.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboPrioridad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPrioridad.FormattingEnabled = true;
            this.cboPrioridad.Items.AddRange(new object[] {
            "Prioridad Baja",
            "Prioridad Media",
            "Prioridad Alta"});
            this.cboPrioridad.Location = new System.Drawing.Point(412, 18);
            this.cboPrioridad.Name = "cboPrioridad";
            this.cboPrioridad.Size = new System.Drawing.Size(146, 21);
            this.cboPrioridad.TabIndex = 4;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(244, 21);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(162, 13);
            this.label16.TabIndex = 135;
            this.label16.Text = "Nivel de Prioridad de la Solicitud:";
            // 
            // dtpFechaFactu
            // 
            this.dtpFechaFactu.CustomFormat = "";
            this.dtpFechaFactu.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFactu.Location = new System.Drawing.Point(103, 19);
            this.dtpFechaFactu.Name = "dtpFechaFactu";
            this.dtpFechaFactu.Size = new System.Drawing.Size(135, 20);
            this.dtpFechaFactu.TabIndex = 3;
            this.dtpFechaFactu.Value = new System.DateTime(2015, 4, 29, 1, 21, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 134;
            this.label5.Text = "Fecha Solicitud:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtDescripcion.Location = new System.Drawing.Point(103, 48);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcion.Size = new System.Drawing.Size(887, 70);
            this.txtDescripcion.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 128;
            this.label2.Text = "Descripción:";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEliminar.Enabled = false;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(814, 182);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(85, 25);
            this.btnEliminar.TabIndex = 10;
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
            this.btnModificar.Location = new System.Drawing.Point(723, 182);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(85, 25);
            this.btnModificar.TabIndex = 9;
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
            this.btcCerrar.Location = new System.Drawing.Point(905, 182);
            this.btcCerrar.Name = "btcCerrar";
            this.btcCerrar.Size = new System.Drawing.Size(85, 25);
            this.btcCerrar.TabIndex = 11;
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
            this.btnGuardar.Location = new System.Drawing.Point(632, 182);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 25);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "   Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lvwSoporteTecnico
            // 
            this.lvwSoporteTecnico.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwSoporteTecnico.AutoArrange = false;
            this.lvwSoporteTecnico.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwSoporteTecnico.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NroTiquet,
            this.FechaHora,
            this.Descripción,
            this.Prioridad,
            this.Usuario,
            this.EstadoActual});
            this.lvwSoporteTecnico.FullRowSelect = true;
            this.lvwSoporteTecnico.GridLines = true;
            this.lvwSoporteTecnico.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwSoporteTecnico.HideSelection = false;
            this.lvwSoporteTecnico.LargeImageList = this.imageList1;
            this.lvwSoporteTecnico.Location = new System.Drawing.Point(12, 67);
            this.lvwSoporteTecnico.MultiSelect = false;
            this.lvwSoporteTecnico.Name = "lvwSoporteTecnico";
            this.lvwSoporteTecnico.Size = new System.Drawing.Size(996, 408);
            this.lvwSoporteTecnico.SmallImageList = this.imageList1;
            this.lvwSoporteTecnico.TabIndex = 2;
            this.lvwSoporteTecnico.UseCompatibleStateImageBehavior = false;
            this.lvwSoporteTecnico.View = System.Windows.Forms.View.Details;
            this.lvwSoporteTecnico.SelectedIndexChanged += new System.EventHandler(this.lvwSoporteTecnico_SelectedIndexChanged);
            // 
            // NroTiquet
            // 
            this.NroTiquet.Text = "Nro Tiquet";
            this.NroTiquet.Width = 75;
            // 
            // FechaHora
            // 
            this.FechaHora.Text = "Fecha";
            this.FechaHora.Width = 145;
            // 
            // Descripción
            // 
            this.Descripción.Text = "Descripción del Caso";
            this.Descripción.Width = 490;
            // 
            // Prioridad
            // 
            this.Prioridad.Text = "Prioridad";
            this.Prioridad.Width = 90;
            // 
            // Usuario
            // 
            this.Usuario.Text = "Usuario";
            this.Usuario.Width = 65;
            // 
            // EstadoActual
            // 
            this.EstadoActual.Text = "Estado Actual";
            this.EstadoActual.Width = 100;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 100000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmSoporteTecnico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1020, 706);
            this.Controls.Add(this.gpCompraProveedor);
            this.Controls.Add(this.gpoCliente);
            this.Controls.Add(this.lvwSoporteTecnico);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSoporteTecnico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Soporte Ténico";
            this.Load += new System.EventHandler(this.frmSoporteTecnico_Load);
            this.gpCompraProveedor.ResumeLayout(false);
            this.gpCompraProveedor.PerformLayout();
            this.tlsBarArticulo.ResumeLayout(false);
            this.tlsBarArticulo.PerformLayout();
            this.gpoCliente.ResumeLayout(false);
            this.gpoCliente.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox gpCompraProveedor;
        private System.Windows.Forms.ToolStrip tlsBarArticulo;
        private System.Windows.Forms.ToolStripButton tsBtnNuevo;
        private System.Windows.Forms.ToolStripButton tsBtnBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnReporte;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TextBox txtBuscarArticulo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboBuscaTiquetSoporte;
        private System.Windows.Forms.GroupBox gpoCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPrioridad;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dtpFechaFactu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btcCerrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ListView lvwSoporteTecnico;
        private System.Windows.Forms.ColumnHeader NroTiquet;
        private System.Windows.Forms.ColumnHeader FechaHora;
        private System.Windows.Forms.ColumnHeader Descripción;
        private System.Windows.Forms.ColumnHeader Prioridad;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader EstadoActual;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader Usuario;
        private System.Windows.Forms.ComboBox cboEstadoActual;
        private System.Windows.Forms.Timer timer1;
    }
}