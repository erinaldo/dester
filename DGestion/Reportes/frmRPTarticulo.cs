using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DGestion.Reportes;

namespace DGestion.Reportes
{
    public partial class frmRPTarticulo : Form
    {
        public frmRPTarticulo() {
            InitializeComponent();
        }

        private void btcCerrar_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void frmRPTarticulo_Load(object sender, EventArgs e) {
            try {                
                this.articuloTableAdapter.Fill(this.articuloDataSet.Articulo);
                this.rptVisorArticulo.RefreshReport();
            }
            catch (System.Exception ex) {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void txBtnBuscar_Click(object sender, EventArgs e) {
            try  {
                this.articuloTableAdapter.verArticulosPorCodigo(this.articuloDataSet.Articulo, this.tsTXTcodArticulo.Text.Trim());
                this.rptVisorArticulo.RefreshReport();
            }
            catch (System.Exception ex) {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void tsBtnVerTodo_Click(object sender, EventArgs e) {
            try {                
                this.articuloTableAdapter.Fill(this.articuloDataSet.Articulo);
                this.rptVisorArticulo.RefreshReport();
            }
            catch (System.Exception ex) {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void rptVisorArticulo_Load(object sender, EventArgs e) {
            tsCboSelect.SelectedIndex = 0;
        }

    }
}
