using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Threading;
using DGestion.Clases;

namespace DGestion
{
    public partial class frmLogIn : Form
    {
        public frmLogIn() {
            InitializeComponent();
        }

        public static string usuarioLogeado;

        //string connstr = DataConexion.Utility.GetConnectionString(); //String de Conexion
        Conexion conectar = new Conexion();

        private void btnAceptar_Click(object sender, EventArgs e)  {
            try {
                conectar.ConectarBD();
                String txtUser = this.txtUsuario.Text;
                String txtPaswd = this.txtPasswd.Text;

                if (conectar.ConsultaConParametrosLogIn("PERSONAL", "USUARIO = '" + txtUser + "' AND PASSWORD = '" + txtPaswd + "'")) {
                    usuarioLogeado = txtUser; //Variable estatica para mostrar el nombre de usuario en la barra de estado de principal

                    //Auditoria
                    AuditoriaSistema AS2 = new AuditoriaSistema();
                    AS2.GAIngresoLogin_0002("Ingresó al Sistema", txtUser);
                    ///////////////////////////////////////////////////////
                    
                    frmPrincipal ss = new frmPrincipal();
                    ss.Show();
                    this.Hide();
                }
                else {
                    MessageBox.Show("Datos de Acceso no validos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //Auditoria
                    AuditoriaSistema AS1 = new AuditoriaSistema();
                    AS1.GAIngresoLogin_0003("Falló ingreso al Sistema", txtUser);
                    ///////////////////////////////////////////////////////
                }
            }

            catch (System.Exception excep) {
                //MessageBox.Show(excep.Message);

                //Auditoria
                AuditoriaSistema AS3 = new AuditoriaSistema();
                AS3.GAErrorLogin_0001(excep.Message, txtUsuario.Text);
                ////////////////////////////////////////////////
            }
        }       
        

        private void btnCerrar_Click(object sender, EventArgs e) {
            conectar.DesconectarBD();
            Application.Exit();
        }

        private void frmLogIn_FormClosed(object sender, FormClosedEventArgs e) {
            conectar.DesconectarBD();
        }        

        private void frmLogIn_Load(object sender, EventArgs e)
        { 
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                epUser.Icon = Properties.Resources.cancel;
                epUser.SetError(txtUsuario, "Error");
            }
            else
            {
                epUser.Icon = Properties.Resources.ok;
                epUser.SetError(txtUsuario, "Error");
            }
        }

        private void txtPasswd_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPasswd.Text))
            {
                epPass.Icon = Properties.Resources.cancel;
                epPass.SetError(txtPasswd, "Error");
            }
            else
            {
                epPass.Icon = Properties.Resources.ok;
                epPass.SetError(txtPasswd, "Ok");
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "" && txtPasswd.Text == "")
            {
                btnAceptar.Enabled = false;
            }
            else
            {
                btnAceptar.Enabled = true;
            }
        }

        private void gbLogin_Enter(object sender, EventArgs e)
        {
            btnAceptar.Enabled = false;
        }
    }
}