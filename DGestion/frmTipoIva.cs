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
    public partial class frmTipoIva : Form
    {

        public delegate void pasarTipoIvaCod(int CodImpuesto);
        public event pasarTipoIvaCod pasarTipoIvaCod1;
        public delegate void pasarTipoIvaDesc(string DescripcionImpuesto);
        public event pasarTipoIvaDesc pasarTipoIvaDesc1;

        public frmTipoIva() {
            InitializeComponent();
        }

        ImpuestosBD conn = new ImpuestosBD();

        private void frmTipoIva_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridTipoIva.Height = 310;

            conn.ConectarBD();
            MostrarDatos();

            cmbBuscar.SelectedIndex = 0;
        }

        private void frmTipoIva_FormClosed(object sender, FormClosedEventArgs e) {
            conn.DesconectarBD();
        }

        public void MostrarDatos() {
            conn.LeeImpuesto("SELECT IDtipoIva As 'Código', Descripcion AS 'Tipo de Iva' FROM TipoIva ORDER BY IdTipoIva", "TipoIva");
            gridTipoIva.DataSource = conn.ds.Tables["TipoIva"];
        }

        private void btnTipoIva_Click(object sender, EventArgs e) {
            frmImpuestos formImpuestos = new frmImpuestos();
            formImpuestos.ShowDialog();
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            gridTipoIva.Height = 195;
            txtCodigo.Text = "Automático"; txtDescripTipoIva.Text = ""; txtCodImpuesto.Text = "";
            
            if(this.cboImpuesto.Text.Trim()!="")
                cboImpuesto.Text="";

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;            
        }

        private void tsBtnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            gridTipoIva.Height = 195;

            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = false;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;       
        }

        private void btcCerrar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridTipoIva.Height = 310;
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e) {
            int CodigoTipoIva;
            if (cmbBuscar.SelectedIndex == 0) {
                if (int.TryParse(txtConsulta.Text, out CodigoTipoIva)) {
                    conn.LeeImpuesto("Select IDtipoIva As 'Código', Descripcion As 'Tipo Iva' FROM TipoIva WHERE IdTipoIva = " + CodigoTipoIva + "", "TipoIva");
                    gridTipoIva.DataSource = conn.ds.Tables["TipoIva"];
                }
                else
                    MessageBox.Show("Error: El Código debe ser un valor numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (cmbBuscar.SelectedIndex == 1) {
                conn.LeeImpuesto("Select IdTipoIva As 'Código', Descripcion As 'Tipo Iva' FROM TipoIva WHERE Descripcion LIKE '%" + txtConsulta.Text + "%' ORDER BY IdTipoIva", "TipoIva");
                gridTipoIva.DataSource = conn.ds.Tables["TipoIva"];
            }
        }

        private void btnImpuesto_Click(object sender, EventArgs e) {
            frmImpuestos formImp = new frmImpuestos();
            formImp.pasarImpuestoCod1 += new frmImpuestos.pasarImpuestoCod(CodImp);  //Delegado11 Rubro Articulo
            formImp.pasarImpuestoDesc1 += new frmImpuestos.pasarImpuestoDesc(DescImp); //Delegado2 Rubro Articulo
            formImp.pasarImpuestoAlicuota1 += new frmImpuestos.pasarImpuestoAlic(Alic);
            formImp.ShowDialog();
        }

        //Metodos de delegado Tipo Articulo
        public void CodImp(int dato1) {
            this.txtCodImpuesto.Text = dato1.ToString();
        }

        public void DescImp(string dato2) {
            this.cboImpuesto.Text = dato2.ToString();
        }

        public void Alic(string Dato3)
        {
        }

        //

        private void gridTipoIva_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            try {
                pasarTipoIvaCod1(Int16.Parse(this.txtCodigo.Text));
                pasarTipoIvaDesc1(this.txtDescripTipoIva.Text);
                this.Close();
            }
            catch { }
        }

        private void gridTipoIva_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            string CodTipoIva;
            DataGridViewRow dbGrid = gridTipoIva.Rows[e.RowIndex];
            CodTipoIva = dbGrid.Cells[0].Value.ToString();
            txtDescripTipoIva.Text = dbGrid.Cells[1].Value.ToString();

            conn.ConsultaImpuesto("SELECT * FROM TipoIva WHERE IdTipoIva = '" + CodTipoIva + "'", "TipoIva");

            this.txtCodigo.Text = conn.leerImp["IdTipoIva"].ToString();
            this.txtDescripTipoIva.Text = conn.leerImp["Descripcion"].ToString();
            this.txtCodImpuesto.Text = conn.leerImp["IdImpuesto"].ToString();

            conn.DesconectarBDLeeImp();   
        }

        private void txtCodImpuesto_TextChanged(object sender, EventArgs e) {
            try {
                if (this.txtCodImpuesto.Text.Trim() != "") {
                    conn.ConsultaTipoImpuestos("Select IdImpuesto As 'Código', Descripcion As 'Tipo Impuesto' FROM Impuesto WHERE IdImpuesto = " + txtCodImpuesto.Text + "", "Impuesto");

                    this.cboImpuesto.DataSource = conn.ds.Tables[0];
                    this.cboImpuesto.ValueMember = "Código";
                    this.cboImpuesto.DisplayMember = "Tipo Impuesto";
                }
                else
                    cboImpuesto.Text = "";
            }
            catch { }
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            
            gpoCliente.Visible = false;
            gridTipoIva.Height = 310;

            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;

            if (txtDescripTipoIva.Text.Trim() != "")
            {
                string agregar = "INSERT INTO TipoIva values('" + txtDescripTipoIva.Text + "'," + txtCodImpuesto.Text + ")";
                if (conn.InsertarImpuesto(agregar)) {
                    MostrarDatos();
                    MessageBox.Show("Datos Agregados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("El campo descripcion esta vacio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridTipoIva.Height = 310;

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;

            string actualizar = "Descripcion='" + txtDescripTipoIva.Text + "'";
            if (conn.ActualizaImpuesto("TipoIva", actualizar, " IdTipoIva = " + txtCodigo.Text)) {
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

            if (conn.EliminarImpuesto("TipoIva", " IdTipoIva = " + txtCodigo.Text)) {
                MostrarDatos();
                MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error al Eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }                                   

    }
}
