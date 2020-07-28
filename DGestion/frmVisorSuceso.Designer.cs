namespace DGestion
{
    partial class frmVisorSuceso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVisorSuceso));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Aplicación", 1, 1);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Sistema");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Seguridad", 2, 2);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Actividades del Sistema", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.lvwSuceso = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EventoSuceso = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Estado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Descripcion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FechaHora = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Usuario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.tvwVisor = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnCerrar = new System.Windows.Forms.Button();
            this.gpCompraProveedor = new System.Windows.Forms.GroupBox();
            this.tlsBarArticulo = new System.Windows.Forms.ToolStrip();
            this.tsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnReporte = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.txtBuscarArticulo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBuscaSuceso = new System.Windows.Forms.ComboBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.gpCompraProveedor.SuspendLayout();
            this.tlsBarArticulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwSuceso
            // 
            this.lvwSuceso.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwSuceso.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.EventoSuceso,
            this.Estado,
            this.Descripcion,
            this.FechaHora,
            this.Usuario});
            this.lvwSuceso.FullRowSelect = true;
            this.lvwSuceso.GridLines = true;
            this.lvwSuceso.HideSelection = false;
            this.lvwSuceso.LargeImageList = this.imageList2;
            this.lvwSuceso.Location = new System.Drawing.Point(251, 68);
            this.lvwSuceso.MultiSelect = false;
            this.lvwSuceso.Name = "lvwSuceso";
            this.lvwSuceso.Size = new System.Drawing.Size(701, 621);
            this.lvwSuceso.SmallImageList = this.imageList2;
            this.lvwSuceso.TabIndex = 0;
            this.lvwSuceso.UseCompatibleStateImageBehavior = false;
            this.lvwSuceso.View = System.Windows.Forms.View.Details;
            // 
            // Id
            // 
            this.Id.Text = "-";
            this.Id.Width = 25;
            // 
            // EventoSuceso
            // 
            this.EventoSuceso.Text = "Evento Ejecutor";
            this.EventoSuceso.Width = 275;
            // 
            // Estado
            // 
            this.Estado.Text = "Estado";
            this.Estado.Width = 0;
            // 
            // Descripcion
            // 
            this.Descripcion.Text = "Descripción";
            this.Descripcion.Width = 135;
            // 
            // FechaHora
            // 
            this.FechaHora.Text = "Fecha y Hora";
            this.FechaHora.Width = 150;
            // 
            // Usuario
            // 
            this.Usuario.Text = "Usuario";
            this.Usuario.Width = 80;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "man key.ico");
            this.imageList2.Images.SetKeyName(1, "insect blue.ico");
            this.imageList2.Images.SetKeyName(2, "insect.ico");
            this.imageList2.Images.SetKeyName(3, "error.ico");
            this.imageList2.Images.SetKeyName(4, "Sign Error.ico");
            this.imageList2.Images.SetKeyName(5, "Sign Info.ico");
            // 
            // tvwVisor
            // 
            this.tvwVisor.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tvwVisor.FullRowSelect = true;
            this.tvwVisor.ImageIndex = 0;
            this.tvwVisor.ImageList = this.imageList1;
            this.tvwVisor.Indent = 38;
            this.tvwVisor.ItemHeight = 35;
            this.tvwVisor.Location = new System.Drawing.Point(12, 68);
            this.tvwVisor.Name = "tvwVisor";
            treeNode1.ImageIndex = 1;
            treeNode1.Name = "Aplicación";
            treeNode1.SelectedImageIndex = 1;
            treeNode1.Text = "Aplicación";
            treeNode2.ImageIndex = 3;
            treeNode2.Name = "Sistema";
            treeNode2.SelectedImageKey = "App my mac.ico";
            treeNode2.Text = "Sistema";
            treeNode3.ImageIndex = 2;
            treeNode3.Name = "Seguridad";
            treeNode3.SelectedImageIndex = 2;
            treeNode3.Text = "Seguridad";
            treeNode4.ImageKey = "Library.ico";
            treeNode4.Name = "VisorSuceso";
            treeNode4.SelectedImageIndex = 0;
            treeNode4.Text = "Actividades del Sistema";
            this.tvwVisor.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.tvwVisor.SelectedImageIndex = 0;
            this.tvwVisor.Size = new System.Drawing.Size(233, 621);
            this.tvwVisor.TabIndex = 1;
            this.tvwVisor.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwVisor_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "App database.ico");
            this.imageList1.Images.SetKeyName(1, "folder.ico");
            this.imageList1.Images.SetKeyName(2, "lock.ico");
            this.imageList1.Images.SetKeyName(3, "App my mac.ico");
            this.imageList1.Images.SetKeyName(4, "App konsole.ico");
            this.imageList1.Images.SetKeyName(5, "Action db status.ico");
            this.imageList1.Images.SetKeyName(6, "App devices.ico");
            this.imageList1.Images.SetKeyName(7, "Case.ico");
            this.imageList1.Images.SetKeyName(8, "database.ico");
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrar.Location = new System.Drawing.Point(867, 695);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(85, 25);
            this.btnCerrar.TabIndex = 28;
            this.btnCerrar.Text = "   Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // gpCompraProveedor
            // 
            this.gpCompraProveedor.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpCompraProveedor.Controls.Add(this.tlsBarArticulo);
            this.gpCompraProveedor.Controls.Add(this.txtBuscarArticulo);
            this.gpCompraProveedor.Controls.Add(this.label1);
            this.gpCompraProveedor.Controls.Add(this.cboBuscaSuceso);
            this.gpCompraProveedor.Location = new System.Drawing.Point(12, 12);
            this.gpCompraProveedor.Name = "gpCompraProveedor";
            this.gpCompraProveedor.Size = new System.Drawing.Size(940, 50);
            this.gpCompraProveedor.TabIndex = 33;
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
            this.tsBtnBuscar,
            this.toolStripSeparator2,
            this.tsBtnReporte,
            this.toolStripSeparator1,
            this.tsBtnSalir,
            this.toolStripSeparator3});
            this.tlsBarArticulo.Location = new System.Drawing.Point(825, 12);
            this.tlsBarArticulo.Name = "tlsBarArticulo";
            this.tlsBarArticulo.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tlsBarArticulo.Size = new System.Drawing.Size(105, 31);
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
            // cboBuscaSuceso
            // 
            this.cboBuscaSuceso.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cboBuscaSuceso.FormattingEnabled = true;
            this.cboBuscaSuceso.Items.AddRange(new object[] {
            "Fecha",
            "Usuario",
            "Estado"});
            this.cboBuscaSuceso.Location = new System.Drawing.Point(78, 19);
            this.cboBuscaSuceso.Name = "cboBuscaSuceso";
            this.cboBuscaSuceso.Size = new System.Drawing.Size(120, 21);
            this.cboBuscaSuceso.TabIndex = 0;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEliminar.Enabled = false;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(776, 695);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(85, 25);
            this.btnEliminar.TabIndex = 35;
            this.btnEliminar.Text = "   Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            // 
            // frmVisorSuceso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(964, 729);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.gpCompraProveedor);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.tvwVisor);
            this.Controls.Add(this.lvwSuceso);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmVisorSuceso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Actividades del Sistema";
            this.Load += new System.EventHandler(this.frmVisorSuceso_Load);
            this.gpCompraProveedor.ResumeLayout(false);
            this.gpCompraProveedor.PerformLayout();
            this.tlsBarArticulo.ResumeLayout(false);
            this.tlsBarArticulo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwSuceso;
        private System.Windows.Forms.TreeView tvwVisor;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader EventoSuceso;
        private System.Windows.Forms.ColumnHeader FechaHora;
        private System.Windows.Forms.ColumnHeader Usuario;
        private System.Windows.Forms.ColumnHeader Estado;
        private System.Windows.Forms.GroupBox gpCompraProveedor;
        private System.Windows.Forms.ToolStrip tlsBarArticulo;
        private System.Windows.Forms.ToolStripButton tsBtnBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnReporte;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TextBox txtBuscarArticulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBuscaSuceso;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.ColumnHeader Descripcion;
    }
}