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
    public partial class frmRPTListaPrecio : Form
    {
        public frmRPTListaPrecio()
        {
            InitializeComponent();
        }

        int Idlista = 1;

        /*EmpresaBD connEmpresa = new EmpresaBD();
        int IDEMPRESA;
        string sPtoVenta;

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
        }*/

        private void frmRPTListaPrecio_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dGestionDTGeneral.rpt_GralFactu_1' Puede moverla o quitarla según sea necesario.
            //listaPrecioTableAdapter.Fill(this.dGestionDTGeneral.ListaPrecio, Idlista);
            tsCboSelect.SelectedIndex = 0;

            /*tstcboPtoVta.Text = frmPrincipal.PtoVenta.ToString();
            txtcboEmpresa.Text = frmPrincipal.Empresa.ToString();

            sPtoVenta = frmPrincipal.PtoVenta.Trim();
            IDEMPRESA = ConsultaEmpresa(); //Lee Empresa
            */
        }


        private void ProcesaReportes()//(string NumeroFactura, int iTipoFactura, bool bListaFactu)
        {
            try
            {
                if (tsCboSelect.Text.Trim() == "LISTA GENERAL")
                {
                    rptConsultaListaPrecio.Visible = true;
                    Idlista = 1;
                    listaPrecioTableAdapter.Fill(this.dGestionDTGeneral.ListaPrecio, Idlista);
                    this.rptConsultaListaPrecio.RefreshReport();
                }

                else if (tsCboSelect.Text.Trim() == "LISTA ESPECIAL")
                {
                    Idlista = 2;
                    listaPrecioTableAdapter.Fill(this.dGestionDTGeneral.ListaPrecio, Idlista);
                    this.rptConsultaListaPrecio.RefreshReport();
                    rptConsultaListaPrecio.Visible = true;
                }

                else if (tsCboSelect.Text.Trim() == "LISTA PUBLICO")
                {
                    Idlista = 1002;
                    listaPrecioTableAdapter.Fill(this.dGestionDTGeneral.ListaPrecio, Idlista);
                    this.rptConsultaListaPrecio.RefreshReport();
                    rptConsultaListaPrecio.Visible = true;
                }

                else if (tsCboSelect.Text.Trim() == "LISTA CONTADO")
                {
                    Idlista = 2002;
                    listaPrecioTableAdapter.Fill(this.dGestionDTGeneral.ListaPrecio, Idlista);
                    this.rptConsultaListaPrecio.RefreshReport();
                    rptConsultaListaPrecio.Visible = true;
                }
                else if (tsCboSelect.Text.Trim() == "LISTA CON RECARGO")
                {
                    Idlista = 3002;
                    listaPrecioTableAdapter.Fill(this.dGestionDTGeneral.ListaPrecio, Idlista);
                    this.rptConsultaListaPrecio.RefreshReport();
                    rptConsultaListaPrecio.Visible = true;
                }
                else
                    rptConsultaListaPrecio.Visible = false;

            }
            catch { }
        }

        private void txBtnBuscar_Click(object sender, EventArgs e)
        {
            ProcesaReportes();
        }
    }
}
