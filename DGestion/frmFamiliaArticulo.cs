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

namespace DGestion
{
    public partial class frmFamiliaArticulo : Form
    {
         // Declaraciones para delegados de datos  
        public delegate void pasar1(int CodFamilia);
        public event pasar1 pasado1;
        public delegate void pasar2(string DescripcionFamilia);
        public event pasar2 pasado2;

        public frmFamiliaArticulo() {
            InitializeComponent();
        }

        //string connstr = DataConexion.Utility.GetConnectionString(); //String de Conexion
        ArticulosBD conn = new ArticulosBD();
                     
        private void frmFamiliaArticulo_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridFamiliaArticulo.Height = 350;

            conn.ConectarBD();
            MostrarDatos();

            cmbBuscar.SelectedIndex = 0;
        }

        public void MostrarDatos() {
            conn.consultaFamiliaArticulo("Select IDFamilia As 'Código', Descripcion As 'Familia de Articulo' From Familia Order By Descripcion", "Familia");
            gridFamiliaArticulo.DataSource = conn.ds.Tables["Familia"];
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridFamiliaArticulo.Height = 350;

            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;

            if (txtDescripcion.Text.Trim() != "") {
                string agregar = "insert into Familia values('" + txtDescripcion.Text + "')";
                if (conn.InsertarFamilia(agregar)) {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados");
                }
                else
                    MessageBox.Show("Error al Agregar");
            }
            else
                MessageBox.Show("El campo descripcion esta vacio");
        }

        private void btcCerrar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridFamiliaArticulo.Height = 350;
            
            tsBtnNuevo.Enabled = true;
            tsBtnModificar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            gridFamiliaArticulo.Height = 240;
            txtCodigo.Text = "Automático"; txtDescripcion.Text = "";

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
        }
       
        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }       

        private void btnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridFamiliaArticulo.Height = 350;

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;           
            tsBtnNuevo.Enabled = true;

            string actualizar = "Descripcion='" + txtDescripcion.Text + "'";
            if (conn.ActualizarFamilia("Familia", actualizar, " IdFamilia = " + txtCodigo.Text))  {
                MostrarDatos();
                MessageBox.Show("Datos Actualizados");
            }
            else
                MessageBox.Show("Error al Actualizar datos");
        }

        private void frmFamiliaArticulo_FormClosed(object sender, FormClosedEventArgs e) {
            conn.DesconectarBD();
        }

        private void tsBtnModificar_Click(object sender, EventArgs e)  {
            gpoCliente.Visible = true;
            gridFamiliaArticulo.Height = 240;

            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = false;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e) {
            int CodigoFamiliaArticulo;
            if (cmbBuscar.SelectedIndex == 0) {
                if (int.TryParse(txtConsulta.Text, out CodigoFamiliaArticulo)) {
                    conn.consultaFamiliaArticulo("Select IDFamilia As 'Código', Descripcion As 'Familia Artículo' From Familia WHERE IDFamilia = " + CodigoFamiliaArticulo + "", "Familia");
                    gridFamiliaArticulo.DataSource = conn.ds.Tables["Familia"];
                }
                else
                    MessageBox.Show("Error: El Código debe ser un valor numérico");
            }

            else if (cmbBuscar.SelectedIndex == 1) {
                conn.consultaFamiliaArticulo("Select IDFamilia As 'Código', Descripcion As 'Familia Artículo' From Familia WHERE Descripcion LIKE '" + txtConsulta.Text + "%' Order By descripcion", "Familia");
                gridFamiliaArticulo.DataSource = conn.ds.Tables["Familia"];
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)  {            
            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;

            if (conn.EliminarFamilia("Familia", " IdFamilia = " + txtCodigo.Text))
            {
                MostrarDatos();
                MessageBox.Show("Datos Eliminados");
            }
            else
                MessageBox.Show("Error al Eliminar");
        }

        private void gridFamiliaArticulo_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            DataGridViewRow dbGrid = gridFamiliaArticulo.Rows[e.RowIndex];
            txtCodigo.Text = dbGrid.Cells[0].Value.ToString();
            txtDescripcion.Text = dbGrid.Cells[1].Value.ToString();
        }

        private void gridFamiliaArticulo_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            DataGridViewRow dbGrid = gridFamiliaArticulo.Rows[e.RowIndex];            
            txtCodigo.Text = dbGrid.Cells[0].Value.ToString();
            txtDescripcion.Text = dbGrid.Cells[1].Value.ToString();
        }

        private void gridFamiliaArticulo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            try {
                pasado1(Int16.Parse(this.txtCodigo.Text));  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                pasado2(this.txtDescripcion.Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch { 
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
