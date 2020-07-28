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
    public partial class frmEstadoCivil : Form
    {
        public frmEstadoCivil()
        {
            InitializeComponent();
        }

        private void frmEstadoCivil_Load(object sender, EventArgs e)  {
            gpoCliente.Visible = false;
            gridEstadoCivil.Height = 335;
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            gridEstadoCivil.Height = 335;
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = true;
            gridEstadoCivil.Height = 240;
        }

        private void btcCerrar_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = false;
            gridEstadoCivil.Height = 335;
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }
    }
}
