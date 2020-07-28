using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DGestion.Clases;
using System.Threading;

namespace DGestion.Reportes
{
    public partial class frmRPTRemito : Form
    {
        public string NroRemito { get; set; }
        public int NroRemitoInt { get; set; }

        EmpresaBD connEmpresa = new EmpresaBD();
        int IDEMPRESA;

        public frmRPTRemito()
        {
            InitializeComponent();
        }

        private int ConsultaEmpresa()
        {
            try
            {
                int IdEmpresa;
                connEmpresa.ObtenerEmpresaActiva("SELECT * FROM Empresa WHERE RazonSocial = '" + frmPrincipal.Empresa + "'", "Empresa");
                IdEmpresa = Convert.ToInt32(connEmpresa.leerEmpresa["IdEmpresa"].ToString());

                connEmpresa.DesconectarBDLeeEmpresa();

                return IdEmpresa;
            }
            catch { return 0; }
        }

        private void frmRPTRemito_Load(object sender, EventArgs e)
        {
            try
            {
                NroRemito = frmRemito.nroRemito;
                NroRemitoInt = frmRemito.nroRemitoInt;

                IDEMPRESA = ConsultaEmpresa(); //Lee Empresa

                empresaActivaTableAdapter.Fill(this.dGestionDTGeneral.EmpresaActiva, IDEMPRESA, frmPrincipal.PtoVenta.Trim());

                remitoClienteTableAdapter.Fill(this.dGestionDTGeneral.RemitoCliente, NroRemito); ///Remito Impresa
                detalleRemitoClienteTableAdapter.Fill(this.dGestionDTGeneral.DetalleRemitoCliente, NroRemitoInt); ///Remito Impresa            

                if (frmRemito.OptRemito == 0)
                {
                    rptRemito.Visible = true;
                    rptRemitoAPreimpreso.Visible = false;
                    this.rptRemito.RefreshReport();
                }
                else
                {
                    rptRemitoAPreimpreso.Visible = true;
                    rptRemito.Visible = false;
                    this.rptRemitoAPreimpreso.RefreshReport();
                }

            }
            catch { }

        }
    }
}
