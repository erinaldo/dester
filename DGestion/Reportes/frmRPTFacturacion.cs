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
    public partial class frmRPTFacturacion : Form
    {

        public frmRPTFacturacion()
        {
            InitializeComponent();
        }

        public string NumeroFactura { get; set; }
        public int NumeroFacturaInt { get; set; }
        public int  TipoFactura { get; set; }
        public bool ListaFactu { get; set; }

        public bool bListaFacturaClienteFechaVenta { get; set; }


        public string sFechaDesde { get; set; }
        public string sFechaHasta { get; set; }

        EmpresaBD connEmpresa = new EmpresaBD();
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
        }

        private void frmRPTFacturacion_Load(object sender, EventArgs e)
        {            
            NumeroFactura = frmFacturacion.nroComprob;
            NumeroFacturaInt = frmFacturacion.nroComprobInt;
            sPtoVenta = frmPrincipal.PtoVenta.Trim();

            // TODO: esta línea de código carga datos en la tabla 'dGestionDTGeneral1.FacturaVentaCliente' Puede moverla o quitarla según sea necesario.
            this.facturaVentaClienteTableAdapter1.Fill(this.dGestionDTGeneral1.FacturaVentaCliente);

            empresaActivaTableAdapter.Fill(this.dGestionDTGeneral1.EmpresaActiva, IDEMPRESA, frmPrincipal.PtoVenta.Trim());
            facturacionClienteTableAdapter1.Fill(this.dGestionDTGeneral1.FacturacionCliente, IDEMPRESA, NumeroFactura, frmPrincipal.PtoVenta.Trim()); ///Factura Impresa
            detalleFacturacionClienteTableAdapter.Fill(this.dGestionDTGeneral1.DetalleFacturacionCliente, NumeroFacturaInt); ///Factura Impresa       
            

            NumeroFactura = frmFacturacion.nroComprob;
            ListaFactu = frmFacturacion.listaFactu;
            bListaFacturaClienteFechaVenta = frmFacturacion.listaFactuVentaCliente;
            TipoFactura = frmFacturacion.tipoFactu;
            
            IDEMPRESA = ConsultaEmpresa(); //Lee Empresa

            sFechaDesde = frmFacturacion.FechaDesde;
            sFechaHasta = frmFacturacion.FechaHasta;

            facturaVentaFechaClienteTableAdapter.Fill(this.dGestionDTGeneral1.FacturaVentaFechaCliente, Convert.ToDateTime(sFechaDesde), Convert.ToDateTime(sFechaHasta), IDEMPRESA, sPtoVenta);

            ProcesaReportes(NumeroFactura, TipoFactura, ListaFactu);

            rptReporteFacturacion.RefreshReport();
            rptFacturacionImpresion_A.RefreshReport();
            rptFacturacionImpresion_B.RefreshReport();
            rptFacturacionImpresion_X.RefreshReport();
            rptFacturacionImpresion_P.RefreshReport();
            rptFacturaVtaClienteFecha.RefreshReport();
        }

        private void ProcesaReportes(string NumeroFactura, int iTipoFactura, bool bListaFactu)
        {
            try
            {
                if (bListaFacturaClienteFechaVenta == true)
                {
                    rptReporteFacturacion.Visible = false;
                    rptFacturacionImpresion_A.Visible = false;
                    rptFacturacionImpresion_B.Visible = false;
                    rptFacturacionImpresion_X.Visible = false;
                    rptFacturacionImpresion_P.Visible = false;
                    rptFacturaVtaClienteFecha.Visible = true;

                    facturaVentaFechaClienteTableAdapter.Fill(this.dGestionDTGeneral1.FacturaVentaFechaCliente, Convert.ToDateTime(sFechaDesde), Convert.ToDateTime(sFechaHasta), IDEMPRESA, sPtoVenta);
                    this.rptFacturaVtaClienteFecha.RefreshReport();                    
                }
                else
                {
                    if (bListaFactu == true)
                    {
                        //gpReportesFactura.Visible = true;
                        rptReporteFacturacion.Visible = true;
                        rptFacturacionImpresion_A.Visible = false;
                        rptFacturacionImpresion_B.Visible = false;
                        rptFacturacionImpresion_X.Visible = false;
                        rptFacturaVtaClienteFecha.Visible = false;
                        rptFacturacionImpresion_P.Visible = false;

                        this.facturaVentaClienteTableAdapter1.Fill(this.dGestionDTGeneral1.FacturaVentaCliente); //Factura Cliente
                        this.rptReporteFacturacion.RefreshReport();
                    }

                    else
                    {
                        if (NumeroFactura == "0")
                            MessageBox.Show("Error: No se ha filtrado datos para mostrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            if (iTipoFactura == 1) //R.I
                            {
                                rptReporteFacturacion.Visible = false;
                                rptFacturacionImpresion_A.Visible = true;
                                rptFacturacionImpresion_B.Visible = false;
                                rptFacturacionImpresion_X.Visible = false;
                                rptFacturaVtaClienteFecha.Visible = false;
                                rptFacturacionImpresion_P.Visible = false;

                                //NumeroFactura = lvwFacturaVenta.SelectedItems[0].SubItems[1].Text;      
                                empresaActivaTableAdapter.Fill(this.dGestionDTGeneral1.EmpresaActiva, IDEMPRESA, frmPrincipal.PtoVenta.Trim());
                                facturacionClienteTableAdapter1.Fill(this.dGestionDTGeneral1.FacturacionCliente, IDEMPRESA, NumeroFactura.Trim(), frmPrincipal.PtoVenta.Trim()); ///Factura Impresa
                                detalleFacturacionClienteTableAdapter.Fill(this.dGestionDTGeneral1.DetalleFacturacionCliente, NumeroFacturaInt); ///Factura Impresa

                                this.rptFacturacionImpresion_A.RefreshReport();
                            }
                            //else if (iTipoFactura == 2 || iTipoFactura == 3)
                            else if (iTipoFactura == 2) //Consumidor final
                            {
                                rptReporteFacturacion.Visible = false;
                                rptFacturacionImpresion_A.Visible = false;
                                rptFacturacionImpresion_B.Visible = true;
                                rptFacturaVtaClienteFecha.Visible = false;
                                rptFacturacionImpresion_X.Visible = false;
                                rptFacturacionImpresion_P.Visible = false;

                                //NumeroFactura = lvwFacturaVenta.SelectedItems[0].SubItems[1].Text;
                                empresaActivaTableAdapter.Fill(this.dGestionDTGeneral1.EmpresaActiva, IDEMPRESA, frmPrincipal.PtoVenta.Trim());
                                facturacionClienteTableAdapter1.Fill(this.dGestionDTGeneral1.FacturacionCliente, IDEMPRESA, NumeroFactura, frmPrincipal.PtoVenta.Trim()); ///Factura Impresa
                                detalleFacturacionClienteTableAdapter.Fill(this.dGestionDTGeneral1.DetalleFacturacionCliente, NumeroFacturaInt); ///Factura Impresa

                                this.rptFacturacionImpresion_B.RefreshReport();
                            }

                            else if (iTipoFactura == 3) //Exento
                            {
                                rptReporteFacturacion.Visible = false;
                                rptFacturacionImpresion_A.Visible = false;
                                rptFacturacionImpresion_B.Visible = false;
                                rptFacturacionImpresion_E.Visible = true;
                                rptFacturaVtaClienteFecha.Visible = false;
                                rptFacturacionImpresion_X.Visible = false;
                                rptFacturacionImpresion_P.Visible = false;

                                //NumeroFactura = lvwFacturaVenta.SelectedItems[0].SubItems[1].Text;
                                empresaActivaTableAdapter.Fill(this.dGestionDTGeneral1.EmpresaActiva, IDEMPRESA, frmPrincipal.PtoVenta.Trim());
                                facturacionClienteTableAdapter1.Fill(this.dGestionDTGeneral1.FacturacionCliente, IDEMPRESA, NumeroFactura, frmPrincipal.PtoVenta.Trim()); ///Factura Impresa
                                

                                detalleFacturacionClienteTableAdapter.Fill(this.dGestionDTGeneral1.DetalleFacturacionCliente, NumeroFacturaInt); ///Factura Impresa

                                this.rptFacturacionImpresion_E.RefreshReport();
                            }

                            else if (iTipoFactura == 4)
                            {
                                rptReporteFacturacion.Visible = false;
                                rptFacturacionImpresion_A.Visible = false;
                                rptFacturacionImpresion_B.Visible = false;
                                rptFacturacionImpresion_X.Visible = true;
                                rptFacturaVtaClienteFecha.Visible = false;
                                rptFacturacionImpresion_P.Visible = false;

                                //NumeroFactura = lvwFacturaVenta.SelectedItems[0].SubItems[1].Text;
                                empresaActivaTableAdapter.Fill(this.dGestionDTGeneral1.EmpresaActiva, IDEMPRESA, frmPrincipal.PtoVenta.Trim());
                                facturacionClienteTableAdapter1.Fill(this.dGestionDTGeneral1.FacturacionCliente, IDEMPRESA, NumeroFactura, frmPrincipal.PtoVenta.Trim()); ///Factura Impresa
                                detalleFacturacionClienteTableAdapter.Fill(this.dGestionDTGeneral1.DetalleFacturacionCliente, NumeroFacturaInt); ///Factura Impresa

                                this.rptFacturacionImpresion_X.RefreshReport();
                            }

                            else if (iTipoFactura == 5)
                            {
                                rptReporteFacturacion.Visible = false;
                                rptFacturacionImpresion_A.Visible = false;
                                rptFacturacionImpresion_B.Visible = false;
                                rptFacturacionImpresion_X.Visible = false;
                                rptFacturaVtaClienteFecha.Visible = false;
                                rptFacturacionImpresion_P.Visible = true;

                                //NumeroFactura = lvwFacturaVenta.SelectedItems[0].SubItems[1].Text;
                                empresaActivaTableAdapter.Fill(this.dGestionDTGeneral1.EmpresaActiva, IDEMPRESA, frmPrincipal.PtoVenta.Trim());
                                facturacionClienteTableAdapter1.Fill(this.dGestionDTGeneral1.FacturacionCliente, IDEMPRESA, NumeroFactura, frmPrincipal.PtoVenta.Trim()); ///Factura Impresa
                                detalleFacturacionClienteTableAdapter.Fill(this.dGestionDTGeneral1.DetalleFacturacionCliente, NumeroFacturaInt); ///Factura Impresa

                                this.rptFacturacionImpresion_P.RefreshReport();
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                //gpReportesFactura.Visible = false;
                MessageBox.Show("Error: No se ha seleccionado factura a imprimir.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
