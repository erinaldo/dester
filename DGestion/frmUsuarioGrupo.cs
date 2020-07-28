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
    public partial class frmUsuarioGrupo : Form
    {
        CGenericBD conn = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        public frmUsuarioGrupo()
        {
            InitializeComponent();
        }

        private void btcCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUsuarioGrupo_Load(object sender, EventArgs e) {   
            try {                
                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();               

                FormatoListView();
                MostrarDatos();

                conn.ConsultaGeneric("Select * FROM TipoPersonal", "TipoPersonal");

                this.cboGrupoUsuario.DataSource = conn.ds.Tables[0];
                this.cboGrupoUsuario.ValueMember = "Idtipopersonal";
                this.cboGrupoUsuario.DisplayMember = "Descripcion";

                cboBuscaPersonal.SelectedIndex = 0;
                cboGrupoUsuario.SelectedIndex = 0;
            }
            catch {
                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();
            }            
        }

        public void MostrarDatos() {
            try {

                lvwPersonal.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM Personal, TipoPersonal WHERE personal.IDTIPOPERSONAL = TipoPersonal.IDTIPOPERSONAL", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows) {
                    lvwPersonal.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDPERSONAL"].ToString());
                    item.SubItems.Add(dr["NOMBREYAPELLIDO"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());
                    item.SubItems.Add(dr["USUARIO"].ToString());
                    item.SubItems.Add(dr["IDTIPOPERSONAL"].ToString());

                    if (item.SubItems[3].Text.Trim() == "Sin Grupo" || item.SubItems[3].Text.Trim() == "" || item.SubItems[3].Text.Trim() == "-")
                        item.ImageIndex = 6;
                    else {
                        if (item.SubItems[4].Text == "1")
                            item.ImageIndex = 0;
                        else if (item.SubItems[4].Text == "2")
                            item.ImageIndex = 2;
                        else if (item.SubItems[4].Text == "3")
                            item.ImageIndex = 3;
                        else if (item.SubItems[4].Text == "4")
                            item.ImageIndex = 4;
                        else if (item.SubItems[4].Text == "5")
                            item.ImageIndex = 5;
                        else
                            item.ImageIndex = 6;
                    }
                    lvwPersonal.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void FormatoListView() {
            lvwPersonal.View = View.Details;
            lvwPersonal.LabelEdit = false;
            lvwPersonal.AllowColumnReorder = true;
            lvwPersonal.FullRowSelect = true;
            lvwPersonal.GridLines = true;
        }

        private void lvwPersonal_SelectedIndexChanged(object sender, EventArgs e) {
            try {   
                txtNombreUsuario.Text = lvwPersonal.SelectedItems[0].SubItems[3].Text.Trim();
                cboGrupoUsuario.Text = lvwPersonal.SelectedItems[0].SubItems[2].Text.Trim();

                txtContraseña.Text = ""; txtRepetirContraseña.Text = "";

                if (lvwPersonal.SelectedItems[0].SubItems[3].Text.Trim() == "")
                {
                    MessageBox.Show("La persona seleccionada no tiene nombre de usuario creado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContraseña.Text = ""; txtRepetirContraseña.Text = "";
                }

                if (lvwPersonal.SelectedItems[0].SubItems[2].Text == "Sin Grupo")
                {
                    MessageBox.Show("La persona no esta incluida dentro de un grupo de usuario", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContraseña.Text = ""; txtRepetirContraseña.Text = "";
                }
            }
            catch { }
        }

        private void btnGuardarDatos_Click(object sender, EventArgs e) {
            try {
                if (lvwPersonal.SelectedItems[0].SubItems[2].Text == "Sin Grupo")
                    MessageBox.Show("La persona debe estar incluida dentro de un grupo y tener un nombre de usuario registrado en el sistema para poder operar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else {
                    if (txtContraseña.Text.Trim() == txtRepetirContraseña.Text.Trim()) {
                        string actualizar = "USUARIO='" + txtNombreUsuario.Text.Trim() + "', PASSWORD='" + txtContraseña.Text.Trim() + "'";
                        if (conn.ActualizaGeneric("Personal", actualizar, " IdPersonal = " + lvwPersonal.SelectedItems[0].SubItems[0].Text + "")) {
                            MostrarDatos();
                            if (this.txtNombreUsuario.Text.Trim() == "")
                                txtNombreUsuario.Text = "";
                            if (this.txtContraseña.Text.Trim() == "")
                                txtContraseña.Text = "";
                            if (this.txtRepetirContraseña.Text.Trim() == "")
                                txtRepetirContraseña.Text = "";
                            //MessageBox.Show("Datos Actualizados");
                            MostrarDatos();
                        }
                    }
                    else
                        MessageBox.Show("Las contraseñas ingresadas son diferentes, verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch {
                MessageBox.Show("Error: Al Actualizar datos");
                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();
            }
        }

        private void btnIncluirAlGrupo_Click(object sender, EventArgs e) {
            try {
                int IdTipoPerso;
                string Personal;
                if (txtContraseña.Text.Trim() == txtRepetirContraseña.Text.Trim()) {
                    SqlCommand cm = new SqlCommand("SELECT * FROM TipoPersonal WHERE Descripcion = '" + this.cboGrupoUsuario.Text.Trim() + "'", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows) {
                        IdTipoPerso = Convert.ToInt32(dr["IDTIPOPERSONAL"].ToString());
                        Personal = dr["Descripcion"].ToString();                    

                       string actualizar = "IDTIPOPERSONAL=" + IdTipoPerso + "";
                       if (conn.ActualizaGeneric("Personal", actualizar, " IdPersonal = " + Convert.ToInt32(lvwPersonal.SelectedItems[0].SubItems[0].Text) + ""))
                       {
                                  MostrarDatos();
                                 // MessageBox.Show("Datos Actualizados");
                       }
                    }
                }
                         else
                           MessageBox.Show("Las contraseñas ingresadas son diferentes, verificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            catch { MessageBox.Show("Error: Al Actualizar datos"); }
        }

    }
}
