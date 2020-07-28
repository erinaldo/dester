using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DGestion
{
    public partial class frmChangeLog : Form
    {
        public frmChangeLog()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
