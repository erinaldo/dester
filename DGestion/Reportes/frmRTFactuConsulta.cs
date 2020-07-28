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
    public partial class frmRTFactuConsulta : Form
    {
        public frmRTFactuConsulta()
        {
            InitializeComponent();
        }
        
        EmpresaBD connEmpresa = new EmpresaBD();
        int IDEMPRESA;
        string sRazonSocialEmpresa;
        string sPtoVenta;       

        
        private int ConsultaEmpresa()
        {
            try
            {
                connEmpresa.ObtenerEmpresaActiva("SELECT * FROM Empresa WHERE RazonSocial = '" + txtcboEmpresa.Text.Trim() + "'", "Empresa");
                IDEMPRESA = Convert.ToInt32(connEmpresa.leerEmpresa["IdEmpresa"].ToString());
                sRazonSocialEmpresa = connEmpresa.leerEmpresa["RazonSocial"].ToString();
                connEmpresa.DesconectarBDLeeEmpresa();

                return IDEMPRESA;
            }
            catch { return 0; }
        } 

        private void frmRTFactuConsulta_Load(object sender, EventArgs e)
        {
            try
            {
                tsCboSelect.SelectedIndex = 0;
                tsCboMes.SelectedIndex = 0;

                tstcboPtoVta.Text = frmPrincipal.PtoVenta.ToString();
                txtcboEmpresa.Text = frmPrincipal.Empresa.ToString();
                sPtoVenta = frmPrincipal.PtoVenta.Trim();


                sPtoVenta = tstcboPtoVta.Text.Trim();
                IDEMPRESA = ConsultaEmpresa();
                                
                this.rpt_GralFactu_1TableAdapter.Fill(this.dGestionDTGeneral.rpt_GralFactu_1, IDEMPRESA, sPtoVenta, tstxtAño.Text.Trim());
                this.rpt_GralFactu_2TableAdapter.Fill(this.dGestionDTGeneral.rpt_GralFactu_2, IDEMPRESA, sPtoVenta, tstxtAño.Text.Trim());
                this.rpt_GralFactu_3TableAdapter.Fill(this.dGestionDTGeneral.rpt_GralFactu_3, IDEMPRESA, sPtoVenta, tstxtAño.Text.Trim());
                this.rpt_GralFactu_4TableAdapter.Fill(this.dGestionDTGeneral.rpt_GralFactu_4, IDEMPRESA, sPtoVenta, tstxtAño.Text.Trim());

                //Grafico
                //this.rpt_Grafico_1TableAdapter.Fill(this.dGestionDTGeneral.rpt_Grafico_1, IDEMPRESA, sPtoVenta, tstxtAño.Text.Trim());

            }
            catch { }
        }

        private void ProcesaReportes()//(string NumeroFactura, int iTipoFactura, bool bListaFactu)
        {
            try
            {
                if (tsCboSelect.Text.Trim() == "Facturación Anual por Cliente") //ok
                {
                    rptConsultaFacturacion_1.Clear();

                    rptConsultaFacturacion_1.Visible = false;
                    rptConsultaFacturacion_2.Visible = true;
                    rptConsultaFacturacion_3.Visible = false;
                    rptConsultaFacturacion_4.Visible = false;
                    rptGrafico_1.Visible = false;

                    rptConsultaFacturacion_2.Clear();
                    rptConsultaFacturacion_3.Clear();
                    rptConsultaFacturacion_4.Clear();
                    rptGrafico_1.Clear();

                    sPtoVenta = tstcboPtoVta.Text.Trim();
                    IDEMPRESA = ConsultaEmpresa();

                    rpt_GralFactu_1TableAdapter.Fill(this.dGestionDTGeneral.rpt_GralFactu_1, IDEMPRESA, sPtoVenta, tstxtAño.Text.Trim());
                    this.rptConsultaFacturacion_1.RefreshReport();
                    this.rptConsultaFacturacion_2.RefreshReport();
                    this.rptConsultaFacturacion_3.RefreshReport();
                    this.rptConsultaFacturacion_4.RefreshReport();
                    this.rptGrafico_1.RefreshReport();
                }

                else if (tsCboSelect.Text.Trim() == "Facturación Mensual Por Cliente") //ok
                {
                    rptConsultaFacturacion_2.Clear();

                    rptConsultaFacturacion_1.Visible = true;
                    rptConsultaFacturacion_2.Visible = false;
                    rptConsultaFacturacion_3.Visible = false;
                    rptConsultaFacturacion_4.Visible = false;
                    rptGrafico_1.Visible = false;

                    rptConsultaFacturacion_1.Clear();
                    rptConsultaFacturacion_3.Clear();
                    rptConsultaFacturacion_4.Clear();
                    rptGrafico_1.Clear();

                    sPtoVenta = tstcboPtoVta.Text.Trim();
                    IDEMPRESA = ConsultaEmpresa();

                    rpt_GralFactu_2TableAdapter.Fill(this.dGestionDTGeneral.rpt_GralFactu_2, IDEMPRESA, sPtoVenta, tstxtAño.Text.Trim());

                    this.rptConsultaFacturacion_1.RefreshReport();
                    this.rptConsultaFacturacion_2.RefreshReport();
                    this.rptConsultaFacturacion_3.RefreshReport();
                    this.rptConsultaFacturacion_4.RefreshReport();
                    this.rptGrafico_1.RefreshReport();
                }

                else if (tsCboSelect.Text.Trim() == "Total Facturación Mensual y Anual x Cliente")
                {
                    rptConsultaFacturacion_3.Clear();

                    rptConsultaFacturacion_1.Visible = false;
                    rptConsultaFacturacion_2.Visible = false;
                    rptConsultaFacturacion_3.Visible = true;
                    rptConsultaFacturacion_4.Visible = false;
                    rptGrafico_1.Visible = false;

                    rptConsultaFacturacion_1.Clear();
                    rptConsultaFacturacion_2.Clear();
                    rptConsultaFacturacion_4.Clear();
                    rptGrafico_1.Clear();

                    sPtoVenta = tstcboPtoVta.Text.Trim();
                    IDEMPRESA = ConsultaEmpresa();

                    rpt_GralFactu_3TableAdapter.Fill(this.dGestionDTGeneral.rpt_GralFactu_3, IDEMPRESA, sPtoVenta, tstxtAño.Text.Trim());

                    this.rptConsultaFacturacion_1.RefreshReport();
                    this.rptConsultaFacturacion_2.RefreshReport();
                    this.rptConsultaFacturacion_3.RefreshReport();
                    this.rptConsultaFacturacion_4.RefreshReport();
                    this.rptGrafico_1.RefreshReport();
                }
                                

                else if (tsCboSelect.Text.Trim() == "Total de Facturacion Anual x Empresa y Pto.Vta.")
                {

                    rptConsultaFacturacion_4.Clear();

                    rptConsultaFacturacion_1.Visible = false;
                    rptConsultaFacturacion_2.Visible = false;
                    rptConsultaFacturacion_3.Visible = false;
                    rptConsultaFacturacion_4.Visible = true;
                    rptGrafico_1.Visible = false;

                    rptConsultaFacturacion_1.Clear();
                    rptConsultaFacturacion_2.Clear();
                    rptConsultaFacturacion_3.Clear();                    
                    rptGrafico_1.Clear();


                    sPtoVenta = tstcboPtoVta.Text.Trim();
                    IDEMPRESA = ConsultaEmpresa();

                    this.rpt_GralFactu_4TableAdapter.Fill(this.dGestionDTGeneral.rpt_GralFactu_4, IDEMPRESA, sPtoVenta, tstxtAño.Text.Trim());
                                        
                    this.rptConsultaFacturacion_1.RefreshReport();
                    this.rptConsultaFacturacion_2.RefreshReport();
                    this.rptConsultaFacturacion_3.RefreshReport();
                    this.rptConsultaFacturacion_4.RefreshReport();
                    this.rptGrafico_1.RefreshReport();

                }

                else if (tsCboSelect.Text.Trim() == "Gráfico de Fact. Mensual x Empresa y Pto. Vta.")
                {

                    rptGrafico_1.Clear();

                    rptConsultaFacturacion_1.Visible = false;
                    rptConsultaFacturacion_2.Visible = false;
                    rptConsultaFacturacion_3.Visible = false;
                    rptConsultaFacturacion_4.Visible = false;
                    rptGrafico_1.Visible = true;

                    rptConsultaFacturacion_1.Clear();
                    rptConsultaFacturacion_2.Clear();
                    rptConsultaFacturacion_3.Clear();
                                       

                    sPtoVenta = tstcboPtoVta.Text.Trim();
                    IDEMPRESA = ConsultaEmpresa();

                    this.rpt_Grafico_1TableAdapter.Fill(this.dGestionDTGeneral.rpt_Grafico_1, IDEMPRESA, sPtoVenta, tstxtAño.Text.Trim());

                    this.rptGrafico_1.RefreshReport();
                    this.rptConsultaFacturacion_1.RefreshReport();
                    this.rptConsultaFacturacion_2.RefreshReport();
                    this.rptConsultaFacturacion_3.RefreshReport();
                    this.rptConsultaFacturacion_4.RefreshReport();
                }

            }
            catch { }
        }

        private void rptConsultaFacturacion_Load(object sender, EventArgs e)
        {
        }

        private void txBtnBuscar_Click(object sender, EventArgs e)
        {
            ProcesaReportes();
        }
    }
}
