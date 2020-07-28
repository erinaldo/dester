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
using DGestion.Clases;

namespace DGestion
{
    public partial class frmEmpresa : Form
    {
        public delegate void pasarEmpresas(string NEmpresas);
        public event pasarEmpresas NEmpresas;

        public delegate void pasarPtoVtaEmpresa(string PtoVtaEmpresa);
        public event pasarPtoVtaEmpresa PtoVenta;

        CGenericBD conn = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        public static string nombreEmpresa_login = "-";
        public static string PtoVta_login;

        private int idEmpresa;

        //private bool bAccesoSistema=false;


        public frmEmpresa()
        {
            InitializeComponent();
        }

        private void conPermi()
        {
            try
            {
                string sUsuarioLegueado;
                sUsuarioLegueado = frmPrincipal.Usuario;

                SqlCommand cm = new SqlCommand("SELECT Personal.USUARIO, Personal.NOMBREYAPELLIDO, PermisoModulo.Idmodulo, PermisoControl.IdControl, PermisoControl.Control, EstadoSistema.Descripcion, PermisoUsuario.IdPersonal FROM Personal, PermisoModulo, PermisoControl, PermisoUsuario, EstadoSistema WHERE PermisoUsuario.IdPersonal = Personal.IdPersonal AND PermisoUsuario.IdModulo = PermisoModulo.IdModulo AND PermisoUsuario.IdControl = PermisoControl.IdControl AND PermisoUsuario.IdEstadoSistema = EstadoSistema.IdEstado AND PermisoUsuario.IdModulo = 8 AND Personal.USUARIO = '" + sUsuarioLegueado + "'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboPtoVta.Items.Clear();



                if (dt.Rows[0]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[0]["Control"].ToString().Trim() == "Habilitar Pto. Vta. 0001 - Mendoza Sur 439, San Juan, San Juan"  && Convert.ToInt32(lvwEmpresaLogin.SelectedItems[0].SubItems[0].Text) == 1)                
                   cboPtoVta.Items.Add("0001");
                if (dt.Rows[1]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[1]["Control"].ToString().Trim() == "Habilitar Pto. Vta. 0002 - Facturaciòn Manual" && Convert.ToInt32(lvwEmpresaLogin.SelectedItems[0].SubItems[0].Text) == 1)
                    cboPtoVta.Items.Add("0002");
                if (dt.Rows[2]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[2]["Control"].ToString().Trim() == "Habilitar Pto. Vta. 0003 - Fleming 576, San Josè, Mendoza" && Convert.ToInt32(lvwEmpresaLogin.SelectedItems[0].SubItems[0].Text) == 1)
                    cboPtoVta.Items.Add("0003");
                if (dt.Rows[3]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[3]["Control"].ToString().Trim() == "Habilitar Pto. Vta. 0004 - Mendoza Sur 439, San Juan, San Juan" && Convert.ToInt32(lvwEmpresaLogin.SelectedItems[0].SubItems[0].Text) == 1)
                    cboPtoVta.Items.Add("0004");

                if (dt.Rows[4]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[4]["Control"].ToString().Trim() == "Habilitar Pto. Vta. 0001 - Rio Juramento 5587, San Jose Mendoza" && Convert.ToInt32(lvwEmpresaLogin.SelectedItems[0].SubItems[0].Text) == 2)
                    cboPtoVta.Items.Add("0001");
                if (dt.Rows[5]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[5]["Control"].ToString().Trim() == "Habilitar Pto. Vta. 0002 - Facturaciòn Manual" && Convert.ToInt32(lvwEmpresaLogin.SelectedItems[0].SubItems[0].Text) == 2)
                    cboPtoVta.Items.Add("0002");
                if (dt.Rows[6]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[6]["Control"].ToString().Trim() == "Habilitar Pto. Vta. 0003 - Rio Juramento 5587, San Jose Mendoza" && Convert.ToInt32(lvwEmpresaLogin.SelectedItems[0].SubItems[0].Text) == 2)
                    cboPtoVta.Items.Add("0003");
                if (dt.Rows[7]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[7]["Control"].ToString().Trim() == "Habilitar Pto. Vta. 0004 - Mendoza Sur 439, San Juan, San Juan" && Convert.ToInt32(lvwEmpresaLogin.SelectedItems[0].SubItems[0].Text) == 2)
                    cboPtoVta.Items.Add("0004");

                cboPtoVta.SelectedIndex = 0;


                cm.Connection.Close();

            }
            catch { }
        }

        private void frmEmpresa_Load(object sender, EventArgs e)
        {            
            gpoCliente.Visible = false;
            this.lvwEmpresaLogin.Height = 410;

            cboFormaPago.SelectedIndex = 0;
            //cboPtoVta.SelectedIndex = 0;
            MostrarDatos();
            FormatoListView();
        }

        private void FormatoListView()
        {
            try
            {
                this.lvwEmpresaLogin.View = View.Details;
                this.lvwEmpresaLogin.LabelEdit = false;
                this.lvwEmpresaLogin.AllowColumnReorder = false;
                this.lvwEmpresaLogin.FullRowSelect = true;
                this.lvwEmpresaLogin.GridLines = true;
            }
            catch { }
        }

        
        public void MostrarDatos()
        {
            try
            {
                lvwEmpresaLogin.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM Empresa ORDER BY IDEMPRESA", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwEmpresaLogin.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDEMPRESA"].ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString());
                    item.SubItems.Add(dr["CUIT"].ToString());
                    //item.SubItems.Add(dr["DOMICILIOFISCAL"].ToString());
                    
                    item.ImageIndex = 0;

                    lvwEmpresaLogin.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void frmEmpresa_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Conexion conn = new Conexion();
            // conn.DesconectarBD();
            // Application.Exit();
        }

        private void frmEmpresa_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //Auditoria
                //AuditoriaSistema AS1 = new AuditoriaSistema();
                //AS1.GACierreLogin_0004("Cerró Sesión", StatusBar.Items[5].Text);
                ///////////////////////////////////////////////////////
                //Application.Exit();
            }
            catch { }
        }

        private void lvwEmpresaLogin_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conn.LeeGeneric("SELECT * FROM Empresa WHERE idEmpresa = " + Convert.ToInt32(lvwEmpresaLogin.SelectedItems[0].SubItems[0].Text) + " ORDER BY IDEMPRESA", "Empresa");
                this.txtIdEmpresa.Text = conn.leerGeneric["IdEmpresa"].ToString();
                this.txtRazonSocial.Text = conn.leerGeneric["RazonSocial"].ToString();
                this.txtCuit.Text = conn.leerGeneric["CUIT"].ToString();                
                this.txtEmail.Text = conn.leerGeneric["Email"].ToString();

                idEmpresa = Convert.ToInt32(lvwEmpresaLogin.SelectedItems[0].SubItems[0].Text);
                //ptosVenta(idEmpresa);
                gbPtoVta.Visible = true;
                conn.DesconectarBDLeeGeneric();

                conPermi();

            }
            catch { conn.DesconectarBDLeeGeneric(); }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            gbPtoVta.Visible = false;
            lvwEmpresaLogin.Enabled = true;
            gbEmpresa.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                /*tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;
                btnModificar.Enabled = false;*/

                string agregar = "INSERT INTO Empresa (RazonSocial, CUIT, Email) VALUES ('" + this.txtRazonSocial.Text + "', '" + this.txtCuit.Text + "', '" + this.txtEmail.Text + "')";

                if (conn.InsertarGeneric(agregar))
                {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados");
                }
                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            //conectar.DesconectarBD();
            Application.Exit();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                nombreEmpresa_login = lvwEmpresaLogin.SelectedItems[0].SubItems[1].Text;
                PtoVta_login = cboPtoVta.Text;                

                //frmPrincipal status = new frmPrincipal();
                //status.StatusBar.Items[25].Text = nombreEmpresa_login;       
                //status.Empresas();
                
                     
                NEmpresas(nombreEmpresa_login);
                PtoVenta(PtoVta_login);
                this.Close();

                gbPtoVta.Visible = false;
                lvwEmpresaLogin.Enabled = true;
                gbEmpresa.Enabled = true;

                
            }
            catch {
                frmPrincipal ss = new frmPrincipal();
                ss.Show();
                this.Close();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                lvwEmpresaLogin.Height = 275;

                //btnEliminar.Enabled = false;
                tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;

                //if (txtDescripcion.Text.Trim() == "")
                //    txtDescripcion.Text = "-";

                //  if (txtProcDesc.Text.Trim() == "")
                //      txtProcDesc.Text = "-";

                string actualizar = "RazonSocial = '" + txtRazonSocial.Text + "', CUIT = '" + txtCuit.Text + "', EMAIL = '" + txtEmail.Text + "' ";

                if (conn.ActualizaGeneric("Empresa", actualizar, " idEmpresa = " + Convert.ToInt32(lvwEmpresaLogin.SelectedItems[0].SubItems[0].Text) + ""))
                {
                    MostrarDatos();
                    //gpoCliente.Visible = false;
                    //lvwProveedores.Height = 420;                   
                    MessageBox.Show("Datos Actualizados");
                }
            }
            catch
            {
                MessageBox.Show("Error: Al Actualizar datos");
            }
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = true;
            this.lvwEmpresaLogin.Height = 275;

            this.txtIdEmpresa.Text = "";
            this.txtRazonSocial.Text = "";
            this.txtCuit.Text = "";            
            this.txtEmail.Text = "";
        }

        private void lvwEmpresaLogin_MouseClick(object sender, MouseEventArgs e)
        {
            lvwEmpresaLogin.Enabled = false;
            gbEmpresa.Enabled = false;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = false;
            this.lvwEmpresaLogin.Height = 410;
        }

        private void tsBtnModificar_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = true;
            this.lvwEmpresaLogin.Height = 275;
        }

        private void ptosVenta(int idEmpresa)
        {
            try
            {
                cboPtoVta.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM EmpresaCentro WHERE IDEmpresa = "+ idEmpresa + " ORDER BY IDEmpresaCentro", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {                    
                    cboPtoVta.Items.Add(dr["NROCENTRO"].ToString());
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void cboPtoVta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (idEmpresa == 0)
                    idEmpresa = 1;

                SqlCommand cm = new SqlCommand("SELECT * FROM EmpresaCentro WHERE NROCENTRO = " + cboPtoVta.Text.Trim() + " AND IDEmpresa = "+ idEmpresa+" ORDER BY IDEmpresaCentro", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    txtDirFiscal.Text = dr["DOMICILIOCENTRO"].ToString();
                }
                cm.Connection.Close();
            }
            catch { }
        }
    }
}
