namespace DGestion
{
    partial class frmTipoPersonal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTipoPersonal));
            this.gpoTPersonal = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
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
            this.txtBuscarPersonal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBuscaPersonal = new System.Windows.Forms.ComboBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.gridTipoPersonal = new System.Windows.Forms.DataGridView();
            this.gpoTPersonal.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tlsBarArticulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTipoPersonal)).BeginInit();
            this.SuspendLayout();
            // 
            // gpoTPersonal
            // 
            this.gpoTPersonal.Controls.Add(this.btnCerrar);
            this.gpoTPersonal.Controls.Add(this.btnGuardar);
            this.gpoTPersonal.Controls.Add(this.textBox2);
            this.gpoTPersonal.Controls.Add(this.label2);
            this.gpoTPersonal.Location = new System.Drawing.Point(12, 310);
            this.gpoTPersonal.Name = "gpoTPersonal";
            this.gpoTPersonal.Size = new System.Drawing.Size(570, 91);
            this.gpoTPersonal.TabIndex = 33;
            this.gpoTPersonal.TabStop = false;
            this.gpoTPersonal.Enter += new System.EventHandler(this.gpoTPersonal_Enter);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrar.Location = new System.Drawing.Point(479, 60);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(85, 25);
            this.btnCerrar.TabIndex = 49;
            this.btnCerrar.Text = "     Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(388, 60);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 25);
            this.btnGuardar.TabIndex = 47;
            this.btnGuardar.Text = "   Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(78, 19);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(486, 20);
            this.textBox2.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Descripción:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tlsBarArticulo);
            this.groupBox1.Controls.Add(this.txtBuscarPersonal);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboBuscaPersonal);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 50);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            // 
            // tlsBarArticulo
            // 
            this.tlsBarArticulo.BackColor = System.Drawing.SystemColors.Control;
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
            this.tlsBarArticulo.Location = new System.Drawing.Point(405, 11);
            this.tlsBarArticulo.Name = "tlsBarArticulo";
            this.tlsBarArticulo.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tlsBarArticulo.Size = new System.Drawing.Size(192, 31);
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
            this.tsBtnSalir.Text = "Salir del Módulo Artículos";
            this.tsBtnSalir.Click += new System.EventHandler(this.tsBtnSalir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // txtBuscarPersonal
            // 
            this.txtBuscarPersonal.Location = new System.Drawing.Point(204, 19);
            this.txtBuscarPersonal.Name = "txtBuscarPersonal";
            this.txtBuscarPersonal.Size = new System.Drawing.Size(150, 20);
            this.txtBuscarPersonal.TabIndex = 1;
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
            this.cboBuscaPersonal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBuscaPersonal.FormattingEnabled = true;
            this.cboBuscaPersonal.Items.AddRange(new object[] {
            "Tipo Personal"});
            this.cboBuscaPersonal.Location = new System.Drawing.Point(78, 19);
            this.cboBuscaPersonal.Name = "cboBuscaPersonal";
            this.cboBuscaPersonal.Size = new System.Drawing.Size(120, 21);
            this.cboBuscaPersonal.TabIndex = 0;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(12, 294);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(10, 10);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // gridTipoPersonal
            // 
            this.gridTipoPersonal.AllowUserToAddRows = false;
            this.gridTipoPersonal.AllowUserToDeleteRows = false;
            this.gridTipoPersonal.AllowUserToOrderColumns = true;
            this.gridTipoPersonal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridTipoPersonal.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridTipoPersonal.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.gridTipoPersonal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTipoPersonal.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.gridTipoPersonal.Location = new System.Drawing.Point(12, 68);
            this.gridTipoPersonal.MultiSelect = false;
            this.gridTipoPersonal.Name = "gridTipoPersonal";
            this.gridTipoPersonal.ReadOnly = true;
            this.gridTipoPersonal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTipoPersonal.Size = new System.Drawing.Size(570, 236);
            this.gridTipoPersonal.StandardTab = true;
            this.gridTipoPersonal.TabIndex = 45;
            // 
            // frmTipoPersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(594, 411);
            this.Controls.Add(this.gridTipoPersonal);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpoTPersonal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmTipoPersonal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tipo de Personal";
            this.Load += new System.EventHandler(this.frmTipoPersonal_Load);
            this.gpoTPersonal.ResumeLayout(false);
            this.gpoTPersonal.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tlsBarArticulo.ResumeLayout(false);
            this.tlsBarArticulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTipoPersonal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpoTPersonal;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
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
        private System.Windows.Forms.TextBox txtBuscarPersonal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBuscaPersonal;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridView gridTipoPersonal;
    }
}