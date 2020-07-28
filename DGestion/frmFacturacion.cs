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

namespace DGestion
{
    public partial class frmFacturacion : Form
    {
        public static string nroComprob;
        public static int nroComprobInt;

        public static bool listaFactu;
        public static bool listaFactuVentaCliente = false;

        public static string FechaDesde;
        public static string FechaHasta;

        public static int tipoFactu;

        CGenericBD connGeneric = new CGenericBD();
        EmpresaBD connEmpresa = new EmpresaBD();
        CGenericBD conn = new CGenericBD();
        ArticulosBD connArt = new ArticulosBD();

        Permisos connPermi = new Permisos();

        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        int iNumRemito;

        double dSubTotal;
        double dImpuesto1;
        double dImpuesto2;
        double dImporteTotal;

        double sumaImpuesto1;
        double sumaImpuesto2;
        double sumaNetos;
        double sumaTotales;

        double Impuesto1;
        double Impuesto2;
        double Neto;
        DateTime fechaFacturaVenta;

        int contadorNROfactNuevo;

        bool nuevaFactu = false;
        int idNROFACTUINTERNO;

        int iCantFactusExportados;
        int iCodigoListaPrecioCliente;
        bool nuevoRemito = false;
        int indiceLvwNotaPedido;
        int idArtuculo;

        double porcGeneralLista = 0;
        double procenFleteLista = 0;
        double CostoEnLista = 0;

        double ValorUnitarioArticulo;

        int iTipoFactura;

        int IDEMPRESA;

        bool bExportFactuVenta = false;
        int iExportRemitoFactura = 0;
        int nroFacturaInterCreado = 0;

        int CantItemDetalleRemito = 0;

        string sExento = "";

        bool flagControlUnificacionComprobante = true;

        int iTipoRemito;
        string sPtoVta;

        bool bNumeracion = false;

        int PagAv = 30, PagRe = 1;

        public string NumeroFactura { get; set; }

        private void conPermi()
        {
            try
            {
                string sUsuarioLegueado;
                sUsuarioLegueado = frmPrincipal.Usuario;

                SqlCommand cm = new SqlCommand("SELECT Personal.USUARIO, Personal.NOMBREYAPELLIDO, PermisoModulo.Idmodulo, PermisoControl.IdControl, PermisoControl.Control, EstadoSistema.Descripcion, PermisoUsuario.IdPersonal FROM Personal, PermisoModulo, PermisoControl, PermisoUsuario, EstadoSistema WHERE PermisoUsuario.IdPersonal = Personal.IdPersonal AND PermisoUsuario.IdModulo = PermisoModulo.IdModulo AND PermisoUsuario.IdControl = PermisoControl.IdControl AND PermisoUsuario.IdEstadoSistema = EstadoSistema.IdEstado AND PermisoUsuario.IdModulo = 1 AND Personal.USUARIO = '" + sUsuarioLegueado + "'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows[0]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[0]["Control"].ToString().Trim() == "Actualizar Factura")
                {
                    btnModificar.Enabled = true;
                    tsBtnModificar.Enabled = true;
                }
                else
                {
                    btnModificar.Enabled = false;
                    tsBtnModificar.Enabled = false;
                }

                if (dt.Rows[1]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[1]["Control"].ToString().Trim() == "Eliminar Factura")
                    btnEliminar.Enabled = true;
                else
                    btnEliminar.Enabled = false;

                if (dt.Rows[2]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[2]["Control"].ToString().Trim() == "Habilitar Facturación Manual")
                    gpDetalleFactura.Enabled = true;
                else
                    gpDetalleFactura.Enabled = false;


                if (dt.Rows[3]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[3]["Control"].ToString().Trim() == "Agregar Item de Factura")
                    btnAgregaArt.Enabled = true;
                else
                    btnAgregaArt.Enabled = false;


                if (dt.Rows[4]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[4]["Control"].ToString().Trim() == "Eliminar Item de Factura")
                    btnQuitaArt.Enabled = true;
                else
                    btnQuitaArt.Enabled = false;


                if (dt.Rows[5]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[5]["Control"].ToString().Trim() == "Activar Apertura/Cierre Factura")
                    tsBtnEstadoImpresion.Enabled = true;
                else
                    tsBtnEstadoImpresion.Enabled = false;

                cm.Connection.Close();

            }
            catch { }
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

        private void NumeradorIgualadorCond_Exento_y_Final()
        {
            try
            {
                int nroFactuExento;
                int nroFactuFinal;

                int nrofacturaInteno;

                connGeneric.LeeGeneric("SELECT MAX(NROFACTURA) as 'NROExento' FROM FacturasVentas WHERE IdTipoFactura=3 AND SUCURSAL = '" + sPtoVta.Trim() + "' AND IDEMPRESA = " + IDEMPRESA + " ORDER BY NROExento", "FacturasVentas");
                nroFactuExento = Convert.ToInt32(connGeneric.leerGeneric["NROExento"].ToString());
                connGeneric.LeeGeneric("SELECT MAX(NROFACTURA) as 'NROFinal' FROM FacturasVentas WHERE IDTipoFactura=2 AND SUCURSAL = '" + sPtoVta.Trim() + "' AND IDEMPRESA = " + IDEMPRESA + " ORDER BY NROFinal", "FacturasVentas");
                nroFactuFinal = Convert.ToInt32(connGeneric.leerGeneric["NROFinal"].ToString());

                if (nroFactuExento != nroFactuFinal)
                {
                    if (nroFactuExento > nroFactuFinal)
                    {
                        //Ultimo Movimiento Exento
                        connGeneric.LeeGeneric("SELECT FacturasVentas.NroFacturaInterno,  FacturasVentas.NroFactura FROM FacturasVentas WHERE NroFactura=" + nroFactuFinal + " AND IdTipoFactura=2 AND SUCURSAL = '" + sPtoVta.Trim() + "' AND IDEMPRESA = " + IDEMPRESA + " ORDER BY NROFactura", "FacturasVentas");
                        nrofacturaInteno = Convert.ToInt32(connGeneric.leerGeneric["NroFacturaInterno"].ToString());

                        string actualizaNro = "NroFactura=" + nroFactuExento + "";
                        connGeneric.ActualizaGeneric("FacturasVentas", actualizaNro, " NROFACTURAINTERNO= " + nrofacturaInteno + "");
                    }

                    if (nroFactuFinal > nroFactuExento)
                    {
                        //Ultimo Movimiento Exento
                        connGeneric.LeeGeneric("SELECT FacturasVentas.NroFacturaInterno,  FacturasVentas.NroFactura FROM FacturasVentas WHERE NroFactura=" + nroFactuExento + " AND IdTipoFactura=3 AND SUCURSAL = '" + sPtoVta.Trim() + "' AND IDEMPRESA = " + IDEMPRESA + " ORDER BY NROFactura", "FacturasVentas");
                        nrofacturaInteno = Convert.ToInt32(connGeneric.leerGeneric["NroFacturaInterno"].ToString());

                        string actualizaNro = "NroFactura=" + nroFactuFinal + "";
                        connGeneric.ActualizaGeneric("FacturasVentas", actualizaNro, " NROFACTURAINTERNO= " + nrofacturaInteno + "");
                    }
                }
            }
            catch { }
        }

        public frmFacturacion()
        {
            InitializeComponent();
        }

        private string FormateoFecha()
        {
            DateTimePicker dtr = new DateTimePicker();
            dtr.Value = DateTime.Now;
            return String.Format("{0:d/M/yyyy HH:mm:ss}", dtr.Value);
        }

        private double CalculoPorcentajeListaVenta(double valorArticulo)
        {
            try
            {
                double valorMasArticuloPorcVenta;
                double valorMasArticuloPorcFlete;
                double valorTotalArticulo;

                connGeneric.DesconectarBD(); connGeneric.DesconectarBDLeeGeneric();
                conn.DesconectarBD(); conn.DesconectarBDLeeGeneric();

                //////BUSCO LOS PORCENTAJES DE LA LISTA DE PRECIO DE VENTA///////               
                conn.LeeGeneric("SELECT * FROM ListaPrecios WHERE IDLISTAPRECIO = " + iCodigoListaPrecioCliente + "", "ListaPrecios");
                porcGeneralLista = Convert.ToDouble(conn.leerGeneric["Porcentaje"].ToString());
                procenFleteLista = Convert.ToDouble(conn.leerGeneric["Porcflete"].ToString());

                connGeneric.DesconectarBD(); connGeneric.DesconectarBDLeeGeneric();
                conn.DesconectarBD(); conn.DesconectarBDLeeGeneric();
                ////////////////////////////////////// //////////////////////////////////////

                if (procenFleteLista < 1)
                    valorTotalArticulo = valorArticulo;
                else
                {
                    valorMasArticuloPorcFlete = Math.Round(((valorArticulo * procenFleteLista) / 100), 3);
                    valorTotalArticulo = Math.Round((valorMasArticuloPorcFlete + valorArticulo), 3);
                }

                if (porcGeneralLista < 1)
                    valorTotalArticulo = Math.Round(valorArticulo, 3);
                else
                {
                    valorMasArticuloPorcVenta = Math.Round(((valorTotalArticulo * porcGeneralLista) / 100), 3);
                    valorTotalArticulo = Math.Round((valorMasArticuloPorcVenta + valorTotalArticulo), 3);
                }

                if (cboFormaPago.Text.Trim() == "PAGO MOSTRADOR" || txtCodFormaPago.Text.Trim() == "1")
                    return Math.Ceiling(valorTotalArticulo);
                else
                    return Math.Round(valorTotalArticulo, 3);
            }
            catch { return 0; }
        }

        private void GuardaItemsDatos(bool status, int nroFacturaVtaInter)
        {
            try
            {
                dSubTotal = 0;
                dImpuesto1 = 0;
                dImpuesto2 = 0;
                dImporteTotal = 0;
                sumaImpuesto1 = 0;
                sumaImpuesto2 = 0;
                sumaNetos = 0;
                sumaTotales = 0;
                Impuesto1 = 0;
                Impuesto2 = 0;
                Neto = 0;

                int IdArticulo;
                int IdImpuesto;
                string Codigo;
                string Descripcion;
                string CodigoFabrica;
                double CostoUnitarioArticulo = 0;
                string ProcCostoEnLista;
                int Cantidad = 0;
                double SubTotal = 0;
                double SubTotal2 = 0;
                double Importe = 0;

                int CantActualArticulo;
                int Cant_Pedida;
                double dPorcDescuento;
                double dTotalDescuento;
                //int Cant_Restante;
                //string Observaciones;

                porcGeneralLista = 0;
                procenFleteLista = 0;
                CostoEnLista = 0;

                if (txtCantArticulo.Text.Trim() != "")
                {
                    connGeneric.DesconectarBD(); connGeneric.DesconectarBDLeeGeneric();
                    conn.DesconectarBD(); conn.DesconectarBDLeeGeneric();

                    //////BUSCO VALORES DEL ARTICULO///////
                    conn.LeeGeneric("SELECT * FROM Articulo WHERE CODIGO = '" + txtCodArticulo.Text.Trim() + "'", "Articulo");
                    IdArticulo = Convert.ToInt32(conn.leerGeneric["IDARTICULO"].ToString());
                    Codigo = conn.leerGeneric["Codigo"].ToString();
                    Descripcion = conn.leerGeneric["Descripcion"].ToString();
                    CodigoFabrica = conn.leerGeneric["Codigo"].ToString();

                    CostoUnitarioArticulo = Convert.ToDouble(conn.leerGeneric["Costo"].ToString());
                    ProcCostoEnLista = conn.leerGeneric["ProcCostoEnLista"].ToString();
                    CostoEnLista = Convert.ToDouble(conn.leerGeneric["CostoEnLista"].ToString());


                    Cantidad = Convert.ToInt32(txtCantArticulo.Text.Trim());
                    CantActualArticulo = Convert.ToInt32(conn.leerGeneric["CANT_ACTUAL"].ToString());
                    IdImpuesto = Convert.ToInt32(conn.leerGeneric["IDIMPUESTO"].ToString());

                    Cant_Pedida = Convert.ToInt32(txtCantArticulo.Text.Trim());
                    //Cant_Restante = Convert.ToInt32(txtCantRestante.Text.Trim());

                    /////////////////////////////////////EXENTO/////////////////////////////////////////////

                    ///////////////////////////////////////////////////////////////////////////////////////
                    if (cboFormaPago.Text.Trim() == "PAGO MOSTRADOR" || txtCodFormaPago.Text.Trim() == "1")
                    {
                        ///Redondeo hacia arriba
                        SubTotal = Math.Ceiling((Cant_Pedida * CalculoPorcentajeListaVenta(CostoEnLista)));
                        dPorcDescuento = Convert.ToDouble(txtProcDesc.Text);
                        SubTotal2 = Math.Ceiling((SubTotal - ((SubTotal * dPorcDescuento) / 100)));
                        Importe = Math.Ceiling((CalculoPorcentajeListaVenta(CostoEnLista) * Cant_Pedida));
                        dPorcDescuento = Convert.ToDouble(txtProcDesc.Text);
                        Importe = Math.Ceiling(Importe - ((Importe * dPorcDescuento) / 100));
                    }
                    else
                    {
                        SubTotal = Math.Round((Cant_Pedida * CalculoPorcentajeListaVenta(CostoEnLista)), 3);
                        dPorcDescuento = Convert.ToDouble(txtProcDesc.Text);
                        SubTotal2 = Math.Round((SubTotal - ((SubTotal * dPorcDescuento) / 100)), 2);
                        Importe = Math.Round((CalculoPorcentajeListaVenta(CostoEnLista) * Cant_Pedida), 2);
                        dPorcDescuento = Convert.ToDouble(txtProcDesc.Text);
                        Importe = Math.Round(Importe - ((Importe * dPorcDescuento) / 100), 2);
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////  

                    lvwDetalleFacturaVenta.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(IdArticulo.ToString());
                    item.SubItems.Add(Codigo.ToString());                                                           ///ITEM 0
                    item.SubItems.Add(Descripcion.ToString());                                                      ///ITEM 1
                    item.SubItems.Add(Cant_Pedida.ToString());                                                      ///ITEM 2  

                    item.SubItems.Add("$ " + Math.Round(CalculoPorcentajeListaVenta(CostoEnLista), 3).ToString());  ///ITEM 3

                    item.SubItems.Add("% " + dPorcDescuento.ToString());
                    dTotalDescuento = (SubTotal - SubTotal2);
                    item.SubItems.Add("$ " + Math.Round(dTotalDescuento, 2).ToString());
                    item.SubItems.Add("$ " + Math.Round(Importe, 2).ToString());                                    ///ITEM 4  

                    if (IdImpuesto == 3 || cboTipoFactura.Text.Trim() == "X" || cboTipoFactura.Text.Trim() == "P" || cboTipoFactura.Text == "E")
                    {
                        if (cboFormaPago.Text.Trim() == "PAGO MOSTRADOR" || txtCodFormaPago.Text.Trim() == "1")
                        {
                            Impuesto1 = Math.Ceiling(((SubTotal2 * 1) - SubTotal2));
                            Neto = Math.Ceiling((Importe + Impuesto1));
                            item.SubItems.Add("$ " + Math.Ceiling(Impuesto1).ToString());                              ///ITEM 6A
                            item.SubItems.Add("$ " + Math.Ceiling(Neto).ToString());                                   ///ITEM 7B

                            sumaImpuesto1 += Impuesto1;
                            txtImpuesto1.Text = "$ " + Math.Ceiling(sumaImpuesto1).ToString();

                            sumaNetos += Neto;
                            txtImporteTotal.Text = "$ " + Math.Ceiling(sumaNetos).ToString();
                        }

                        else if (cboTipoFactura.Text == "E")
                        {
                            Impuesto1 = Math.Round(((SubTotal2 * 1) - SubTotal2), 2);
                            Neto = Math.Round((Importe + Impuesto1), 3);
                            item.SubItems.Add("$ " + Math.Round(Impuesto1, 2).ToString());                              ///ITEM 6A
                            item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());                                   ///ITEM 7B

                            sumaImpuesto1 += Impuesto1;
                            txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto1, 2).ToString();

                            sumaNetos += Neto;
                            txtImporteTotal.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                        }
                        else
                        {
                            Impuesto1 = Math.Round(((SubTotal2 * 1) - SubTotal2), 2);
                            Neto = Math.Round((Importe + Impuesto1), 3);
                            item.SubItems.Add("$ " + Math.Round(Impuesto1, 2).ToString());                              ///ITEM 6A
                            item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());                                   ///ITEM 7B

                            sumaImpuesto1 += Impuesto1;
                            txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto1, 2).ToString();

                            sumaNetos += Neto;
                            txtImporteTotal.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                        }
                    }

                    else if (IdImpuesto == 2 && cboTipoFactura.Text.Trim() != "X" || cboTipoFactura.Text.Trim() == "P")
                    {
                        if (cboFormaPago.Text.Trim() == "PAGO MOSTRADOR" || txtCodFormaPago.Text.Trim() == "1")
                        {
                            Impuesto1 = Math.Ceiling(((SubTotal2 * 1.105) - SubTotal2));
                            Neto = Math.Ceiling((Importe + Impuesto1));
                            item.SubItems.Add("$ " + Math.Ceiling(Impuesto1).ToString());                              ///ITEM 6A
                            item.SubItems.Add("$ " + Math.Ceiling(Neto).ToString());                                   ///ITEM 7B

                            sumaImpuesto1 += Impuesto1;
                            txtImpuesto1.Text = "$ " + Math.Ceiling(sumaImpuesto1).ToString();

                            sumaNetos += Neto;
                            txtImporteTotal.Text = "$ " + Math.Ceiling(sumaNetos).ToString();
                        }
                        else
                        {
                            Impuesto1 = Math.Round(((SubTotal2 * 1.105) - SubTotal2), 2);
                            Neto = Math.Round((Importe + Impuesto1), 3);
                            item.SubItems.Add("$ " + Math.Round(Impuesto1, 2).ToString());                              ///ITEM 6A
                            item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());                                   ///ITEM 7B

                            sumaImpuesto1 += Impuesto1;
                            txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto1, 2).ToString();

                            sumaNetos += Neto;
                            txtImporteTotal.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                        }
                    }

                    else if (IdImpuesto == 1 && cboTipoFactura.Text.Trim() != "X" || cboTipoFactura.Text.Trim() == "P")
                    {
                        if (cboFormaPago.Text.Trim() == "PAGO MOSTRADOR" || txtCodFormaPago.Text.Trim() == "1")
                        {
                            Impuesto2 = Math.Ceiling(((SubTotal2 * 1.21) - SubTotal2));
                            Neto = Math.Ceiling((Importe + Impuesto2));
                            item.SubItems.Add("$ " + Math.Ceiling(Impuesto2).ToString());                              ///ITEM 6B
                            item.SubItems.Add("$ " + Math.Ceiling(Neto).ToString());                                   ///ITEM 7B    

                            sumaImpuesto2 += Impuesto2;
                            txtImpuesto2.Text = "$ " + Math.Ceiling(sumaImpuesto2).ToString();

                            sumaNetos += Neto;
                            txtImporteTotal.Text = "$ " + Math.Ceiling(sumaNetos).ToString();

                            /*
                            Impuesto2 = Math.Ceiling(-((SubTotal2 / 1.21) - SubTotal2));
                            Neto = Math.Ceiling((Importe - Impuesto2));
                            item.SubItems.Add("$ " + Math.Ceiling(Impuesto2).ToString());                              ///ITEM 6B
                            item.SubItems.Add("$ " + Math.Ceiling(Neto).ToString());                                   ///ITEM 7B    

                            sumaImpuesto2 += Impuesto2;
                            txtImpuesto2.Text = "$ " + Math.Ceiling(sumaImpuesto2).ToString();

                            sumaNetos += Neto;
                            txtImporteTotal.Text = "$ " + Math.Ceiling(sumaNetos).ToString();
                            */
                        }
                        else
                        {
                            Impuesto2 = Math.Round(((SubTotal2 * 1.21) - SubTotal2), 2);
                            Neto = Math.Round((Importe + Impuesto2), 3);
                            item.SubItems.Add("$ " + Math.Round(Impuesto2, 2).ToString());                              ///ITEM 6B
                            item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());                                   ///ITEM 7B    

                            sumaImpuesto2 += Impuesto2;
                            txtImpuesto2.Text = "$ " + Math.Round(sumaImpuesto2, 2).ToString();

                            sumaNetos += Neto;
                            txtImporteTotal.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                        }
                    }

                    sumaTotales += SubTotal;
                    txtSubTotal.Text = "$ " + Math.Round(sumaTotales, 3).ToString();

                    item.SubItems.Add(CantActualArticulo.ToString());                                              ///ITEM 8
                    item.SubItems.Add("0000");     //Observaciones                                                 ///ITEM 9

                    //item.SubItems.Add("-");
                    //item.SubItems.Add("0");  //colocar el IDNotaPedido                                           ///ITEM 10

                    if (IdImpuesto != 2)
                        Impuesto1 = 0;
                    if (IdImpuesto != 1)
                        Impuesto2 = 0;

                    item.SubItems.Add(Convert.ToDecimal(Math.Round(Impuesto1, 2)).ToString());                  ///ITEM 11                                      
                    item.SubItems.Add(Convert.ToDecimal(Math.Round(Impuesto2, 2)).ToString());                  ///ITEM 12

                    item.ImageIndex = 1;
                    lvwDetalleFacturaVenta.Items.Add(item);

                    //Normalizacion de Saldos totales
                    if (lvwDetalleFacturaVenta.Items.Count != 0)
                    {
                        dSubTotal = 0.000;
                        dImpuesto1 = 0.000;
                        dImpuesto2 = 0.000;
                        dImporteTotal = 0.00;

                        char[] QuitaSimbolo = { '$', ' ' };
                        for (int i = 0; i < (lvwDetalleFacturaVenta.Items.Count); i++)
                        {

                            //En cualquier otro caso no aplica redondeo + en articulos cargados
                            dImporteTotal += Math.Round(Convert.ToSingle(lvwDetalleFacturaVenta.Items[i].SubItems[9].Text.TrimStart(QuitaSimbolo)), 2);
                            dImpuesto1 += Math.Round(Convert.ToSingle(lvwDetalleFacturaVenta.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo)), 2);
                            dImpuesto2 += Math.Round(Convert.ToSingle(lvwDetalleFacturaVenta.Items[i].SubItems[13].Text.TrimStart(QuitaSimbolo)), 2);
                            dSubTotal += Math.Round(Convert.ToSingle(lvwDetalleFacturaVenta.Items[i].SubItems[7].Text.TrimStart(QuitaSimbolo)), 3);

                        }
                        this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImporteTotal, 2));
                        this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImpuesto1, 2));
                        this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImpuesto2, 2));
                        this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dSubTotal, 2));
                    }
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    ////////////////////////////////////////////////// CARGA ITEMS DE FACTURA ///////////////////////////////////////////////////////
                    double subTotalfactu;
                    double iva105Factu;
                    double iva21Factu;
                    double importeFactu;

                    if (status == false)
                    {
                        if (fechaFacturaVenta.AddDays(180) <= DateTime.Today)
                            MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            txtNroInternoFact.Text = idNROFACTUINTERNO.ToString();
                            idNROFACTUINTERNO = nroFacturaVtaInter;

                            connGeneric.EliminarGeneric("DetalleFacturaVentas", " NROFACTURAINTERNO = " + nroFacturaVtaInter);

                            char[] QuitaSimbolo1 = { '$', ' ' };
                            char[] QuitaSimbolo2 = { '%', ' ' };

                            for (int i = 0; i < (lvwDetalleFacturaVenta.Items.Count); i++)
                            {
                                //Si es mostrador o no
                                if (cboFormaPago.Text.Trim() == "PAGO MOSTRADOR" || txtCodFormaPago.Text.Trim() == "1")
                                {
                                    string agregarItem = "INSERT INTO DetalleFacturaVentas(IDARTICULO, CANTIDAD, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, NROFACTURAINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleFacturaVenta.Items[i].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3))), (ROUND(Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[7].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3)),1)), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[6].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,2))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,3))), (ROUND(Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3)),1)), (ROUND(Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[13].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3)),1)), (ROUND(Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[9].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,2)),1)), " + nroFacturaVtaInter + ")";
                                    conn.InsertarGeneric(agregarItem);
                                }
                                else
                                {
                                    string agregarItem = "INSERT INTO DetalleFacturaVentas(IDARTICULO, CANTIDAD, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, NROFACTURAINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleFacturaVenta.Items[i].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[7].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[6].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,2))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[13].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[9].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,2))), " + nroFacturaVtaInter + ")";
                                    conn.InsertarGeneric(agregarItem);
                                }

                                //////////////////////////////////////////////////////////ACTUALIZA STOCK///////////////////////////////////////////////////////////
                                if (chkBoxPresupuesto.CheckState == CheckState.Unchecked)
                                {
                                    int iTotalStock;
                                    iTotalStock = CantActualArticulo - Convert.ToInt32(txtCantArticulo.Text);//Convert.ToInt32(lvwDetalleFacturaVenta.Items[i].SubItems[3].Text);

                                    string actualizaStock = "CANT_ACTUAL=(Cast(replace(" + iTotalStock + ", ',', '.') as decimal(10,0)))";
                                    if (connGeneric.ActualizaGeneric("Articulo", actualizaStock, " IDARTICULO= " + IdArticulo + ""))
                                    {
                                        connGeneric.DesconectarBD();
                                        connGeneric.DesconectarBDLeeGeneric();
                                    }
                                }
                                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            }
                            //MostrarDatos();
                            MostrarItemsDatos2(nroFacturaVtaInter);
                        }
                    }

                    else if (status == true)
                    {
                        int nroremInt;
                        nroremInt = UltimaFactura();

                        char[] QuitaSimbolo1 = { '$', ' ' };
                        char[] QuitaSimbolo2 = { '%', ' ' };
                        for (int i = 0; i < (lvwDetalleFacturaVenta.Items.Count); i++)
                        {
                            //string agregarItem = "INSERT INTO DetalleFacturaVentas(IDARTICULO, CANTIDAD, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, NROFACTURAINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleFacturaVenta.Items[i].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[7].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[6].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,2))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[13].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[9].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,2))), " + Convert.ToInt32(lvwFacturaVenta.Items[lvwFacturaVenta.Items.Count - 1].SubItems[0].Text) + ")";
                            string agregarItem = "INSERT INTO DetalleFacturaVentas(IDARTICULO, CANTIDAD, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, NROFACTURAINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleFacturaVenta.Items[i].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[7].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[6].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,2))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[13].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[i].SubItems[9].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10,2))), " + nroremInt + ")";

                            //nroFacturaVtaInter = Convert.ToInt32(lvwFacturaVenta.Items[lvwFacturaVenta.Items.Count - 1].SubItems[0].Text);
                            nroFacturaVtaInter = UltimaFactura();
                            conn.InsertarGeneric(agregarItem);

                            //////////////////////////////////////////////////////////ACTUALIZA STOCK///////////////////////////////////////////////////////////
                            if (chkBoxPresupuesto.CheckState == CheckState.Unchecked)
                            {
                                int iTotalStock;
                                iTotalStock = CantActualArticulo - Convert.ToInt32(txtCantArticulo.Text);

                                string actualizaStock = "CANT_ACTUAL=(Cast(replace(" + iTotalStock + ", ',', '.') as decimal(10,0)))";
                                if (connGeneric.ActualizaGeneric("Articulo", actualizaStock, " IDARTICULO= " + IdArticulo + ""))
                                {
                                    connGeneric.DesconectarBD();
                                    connGeneric.DesconectarBDLeeGeneric();
                                }
                            }
                            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                         
                        }
                        //MessageBox.Show("Item Actualizado/Agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);                           
                    }

                    //////////////////////////////////////////////// ACTUALIZA EL AGREGADO DE DATOS ////////////////////////////////////////////////
                    connGeneric.DesconectarBDLeeGeneric();
                    connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleFacturaVentas WHERE NROFACTURAINTERNO = " + nroFacturaVtaInter + "", "DetalleFacturaVentas");

                    subTotalfactu = Convert.ToSingle(connGeneric.leerGeneric["SubTotal"].ToString());
                    iva105Factu = Convert.ToSingle(connGeneric.leerGeneric["Iva105"].ToString());
                    iva21Factu = Convert.ToSingle(connGeneric.leerGeneric["Iva21"].ToString());
                    importeFactu = Convert.ToSingle(connGeneric.leerGeneric["Importe"].ToString());

                    string actualizar = "BASICO=(Cast(replace('" + subTotalfactu + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + iva105Factu + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + iva21Factu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeFactu + "', ',', '.') as decimal(10,2))), PENDIENTE=(Cast(replace('" + (-importeFactu) + "', ',', '.') as decimal(10,2)))";

                    this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeFactu, 2));
                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)iva105Factu, 2));
                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)iva21Factu, 2));
                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)subTotalfactu, 2));

                    if (connGeneric.ActualizaGeneric("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + nroFacturaVtaInter + ""))
                    {
                        MostrarDatos2(nroFacturaVtaInter);
                        MostrarItemsDatos2(nroFacturaVtaInter);
                        // MessageBox.Show("La información de la factura ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se ha podido actualizar los datos de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //redondeo de totales
                    if (cboFormaPago.Text.Trim() == "PAGO MOSTRADOR" || txtCodFormaPago.Text.Trim() == "1")
                    {
                        char[] QuitaSimbolo = { '$', ' ' };
                        decimal dTotalFactu, dSubtotalFactu;

                        dTotalFactu = Math.Round(Convert.ToDecimal(txtImporteTotal.Text.TrimStart(QuitaSimbolo)), 1);
                        dSubtotalFactu = Math.Round(Convert.ToDecimal(txtSubTotal.Text.TrimStart(QuitaSimbolo)), 1);
                        txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dTotalFactu, 2));
                        txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dSubtotalFactu, 2));

                        if (chkBoxPresupuesto.CheckState == CheckState.Unchecked)
                        {
                            //Actualiza factu haciendo un redondeo hacia arriba
                            string actualizarFactuRedondeo = "BASICO=(Cast(replace('" + dSubtotalFactu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + dTotalFactu + "', ',', '.') as decimal(10,2)))";
                            connGeneric.ActualizaGeneric("FacturasVentas", actualizarFactuRedondeo, " IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL= '" + sPtoVta.Trim() + "' AND NROFACTURA = '" + txtNroFacturaVenta.Text.Trim() + "'");
                        }
                    }

                }
                else
                    MessageBox.Show("Error al Agregar Artículo: No se ha ingresado artículo o cantidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
            catch //(System.Exception excep)
            {
                MessageBox.Show("Error: Falta algun tipo de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Auditoria                
                AuditoriaSistema AS1 = new AuditoriaSistema();
                AS1.SistemaProcesoAuditor_0001("Proc. GuardaItemsDatos() ", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////
            }
        }


        private void GuardaDatosTesoreria(int nroFacturaInt, int nroReciboInt, string nroFactu)
        {
            try
            {
                if (txtCodFormaPago.Text == "1")  //pago mostrador
                {
                    int iUltimoIdCaja;

                    connGeneric.LeeGeneric("SELECT MAX(NroCaja) as NroUltimaCajaAbierta FROM TesoreriaCaja WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = " + sPtoVta + " AND IdEstadoCaja=20", "TesoreriaCaja");
                    iUltimoIdCaja = Convert.ToInt32(connGeneric.leerGeneric["NroUltimaCajaAbierta"].ToString());

                    char[] QuitaSimbolo = { '$', ' ' };
                    TesoreriaMovimientoCaja caja1 = new TesoreriaMovimientoCaja();
                    caja1.IngresoCaja(sPtoVta, 2, "EFECTIVO", Convert.ToDecimal(txtImporteTotal.Text.TrimStart((QuitaSimbolo))), 0, frmPrincipal.Usuario, iUltimoIdCaja, nroFacturaInt, nroReciboInt, nroFactu, nroFactu);
                }


                //EN CONSTRUCCION//

            }
            catch { }
        }


        private void MostrarDatos2(int nroFactura)
        {
            try
            {
                this.lvwFacturaVenta.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT NROFACTURAINTERNO as 'Código', NROFACTURA as 'Nro Fact', Sucursal, Fecha, Basico as 'Básico', DESCUENTOS as 'Desc', IMPUESTO1 as 'Iva 10,5', IMPUESTO2 as 'Iva 21', TOTAL as 'Total', FacturasVentas.OBSERVACIONES as 'Observaciones', FacturasVentas.nroncinterno, FacturasVentas.nroNC, FacturasVentas.Impresa, Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, TipoFactura.Descripcion as 'Tipo' , EstadoSistema.* FROM FacturasVentas, Cliente, TipoFactura, EstadoSistema WHERE TipoFactura.IdTipoFactura=FacturasVentas.IdTipoFactura AND FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND FacturasVentas.SUCURSAL = '" + sPtoVta + "' AND EstadoSistema.IdEstado=FacturasVentas.IDESTADO AND FacturasVentas.IDCLIENTE = Cliente.IDCLIENTE AND NROFACTURAINTERNO= " + nroFactura + "  ORDER BY Código", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwFacturaVenta.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Código"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["Nro Fact"].ToString());
                    item.SubItems.Add(dr["Fecha"].ToString());
                    item.SubItems.Add(dr["Tipo"].ToString());

                    item.SubItems.Add(dr["IDCLIENTE"].ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString());

                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Básico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Desc"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Iva 21"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());

                    item.SubItems.Add(dr["Descripcion"].ToString());
                    item.SubItems.Add(dr["IdEstado"].ToString());

                    item.SubItems.Add(dr["NRONCINTERNO"].ToString());
                    item.SubItems.Add(dr["NRONC"].ToString());

                    if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(180) <= DateTime.Today)
                        item.ImageIndex = 2;
                    else if (dr["IDESTADO"].ToString() == "19")
                        item.ImageIndex = 5;
                    else
                        item.ImageIndex = 0;

                    item.UseItemStyleForSubItems = false;
                    lvwFacturaVenta.Items.Add(item);
                }
                cm.Connection.Close();

                /*if (txtObservacionFactura.Text.Trim() == "Pago Contado")                
                    btnCerrarFactura.Visible = false;*/

            }
            catch { }
        }

        private int UltimoNroFacturaSegunTipoComprobante(int idtipofactura)
        {
            int nrofacturaint = 0;
            int iTipoFactu;

            if (idtipofactura == 2)
                iTipoFactu = 3;
            else
                iTipoFactu = 2;

            connGeneric.LeeGeneric("SELECT MAX(NROFACTURA) as NROFACTU FROM FacturasVentas WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = " + sPtoVta + " AND (IDTIPOFACTURA = " + idtipofactura + " OR IDTIPOFACTURA = " + iTipoFactu + ") ORDER BY NROFACTU", "FacturasVentas");

            if (connGeneric.leerGeneric["NROFACTU"].ToString() == "")
                return 0;
            else
            {
                nrofacturaint = Convert.ToInt32(connGeneric.leerGeneric["NROFACTU"].ToString());
                nrofacturaint = nrofacturaint + 1;
                return nrofacturaint;
            }
        }

        private int UltimaFactura()
        {
            int nrofacturaint = 0;

            connGeneric.LeeGeneric("SELECT MAX(NROFACTURAINTERNO) as NROINTERNO FROM FacturasVentas WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = " + sPtoVta + " ORDER BY NROINTERNO", "FacturasVentas");

            if (connGeneric.leerGeneric["NROINTERNO"].ToString() == "")
                return 0;
            else
            {
                nrofacturaint = Convert.ToInt32(connGeneric.leerGeneric["NROINTERNO"].ToString());
                //nroremitoint = nroremitoint + 1;
                return nrofacturaint;
            }
        }

        private void MostrarItemsDatos2(int NROFacturaInterno)
        {
            try
            {
                lvwDetalleFacturaVenta.Items.Clear();

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                double iva105;
                double iva21;
                //int iCantPedida, iCantActual, iCantRestante;
                //int CantPendiente;

                connGeneric.LeeGeneric("SELECT FacturasVentas.NROFACTURAINTERNO, DetalleFacturaVentas.IDDETALLEFACTURAVENTAS, DetalleFacturaVentas.IDDETALLEREMITO, Articulo.DESCRIPCION as 'Artículo', Articulo.CANT_ACTUAL, DetalleFacturaVentas.CANTIDAD, DetalleFacturaVentas.PRECUNITARIO as 'Precio Unitario', DetalleFacturaVentas.IMPORTE as 'Importe', DetalleFacturaVentas.DESCUENTO as 'Descuento', DetalleFacturaVentas.PORCDESC as '% Desc', DetalleFacturaVentas.SUBTOTAL as 'Subtotal', DetalleFacturaVentas.IMPUESTO1 as 'Iva 10,5', DetalleFacturaVentas.IMPUESTO2 as 'Iva 21', DetalleFacturaVentas.OBSERVACIONES as 'Observaciones' FROM FacturasVentas, DetalleFacturaVentas, Articulo, Cliente, Personal WHERE FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND DetalleFacturaVentas.IDARTICULO = Articulo.IDARTICULO AND FacturasVentas.IDCLIENTE = Cliente.IDCLIENTE AND FacturasVentas.IDPERSONAL = Personal.IDPERSONAL AND DetalleFacturaVentas.NROFACTURAINTERNO = FacturasVentas.NROFACTURAINTERNO AND FacturasVentas.NROFACTURAINTERNO = " + NROFacturaInterno + "", "FacturasVentas");

                iva105 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 10,5"].ToString());
                iva21 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 21"].ToString());

                SqlCommand cm = new SqlCommand("SELECT Articulo.Codigo, FacturasVentas.NROFACTURAINTERNO, DetalleFacturaVentas.IDDETALLEFACTURAVENTAS, DetalleFacturaVentas.NROFACTURAINTERNO, DetalleFacturaVentas.IDDETALLEREMITO, DetalleFacturaVentas.IDArticulo, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL, DetalleFacturaVentas.CANTIDAD, DetalleFacturaVentas.PRECUNITARIO, DetalleFacturaVentas.IMPORTE, DetalleFacturaVentas.DESCUENTO, DetalleFacturaVentas.PORCDESC, DetalleFacturaVentas.SUBTOTAL, DetalleFacturaVentas.IMPUESTO1 as 'Iva 10,5', DetalleFacturaVentas.IMPUESTO2 as 'Iva 21', DetalleFacturaVentas.OBSERVACIONES FROM FacturasVentas, DetalleFacturaVentas, Articulo, Cliente, Personal WHERE FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND DetalleFacturaVentas.IDARTICULO = Articulo.IDARTICULO AND FacturasVentas.IDCLIENTE = Cliente.IDCLIENTE AND FacturasVentas.IDPERSONAL = Personal.IDPERSONAL AND DetalleFacturaVentas.NROFACTURAINTERNO = FacturasVentas.NROFACTURAINTERNO AND FacturasVentas.NROFACTURAINTERNO = " + NROFacturaInterno + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwDetalleFacturaVenta.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDArticulo"].ToString());
                    item.SubItems.Add(dr["Codigo"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());

                    item.SubItems.Add(dr["CANTIDAD"].ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["PRECUNITARIO"]), 3).ToString());
                    item.SubItems.Add("% " + Math.Round(Convert.ToDecimal(dr["PORCDESC"]), 2).ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["DESCUENTO"]), 2).ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Subtotal"]), 2).ToString());

                    if (dr["Iva 10,5"].ToString() != "0,0000")
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 2).ToString());
                    else
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 21"]), 2).ToString());

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPORTE"]), 2).ToString());
                    //   item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.Empty, Color.LightGray, null);
                    //   item.SubItems.Add(dr["CANTIDADRESTANTE"].ToString(), Color.Empty, Color.LightGray, null);

                    item.SubItems.Add(dr["OBSERVACIONES"].ToString());
                    item.SubItems.Add(dr["IDDETALLEFACTURAVENTAS"].ToString());
                    item.SubItems.Add(dr["Iva 10,5"].ToString());
                    item.SubItems.Add(dr["Iva 21"].ToString());

                    item.ImageIndex = 1;

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleFacturaVenta.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch //(System.Exception excep)
            {
                //Auditoria
                AuditoriaSistema AS2 = new AuditoriaSistema();
                AS2.SistemaProcesoAuditor_0002("Proc. MostrarItemsDatos2()", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////            
            }
        }

        private void frmFacturacion_Load(object sender, EventArgs e)
        {
            conPermi();

            // TODO: esta línea de código carga datos en la tabla 'dGestionDTGeneral.FacturacionCliente' Puede moverla o quitarla según sea necesario.
            //this.facturacionClienteTableAdapter.Fill(this.dGestionDTGeneral.FacturacionCliente, NumeroFactura);
            // TODO: esta línea de código carga datos en la tabla 'dGestionDTGeneral.FacturaVentaCliente' Puede moverla o quitarla según sea necesario.
            //this.facturaVentaClienteTableAdapter.Fill(this.dGestionDTGeneral.FacturaVentaCliente);            

            gpoFacturacion.Visible = false;
            gpFactura.Width = 1030;
            lvwFacturaVenta.Width = 1003;

            lvwDetalleFacturaVenta.Height = 250;

            lblCodArt.Visible = false;
            txtCodArticulo.Visible = false;
            cboArticulo.Visible = false;
            btnAgregaArt.Visible = false;
            btnQuitaArt.Visible = false;
            lblCantidad.Visible = false;
            txtCantArticulo.Visible = false;
            btnArt.Visible = false;
            //btnRemito.Visible = false;
            //btnGuardar.Visible = false;

            lblPrecio.Visible = false;
            txtPrecio.Visible = false;
            lblDescuento.Visible = false;
            txtProcDesc.Visible = false;

            dtpFechaFactu.Value = DateTime.Today;
            //fechaFacturaCompra = DateTime.Today;

            conn.ConectarBD();

            FormatoListView();
            IDEMPRESA = ConsultaEmpresa(); //Lee Empresa
            MostrarDatos(1, 30);

            cboBuscaFactura.SelectedIndex = 0;
            //cboNroSucursal.SelectedIndex = 0;
            cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();
            //rptReporteFacturacion.RefreshReport();
            //rptFacturacionImpresion_A.RefreshReport();         

        }

        private void FormatoListView()
        {
            lvwFacturaVenta.View = View.Details;
            lvwFacturaVenta.LabelEdit = true;
            lvwFacturaVenta.AllowColumnReorder = true;
            lvwFacturaVenta.FullRowSelect = true;
            lvwFacturaVenta.GridLines = true;

            lvwDetalleFacturaVenta.View = View.Details;
            lvwDetalleFacturaVenta.LabelEdit = true;
            lvwDetalleFacturaVenta.AllowColumnReorder = true;
            lvwDetalleFacturaVenta.FullRowSelect = true;
            lvwDetalleFacturaVenta.GridLines = true;

            lvwRemitoPendiente.View = View.Details;
            lvwRemitoPendiente.LabelEdit = true;
            lvwRemitoPendiente.AllowColumnReorder = true;
            lvwRemitoPendiente.FullRowSelect = true;
            lvwRemitoPendiente.GridLines = true;
        }

        private void MostrarDatos(int iPaginaAv, int iPaginaRe)
        {
            try
            {
                lvwFacturaVenta.Items.Clear();

                tsTextBox.Text = "FA " + iPaginaAv + "-" + iPaginaRe;

                SqlCommand cm = new SqlCommand("SELECT NROFACTURAINTERNO as 'Código', NROFACTURA as 'Nro Fact', Sucursal, Fecha, Basico as 'Básico', DESCUENTOS as 'Desc', IMPUESTO1 as 'Iva 10,5', IMPUESTO2 as 'Iva 21', TOTAL as 'Total', FacturasVentas.OBSERVACIONES as 'Observaciones', FacturasVentas.nroncinterno, FacturasVentas.nroNC, FacturasVentas.Impresa, Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, TipoFactura.Descripcion as 'Tipo', EstadoSistema.* FROM FacturasVentas, Cliente, TipoFactura, EstadoSistema WHERE TipoFactura.IdTipoFactura=FacturasVentas.IdTipoFactura AND FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND FacturasVentas.SUCURSAL = '" + sPtoVta + "' AND EstadoSistema.IdEstado=FacturasVentas.IDESTADO AND FacturasVentas.IDCLIENTE = Cliente.IDCLIENTE AND NROFACTURA BETWEEN " + iPaginaAv + " AND " + iPaginaRe + " ORDER BY Código", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwFacturaVenta.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Código"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["Nro Fact"].ToString());
                    item.SubItems.Add(dr["Fecha"].ToString());
                    item.SubItems.Add(dr["Tipo"].ToString());

                    item.SubItems.Add(dr["IDCLIENTE"].ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString());

                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Básico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Desc"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Iva 21"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());
                    item.SubItems.Add(dr["Descripcion"].ToString());
                    item.SubItems.Add(dr["IdEstado"].ToString());

                    item.SubItems.Add(dr["NRONCINTERNO"].ToString());
                    item.SubItems.Add(dr["NRONC"].ToString());

                    if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(180) <= DateTime.Today)
                        item.ImageIndex = 2;
                    else if (dr["IDESTADO"].ToString() == "19")
                        item.ImageIndex = 5;
                    else
                        item.ImageIndex = 0;



                    item.UseItemStyleForSubItems = false;
                    lvwFacturaVenta.Items.Add(item);
                }
                cm.Connection.Close();

                /*if (txtObservacionFactura.Text.Trim() == "Pago Contado")
                    btnCerrarFactura.Visible = false;*/

            }
            catch //(System.Exception excep)
            {
                //Auditoria
                AuditoriaSistema AS3 = new AuditoriaSistema();
                AS3.SistemaProcesoAuditor_0003("Proc. MostrarDatos() ", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////            
            }
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            NuevaFactura();
        }

        private void NuevaFactura()
        {
            try
            {
                nuevaFactu = true;
                //timer1.Enabled = true;

                chkImpresa.CheckState = CheckState.Unchecked;

                chkBoxPresupuesto.CheckState = CheckState.Unchecked;

                dSubTotal = 0.00;
                dImpuesto1 = 0.00;
                dImpuesto2 = 0.00;
                dImporteTotal = 0.00;

                dtpFechaFactu.Value = DateTime.Today;
                fechaFacturaVenta = DateTime.Today;

                lvwDetalleFacturaVenta.Items.Clear();
                lvwFacturaVenta.Items.Clear();

                gpoFacturacion.Visible = true;
                gpDetalleFactura.Visible = true;
                gpFactura.Height = 310;
                gpFactura.Width = 270;
                lvwFacturaVenta.Height = 280;
                lvwFacturaVenta.Width = 245;
                lvwDetalleFacturaVenta.Height = 220;

                cboBuscaFactura.SelectedIndex = 0;

                lblCodArt.Visible = true;
                txtCodArticulo.Visible = true;
                cboArticulo.Visible = true;
                btnAgregaArt.Visible = true;
                btnQuitaArt.Visible = true;
                lblCantidad.Visible = true;
                txtCantArticulo.Visible = true;
                btnArt.Visible = true;

                tsBtnNuevo.Enabled = true;

                //btnGuardar.Enabled = true;
                //btnGuardar.Visible = true;

                lblPrecio.Visible = true;
                txtPrecio.Visible = true;
                lblDescuento.Visible = true;
                txtProcDesc.Visible = true;

                this.txtCantArticulo.Text = "";
                this.txtProcDesc.Text = "0";
                this.txtNroInternoFact.Text = "0";
                this.txtNroFacturaVenta.Text = "0";
                this.txtObservacionFactura.Text = "";
                //this.cboNroSucursal.SelectedIndex = 0;
                cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();
                this.txtIva.Text = "";
                this.txtCodArticulo.Text = "";
                this.txtCodPersonal.Text = "";
                this.txtCodCliente.Text = "";
                this.txtCodVendedor.Text = "";
                this.txtCodFormaPago.Text = "";
                this.txtCodTipoFactura.Text = "";
                this.txtCuit.Text = "";
                this.txtDescuento.Text = "$ 0.000";
                this.txtSubTotal.Text = "$ 0.000";
                this.txtImpuesto1.Text = "$ 0.000";
                this.txtImpuesto2.Text = "$ 0.000";
                this.txtImporteTotal.Text = "$ 0.00";

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                /////////////////////////////////////////// AUTONUMERICO NRO INTERNO //////////////////////////////////////////
                connGeneric.LeeGeneric("SELECT MAX(NROFACTURA) as 'NRO' FROM FacturasVentas WHERE SUCURSAL = '" + sPtoVta.Trim() + "' AND IDEMPRESA = " + IDEMPRESA + " ORDER BY NRO", "FacturasVentas");
                /*
                  if (connGeneric.leerGeneric["NRO"].ToString() == "")
                      txtNroInternoFact.Text = "0";
                  else
                      txtNroInternoFact.Text = connGeneric.leerGeneric["NRO"].ToString();

                  contadorNROfactNuevo = (Convert.ToInt32(txtNroInternoFact.Text));
                  contadorNROfactNuevo = contadorNROfactNuevo + 1;
                  txtNroInternoFact.Text = contadorNROfactNuevo.ToString();

                  //txtNroIntRemito.Text = this.txtNroIntRemito.Text;
                  //this.txtNroFacturaVenta.Text = this.cboNroSucursal.Text.Trim() + "-" + this.txtNroInternoFact.Text;
                  this.txtNroFacturaVenta.Text = this.txtNroInternoFact.Text;

                  ValidaNumerador(txtNroFacturaVenta.Text.Trim());
                  */

                if (connGeneric.leerGeneric["NRO"].ToString() == "")
                {
                    txtNroInternoFact.Text = "0";
                    txtNroFacturaVenta.Text = "0";
                }
                else
                {
                    txtNroInternoFact.Text = connGeneric.leerGeneric["NRO"].ToString();
                    txtNroFacturaVenta.Text = connGeneric.leerGeneric["NRO"].ToString();
                }

                contadorNROfactNuevo = (Convert.ToInt32(txtNroFacturaVenta.Text));
                contadorNROfactNuevo = contadorNROfactNuevo + 1;

                txtNroFacturaVenta.Text = Convert.ToString(contadorNROfactNuevo);

                ValidaNumerador(this.txtNroFacturaVenta.Text.Trim());

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////               


                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                conPermi();


                ////Condicion de precarga de datos
                if (sPtoVta == "0001" && IDEMPRESA == 1)
                    txtCodCliente.Text = "6237";
                //////////////////////////////////////

                /*if (cboVendedor.Text.Trim() == "MOSTRADOR")
                {
                    if (txtCodTipoFactura.Text.Trim() == "5" && cboTipoFactura.Text.Trim() == "P")
                        btnCerrarFactura.Visible = false;
                    else
                        btnCerrarFactura.Visible = true;
                }*/


            }
            catch //(System.Exception excep)
            {
                MessageBox.Show("Error: El nro ingresado no es numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Auditoria
                AuditoriaSistema AS4 = new AuditoriaSistema();
                AS4.SistemaProcesoAuditor_0004("Proc. NuevaFactura()", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////            
            }
        }

        private bool ValidaNumerador(string nrocomprobante)
        {
            try
            {
                SqlCommand cm = new SqlCommand("SELECT NROFACTURA FROM FacturasVentas WHERE IDEMPRESA = " + IDEMPRESA + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    if (nrocomprobante == dr["NROFACTURA"].ToString().Trim())
                        return true;
                }

                cm.Connection.Close();
                return false;

            }
            catch
            {
                connGeneric.DesconectarBD();
                return false;
            }
        }

        private void tsBtnModificar_Click(object sender, EventArgs e)
        {
            nuevaFactu = false;
            //timer1.Enabled = false;

            if (lvwFacturaVenta.SelectedItems.Count == 0)
                MessageBox.Show("Error: No se ha seleccionado ninguna factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                gpoFacturacion.Visible = true;
                gpDetalleFactura.Visible = true;
                gpFactura.Height = 310;
                gpFactura.Width = 270;
                lvwFacturaVenta.Height = 280;
                lvwFacturaVenta.Width = 245;
                lvwDetalleFacturaVenta.Height = 220;


                cboBuscaFactura.SelectedIndex = 0;
                //cboNroSucursal.SelectedIndex = 0;
                cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();

                lblCodArt.Visible = true;
                txtCodArticulo.Visible = true;
                cboArticulo.Visible = true;
                btnAgregaArt.Visible = true;
                btnQuitaArt.Visible = true;

                lblCantidad.Visible = true;
                txtCantArticulo.Visible = true;
                btnArt.Visible = true;
                lblDescuento.Visible = true;
                lblPrecio.Visible = true;
                txtPrecio.Visible = true;
                txtProcDesc.Visible = true;

                tsBtnNuevo.Enabled = true;
                conPermi();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            gpoFacturacion.Visible = false;
            gpFactura.Width = 1030;
            lvwFacturaVenta.Width = 1003;
            lvwDetalleFacturaVenta.Height = 250;

            lblCodArt.Visible = false;
            txtCodArticulo.Visible = false;
            cboArticulo.Visible = false;
            btnAgregaArt.Visible = false;
            btnQuitaArt.Visible = false;
            lblCantidad.Visible = false;
            txtCantArticulo.Visible = false;
            btnArt.Visible = false;

            lblPrecio.Visible = false;
            txtPrecio.Visible = false;
            lblDescuento.Visible = false;
            txtProcDesc.Visible = false;

            tsBtnNuevo.Enabled = true;

            conPermi();

            //limpieza();
            MostrarDatos(PagRe, PagAv);

        }

        private void limpieza()
        {
            this.txtCantArticulo.Text = "";
            this.txtProcDesc.Text = "0";
            this.txtNroInternoFact.Text = "0";
            this.txtNroFacturaVenta.Text = "0";
            this.txtObservacionFactura.Text = "";
            //this.cboNroSucursal.SelectedIndex = 0;
            cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();
            this.txtIva.Text = "";
            this.txtCodArticulo.Text = "";
            this.txtCodPersonal.Text = "";
            this.txtCodCliente.Text = "";
            this.txtCodVendedor.Text = "";
            this.txtCodFormaPago.Text = "";
            this.txtCodTipoFactura.Text = "";
            this.txtCuit.Text = "";
            this.txtProcDesc.Text = "";
            this.txtDescuento.Text = "$ 0.000";
            this.txtSubTotal.Text = "$ 0.000";
            this.txtImpuesto1.Text = "$ 0.000";
            this.txtImpuesto2.Text = "$ 0.000";
            this.txtImporteTotal.Text = "$ 0.00";
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            frmCliente formCliente = new frmCliente();
            formCliente.pasarClienteCod += new frmCliente.pasarClienteCod1(CodClient);  //Delegado1 
            formCliente.pasarClientRS += new frmCliente.pasarClienteRS(RazonS); //Delegado2

            txtCodTipoFactura.Focus();
            formCliente.ShowDialog();

            //Detecta Numerador Facturación
            if (txtCodTipoFactura.Text == "1")
                txtNroFacturaVenta.Text = Convert.ToString(NumeradorFactura(1));

            else if (txtCodTipoFactura.Text == "2")
                txtNroFacturaVenta.Text = Convert.ToString(NumeradorFactura(2));

            else if (txtCodTipoFactura.Text == "3")
                txtNroFacturaVenta.Text = Convert.ToString(NumeradorFactura(2));

            else if (txtCodTipoFactura.Text == "4")
                txtNroFacturaVenta.Text = Convert.ToString(NumeradorFactura(4));
            else
                txtNroFacturaVenta.Text = Convert.ToString(NumeradorFactura(5));
        }

        public void CodClient(int CodCliente)
        {
            this.txtCodCliente.Text = CodCliente.ToString();
        }

        public void RazonS(string RSCliente)
        {
            this.cboCliente.Text = RSCliente.ToString();
        }

        private void txtCodCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int iTablaCant = 0;
                int iTablaCant2 = 0;

                if (this.txtCodCliente.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("SELECT * FROM Cliente WHERE IDEmpresa = " + IDEMPRESA + " AND NROCENTRO=" + sPtoVta.Trim() + " AND IdCliente = " + Convert.ToInt32(this.txtCodCliente.Text) + "", "Cliente");

                    this.cboCliente.DataSource = conn.ds.Tables[0];
                    this.cboCliente.ValueMember = "IdCliente";
                    this.cboCliente.DisplayMember = "RazonSocial";

                    iTablaCant = conn.ds.Tables[0].Rows.Count;

                    connGeneric.DesconectarBD();
                    connGeneric.DesconectarBDLeeGeneric();

                    connGeneric.LeeGeneric("SELECT Cliente.NUMDECUIT, Cliente.IDPERSONAL, cliente.IDFORMADEPAGO, TipoIva.DESCRIPCION as 'TipoIva', TipoIva.IdTipoIva, ListaPrecios.IDLISTAPRECIO, ListaPrecios.DESCRIPCION as 'DescLista' FROM Cliente, TipoIva, ListaPrecios WHERE Cliente.IDTIPOIVA = TipoIva.IDTIPOIVA AND Cliente.IDLISTAPRECIO = ListaPrecios.IDLISTAPRECIO AND Cliente.IDCLIENTE = " + Convert.ToInt32(this.txtCodCliente.Text) + "", "Cliente");

                    txtCuit.Text = connGeneric.leerGeneric["NUMDECUIT"].ToString();
                    txtIva.Text = connGeneric.leerGeneric["TipoIva"].ToString();
                    txtCodFormaPago.Text = connGeneric.leerGeneric["IDFORMADEPAGO"].ToString();
                    txtCodPersonal.Text = connGeneric.leerGeneric["IDPERSONAL"].ToString();

                    if (chkBoxPresupuesto.CheckState == CheckState.Unchecked)
                    {
                        if (txtIva.Text == "Exento")
                            txtCodTipoFactura.Text = "3";
                        else

                        if (txtIva.Text == "Responsable inscripto")
                            txtCodTipoFactura.Text = "1";
                        else
                            txtCodTipoFactura.Text = "2";
                    }

                    iCodigoListaPrecioCliente = Convert.ToInt32(connGeneric.leerGeneric["IDLISTAPRECIO"].ToString());
                    //cboListaCliente.Text = connGeneric.leerGeneric["DescLista"].ToString();

                    connGeneric.DesconectarBD();
                    connGeneric.DesconectarBDLeeGeneric();
                    conn.DesconectarBD();
                    conn.DesconectarBDLeeGeneric();

                    //Verifica si es un vendedor el usuario logueado 
                    conn.ConsultaGeneric("SELECT * FROM Vendedor, Personal WHERE Vendedor.IDPERSONAL = personal.IDPERSONAL AND personal.IDTIPOPERSONAL = 5 AND personal.usuario = LTRIM('" + frmLogIn.usuarioLogeado + "')", "Cliente");
                    iTablaCant2 = conn.ds.Tables[0].Rows.Count;

                    connGeneric.LeeGeneric("SELECT * FROM Vendedor, Personal WHERE Vendedor.IDPERSONAL = personal.IDPERSONAL AND personal.IDTIPOPERSONAL = 5 AND personal.usuario = LTRIM('" + frmLogIn.usuarioLogeado + "')", "Cliente");
                    if (iTablaCant2 != 0)
                        txtCodVendedor.Text = connGeneric.leerGeneric["IDVENDEDOR"].ToString();

                    connGeneric.DesconectarBD();
                    connGeneric.DesconectarBDLeeGeneric();
                    conn.DesconectarBD();
                    conn.DesconectarBDLeeGeneric();
                }
                else
                {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    //txtCodCliente.Text = "";
                    txtCodTipoFactura.Text = "";
                    txtCodPersonal.Text = "";
                    cboFormaPago.Text = "";
                    txtCodFormaPago.Text = "";

                    //txtCodListaCliente.Text = "";
                    //cboListaCliente.Text = "";
                }

                if (iTablaCant < 1)
                {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    //txtCodCliente.Text = "";
                    //txtCodListaCliente.Text = "";
                    //cboListaCliente.Text = "";
                }

                if (this.txtCuit.Text.Trim() == "0")
                {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    //txtCodCliente.Text = "";
                    //txtCodListaCliente.Text = "";
                    //cboListaCliente.Text = "";
                    MessageBox.Show("Error: Falta informacion relacionada con el Cliente (CUIT)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //Datos de facturacion de pto. vta predeterminado
                if (sPtoVta == "0001" && IDEMPRESA == 1 && cboCliente.Text.Trim() == "CONSUMIDOR FINAL")
                {
                    txtCodVendedor.Text = "1029";
                    txtCodTipoFactura.Text = "5";
                    chkBoxPresupuesto.CheckState = CheckState.Checked;
                }

                if (sPtoVta == "0001" && IDEMPRESA == 2)
                {
                    txtCodTipoFactura.Text = "4";
                    chkBoxPresupuesto.CheckState = CheckState.Unchecked;
                }
                ////////////////////////////////////////////////

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
            }
            catch
            {
                cboCliente.Text = "";
                txtCuit.Text = "";
                txtIva.Text = "";
            }
        }

        private int NumeradorFactura(int iCodTipoFactu)
        {
            try
            {
                int iNroFactura = 0;
                bool bValidaNumerador;
                int iTipoFact;

                if (iCodTipoFactu == 2)
                    iTipoFact = 3;
                else
                    iTipoFact = 2;

                SqlCommand cm = new SqlCommand("SELECT MAX(NROFACTURA) AS 'UltamoNroFactura' FROM FacturasVentas, TipoFactura WHERE FacturasVentas.IDTIPOFACTURA = TipoFactura.IDTIPOFACTURA AND FacturasVentas.SUCURSAL = '" + sPtoVta + "' AND FacturasVentas.IdEmpresa = " + IDEMPRESA + " AND (FacturasVentas.IDTIPOFACTURA = " + iCodTipoFactu + " OR FacturasVentas.IDTIPOFACTURA =" + iTipoFact + ")", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                    iNroFactura = Convert.ToInt32(dr["UltamoNroFactura"].ToString());

                iNroFactura = iNroFactura + 1;

                //bValidaNumerador = ValidaNumerador(iNroFactura.ToString());

                return iNroFactura;
            }
            catch { return 0; }
        }

        private void btnFormaPago_Click(object sender, EventArgs e)
        {
            frmFormaPago frmFPago = new frmFormaPago();
            frmFPago.pasarFPCod += new frmFormaPago.pasarFormaPagoCod1(CodFormaPago);  //Delegado11 Rubro Articulo
            frmFPago.pasarFPN += new frmFormaPago.pasarFormaPagoRS(DesFormaPago); //Delegado2 Rubro Articulo

            txtCodVendedor.Focus();

            frmFPago.ShowDialog();
        }

        public void CodFormaPago(int CodFP)
        {
            this.txtCodFormaPago.Text = CodFP.ToString();
        }

        public void DesFormaPago(string NTR)
        {
            this.cboFormaPago.Text = NTR.ToString();
        }

        private void txtCodFormaPago_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodFormaPago.Text.Trim() != "")
                {
                    /*if (txtCodFormaPago.Text.Trim() == "1")
                        btnCerrarFactura.Visible = true;
                    else
                        btnCerrarFactura.Visible = false;*/

                    conn.ConsultaGeneric("SELECT * FROM FormaPago WHERE IdFormaPago = " + Convert.ToInt32(this.txtCodFormaPago.Text) + "", "FormaPago");

                    this.cboFormaPago.DataSource = conn.ds.Tables[0];
                    this.cboFormaPago.ValueMember = "IdFormaPago";
                    this.cboFormaPago.DisplayMember = "Descripcion";
                }
                else
                    this.cboFormaPago.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboFormaPago.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void txtCodPersonal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodPersonal.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("SELECT * FROM Personal WHERE Personal.IDPERSONAL = " + Convert.ToInt32(this.txtCodPersonal.Text) + "", "Personal");

                    this.cboPersonal.DataSource = conn.ds.Tables[0];
                    this.cboPersonal.ValueMember = "IDPERSONAL";
                    this.cboPersonal.DisplayMember = "NOMBREYAPELLIDO";
                }
                else
                    this.cboPersonal.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboPersonal.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void btnCodigoPersonal_Click(object sender, EventArgs e)
        {
            frmPersonal frmPerso = new frmPersonal();
            frmPerso.pasadoPerso1 += new frmPersonal.pasarPersona1(CodPPerso);  //Delegado11 Rubro Articulo
            frmPerso.pasadoPerso2 += new frmPersonal.pasarPersona2(RSPerso); //Delegado2 Rubro Articulo

            txtObservacionFactura.Focus();

            frmPerso.ShowDialog();
        }

        public void CodPPerso(int CodPersonal)
        {
            this.txtCodPersonal.Text = CodPersonal.ToString();
        }

        public void RSPerso(string DescPersonal)
        {
            this.cboPersonal.Text = DescPersonal.ToString();
        }

        private void txtCodTipoFactura_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodTipoFactura.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("SELECT * FROM TipoFactura WHERE IdTipoFactura = " + Convert.ToInt32(this.txtCodTipoFactura.Text) + "", "TipoFactura");

                    this.cboTipoFactura.DataSource = conn.ds.Tables[0];
                    this.cboTipoFactura.ValueMember = "IdTipoFactura";
                    this.cboTipoFactura.DisplayMember = "Descripcion";
                }
                else
                    this.cboTipoFactura.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboTipoFactura.Text = "";

                if (this.cboTipoFactura.Text == "P" || txtCodTipoFactura.Text == "5")
                {
                    NumeradorFactura(5);
                    txtNroFacturaVenta.Text = NumeradorFactura(5).ToString();
                }


                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void btnCodTipoFactura_Click(object sender, EventArgs e)
        {
            //frmTipoRemito frmTRemito = new frmTipoRemito();
            //frmTRemito.pasarTRCod += new frmTipoRemito.pasarTipoRemitoCod1(pasarTRCod);  //Delegado1 
            //frmTRemito.pasarTRN += new frmTipoRemito.pasarTipoRemitoRS(NTR); //Delegado2

            //txtCodFormaPago.Focus();
            //frmTRemito.ShowDialog();

            frmTipoFactura frmTipoFactura = new frmTipoFactura();
            frmTipoFactura.pasadoTipoFactu1 += new frmTipoFactura.pasarTipoFactura1(pasarTRCod);  //Delegado1 
            frmTipoFactura.pasadoTipoFactu2 += new frmTipoFactura.pasarTipoFactura2(NTR); //Delegado2

            txtCodFormaPago.Focus();
            frmTipoFactura.ShowDialog();
        }

        public void pasarTRCod(int CodTipoRemito)
        {
            this.txtCodTipoFactura.Text = CodTipoRemito.ToString();
        }

        public void NTR(string NTR)
        {
            this.cboTipoFactura.Text = NTR.ToString();
        }

        private void txtCodVendedor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodVendedor.Text.Trim() != "")
                {
                    //conn.ConsultaGeneric("SELECT * FROM VENDEDOR, Personal, TipoPersonal WHERE personal.IDTIPOPERSONAL= TipoPersonal.IDTIPOPERSONAL AND Personal.IDPERSONAL= Vendedor.IDPERSONAL AND Vendedor.IDVENDEDOR = " + Convert.ToInt32(this.txtCodVendedor.Text) + "", "Vendedor");
                    conn.ConsultaGeneric("SELECT* FROM Personal, TipoPersonal, Vendedor WHERE Personal.IDTipoPersonal = TipoPersonal.IdTipoPersonal AND Vendedor.IDTipoPersonal = TipoPersonal.IdTipoPersonal AND personal.idpersonal = Vendedor.idpersonal AND Vendedor.IDVENDEDOR = " + Convert.ToInt32(this.txtCodVendedor.Text) + " ORDER BY NOMBREYAPELLIDO", "Vendedor");

                    this.cboVendedor.DataSource = conn.ds.Tables[0];
                    this.cboVendedor.ValueMember = "IDVENDEDOR";
                    this.cboVendedor.DisplayMember = "NOMBREYAPELLIDO";
                }
                else
                    this.cboVendedor.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboVendedor.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void btnVendedor_Click(object sender, EventArgs e)
        {
            frmVendedor formVende = new frmVendedor();
            formVende.pasarVendeCod += new frmVendedor.pasarVendeCod1(CodVende);  //Delegado1 
            formVende.pasarVendeN += new frmVendedor.pasarVendeRS(NombreVende); //Delegado2

            txtCodPersonal.Focus();
            formVende.ShowDialog();
        }

        public void CodVende(int dato1)
        {
            this.txtCodVendedor.Text = dato1.ToString();
        }

        public void NombreVende(string dato2)
        {
            this.cboVendedor.Text = dato2.ToString();
        }

        private void btnAgregaArt_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCodVendedor.Text != "")
                {

                    bool EstadoFactura;
                    EstadoFactura = LeeEstadoFactura(Convert.ToInt32(this.txtNroInternoFact.Text));


                    string EstadoFacturaAnulado;
                    EstadoFacturaAnulado = EstadoFacturaAnulada(Convert.ToInt32(this.txtNroInternoFact.Text));


                    if (EstadoFacturaAnulado == "19")
                        MessageBox.Show("Error factura anulada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        if (EstadoFactura == false)
                        {
                            if (txtCodArticulo.Text.Trim() == "" || cboArticulo.Text.Trim() == "")
                                MessageBox.Show("No se ha ingresado información de artículo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            else
                            {
                                if (fechaFacturaVenta.AddDays(180) <= DateTime.Today)
                                    MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                else
                                {
                                    if (lvwDetalleRemito.Items.Count >= 25)
                                        MessageBox.Show("Límite de cantidad de items por remito excedida. Se deberá crear un nuevo remito para continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    else
                                    {
                                        //timer1.Enabled = true;
                                        GuardarTodosLosDatos();
                                        txtCodArticulo.Text = "";
                                        cboArticulo.Text = "";
                                        //txtCantPedida.Text = "";
                                        //txtProcDesc.Text = "";
                                        txtPrecio.Text = "";

                                        txtCodArticulo.Focus();
                                    }
                                }
                            }

                        }
                        else
                            MessageBox.Show("Error factura cerrada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    //NumeradorIgualadorCond_Exento_y_Final();
                }
                else
                    MessageBox.Show("Datos Imcompletos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { }
        }

        private void GuardarTodosLosDatos()
        {
            try
            {
                float subTotal;
                float impuesto1; float impuesto2;
                float descuento; float importeTotal;
                string agregar = "";

                //Quita Simbolos para guardar los datos en formato numéricos
                char[] QuitaSimbolo = { '$', ' ' };
                importeTotal = Convert.ToSingle(this.txtImporteTotal.Text.TrimStart(QuitaSimbolo));
                impuesto1 = Convert.ToSingle(this.txtImpuesto1.Text.TrimStart(QuitaSimbolo));
                impuesto2 = Convert.ToSingle(this.txtImpuesto2.Text.TrimStart(QuitaSimbolo));
                subTotal = Convert.ToSingle(this.txtSubTotal.Text.TrimStart(QuitaSimbolo));
                descuento = Convert.ToSingle(this.txtDescuento.Text.TrimStart(QuitaSimbolo));
                /////////////////////////////////////////////////////////////////////////////////

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                if (nuevaFactu == true)
                    connGeneric.ConsultaGeneric("Select * FROM FacturasVentas WHERE IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + Convert.ToInt32(txtNroInternoFact.Text) + "", "FacturasVentas");
                else
                    connGeneric.ConsultaGeneric("Select * FROM FacturasVentas WHERE IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + idNROFACTUINTERNO + "", "FacturasVentas");
                if (connGeneric.ds.Tables[0].Rows.Count == 0)
                {
                    if (chkBoxPresupuesto.CheckState == CheckState.Unchecked)
                        agregar = "INSERT INTO FacturasVentas(NROFACTURA, SUCURSAL, IDTIPOFACTURA, FECHA, IDCLIENTE, IDVENDEDOR, IDPERSONAL, IDFORMAPAGO, BASICO, PORCDESC, DESCUENTOS, IMPUESTO1, IMPUESTO2, TOTAL, PENDIENTE, OBSERVACIONES, IDEMPRESA) VALUES ('" + txtNroFacturaVenta.Text.Trim() + "', '" + cboNroSucursal.Text.Trim() + "', " + txtCodTipoFactura.Text.Trim() + ", '" + FormateoFecha() + "', " + txtCodCliente.Text.Trim() + ", " + txtCodVendedor.Text + ", " + txtCodPersonal.Text.Trim() + ", " + txtCodFormaPago.Text.Trim() + ", (Cast(replace('" + Math.Round(subTotal, 3) + "', ',', '.') as decimal(10,3))), " + 0 + ", " + descuento + " , (Cast(replace('" + Math.Round(impuesto1, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(impuesto2, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(importeTotal, 2) + "', ',', '.') as decimal(10,2))), (Cast(replace('" + Math.Round(-importeTotal, 2) + "', ',', '.') as decimal(10,2))), '" + txtObservacionFactura.Text + "', " + IDEMPRESA + ")";
                    else
                        agregar = "INSERT INTO FacturasVentas(NROFACTURA, SUCURSAL, IDTIPOFACTURA, FECHA, IDCLIENTE, IDVENDEDOR, IDPERSONAL, IDFORMAPAGO, BASICO, PORCDESC, DESCUENTOS, IMPUESTO1, IMPUESTO2, TOTAL, PENDIENTE, OBSERVACIONES, IDEMPRESA) VALUES ('" + txtNroFacturaVenta.Text.Trim() + "', '" + cboNroSucursal.Text.Trim() + "', '5', '" + FormateoFecha() + "', " + txtCodCliente.Text.Trim() + ", " + txtCodVendedor.Text + ", " + txtCodPersonal.Text.Trim() + ", " + txtCodFormaPago.Text.Trim() + ", (Cast(replace('" + Math.Round(subTotal, 3) + "', ',', '.') as decimal(10,3))), " + 0 + ", " + descuento + " , (Cast(replace('" + Math.Round(impuesto1, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(impuesto2, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(importeTotal, 2) + "', ',', '.') as decimal(10,2))), (Cast(replace('" + Math.Round(-importeTotal, 2) + "', ',', '.') as decimal(10,2))),'" + txtObservacionFactura.Text + "', " + IDEMPRESA + ")";

                    this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeTotal, 2));
                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto1, 2));
                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto2, 2));
                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)subTotal, 2));

                    if (conn.InsertarGeneric(agregar))
                    {
                        MostrarDatos(0, 30);
                        GuardaItemsDatos(true, 0);
                        lvwFacturaVenta.Items[lvwFacturaVenta.Items.Count - 1].Selected = true;
                        txtNroInternoFact.Text = lvwFacturaVenta.Items[lvwFacturaVenta.Items.Count - 1].Text;
                        idNROFACTUINTERNO = Convert.ToInt32(lvwFacturaVenta.Items[lvwFacturaVenta.Items.Count - 1].Text);
                    }
                    else
                        MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    GuardaItemsDatos(false, idNROFACTUINTERNO);
            }
            catch //(System.Exception excep)
            {
                conn.DesconectarBD(); connGeneric.DesconectarBD(); connGeneric.DesconectarBDLeeGeneric();

                //Auditoria
                AuditoriaSistema AS5 = new AuditoriaSistema();
                AS5.SistemaProcesoAuditor_0005("Proc. GuardarTodosLosDatos() ", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////               
            }
        }

        private void btnQuitaArt_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtCodVendedor.Text != "")
                {

                    bool EstadoFactura;
                    EstadoFactura = LeeEstadoFactura(Convert.ToInt32(this.txtNroInternoFact.Text));

                    string EstadoFacturaAnulado;
                    EstadoFacturaAnulado = EstadoFacturaAnulada(Convert.ToInt32(this.txtNroInternoFact.Text));

                    if (EstadoFacturaAnulado == "19")
                        MessageBox.Show("Error factura anulada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        if (EstadoFactura == false)
                        {

                            int iIndex = 0;

                            if (fechaFacturaVenta.AddDays(180) <= DateTime.Today)
                                MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                //timer1.Enabled = false;
                                if (chkBoxPresupuesto.CheckState == CheckState.Unchecked)
                                    actualizaStock_Anulacion_o_eliminacion(Convert.ToInt32(txtNroInternoFact.Text), lvwDetalleFacturaVenta.SelectedItems[0].SubItems[1].Text.Trim());

                                iIndex = Convert.ToInt32(lvwDetalleFacturaVenta.SelectedItems[0].SubItems[11].Text);  //Elemento de la base de datos
                                lvwDetalleFacturaVenta.Items[lvwDetalleFacturaVenta.SelectedItems[0].Index].Remove(); //Elemento del listview


                                if (connArt.EliminarArticulo("DetalleFacturaVentas", " IDDETALLEFACTURAVENTAS = " + iIndex))
                                    //MostrarItemsDatos2(idNROFACTUINTERNO);

                                    if (lvwDetalleFacturaVenta.Items.Count != 0)
                                    {
                                        if (iIndex != 0)
                                        {
                                            string subTotalfactu;
                                            string iva105Factu;
                                            string iva21Factu;
                                            string importeFactu;
                                            decimal importeFactuPendiente;

                                            connGeneric.DesconectarBDLeeGeneric();
                                            connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleFacturaVentas WHERE NROFACTURAINTERNO = " + idNROFACTUINTERNO + "", "DetalleFacturaVentas");

                                            importeFactu = connGeneric.leerGeneric["Importe"].ToString();
                                            iva105Factu = connGeneric.leerGeneric["Iva105"].ToString();
                                            iva21Factu = connGeneric.leerGeneric["Iva21"].ToString();
                                            subTotalfactu = connGeneric.leerGeneric["SubTotal"].ToString();
                                            importeFactuPendiente = Convert.ToDecimal(connGeneric.leerGeneric["Importe"].ToString());

                                            string actualizar = "BASICO=(Cast(replace('" + subTotalfactu + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + iva105Factu + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + iva21Factu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeFactu + "', ',', '.') as decimal(10,2))), PENDIENTE=(Cast(replace('" + -importeFactuPendiente + "', ',', '.') as decimal(10, 2)))";

                                            this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(importeFactu), 2));
                                            this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(iva105Factu), 2));
                                            this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(iva21Factu), 2));
                                            this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(subTotalfactu), 3));

                                            if (connGeneric.ActualizaGeneric("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + idNROFACTUINTERNO + ""))
                                            {
                                                MostrarDatos2(idNROFACTUINTERNO);
                                                MostrarItemsDatos2(idNROFACTUINTERNO);
                                                // MessageBox.Show("La información de la factura ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                                MessageBox.Show("No se ha podido actualizar los datos de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }

                                        dSubTotal = 0.000;
                                        dImpuesto1 = 0.000;
                                        dImpuesto2 = 0.000;
                                        dImporteTotal = 0.00;

                                        char[] QuitaSimbolo = { '$', ' ' };

                                        for (int i = 0; i < (lvwDetalleFacturaVenta.Items.Count); i++)
                                        {
                                            dSubTotal += Math.Round(Convert.ToSingle(lvwDetalleFacturaVenta.Items[i].SubItems[7].Text.TrimStart(QuitaSimbolo)), 3);
                                            dImpuesto1 += Math.Round(Convert.ToSingle(lvwDetalleFacturaVenta.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo)), 2);
                                            dImpuesto2 += Math.Round(Convert.ToSingle(lvwDetalleFacturaVenta.Items[i].SubItems[13].Text.TrimStart(QuitaSimbolo)), 2);
                                            dImporteTotal += Math.Round(Convert.ToSingle(lvwDetalleFacturaVenta.Items[i].SubItems[9].Text.TrimStart(QuitaSimbolo)), 2);
                                        }

                                        this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImporteTotal, 2));
                                        this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImpuesto1, 2));
                                        this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImpuesto2, 2));
                                        this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dSubTotal, 2));
                                    }
                                    else
                                    {
                                        this.txtImporteTotal.Text = "$ " + "0,00";
                                        this.txtImpuesto1.Text = "$ " + "0,00";
                                        this.txtImpuesto2.Text = "$ " + "0,00";
                                        this.txtSubTotal.Text = "$ " + "0,00";

                                        string actualizar = "BASICO=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,2))), PENDIENTE=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,2)))";
                                        connGeneric.ActualizaGeneric("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + idNROFACTUINTERNO + "");
                                        MostrarDatos2(idNROFACTUINTERNO);
                                        MostrarItemsDatos2(idNROFACTUINTERNO);
                                    }
                            }
                        }
                        else
                            MessageBox.Show("Error factura cerrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Error Datos Incompletos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch //(System.Exception excep) 
            {
                conn.DesconectarBD(); MostrarItemsDatos();

                //Auditoria
                AuditoriaSistema AS6 = new AuditoriaSistema();
                AS6.SistemaProcesoAuditor_0006("Evento btnQuitaArt_Click()", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////
            }
        }

        private void MostrarItemsDatos()
        {
            try
            {
                lvwDetalleFacturaVenta.Items.Clear();

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                double iva105;
                double iva21;

                //int iCantPedida, iCantActual, iCantRestante;
                //int CantPendiente;

                connGeneric.LeeGeneric("SELECT FacturasVentas.NROFACTURAINTERNO, DetalleFacturaVentas.IDDETALLEREMITO, DetalleFacturaVentas.IDDETALLEFACTURAVENTAS, Articulo.DESCRIPCION as 'Artículo', Articulo.CANT_ACTUAL, DetalleFacturaVentas.CANTIDAD, DetalleFacturaVentas.PRECUNITARIO as 'Precio Unitario', DetalleFacturaVentas.IMPORTE as 'Importe', DetalleFacturaVentas.DESCUENTO as 'Descuento', DetalleFacturaVentas.PORCDESC as '% Desc', DetalleFacturaVentas.SUBTOTAL as 'Subtotal', DetalleFacturaVentas.IMPUESTO1 as 'Iva 10,5', DetalleFacturaVentas.IMPUESTO2 as 'Iva 21', DetalleFacturaVentas.OBSERVACIONES as 'Observaciones' FROM FacturasVentas, DetalleFacturaVentas, Articulo, Cliente, Personal WHERE FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND DetalleFacturaVentas.IDARTICULO = Articulo.IDARTICULO AND FacturasVentas.IDCLIENTE = Cliente.IDCLIENTE AND FacturasVentas.IDPERSONAL = Personal.IDPERSONAL AND DetalleFacturaVentas.NROFACTURAINTERNO = FacturasVentas.NROFACTURAINTERNO AND FacturasVentas.NROFACTURAINTERNO = " + Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text) + "", "FacturasVentas");

                iva105 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 10,5"].ToString());
                iva21 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 21"].ToString());

                SqlCommand cm = new SqlCommand("SELECT Articulo.Codigo, FacturasVentas.NROFACTURAINTERNO, DetalleFacturaVentas.IDDETALLEFACTURAVENTAS, DetalleFacturaVentas.NROFACTURAINTERNO, DetalleFacturaVentas.IDDETALLEREMITO, DetalleFacturaVentas.IDArticulo, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL, DetalleFacturaVentas.CANTIDAD, DetalleFacturaVentas.PRECUNITARIO, DetalleFacturaVentas.IMPORTE, DetalleFacturaVentas.DESCUENTO, DetalleFacturaVentas.PORCDESC, DetalleFacturaVentas.SUBTOTAL, DetalleFacturaVentas.IMPUESTO1 as 'Iva 10,5', DetalleFacturaVentas.IMPUESTO2 as 'Iva 21', DetalleFacturaVentas.OBSERVACIONES FROM FacturasVentas, DetalleFacturaVentas, Articulo, Cliente, Personal WHERE FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND DetalleFacturaVentas.IDARTICULO = Articulo.IDARTICULO AND FacturasVentas.IDCLIENTE = Cliente.IDCLIENTE AND FacturasVentas.IDPERSONAL = Personal.IDPERSONAL AND DetalleFacturaVentas.NROFACTURAINTERNO = FacturasVentas.NROFACTURAINTERNO AND FacturasVentas.NROFACTURAINTERNO =" + Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text) + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwDetalleFacturaVenta.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDArticulo"].ToString());
                    item.SubItems.Add(dr["Codigo"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());

                    item.SubItems.Add(dr["CANTIDAD"].ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["PRECUNITARIO"]), 3).ToString());
                    item.SubItems.Add("% " + Math.Round(Convert.ToDecimal(dr["PORCDESC"]), 2).ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["DESCUENTO"]), 2).ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Subtotal"]), 2).ToString());

                    //string sprueba;
                    //sprueba = dr["Iva 10,5"].ToString().Trim();

                    if (dr["Iva 10,5"].ToString().Trim() != "0,0000")
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 2).ToString());
                    else
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 21"]), 2).ToString());

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPORTE"]), 2).ToString());
                    //item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.Empty, Color.LightGray, null);
                    //item.SubItems.Add(dr["CANTIDADRESTANTE"].ToString(), Color.Empty, Color.LightGray, null);

                    item.SubItems.Add(dr["OBSERVACIONES"].ToString());
                    item.SubItems.Add(dr["IDDETALLEFACTURAVENTAS"].ToString());
                    item.SubItems.Add(dr["Iva 10,5"].ToString());
                    item.SubItems.Add(dr["Iva 21"].ToString());

                    item.ImageIndex = 1;

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleFacturaVenta.Items.Add(item);
                }
                cm.Connection.Close();
                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
            }
            catch //(System.Exception excep)
            {
                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                //Auditoria
                AuditoriaSistema AS7 = new AuditoriaSistema();
                AS7.SistemaProcesoAuditor_0007("Proc. MostrarItemsDatos()", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////
            }
        }

        private void lvwFacturaVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //timer1.Enabled = false;

                nuevoRemito = false;
                bNumeracion = false;

                gboRemitoFactura.Visible = false;



                /*if (txtCodTipoFactura.Text.Trim() == "5" && cboTipoFactura.Text.Trim()=="P")
                    btnCerrarFactura.Visible = false;
                else
                    btnCerrarFactura.Visible = true;*/

                MostrarItemsDatos();

                idNROFACTUINTERNO = Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text);
                indiceLvwNotaPedido = lvwFacturaVenta.SelectedItems[0].Index;


                SqlCommand cm = new SqlCommand("SELECT * FROM FacturasVentas, Cliente, ListaPrecios WHERE FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDLISTAPRECIO = ListaPrecios.IDLISTAPRECIO AND Cliente.IDCLIENTE = FacturasVentas.IDCLIENTE AND NROFACTURAINTERNO = " + Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text) + "", conectaEstado);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    this.txtNroInternoFact.Text = dr["NROFACTURAINTERNO"].ToString();
                    this.cboNroSucursal.Text = dr["SUCURSAL"].ToString();
                    this.txtNroFacturaVenta.Text = dr["NROFACTURA"].ToString();

                    this.dtpFechaFactu.Value = Convert.ToDateTime(dr["FECHA"].ToString());
                    fechaFacturaVenta = Convert.ToDateTime(dr["FECHA"].ToString());

                    this.txtCodCliente.Text = dr["IDCLIENTE"].ToString();
                    if (this.txtCodCliente.Text.Trim() == "")
                        this.cboCliente.Text = "";

                    this.txtCodPersonal.Text = dr["IDPERSONAL"].ToString();
                    if (this.txtCodPersonal.Text.Trim() == "")
                        this.cboPersonal.Text = "";

                    this.txtCodTipoFactura.Text = dr["IDTIPOFACTURA"].ToString();
                    iTipoFactura = Convert.ToInt32(txtCodTipoFactura.Text);
                    if (this.txtCodTipoFactura.Text.Trim() == "")
                        this.cboTipoFactura.Text = "";

                    if (iTipoFactura == 5)
                        chkBoxPresupuesto.CheckState = CheckState.Checked;
                    else
                        chkBoxPresupuesto.CheckState = CheckState.Unchecked;

                    this.txtCodFormaPago.Text = dr["IDFORMAPAGO"].ToString();
                    if (this.txtCodFormaPago.Text.Trim() == "")
                        this.cboFormaPago.Text = "";

                    this.txtCodVendedor.Text = dr["IDVENDEDOR"].ToString();
                    if (this.txtCodVendedor.Text.Trim() == "")
                        this.cboVendedor.Text = "";

                    //this.txtCodListaCliente.Text = conn.leerGeneric["IDLISTAPRECIO"].ToString();
                    //cboListaCliente.Text = connArt.leer["DescLista"].ToString();

                    this.txtObservacionFactura.Text = dr["OBSERVACIONES"].ToString();

                    this.txtSubTotal.Text = "$ " + Math.Round(Convert.ToDecimal(dr["BASICO"]), 2).ToString();
                    this.txtImpuesto1.Text = "$ " + Math.Round(Convert.ToDecimal(dr["IMPUESTO1"]), 2).ToString();
                    this.txtImpuesto2.Text = "$ " + Math.Round(Convert.ToDecimal(dr["IMPUESTO2"]), 2).ToString();
                    this.txtDescuento.Text = "$ " + Math.Round(Convert.ToDecimal(dr["DESCUENTOS"]), 2).ToString();
                    this.txtImporteTotal.Text = "$ " + Math.Round(Convert.ToDecimal(dr["TOTAL"]), 2).ToString();

                    bool bFactuImpresa;
                    bFactuImpresa = Convert.ToBoolean(dr["IMPRESA"].ToString());
                    if (bFactuImpresa == true)
                        chkImpresa.CheckState = CheckState.Checked;
                    else
                        chkImpresa.CheckState = CheckState.Unchecked;

                }



                cm.Connection.Close();

                //btnGuardar.Enabled = true;                
                MostrarItemsDatos();
                // LimpiarDetalleNotaPedido();

                //  if (fechaFacturaCompra.AddDays(1) <= DateTime.Today)                                
                //  MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                conPermi();

                /*if (txtObservacionFactura.Text.Trim() == "Pago Contado")
                    btnCerrarFactura.Visible = false;*/

            }
            catch //(System.Exception excep) 
            {
                //Auditoria
                AuditoriaSistema AS8 = new AuditoriaSistema();
                AS8.SistemaProcesoAuditor_0008("Evento lvwFacturaVenta_SelectedIndexChanged() ", frmPrincipal.Usuario);
                ////////////////////////////////////////////////////// 
            }
        }

        private void txtCodArticulo_TextChanged(object sender, EventArgs e)
        {
            try

            {
                string sCodArt;
                char[] QuitaSimbolo = { '$', ' ' };
                char[] QuitaSimbolo2 = { '*', ' ' };

                sCodArt = txtCodArticulo.Text.TrimStart(QuitaSimbolo2);
                sCodArt = sCodArt.TrimEnd(QuitaSimbolo2);
                this.txtCodArticulo.Text = sCodArt;

                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();


                if (this.txtCodArticulo.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("SELECT * FROM Articulo WHERE CODIGO = '" + this.txtCodArticulo.Text.Trim() + "'", "Articulo");

                    this.cboArticulo.DataSource = conn.ds.Tables[0];
                    this.cboArticulo.ValueMember = "IdArticulo";
                    this.cboArticulo.DisplayMember = "Descripcion";
                }
                else
                {
                    cboArticulo.Text = "";
                    txtPrecio.Text = "";
                    txtCantArticulo.Text = "";
                    txtProcDesc.Text = "0";
                    txtDescuento.Text = "0";
                }

                conn.DesconectarBD();

                if (conn.ds.Tables[0].Rows.Count < 1)
                {
                    cboArticulo.Text = "";
                    txtCantArticulo.Text = "";
                    txtPrecio.Text = "";
                    txtDescuento.Text = "0";
                    txtProcDesc.Text = "0";
                }
                else
                {
                    conn.LeeGeneric("SELECT * FROM Articulo WHERE CODIGO = '" + this.txtCodArticulo.Text.Trim() + "'", "Articulo");
                    //txtCantArticulo.Text = conn.leerGeneric["CANT_ACTUAL"].ToString();
                    txtCantArticulo.Text = "";
                    txtProcDesc.Text = "0";
                    idArtuculo = Convert.ToInt32(conn.leerGeneric["IdArticulo"].ToString());

                    if (cboFormaPago.Text.Trim() == "PAGO MOSTRADOR" || txtCodFormaPago.Text.Trim() == "1")
                        txtPrecio.Text = "$ " + Math.Ceiling(CalculoPorcentajeListaVenta(Convert.ToDouble(conn.leerGeneric["COSTOENLISTA"].ToString())));
                    else
                        txtPrecio.Text = "$ " + Math.Round(CalculoPorcentajeListaVenta(Convert.ToDouble(conn.leerGeneric["COSTOENLISTA"].ToString())), 3);


                    ValorUnitarioArticulo = Convert.ToDouble(txtPrecio.Text.TrimStart(QuitaSimbolo));

                    conn.DesconectarBDLeeGeneric();
                }
            }
            catch
            {
                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();
            }
        }

        private void btnCargaRemito_Click(object sender, EventArgs e)
        {
            //frmRemito formRemito = new frmRemito();
            //formCliente.pasarClienteCod += new frmCliente.pasarClienteCod1(CodClient);  //Delegado1 
            //formCliente.pasarClientRS += new frmCliente.pasarClienteRS(RazonS); //Delegado2
            //formRemito.ShowDialog();
        }

        private void tsBtnRemito_Click(object sender, EventArgs e)
        {
            try
            {
                NuevaFactura();

                frmRemito formRemito = new frmRemito();
                formRemito.pasarCodRemito += new frmRemito.pasarCodRemito1(CodRemito); //Delegado2
                formRemito.pasarCodCliente += new frmRemito.pasarCodCliente1(CodCliente);  //Delegado1             
                formRemito.ShowDialog();

                connGeneric.LeeGeneric("SELECT * FROM Remito WHERE IDEMPRESA = " + IDEMPRESA + " AND NROREMITOINTERNO = " + iNumRemito + "", "Remito");
                if (connGeneric.leerGeneric["ESTADOREMITO"].ToString() == "FACTURADO")
                {
                    MessageBox.Show("El remito ya ha sido facturado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    NuevaFactura();
                }
                else
                {
                    connGeneric.LeeGeneric("SELECT * FROM Remito WHERE IDEMPRESA = " + IDEMPRESA + " AND NROREMITOINTERNO = " + iNumRemito + "", "Remito");
                    iTipoRemito = Convert.ToInt32(connGeneric.leerGeneric["IdTipoRemito"].ToString());
                    ArmadoFacturaRemito(iNumRemito, Convert.ToInt32(txtCodCliente.Text), iTipoRemito);
                }
            }
            catch //(System.Exception excep)
            {
                //Auditoria
                AuditoriaSistema AS8 = new AuditoriaSistema();
                AS8.SistemaProcesoAuditor_0008("Evento tsBtnRemito_Click()", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////  
            }
        }

        public void CodRemito(int CodRemito)
        {
            try
            {
                iNumRemito = CodRemito;

                //Si NO existe Nota de Pedido
                conn.LeeGeneric("SELECT * FROM Remito WHERE IDEMPRESA = " + IDEMPRESA + " AND Remito.NROREMITOINTERNO = " + iNumRemito + "", "Remito");
                txtCodPersonal.Text = conn.leerGeneric["IDPERSONAL"].ToString();
                txtCodFormaPago.Text = conn.leerGeneric["IDFORMAPAGO"].ToString();
                conn.DesconectarBDLeeGeneric();

                conn.LeeGeneric("SELECT * FROM Personal WHERE Usuario = '" + frmPrincipal.Usuario + "'", "Personal");
                txtCodVendedor.Text = conn.leerGeneric["IDPERSONAL"].ToString(); ;
                conn.DesconectarBDLeeGeneric();
                //////////////////////////////////////////////////////////////////////


                //Si existe Nota de Pedido
                conn.LeeGeneric("SELECT * FROM Remito, NotaPedido WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND NotaPedido.IDEMPRESA = " + IDEMPRESA + " AND Remito.IDNOTAPEDIDO=NotaPedido.IDNOTAPEDIDO AND Remito.NROREMITOINTERNO = " + iNumRemito + "", "Remito");
                txtCodPersonal.Text = conn.leerGeneric["IDPERSONAL"].ToString();
                txtCodFormaPago.Text = conn.leerGeneric["IDFORMAPAGO"].ToString();
                txtCodVendedor.Text = conn.leerGeneric["IDVENDEDOR"].ToString();
                conn.DesconectarBDLeeGeneric();
                //////////////////////////////////////////////////////////////////////

            }
            catch //(System.Exception excep)
            {
                conn.DesconectarBDLeeGeneric();
                //Auditoria
                AuditoriaSistema AS9 = new AuditoriaSistema();
                AS9.SistemaProcesoAuditor_0009("Proc. CodRemito()", frmPrincipal.Usuario);
                ////////////////////////////////////////////////////// 
            }
        }

        public void CodCliente(int CodCliente)
        {
            this.txtCodCliente.Text = CodCliente.ToString();

            if (txtIva.Text == "Responsable inscripto")
                txtCodTipoFactura.Text = "1";
            else
                txtCodTipoFactura.Text = "2";
        }

        private void ArmadoFacturaRemito(int CodRemito, int CodCliente, int TipoRemito) //Funcion de Facturacion de Remitos Pendientes y seleccionados
        {
            try
            {
                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                int iCantRemitos = 0;
                conn.LeeGeneric("SELECT COUNT(*) as 'Remitos' FROM Remito, Cliente WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND Remito.ESTADOREMITO = 'NO FACTURADO' AND CLIENTE.IDCLIENTE=Remito.IDCLIENTE AND CLIENTE.IDCLIENTE = " + CodCliente + " AND REMITO.SUCURSAL = '" + sPtoVta + "'", "Remito");
                iCantRemitos = Convert.ToInt32(conn.leerGeneric["Remitos"].ToString());

                if (iCantRemitos > 1)
                {
                    DialogResult dialogResult = MessageBox.Show("¿Desea Facturar todos los remitos pendientes del cliente seleccionado?", "Facturar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {

                        GuardaFacturaDeTodosLosRemitosPendientesDelCliente(Convert.ToInt32(txtCodCliente.Text), TipoRemito);

                        if (nroFacturaInterCreado != 0)
                            MostrarItemsDatos2(nroFacturaInterCreado);
                    }
                    else
                    {
                        gpbRemitoPendiente.Visible = true;
                        gpFactura.Enabled = false;
                        gpDetalleFactura.Enabled = false;
                        gpoFacturacion.Enabled = false;
                        gpCompraProveedor.Enabled = false;

                        lvwRemitoPendiente.Items.Clear();
                        SqlCommand cm = new SqlCommand("SELECT * FROM Remito, Cliente WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND ESTADOREMITO = 'NO FACTURADO' AND remito.IDCLIENTE = Cliente.IDCLIENTE AND remito.IDCLIENTE = " + CodCliente + " AND REMITO.SUCURSAL = '" + sPtoVta + "'", conectaEstado);

                        SqlDataAdapter da = new SqlDataAdapter(cm);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        foreach (DataRow dr in dt.Rows)
                        {
                            lvwRemitoPendiente.SmallImageList = imageList1;
                            ListViewItem item = new ListViewItem(dr["NROREMITOINTERNO"].ToString());
                            item.SubItems.Add(dr["NROREMITO"].ToString());
                            item.SubItems.Add(dr["Fecha"].ToString());
                            item.SubItems.Add(dr["IdCliente"].ToString());
                            item.SubItems.Add(dr["RazonSocial"].ToString());
                            item.SubItems.Add(dr["ESTADOREMITO"].ToString());

                            if (dr["ESTADOREMITO"].ToString() == "NO FACTURADO")
                                item.ImageIndex = 3;
                            else
                                item.ImageIndex = 4;

                            item.UseItemStyleForSubItems = false;
                            lvwRemitoPendiente.Items.Add(item);

                        }
                        cm.Connection.Close();
                    }
                }
                else
                {
                    GuardaFacturaSegunRemitoSeleccionado(iNumRemito, Convert.ToInt32(txtCodCliente.Text), false, TipoRemito);

                    MostrarItemsDatos2(nroFacturaInterCreado);
                }

                /*SqlCommand cm1 = new SqlCommand("SELECT * FROM Remito, Cliente WHERE Remito.ESTADOREMITO = 'NO FACTURADO' AND CLIENTE.IDCLIENTE=Remito.IDCLIENTE AND CLIENTE.IDCLIENTE = " + Convert.ToInt32(txtCodCliente.Text) + "", conectaEstado);

                SqlDataAdapter da1 = new SqlDataAdapter(cm1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                foreach (DataRow dr1 in dt1.Rows)
                {
                }
                cm1.Connection.Close();*/

                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();
            }
            catch //(System.Exception excep)
            {
                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                //Auditoria
                AuditoriaSistema AS10 = new AuditoriaSistema();
                AS10.SistemaProcesoAuditor_0010("Proc. ArmadoFacturaRemito()", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////      
            }
        }

        private void btnCerrarRemitosPendientes_Click(object sender, EventArgs e)
        {
            gpbRemitoPendiente.Visible = false;
            gpbDetalleRemito.Visible = false;
            gpFactura.Enabled = true;
            gpDetalleFactura.Enabled = true;
            gpoFacturacion.Enabled = true;
            gpCompraProveedor.Enabled = true;
        }

        private void lvwRemitoPendiente_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*  try
              {
                  GuardaFacturaSegunRemitoSeleccionado(Convert.ToInt32(lvwRemitoPendiente.SelectedItems[0].SubItems[0].Text), Convert.ToInt32(lvwRemitoPendiente.SelectedItems[0].SubItems[3].Text));

                  gpbRemitoPendiente.Visible = false;
                  gpFactura.Enabled = true;
                  gpDetalleFactura.Enabled = true;
                  gpoFacturacion.Enabled = true;
                  gpCompraProveedor.Enabled = true;
              }
              catch //(System.Exception excep) 
              {
                  //Auditoria
                  AuditoriaSistema AS11 = new AuditoriaSistema();
                  AS11.SistemaProcesoAuditor_0011("Proc. lvwRemitoPendiente_MouseDoubleClick()", frmPrincipal.Usuario);
                  //////////////////////////////////////////////////////
              }*/
        }

        private void GuardaFacturaDeTodosLosRemitosPendientesDelCliente(int iCodCliente, int TipoRemito)
        {
            try
            {
                flagControlUnificacionComprobante = true;
                double PUnitario = 0;
                double subTotal = 0;
                double impuesto1 = 0;
                double impuesto2 = 0;
                double importeTotal = 0;
                double descuento = 0;
                int nroFacturaInterCreado = 0;
                int Item;
                int iCodRemitoACargado = 0;

                ///Variables para recalculo de valores de entregas parciales

                //double descuento = 0;
                int idImpuesto = 0;
                //////////////////////////////

                //Quita Simbolos para guardar los datos en formato numéricos
                char[] QuitaSimbolo = { '$', ' ' };
                importeTotal = Convert.ToSingle(this.txtImporteTotal.Text.TrimStart(QuitaSimbolo));
                impuesto1 = Convert.ToSingle(this.txtImpuesto1.Text.TrimStart(QuitaSimbolo));
                impuesto2 = Convert.ToSingle(this.txtImpuesto2.Text.TrimStart(QuitaSimbolo));
                subTotal = Convert.ToSingle(this.txtSubTotal.Text.TrimStart(QuitaSimbolo));
                descuento = Convert.ToSingle(this.txtDescuento.Text.TrimStart(QuitaSimbolo));
                /////////////////////////////////////////////////////////////////////////////////

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                if (lvwDetalleFacturaVenta.Items.Count == 0)
                {
                    if (TipoRemito == 1)
                    {
                        txtCodTipoFactura.Text = "4";
                        txtNroFacturaVenta.Text = UltimoNroFacturaSegunTipoComprobante(4).ToString();
                    }

                    else if (txtIva.Text == "Exento")
                    {
                        txtCodTipoFactura.Text = "3";
                        txtNroFacturaVenta.Text = UltimoNroFacturaSegunTipoComprobante(2).ToString();
                    }

                    else if (txtIva.Text == "Consumidor Final" || txtIva.Text == "Consumidor final" || txtIva.Text == "consumidor final")
                    {
                        txtCodTipoFactura.Text = "2";
                        txtNroFacturaVenta.Text = UltimoNroFacturaSegunTipoComprobante(2).ToString();
                    }

                    else if (txtIva.Text == "Monotributista" || txtIva.Text == "monotributista")
                    {
                        txtCodTipoFactura.Text = "2";
                        txtNroFacturaVenta.Text = UltimoNroFacturaSegunTipoComprobante(2).ToString();
                    }

                    else if (txtIva.Text == "Responsable inscripto" || txtIva.Text == "Responsable Inscripto")
                    {
                        txtCodTipoFactura.Text = "1";
                        txtNroFacturaVenta.Text = UltimoNroFacturaSegunTipoComprobante(1).ToString();
                    }
                    else
                    {
                        txtCodTipoFactura.Text = "1";
                        txtNroFacturaVenta.Text = UltimoNroFacturaSegunTipoComprobante(1).ToString();
                    }

                    if (txtCodFormaPago.Text.Trim() == "")
                        txtCodFormaPago.Text = "1";

                    string agregar = "INSERT INTO FacturasVentas(NROFACTURA, SUCURSAL, IDTIPOFACTURA, FECHA, IDCLIENTE, IDPERSONAL, IDFORMAPAGO, BASICO, PORCDESC, DESCUENTOS, IMPUESTO1, IMPUESTO2, TOTAL, PENDIENTE, OBSERVACIONES, IDEMPRESA) VALUES ('" + txtNroFacturaVenta.Text.Trim() + "', '" + cboNroSucursal.Text.Trim() + "', " + txtCodTipoFactura.Text.Trim() + ", '" + FormateoFecha() + "', " + iCodCliente + ", " + txtCodPersonal.Text.Trim() + ", " + txtCodFormaPago.Text.Trim() + ", (Cast(replace('" + Math.Round(subTotal, 3) + "', ',', '.') as decimal(10,3))), " + 0 + ", " + descuento + " , (Cast(replace('" + Math.Round(impuesto1, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(impuesto2, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(importeTotal, 2) + "', ',', '.') as decimal(10,2))), (Cast(replace('" + Math.Round(-importeTotal, 2) + "', ',', '.') as decimal(10,2))), '" + txtObservacionFactura.Text + "', " + IDEMPRESA + ")";

                    this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeTotal, 2));
                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto1, 2));
                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto2, 2));
                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)subTotal, 2));

                    if (conn.InsertarGeneric(agregar) == false)
                        MessageBox.Show("Error al Agregar, falta información requerida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MostrarDatos(0, 30);
                        //GuardaItemsDatos(true, 0);
                        //lvwFacturaVenta.Items[lvwFacturaVenta.Items.Count - 1].Selected = true;
                        //txtNroInternoFact.Text = lvwFacturaVenta.Items[lvwFacturaVenta.Items.Count - 1].Text;
                        //nroFacturaInterCreado = Convert.ToInt32(lvwFacturaVenta.Items[lvwFacturaVenta.Items.Count - 1].Text);

                        txtNroInternoFact.Text = UltimaFactura().ToString();
                        nroFacturaInterCreado = Convert.ToInt32(txtNroInternoFact.Text);
                    }
                }

                //LEE TODOS LOS REMITOS PENDIENTES PARA FACTURAR
                SqlCommand cm = new SqlCommand("SELECT * FROM Remito, Cliente WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND ESTADOREMITO = 'NO FACTURADO' AND remito.IDCLIENTE = Cliente.IDCLIENTE AND remito.IDCLIENTE = " + iCodCliente + " ORDER BY NROREMITOINTERNO", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    //LEE CADA REMITO DEL CLIENTE
                    SqlCommand cm1 = new SqlCommand("SELECT Articulo.*, DetalleRemito.* FROM Articulo, DetalleRemito WHERE DetalleRemito.IDARTICULO = Articulo.IDARTICULO AND DetalleRemito.NROREMITOINTERNO = " + dr["NROREMITOINTERNO"].ToString() + " ORDER BY IdDetalleRemito", conectaEstado);

                    SqlDataAdapter da1 = new SqlDataAdapter(cm1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        Item = 0;
                        string sImpuesto1 = "0,0000", sImpuesto2 = "0,0000";
                        lvwDetalleFacturaVenta.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr1["IDARTICULO"].ToString());
                        item.SubItems.Add(dr1["Codigo"].ToString());
                        item.SubItems.Add(dr1["Descripcion"].ToString());
                        item.SubItems.Add(dr1["CANTIDAD"].ToString());
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr1["PRECUNITARIO"]), 3).ToString());

                        item.SubItems.Add("% 0");
                        item.SubItems.Add("$ " + "0");

                        PUnitario = Math.Round(Convert.ToSingle(dr1["PRECUNITARIO"]), 3);
                        subTotal = PUnitario * Convert.ToInt32(dr1["CANTIDAD"]);
                        CostoEnLista = Convert.ToDouble(dr1["CostoEnLista"].ToString());

                        importeTotal = CalculoPorcentajeListaVenta(CostoEnLista) * Convert.ToInt32(dr1["CANTIDAD"]);
                        idImpuesto = Convert.ToInt32(dr1["IDIMPUESTO"].ToString());

                        sumaTotales += subTotal;
                        txtSubTotal.Text = "$ " + Math.Round(sumaTotales, 3).ToString();

                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(subTotal), 3).ToString());                ///SubTotal
                        //item.SubItems.Add(dr["DESCUENTO"].ToString());                                       

                        if (idImpuesto == 3 || TipoRemito == 1 || txtIva.Text == "Exento")
                        {
                            impuesto2 = Math.Round(((subTotal * 1) - subTotal), 2);
                            Neto = Math.Round((importeTotal + impuesto2), 3);
                            item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(impuesto2), 2).ToString());
                            item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());
                            sumaImpuesto2 += impuesto2;
                            txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto2, 2).ToString();
                            sumaNetos += Neto;
                            txtImporteTotal.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                        }

                        else if (idImpuesto == 2 && TipoRemito != 1)
                        {
                            impuesto2 = Math.Round(((subTotal * 1.105) - subTotal), 2);
                            Neto = Math.Round((importeTotal + impuesto2), 3);
                            item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(impuesto2), 2).ToString());
                            item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());
                            sumaImpuesto2 += impuesto2;
                            txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto2, 2).ToString();
                            sumaNetos += Neto;
                            txtImporteTotal.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                        }
                        else if (idImpuesto == 1 && TipoRemito != 1)
                        {
                            impuesto1 = Math.Round(((subTotal * 1.21) - subTotal), 2);
                            Neto = Math.Round((importeTotal + impuesto1), 3);
                            item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(impuesto1), 2).ToString());
                            item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());
                            sumaImpuesto1 += impuesto1;
                            txtImpuesto2.Text = "$ " + Math.Round(sumaImpuesto1, 2).ToString();
                            sumaNetos += Neto;
                            txtImporteTotal.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                        }

                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr1["IMPORTE"]), 2).ToString());

                        item.SubItems.Add(dr1["OBSERVACIONES"].ToString());
                        item.ImageIndex = 1;
                        item.SubItems.Add(dr1["IDDETALLENOTAPEDIDO"].ToString());

                        if (idImpuesto != 2)
                            impuesto2 = 0;
                        if (idImpuesto != 1)
                            impuesto1 = 0;

                        item.SubItems.Add(Convert.ToDecimal(Math.Round(impuesto2, 2)).ToString());
                        item.SubItems.Add(Convert.ToDecimal(Math.Round(impuesto1, 2)).ToString());

                        item.UseItemStyleForSubItems = false;
                        lvwDetalleFacturaVenta.Items.Add(item);

                        Item = (lvwDetalleFacturaVenta.Items.Count - 1);

                        iCodRemitoACargado = Convert.ToInt32(dr["NROREMITOINTERNO"].ToString());
                        GuardaItemDatoRemito(Item, iCodRemitoACargado);
                    }

                    //////////////////////////////////////////////// ACTUALIZA EL AGREGADO DE DATOS ////////////////////////////////////////////////
                    connGeneric.DesconectarBDLeeGeneric();
                    connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleFacturaVentas WHERE NROFACTURAINTERNO = " + nroFacturaInterCreado + "", "DetalleFacturasVentas");

                    subTotal = Convert.ToSingle(connGeneric.leerGeneric["SubTotal"].ToString());
                    impuesto1 = Convert.ToSingle(connGeneric.leerGeneric["Iva105"].ToString());
                    impuesto2 = Convert.ToSingle(connGeneric.leerGeneric["Iva21"].ToString());
                    importeTotal = Convert.ToSingle(connGeneric.leerGeneric["Importe"].ToString());

                    string actualizar = "BASICO=(Cast(replace('" + subTotal + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + impuesto1 + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + impuesto2 + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeTotal + "', ',', '.') as decimal(10,2))), PENDIENTE=(Cast(replace('" + -importeTotal + "', ',', '.') as decimal(10,2)))";

                    this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeTotal, 2));
                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto1, 2));
                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto2, 2));
                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)subTotal, 2));

                    connGeneric.ActualizaGeneric("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + nroFacturaInterCreado + "");
                    connGeneric.DesconectarBD();
                    connGeneric.DesconectarBDLeeGeneric();
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    

                    //ACTUALIZA EL REMITO COMO FACTURADO
                    //timer1.Enabled = false;
                    string actualizarEstadoRemito = "ESTADOREMITO = 'FACTURADO'";

                    if (connGeneric.ActualizaGeneric("Remito", actualizarEstadoRemito, " IDEMPRESA = " + IDEMPRESA + " AND NRORemitoInterno = " + iCodRemitoACargado + ""))
                    {
                        MostrarDatos(0, 30);
                        //MostrarItemsDatos2(iCodRemito);
                        MessageBox.Show("La información del remito N° " + dr["NROREMITOINTERNO"].ToString() + " ha sido insertada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se ha podido actualizar los datos de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                flagControlUnificacionComprobante = true;
                VerificaItemsAgregados();

                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                MostrarItemsDatos2(idNROFACTUINTERNO);
            }

            catch //(System.Exception excep) 
            {
                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                //Auditoria
                AuditoriaSistema AS11 = new AuditoriaSistema();
                AS11.SistemaProcesoAuditor_0011("Proc. GuardaFacturaDeTodosLosRemitosPendientesDelCliente()", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////
            }
        }

        private void VerificaItemsAgregados()
        {
            try
            {
                string CodArticulo = "";
                string CodArticuloANT = "";
                int idDetallerFactVenta = 0;
                int idDetallerFactVentaANT = 0;


                ///Datos de Detalle///
                int iCantidad = 0;
                decimal dSubtotal = 0;
                decimal dImpuesto1 = 0;
                decimal dImpuesto2 = 0;
                decimal dImporte = 0;

                int iCantidadANT = 0;
                decimal dSubtotalANT = 0;
                decimal dImpuesto1ANT = 0;
                decimal dImpuesto2ANT = 0;
                decimal dImporteANT = 0;

                int iCantTotales = 0;
                decimal dSubTotales = 0;
                decimal dImpuesto1Totales = 0;
                decimal dImpuesto2Totales = 0;
                decimal dImporteTotales = 0;

                int iUltimaFactu;
                iUltimaFactu = UltimaFactura();
                idNROFACTUINTERNO = iUltimaFactu;

                //Lee ultima factura cargada
                SqlCommand cm = new SqlCommand("SELECT * FROM DetalleFacturaVentas, Articulo WHERE DetalleFacturaVentas.IDARTICULO = Articulo.IDARTICULO AND DetalleFacturaVentas.NROFACTURAINTERNO = " + idNROFACTUINTERNO + " ORDER BY Articulo.Codigo", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    CodArticulo = dr["Codigo"].ToString().Trim();
                    idDetallerFactVenta = Convert.ToInt32(dr["IdDetalleFacturaVentas"].ToString());
                    iCantidad = Convert.ToInt32(dr["Cantidad"].ToString());
                    dSubtotal = Convert.ToDecimal(dr["Subtotal"].ToString());
                    dImpuesto1 = Convert.ToDecimal(dr["Impuesto1"].ToString());
                    dImpuesto2 = Convert.ToDecimal(dr["Impuesto2"].ToString());
                    dImporte = Convert.ToDecimal(dr["Importe"].ToString());


                    if ((CodArticulo == CodArticuloANT) && (flagControlUnificacionComprobante == true))
                    {
                        iCantTotales = iCantidad + iCantidadANT;
                        dSubTotales = dSubtotal + dSubtotalANT;
                        dImpuesto1Totales = dImpuesto1 + dImpuesto1ANT;
                        dImpuesto2Totales = dImpuesto2 + dImpuesto2ANT;
                        dImporteTotales = dImporte + dImporteANT;

                        string actualizarEstadoRemito = "Cantidad = " + iCantTotales + ", SUBTOTAL = Cast(replace('" + dSubTotales + "', ',', '.') as decimal(10, 3)), IMPUESTO1 = Cast(replace('" + dImpuesto1Totales + "', ',', '.') as decimal(10, 3)), IMPUESTO2 = Cast(replace('" + dImpuesto2Totales + "', ',', '.') as decimal(10, 3)), IMPORTE = Cast(replace('" + dImporteTotales + "', ',', '.') as decimal(10, 3))";
                        connGeneric.ActualizaGeneric("DetalleFacturaVentas", actualizarEstadoRemito, " IdDetalleFacturaVentas = " + idDetallerFactVenta);

                        connGeneric.EliminarGeneric("DetalleFacturaVentas", " IdDetalleFacturaVentas = " + idDetallerFactVentaANT);

                        VerificaItemsAgregados();
                    }


                    CodArticuloANT = CodArticulo;
                    idDetallerFactVentaANT = idDetallerFactVenta;

                    iCantidadANT = iCantidad;
                    dSubtotalANT = dSubtotal;
                    dImpuesto1ANT = dImpuesto1;
                    dImpuesto2ANT = dImpuesto2;
                    dImporteANT = dImporte;

                }
                cm.Connection.Close();
                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                CodArticulo = "";
                CodArticuloANT = "";
                idDetallerFactVenta = 0;
                idDetallerFactVentaANT = 0;


                flagControlUnificacionComprobante = false;

            }
            catch { MessageBox.Show("Error al unificar la información de los comprobantes remito-factura, consultar con el administrador del sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void GuardaFacturaSegunRemitoSeleccionado(int iCodRemito, int iCodCliente, bool iExport, int TipoRemito)
        {
            try
            {
                double PUnitario = 0;
                double subTotal = 0;
                double impuesto1 = 0;
                double impuesto2 = 0;
                double importeTotal = 0;
                double descuento = 0;
                int Item = 0;

                ///Variables para recalculo de valores de entregas parciales

                //double descuento = 0;
                int idImpuesto = 0;
                sumaTotales = 0;
                CostoEnLista = 0;
                sumaImpuesto1 = 0;
                sumaImpuesto2 = 0;
                Neto = 0;
                sumaNetos = 0;
                //////////////////////////////

                //Quita Simbolos para guardar los datos en formato numéricos
                char[] QuitaSimbolo = { '$', ' ' };
                importeTotal = Convert.ToDouble(this.txtImporteTotal.Text.TrimStart(QuitaSimbolo));
                impuesto1 = Convert.ToDouble(this.txtImpuesto1.Text.TrimStart(QuitaSimbolo));
                impuesto2 = Convert.ToDouble(this.txtImpuesto2.Text.TrimStart(QuitaSimbolo));
                subTotal = Convert.ToDouble(this.txtSubTotal.Text.TrimStart(QuitaSimbolo));
                descuento = Convert.ToDouble(this.txtDescuento.Text.TrimStart(QuitaSimbolo));
                /////////////////////////////////////////////////////////////////////////////////

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
                conn.DesconectarBD();


                if (lvwDetalleFacturaVenta.Items.Count == 0 && iExport == false)
                {
                    if (TipoRemito == 1)
                    {
                        txtCodTipoFactura.Text = "4";
                        txtNroFacturaVenta.Text = UltimoNroFacturaSegunTipoComprobante(4).ToString();
                    }

                    else if (txtIva.Text == "Exento")
                    {
                        txtCodTipoFactura.Text = "3";
                        txtNroFacturaVenta.Text = UltimoNroFacturaSegunTipoComprobante(2).ToString();
                    }

                    else if (txtIva.Text == "Consumidor Final" || txtIva.Text == "Consumidor final" || txtIva.Text == "consumidor final")
                    {
                        txtCodTipoFactura.Text = "2";
                        txtNroFacturaVenta.Text = UltimoNroFacturaSegunTipoComprobante(2).ToString();
                    }

                    else if (txtIva.Text == "Monotributista" || txtIva.Text == "monotributista")
                    {
                        txtCodTipoFactura.Text = "2";
                        txtNroFacturaVenta.Text = UltimoNroFacturaSegunTipoComprobante(2).ToString();
                    }

                    else if (txtIva.Text == "Responsable inscripto" || txtIva.Text == "Responsable Inscripto")
                    {
                        txtCodTipoFactura.Text = "1";
                        txtNroFacturaVenta.Text = UltimoNroFacturaSegunTipoComprobante(1).ToString();
                    }
                    else
                    {
                        txtCodTipoFactura.Text = "1";
                        txtNroFacturaVenta.Text = UltimoNroFacturaSegunTipoComprobante(1).ToString();
                    }

                    if (txtCodFormaPago.Text.Trim() == "")
                        txtCodFormaPago.Text = "1";



                    string agregar = "INSERT INTO FacturasVentas(NROFACTURA, SUCURSAL, IDTIPOFACTURA, FECHA, IDCLIENTE, IDPERSONAL, IDFORMAPAGO, BASICO, PORCDESC, DESCUENTOS, IMPUESTO1, IMPUESTO2, TOTAL, PENDIENTE, OBSERVACIONES, IDEMPRESA) VALUES ('" + txtNroFacturaVenta.Text.Trim() + "', '" + cboNroSucursal.Text.Trim() + "', " + txtCodTipoFactura.Text.Trim() + ", '" + FormateoFecha() + "', " + iCodCliente + ", " + txtCodPersonal.Text.Trim() + ", " + txtCodFormaPago.Text.Trim() + ", (Cast(replace('" + Math.Round(subTotal, 3) + "', ',', '.') as decimal(10,3))), " + 0 + ", " + descuento + " , (Cast(replace('" + Math.Round(impuesto1, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(impuesto2, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(importeTotal, 2) + "', ',', '.') as decimal(10,2))), (Cast(replace('" + Math.Round(-importeTotal, 2) + "', ',', '.') as decimal(10,2))), '" + txtObservacionFactura.Text + "', " + IDEMPRESA + ")";

                    this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeTotal, 2));
                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto1, 2));
                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto2, 2));
                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)subTotal, 2));

                    if (conn.InsertarGeneric(agregar) == false)
                        MessageBox.Show("Error al Agregar, falta información requerida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        int iUltimaFactu;

                        iUltimaFactu = UltimaFactura();
                        MostrarDatos2(iUltimaFactu);
                        //GuardaItemsDatos(true, 0);
                        //lvwFacturaVenta.Items[lvwFacturaVenta.Items.Count - 1].Selected = true;
                        //txtNroInternoFact.Text = lvwFacturaVenta.Items[lvwFacturaVenta.Items.Count - 1].Text;
                        txtNroInternoFact.Text = iUltimaFactu.ToString();
                        //nroFacturaInterCreado = Convert.ToInt32(lvwFacturaVenta.Items[lvwFacturaVenta.Items.Count - 1].Text);
                        nroFacturaInterCreado = iUltimaFactu;
                    }
                }

                SqlCommand cm = new SqlCommand("SELECT * FROM Articulo, DetalleRemito WHERE DetalleRemito.IDARTICULO = Articulo.IDARTICULO AND DetalleRemito.NROREMITOINTERNO = " + iCodRemito + " ORDER BY IdDetalleRemito", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Item = 0;
                    string sImpuesto1 = "0,0000", sImpuesto2 = "0,0000";
                    lvwDetalleFacturaVenta.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IdArticulo"].ToString());
                    item.SubItems.Add(dr["Codigo"].ToString());
                    item.SubItems.Add(dr["Descripcion"].ToString());
                    item.SubItems.Add(dr["CANTIDAD"].ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["PRECUNITARIO"]), 3).ToString());

                    item.SubItems.Add("% 0");
                    item.SubItems.Add("$ " + "0");

                    PUnitario = Math.Round(Convert.ToSingle(dr["PRECUNITARIO"]), 3);
                    subTotal = PUnitario * Convert.ToInt32(dr["CANTIDAD"]);
                    CostoEnLista = Convert.ToDouble(dr["CostoEnLista"].ToString());

                    importeTotal = CalculoPorcentajeListaVenta(CostoEnLista) * Convert.ToInt32(dr["CANTIDAD"]);
                    idImpuesto = Convert.ToInt32(dr["IDIMPUESTO"].ToString());

                    sumaTotales += subTotal;
                    txtSubTotal.Text = "$ " + Math.Round(sumaTotales, 3).ToString();

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(subTotal), 3).ToString());                ///SubTotal
                    //item.SubItems.Add(dr["DESCUENTO"].ToString());                                       

                    if (idImpuesto == 3 || TipoRemito == 1 || txtIva.Text.Trim() == "Exento")
                    {
                        impuesto2 = Math.Round(((subTotal * 1) - subTotal), 2);
                        Neto = Math.Round((importeTotal + impuesto2), 3);
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(impuesto2), 2).ToString());
                        item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());
                        sumaImpuesto2 += impuesto2;
                        txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto2, 2).ToString();
                        sumaNetos += Neto;
                        txtImporteTotal.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                    }

                    else if (idImpuesto == 2 && TipoRemito != 1)
                    {
                        impuesto2 = Math.Round(((subTotal * 1.105) - subTotal), 2);
                        Neto = Math.Round((importeTotal + impuesto2), 3);
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(impuesto2), 2).ToString());
                        item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());
                        sumaImpuesto2 += impuesto2;
                        txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto2, 2).ToString();
                        sumaNetos += Neto;
                        txtImporteTotal.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                    }
                    else if (idImpuesto == 1 && TipoRemito != 1)
                    {
                        impuesto1 = Math.Round(((subTotal * 1.21) - subTotal), 2);
                        Neto = Math.Round((importeTotal + impuesto1), 3);
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(impuesto1), 2).ToString());
                        item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());
                        sumaImpuesto1 += impuesto1;
                        txtImpuesto2.Text = "$ " + Math.Round(sumaImpuesto1, 2).ToString();
                        sumaNetos += Neto;
                        txtImporteTotal.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                    }

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPORTE"]), 2).ToString());

                    item.SubItems.Add(dr["OBSERVACIONES"].ToString());
                    item.ImageIndex = 1;
                    item.SubItems.Add(dr["IDDETALLENOTAPEDIDO"].ToString());

                    if (idImpuesto != 2)
                        impuesto2 = 0;
                    if (idImpuesto != 1)
                        impuesto1 = 0;

                    item.SubItems.Add(Convert.ToDecimal(Math.Round(impuesto2, 2)).ToString());
                    item.SubItems.Add(Convert.ToDecimal(Math.Round(impuesto1, 2)).ToString());

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleFacturaVenta.Items.Add(item);

                    Item = (lvwDetalleFacturaVenta.Items.Count - 1);
                    GuardaItemDatoRemito(Item, iCodRemito);
                }

                //////////////////////////////////////////////// ACTUALIZA EL AGREGADO DE DATOS ////////////////////////////////////////////////
                //   if (iExport == false)
                //   {
                connGeneric.DesconectarBDLeeGeneric();
                connGeneric.LeeGeneric("Select Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleFacturaVentas WHERE NROFACTURAINTERNO = " + nroFacturaInterCreado + "", "DetalleFacturasVentas");

                subTotal = Convert.ToDouble(connGeneric.leerGeneric["SubTotal"].ToString());
                impuesto1 = Convert.ToDouble(connGeneric.leerGeneric["Iva105"].ToString());
                impuesto2 = Convert.ToDouble(connGeneric.leerGeneric["Iva21"].ToString());
                importeTotal = Convert.ToDouble(connGeneric.leerGeneric["Importe"].ToString());

                string actualizar = "BASICO=(Cast(replace('" + subTotal + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + impuesto1 + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + impuesto2 + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeTotal + "', ',', '.') as decimal(10,2))), PENDIENTE=(Cast(replace('" + -importeTotal + "', ',', '.') as decimal(10,2)))";

                this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeTotal, 2));
                this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto1, 2));
                this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto2, 2));
                this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)subTotal, 2));

                connGeneric.ActualizaGeneric("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + nroFacturaInterCreado + "");
                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
                // }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    

                //ACTUALIZA EL REMITO COMO FACTURADO
                //timer1.Enabled = false;               

                string actualizarEstadoRemito = "ESTADOREMITO = 'FACTURADO'";
                if (connGeneric.ActualizaGeneric("Remito", actualizarEstadoRemito, "IDEMPRESA = " + IDEMPRESA + " AND NRORemitoInterno = " + iCodRemito + ""))
                {
                    int iUltimaFactu;
                    iUltimaFactu = UltimaFactura();
                    MostrarDatos2(iUltimaFactu);
                    MostrarItemsDatos();
                    //MostrarItemsDatos2(iCodRemito);

                    connGeneric.LeeGeneric("Select * FROM Remito WHERE IDEMPRESA = " + IDEMPRESA + " AND NRORemitoInterno = " + iCodRemito + "", "Remito");
                    MessageBox.Show("La información del remito N° " + connGeneric.leerGeneric["NROREMITO"].ToString() + " ha sido insertada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("No se ha podido actualizar los datos de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch //(System.Exception excep)
            {
                conn.DesconectarBD(); connGeneric.DesconectarBD(); connGeneric.DesconectarBDLeeGeneric();

                //Auditoria
                AuditoriaSistema AS12 = new AuditoriaSistema();
                AS12.SistemaProcesoAuditor_0012("Proc. MostrarDatos()", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////     
            }
        }

        private void GuardaItemDatoRemito(int Item, int iCodRemito)
        {
            try
            {
                //int ItemFactura = 0;

                int iUltimaFactu;
                iUltimaFactu = UltimaFactura();


                if (Item > -1)
                {
                    //int iTotalStock, CantActualArticulo = 0;
                    //////////////////////////GUARDA ITEMS DE DATOS DE REMITO//////////////////////////////////////

                    //  for (ItemFactura = 0; ItemFactura <= lvwDetalleFacturaVenta.Items.Count; ItemFactura++)

                    char[] QuitaSimbolo1 = { '$', ' ' };
                    char[] QuitaSimbolo2 = { '%', ' ' };
                    //string agregarItem = "INSERT INTO DetalleFacturaVentas(IDARTICULO, CANTIDAD, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, NROFACTURAINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleFacturaVenta.Items[Item].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[3].Text + "', ',', '.') as decimal(10,0))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[4].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,3))), '0','0', (Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[5].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[12].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[13].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,2))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[9].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,3))), " + Convert.ToInt32(lvwFacturaVenta.Items[lvwFacturaVenta.Items.Count - 1].SubItems[0].Text) + ")";
                    string agregarItem = "INSERT INTO DetalleFacturaVentas(IDARTICULO, CANTIDAD, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, NROFACTURAINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleFacturaVenta.Items[Item].SubItems[0].Text) + ",(Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[3].Text + "', ',', '.') as decimal(10, 3))),(Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[4].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10, 3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[7].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10, 3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[6].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10, 2))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[5].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10, 3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[13].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10, 3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[14].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10, 3))), (Cast(replace('" + lvwDetalleFacturaVenta.Items[Item].SubItems[10].Text.TrimStart(QuitaSimbolo1) + "', ',', '.') as decimal(10, 2))), " + iUltimaFactu + ")";



                    //nroRemitoInter = Convert.ToInt32(lvwRemito.Items[lvwRemito.Items.Count - 1].SubItems[0].Text);
                    conn.InsertarGeneric(agregarItem);

                    ///ACTUALIZA RELACION REMITO-FACTURA/// 
                    //string actualizaStockArticulo = "NROFACTURAINTERNO = " + Convert.ToInt32(lvwFacturaVenta.Items[lvwFacturaVenta.Items.Count - 1].SubItems[0].Text) + "";
                    string actualizaStockArticulo = "NROFACTURAINTERNO = " + iUltimaFactu + "";
                    connGeneric.ActualizaGeneric("Remito", actualizaStockArticulo, " IDEMPRESA = " + IDEMPRESA + " AND NROREMITOINTERNO = " + iCodRemito + "");

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ////// ACTUALIZA ESTADOS DE EXISTENCIA DE ARTICULOS Y  DETALLE DE LA NOTA DE PEDIDO (Estado Remito y Cantidad a Entregar) ///////
                    ///ACTUALIZA STOCK///               
                    //connGeneric.LeeGeneric("SELECT * FROM Articulo WHERE Codigo = '" + lvwDetalleRemito.Items[Item].SubItems[1].Text.Trim() + "'", "Articulo");
                    //CantActualArticulo = Convert.ToInt32(connGeneric.leerGeneric["CANT_ACTUAL"].ToString());
                    //iTotalStock = CantActualArticulo - Convert.ToInt32(lvwDetalleRemito.Items[Item].SubItems[3].Text);

                    // if (iTotalStock < 0)
                    //     iTotalStock = 0;

                    //string actualizaStockArticulo = "CANT_ACTUAL=(Cast(replace(" + iTotalStock + ", ',', '.') as decimal(10,0)))";
                    //if (connGeneric.ActualizaGeneric("Articulo", actualizaStockArticulo, " IDARTICULO= " + Convert.ToInt32(lvwDetalleRemito.Items[Item].SubItems[0].Text) + ""))
                    // {
                    //     connGeneric.DesconectarBD();
                    //     connGeneric.DesconectarBDLeeGeneric();
                    //}
                    ///COLOCA ENTREGAS A CERO ///     
                }
                else
                    MessageBox.Show("No existen ítems en el remito seleccionado.", "Remito Vacio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            catch //(System.Exception excep)
            {
                //Auditoria
                AuditoriaSistema AS13 = new AuditoriaSistema();
                AS13.SistemaProcesoAuditor_0013("Proc. GuardaItemDatoRemito()", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////            
            }
        }

        private void lvwFacturaVenta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Visualiza Remitos en Factura

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                bool estadoFactu;
                estadoFactu = LeeEstadoFactura(Convert.ToInt32(txtNroInternoFact.Text));
                string EstadoFacturaAnulado;
                EstadoFacturaAnulado = EstadoFacturaAnulada(Convert.ToInt32(this.txtNroInternoFact.Text));

                //if ((this.lvwDetalleFacturaVenta.Items.Count > 0) && (txtCodTipoFactura.Text.Trim() == "1" || txtCodTipoFactura.Text.Trim() == "2" || txtCodTipoFactura.Text.Trim() == "3"))
                //    MessageBox.Show("Error, no se puede realizar cambios el tipo de factura, ya se han cargado items para facturar, elimine los items y vuelta a intentarlo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //else
                // {
                if (EstadoFacturaAnulado == "19")
                    MessageBox.Show("Error factura anulada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    if (estadoFactu == false)
                    {
                        /*if (txtCodTipoFactura.Text.Trim() == "4"  && txtCodFormaPago.Text.Trim()=="1")
                            btnCerrarFactura.Visible = true;
                        else
                            btnCerrarFactura.Visible = false;*/

                        //timer1.Enabled = false;
                        string actualizar = "NROFactura='" + this.txtNroFacturaVenta.Text.Trim() + "', SUCURSAL='" + cboNroSucursal.Text.Trim() + "', IDTIPOFACTURA=" + txtCodTipoFactura.Text.Trim() + ", FECHA='" + dtpFechaFactu.Text.Trim() + "', IDCLIENTE=" + this.txtCodCliente.Text.Trim() + " , IDVENDEDOR=" + this.txtCodVendedor.Text.Trim() + ", IDPERSONAL=" + this.txtCodPersonal.Text.Trim() + ", IDFORMAPAGO=" + this.txtCodFormaPago.Text.Trim() + ", OBSERVACIONES='" + this.txtObservacionFactura.Text.Trim() + "'";
                        if (connGeneric.ActualizaGeneric("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NroFacturaInterno = " + Convert.ToInt32(txtNroInternoFact.Text) + ""))
                        {
                            MostrarDatos2(Convert.ToInt32(txtNroInternoFact.Text));
                            MostrarItemsDatos2(Convert.ToInt32(txtNroInternoFact.Text));
                            MessageBox.Show("La información de la factura ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se ha podido actualizar los datos de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            connGeneric.DesconectarBD();
                        }
                    }
                    else
                        MessageBox.Show("Factura cerrada, no es posible modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //}                 
            }

            catch //(System.Exception excep)
            {
                MessageBox.Show("Error: No se ha podido actualizar la información de la factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connGeneric.DesconectarBD();

                //Auditoria
                AuditoriaSistema AS15 = new AuditoriaSistema();
                AS15.SistemaProcesoAuditor_0015("Evento btnModificar_Click()", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////   
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                bool EstadoFactura;
                EstadoFactura = LeeEstadoFactura(Convert.ToInt32(this.txtNroInternoFact.Text));

                tsBtnNuevo.Enabled = true;

                //btnGuardar.Enabled = false;
                //timer1.Enabled = false;

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                string EstadoFacturaAnulado;
                EstadoFacturaAnulado = EstadoFacturaAnulada(Convert.ToInt32(this.txtNroInternoFact.Text));


                if (EstadoFactura == false)
                {
                    if (fechaFacturaVenta.AddDays(180) <= DateTime.Today || EstadoFacturaAnulado == "ANULADO")
                        MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        if (connGeneric.EliminarGeneric("FacturasVentas", " IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + Convert.ToInt32(this.txtNroInternoFact.Text)))
                        {
                            ActualizaEstadoRemito(Convert.ToInt32(this.txtNroInternoFact.Text));
                            MostrarDatos2(Convert.ToInt32(this.txtNroInternoFact.Text));

                            tsBtnNuevo.Enabled = true;

                            //btnGuardar.Enabled = true;

                            this.txtCantArticulo.Text = "";
                            this.txtProcDesc.Text = "0";
                            this.txtNroInternoFact.Text = "";
                            this.txtNroFacturaVenta.Text = "";
                            this.txtObservacionFactura.Text = "";
                            //this.cboNroSucursal.SelectedIndex = 0;
                            cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();
                            this.txtIva.Text = "";
                            this.txtCodArticulo.Text = "";
                            this.txtCodPersonal.Text = "";
                            this.txtCodCliente.Text = "";
                            this.txtCuit.Text = "";
                            this.txtDescuento.Text = "$ 0.00";
                            this.txtSubTotal.Text = "$ 0.00";
                            this.txtImpuesto1.Text = "$ 0.00";
                            this.txtImpuesto2.Text = "$ 0.00";
                            this.txtImporteTotal.Text = "$ 0.00";
                            MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //timer1.Enabled = true;



                            conPermi();
                            actualizaStock_Anulacion_o_eliminacion(Convert.ToInt32(this.txtNroInternoFact.Text), lvwDetalleFacturaVenta.Items[0].SubItems[1].Text.Trim());
                        }
                        else
                            MessageBox.Show("Error al Eliminar. No se han eliminado los items de factura. Verifique items en el detalle y remitos relacionados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Error al Eliminar. La factura se encuentra cerrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch //(System.Exception excep)  
            {
                MessageBox.Show("Error: Seleccione la factura a eliminar y verifique que no existen items en le detalle y remitos asociados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Auditoria
                AuditoriaSistema AS14 = new AuditoriaSistema();
                AS14.SistemaProcesoAuditor_0014("Evento btnEliminar_Click() ", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////   
            }
        }

        private void ActualizaEstadoRemito(int nrofactuInterna)
        {
            try
            {
                /*  SqlCommand cm = new SqlCommand("SELECT Remito.ESTADOREMITO, remito.NROFACTURAINTERNO FROM Remito WHERE Remito.ESTADOREMITO = 'FACTURADO' ORDER BY NROFACTURAINTERNO", conectaEstado);

                  SqlDataAdapter da = new SqlDataAdapter(cm);
                  DataTable dt = new DataTable();
                  da.Fill(dt);

                  foreach (DataRow dr in dt.Rows)
                  {
                      SqlCommand cm1 = new SqlCommand("SELECT * FROM FacturasVentas WHERE NROFACTURAINTERNO = " + dr["NROFACTURAINTERNO"].ToString() + "", conectaEstado);

                      SqlDataAdapter da1 = new SqlDataAdapter(cm1);
                      DataTable dt1 = new DataTable();
                      da1.Fill(dt1);

                      if (dt1.Rows.Count == 0)
                      {
                          string actualizar = "ESTADOREMITO = 'NO FACTURADO', NROFACTURAINTERNO = NULL";
                          connGeneric.ActualizaGeneric("Remito", actualizar, " NROFACTURAINTERNO = " + dr["NROFACTURAINTERNO"].ToString() + "");
                          //UPDATE Remito SET ESTADOREMITO = 'NO FACTURADO', NROFACTURAINTERNO = NULL WHERE NROFACTURAINTERNO = 4202
                      }
                      cm1.Connection.Close();
                  }
                  cm.Connection.Close();

                  //this.lvwRemito.Items.Clear();
                  MostrarDatos(0,30);*/


                SqlCommand cm = new SqlCommand("SELECT Remito.ESTADOREMITO, remito.NROFACTURAINTERNO FROM Remito WHERE Remito.ESTADOREMITO = 'FACTURADO' AND NroFacturaInterno = " + nrofactuInterna + " ORDER BY NROFACTURAINTERNO", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    if (dt.Rows.Count >= 0)
                    {
                        string actualizar = "ESTADOREMITO = 'NO FACTURADO', NROFACTURAINTERNO = NULL";
                        connGeneric.ActualizaGeneric("Remito", actualizar, " NROFACTURAINTERNO = " + nrofactuInterna + "");
                        //UPDATE Remito SET ESTADOREMITO = 'NO FACTURADO', NROFACTURAINTERNO = NULL WHERE NROFACTURAINTERNO = 4202
                    }
                    cm.Connection.Close();
                }
                cm.Connection.Close();

                //this.lvwRemito.Items.Clear();
                MostrarDatos(0, 30);

            }
            catch { /*MessageBox.Show("Error: No se puede actualizar el estado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);*/ }
        }

        private void btnArt_Click(object sender, EventArgs e)
        {
            frmArticulo formArt = new frmArticulo();
            formArt.pasadoArt1 += new frmArticulo.pasarArticulo1(CodVArt);  //Delegado1 
            formArt.pasadoArt2 += new frmArticulo.pasarArticulo2(NombreArt); //Delegado2
            formArt.pasadoArt3 += new frmArticulo.pasarArticulo3(CostoArt); //Delegado2
            formArt.ShowDialog();

            txtCantArticulo.Focus();
        }

        //Metodos de delegado Tipo Impuesto
        public void CodVArt(string dato1)
        {
            this.txtCodArticulo.Text = dato1.ToString();
        }

        public void NombreArt(string dato2)
        {
            this.cboArticulo.Text = dato2.ToString();
        }

        public void CostoArt(string dato3)
        {
            char[] QuitaSimbolo1 = { '$', ' ' };
            this.txtPrecio.Text = "$ " + Math.Round(CalculoPorcentajeListaVenta(Convert.ToSingle(dato3.ToString().TrimStart(QuitaSimbolo1))), 3);
        }

        private void cboBuscaFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtBuscarFactura.Focus();
            }
        }

        private void txtBuscarFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboNroSucursal.Focus();
            }
        }

        private void cboNroSucursal_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtNroFacturaVenta.Focus();
            }
        }

        private void txtNroFacturaVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                dtpFechaFactu.Focus();
            }
        }

        private void dtpFechaFactu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodCliente.Focus();
            }
        }

        private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboCliente.Focus();
            }
        }

        private void cboCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnCliente.Focus();
            }
        }

        private void btnCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodTipoFactura.Focus();
            }
        }

        private void txtCodTipoFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboTipoFactura.Focus();
            }
        }

        private void cboTipoFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnCodTipoFactura.Focus();
            }
        }

        private void btnCodTipoFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodFormaPago.Focus();
            }
        }

        private void txtCodFormaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboFormaPago.Focus();
            }
        }

        private void cboFormaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnFormaPago.Focus();
            }
        }

        private void btnFormaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodVendedor.Focus();
            }
        }

        private void txtCodVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboVendedor.Focus();
            }
        }

        private void cboVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnVendedor.Focus();
            }
        }

        private void btnVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodPersonal.Focus();
            }
        }

        private void txtCodPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboPersonal.Focus();
            }
        }

        private void cboPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnCodigoPersonal.Focus();
            }
        }

        private void btnCodigoPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtObservacionFactura.Focus();
            }
        }

        private void txtObservacionFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodArticulo.Focus();
            }
        }

        private void txtCodArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboArticulo.Focus();
            }
            txtProcDesc.Text = "0";
        }

        private void cboArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnArt.Focus();
            }
        }

        private void btnArt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodArticulo.Focus();
            }
        }

        private void txtCantArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnAgregaArt.Focus();
            }
        }

        private void btnAgregaArt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodArticulo.Focus();
            }
        }


        private void tsBtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboBuscaFactura.SelectedIndex == 0)
                {
                    BuscarDatos1();
                }

                else if (cboBuscaFactura.SelectedIndex == 1)
                {
                    BuscarDatos2();
                }

                else if (cboBuscaFactura.SelectedIndex == 2)
                {
                    BuscarDatos3();
                }

                //Tipo Factura
                else if (cboBuscaFactura.SelectedIndex == 3)
                {
                    BuscarDatos4();
                }

                else if (cboBuscaFactura.SelectedIndex == 4)
                {
                    BuscarDatos5();
                }

            }
            catch { }
        }

        public void BuscarDatos1()
        {
            try
            {
                lvwFacturaVenta.Items.Clear();
                lvwDetalleFacturaVenta.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT TipoFactura.Descripcion as 'Tipo', Facturasventas.*, Cliente.*, EstadoSistema.*  FROM TipoFactura, Facturasventas, Cliente, EstadoSistema WHERE TipoFactura.IDTipoFactura=FacturasVentas.IDTipoFactura AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND FacturasVentas.SUCURSAL = '" + sPtoVta + "' AND EstadoSistema.IdEstado=FacturasVentas.IDESTADO AND FacturasVentas.IDCLIENTE = Cliente.IDCLIENTE AND Facturasventas.NROFACTURA LIKE '%" + txtBuscarFactura.Text + "%'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwFacturaVenta.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NROFACTURAINTERNO"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["NROFACTURA"].ToString());
                    item.SubItems.Add(dr["Fecha"].ToString());
                    item.SubItems.Add(dr["Tipo"].ToString());
                    item.SubItems.Add(dr["IdCliente"].ToString());
                    item.SubItems.Add(dr["RazonSocial"].ToString());

                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Basico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Descuentos"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto1"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto2"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());
                    item.SubItems.Add(dr["Descripcion"].ToString());
                    item.SubItems.Add(dr["IdEstado"].ToString());

                    item.SubItems.Add(dr["NRONCINTERNO"].ToString());
                    item.SubItems.Add(dr["NRONC"].ToString());

                    if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(180) <= DateTime.Today)
                        item.ImageIndex = 2;
                    else if (dr["IDESTADO"].ToString() == "19")
                        item.ImageIndex = 5;
                    else
                        item.ImageIndex = 0;

                    item.UseItemStyleForSubItems = false;
                    lvwFacturaVenta.Items.Add(item);
                }
                cm.Connection.Close();

            }
            catch { }
        }

        public void BuscarDatos2()
        {
            try
            {
                lvwFacturaVenta.Items.Clear();
                lvwDetalleFacturaVenta.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT TipoFactura.Descripcion as 'Tipo', Facturasventas.*, Cliente.*, EstadoSistema.* FROM TipoFactura, Facturasventas, Cliente, EstadoSistema WHERE TipoFactura.IDTipoFactura=FacturasVentas.IDTipoFactura AND Cliente.IDEMPRESA = " + IDEMPRESA + "  AND FacturasVentas.SUCURSAL = '" + sPtoVta + "' AND EstadoSistema.IdEstado=FacturasVentas.IDESTADO AND Facturasventas.IDCLIENTE = Cliente.IDCLIENTE AND Facturasventas.fecha >= '" + txtBuscarFactura.Text + "'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwFacturaVenta.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NROFACTURAINTERNO"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["NROFACTURA"].ToString());
                    item.SubItems.Add(dr["Fecha"].ToString());
                    item.SubItems.Add(dr["Tipo"].ToString());
                    item.SubItems.Add(dr["IdCliente"].ToString());
                    item.SubItems.Add(dr["RazonSocial"].ToString());

                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Basico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Descuentos"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto1"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto2"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());
                    item.SubItems.Add(dr["Descripcion"].ToString());
                    item.SubItems.Add(dr["IdEstado"].ToString());

                    item.SubItems.Add(dr["NRONCINTERNO"].ToString());
                    item.SubItems.Add(dr["NRONC"].ToString());

                    if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(180) <= DateTime.Today)
                        item.ImageIndex = 2;
                    else if (dr["IDESTADO"].ToString() == "19")
                        item.ImageIndex = 5;
                    else
                        item.ImageIndex = 0;

                    item.UseItemStyleForSubItems = false;
                    lvwFacturaVenta.Items.Add(item);

                }
                cm.Connection.Close();

            }
            catch { MessageBox.Show("Error: El formato de fecha correcto es DD/MM/AAAA.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void BuscarDatos3()
        {
            try
            {
                lvwFacturaVenta.Items.Clear();
                lvwDetalleFacturaVenta.Items.Clear();
                if (cboBuscaFactura.SelectedIndex == 2 && txtBuscarFactura.Text != "")
                {
                    SqlCommand cm = new SqlCommand("SELECT TipoFactura.Descripcion as 'Tipo', Facturasventas.*, Cliente.*, EstadoSistema.* FROM TipoFactura, Facturasventas, Cliente, EstadoSistema WHERE TipoFactura.IDTipoFactura=FacturasVentas.IDTipoFactura AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND Facturasventas.IDCLIENTE = Cliente.IDCLIENTE AND FacturasVentas.SUCURSAL = '" + sPtoVta + "' AND EstadoSistema.IdEstado=FacturasVentas.IdEstado AND Cliente.RAZONSOCIAL LIKE '%" + txtBuscarFactura.Text + "%'", conectaEstado);

                    //SELECT IdCliente, RAZONSOCIAL, LOCALIDAD, TELEFONOSCOMERCIALES, NUMDECUIT FROM Cliente WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND RAZONSOCIAL LIKE '%" + txtBuscarRemito.Text + "%' ORDER BY RAZONSOCIAL", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwFacturaVenta.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["NROFACTURAINTERNO"].ToString());
                        item.SubItems.Add(dr["Sucursal"].ToString());
                        item.SubItems.Add(dr["NROFACTURA"].ToString());
                        item.SubItems.Add(dr["Fecha"].ToString());
                        item.SubItems.Add(dr["Tipo"].ToString());
                        item.SubItems.Add(dr["IdCliente"].ToString());
                        item.SubItems.Add(dr["RazonSocial"].ToString());

                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Basico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                        item.SubItems.Add(dr["Descuentos"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto1"]), 2).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto2"]), 2).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                        item.SubItems.Add(dr["Observaciones"].ToString());
                        item.SubItems.Add(dr["Descripcion"].ToString());
                        item.SubItems.Add(dr["IdEstado"].ToString());

                        item.SubItems.Add(dr["NRONCINTERNO"].ToString());
                        item.SubItems.Add(dr["NRONC"].ToString());

                        if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(180) <= DateTime.Today)
                            item.ImageIndex = 2;
                        else if (dr["IDESTADO"].ToString() == "19")
                            item.ImageIndex = 5;
                        else
                            item.ImageIndex = 0;

                        item.UseItemStyleForSubItems = false;
                        lvwFacturaVenta.Items.Add(item);

                    }
                    cm.Connection.Close();
                }
            }
            catch { }
        }

        public void BuscarDatos4()
        {
            try
            {
                lvwFacturaVenta.Items.Clear();
                lvwDetalleFacturaVenta.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT TipoFactura.Descripcion as 'Tipo', Facturasventas.*, Cliente.*, EstadoSistema.* FROM TipoFactura, Facturasventas, Cliente, EstadoSistema WHERE TipoFactura.IDTipoFactura=FacturasVentas.IDTipoFactura AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND FacturasVentas.SUCURSAL = '" + sPtoVta + "' AND EstadoSistema.IdEstado=FacturasVentas.IdEstado AND Facturasventas.IDCLIENTE = Cliente.IDCLIENTE AND TipoFactura.Descripcion = '" + txtBuscarFactura.Text + "'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwFacturaVenta.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NROFACTURAINTERNO"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["NROFACTURA"].ToString());
                    item.SubItems.Add(dr["Fecha"].ToString());
                    item.SubItems.Add(dr["Tipo"].ToString());
                    item.SubItems.Add(dr["IdCliente"].ToString());
                    item.SubItems.Add(dr["RazonSocial"].ToString());

                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Basico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Descuentos"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto1"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto2"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());
                    item.SubItems.Add(dr["Descripcion"].ToString());
                    item.SubItems.Add(dr["IdEstado"].ToString());

                    item.SubItems.Add(dr["NRONCINTERNO"].ToString());
                    item.SubItems.Add(dr["NRONC"].ToString());

                    if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(180) <= DateTime.Today)
                        item.ImageIndex = 2;
                    else if (dr["IDESTADO"].ToString() == "19")
                        item.ImageIndex = 5;
                    else
                        item.ImageIndex = 0;

                    item.UseItemStyleForSubItems = false;
                    lvwFacturaVenta.Items.Add(item);

                }
                cm.Connection.Close();

            }
            catch { }
        }

        public void BuscarDatos5()
        {
            try
            {
                lvwFacturaVenta.Items.Clear();
                lvwDetalleFacturaVenta.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT TipoFactura.Descripcion as 'Tipo', Facturasventas.*, Cliente.*, EstadoSistema.* FROM TipoFactura, Facturasventas, Cliente, EstadoSistema WHERE TipoFactura.IDTipoFactura=FacturasVentas.IDTipoFactura AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND FacturasVentas.SUCURSAL = '" + sPtoVta + "' AND EstadoSistema.IdEstado=FacturasVentas.IdEstado AND Facturasventas.IDCLIENTE = Cliente.IDCLIENTE AND Cliente.IdCliente = '" + txtBuscarFactura.Text + "'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwFacturaVenta.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NROFACTURAINTERNO"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["NROFACTURA"].ToString());
                    item.SubItems.Add(dr["Fecha"].ToString());
                    item.SubItems.Add(dr["Tipo"].ToString());
                    item.SubItems.Add(dr["IdCliente"].ToString());
                    item.SubItems.Add(dr["RazonSocial"].ToString());

                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Basico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Descuentos"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto1"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto2"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());
                    item.SubItems.Add(dr["Descripcion"].ToString());
                    item.SubItems.Add(dr["IdEstado"].ToString());

                    item.SubItems.Add(dr["NRONCINTERNO"].ToString());
                    item.SubItems.Add(dr["NRONC"].ToString());

                    if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(180) <= DateTime.Today)
                        item.ImageIndex = 2;
                    else if (dr["IDESTADO"].ToString() == "19")
                        item.ImageIndex = 5;
                    else
                        item.ImageIndex = 0;

                    item.UseItemStyleForSubItems = false;
                    lvwFacturaVenta.Items.Add(item);

                }
                cm.Connection.Close();

            }
            catch { }
        }

        /////////////////////////////////////////////////////FIN BLOQUE DE BUSQUEDA/////////////////////////////////////////////////////////////////

        private void txtNroFacturaVenta_TextChanged(object sender, EventArgs e)
        {
            if (cboFormaPago.Text.Trim() == "PAGO MOSTRADOR")
            {
                //this.txtNroFacturaVenta.Text = lvwFacturaVenta.SelectedItems[0].SubItems[2].Text.Trim();
                //txtNroFacturaVenta.ReadOnly = true;
            }
        }

        private void txtNroFacturaVenta_Leave(object sender, EventArgs e)
        {
            /*if (ValidaNumerador(txtNroFacturaVenta.Text.Trim()) == true)
            {
                MessageBox.Show("Error de Numerador. Ya existe el numero ingresado, el numero ha sido corregido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NuevaFactura();
            }*/
        }

        private void tsBtnReporte_Click(object sender, EventArgs e)
        {
            try
            {/*

                if (lvwFacturaVenta.Items.Count == 0)
                    MessageBox.Show("Error: No existen datos para mostrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    dtpFechaHasta.Value = DateTime.Now;
                    //gpReportesFactura.Visible = true;
                    //rptReporteFacturacion.Visible = true;
                    //rptFacturacionImpresion_A.Visible = false;
                    //rptFacturacionImpresion_B.Visible = false;

                    // DGestion.Reportes.frmRPTFacturacion frmRptFactu = new DGestion.Reportes.frmRPTFacturacion();
                    // frmRptFactu.pasarNroCompro += new DGestion.Reportes.frmRPTFacturacion.pasarNroComprobante1(nroComprob);
                    // frmRptFactu.ShowDialog();

                    //this.facturaVentaClienteTableAdapter.Fill(this.dGestionDTGeneral.FacturaVentaCliente); //Factura Cliente
                    //this.rptReporteFacturacion.RefreshReport();

                    listaFactu = true;
                    listaFactuVentaCliente = false;
                    nroComprob = lvwFacturaVenta.SelectedItems[0].SubItems[2].Text;
                    nroComprobInt = Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text);

                    DGestion.Reportes.frmRPTFacturacion frmRptFactu = new DGestion.Reportes.frmRPTFacturacion();
                    frmRptFactu.ShowDialog();
                }*/

                DGestion.Reportes.frmRTFactuConsulta frmRptConsultaFactu = new DGestion.Reportes.frmRTFactuConsulta();
                frmRptConsultaFactu.ShowDialog();
            }
            catch (System.Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                MessageBox.Show("Error: No se ha seleccionado el comprobante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsImprimirFactura_Click(object sender, EventArgs e)
        {
            try
            {
                listaFactu = false;
                listaFactuVentaCliente = false;

                if (lvwFacturaVenta.Items.Count == 0)
                    MessageBox.Show("Error: No se ha filtrado datos para mostrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {

                    if (iTipoFactura == 2)
                    {
                        /*gpReportesFactura.Visible = true;
                        rptFacturacionImpresion_A.Visible = true;
                        rptReporteFacturacion.Visible = false;
                        rptFacturacionImpresion_A.Visible = true;
                        rptFacturacionImpresion_B.Visible = false;*/

                        //NumeroFactura = lvwFacturaVenta.SelectedItems[0].SubItems[1].Text;
                        //facturacionClienteTableAdapter.Fill(this.dGestionDTGeneral.FacturacionCliente, NumeroFactura); ///Factura Impresa
                        //this.rptFacturacionImpresion_A.RefreshReport();

                        tipoFactu = 2;
                        nroComprobInt = Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text);
                        nroComprob = lvwFacturaVenta.SelectedItems[0].SubItems[2].Text;
                        DGestion.Reportes.frmRPTFacturacion frmRptFactu = new DGestion.Reportes.frmRPTFacturacion();

                        EstadoFactura(nroComprobInt);
                        frmRptFactu.ShowDialog();
                    }
                    else if (iTipoFactura == 1)
                    {
                        /*gpReportesFactura.Visible = true;
                        rptFacturacionImpresion_A.Visible = true;
                        rptReporteFacturacion.Visible = false;
                        rptFacturacionImpresion_A.Visible = false;
                        rptFacturacionImpresion_B.Visible = true;*/

                        //NumeroFactura = lvwFacturaVenta.SelectedItems[0].SubItems[1].Text;
                        //facturacionClienteTableAdapter.Fill(this.dGestionDTGeneral.FacturacionCliente, NumeroFactura); ///Factura Impresa
                        //this.rptFacturacionImpresion_B.RefreshReport();

                        tipoFactu = 1;
                        nroComprobInt = Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text);
                        nroComprob = lvwFacturaVenta.SelectedItems[0].SubItems[2].Text;
                        DGestion.Reportes.frmRPTFacturacion frmRptFactu = new DGestion.Reportes.frmRPTFacturacion();

                        EstadoFactura(nroComprobInt);
                        frmRptFactu.ShowDialog();
                    }
                    else if (iTipoFactura == 3)
                    {
                        tipoFactu = 3;
                        nroComprobInt = Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text);
                        nroComprob = lvwFacturaVenta.SelectedItems[0].SubItems[2].Text;
                        DGestion.Reportes.frmRPTFacturacion frmRptFactu = new DGestion.Reportes.frmRPTFacturacion();

                        EstadoFactura(nroComprobInt);
                        frmRptFactu.ShowDialog();
                    }

                    else if (iTipoFactura == 4)
                    {
                        if (chkImpresa.CheckState == CheckState.Checked)
                        {
                            tipoFactu = 4;
                            nroComprobInt = Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text);
                            nroComprob = lvwFacturaVenta.SelectedItems[0].SubItems[2].Text;
                            DGestion.Reportes.frmRPTFacturacion frmRptFactu = new DGestion.Reportes.frmRPTFacturacion();
                            frmRptFactu.ShowDialog();
                        }
                        else if (chkImpresa.CheckState == CheckState.Unchecked)
                        {
                            if (this.txtCodFormaPago.Text.Trim() == "1" && cboFormaPago.Text.Trim() == "PAGO MOSTRADOR")
                            {
                                char[] QuitaSimbolo = { '$', ' ' };
                                string NuevoRecibo = "";
                                int nroReciboInt = 0;
                                int ContaNroReciboNuevo = 0;

                                nroComprobInt = Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text);
                                nroComprob = lvwFacturaVenta.SelectedItems[0].SubItems[2].Text;
                                //Busca el ultimo nro Recibo

                                if (this.lvwDetalleFacturaVenta.Items.Count > 0)
                                {
                                    /*conn.LeeGeneric("SELECT MAX(NRORECIBO) as 'NRO' FROM Recibos WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = '" + sPtoVta.Trim() + "' ORDER BY NRO", "Recibos");
                                    if (conn.leerGeneric["NRO"].ToString() == "")
                                        NuevoRecibo = "0";
                                    else
                                        NuevoRecibo = conn.leerGeneric["NRO"].ToString();

                                    ContaNroReciboNuevo = (Convert.ToInt32(NuevoRecibo));
                                    ContaNroReciboNuevo = ContaNroReciboNuevo + 1;

                                    NuevoRecibo = ContaNroReciboNuevo.ToString();*/

                                    conn.DesconectarBD();
                                    conn.DesconectarBDLeeGeneric();
                                    //////////////////////////////////////////////////////

                                    //dTotalFactu = Math.Ceiling(Convert.ToDecimal(txtImporteTotal.Text.TrimStart(QuitaSimbolo)));
                                    //dSubtotalFactu = Math.Ceiling(Convert.ToDecimal(txtSubTotal.Text.TrimStart(QuitaSimbolo)));
                                    //txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dTotalFactu, 2));
                                    //txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dSubtotalFactu, 2));

                                    //Genera recibo automaticamente
                                    string agregar = "INSERT INTO Recibos(NRORECIBO, FECHA, SUCURSAL, IDCLIENTE, IDPERSONAL, IDVENDEDOR, TOTAL, OBSERVACIONES, IDIDENTIFICADOR, NROFACTURAINTERNO, IMPORTERESTANTE, IDESTADO, IDEMPRESA) VALUES ('" + nroComprob + "', '" + FormateoFecha() + "', '" + sPtoVta + "', " + txtCodCliente.Text.Trim() + ", " + txtCodPersonal.Text.Trim() + ", " + txtCodVendedor.Text.Trim() + ", (Cast(replace('" + txtImporteTotal.Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))), 'Recibo Generado Automáticamente', '2', " + lvwFacturaVenta.SelectedItems[0].SubItems[0].Text + ", '0', 18, " + IDEMPRESA + ")";
                                    if (conn.InsertarGeneric(agregar))
                                    {
                                    }
                                    //////////////////////////////////////

                                    //ULTIMO NUMERO RECIBO INTERNO CREADO
                                    conn.LeeGeneric("SELECT NRORECIBOINTERNO as 'NROINTERNO' FROM Recibos WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = '" + sPtoVta.Trim() + "' AND NRORecibo = '" + nroComprob + "' ORDER BY NROINTERNO", "Recibos");
                                    if (conn.leerGeneric["NROINTERNO"].ToString() == "")
                                        NuevoRecibo = "0";
                                    else
                                        nroReciboInt = Convert.ToInt32(conn.leerGeneric["NROINTERNO"].ToString());

                                    conn.DesconectarBD();
                                    conn.DesconectarBDLeeGeneric();
                                    ////////////////////////////////////////

                                    string agregarItem = "INSERT INTO DetalleRecibos(IDTIPOPAGO, IDSUBTIPODEPAGO, NUMERO, IMPORTE, NRORECIBOINTERNO) VALUES ('2', '3', '0', (Cast(replace('" + txtImporteTotal.Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))), " + nroReciboInt + ")";
                                    if (conn.InsertarGeneric(agregarItem))
                                    { }

                                    //Actualiza Estado Factura
                                    string actualizar = "Observaciones='Pago Contado',NRORECIBOINTERNO=" + nroReciboInt + ", IdEstado='12'";
                                    if (connGeneric.ActualizaGeneric("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND Sucursal = '" + sPtoVta + "' AND NroFacturaInterno = " + Convert.ToInt32(txtNroInternoFact.Text) + ""))
                                    {
                                        MostrarDatos2(Convert.ToInt32(txtNroInternoFact.Text));
                                        MostrarItemsDatos2(Convert.ToInt32(txtNroInternoFact.Text));
                                        MessageBox.Show("La factura se cerro correctamente, recibo generado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se ha podido actualizar los datos de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        connGeneric.DesconectarBD();
                                    }
                                    //////////////////////////////////

                                    //Actualiza factu haciendo un redondeo hacia arriba
                                    //string actualizarFactuRedondeo = "BASICO=(Cast(replace('" + dSubtotalFactu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + dTotalFactu + "', ',', '.') as decimal(10,2)))";
                                    //connGeneric.ActualizaGeneric("FacturasVentas", actualizarFactuRedondeo, " IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL= '" + sPtoVta.Trim() + "' AND NROFACTURA = '" + txtNroFacturaVenta.Text.Trim() + "'");

                                    //string actualizarDetalleFactuRedondeo = "IMPORTE=(Cast(replace('" + dTotalFactu + "', ',', '.') as decimal(10,2)))";
                                    //connGeneric.ActualizaGeneric("DetalleFacturaVentas", actualizarDetalleFactuRedondeo, " IDDETALLEFACTURAVENTAS = " + lvwDetalleFacturaVenta.Items[0].SubItems[10].Text + "");

                                    ////////////////////

                                    //btnCerrarFactura.Visible = false;                                    
                                    EstadoFactura(nroComprobInt);

                                    chkImpresa.CheckState = CheckState.Checked;

                                    tipoFactu = 4;
                                    //nroComprobInt = Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text);
                                    //nroComprob = lvwFacturaVenta.SelectedItems[0].SubItems[2].Text;
                                    DGestion.Reportes.frmRPTFacturacion frmRptFactu = new DGestion.Reportes.frmRPTFacturacion();
                                    frmRptFactu.ShowDialog();

                                    GuardaDatosTesoreria(nroComprobInt, nroReciboInt, nroComprob);
                                }
                            }
                            //Si es X y no es un movimiento de mostrador
                            else if (this.txtCodFormaPago.Text.Trim() != "1" && cboFormaPago.Text.Trim() != "PAGO MOSTRADOR")
                            {
                                nroComprobInt = Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text);
                                nroComprob = lvwFacturaVenta.SelectedItems[0].SubItems[2].Text;
                                //Busca el ultimo nro Recibo

                                if (this.lvwDetalleFacturaVenta.Items.Count > 0)
                                {
                                    EstadoFactura(nroComprobInt);
                                    chkImpresa.CheckState = CheckState.Checked;

                                    tipoFactu = 4;
                                    nroComprobInt = Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text);
                                    nroComprob = lvwFacturaVenta.SelectedItems[0].SubItems[2].Text;
                                    DGestion.Reportes.frmRPTFacturacion frmRptFactu = new DGestion.Reportes.frmRPTFacturacion();
                                    frmRptFactu.ShowDialog();
                                }
                                ///////////////////////////////////////////////////////////////
                            }
                        }

                    }
                    else
                    {

                        if (iTipoFactura == 5)
                        {
                            DialogResult dialogResultN1 = MessageBox.Show("El comprobante es un presupuesto y no una factura. ¿La operación es correcta?", "Presupuesto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dialogResultN1 == DialogResult.Yes)
                            {
                                tipoFactu = 5;
                                nroComprobInt = Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text);
                                nroComprob = lvwFacturaVenta.SelectedItems[0].SubItems[2].Text;
                                DGestion.Reportes.frmRPTFacturacion frmRptFactu = new DGestion.Reportes.frmRPTFacturacion();

                                //EstadoFactura(nroComprobInt);
                                frmRptFactu.ShowDialog();

                            }
                            else
                                MessageBox.Show("Indicar correctamente el tipo de operación para continuar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void EstadoFactura(int nroFactuInt)
        {
            try
            {
                string actualizar = "IMPRESA = 'True'";
                connGeneric.ActualizaGeneric("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND Sucursal = '" + sPtoVta + "' AND NroFacturaInterno = " + nroFactuInt + "");
                chkImpresa.CheckState = CheckState.Checked;
            }
            catch { }
        }

        private bool LeeEstadoFactura(int NroFactura)
        {
            try
            {
                bool EstadoFacturaImpresion = false;
                conn.LeeGeneric("SELECT IMPRESA FROM FacturasVentas WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = '" + sPtoVta.Trim() + "' AND NroFacturaInterno = " + NroFactura + "", "FacturasVentas");

                EstadoFacturaImpresion = Convert.ToBoolean(conn.leerGeneric["IMPRESA"].ToString());

                return EstadoFacturaImpresion;
            }
            catch { return false; }
        }

        private string EstadoFacturaAnulada(int NroFacturaInt)
        {
            try
            {
                string EstadoFacturaAnulado;
                conn.LeeGeneric("SELECT IDESTADO FROM FacturasVentas WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = '" + sPtoVta.Trim() + "' AND NroFacturaInterno = " + NroFacturaInt + "", "FacturasVentas");

                EstadoFacturaAnulado = conn.leerGeneric["IDESTADO"].ToString();

                return EstadoFacturaAnulado;
            }
            catch { return "Error"; }
        }

        private void btCerrar_Click(object sender, EventArgs e)
        {
            //gpReportesFactura.Visible = false;            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                ListView.CheckedListViewItemCollection checkedItems = lvwRemitoPendiente.CheckedItems;
                foreach (ListViewItem item in checkedItems)
                {
                    //GuardaFacturaSegunRemitoSeleccionado(Convert.ToInt32(lvwRemitoPendiente.SelectedItems[0].SubItems[0].Text), Convert.ToInt32(lvwRemitoPendiente.SelectedItems[0].SubItems[3].Text));                        

                    iExportRemitoFactura = checkedItems.Count;
                    if (iExportRemitoFactura >= 0)
                    {
                        connGeneric.LeeGeneric("SELECT * FROM Remito WHERE IDEMPRESA = " + IDEMPRESA + " AND NROREMITOINTERNO = " + Convert.ToInt32(item.SubItems[0].Text) + " AND SUCURSAL = '" + sPtoVta + "'", "Remito");
                        iTipoRemito = Convert.ToInt32(connGeneric.leerGeneric["IdTipoRemito"].ToString());

                        GuardaFacturaSegunRemitoSeleccionado(Convert.ToInt32(item.SubItems[0].Text), Convert.ToInt32(item.SubItems[3].Text), bExportFactuVenta, iTipoRemito);
                        bExportFactuVenta = true;
                        iExportRemitoFactura = iExportRemitoFactura - 1;

                        if (iExportRemitoFactura == 0)
                            bExportFactuVenta = false;
                    }
                    else
                        bExportFactuVenta = false;
                }

                gpbRemitoPendiente.Visible = false;
                gpbDetalleRemito.Visible = false;
                gpFactura.Enabled = true;
                gpDetalleFactura.Enabled = true;
                gpoFacturacion.Enabled = true;
                gpCompraProveedor.Enabled = true;

                bExportFactuVenta = false;

                flagControlUnificacionComprobante = true;
                VerificaItemsAgregados();


                MostrarItemsDatos2(idNROFACTUINTERNO);

            }
            catch //(System.Exception excep) 
            {
                //Auditoria
                AuditoriaSistema AS11 = new AuditoriaSistema();
                AS11.SistemaProcesoAuditor_0011("Proc. lvwRemitoPendiente_MouseDoubleClick()", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////
            }
        }

        private void btnAceptarDetalle_Click(object sender, EventArgs e)
        {
            gpbDetalleRemito.Visible = false;
            //gpbRemitoPendiente.Visible = true;
        }

        private void lvwRemitoPendiente_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///LEE DETALLE REMITOS//
            try
            {
                lvwDetalleRemito.Items.Clear();
                gpbDetalleRemito.Visible = true;
                //gpbRemitoPendiente.Visible = false;

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                double iva105;
                double iva21;

                //int iCantPedida, iCantActual, iCantRestante;
                //int CantPendiente;


                connGeneric.LeeGeneric("SELECT Remito.NROREMITOINTERNO, DetalleRemito.IDDETALLEREMITO as 'Código', Articulo.DESCRIPCION as 'Artículo', Articulo.CANT_ACTUAL, DetalleRemito.CANTPENDIENTE, DetalleRemito.CANTIDAD, DetalleRemito.PRECUNITARIO as 'Precio Unitario', DetalleRemito.IMPORTE as 'Importe', DetalleRemito.DESCUENTO as 'Descuento', DetalleRemito.PORCDESC as '% Desc', DetalleRemito.SUBTOTAL as 'Subtotal', DetalleRemito.IMPUESTO1 as 'Iva 10,5', DetalleRemito.IMPUESTO2 as 'Iva 21', DetalleRemito.OBSERVACIONES as 'Observaciones' FROM Remito, DetalleRemito, Articulo, Cliente, Personal WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND DetalleRemito.IDARTICULO = Articulo.IDARTICULO AND Remito.IDCLIENTE = Cliente.IDCLIENTE AND Remito.IDPERSONAL = Personal.IDPERSONAL AND DetalleRemito.NROREMITOINTERNO = Remito.NROREMITOINTERNO AND Remito.NROREMITOINTERNO = " + Convert.ToInt32(lvwRemitoPendiente.SelectedItems[0].SubItems[0].Text) + "", "Remito");

                iva105 = Convert.ToDouble(this.connGeneric.leerGeneric["Iva 10,5"].ToString());
                iva21 = Convert.ToDouble(this.connGeneric.leerGeneric["Iva 21"].ToString());

                SqlCommand cm = new SqlCommand("SELECT Articulo.Codigo, Remito.NROREMITOINTERNO, DetalleRemito.NROREMITOINTERNO, DetalleRemito.IDDETALLEREMITO, DetalleRemito.IDArticulo, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL, DetalleRemito.CANTPENDIENTE, DetalleRemito.CANTIDAD, DetalleRemito.PRECUNITARIO, DetalleRemito.IMPORTE, DetalleRemito.DESCUENTO, DetalleRemito.PORCDESC, DetalleRemito.SUBTOTAL, DetalleRemito.IMPUESTO1 as 'Iva 10,5', DetalleRemito.IMPUESTO2 as 'Iva 21', DetalleRemito.OBSERVACIONES FROM Remito, DetalleRemito, Articulo, Cliente, Personal WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND DetalleRemito.IDARTICULO = Articulo.IDARTICULO AND Remito.IDCLIENTE = Cliente.IDCLIENTE AND Remito.IDPERSONAL = Personal.IDPERSONAL AND DetalleRemito.NROREMITOINTERNO = Remito.NROREMITOINTERNO AND Remito.NROREMITOINTERNO = " + Convert.ToInt32(lvwRemitoPendiente.SelectedItems[0].SubItems[0].Text) + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwDetalleRemito.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDArticulo"].ToString());
                    item.SubItems.Add(dr["Codigo"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());

                    item.SubItems.Add(dr["CANTIDAD"].ToString());

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["PRECUNITARIO"]), 3).ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Subtotal"]), 2).ToString());
                    item.SubItems.Add(dr["DESCUENTO"].ToString());

                    if (dr["Iva 10,5"].ToString() != "0,0000")
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 2).ToString());
                    else
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 21"]), 2).ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPORTE"]), 2).ToString());
                    item.SubItems.Add(dr["OBSERVACIONES"].ToString());

                    item.ImageIndex = 1;

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleRemito.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void txtProcDesc_Leave(object sender, EventArgs e)
        {
        }

        private void txtProcDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == ".")
            {
                e.Handled = true;
                this.txtProcDesc.Text += ",";
                SendKeys.Send("{END}");
            }
        }

        private void tsBtnFacturaVentaCliente_Click(object sender, EventArgs e)
        {
            grbFechaBuscar.Visible = true;
        }

        private void btnCancelaFechaBuscar_Click(object sender, EventArgs e)
        {
            grbFechaBuscar.Visible = false;
        }

        private void btnAceptarFechas_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwFacturaVenta.Items.Count == 0)
                    MessageBox.Show("Error: No existen datos para mostrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    //gpReportesFactura.Visible = true;
                    //rptReporteFacturacion.Visible = true;
                    //rptFacturacionImpresion_A.Visible = false;
                    //rptFacturacionImpresion_B.Visible = false;

                    // DGestion.Reportes.frmRPTFacturacion frmRptFactu = new DGestion.Reportes.frmRPTFacturacion();
                    // frmRptFactu.pasarNroCompro += new DGestion.Reportes.frmRPTFacturacion.pasarNroComprobante1(nroComprob);
                    // frmRptFactu.ShowDialog();

                    //this.facturaVentaClienteTableAdapter.Fill(this.dGestionDTGeneral.FacturaVentaCliente); //Factura Cliente
                    //this.rptReporteFacturacion.RefreshReport();


                    listaFactuVentaCliente = true;
                    FechaDesde = dtpFechaDesde.Text;
                    FechaHasta = dtpFechaHasta.Text;

                    // nroComprob = lvwFacturaVenta.SelectedItems[0].SubItems[1].Text;
                    // nroComprobInt = Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text);

                    DGestion.Reportes.frmRPTFacturacion frmRptFactu = new DGestion.Reportes.frmRPTFacturacion();
                    frmRptFactu.ShowDialog();
                }
            }
            catch (System.Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                MessageBox.Show("Error: No se ha seleccionado el comprobante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCodCliente_Leave(object sender, EventArgs e)
        {
            //Detecta Numerador Facturación
            if (txtCodTipoFactura.Text == "1")
                txtNroFacturaVenta.Text = Convert.ToString(NumeradorFactura(1));

            else if (txtCodTipoFactura.Text == "2")
                txtNroFacturaVenta.Text = Convert.ToString(NumeradorFactura(2));

            else if (txtCodTipoFactura.Text == "3")
                txtNroFacturaVenta.Text = Convert.ToString(NumeradorFactura(2));

            else if (txtCodTipoFactura.Text == "4")
                txtNroFacturaVenta.Text = Convert.ToString(NumeradorFactura(4));

            else
                txtNroFacturaVenta.Text = Convert.ToString(NumeradorFactura(5));
        }

        private void txtBuscarFactura_TextChanged(object sender, EventArgs e)
        {


            /*  string sCodArt;
              char[] QuitaSimbolo = { '$', ' ' };
              char[] QuitaSimbolo2 = { '*', ' ' };

              sCodArt = txtBuscarFactura.Text.TrimStart(QuitaSimbolo2);
              sCodArt = sCodArt.TrimEnd(QuitaSimbolo2);
              this.txtBuscarFactura.Text = sCodArt;*/


        }

        private void chkBoxPresupuesto_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkBoxPresupuesto.CheckState == CheckState.Checked)
                {
                    txtCodTipoFactura.Text = "5";
                }
                else
                {
                    //txtCodTipoFactura.Text = "";
                    //cboTipoFactura.Text = "";

                    if (sPtoVta == "0001" && IDEMPRESA == 1 && cboCliente.Text.Trim() == "CONSUMIDOR FINAL" && txtCodTipoFactura.Text.Trim() == "5")
                    {
                        txtCodVendedor.Text = "1029";
                        txtCodTipoFactura.Text = "4";
                        txtNroFacturaVenta.Text = NumeradorFactura(4).ToString();
                    }

                }
            }
            catch { }
        }

        private void tsBtnPresupuesto_Click(object sender, EventArgs e)
        {

            NuevaFactura();

            chkBoxPresupuesto.CheckState = CheckState.Checked;

            if (chkBoxPresupuesto.CheckState == CheckState.Checked)
            {
                txtCodTipoFactura.Text = "5";
            }
            else
            {
                txtCodTipoFactura.Text = "";
                cboTipoFactura.Text = "";

            }
        }

        private void tsBtnAvPag_Click(object sender, EventArgs e)
        {
            int iAnterior;

            iAnterior = PagAv;
            PagAv = PagAv + 30;

            PagRe = iAnterior;
            MostrarDatos(PagRe, PagAv);
        }

        private void btnCerrarFactura_Click(object sender, EventArgs e)
        {

        }

        private void tlsBarArticulo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnCerrarRemitoFactura_Click(object sender, EventArgs e)
        {
            gboRemitoFactura.Visible = false;
        }

        private void tsRemitosFactura_Click(object sender, EventArgs e)
        {

            if (lvwFacturaVenta.SelectedItems.Count == 1)
            {
                gboRemitoFactura.Visible = true;
                txtNroFacturaBusqueda.Text = NroFact.Text;
                lvwRemitoFactura.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT remito.NROREMITOINTERNO, Remito.NROREMITO, Remito.SUCURSAL, remito.FECHA FROM FacturasVentas, Remito WHERE FacturasVentas.NROFACTURAINTERNO = Remito.NROFACTURAINTERNO AND FacturasVentas.SUCURSAL=" + sPtoVta.Trim() + " AND  FacturasVentas.IDEMPRESA=" + IDEMPRESA + " AND FacturasVentas.NROFACTURA = " + Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[2].Text) + " ORDER BY Remito.NROFACTURAINTERNO", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwRemitoFactura.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NROREMITOINTERNO"].ToString());
                    item.SubItems.Add(dr["SUCURSAL"].ToString());
                    item.SubItems.Add(dr["NROREMITO"].ToString());
                    item.SubItems.Add(dr["FECHA"].ToString());

                    item.ImageIndex = 1;

                    item.UseItemStyleForSubItems = false;
                    lvwRemitoFactura.Items.Add(item);
                }
                cm.Connection.Close();

            }
        }

        private void tsBtnEstadoImpresion_Click(object sender, EventArgs e)
        {
            gpbActivarImpresion.Visible = true;
        }

        private void btnAbrirFactu_Click(object sender, EventArgs e)
        {
            try
            {
                string actualizar = "IMPRESA = 'false'";
                connGeneric.ActualizaGeneric("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND Sucursal = '" + sPtoVta + "' AND NroFacturaInterno = " + Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text) + "");
                chkImpresa.CheckState = CheckState.Unchecked;
            }
            catch { }
        }

        private void btnCerrarFactu_Click(object sender, EventArgs e)
        {
            try
            {
                string actualizar = "IMPRESA = 'true'";
                connGeneric.ActualizaGeneric("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND Sucursal = '" + sPtoVta + "' AND NroFacturaInterno = " + Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text) + "");
                chkImpresa.CheckState = CheckState.Checked;
            }
            catch { }
        }

        private void btnCancelarACFactu_Click(object sender, EventArgs e)
        {
            gpbActivarImpresion.Visible = false;
        }

        private void btnAnularComprobante_Click(object sender, EventArgs e)
        {
            try
            {
                int iEstadoFactura;

                connGeneric.LeeGeneric("SELECT FacturasVentas.IDESTADO FROM FacturasVentas WHERE FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND FacturasVentas.NROFACTURAINTERNO = " + Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text) + "", "FacturasVentas");
                iEstadoFactura = Convert.ToInt16(this.connGeneric.leerGeneric["IDEstado"].ToString());

                if (iEstadoFactura != 19)
                {
                    if (lvwFacturaVenta.SelectedItems.Count == 1)
                    {
                        string actualizar = "IDESTADO = 19";
                        connGeneric.ActualizaGeneric("FacturasVentas", actualizar, " NROFACTURAINTERNO = " + Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text) + "");
                    }

                    actualizaStock_Anulacion_o_eliminacion(Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text), lvwDetalleFacturaVenta.Items[0].SubItems[1].Text.Trim());

                    MostrarDatos2(Convert.ToInt32(lvwFacturaVenta.SelectedItems[0].SubItems[0].Text));
                }
                else
                    MessageBox.Show("El movimiento ya se encuentra anulado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch
            {
                //MessageBox.Show("Error: No se puede actualizar el estado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtnRePag_Click(object sender, EventArgs e)
        {
            if (PagRe != 0 && PagRe != 1)
            {
                int iPosterior;

                iPosterior = PagRe;
                PagRe = PagRe - 30;

                PagAv = iPosterior;

                MostrarDatos(PagRe, PagAv);
            }
            else
            {
                PagRe = 0;
                PagAv = 30;
                MostrarDatos(PagRe, PagAv);
            }
        }

        private void actualizaStock_Anulacion_o_eliminacion(int nFacturaInt, string CodArticulo)
        {
            try
            {
                double dCantidad_articulo_facturaVenta;
                double dCantidad_Articulo_Actual;
                double iIdArticulo = 0;


                //  if (lvwFacturaVenta.SelectedItems.Count == 1)
                //   {
                SqlCommand cm = new SqlCommand("SELECT FacturasVentas.NROFACTURAINTERNO, DetalleFacturaVentas.IDDETALLEFACTURAVENTAS, Articulo.IDARTICULO, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL as 'AntidadArticuloDisponible', DetalleFacturaVentas.CANTIDAD as 'CantidadArtFacturado' FROM FacturasVentas, DetalleFacturaVentas, Articulo WHERE FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND DetalleFacturaVentas.IDARTICULO = Articulo.IDARTICULO AND FacturasVentas.NROFACTURAINTERNO = DetalleFacturaVentas.NROFACTURAINTERNO AND Articulo.Codigo = '" + CodArticulo + "' AND FacturasVentas.NROFACTURAINTERNO = " + nFacturaInt + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    dCantidad_Articulo_Actual = Convert.ToDouble(dr["AntidadArticuloDisponible"].ToString());
                    dCantidad_articulo_facturaVenta = Convert.ToDouble(dr["CantidadArtFacturado"].ToString());
                    iIdArticulo = Convert.ToDouble(dr["IDARTICULO"].ToString());

                    dCantidad_Articulo_Actual = dCantidad_Articulo_Actual + dCantidad_articulo_facturaVenta;

                    //Actualiza la Cantidad de Stock
                    string actualizaStock = "CANT_ACTUAL=(Cast(replace(" + dCantidad_Articulo_Actual + ", ',', '.') as decimal(10,0)))";
                    if (connGeneric.ActualizaGeneric("Articulo", actualizaStock, " IDARTICULO= " + iIdArticulo + ""))
                    {
                        connGeneric.DesconectarBD();
                        connGeneric.DesconectarBDLeeGeneric();
                    }
                    ///////////////////////////////////////////////////////////

                    dCantidad_articulo_facturaVenta = 0;
                    dCantidad_Articulo_Actual = 0;
                }
                cm.Connection.Close();
            }


            //   }
            catch { }
        }


    }
}