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
    public partial class frmTipoIBArticulo : Form
    {
        public delegate void pasar1111(int CodTipoArt);
        public event pasar1111 pasado1111;
        public delegate void pasar2222(string DescripcionTipoArt);
        public event pasar2222 pasado2222;

        public frmTipoIBArticulo()
        {
            InitializeComponent();
        }

        ArticulosBD conn = new ArticulosBD();

        public void MostrarDatos() {
            conn.consultaTipoArticulo("Select IDTipoArticulo As 'Código', Descripcion As 'Tipo Articulo' From TipoArticulo Order By Descripcion", "TipoArticulo");
            gridTipoArticulo.DataSource = conn.ds.Tables["TipoArticulo"];
        }

        private void frmTipoArticulo_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridTipoArticulo.Height = 350;

            conn.ConectarBD();
            MostrarDatos();

            cboBuscar.SelectedIndex = 0;
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            gridTipoArticulo.Height = 240;
            txtCodigo.Text = "Automático"; txtDescripcion.Text = "";

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private void tsBtnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            gridTipoArticulo.Height = 240;

            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = false;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e) {
            int CodigoTipoArticulo;
            if (cboBuscar.SelectedIndex == 0) {
                if (int.TryParse(txtConsulta.Text, out CodigoTipoArticulo)) {
                    conn.consultaTipoArticulo("Select IDTipoArticulo As 'Código', Descripcion As 'Tipo Artículo' FROM TipoArticulo WHERE IDTipoArticulo = " + CodigoTipoArticulo + "", "TipoArticulo");
                    gridTipoArticulo.DataSource = conn.ds.Tables["TipoArticulo"];
                }
                else
                    MessageBox.Show("Error: El Código debe ser un valor numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (cboBuscar.SelectedIndex == 1) {
                conn.consultaTipoArticulo("Select IDTipoArticulo As 'Código', Descripcion As 'Tipo Artículo' FROM TipoArticulo WHERE Descripcion LIKE '" + txtConsulta.Text + "%' Order By descripcion", "TipoArticulo");
                gridTipoArticulo.DataSource = conn.ds.Tables["TipoArticulo"];
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridTipoArticulo.Height = 350;

            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;

            if (txtDescripcion.Text.Trim() != "")
            {
                string agregar = "insert into TipoArticulo values('" + txtDescripcion.Text + "')";
                if (conn.InsertarTipoArticulo(agregar))
                {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("El campo descripcion esta vacio", "Adventencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridTipoArticulo.Height = 350;

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;

            string actualizar = "Descripcion='" + txtDescripcion.Text + "'";
            if (conn.ActualizarTipoArticulo("TipoArticulo", actualizar, " IdTipoArticulo = " + txtCodigo.Text))
            {
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

            if (conn.EliminarTipoArticulo("TipoArticulo", " IDTipoArticulo = " + txtCodigo.Text))
            {
                MostrarDatos();
                MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error al Eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCerrar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridTipoArticulo.Height = 350;

            tsBtnNuevo.Enabled = true;
            tsBtnModificar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void gridTipoArticulo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            try {
                pasado1111(Int16.Parse(this.txtCodigo.Text));  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                pasado2222(this.txtDescripcion.Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch { }
        }

        private void gridTipoArticulo_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            DataGridViewRow dbGrid = gridTipoArticulo.Rows[e.RowIndex];
            txtCodigo.Text = dbGrid.Cells[0].Value.ToString();
            txtDescripcion.Text = dbGrid.Cells[1].Value.ToString();
        }

        private void frmTipoArticulo_FormClosed(object sender, FormClosedEventArgs e) {
            conn.DesconectarBD();
        }

        private void gridTipoArticulo_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            DataGridViewRow dbGrid = gridTipoArticulo.Rows[e.RowIndex];
            txtCodigo.Text = dbGrid.Cells[0].Value.ToString();
            txtDescripcion.Text = dbGrid.Cells[1].Value.ToString();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
