using DGestion.Clases;
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
    public partial class frmTipoPersonal : Form
    {
        public frmTipoPersonal() {
            InitializeComponent();
        }

        CGenericBD conn = new CGenericBD();

        public void MostrarDatos()
        {
            conn.ConsultaGeneric("Select IdTipoPersonal As 'Código', Descripcion As 'Tipo Factura' FROM TipoPersonal ORDER BY Código", "TipoPersonal");
            gridTipoPersonal.DataSource = conn.ds.Tables["TipoPersonal"];
        }

        private void frmTipoPersonal_Load(object sender, EventArgs e) {
            gpoTPersonal.Visible = false;
            gridTipoPersonal.Height = 330;

            cboBuscaPersonal.SelectedIndex = 0;

            conn.ConectarBD();
            MostrarDatos();
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoTPersonal.Visible = true;
            gridTipoPersonal.Height = 235;
        }

        private void btnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            gpoTPersonal.Visible = false;
            gridTipoPersonal.Height = 330;

            string agregar = "insert into TipoPersonal values('" + cboBuscaPersonal.Text + "')";
            if (conn.InsertarGeneric(agregar))
            {
                MostrarDatos();
                MessageBox.Show("Datos Agregados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);    
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            gpoTPersonal.Visible = false;
            gridTipoPersonal.Height = 330;
        }

        private void gpoTPersonal_Enter(object sender, EventArgs e)
        {

        }        

    }
}
