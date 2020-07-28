namespace DGestion
{
    partial class frmSubTipoPago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSubTipoPago));
            this.lvwSubtipoPago = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Descripción = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CodTipoPago = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsModificarBanco = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.gpoCliente = new System.Windows.Forms.GroupBox();
            this.btnModificar = new System.Windows.Forms.Button();
            this.txtCodTipoPago = new System.Windows.Forms.TextBox();
            this.btnTipoPago = new System.Windows.Forms.Button();
            this.cboTipoPago = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btcCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gpoCliente.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwSubtipoPago
            // 
            this.lvwSubtipoPago.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwSubtipoPago.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.Descripción,
            this.CodTipoPago});
            this.lvwSubtipoPago.FullRowSelect = true;
            this.lvwSubtipoPago.GridLines = true;
            this.lvwSubtipoPago.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwSubtipoPago.HideSelection = false;
            this.lvwSubtipoPago.Location = new System.Drawing.Point(12, 67);
            this.lvwSubtipoPago.Name = "lvwSubtipoPago";
            this.lvwSubtipoPago.Size = new System.Drawing.Size(383, 186);
            this.lvwSubtipoPago.TabIndex = 50;
            this.lvwSubtipoPago.UseCompatibleStateImageBehavior = false;
            this.lvwSubtipoPago.View = System.Windows.Forms.View.Details;
            this.lvwSubtipoPago.SelectedIndexChanged += new System.EventHandler(this.lvwSubtipoPago_SelectedIndexChanged);
            this.lvwSubtipoPago.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvwSubtipoPago_KeyPress);
            this.lvwSubtipoPago.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwSubtipoPago_MouseDoubleClick);
            // 
            // Id
            // 
            this.Id.Text = "Id";
            this.Id.Width = 25;
            // 
            // Descripción
            // 
            this.Descripción.Text = "Descripción";
            this.Descripción.Width = 300;
            // 
            // CodTipoPago
            // 
            this.CodTipoPago.Text = "CodTipoPago";
            this.CodTipoPago.Width = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 49);
            this.groupBox1.TabIndex = 49;
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
            this.tsModificarBanco,
            this.toolStripSeparator1,
            this.tsBtnSalir,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(284, 15);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(99, 31);
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
            // tsModificarBanco
            // 
            this.tsModificarBanco.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsModificarBanco.Image = ((System.Drawing.Image)(resources.GetObject("tsModificarBanco.Image")));
            this.tsModificarBanco.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsModificarBanco.Name = "tsModificarBanco";
            this.tsModificarBanco.Size = new System.Drawing.Size(28, 28);
            this.tsModificarBanco.Text = "Modificar Banco";
            this.tsModificarBanco.Click += new System.EventHandler(this.tsModificarBanco_Click);
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
            // gpoCliente
            // 
            this.gpoCliente.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpoCliente.Controls.Add(this.btnModificar);
            this.gpoCliente.Controls.Add(this.txtCodTipoPago);
            this.gpoCliente.Controls.Add(this.btnTipoPago);
            this.gpoCliente.Controls.Add(this.cboTipoPago);
            this.gpoCliente.Controls.Add(this.label6);
            this.gpoCliente.Controls.Add(this.txtDescripcion);
            this.gpoCliente.Controls.Add(this.btcCerrar);
            this.gpoCliente.Controls.Add(this.btnGuardar);
            this.gpoCliente.Controls.Add(this.label2);
            this.gpoCliente.Location = new System.Drawing.Point(12, 259);
            this.gpoCliente.Name = "gpoCliente";
            this.gpoCliente.Size = new System.Drawing.Size(383, 127);
            this.gpoCliente.TabIndex = 48;
            this.gpoCliente.TabStop = false;
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.Location = new System.Drawing.Point(193, 89);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(85, 25);
            this.btnModificar.TabIndex = 42;
            this.btnModificar.Text = "   Actualizar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // txtCodTipoPago
            // 
            this.txtCodTipoPago.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCodTipoPago.Location = new System.Drawing.Point(96, 50);
            this.txtCodTipoPago.Name = "txtCodTipoPago";
            this.txtCodTipoPago.Size = new System.Drawing.Size(70, 20);
            this.txtCodTipoPago.TabIndex = 38;
            this.txtCodTipoPago.TextChanged += new System.EventHandler(this.txtCodTipoPago_TextChanged);
            // 
            // btnTipoPago
            // 
            this.btnTipoPago.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTipoPago.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTipoPago.Image = ((System.Drawing.Image)(resources.GetObject("btnTipoPago.Image")));
            this.btnTipoPago.Location = new System.Drawing.Point(339, 47);
            this.btnTipoPago.Name = "btnTipoPago";
            this.btnTipoPago.Size = new System.Drawing.Size(30, 25);
            this.btnTipoPago.TabIndex = 40;
            this.btnTipoPago.UseVisualStyleBackColor = false;
            this.btnTipoPago.Click += new System.EventHandler(this.btnTipoPago_Click);
            // 
            // cboTipoPago
            // 
            this.cboTipoPago.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboTipoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboTipoPago.FormattingEnabled = true;
            this.cboTipoPago.Location = new System.Drawing.Point(172, 49);
            this.cboTipoPago.Name = "cboTipoPago";
            this.cboTipoPago.Size = new System.Drawing.Size(161, 21);
            this.cboTipoPago.TabIndex = 39;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "Cód. Tipo Pago:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtDescripcion.Location = new System.Drawing.Point(96, 23);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(273, 20);
            this.txtDescripcion.TabIndex = 37;
            // 
            // btcCerrar
            // 
            this.btcCerrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btcCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btcCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btcCerrar.Image")));
            this.btcCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btcCerrar.Location = new System.Drawing.Point(284, 89);
            this.btcCerrar.Name = "btcCerrar";
            this.btcCerrar.Size = new System.Drawing.Size(85, 25);
            this.btcCerrar.TabIndex = 33;
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
            this.btnGuardar.Location = new System.Drawing.Point(102, 89);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 25);
            this.btnGuardar.TabIndex = 31;
            this.btnGuardar.Text = "   Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Descripción:";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Payment.ico");
            // 
            // frmSubTipoPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(404, 396);
            this.Controls.Add(this.lvwSubtipoPago);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpoCliente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSubTipoPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subtipo de Pago";
            this.Load += new System.EventHandler(this.frmSubTipoPago_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gpoCliente.ResumeLayout(false);
            this.gpoCliente.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwSubtipoPago;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader Descripción;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox gpoCliente;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btcCerrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodTipoPago;
        private System.Windows.Forms.Button btnTipoPago;
        private System.Windows.Forms.ComboBox cboTipoPago;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader CodTipoPago;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.ToolStripButton tsModificarBanco;
    }
}