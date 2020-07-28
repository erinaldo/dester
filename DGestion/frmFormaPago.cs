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
    public partial class frmFormaPago : Form
    {
        public delegate void pasarFormaPagoCod1(int CodFormaPago);
        public event pasarFormaPagoCod1 pasarFPCod;
        public delegate void pasarFormaPagoRS(string NFP);
        public event pasarFormaPagoRS pasarFPN;

        public frmFormaPago() {
            InitializeComponent();
        }

        CGenericBD conn = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private void frmFormaPago_Load(object sender, EventArgs e) {            
            gpoCliente.Visible = false;
            this.lvwFormaPago.Height = 330;

            cboFormaPago.SelectedIndex = 0;

            conn.ConectarBD();
            MostrarDatos();
            FormatoListView();            
        }

        private void FormatoListView() {
            try {
                this.lvwFormaPago.View = View.Details;
                this.lvwFormaPago.LabelEdit = false;
                this.lvwFormaPago.AllowColumnReorder = false;
                this.lvwFormaPago.FullRowSelect = true;
                this.lvwFormaPago.GridLines = true;
            }
            catch { }
        }

        public void MostrarDatos() {
            try {
                lvwFormaPago.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM Formapago ORDER BY Descripcion", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows) {
                    lvwFormaPago.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDFORMAPAGO"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());
                    //item.SubItems.Add(dr["DESCUENTO"].ToString());                    

                    item.ImageIndex = 0;

                    lvwFormaPago.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btcCerrar_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = false;
            this.lvwFormaPago.Height = 330;
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            this.lvwFormaPago.Height = 240;

            //txtProcDesc.Text = "";
            txtDescripcion.Text = "";            
        }

        private void tsBtnModificar_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = true;
            this.lvwFormaPago.Height = 240;
        }

        private void btnCerrarCarga_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = false;
            this.lvwFormaPago.Height = 330;
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            try {
                /*tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;
                btnModificar.Enabled = false;*/

                string agregar = "INSERT INTO FormaPago (Descripcion, Descuento) VALUES ('" + this.txtDescripcion.Text + "', '0')";

                if (conn.InsertarGeneric(agregar)) {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados");
                }
                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void lvwFormaPago_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                conn.LeeGeneric("SELECT * FROM FormaPago WHERE IdFormaPago = " + Convert.ToInt32(lvwFormaPago.SelectedItems[0].SubItems[0].Text) + " ORDER BY IDFORMAPAGO", "FormaPago");
                this.txtDescripcion.Text = conn.leerGeneric["DESCRIPCION"].ToString();
                //this.txtProcDesc.Text = conn.leerGeneric["DESCUENTO"].ToString();

                conn.DesconectarBDLeeGeneric();
            }
            catch { conn.DesconectarBDLeeGeneric(); }
        }

        private void btnModificar_Click(object sender, EventArgs e) {
            try  {

                lvwFormaPago.Height = 240;

               // btnEliminar.Enabled = false;
              //  tsBtnModificar.Enabled = true;
              //  tsBtnNuevo.Enabled = true;

                if (txtDescripcion.Text.Trim() == "")
                    txtDescripcion.Text = "-";

              //  if (txtProcDesc.Text.Trim() == "")
              //      txtProcDesc.Text = "-";

                string actualizar = "DESCRIPCION='" + txtDescripcion.Text.Trim() + "'";

                if (conn.ActualizaGeneric("FormaPago", actualizar, " IdFormaPago = " + Convert.ToInt32(lvwFormaPago.SelectedItems[0].SubItems[0].Text) + "")) {
                    MostrarDatos();
                    //gpoCliente.Visible = false;
                    //lvwProveedores.Height = 420;                   
                    MessageBox.Show("Datos Actualizados");
                }
            }
            catch { MessageBox.Show("Error: Al Actualizar datos"); }
        }

        private void lvwFormaPago_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RecargaDatos();
        }

        private void RecargaDatos()
        {
            try
            {
                pasarFPCod(Int16.Parse(this.lvwFormaPago.SelectedItems[0].SubItems[0].Text));  //Si doble click en cella ejecuta delegado para pasar datos entre forms
                pasarFPN(this.lvwFormaPago.SelectedItems[0].SubItems[1].Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch { }
        }

        private void lvwFormaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                RecargaDatos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (conn.EliminarGeneric("FormaPago", " IdFormaPago = " + Int16.Parse(this.lvwFormaPago.SelectedItems[0].SubItems[0].Text)))
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
    }
}
