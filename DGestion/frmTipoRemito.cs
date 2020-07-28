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
    public partial class frmTipoRemito : Form
    {
        public delegate void pasarTipoRemitoCod1(int CodTipoRemito);
        public event pasarTipoRemitoCod1 pasarTRCod;
        public delegate void pasarTipoRemitoRS(string NTR);
        public event pasarTipoRemitoRS pasarTRN;

        public frmTipoRemito() {
            InitializeComponent();
        }

        CGenericBD conn = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private void frmTipoRemito_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            this.lvwTipoRemito.Height = 330;

            cboBuscaTipoRemito.SelectedIndex = 0;

            conn.ConectarBD();
            MostrarDatos();
            FormatoListView();
        }

        private void FormatoListView() {
            try {
                this.lvwTipoRemito.View = View.Details;
                this.lvwTipoRemito.LabelEdit = false;
                this.lvwTipoRemito.AllowColumnReorder = false;
                this.lvwTipoRemito.FullRowSelect = true;
                this.lvwTipoRemito.GridLines = true;
            }
            catch { }
        }

        public void MostrarDatos() {
            try {
                lvwTipoRemito.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM TipoRemito ORDER BY Descripcion", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows) {
                    lvwTipoRemito.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDTIPOREMITO"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());

                    item.ImageIndex = 0;

                    lvwTipoRemito.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tsBtnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            this.lvwTipoRemito.Height = 240;
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            this.lvwTipoRemito.Height = 240;
        }

        private void btnCerrarDetalleRemito_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            this.lvwTipoRemito.Height = 330;
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            try {
                /*tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;
                btnModificar.Enabled = false;*/

                string agregar = "INSERT INTO TipoRemito (Descripcion) VALUES ('" + this.txtDescripTipoRemito.Text + "')";

                if (conn.InsertarGeneric(agregar)) {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados");
                }
                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void btnModificar_Click(object sender, EventArgs e) {
            try {

                lvwTipoRemito.Height = 240;

                // btnEliminar.Enabled = false;
                //  tsBtnModificar.Enabled = true;
                //  tsBtnNuevo.Enabled = true;

                if (txtDescripTipoRemito.Text.Trim() == "")
                    txtDescripTipoRemito.Text = "-";

                string actualizar = "DESCRIPCION='" + txtDescripTipoRemito.Text.Trim() + "'";

                if (conn.ActualizaGeneric("TipoRemito", actualizar, " IdTipoRemito = " + Convert.ToInt32(lvwTipoRemito.SelectedItems[0].SubItems[0].Text) + "")) {
                    MostrarDatos();
                    //gpoCliente.Visible = false;
                    //lvwProveedores.Height = 420;                   
                    MessageBox.Show("Datos Actualizados");
                }
            }
            catch { MessageBox.Show("Error: Al Actualizar datos"); }
        }

        private void lvwTipoRemito_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                conn.LeeGeneric("SELECT * FROM TipoRemito WHERE IdTipoRemito = " + Convert.ToInt32(this.lvwTipoRemito.SelectedItems[0].SubItems[0].Text) + " ORDER BY IdTipoRemito", "TipoRemito");
                this.txtDescripTipoRemito.Text = conn.leerGeneric["DESCRIPCION"].ToString();
                
                conn.DesconectarBDLeeGeneric();
            }
            catch { conn.DesconectarBDLeeGeneric(); }
        }

        private void lvwTipoRemito_MouseDoubleClick(object sender, MouseEventArgs e) {
            RecargaDatos();
        }

        private void RecargaDatos()
        {
            try
            {
                pasarTRCod(Int16.Parse(this.lvwTipoRemito.SelectedItems[0].SubItems[0].Text));  //Si doble click en cella ejecuta delegado para pasar datos entre forms
                pasarTRN(lvwTipoRemito.SelectedItems[0].SubItems[1].Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch { }
        }

        private void lvwTipoRemito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                RecargaDatos();
            }
        }

    }
}
