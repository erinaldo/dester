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
    public partial class frmRubroArticulo : Form
    {
         // Declaraciones para delegados de datos  
        public delegate void pasar11(int CodRubro);
        public event pasar11 pasado11;
        public delegate void pasar22(string DescripcionRubro);
        public event pasar22 pasado22;

        public frmRubroArticulo() {
            InitializeComponent();
        }

        //string connstr = DataConexion.Utility.GetConnectionString(); //String de Conexion
        ArticulosBD conn = new ArticulosBD();
                     
        private void frmFamiliaArticulo_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridRubroArticulo.Height = 350;

            conn.ConectarBD();
            MostrarDatos();

            cmbBuscar.SelectedIndex = 0;
        }

        public void MostrarDatos() {
            conn.consultaRubroArticulo("Select IDRubro As 'Código', Descripcion As 'Rubro de Articulo' From Rubro Order By Descripcion", "Rubro");
            gridRubroArticulo.DataSource = conn.ds.Tables["Rubro"];
        }

        private void frmRubroArticulo_FormClosed(object sender, FormClosedEventArgs e) {
            conn.DesconectarBD();
        }

        private void gridRubroArticulo_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            DataGridViewRow dbGrid = gridRubroArticulo.Rows[e.RowIndex];
            txtCodigo.Text = dbGrid.Cells[0].Value.ToString();
            txtDescripcion.Text = dbGrid.Cells[1].Value.ToString();
        }
        
        private void gridRubroArticulo_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            DataGridViewRow dbGrid = gridRubroArticulo.Rows[e.RowIndex];
            txtCodigo.Text = dbGrid.Cells[0].Value.ToString();
            txtDescripcion.Text = dbGrid.Cells[1].Value.ToString();
        }

        private void gridRubroArticulo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            try {
                pasado11(Int16.Parse(this.txtCodigo.Text));  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                pasado22(this.txtDescripcion.Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch  {
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)  {
            gpoCliente.Visible = false;
            gridRubroArticulo.Height = 350;

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;

            string actualizar = "Descripcion='" + txtDescripcion.Text + "'";
            if (conn.ActualizarRubro("Rubro", actualizar, " IdRubro = " + txtCodigo.Text)) {
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

            if (conn.EliminarRubro("Rubro", " IRubro = " + txtCodigo.Text)) {
                MostrarDatos();
                MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error al Eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridRubroArticulo.Height = 350;

            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;

            if (txtDescripcion.Text.Trim() != "") {
                string agregar = "insert into Rubro values('" + txtDescripcion.Text + "')";
                if (conn.InsertarRubro(agregar)) {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("El campo descripcion esta vacio", "Adventencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            gridRubroArticulo.Height = 240;
            txtCodigo.Text = "Automático"; txtDescripcion.Text = "";

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tsBtnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            gridRubroArticulo.Height = 240;

            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = false;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
        }

        private void btcCerrar_Click(object sender, EventArgs e)  {
            gpoCliente.Visible = false;
            gridRubroArticulo.Height = 350;

            tsBtnNuevo.Enabled = true;
            tsBtnModificar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e) {
            int CodigoRubroArticulo;
            if (cmbBuscar.SelectedIndex == 0) {
                if (int.TryParse(txtConsulta.Text, out CodigoRubroArticulo)) {
                    conn.consultaRubroArticulo("Select IDRubro As 'Código', Descripcion As 'Rubro Artículo' FROM Rubro WHERE IDRubro = " + CodigoRubroArticulo + "", "Rubro");
                    gridRubroArticulo.DataSource = conn.ds.Tables["Rubro"];
                }
                else
                    MessageBox.Show("Error: El Código debe ser un valor numérico");
            }

            else if (cmbBuscar.SelectedIndex == 1) {
                conn.consultaRubroArticulo("Select IDRubro As 'Código', Descripcion As 'Rubro Artículo' FROM Rubro WHERE Descripcion LIKE '" + txtConsulta.Text + "%' Order By descripcion", "Rubro");
                gridRubroArticulo.DataSource = conn.ds.Tables["Rubro"];
            }
        }

        private void frmRubroArticulo_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridRubroArticulo.Height = 350;

            conn.ConectarBD();
            MostrarDatos();

            cmbBuscar.SelectedIndex = 0;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }     


    }
}
