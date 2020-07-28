namespace DGestion
{
    partial class frmPermisos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPermisos));
            this.gpbModuloControl = new System.Windows.Forms.GroupBox();
            this.gpbDetallePermisoUsuario = new System.Windows.Forms.GroupBox();
            this.btnPermisoPorModulo = new System.Windows.Forms.Button();
            this.btnPermisoGlobalUsusario = new System.Windows.Forms.Button();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.lvwDetalleControl = new System.Windows.Forms.ListView();
            this.Usuario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Nombres = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdModulo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdControl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Control = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Estado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdPersonal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAceptarDetalle = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboProcedimientoControl = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboModuloControl = new System.Windows.Forms.ComboBox();
            this.lblPermiso = new System.Windows.Forms.Label();
            this.cboUsuarioPermiso = new System.Windows.Forms.ComboBox();
            this.gpoCliente = new System.Windows.Forms.GroupBox();
            this.btnDesactivarPermiso = new System.Windows.Forms.Button();
            this.btcCerrar = new System.Windows.Forms.Button();
            this.btnActivarPermiso = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gpbModuloControl.SuspendLayout();
            this.gpbDetallePermisoUsuario.SuspendLayout();
            this.gpoCliente.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbModuloControl
            // 
            this.gpbModuloControl.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpbModuloControl.Controls.Add(this.gpbDetallePermisoUsuario);
            this.gpbModuloControl.Controls.Add(this.label3);
            this.gpbModuloControl.Controls.Add(this.cboProcedimientoControl);
            this.gpbModuloControl.Controls.Add(this.label2);
            this.gpbModuloControl.Controls.Add(this.cboModuloControl);
            this.gpbModuloControl.Controls.Add(this.lblPermiso);
            this.gpbModuloControl.Controls.Add(this.cboUsuarioPermiso);
            this.gpbModuloControl.Location = new System.Drawing.Point(12, 12);
            this.gpbModuloControl.Name = "gpbModuloControl";
            this.gpbModuloControl.Size = new System.Drawing.Size(520, 427);
            this.gpbModuloControl.TabIndex = 5;
            this.gpbModuloControl.TabStop = false;
            this.gpbModuloControl.Text = "Área de Control de Accesos";
            // 
            // gpbDetallePermisoUsuario
            // 
            this.gpbDetallePermisoUsuario.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpbDetallePermisoUsuario.Controls.Add(this.btnPermisoPorModulo);
            this.gpbDetallePermisoUsuario.Controls.Add(this.btnPermisoGlobalUsusario);
            this.gpbDetallePermisoUsuario.Controls.Add(this.btnConsultar);
            this.gpbDetallePermisoUsuario.Controls.Add(this.lvwDetalleControl);
            this.gpbDetallePermisoUsuario.Controls.Add(this.btnAceptarDetalle);
            this.gpbDetallePermisoUsuario.Location = new System.Drawing.Point(6, 113);
            this.gpbDetallePermisoUsuario.Name = "gpbDetallePermisoUsuario";
            this.gpbDetallePermisoUsuario.Size = new System.Drawing.Size(508, 308);
            this.gpbDetallePermisoUsuario.TabIndex = 138;
            this.gpbDetallePermisoUsuario.TabStop = false;
            this.gpbDetallePermisoUsuario.Text = "Permisos Efectivos del Usuario";
            // 
            // btnPermisoPorModulo
            // 
            this.btnPermisoPorModulo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPermisoPorModulo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPermisoPorModulo.Image = ((System.Drawing.Image)(resources.GetObject("btnPermisoPorModulo.Image")));
            this.btnPermisoPorModulo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPermisoPorModulo.Location = new System.Drawing.Point(156, 275);
            this.btnPermisoPorModulo.Name = "btnPermisoPorModulo";
            this.btnPermisoPorModulo.Size = new System.Drawing.Size(136, 25);
            this.btnPermisoPorModulo.TabIndex = 33;
            this.btnPermisoPorModulo.Text = "    Permisos por Módulo";
            this.btnPermisoPorModulo.UseVisualStyleBackColor = false;
            this.btnPermisoPorModulo.Click += new System.EventHandler(this.btnPermisoPorModulo_Click);
            // 
            // btnPermisoGlobalUsusario
            // 
            this.btnPermisoGlobalUsusario.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPermisoGlobalUsusario.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPermisoGlobalUsusario.Image = ((System.Drawing.Image)(resources.GetObject("btnPermisoGlobalUsusario.Image")));
            this.btnPermisoGlobalUsusario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPermisoGlobalUsusario.Location = new System.Drawing.Point(350, 275);
            this.btnPermisoGlobalUsusario.Name = "btnPermisoGlobalUsusario";
            this.btnPermisoGlobalUsusario.Size = new System.Drawing.Size(152, 25);
            this.btnPermisoGlobalUsusario.TabIndex = 32;
            this.btnPermisoGlobalUsusario.Text = "    Permisos Global Usuario";
            this.btnPermisoGlobalUsusario.UseVisualStyleBackColor = false;
            this.btnPermisoGlobalUsusario.Click += new System.EventHandler(this.btnPermisoGlobalUsusario_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConsultar.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultar.Image")));
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.Location = new System.Drawing.Point(6, 275);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(144, 25);
            this.btnConsultar.TabIndex = 31;
            this.btnConsultar.Text = "    Permiso Seleccionado";
            this.btnConsultar.UseVisualStyleBackColor = false;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // lvwDetalleControl
            // 
            this.lvwDetalleControl.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwDetalleControl.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lvwDetalleControl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Usuario,
            this.Nombres,
            this.IdModulo,
            this.IdControl,
            this.Control,
            this.Estado,
            this.IdPersonal});
            this.lvwDetalleControl.FullRowSelect = true;
            this.lvwDetalleControl.GridLines = true;
            this.lvwDetalleControl.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwDetalleControl.HideSelection = false;
            this.lvwDetalleControl.Location = new System.Drawing.Point(6, 21);
            this.lvwDetalleControl.Name = "lvwDetalleControl";
            this.lvwDetalleControl.Size = new System.Drawing.Size(496, 248);
            this.lvwDetalleControl.TabIndex = 30;
            this.lvwDetalleControl.UseCompatibleStateImageBehavior = false;
            this.lvwDetalleControl.View = System.Windows.Forms.View.Details;
            // 
            // Usuario
            // 
            this.Usuario.Text = "Usuario";
            this.Usuario.Width = 75;
            // 
            // Nombres
            // 
            this.Nombres.Text = "Nombres";
            this.Nombres.Width = 140;
            // 
            // IdModulo
            // 
            this.IdModulo.Text = "IdModulo";
            this.IdModulo.Width = 0;
            // 
            // IdControl
            // 
            this.IdControl.Text = "IdControl";
            this.IdControl.Width = 0;
            // 
            // Control
            // 
            this.Control.Text = "Procedimiento del Módulo";
            this.Control.Width = 200;
            // 
            // Estado
            // 
            this.Estado.Text = "Estado";
            // 
            // IdPersonal
            // 
            this.IdPersonal.Text = "IdPersonal";
            this.IdPersonal.Width = 0;
            // 
            // btnAceptarDetalle
            // 
            this.btnAceptarDetalle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAceptarDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAceptarDetalle.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptarDetalle.Image")));
            this.btnAceptarDetalle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptarDetalle.Location = new System.Drawing.Point(433, 560);
            this.btnAceptarDetalle.Name = "btnAceptarDetalle";
            this.btnAceptarDetalle.Size = new System.Drawing.Size(85, 25);
            this.btnAceptarDetalle.TabIndex = 29;
            this.btnAceptarDetalle.TabStop = false;
            this.btnAceptarDetalle.Text = "   Cerrar";
            this.btnAceptarDetalle.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Procedimiento del Módulo:";
            // 
            // cboProcedimientoControl
            // 
            this.cboProcedimientoControl.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboProcedimientoControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProcedimientoControl.FormattingEnabled = true;
            this.cboProcedimientoControl.Location = new System.Drawing.Point(199, 77);
            this.cboProcedimientoControl.Name = "cboProcedimientoControl";
            this.cboProcedimientoControl.Size = new System.Drawing.Size(191, 21);
            this.cboProcedimientoControl.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Módulo del Sistema:";
            // 
            // cboModuloControl
            // 
            this.cboModuloControl.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboModuloControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModuloControl.FormattingEnabled = true;
            this.cboModuloControl.Location = new System.Drawing.Point(199, 50);
            this.cboModuloControl.Name = "cboModuloControl";
            this.cboModuloControl.Size = new System.Drawing.Size(191, 21);
            this.cboModuloControl.TabIndex = 1;
            this.cboModuloControl.SelectedIndexChanged += new System.EventHandler(this.cboModuloControl_SelectedIndexChanged);
            // 
            // lblPermiso
            // 
            this.lblPermiso.AutoSize = true;
            this.lblPermiso.Location = new System.Drawing.Point(90, 26);
            this.lblPermiso.Name = "lblPermiso";
            this.lblPermiso.Size = new System.Drawing.Size(103, 13);
            this.lblPermiso.TabIndex = 6;
            this.lblPermiso.Text = "Usuario del Sistema:";
            // 
            // cboUsuarioPermiso
            // 
            this.cboUsuarioPermiso.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboUsuarioPermiso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsuarioPermiso.FormattingEnabled = true;
            this.cboUsuarioPermiso.Location = new System.Drawing.Point(199, 23);
            this.cboUsuarioPermiso.Name = "cboUsuarioPermiso";
            this.cboUsuarioPermiso.Size = new System.Drawing.Size(191, 21);
            this.cboUsuarioPermiso.TabIndex = 3;
            this.cboUsuarioPermiso.SelectedIndexChanged += new System.EventHandler(this.cboUsuario_SelectedIndexChanged);
            // 
            // gpoCliente
            // 
            this.gpoCliente.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpoCliente.Controls.Add(this.btnDesactivarPermiso);
            this.gpoCliente.Controls.Add(this.btcCerrar);
            this.gpoCliente.Controls.Add(this.btnActivarPermiso);
            this.gpoCliente.Location = new System.Drawing.Point(12, 445);
            this.gpoCliente.Name = "gpoCliente";
            this.gpoCliente.Size = new System.Drawing.Size(520, 60);
            this.gpoCliente.TabIndex = 28;
            this.gpoCliente.TabStop = false;
            // 
            // btnDesactivarPermiso
            // 
            this.btnDesactivarPermiso.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnDesactivarPermiso.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDesactivarPermiso.Image = ((System.Drawing.Image)(resources.GetObject("btnDesactivarPermiso.Image")));
            this.btnDesactivarPermiso.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDesactivarPermiso.Location = new System.Drawing.Point(108, 19);
            this.btnDesactivarPermiso.Name = "btnDesactivarPermiso";
            this.btnDesactivarPermiso.Size = new System.Drawing.Size(85, 25);
            this.btnDesactivarPermiso.TabIndex = 5;
            this.btnDesactivarPermiso.Text = "   Inactivo";
            this.btnDesactivarPermiso.UseVisualStyleBackColor = false;
            this.btnDesactivarPermiso.Click += new System.EventHandler(this.btnDesactivarPermiso_Click);
            // 
            // btcCerrar
            // 
            this.btcCerrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btcCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btcCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btcCerrar.Image")));
            this.btcCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btcCerrar.Location = new System.Drawing.Point(429, 19);
            this.btcCerrar.Name = "btcCerrar";
            this.btcCerrar.Size = new System.Drawing.Size(85, 25);
            this.btcCerrar.TabIndex = 6;
            this.btcCerrar.Text = "   Cerrar";
            this.btcCerrar.UseVisualStyleBackColor = false;
            this.btcCerrar.Click += new System.EventHandler(this.btcCerrar_Click);
            // 
            // btnActivarPermiso
            // 
            this.btnActivarPermiso.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnActivarPermiso.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActivarPermiso.Image = ((System.Drawing.Image)(resources.GetObject("btnActivarPermiso.Image")));
            this.btnActivarPermiso.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActivarPermiso.Location = new System.Drawing.Point(17, 19);
            this.btnActivarPermiso.Name = "btnActivarPermiso";
            this.btnActivarPermiso.Size = new System.Drawing.Size(85, 25);
            this.btnActivarPermiso.TabIndex = 4;
            this.btnActivarPermiso.Text = "  Activo";
            this.btnActivarPermiso.UseVisualStyleBackColor = false;
            this.btnActivarPermiso.Click += new System.EventHandler(this.btnActivarPermiso_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "lock on.ico");
            this.imageList1.Images.SetKeyName(1, "lock off.ico");
            // 
            // frmPermisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(544, 516);
            this.Controls.Add(this.gpoCliente);
            this.Controls.Add(this.gpbModuloControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPermisos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administración de Permisos del Sistema";
            this.Load += new System.EventHandler(this.frmPermisos_Load);
            this.gpbModuloControl.ResumeLayout(false);
            this.gpbModuloControl.PerformLayout();
            this.gpbDetallePermisoUsuario.ResumeLayout(false);
            this.gpoCliente.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbModuloControl;
        private System.Windows.Forms.Label lblPermiso;
        private System.Windows.Forms.ComboBox cboUsuarioPermiso;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboProcedimientoControl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboModuloControl;
        private System.Windows.Forms.GroupBox gpoCliente;
        private System.Windows.Forms.Button btcCerrar;
        private System.Windows.Forms.Button btnDesactivarPermiso;
        private System.Windows.Forms.Button btnActivarPermiso;
        private System.Windows.Forms.GroupBox gpbDetallePermisoUsuario;
        private System.Windows.Forms.ListView lvwDetalleControl;
        private System.Windows.Forms.ColumnHeader Usuario;
        private System.Windows.Forms.ColumnHeader Nombres;
        private System.Windows.Forms.ColumnHeader IdModulo;
        private System.Windows.Forms.ColumnHeader IdControl;
        private System.Windows.Forms.ColumnHeader Control;
        private System.Windows.Forms.ColumnHeader IdPersonal;
        private System.Windows.Forms.Button btnAceptarDetalle;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader Estado;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Button btnPermisoGlobalUsusario;
        private System.Windows.Forms.Button btnPermisoPorModulo;
    }
}