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
    public partial class frmRPTNotaCredito : Form
    {
        public int NumeroNC { get; set; }
        public int NumeroNCint { get; set; }
        public int TipoNC { get; set; }
        public bool ListaNC { get; set; }

        EmpresaBD connEmpresa = new EmpresaBD();
        int IDEMPRESA;
        string sPtoVenta;

        public frmRPTNotaCredito()
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


        private void ProcesaReportes(string NumeroFactura, int iTipoFactura)
        {
            try
            {

                if (NumeroFactura == "0")
                    MessageBox.Show("Error: No se ha filtrado datos para mostrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (iTipoFactura == 2)
                    {

                        rptNotaCreditoImpreso_B.Visible = true;
                        rptNotaCreditoImpreso_A.Visible = false;


                        //NumeroFactura = lvwFacturaVenta.SelectedItems[0].SubItems[1].Text;      
                        empresaActivaTableAdapter.Fill(this.dGestionDTGeneral.EmpresaActiva, IDEMPRESA, frmPrincipal.PtoVenta.Trim());
                        notaCreditoClienteTableAdapter.Fill(this.dGestionDTGeneral.NotaCreditoCliente, IDEMPRESA, NumeroNC.ToString(), frmPrincipal.PtoVenta.Trim()); ///Factura Impresa
                        detalleNotaCreditoClienteTableAdapter.Fill(this.dGestionDTGeneral.DetalleNotaCreditoCliente, NumeroNCint); ///Factura Impresa

                        this.rptNotaCreditoImpreso_B.RefreshReport();
                    }
                    else if (iTipoFactura == 1)
                    {

                        rptNotaCreditoImpreso_A.Visible = true;
                        rptNotaCreditoImpreso_B.Visible = false;
                        
                        //NumeroFactura = lvwFacturaVenta.SelectedItems[0].SubItems[1].Text;
                        empresaActivaTableAdapter.Fill(this.dGestionDTGeneral.EmpresaActiva, IDEMPRESA, frmPrincipal.PtoVenta.Trim());
                        notaCreditoClienteTableAdapter.Fill(this.dGestionDTGeneral.NotaCreditoCliente, IDEMPRESA, NumeroNC.ToString(), frmPrincipal.PtoVenta.Trim()); ///Factura Impresa
                        detalleNotaCreditoClienteTableAdapter.Fill(this.dGestionDTGeneral.DetalleNotaCreditoCliente, NumeroNCint); ///Factura Impresa

                        this.rptNotaCreditoImpreso_A.RefreshReport();
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

        private void frmRPTNotaCredito_Load(object sender, EventArgs e)
        {

            NumeroNC = frmNotaDeCredito.nroNotaCredito;
            TipoNC = frmNotaDeCredito.tipoNC;
            NumeroNCint = frmNotaDeCredito.nroNCInt;
            sPtoVenta = frmPrincipal.PtoVenta.Trim();

            IDEMPRESA = ConsultaEmpresa(); //Lee Empresa

            // TODO: esta línea de código carga datos en la tabla 'dGestionDTGeneral.NotaCreditoCliente' Puede moverla o quitarla según sea necesario.
            this.notaCreditoClienteTableAdapter.Fill(this.dGestionDTGeneral.NotaCreditoCliente,IDEMPRESA,NumeroNC.ToString(),sPtoVenta.ToString());
            // TODO: esta línea de código carga datos en la tabla 'dGestionDTGeneral.DetalleNotaCreditoCliente' Puede moverla o quitarla según sea necesario.
            this.detalleNotaCreditoClienteTableAdapter.Fill(this.dGestionDTGeneral.DetalleNotaCreditoCliente,NumeroNCint);
            // TODO: esta línea de código carga datos en la tabla 'dGestionDTGeneral.EmpresaActiva' Puede moverla o quitarla según sea necesario.
            this.empresaActivaTableAdapter.Fill(this.dGestionDTGeneral.EmpresaActiva, IDEMPRESA,sPtoVenta.ToString());


            this.rptNotaCreditoImpreso_A.RefreshReport();
            this.rptNotaCreditoImpreso_B.RefreshReport();

            ProcesaReportes(NumeroNC.ToString(), TipoNC);
        }
    }
}
