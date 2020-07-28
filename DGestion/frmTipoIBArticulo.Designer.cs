namespace DGestion
{
    partial class frmTipoIBArticulo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTipoIBArticulo));
            this.gpoCliente = new System.Windows.Forms.GroupBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsBtnModificar = new System.Windows.Forms.ToolStripButton();
            this.tsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.txtConsulta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBuscar = new System.Windows.Forms.ComboBox();
            this.gridTipoArticulo = new System.Windows.Forms.DataGridView();
            this.btnSalir = new System.Windows.Forms.Button();
            this.gpoCliente.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTipoArticulo)).BeginInit();
            this.SuspendLayout();
            // 
            // gpoCliente
            // 
            this.gpoCliente.Controls.Add(this.btnEliminar);
            this.gpoCliente.Controls.Add(this.btnModificar);
            this.gpoCliente.Controls.Add(this.txtCodigo);
            this.gpoCliente.Controls.Add(this.label3);
            this.gpoCliente.Controls.Add(this.btnCerrar);
            this.gpoCliente.Controls.Add(this.btnGuardar);
            this.gpoCliente.Controls.Add(this.txtDescripcion);
            this.gpoCliente.Controls.Add(this.label2);
            this.gpoCliente.Location = new System.Drawing.Point(12, 316);
            this.gpoCliente.Name = "gpoCliente";
            this.gpoCliente.Size = new System.Drawing.Size(627, 101);
            this.gpoCliente.TabIndex = 43;
            this.gpoCliente.TabStop = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(445, 70);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(85, 25);
            this.btnEliminar.TabIndex = 26;
            this.btnEliminar.Text = "   Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.Location = new System.Drawing.Point(354, 70);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(85, 25);
            this.btnModificar.TabIndex = 8;
            this.btnModificar.Text = "   Actualizar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtCodigo.Location = new System.Drawing.Point(76, 18);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(70, 20);
            this.txtCodigo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Código:";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrar.Location = new System.Drawing.Point(536, 70);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(85, 25);
            this.btnCerrar.TabIndex = 9;
            this.btnCerrar.Text = "   Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(263, 70);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 25);
            this.btnGuardar.TabIndex = 7;
            this.btnGuardar.Text = "   Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(76, 44);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(545, 20);
            this.txtDescripcion.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Descripción:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.txtConsulta);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboBuscar);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(627, 49);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnNuevo,
            this.tsBtnModificar,
            this.tsBtnBuscar,
            this.toolStripSeparator3,
            this.tsBtnSalir,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(498, 12);
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
            this.tsBtnNuevo.Text = "Nuevo";
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
            this.txtConsulta.Location = new System.Drawing.Point(202, 16);
            this.txtConsulta.Name = "txtConsulta";
            this.txtConsulta.Size = new System.Drawing.Size(150, 20);
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
            // cboBuscar
            // 
            this.cboBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBuscar.FormattingEnabled = true;
            this.cboBuscar.Items.AddRange(new object[] {
            "Cód. I.B.",
            "Descripción"});
            this.cboBuscar.Location = new System.Drawing.Point(76, 16);
            this.cboBuscar.Name = "cboBuscar";
            this.cboBuscar.Size = new System.Drawing.Size(120, 21);
            this.cboBuscar.TabIndex = 0;
            // 
            // gridTipoArticulo
            // 
            this.gridTipoArticulo.AllowUserToAddRows = false;
            this.gridTipoArticulo.AllowUserToDeleteRows = false;
            this.gridTipoArticulo.AllowUserToOrderColumns = true;
            this.gridTipoArticulo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridTipoArticulo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridTipoArticulo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.gridTipoArticulo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTipoArticulo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.gridTipoArticulo.Location = new System.Drawing.Point(12, 67);
            this.gridTipoArticulo.MultiSelect = false;
            this.gridTipoArticulo.Name = "gridTipoArticulo";
            this.gridTipoArticulo.ReadOnly = true;
            this.gridTipoArticulo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTipoArticulo.Size = new System.Drawing.Size(626, 243);
            this.gridTipoArticulo.StandardTab = true;
            this.gridTipoArticulo.TabIndex = 41;
            this.gridTipoArticulo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTipoArticulo_CellContentClick);
            this.gridTipoArticulo.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridTipoArticulo_CellMouseDoubleClick);
            this.gridTipoArticulo.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridTipoArticulo_CellMouseDown);
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(12, 300);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(10, 10);
            this.btnSalir.TabIndex = 49;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmTipoArticulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(649, 426);
            this.Controls.Add(this.gridTipoArticulo);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.gpoCliente);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmTipoArticulo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso Bruto";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTipoArticulo_FormClosed);
            this.Load += new System.EventHandler(this.frmTipoArticulo_Load);
            this.gpoCliente.ResumeLayout(false);
            this.gpoCliente.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTipoArticulo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpoCliente;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnNuevo;
        private System.Windows.Forms.ToolStripButton tsBtnModificar;
        private System.Windows.Forms.ToolStripButton tsBtnBuscar;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TextBox txtConsulta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBuscar;
        private System.Windows.Forms.DataGridView gridTipoArticulo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Button btnSalir;

    }
}