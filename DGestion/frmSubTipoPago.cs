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
    public partial class frmSubTipoPago : Form
    {
        public delegate void pasarSubTipoPagoCod1(int CodSubTipoPago);
        public event pasarSubTipoPagoCod1 pasarSubTPCod;
        public delegate void pasarSubTipoPagoRS(string NSubTP);
        public event pasarSubTipoPagoRS pasarSubTPN;

        CGenericBD conn = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        public frmSubTipoPago()
        {
            InitializeComponent();
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSubTipoPago_Load(object sender, EventArgs e)
        {
            gpoCliente.Visible = false;
            this.lvwSubtipoPago.Height = 315;

            conn.ConectarBD();
            MostrarDatos();
            FormatoListView();  
        }

        private void FormatoListView()
        {
            try
            {
                this.lvwSubtipoPago.View = View.Details;
                this.lvwSubtipoPago.LabelEdit = false;
                this.lvwSubtipoPago.AllowColumnReorder = false;
                this.lvwSubtipoPago.FullRowSelect = true;
                this.lvwSubtipoPago.GridLines = true;
            }
            catch { }
        }

        public void MostrarDatos()
        {
            try
            {
                lvwSubtipoPago.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM SUBTIPOPAGO ORDER BY Descripcion", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwSubtipoPago.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDSUBTIPOPAGO"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());

                    item.ImageIndex = 0;

                    lvwSubtipoPago.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void lvwSubtipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conn.LeeGeneric("SELECT * FROM SubtipoPago, TipoPago WHERE TipoPago.IdTipoPago = SubTipoPago.IdTipoPago AND IdSubTipoPago = " + Convert.ToInt32(lvwSubtipoPago.SelectedItems[0].SubItems[0].Text) + " ORDER BY SubtipoPago.DESCRIPCION", "SubTipoPago");


                this.txtCodTipoPago.Text = conn.leerGeneric["IDTIPOPAGO"].ToString();
                this.txtDescripcion.Text = conn.leerGeneric["DESCRIPCION"].ToString();
                
                conn.DesconectarBDLeeGeneric();
            }
            catch { conn.DesconectarBDLeeGeneric(); }
        }

        private void lvwSubtipoPago_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RecargaDatos();
        }

        private void btnTipoPago_Click(object sender, EventArgs e)
        {

        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {            
            txtDescripcion.Text = "";
            txtCodTipoPago.Text = "";
            cboTipoPago.Text = "";

            gpoCliente.Visible = true;
            this.lvwSubtipoPago.Height = 190;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string agregar = "INSERT INTO SubtipoPago VALUES('" + txtDescripcion.Text + "', " + txtCodTipoPago.Text + ")";

                if (conn.InsertarGeneric(agregar))
                {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void btcCerrar_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = false;
            this.lvwSubtipoPago.Height = 315;
        }

        private void RecargaDatos()
        {
            try
            {
                pasarSubTPCod(Int16.Parse(this.lvwSubtipoPago.SelectedItems[0].SubItems[0].Text));  //Si doble click en cella ejecuta delegado para pasar datos entre forms
                pasarSubTPN(this.lvwSubtipoPago.SelectedItems[0].SubItems[1].Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch { }
        }

        private void lvwSubtipoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                RecargaDatos();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //gpoCliente.Visible = false;

            string actualizar = "Descripcion='" + txtDescripcion.Text + "', IdTipoPago=" + Convert.ToInt32(txtCodTipoPago.Text) + "";
            if (conn.ActualizaGeneric("SubtipoPago", actualizar, " IdSubtipoPago = " + Convert.ToInt32(lvwSubtipoPago.SelectedItems[0].SubItems[0].Text)))
            {
                MostrarDatos();
                MessageBox.Show("Datos Actualizados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error al Actualizar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tsModificarBanco_Click(object sender, EventArgs e)
        {
            //txtDescripcion.Text = "";
            //txtCodTipoPago.Text = "";
            //cboTipoPago.Text = "";

            gpoCliente.Visible = true;
            this.lvwSubtipoPago.Height = 190;
        }

        private void txtCodTipoPago_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodTipoPago.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("SELECT * FROM TipoPago WHERE IdTipoPago = " + Convert.ToInt32(this.txtCodTipoPago.Text) + "", "TipoPago");

                    this.cboTipoPago.DataSource = conn.ds.Tables[0];
                    this.cboTipoPago.ValueMember = "IdTipoPago";
                    this.cboTipoPago.DisplayMember = "Descripcion";
                }
                else
                    this.cboTipoPago.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboTipoPago.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

    }
}
