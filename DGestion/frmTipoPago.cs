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
    public partial class frmTipoPago : Form
    {
        public delegate void pasarTipoPagoCod1(int CodTipoPago);
        public event pasarTipoPagoCod1 pasarTPCod;
        public delegate void pasarTipoPagoRS(string NTP);
        public event pasarTipoPagoRS pasarTPN;

        public frmTipoPago()
        {
            InitializeComponent();
        }

        CGenericBD conn = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string agregar = "INSERT INTO TipoPago VALUES('" + txtDescripcion.Text + "')";

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

        private void frmTipoPago_Load(object sender, EventArgs e)
        {
            gpoCliente.Visible = false;
            this.lvwTipoPago.Height = 310;

            conn.ConectarBD();
            MostrarDatos();
            FormatoListView();   
        }

        private void FormatoListView()
        {
            try
            {
                this.lvwTipoPago.View = View.Details;
                this.lvwTipoPago.LabelEdit = false;
                this.lvwTipoPago.AllowColumnReorder = false;
                this.lvwTipoPago.FullRowSelect = true;
                this.lvwTipoPago.GridLines = true;
            }
            catch { }
        }

        public void MostrarDatos()
        {
            try
            {
                lvwTipoPago.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM TipoPago ORDER BY Descripcion", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwTipoPago.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDTIPOPAGO"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());                   

                    item.ImageIndex = 0;

                    lvwTipoPago.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            //lvwTipoPago.Items.Clear();
            txtDescripcion.Text = "";

            gpoCliente.Visible = true;
            this.lvwTipoPago.Height = 210;
        }

        private void lvwTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conn.LeeGeneric("SELECT * FROM TipoPago WHERE IdTipoPago = " + Convert.ToInt32(lvwTipoPago.SelectedItems[0].SubItems[0].Text) + " ORDER BY IdTipoPago", "TipoPago");
                
                this.txtDescripcion.Text = conn.leerGeneric["DESCRIPCION"].ToString();
                //this.txtProcDesc.Text = conn.leerGeneric["DESCUENTO"].ToString();

                conn.DesconectarBDLeeGeneric();
            }
            catch { conn.DesconectarBDLeeGeneric(); }
        }

        private void lvwTipoPago_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RecargaDatos();
        }

        private void btcCerrar_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = false;
            this.lvwTipoPago.Height = 310;
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RecargaDatos()
        {
            try
            {
                pasarTPCod(Int16.Parse(this.lvwTipoPago.SelectedItems[0].SubItems[0].Text));  //Si doble click en cella ejecuta delegado para pasar datos entre forms
                pasarTPN(this.lvwTipoPago.SelectedItems[0].SubItems[1].Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch { }
        }

        private void lvwTipoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                RecargaDatos();
            }
        }

    }
}
