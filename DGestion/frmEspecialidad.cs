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
    public partial class frmEspecialidad : Form
    {
        public frmEspecialidad()
        {
            InitializeComponent();
        }

        private void frmEspecialidad_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            lvwEspecialidad.Height = 325;

            cboBuscaEspecialidad.SelectedIndex = 0;
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btcCerrar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            lvwEspecialidad.Height = 325;
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            lvwEspecialidad.Height = 325;
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            lvwEspecialidad.Height = 235;
        }


    }
}
