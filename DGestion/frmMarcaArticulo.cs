using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGestion
{
    public partial class frmMarcaArticulo : Form
    {

        // Declaraciones para delegados de datos  
        public delegate void pasar111(int CodMarca);
        public event pasar111 pasado111;
        public delegate void pasar222(string DescripcionMarca);
        public event pasar222 pasado222;

        public frmMarcaArticulo()
        {
            InitializeComponent();
        }

        //string connstr = DataConexion.Utility.GetConnectionString(); //String de Conexion
        ArticulosBD conn = new ArticulosBD();

     
        public void MostrarDatos() {
            conn.consultaMarcaArticulo("Select IDMarca As 'Código', Descripcion As 'Marca de Articulo' From Marca Order By Descripcion", "Marca");
            gridMarcaArticulo.DataSource = conn.ds.Tables["Marca"];
        }
       

        private void frmMarcaArticulo_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridMarcaArticulo.Height = 350;

            conn.ConectarBD();
            MostrarDatos();

            cboBuscar.SelectedIndex = 0;
        }

        private void btcCerrar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridMarcaArticulo.Height = 350;

            tsBtnNuevo.Enabled = true;
            tsBtnModificar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e) {
            int CodigoMarcaArticulo;
            if (cboBuscar.SelectedIndex == 0)
            {
                if (int.TryParse(txtConsulta.Text, out CodigoMarcaArticulo))
                {
                    conn.consultaMarcaArticulo("Select IDMarca As 'Código', Descripcion As 'Marca Artículo' FROM Marca WHERE IDMarca = " + CodigoMarcaArticulo + "", "Marca");
                    gridMarcaArticulo.DataSource = conn.ds.Tables["Marca"];
                }
                else
                    MessageBox.Show("Error: El Código debe ser un valor numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (cboBuscar.SelectedIndex == 1)
            {
                conn.consultaMarcaArticulo("Select IDMarca As 'Código', Descripcion As 'Marca Artículo' FROM Marca WHERE Descripcion LIKE '" + txtConsulta.Text + "%' Order By descripcion", "Marca");
                gridMarcaArticulo.DataSource = conn.ds.Tables["Marca"];
            }
        }

        private void tsBtnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            gridMarcaArticulo.Height = 240;

            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = false;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            gridMarcaArticulo.Height = 240;
            txtCodigo.Text = "Automático"; txtDescripcion.Text = "";

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridMarcaArticulo.Height = 350;

            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;

            if (txtDescripcion.Text.Trim() != "")
            {
                string agregar = "insert into Marca values('" + txtDescripcion.Text + "')";
                if (conn.InsertarMarca(agregar))
                {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("El campo descripcion esta vacio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnEliminar_Click(object sender, EventArgs e) {
            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;

            if (conn.EliminarMarca("Marca", " IDMarca = " + txtCodigo.Text))
            {
                MostrarDatos();
                MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error al Eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridMarcaArticulo.Height = 350;

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;

            string actualizar = "Descripcion='" + txtDescripcion.Text + "'";
            if (conn.ActualizarMarca("Marca", actualizar, " IdMarca = " + txtCodigo.Text))
            {
                MostrarDatos();
                MessageBox.Show("Datos Actualizados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error al Actualizar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
  
        private void gridMarcaArticulo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            try {
                pasado111(Int16.Parse(this.txtCodigo.Text));  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                pasado222(this.txtDescripcion.Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch {  }
        }

        private void gridMarcaArticulo_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            DataGridViewRow dbGrid = gridMarcaArticulo.Rows[e.RowIndex];
            txtCodigo.Text = dbGrid.Cells[0].Value.ToString();
            txtDescripcion.Text = dbGrid.Cells[1].Value.ToString();
        }

        private void gridMarcaArticulo_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            DataGridViewRow dbGrid = gridMarcaArticulo.Rows[e.RowIndex];
            txtCodigo.Text = dbGrid.Cells[0].Value.ToString();
            txtDescripcion.Text = dbGrid.Cells[1].Value.ToString();
        }

        private void frmMarcaArticulo_FormClosed(object sender, FormClosedEventArgs e) {
            conn.DesconectarBD();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
