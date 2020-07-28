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
    public partial class frmListaPrecioVenta : Form
    {
        public delegate void pasarListaVendeCod(int CodLVende);
        public event pasarListaVendeCod pasarListaVendeCod1;
        public delegate void pasarListaVendeDes(string DLVende);
        public event pasarListaVendeDes pasarListaVendeDesc1;


        public frmListaPrecioVenta() {
            InitializeComponent();
        }

        ArticulosBD connArt = new ArticulosBD();   
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#"); 

        private void btnNuevo_Click(object sender, EventArgs e)  {
            gpoCliente.Visible = true;
            //gridListaPrecioVenta.Height = 300;
            lvwListaPrecioVenta.Height = 300;
        }

        private void frmListaPrecio_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            //gridListaPrecioVenta.Height = 300;
            lvwListaPrecioVenta.Height = 300;

            connArt.ConectarBD();
            FormatoListView();
            MostrarDatos();


            cmbBuscar.SelectedIndex = 0;
        }

        private void FormatoListView()
        {
            try
            {
                this.lvwListaPrecioVenta.View = View.Details;
                this.lvwListaPrecioVenta.LabelEdit = false;
                this.lvwListaPrecioVenta.AllowColumnReorder = false;
                this.lvwListaPrecioVenta.FullRowSelect = true;
                this.lvwListaPrecioVenta.GridLines = true;
            }
            catch { }
        }

        public void MostrarDatos() {
            //conn.ConsultaListaPrecioVenta("SELECT IdListaPrecio As 'Código', Descripcion, Porcentaje, PORCFLETE FROM ListaPrecios ORDER BY IdListaPrecio", "ListaPrecios");
            //gridListaPrecioVenta.DataSource = conn.ds.Tables["ListaPrecios"];
            
            try
            {
                this.lvwListaPrecioVenta.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT IdListaPrecio As 'Código', Descripcion, Porcentaje, PORCFLETE, Criterio FROM ListaPrecios ORDER BY Criterio", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwListaPrecioVenta.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Código"].ToString());
                    item.SubItems.Add(dr["Descripcion"].ToString());
                    item.SubItems.Add(dr["PORCFLETE"].ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Porcentaje"].ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Criterio"].ToString(), Color.Empty, Color.LightGray, null);

                    item.UseItemStyleForSubItems = false;
                    item.ImageIndex = 0;
                    lvwListaPrecioVenta.Items.Add(item);
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
            //gridListaPrecioVenta.Height = 195;
            lvwListaPrecioVenta.Height = 195;
            txtCodigo.Text = "Automático"; txtDescripListaPrecio.Text = ""; txtPorcent.Text = "";

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;      
        }

        private void tsBtnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            //gridListaPrecioVenta.Height = 195;
            lvwListaPrecioVenta.Height = 195;

            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = false;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;   
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e) {
         /*   int CodigoListaPrecioVenta;
            if (cmbBuscar.SelectedIndex == 0) {
                if (int.TryParse(txtConsulta.Text, out CodigoListaPrecioVenta)) {
                    connArt.ConsultaListaPrecioVenta("SELECT IdListaPrecio AS 'Código', Descripcion FROM ListaPrecios WHERE IdListaPrecio = " + CodigoListaPrecioVenta + "", "ListaPrecios");
                    gridListaPrecioVenta.DataSource = connArt.ds.Tables["ListaPrecios"];
                }
                else
                    MessageBox.Show("Error: El Código debe ser un valor numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (cmbBuscar.SelectedIndex == 1) {
                connArt.ConsultaListaPrecioVenta("SELECT IdListaPrecio AS 'Código', Descripcion, Porcentaje FROM ListaPrecios WHERE Descripcion LIKE '%" + txtConsulta.Text + "%' ORDER BY IdListaPrecio", "ListaPrecios");
                gridListaPrecioVenta.DataSource = connArt.ds.Tables["ListaPrecios"];
            }*/
        }

        private void btcCerrar_Click(object sender, EventArgs e)  {
            gpoCliente.Visible = false;
            //gridListaPrecioVenta.Height = 300;
            lvwListaPrecioVenta.Height = 300;                       
        }

        private void btnGuardar_Click(object sender, EventArgs e)  {
            gpoCliente.Visible = false;
            //gridListaPrecioVenta.Height = 300;
            lvwListaPrecioVenta.Height = 300;

            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;

            connArt.DesconectarBD();
            connArt.DesconectarBDLee();

            if (txtDescripListaPrecio.Text.Trim() != "") {
                string agregar = "INSERT INTO ListaPrecios values('" + txtDescripListaPrecio.Text.Trim() + "', " + Convert.ToInt32(txtPorcent.Text.Trim()) + ", " + Convert.ToInt32(txtPorcentFlete.Text.Trim()) + ", '" + txtCriterio.Text + "')";
                if (connArt.InsertarListaPrecioVenta(agregar))
                {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados");
                }
                else
                    MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("El campo descripcion esta vacio", "Adventencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnModificar_Click(object sender, EventArgs e) {
            //gpoCliente.Visible = false;
           // gridListaPrecioVenta.Height = 300;
            //this.lvwListaPrecioVenta.Height = 300;

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;

            string actualizar = "Descripcion='" + txtDescripListaPrecio.Text + "', Porcentaje='" + txtPorcent.Text + "', PorcFlete='"+ txtPorcentFlete.Text +"', Criterio='"+ txtCriterio.Text + "'";
            if (connArt.ActualizarListaPrecioVenta("ListaPrecios", actualizar, " IdListaPrecio = " + txtCodigo.Text)) {
                MostrarDatos();
                MessageBox.Show("Datos Actualizados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error al Actualizar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnEliminar_Click(object sender, EventArgs e) {
            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;

            if (connArt.EliminarListaPrecioVenta("ListaPrecios", " IdListaPrecio = " + txtCodigo.Text)) {
                MostrarDatos();
                MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error al Eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
                
        private void txtPorcent_Leave(object sender, EventArgs e)  {
            this.txtPorcent.Text = this.txtPorcent.Text.Trim();            
        }

        private void txtPorcent_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar.ToString() == ".") {
                e.Handled = true;
                this.txtPorcent.Text += ",";
                SendKeys.Send("{END}");
            }
        }

      /*  private void gridListaPrecioVenta_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            string CodListaPrecio;
            DataGridViewRow dbGrid = gridListaPrecioVenta.Rows[e.RowIndex];
            CodListaPrecio = dbGrid.Cells[0].Value.ToString();
            gridListaPrecioVenta.Text = dbGrid.Cells[1].Value.ToString();

            connArt.LeeListaPrecioVenta("SELECT * FROM ListaPrecios WHERE IdListaPrecio = '" + CodListaPrecio + "'", "ListaPrecios");

            this.txtCodigo.Text = connArt.leerListaArticuloVenta["IdListaPrecio"].ToString();
            this.txtDescripListaPrecio.Text = connArt.leerListaArticuloVenta["Descripcion"].ToString();
            this.txtPorcent.Text = connArt.leerListaArticuloVenta["Porcentaje"].ToString();

            connArt.DesconectarBDLee();   
        }*/

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvwListaPrecioVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                connArt.DesconectarBDLee();
                connArt.DesconectarBD();

                connArt.LeeArticulo("SELECT * FROM LISTAPRECIOS WHERE IDLISTAPRECIO = " + Convert.ToInt32(lvwListaPrecioVenta.SelectedItems[0].SubItems[0].Text) + "", "LISTAPRECIOS");

                this.txtCodigo.Text = connArt.leer["IDLISTAPRECIO"].ToString();
                this.txtDescripListaPrecio.Text = connArt.leer["DESCRIPCION"].ToString();
                this.txtPorcentFlete.Text = connArt.leer["PORCFLETE"].ToString();
                this.txtPorcent.Text = connArt.leer["PORCENTAJE"].ToString();
                this.txtCriterio.Text = connArt.leer["CRITERIO"].ToString();

                connArt.DesconectarBDLee();
                connArt.DesconectarBD();

                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                //btnGuardar.Enabled = true;
                //MostrarItemsDatos();
            }
            catch { connArt.DesconectarBD(); } 
        }

        private void lvwListaPrecioVenta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CargaDatos();
        }

        private void CargaDatos()
        {
            try
            {
                pasarListaVendeCod1(Convert.ToInt32(this.lvwListaPrecioVenta.SelectedItems[0].SubItems[0].Text));
                pasarListaVendeDesc1(lvwListaPrecioVenta.SelectedItems[0].SubItems[1].Text);
                this.Close();

            }
            catch { }
        }



    }
}
