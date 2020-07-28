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
    public partial class frmTipoIVAProducto : Form
    {
        public delegate void pasar11111(int CodTipoProd);
        public event pasar11111 pasado11111;
        public delegate void pasar22222(string DescripcionTipoProd);
        public event pasar22222 pasado22222;

        public frmTipoIVAProducto()
        {
            InitializeComponent();
        }

        ArticulosBD conn = new ArticulosBD();

        public void MostrarDatos() {
            conn.consultaTipoArticulo("Select IDTipoProducto As 'Código', Descripcion As 'Tipo Producto' From TipoProducto Order By Descripcion", "TipoProducto");
            gridTipoProducto.DataSource = conn.ds.Tables["TipoProducto"];
        }

        private void frmTipoProducto_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridTipoProducto.Height = 350;

            conn.ConectarBD();
            MostrarDatos();

            cboBuscar.SelectedIndex = 0;
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            gridTipoProducto.Height = 240;
            txtCodigo.Text = "Automático"; txtDescripcion.Text = "";

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private void tsBtnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            gridTipoProducto.Height = 240;

            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = false;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e) {
            int CodigoTipoProducto;
            if (cboBuscar.SelectedIndex == 0) {
                if (int.TryParse(txtConsulta.Text, out CodigoTipoProducto)) {
                    conn.consultaTipoProducto("Select IDTipoProducto As 'Código', Descripcion As 'Tipo Producto' FROM TipoProducto WHERE IDTipoProducto = " + CodigoTipoProducto + "", "TipoProducto");
                    gridTipoProducto.DataSource = conn.ds.Tables["TipoProducto"];
                }
                else
                    MessageBox.Show("Error: El Código debe ser un valor numérico");
            }

            else if (cboBuscar.SelectedIndex == 1) {
                conn.consultaTipoProducto("Select IDTipoProducto As 'Código', Descripcion As 'Tipo Producto' FROM TipoProducto WHERE Descripcion LIKE '" + txtConsulta.Text + "%' Order By descripcion", "TipoProducto");
                gridTipoProducto.DataSource = conn.ds.Tables["TipoProducto"];
            }
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)  {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridTipoProducto.Height = 350;

            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;

            if (txtDescripcion.Text.Trim() != "") {
                string agregar = "insert into TipoProducto values('" + txtDescripcion.Text + "')";
                if (conn.InsertarTipoProducto(agregar)) {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados");
                }
                else
                    MessageBox.Show("Error al Agregar");
            }
            else
                MessageBox.Show("El campo descripcion esta vacio");
        }

        private void btnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridTipoProducto.Height = 350;

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;

            string actualizar = "Descripcion='" + txtDescripcion.Text + "'";
            if (conn.ActualizarTipoProducto("TipoProducto", actualizar, " IdTipoProducto = " + txtCodigo.Text))
            {
                MostrarDatos();
                MessageBox.Show("Datos Actualizados");
            }
            else
                MessageBox.Show("Error al Actualizar datos");
        }

        private void btnEliminar_Click(object sender, EventArgs e) {
            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;

            if (conn.EliminarTipoProducto("TipoProducto", " IDTipoProducto = " + txtCodigo.Text)) {
                MostrarDatos();
                MessageBox.Show("Datos Eliminados");
            }
            else
                MessageBox.Show("Error al Eliminar");
        }

        private void btnCerrar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridTipoProducto.Height = 350;

            tsBtnNuevo.Enabled = true;
            tsBtnModificar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void gridTipoProducto_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            DataGridViewRow dbGrid = gridTipoProducto.Rows[e.RowIndex];
            txtCodigo.Text = dbGrid.Cells[0].Value.ToString();
            txtDescripcion.Text = dbGrid.Cells[1].Value.ToString();
        }

        private void gridTipoProducto_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            DataGridViewRow dbGrid = gridTipoProducto.Rows[e.RowIndex];
            txtCodigo.Text = dbGrid.Cells[0].Value.ToString();
            txtDescripcion.Text = dbGrid.Cells[1].Value.ToString();
        }

        private void gridTipoProducto_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            try {
                pasado11111(Int16.Parse(this.txtCodigo.Text));  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                pasado22222(this.txtDescripcion.Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch { }
        }

        private void frmTipoProducto_FormClosed(object sender, FormClosedEventArgs e) {
            conn.DesconectarBD();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }        


    }
}
