namespace DGestion
{
    partial class frmLogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogIn));
            this.gbLogin = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.LogIn = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPasswd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.epUser = new System.Windows.Forms.ErrorProvider(this.components);
            this.epPass = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epPass)).BeginInit();
            this.SuspendLayout();
            // 
            // gbLogin
            // 
            this.gbLogin.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gbLogin.Controls.Add(this.btnCerrar);
            this.gbLogin.Controls.Add(this.LogIn);
            this.gbLogin.Controls.Add(this.label2);
            this.gbLogin.Controls.Add(this.txtPasswd);
            this.gbLogin.Controls.Add(this.label1);
            this.gbLogin.Controls.Add(this.btnAceptar);
            this.gbLogin.Controls.Add(this.txtUsuario);
            this.gbLogin.Location = new System.Drawing.Point(12, 12);
            this.gbLogin.Name = "gbLogin";
            this.gbLogin.Size = new System.Drawing.Size(367, 174);
            this.gbLogin.TabIndex = 6;
            this.gbLogin.TabStop = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.AutoSize = true;
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrar.Location = new System.Drawing.Point(270, 137);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(82, 25);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "   Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // LogIn
            // 
            this.LogIn.Image = global::DGestion.Properties.Resources.Key;
            this.LogIn.Location = new System.Drawing.Point(288, 40);
            this.LogIn.Name = "LogIn";
            this.LogIn.Size = new System.Drawing.Size(64, 64);
            this.LogIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.LogIn.TabIndex = 12;
            this.LogIn.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Contraseña:";
            // 
            // txtPasswd
            // 
            this.txtPasswd.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtPasswd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasswd.Location = new System.Drawing.Point(121, 84);
            this.txtPasswd.Name = "txtPasswd";
            this.txtPasswd.PasswordChar = '*';
            this.txtPasswd.Size = new System.Drawing.Size(161, 20);
            this.txtPasswd.TabIndex = 1;
            this.txtPasswd.Leave += new System.EventHandler(this.txtPasswd_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Nombre Usuario:";
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(17, 137);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(82, 25);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "   Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtUsuario
            // 
            this.txtUsuario.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(121, 41);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(161, 20);
            this.txtUsuario.TabIndex = 0;
            this.txtUsuario.Leave += new System.EventHandler(this.txtUsuario_Leave);
            // 
            // epUser
            // 
            this.epUser.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.epUser.ContainerControl = this;
            // 
            // epPass
            // 
            this.epPass.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.epPass.ContainerControl = this;
            // 
            // frmLogIn
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(390, 198);
            this.Controls.Add(this.gbLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio de Sesión";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLogIn_FormClosed);
            this.Load += new System.EventHandler(this.frmLogIn_Load);
            this.gbLogin.ResumeLayout(false);
            this.gbLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epPass)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPasswd;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.PictureBox LogIn;
        private System.Windows.Forms.ErrorProvider epUser;
        private System.Windows.Forms.ErrorProvider epPass;

    }
}