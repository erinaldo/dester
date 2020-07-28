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
    public partial class frmRPTNotaPedido : Form
    {
        public int NroNotaPedido { get; set; }

        EmpresaBD connEmpresa = new EmpresaBD();
        int IDEMPRESA;

        public frmRPTNotaPedido()
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

        private void frmRPTNotaPedido_Load(object sender, EventArgs e)
        {
            NroNotaPedido = frmNotaPedido.nroNotaPedido;

            IDEMPRESA = ConsultaEmpresa(); //Lee Empresa

            empresaActivaTableAdapter.Fill(this.dGestionDTGeneral.EmpresaActiva, IDEMPRESA, frmPrincipal.PtoVenta.Trim());

            notaPedidoClienteTableAdapter.Fill(this.dGestionDTGeneral.NotaPedidoCliente, NroNotaPedido); ///Remito Impresa
            //detalleRemitoClienteTableAdapter.Fill(this.dGestionDTGeneral.DetalleRemitoCliente, NroRemitoInt); ///Remito Impresa
            this.rptNotaPedido.RefreshReport();
        }
    }
}
