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
    public partial class frmTipoFactura : Form
    {
        public delegate void pasarTipoFactura1(int CodTipoFactu);
        public event pasarTipoFactura1 pasadoTipoFactu1;
        public delegate void pasarTipoFactura2(string DTipoFactu);
        public event pasarTipoFactura2 pasadoTipoFactu2;

        public frmTipoFactura()
        {
            InitializeComponent();
        }

        ArticulosBD conn = new ArticulosBD();
        CGenericBD conn1 = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        public void MostrarDatos()
        {
            try
            {
                lvwTipoFactura.Items.Clear();

                SqlCommand cm = new SqlCommand("Select IdTipoFactura As 'Código', Descripcion As 'Tipo Factura' From TipoFactura Order By Descripcion", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwTipoFactura.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Código"].ToString());
                    item.SubItems.Add(dr["Tipo Factura"].ToString());

                    item.ImageIndex = 0;

                    lvwTipoFactura.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btcCerrar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            this.lvwTipoFactura.Height = 290;
        }

        private void frmTipoFactura_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            this.lvwTipoFactura.Height = 290;
            cboTipoFactura.SelectedIndex = 0;
                        
            conn.ConectarBD();
            MostrarDatos();
            FormatoListView();   
        }

        private void FormatoListView()
        {
            try
            {
                this.lvwTipoFactura.View = View.Details;
                this.lvwTipoFactura.LabelEdit = false;
                this.lvwTipoFactura.AllowColumnReorder = false;
                this.lvwTipoFactura.FullRowSelect = true;
                this.lvwTipoFactura.GridLines = true;
            }
            catch { }
        }

        private void btnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            this.lvwTipoFactura.Height = 190;
            txtCodigo.Text = "Automático";
            btnGuardar.Enabled = true;

            //txtCodigo.Text = "";
            //cboTipoFactura.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = false;
            this.lvwTipoFactura.Height = 290;


            string agregar = "insert into TipoArticulo values('" + cboTipoFactura.Text + "')";
                if (conn.InsertarTipoArticulo(agregar))
                {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);           
        }

        private void RecargaDatos()
        {
            try
            {
                pasadoTipoFactu1(Int16.Parse(this.txtCodigo.Text));  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                pasadoTipoFactu2(this.cboTipoFactura.Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch { }
        }               

        private void lvwTipoFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conn1.LeeGeneric("SELECT * FROM TipoFactura WHERE IdTipoFactura = " + Convert.ToInt32(lvwTipoFactura.SelectedItems[0].SubItems[0].Text) + " ORDER BY IdTipoFactura", "TipoFactura");

                this.txtCodigo.Text = conn1.leerGeneric["IdTipoFactura"].ToString();
                this.cboTipoFactura.Text = conn1.leerGeneric["DESCRIPCION"].ToString();

                //conn1.DesconectarBDLeeGeneric();
            }
            catch { conn1.DesconectarBDLeeGeneric(); }
        }

        private void lvwTipoFactura_DoubleClick(object sender, EventArgs e)
        {
            RecargaDatos();     
        }

        private void lvwTipoFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                RecargaDatos();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                gpoCliente.Visible = true;
                //lvwPersonal.Height = 255;

                //btnBajaPersonal.Enabled = false;
                tsBtnNuevo.Enabled = true;                

                string actualizar = "DESCRIPCION = '" + txtDescripcion.Text + "'";

                if (conn.ActualizaArticulo("TipoFactura", actualizar, " IDTIPOFACTURA = " + Convert.ToInt32(lvwTipoFactura.SelectedItems[0].SubItems[0].Text)))
                {
                    MostrarDatos();
                    MessageBox.Show("Datos Actualizados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Actualizar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                gpoCliente.Visible = false;
                this.lvwTipoFactura.Height = 290;
            }
            catch { conn.DesconectarBDLee(); }
        }

        private void tsBtnModificar_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = true;
            this.lvwTipoFactura.Height = 190;
            txtCodigo.Text = "Automático";
            btnGuardar.Enabled = true;
        }
    }
}
