namespace DGestion
{
    partial class frmMultiCentro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMultiCentro));
            this.GpbEmpresa = new System.Windows.Forms.GroupBox();
            this.gbEmpresa = new System.Windows.Forms.GroupBox();
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
            this.label5 = new System.Windows.Forms.Label();
            this.cboFormaPago = new System.Windows.Forms.ComboBox();
            this.gpoCliente = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lvwEmpresaLogin = new System.Windows.Forms.ListView();
            this.Centro = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Docimilio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Empresa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.cboPtoVta = new System.Windows.Forms.ComboBox();
            this.txtCodFamiliaArt = new System.Windows.Forms.TextBox();
            this.btnEmpresa = new System.Windows.Forms.Button();
            this.cmbDescripcionFamiliaArt = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTelComercial = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.GpbEmpresa.SuspendLayout();
            this.gbEmpresa.SuspendLayout();
            this.tlsBarArticulo.SuspendLayout();
            this.gpoCliente.SuspendLayout();
            this.SuspendLayout();
            // 
            // GpbEmpresa
            // 
            this.GpbEmpresa.Controls.Add(this.gbEmpresa);
            this.GpbEmpresa.Controls.Add(this.gpoCliente);
            this.GpbEmpresa.Controls.Add(this.lvwEmpresaLogin);
            this.GpbEmpresa.Location = new System.Drawing.Point(12, 12);
            this.GpbEmpresa.Name = "GpbEmpresa";
            this.GpbEmpresa.Size = new System.Drawing.Size(602, 517);
            this.GpbEmpresa.TabIndex = 64;
            this.GpbEmpresa.TabStop = false;
            this.GpbEmpresa.Text = "Empresa";
            // 
            // gbEmpresa
            // 
            this.gbEmpresa.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gbEmpresa.Controls.Add(this.tlsBarArticulo);
            this.gbEmpresa.Controls.Add(this.txtBuscarArticulo);
            this.gbEmpresa.Controls.Add(this.label5);
            this.gbEmpresa.Controls.Add(this.cboFormaPago);
            this.gbEmpresa.Location = new System.Drawing.Point(6, 19);
            this.gbEmpresa.Name = "gbEmpresa";
            this.gbEmpresa.Size = new System.Drawing.Size(583, 50);
            this.gbEmpresa.TabIndex = 65;
            this.gbEmpresa.TabStop = false;
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
            this.tlsBarArticulo.Location = new System.Drawing.Point(420, 16);
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
            this.tsBtnNuevo.Text = "Nueva Forma de Pago";
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
            this.tsBtnSalir.Text = "Salir de Forma de Pago";
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
            this.txtBuscarArticulo.Location = new System.Drawing.Point(217, 19);
            this.txtBuscarArticulo.Name = "txtBuscarArticulo";
            this.txtBuscarArticulo.Size = new System.Drawing.Size(150, 20);
            this.txtBuscarArticulo.TabIndex = 1;
            this.txtBuscarArticulo.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Buscar por:";
            this.label5.Visible = false;
            // 
            // cboFormaPago
            // 
            this.cboFormaPago.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cboFormaPago.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboFormaPago.FormattingEnabled = true;
            this.cboFormaPago.Items.AddRange(new object[] {
            "ID Empresa",
            "Razón Social"});
            this.cboFormaPago.Location = new System.Drawing.Point(91, 19);
            this.cboFormaPago.Name = "cboFormaPago";
            this.cboFormaPago.Size = new System.Drawing.Size(120, 21);
            this.cboFormaPago.TabIndex = 0;
            this.cboFormaPago.Visible = false;
            // 
            // gpoCliente
            // 
            this.gpoCliente.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpoCliente.Controls.Add(this.txtTelComercial);
            this.gpoCliente.Controls.Add(this.label6);
            this.gpoCliente.Controls.Add(this.txtCodFamiliaArt);
            this.gpoCliente.Controls.Add(this.btnEmpresa);
            this.gpoCliente.Controls.Add(this.cmbDescripcionFamiliaArt);
            this.gpoCliente.Controls.Add(this.label3);
            this.gpoCliente.Controls.Add(this.label1);
            this.gpoCliente.Controls.Add(this.cboPtoVta);
            this.gpoCliente.Controls.Add(this.textBox1);
            this.gpoCliente.Controls.Add(this.label2);
            this.gpoCliente.Controls.Add(this.btnActualizar);
            this.gpoCliente.Controls.Add(this.btnCerrar);
            this.gpoCliente.Controls.Add(this.btnGuardar);
            this.gpoCliente.Location = new System.Drawing.Point(6, 359);
            this.gpoCliente.Name = "gpoCliente";
            this.gpoCliente.Size = new System.Drawing.Size(583, 152);
            this.gpoCliente.TabIndex = 64;
            this.gpoCliente.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.textBox1.Location = new System.Drawing.Point(99, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(407, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "Domicilio Fiscal:";
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActualizar.Location = new System.Drawing.Point(394, 116);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(85, 25);
            this.btnActualizar.TabIndex = 54;
            this.btnActualizar.Text = "   Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrar.Location = new System.Drawing.Point(485, 116);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(85, 25);
            this.btnCerrar.TabIndex = 53;
            this.btnCerrar.Text = "  Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(303, 116);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 25);
            this.btnGuardar.TabIndex = 51;
            this.btnGuardar.Text = "   Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // lvwEmpresaLogin
            // 
            this.lvwEmpresaLogin.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwEmpresaLogin.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwEmpresaLogin.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Centro,
            this.Docimilio,
            this.Empresa});
            this.lvwEmpresaLogin.FullRowSelect = true;
            this.lvwEmpresaLogin.GridLines = true;
            this.lvwEmpresaLogin.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwEmpresaLogin.HideSelection = false;
            this.lvwEmpresaLogin.Location = new System.Drawing.Point(6, 75);
            this.lvwEmpresaLogin.MultiSelect = false;
            this.lvwEmpresaLogin.Name = "lvwEmpresaLogin";
            this.lvwEmpresaLogin.Size = new System.Drawing.Size(583, 278);
            this.lvwEmpresaLogin.TabIndex = 63;
            this.lvwEmpresaLogin.UseCompatibleStateImageBehavior = false;
            this.lvwEmpresaLogin.View = System.Windows.Forms.View.Details;
            // 
            // Centro
            // 
            this.Centro.Text = "Centro";
            this.Centro.Width = 95;
            // 
            // Docimilio
            // 
            this.Docimilio.Text = "Domicilio";
            this.Docimilio.Width = 300;
            // 
            // Empresa
            // 
            this.Empresa.Text = "Empresa";
            this.Empresa.Width = 153;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(371, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Pto.  Vta.:";
            // 
            // cboPtoVta
            // 
            this.cboPtoVta.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboPtoVta.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboPtoVta.FormattingEnabled = true;
            this.cboPtoVta.Items.AddRange(new object[] {
            "0001",
            "0002",
            "0003",
            "0004",
            "0005"});
            this.cboPtoVta.Location = new System.Drawing.Point(431, 20);
            this.cboPtoVta.Name = "cboPtoVta";
            this.cboPtoVta.Size = new System.Drawing.Size(75, 21);
            this.cboPtoVta.TabIndex = 65;
            this.cboPtoVta.Text = "0001";
            // 
            // txtCodFamiliaArt
            // 
            this.txtCodFamiliaArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodFamiliaArt.Location = new System.Drawing.Point(99, 20);
            this.txtCodFamiliaArt.Name = "txtCodFamiliaArt";
            this.txtCodFamiliaArt.Size = new System.Drawing.Size(50, 20);
            this.txtCodFamiliaArt.TabIndex = 67;
            // 
            // btnEmpresa
            // 
            this.btnEmpresa.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEmpresa.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnEmpresa.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEmpresa.Image = ((System.Drawing.Image)(resources.GetObject("btnEmpresa.Image")));
            this.btnEmpresa.Location = new System.Drawing.Point(335, 17);
            this.btnEmpresa.Name = "btnEmpresa";
            this.btnEmpresa.Size = new System.Drawing.Size(30, 25);
            this.btnEmpresa.TabIndex = 69;
            this.btnEmpresa.UseVisualStyleBackColor = false;
            // 
            // cmbDescripcionFamiliaArt
            // 
            this.cmbDescripcionFamiliaArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cmbDescripcionFamiliaArt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbDescripcionFamiliaArt.FormattingEnabled = true;
            this.cmbDescripcionFamiliaArt.Location = new System.Drawing.Point(155, 19);
            this.cmbDescripcionFamiliaArt.Name = "cmbDescripcionFamiliaArt";
            this.cmbDescripcionFamiliaArt.Size = new System.Drawing.Size(174, 21);
            this.cmbDescripcionFamiliaArt.TabIndex = 68;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 70;
            this.label3.Text = "Código Empresa:";
            // 
            // txtTelComercial
            // 
            this.txtTelComercial.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTelComercial.Location = new System.Drawing.Point(99, 72);
            this.txtTelComercial.Name = "txtTelComercial";
            this.txtTelComercial.Size = new System.Drawing.Size(112, 20);
            this.txtTelComercial.TabIndex = 72;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(65, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 74;
            this.label6.Text = "Tel.:";
            // 
            // frmMultiCentro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(623, 535);
            this.Controls.Add(this.GpbEmpresa);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMultiCentro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Punto de Venta";
            this.GpbEmpresa.ResumeLayout(false);
            this.gbEmpresa.ResumeLayout(false);
            this.gbEmpresa.PerformLayout();
            this.tlsBarArticulo.ResumeLayout(false);
            this.tlsBarArticulo.PerformLayout();
            this.gpoCliente.ResumeLayout(false);
            this.gpoCliente.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GpbEmpresa;
        private System.Windows.Forms.GroupBox gbEmpresa;
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboFormaPago;
        private System.Windows.Forms.GroupBox gpoCliente;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ListView lvwEmpresaLogin;
        private System.Windows.Forms.ColumnHeader Centro;
        private System.Windows.Forms.ColumnHeader Docimilio;
        private System.Windows.Forms.ColumnHeader Empresa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPtoVta;
        private System.Windows.Forms.TextBox txtCodFamiliaArt;
        private System.Windows.Forms.Button btnEmpresa;
        private System.Windows.Forms.ComboBox cmbDescripcionFamiliaArt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTelComercial;
        private System.Windows.Forms.Label label6;
    }
}