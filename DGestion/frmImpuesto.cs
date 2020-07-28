using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DGestion.Clases;

namespace DGestion
{
    public partial class frmImpuestos : Form
    {
        public delegate void pasarImpuestoCod(int CodImpuesto);
        public event pasarImpuestoCod pasarImpuestoCod1;

        public delegate void pasarImpuestoDesc(string DescripcionImpuesto);
        public event pasarImpuestoDesc pasarImpuestoDesc1;

        public delegate void pasarImpuestoAlic(string DescripcionAlicuota);
        public event pasarImpuestoAlic pasarImpuestoAlicuota1;


        public frmImpuestos() {
            InitializeComponent();
        }

        ImpuestosBD conn = new ImpuestosBD();

        private void frmImpuestos_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridImpuestos.Height = 305;
            
            conn.ConectarBD();
            MostrarDatos();

            cmbBuscar.SelectedIndex = 0;
        }

        public void MostrarDatos() {
            conn.LeeImpuesto("SELECT IdImpuesto As 'Código', Descripcion AS 'Tipo de Impuesto', Abreviatura, Alicuota FROM Impuesto ORDER BY IdImpuesto", "Impuesto");
            gridImpuestos.DataSource = conn.ds.Tables["Impuesto"];
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
           
            gpoCliente.Visible = true;
            gridImpuestos.Height = 185;
            txtCodigo.Text = "Automático"; txtDescripImpuesto.Text = ""; txtAbreviatura.Text = "";
            txtAlicuota.Text = "";
            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;         
        }

        private void btcCerrar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridImpuestos.Height = 305;
        }

        private void btnGuardar_Click(object sender, EventArgs e) {

            gpoCliente.Visible = false;
            gridImpuestos.Height = 305;

            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;

            if (txtDescripImpuesto.Text.Trim() != "") {
                string agregar = "INSERT INTO Impuesto values('" + txtDescripImpuesto.Text + "','" + txtAbreviatura.Text + "', '" + txtAlicuota.Text + "')";
                if (conn.InsertarImpuesto(agregar)) {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("El campo descripcion esta vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void gridImpuestos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            try {
                pasarImpuestoCod1(Int16.Parse(this.txtCodigo.Text));
                pasarImpuestoDesc1(this.txtDescripImpuesto.Text);
                pasarImpuestoAlicuota1(this.txtAlicuota.Text);
                    this.Close();
                }
            catch { this.Close(); }
        }

        private void gridImpuestos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            string CodTipoImpuesto;
            DataGridViewRow dbGrid = gridImpuestos.Rows[e.RowIndex];
            CodTipoImpuesto = dbGrid.Cells[0].Value.ToString();
            gridImpuestos.Text = dbGrid.Cells[1].Value.ToString();

            conn.ConsultaImpuesto("SELECT * FROM Impuesto WHERE IdImpuesto = '" + CodTipoImpuesto + "'", "Impuesto");

            this.txtCodigo.Text = conn.leerImp["IdImpuesto"].ToString();
            this.txtDescripImpuesto.Text = conn.leerImp["Descripcion"].ToString();
            this.txtAbreviatura.Text = conn.leerImp["Abreviatura"].ToString();
            this.txtAlicuota.Text = conn.leerImp["Alicuota"].ToString();

            conn.DesconectarBDLeeImp();   
        }

        private void btnModificar_Click(object sender, EventArgs e) {

            gpoCliente.Visible = false;
            gridImpuestos.Height = 310;            
            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;

            string actualizar = "Descripcion='" + txtDescripImpuesto.Text + "', Abreviatura='" + txtAbreviatura.Text + "', Alicuota = '" + txtAlicuota.Text + "'";
            if (conn.ActualizaImpuesto("Impuesto", actualizar, " IdImpuesto = " + txtCodigo.Text)) {
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

            if (conn.EliminarImpuesto("Impuesto", " IdImpuesto = " + txtCodigo.Text)) {
                MostrarDatos();
                MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error al Eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tsBtnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            gridImpuestos.Height = 185;

            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = false;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;  
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e) {
            int CodigoImpuesto;
            if (cmbBuscar.SelectedIndex == 0) {
                if (int.TryParse(txtConsulta.Text, out CodigoImpuesto)) {
                    conn.LeeImpuesto("Select IDtipoIva As 'Código', Descripcion As 'Tipo Iva' FROM TipoIva WHERE IdTipoIva = " + CodigoImpuesto + "", "TipoIva");
                    gridImpuestos.DataSource = conn.ds.Tables["TipoIva"];
                }
                else
                    MessageBox.Show("Error: El Código debe ser un valor numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (cmbBuscar.SelectedIndex == 1) {
                conn.LeeImpuesto("Select IdImpuesto As 'Código', Descripcion FROM Impuesto WHERE Descripcion LIKE '%" + txtConsulta.Text + "%' ORDER BY IdImpuesto", "Impuesto");
                gridImpuestos.DataSource = conn.ds.Tables["Impuesto"];
            }
        }

        private void txtDescripImpuesto_Leave(object sender, EventArgs e) {
            this.txtDescripImpuesto.Text = this.txtDescripImpuesto.Text.Trim();
        }

        private void txtAbreviatura_Leave(object sender, EventArgs e) {
            this.txtAbreviatura.Text = this.txtAbreviatura.Text.Trim();
        }

        private void txtAlicuota_Leave(object sender, EventArgs e) {
            this.txtAlicuota.Text = this.txtAlicuota.Text.Trim();
        }

        private void txtAlicuota_KeyPress(object sender, KeyPressEventArgs e)  {
            if (e.KeyChar.ToString() == ".") {
                e.Handled = true;
                this.txtAlicuota.Text += ",";
                SendKeys.Send("{END}");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
