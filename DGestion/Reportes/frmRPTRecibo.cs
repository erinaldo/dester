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
using DGestion.Clases;
using System.IO;
using System.Threading;

namespace DGestion.Reportes
{
    public partial class frmRPTRecibo : Form
    {
        public string NroRecibo { get; set; }
        public int NroReciboInt { get; set; }

        CGenericBD connGeneric = new CGenericBD();
        EmpresaBD connEmpresa = new EmpresaBD();

        string sPtoVta;

        public frmRPTRecibo()
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
                sPtoVta = frmPrincipal.PtoVenta.Trim();

                return IdEmpresa;
            }
            catch { return 0; }
        }

        private void frmRPTRecibo_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dGestionDTGeneral.RecibosRealizados' Puede moverla o quitarla según sea necesario.
            //this.recibosRealizadosTableAdapter.Fill(this.dGestionDTGeneral.RecibosRealizados);
            //this.rptRecibosRealizados.RefreshReport();

            int IdEmpresa;

            IdEmpresa = ConsultaEmpresa();

            NroRecibo = frmRecibo.nroRecibo;
            NroReciboInt = frmRecibo.NroReciboInt;

            recibosRealizadosTableAdapter.Fill(this.dGestionDTGeneral.RecibosRealizados, NroRecibo, IdEmpresa, sPtoVta); ///Remito Impresa
            detalleReciboRealizadoTableAdapter.Fill(this.dGestionDTGeneral.DetalleReciboRealizado, NroReciboInt); ///Remito Impresa
            this.rptRecibosRealizados.RefreshReport();
        }
    }
}
