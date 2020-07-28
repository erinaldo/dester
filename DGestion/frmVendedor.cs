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
    public partial class frmVendedor : Form
    {
        public delegate void pasarVendeCod1(int CodVende);
        public event pasarVendeCod1 pasarVendeCod;
        public delegate void pasarVendeRS(string NVende);
        public event pasarVendeRS pasarVendeN;

        public frmVendedor() {
            InitializeComponent();
        }

        CGenericBD conn = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private void lvwVendedor_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                conn.LeeGeneric("SELECT * FROM Personal, TipoPersonal, Vendedor WHERE Personal.IDTipoPersonal=TipoPersonal.IdTipoPersonal AND Vendedor.IDTipoPersonal=TipoPersonal.IdTipoPersonal AND personal.idpersonal = Vendedor.idpersonal AND IdVendedor = " + Convert.ToInt32(lvwVendedor.SelectedItems[0].SubItems[0].Text) + " ORDER BY NOMBREYAPELLIDO", "Vendedor");

                this.txtCodPersonal.Text = conn.leerGeneric["IDPERSONAL"].ToString();
                this.txtCodTipoPersonal.Text = conn.leerGeneric["IDTIPOPERSONAL"].ToString();
                this.txtComisionVendedor.Text = conn.leerGeneric["COMISION"].ToString();

                conn.DesconectarBDLeeGeneric();
            }
            catch { conn.DesconectarBDLeeGeneric(); }
        }

        private void frmVendedor_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            this.lvwVendedor.Height = 420;

            conn.ConectarBD();
            MostrarDatos();
            FormatoListView();

            cboBuscaVendedor.SelectedIndex = 0;
            cboTipoPersonal.SelectedIndex = 0;
        }

        private void FormatoListView() {
            lvwVendedor.View = View.Details;
            lvwVendedor.LabelEdit = false;
            lvwVendedor.AllowColumnReorder = false;
            lvwVendedor.FullRowSelect = true;
            lvwVendedor.GridLines = true;
        }

        public void MostrarDatos() {
            try {
                lvwVendedor.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM Personal, TipoPersonal, Vendedor WHERE Personal.IDTipoPersonal=TipoPersonal.IdTipoPersonal AND Vendedor.IDTipoPersonal=TipoPersonal.IdTipoPersonal AND personal.idpersonal = Vendedor.idpersonal ORDER BY NOMBREYAPELLIDO", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows) {
                    lvwVendedor.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IdVendedor"].ToString());
                    item.SubItems.Add(dr["NOMBREYAPELLIDO"].ToString());
                    item.SubItems.Add(dr["COMISION"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());

                    item.ImageIndex = 0;

                    lvwVendedor.Items.Add(item);
                }                
                cm.Connection.Close();
            }
            catch { }
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            this.lvwVendedor.Height = 275;
            txtComisionVendedor.Text = "";
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
            btnModificar.Enabled = false;
            
            this.txtCodPersonal.Text = "";            
            this.txtComisionVendedor.Text = "";

        }

        private void btcCerrar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            this.lvwVendedor.Height = 420;

            tsBtnNuevo.Enabled = true;
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = true;            
        }

        private void tsBtnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            this.lvwVendedor.Height = 275;

            tsBtnModificar.Enabled = false;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
            
        }

        private void lvwVendedor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CargaDatos();
        }

        private void CargaDatos()
        {
            try
            {
                pasarVendeCod(Convert.ToInt32(this.lvwVendedor.SelectedItems[0].SubItems[0].Text));  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                pasarVendeN(lvwVendedor.SelectedItems[0].SubItems[1].Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch { }
        }



        private void btnGuardar_Click(object sender, EventArgs e) {
            try {
                tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;
                btnModificar.Enabled = false;

                string agregar = "INSERT INTO Vendedor (COMISION, IDPERSONAL, IDTIPOPERSONAL) VALUES ('" + this.txtComisionVendedor.Text  + "', " + Convert.ToInt32(this.txtCodPersonal.Text)   + ", " + Convert.ToInt32(this.txtCodTipoPersonal.Text) + ")";

                if (conn.InsertarGeneric(agregar)) {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados");
                }
                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void txtCodPersonal_TextChanged(object sender, EventArgs e) {
            try {
                if (this.txtCodPersonal.Text.Trim() != "") {
                    conn.ConsultaGeneric("SELECT * FROM Personal WHERE IDPERSONAL = " + Convert.ToInt32(this.txtCodPersonal.Text) + "", "Personal");

                    this.cboPersonal.DataSource = conn.ds.Tables[0];
                    this.cboPersonal.ValueMember = "IDPERSONAL";
                    this.cboPersonal.DisplayMember = "NOMBREYAPELLIDO";
                }
                else
                    this.cboPersonal.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboPersonal.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MostrarDatos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (conn.EliminarGeneric("Vendedor", " IdVendedor = " + Int16.Parse(this.lvwVendedor.SelectedItems[0].SubItems[0].Text)))            
            {
                MostrarDatos();
                tsBtnNuevo.Enabled = true;
                tsBtnModificar.Enabled = true;
                btnModificar.Enabled = true;
                btnGuardar.Enabled = true;                

                MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error al Eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                lvwVendedor.Height = 275;

                string actualizar = "COMISION=" + Convert.ToDecimal(txtComisionVendedor.Text.Trim()) + ", IDPERSONAL=" + Convert.ToInt32(txtCodPersonal.Text) + ", IDTIPOPERSONAL=" + Convert.ToInt32(txtCodTipoPersonal.Text) + "";

                if (conn.ActualizaGeneric("Vendedor", actualizar, " IdVendedor = " + Convert.ToInt32(lvwVendedor.SelectedItems[0].SubItems[0].Text) + ""))
                {
                    MostrarDatos();
                    //gpoCliente.Visible = false;
                    //lvwProveedores.Height = 420;                   
                    MessageBox.Show("Datos Actualizados");
                }
            }
            catch { MessageBox.Show("Error: Al Actualizar datos"); }
        }

        private void lvwVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                CargaDatos();
            }          
        }

        private void btnVendedor_Click(object sender, EventArgs e)
        {
            frmPersonal frmPerso = new frmPersonal();
            frmPerso.pasadoPerso1 += new frmPersonal.pasarPersona1(codPersonal);  //Delegado1 
            frmPerso.pasadoPerso2 += new frmPersonal.pasarPersona2(personal); //Delegado2
            
            frmPerso.ShowDialog();
        }

        public void codPersonal(int codPerso)
        {
            this.txtCodPersonal.Text = codPerso.ToString();
        }

        public void personal(string perso)
        {
            this.cboPersonal.Text = perso.ToString();
        }

    }
}
