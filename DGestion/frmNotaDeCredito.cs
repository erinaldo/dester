﻿using System;
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

namespace DGestion
{
    public partial class frmNotaDeCredito : Form
    {
        CGenericBD connGeneric = new CGenericBD();
        CGenericBD conn = new CGenericBD();
        ArticulosBD connArt = new ArticulosBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");
        EmpresaBD connEmpresa = new EmpresaBD();

        public static int tipoNC;
        public static int nroNotaCredito;
        public static int nroNCInt;
        public static int tipoFactu;

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
        DateTime fechaNotaCredito;

        int contadorNROfactNuevo;

        bool nuevaNC = false;
        int idNRONCINTERNO;

        int indiceLvwVenta;
        int iCodigoListaPrecioCliente;
        bool nuevoRemito = false;
        int indiceLvwNotaPedido;
        int idArtuculo;

        double porcGeneralLista = 0;
        double procenFleteLista = 0;
        double CostoEnLista = 0;

        double ValorUnitarioArticulo;

        int IDEMPRESA;
        string sPtoVta;

        int iTipoNC;

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

        public frmNotaDeCredito()
        {
            InitializeComponent();
        }

        private string FormateoFecha()
        {
            DateTimePicker dtr = new DateTimePicker();
            dtr.Value = DateTime.Now;
            return String.Format("{0:d/M/yyyy HH:mm:ss}", dtr.Value);
        }

        private double CalculoPorcentajeNotaCredito(double valorArticulo)
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
                    valorMasArticuloPorcFlete = (valorArticulo * procenFleteLista) / 100;
                    valorTotalArticulo = valorMasArticuloPorcFlete + valorArticulo;
                }

                if (porcGeneralLista < 1)
                    valorTotalArticulo = valorArticulo;
                else
                {
                    valorMasArticuloPorcVenta = (valorTotalArticulo * porcGeneralLista) / 100;
                    valorTotalArticulo = valorMasArticuloPorcVenta + valorTotalArticulo;
                }

                return valorTotalArticulo;
            }
            catch { return 0; }
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
                    valorMasArticuloPorcFlete = Math.Round(((valorArticulo * procenFleteLista) / 100),3);
                    valorTotalArticulo = Math.Round((valorMasArticuloPorcFlete + valorArticulo),3);
                }

                if (porcGeneralLista < 1)
                    valorTotalArticulo = Math.Round(valorArticulo,3);
                else
                {
                    valorMasArticuloPorcVenta = Math.Round(((valorTotalArticulo * porcGeneralLista) / 100),3);
                    valorTotalArticulo = Math.Round((valorMasArticuloPorcVenta + valorTotalArticulo),3);
                }                                                

                return Math.Round(valorTotalArticulo,3);                

            }
            catch { return 0; }
        }

        private void GuardaItemsDatos(bool status, int nroNCredInter)
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

                if (txtCantidadArticulo.Text.Trim() != "")
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
                    Cantidad = Convert.ToInt32(txtCantidadArticulo.Text.Trim());
                    CantActualArticulo = Convert.ToInt32(conn.leerGeneric["CANT_ACTUAL"].ToString());
                    IdImpuesto = Convert.ToInt32(conn.leerGeneric["IDIMPUESTO"].ToString());

                    Cant_Pedida = Convert.ToInt32(txtCantidadArticulo.Text.Trim());
                    //Cant_Restante = Convert.ToInt32(txtCantRestante.Text.Trim());

                    SubTotal = Math.Round((Cant_Pedida * CalculoPorcentajeListaVenta(CostoEnLista)), 3);
                    Importe = Math.Round((CalculoPorcentajeListaVenta(CostoEnLista) * Cant_Pedida), 3);
                    ////////////////////////////////////// //////////////////////////////////////


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

                    /* lvwDetalleNotaDeCredito.SmallImageList = imageList1;
                     ListViewItem item = new ListViewItem(IdArticulo.ToString());
                     item.SubItems.Add(Codigo.ToString());                                                           ///ITEM 0
                     item.SubItems.Add(Descripcion.ToString());                                                      ///ITEM 1
                     item.SubItems.Add(Cant_Pedida.ToString());
                     item.SubItems.Add("$ " + Math.Round(CalculoPorcentajeListaVenta(CostoEnLista), 3).ToString());  ///ITEM 2

                     item.SubItems.Add("$ " + Math.Round(Importe, 2).ToString());                                    ///ITEM 3
                     item.SubItems.Add("0");*/

                    lvwDetalleNotaDeCredito.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(IdArticulo.ToString());
                    item.SubItems.Add(Codigo.ToString());                                                           ///ITEM 0
                    item.SubItems.Add(Descripcion.ToString());                                                      ///ITEM 1
                    item.SubItems.Add(Cant_Pedida.ToString());                                                      ///ITEM 2  
                    item.SubItems.Add("$ " + Math.Round(CalculoPorcentajeListaVenta(CostoEnLista), 3).ToString());  ///ITEM 3

                    //item.SubItems.Add("% " + dPorcDescuento.ToString());
                    dTotalDescuento = (SubTotal - SubTotal2);

                    item.SubItems.Add("$ " + Math.Round(Importe, 2).ToString());                                    ///ITEM 4  
                    item.SubItems.Add("$ " + Math.Round(dTotalDescuento, 2).ToString());

                    if (txtIva.Text == "Exento" || txtIva.Text == "exento")
                    {
                        Impuesto1 = Math.Round(((SubTotal * 1) - SubTotal), 2);
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

                        if (IdImpuesto == 2 && cboTipoNC.Text.Trim() != "N")
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
                                Impuesto1 = Math.Round(((SubTotal * 1.105) - SubTotal), 2);
                                Neto = Math.Round((Importe + Impuesto1), 3);
                                item.SubItems.Add("$ " + Math.Round(Impuesto1, 2).ToString());                              ///ITEM 5A
                                item.SubItems.Add("$ " + Math.Round(Neto, 2).ToString());                                   ///ITEM 6B

                                sumaImpuesto1 += Impuesto1;
                                txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto1, 2).ToString();

                                sumaNetos += Neto;
                                txtImporteTotal.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                            }
                        }

                        else if (IdImpuesto == 1 && cboTipoNC.Text.Trim() != "N")
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
                            }
                            else
                            {

                                Impuesto2 = Math.Round(((SubTotal * 1.21) - SubTotal), 2);
                                Neto = Math.Round((Importe + Impuesto2), 3);
                                item.SubItems.Add("$ " + Math.Round(Impuesto2, 2).ToString());                              ///ITEM 5B
                                item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());                                   ///ITEM 6B    

                                sumaImpuesto2 += Impuesto2;
                                txtImpuesto2.Text = "$ " + Math.Round(sumaImpuesto2, 2).ToString();

                                sumaNetos += Neto;
                                txtImporteTotal.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                            }
                        }

                        else if (IdImpuesto == 3 || cboTipoNC.Text.Trim() == "N")
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
                            else
                            {
                                Impuesto1 = Math.Round(((SubTotal * 1) - SubTotal), 2);
                                Neto = Math.Round((Importe + Impuesto1), 3);
                                item.SubItems.Add("$ " + Math.Round(Impuesto1, 2).ToString());                              ///ITEM 5A
                                item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());                                   ///ITEM 6B

                                sumaImpuesto1 += Impuesto1;
                                txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto1, 2).ToString();

                                sumaNetos += Neto;
                                txtImporteTotal.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                            }
                        }
                    }

                    sumaTotales += SubTotal;
                    txtSubTotal.Text = "$ " + Math.Round(sumaTotales, 3).ToString();

                    item.SubItems.Add(CantActualArticulo.ToString());                                               ///ITEM 7
                    item.SubItems.Add("0");     //Observaciones                                                     ///ITEM 8

                    //item.SubItems.Add("-");
                    //item.SubItems.Add("0");  //colocar el IDNotaPedido                                              ///ITEM 9

                    if (IdImpuesto != 2)
                        Impuesto1 = 0;
                    if (IdImpuesto != 1)
                        Impuesto2 = 0;

                    item.SubItems.Add(Convert.ToDecimal(Math.Round(Impuesto1, 2)).ToString());                     ///ITEM 10
                    item.SubItems.Add(Convert.ToDecimal(Math.Round(Impuesto2, 2)).ToString());                    ///ITEM 11

                    /*Cant_Restante = CantActualArticulo - Cant_Pedida;
                    if (Cant_Restante < 0)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;*/

                    item.ImageIndex = 2;

                    lvwDetalleNotaDeCredito.Items.Add(item);

                    //Normalizacion de Saldos totales
                    if (lvwDetalleNotaDeCredito.Items.Count != 0)
                    {
                        dSubTotal = 0.000;
                        dImpuesto1 = 0.000;
                        dImpuesto2 = 0.000;
                        dImporteTotal = 0.00;

                        char[] QuitaSimbolo = { '$', ' ' };
                        char[] QuitaSimbolo2 = { '%', ' ' };
                        for (int i = 0; i < (lvwDetalleNotaDeCredito.Items.Count); i++)
                        {
                            dImporteTotal += Math.Round(Convert.ToSingle(lvwDetalleNotaDeCredito.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo)), 2);
                            dImpuesto1 += Math.Round(Convert.ToSingle(lvwDetalleNotaDeCredito.Items[i].SubItems[11].Text.TrimStart(QuitaSimbolo)), 2);
                            dImpuesto2 += Math.Round(Convert.ToSingle(lvwDetalleNotaDeCredito.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo)), 2);
                            dSubTotal += Math.Round(Convert.ToSingle(lvwDetalleNotaDeCredito.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo)), 3);
                        }
                        this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImporteTotal, 2));
                        this.txtImpuesto1.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)dImpuesto1, 2));
                        this.txtImpuesto2.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)dImpuesto2, 2));
                        this.txtSubTotal.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)dSubTotal, 3));
                    }
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    ////////////////////////////////////////////////// CARGA ITEMS DE FACTURA ///////////////////////////////////////////////////////
                    double subTotalfactu;
                    double iva105Factu;
                    double iva21Factu;
                    double importeFactu;

                    if (status == false)
                    {
                        if (fechaNotaCredito.AddDays(180) <= DateTime.Today)
                            MessageBox.Show("No se puede modificar una NC de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            txtNroInternoNC.Text = idNRONCINTERNO.ToString();
                            idNRONCINTERNO = nroNCredInter;

                            connGeneric.EliminarGeneric("DetalleNotaCredito", " NRONOTAINTERNO = " + nroNCredInter);
                            char[] QuitaSimbolo = { '$', ' ' };

                            for (int i = 0; i < (lvwDetalleNotaDeCredito.Items.Count); i++)
                            {
                                string agregarItem = "INSERT INTO DetalleNotaCredito(IDARTICULO, CANTIDAD, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, NRONOTAINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleNotaDeCredito.Items[i].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[6].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))),'0', (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[11].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))), " + nroNCredInter + ")";

                                if (conn.InsertarGeneric(agregarItem))
                                {
                                    connGeneric.DesconectarBD();
                                    connGeneric.DesconectarBDLeeGeneric();
                                }
                            }
                            //MostrarDatos();
                            MostrarItemsDatos2(nroNCredInter);
                        }
                    }

                    else if (status == true)
                    {
                        char[] QuitaSimbolo = { '$', ' ' };
                        char[] QuitaSimbolo2 = { '%', ' ' };
                        for (int i = 0; i < (lvwDetalleNotaDeCredito.Items.Count); i++)
                        {
                            string agregarItem = "INSERT INTO DetalleNotaCredito(IDARTICULO, CANTIDAD, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, NRONOTAINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleNotaDeCredito.Items[i].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,0))), (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[6].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))),'0', (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[11].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaDeCredito.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))), " + Convert.ToInt32(lvwNotaDeCredito.Items[lvwNotaDeCredito.Items.Count - 1].SubItems[0].Text) + ")";

                            nroNCredInter = Convert.ToInt32(lvwNotaDeCredito.Items[lvwNotaDeCredito.Items.Count - 1].SubItems[0].Text);

                            if (conn.InsertarGeneric(agregarItem))
                            {
                                connGeneric.DesconectarBD();
                                connGeneric.DesconectarBDLeeGeneric();
                            }
                        }
                        //MessageBox.Show("Item Actualizado/Agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);                           
                    }

                    //////////////////////////////////////////////// ACTUALIZA EL AGREGADO DE DATOS ////////////////////////////////////////////////
                    connGeneric.DesconectarBDLeeGeneric();
                    connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleNotaCredito WHERE NRONOTAINTERNO = " + nroNCredInter + "", "DetalleNotaCredito");

                    subTotalfactu = Convert.ToSingle(connGeneric.leerGeneric["SubTotal"].ToString());
                    iva105Factu = Convert.ToSingle(connGeneric.leerGeneric["Iva105"].ToString());
                    iva21Factu = Convert.ToSingle(connGeneric.leerGeneric["Iva21"].ToString());
                    importeFactu = Convert.ToSingle(connGeneric.leerGeneric["Importe"].ToString());

                    string actualizar = "BASICO=(Cast(replace('" + subTotalfactu + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + iva105Factu + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + iva21Factu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeFactu + "', ',', '.') as decimal(10,2)))";

                    this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeFactu, 2));
                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)iva105Factu, 2));
                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)iva21Factu, 2));
                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)subTotalfactu, 2));

                    if (connGeneric.ActualizaGeneric("NotaCredito", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NRONOTAINTERNO = " + nroNCredInter + ""))
                    {
                        MostrarDatos();
                        MostrarItemsDatos2(nroNCredInter);
                        // MessageBox.Show("La información de la factura ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se ha podido actualizar los datos de la NC.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else
                    MessageBox.Show("Error al Agregar Artículo: No se ha ingresado artículo o cantidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { MessageBox.Show("Error: Falta algun tipo de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void MostrarItemsDatos2(int NRONCInterno)
        {
            try
            {
                lvwDetalleNotaDeCredito.Items.Clear();

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                double iva105;
                double iva21;
                //int iCantPedida, iCantActual, iCantRestante;
                //int CantPendiente;

                connGeneric.LeeGeneric("SELECT NotaCredito.NRONOTAINTERNO, DetalleNotaCredito.IDDETALLENOTACREDITO, Articulo.DESCRIPCION as 'Artículo', Articulo.CANT_ACTUAL, DetalleNotaCredito.CANTIDAD, DetalleNotaCredito.PRECUNITARIO as 'Precio Unitario', DetalleNotaCredito.IMPORTE as 'Importe', DetalleNotaCredito.DESCUENTO as 'Descuento', DetalleNotaCredito.PORCDESC as '% Desc', DetalleNotaCredito.SUBTOTAL as 'Subtotal', DetalleNotaCredito.IMPUESTO1 as 'Iva 10,5', DetalleNotaCredito.IMPUESTO2 as 'Iva 21', DetalleNotaCredito.OBSERVACIONES as 'Observaciones' FROM NotaCredito, DetalleNotaCredito, Articulo, Cliente, Personal WHERE NotaCredito.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND DetalleNotaCredito.IDARTICULO = Articulo.IDARTICULO AND NotaCredito.IDCLIENTE = Cliente.IDCLIENTE AND NotaCredito.IDPERSONAL = Personal.IDPERSONAL AND DetalleNotaCredito.NRONOTAINTERNO = NotaCredito.NRONOTAINTERNO AND NotaCredito.NRONOTAINTERNO = " + NRONCInterno + "", "NotaCredito");

                iva105 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 10,5"].ToString());
                iva21 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 21"].ToString());

                SqlCommand cm = new SqlCommand("SELECT Articulo.Codigo, NotaCredito.NRONOTAINTERNO, DetalleNotaCredito.IDDETALLENOTACREDITO, DetalleNotaCredito.NRONOTAINTERNO, DetalleNotaCredito.IDArticulo, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL, DetalleNotaCredito.CANTIDAD, DetalleNotaCredito.PRECUNITARIO, DetalleNotaCredito.IMPORTE, DetalleNotaCredito.DESCUENTO, DetalleNotaCredito.PORCDESC, DetalleNotaCredito.SUBTOTAL, DetalleNotaCredito.IMPUESTO1 as 'Iva 10,5', DetalleNotaCredito.IMPUESTO2 as 'Iva 21', DetalleNotaCredito.OBSERVACIONES FROM NotaCredito, DetalleNotaCredito, Articulo, Cliente, Personal WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND DetalleNotaCredito.IDARTICULO = Articulo.IDARTICULO AND NotaCredito.IDCLIENTE = Cliente.IDCLIENTE AND NotaCredito.IDPERSONAL = Personal.IDPERSONAL AND DetalleNotaCredito.NRONOTAINTERNO = NotaCredito.NRONOTAINTERNO AND NotaCredito.NRONOTAINTERNO = " + NRONCInterno + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwDetalleNotaDeCredito.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDArticulo"].ToString());
                    item.SubItems.Add(dr["Codigo"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());

                    item.SubItems.Add(dr["CANTIDAD"].ToString());

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["PRECUNITARIO"]), 3).ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Subtotal"]), 3).ToString());
                    item.SubItems.Add("$ " + dr["DESCUENTO"].ToString());

                    if (dr["Iva 10,5"].ToString() != "0,0000")
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 2).ToString());
                    else
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 21"]), 2).ToString());

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPORTE"]), 2).ToString());
                    //   item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.Empty, Color.LightGray, null);
                    //   item.SubItems.Add(dr["CANTIDADRESTANTE"].ToString(), Color.Empty, Color.LightGray, null);

                    item.SubItems.Add(dr["OBSERVACIONES"].ToString());
                    item.SubItems.Add(dr["IDDETALLENOTACREDITO"].ToString());
                    item.SubItems.Add(dr["Iva 10,5"].ToString());
                    item.SubItems.Add(dr["Iva 21"].ToString());


                    item.ImageIndex = 2;


                    item.UseItemStyleForSubItems = false;
                    lvwDetalleNotaDeCredito.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void frmNotaDeCredito_Load(object sender, EventArgs e)
        {
            gpoNotaDeCredito.Visible = false;
            gpNC.Width = 1005;
            lvwNotaDeCredito.Width = 984;
            lvwDetalleNotaDeCredito.Height = 267;

            lblCodArt.Visible = false;
            txtCodArticulo.Visible = false;
            cboArticulo.Visible = false;
            btnAgregaArt.Visible = false;
            btnQuitaArt.Visible = false;
            lblCantidad.Visible = false;
            txtCantidadArticulo.Visible = false;
            btnArt.Visible = false;


            lblPrecio.Visible = false;
            txtPrecio.Visible = false;
            lblDescuento.Visible = false;
            txtProcDesc.Visible = false;

            dtpFechaFactu.Value = DateTime.Today;
            //fechaFacturaCompra = DateTime.Today;

            conn.ConectarBD();

            FormatoListView();
            IDEMPRESA = ConsultaEmpresa(); //Lee Empresa
            MostrarDatos();

            cboBuscaNotaCredito.SelectedIndex = 0;
            //cboNroSucursal.SelectedIndex = 0;
            cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();
        }

        private void FormatoListView()
        {
            lvwNotaDeCredito.View = View.Details;
            lvwNotaDeCredito.LabelEdit = true;
            lvwNotaDeCredito.AllowColumnReorder = true;
            lvwNotaDeCredito.FullRowSelect = true;
            lvwNotaDeCredito.GridLines = true;

            lvwDetalleNotaDeCredito.View = View.Details;
            lvwDetalleNotaDeCredito.LabelEdit = true;
            lvwDetalleNotaDeCredito.AllowColumnReorder = true;
            lvwDetalleNotaDeCredito.FullRowSelect = true;
            lvwDetalleNotaDeCredito.GridLines = true;
        }

        private void MostrarDatos()
        {
            try
            {
                lvwNotaDeCredito.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT NRONOTAINTERNO as 'Código', NRONOTACRED as 'Nro NC', Fecha, Sucursal, Basico as 'Básico', DESCUENTOS as 'Desc', IMPUESTO1 as 'Iva 10,5', IMPUESTO2 as 'Iva 21', TOTAL as 'Total', NotaCredito.OBSERVACIONES as 'Observaciones', NotaCredito.IDCLIENTE, NotaCredito.IdEstado, Cliente.RAZONSOCIAL FROM NotaCredito, Cliente WHERE NotaCredito.IDEMPRESA = " + IDEMPRESA + " AND NotaCredito.IDCLIENTE = Cliente.IDCLIENTE AND NotaCredito.SUCURSAL = '" + sPtoVta + "' ORDER BY NRONOTAINTERNO", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwNotaDeCredito.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Código"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["Nro NC"].ToString());
                    item.SubItems.Add(dr["Fecha"].ToString());

                    item.SubItems.Add(dr["IDCLIENTE"].ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString());

                    /*item.SubItems.Add("$ " + dr["Básico"].ToString());
                    item.SubItems.Add(dr["Desc"].ToString());
                    item.SubItems.Add("$ " + dr["Iva 10,5"].ToString());
                    item.SubItems.Add("$ " + dr["Iva 21"].ToString());
                    item.SubItems.Add("$ " + dr["Total"].ToString());
                    item.SubItems.Add(dr["Observaciones"].ToString());*/

                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Básico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Desc"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Iva 21"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());

                    if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(180) <= DateTime.Today)
                        item.ImageIndex = 5;

                    else
                    {
                        if (dr["IdEstado"].ToString() == "14")
                            item.ImageIndex = 5; //No Pagado
                        else
                            item.ImageIndex = 4; //Pagado
                    }
                                       

                    item.UseItemStyleForSubItems = false;
                    lvwNotaDeCredito.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            NuevaNC();
        }

        private void NuevaNC()
        {
            try
            {
                nuevaNC = true;
                //timer1.Enabled = true;

                dSubTotal = 0.00;
                dImpuesto1 = 0.00;
                dImpuesto2 = 0.00;
                dImporteTotal = 0.00;

                dtpFechaFactu.Value = DateTime.Today;
                fechaNotaCredito = DateTime.Today;

                lvwDetalleNotaDeCredito.Items.Clear();
                gpoNotaDeCredito.Visible = true;
                gpDetalleNotaDeCredito.Visible = true;
                gpNC.Height = 280;
                gpNC.Width = 267;
                lvwNotaDeCredito.Height = 255;
                lvwNotaDeCredito.Width = 246;
                lvwDetalleNotaDeCredito.Height = 244;

                this.cboBuscaNotaCredito.SelectedIndex = 0;

                lblCodArt.Visible = true;
                txtCodArticulo.Visible = true;
                cboArticulo.Visible = true;
                btnAgregaArt.Visible = true;
                btnQuitaArt.Visible = true;
                lblCantidad.Visible = true;
                txtCantidadArticulo.Visible = true;
                btnArt.Visible = true;

                btnEliminar.Enabled = false;
                tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;
                btnModificar.Enabled = false;
                //btnGuardar.Enabled = true;
                //btnGuardar.Visible = true;

                lblPrecio.Visible = true;
                txtPrecio.Visible = true;
                lblDescuento.Visible = true;
                txtProcDesc.Visible = true;

                this.txtCantidadArticulo.Text = "";
                this.txtNroInternoNC.Text = "0";
                this.txtNroNC.Text = "0";
                this.txtObservacionNC.Text = "";
                this.txtProcDesc.Text = "0";
                //this.cboNroSucursal.SelectedIndex = 0;
                cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();
                this.txtIva.Text = "";
                this.txtCodArticulo.Text = "";
                this.txtCodPersonal.Text = "";
                this.txtCodCliente.Text = "";
                this.txtCodVendedor.Text = "";
                this.txtCodFormaPago.Text = "";
                this.txtCodTipoNC.Text = "";
                this.txtCuit.Text = "";
                this.txtDescuento.Text = "$ 0.000";
                this.txtSubTotal.Text = "$ 0.000";
                this.txtImpuesto1.Text = "$ 0.000";
                this.txtImpuesto2.Text = "$ 0.000";
                this.txtImporteTotal.Text = "$ 0.00";

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                /////////////////////////////////////////// AUTONUMERICO NRO INTERNO //////////////////////////////////////////
                connGeneric.LeeGeneric("SELECT MAX(NRONOTACRED) as 'NRO' FROM NotaCredito WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = " + sPtoVta + " ORDER BY NRO", "NotaCredito");

                if (connGeneric.leerGeneric["NRO"].ToString() == "")
                {
                    txtNroInternoNC.Text = "0";
                    txtNroNC.Text = "0";
                }
                else
                {
                    //txtNroInternoNC.Text = connGeneric.leerGeneric["NRO"].ToString();
                    txtNroNC.Text = connGeneric.leerGeneric["NRO"].ToString();
                }

                contadorNROfactNuevo = (Convert.ToInt32(txtNroNC.Text.Trim()));
                contadorNROfactNuevo = contadorNROfactNuevo + 1;
                //txtNroInternoNC.Text = contadorNROfactNuevo.ToString();
                txtNroNC.Text= contadorNROfactNuevo.ToString();

                //txtNroIntRemito.Text = this.txtNroIntRemito.Text;
                //this.txtNroNC.Text = this.cboNroSucursal.Text + "-" + this.txtNroInternoNC.Text;
                //this.txtNroNC.Text = this.txtNroInternoNC.Text;

                ValidaNumerador(txtNroNC.Text.Trim());

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
            }
            catch { MessageBox.Show("Error: El nro ingresado no es numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private bool ValidaNumerador(string nrocomprobante)
        {
            try
            {
                SqlCommand cm = new SqlCommand("SELECT NRONOTACRED FROM NotaCredito", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    if (nrocomprobante == dr["NRONOTACRED"].ToString().Trim())
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            gpoNotaDeCredito.Visible = false;
            gpNC.Width = 1005;
            lvwNotaDeCredito.Width = 984;
            lvwDetalleNotaDeCredito.Height = 267;

            lblCodArt.Visible = false;
            txtCodArticulo.Visible = false;
            cboArticulo.Visible = false;
            btnAgregaArt.Visible = false;
            btnQuitaArt.Visible = false;
            lblCantidad.Visible = false;
            txtCantidadArticulo.Visible = false;
            btnArt.Visible = false;

            lblPrecio.Visible = false;
            txtPrecio.Visible = false;
            lblDescuento.Visible = false;
            txtProcDesc.Visible = false;

            tsBtnNuevo.Enabled = true;
            tsBtnModificar.Enabled = true;
            //btnEliminar.Enabled = false;
            //btnModificar.Enabled = true;
            //btnGuardar.Enabled = true;
            //btnGuardar.Visible = false;
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsBtnModificar_Click(object sender, EventArgs e)
        {
            nuevaNC = false;
            //timer1.Enabled = false;

            if (lvwNotaDeCredito.SelectedItems.Count == 0)
                MessageBox.Show("Error: No se ha seleccionado ninguna NC", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                gpoNotaDeCredito.Visible = true;
                gpDetalleNotaDeCredito.Visible = true;

                gpNC.Height = 280;
                gpNC.Width = 267;
                lvwNotaDeCredito.Height = 255;
                lvwNotaDeCredito.Width = 246;
                lvwDetalleNotaDeCredito.Height = 244;


                this.cboBuscaNotaCredito.SelectedIndex = 0;
                //cboNroSucursal.SelectedIndex = 0;
                cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();

                lblCodArt.Visible = true;
                txtCodArticulo.Visible = true;
                cboArticulo.Visible = true;
                btnAgregaArt.Visible = true;
                btnQuitaArt.Visible = true;

                lblCantidad.Visible = true;
                txtCantidadArticulo.Visible = true;
                btnArt.Visible = true;
                lblDescuento.Visible = true;
                lblPrecio.Visible = true;
                txtPrecio.Visible = true;
                txtProcDesc.Visible = true;

                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;
            }
        }

        private void btnAgregaArt_Click(object sender, EventArgs e)
        {
            if (fechaNotaCredito.AddDays(180) <= DateTime.Today)
                MessageBox.Show("No se puede modificar una NC de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (lvwDetalleNotaDeCredito.Items.Count >= 25)
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
                    txtProcDesc.Text = "0";

                    txtCodArticulo.Focus();
                }
            }
        }

        private void GuardarTodosLosDatos()
        {
            try
            {
                float subTotal;
                float impuesto1; float impuesto2;
                float descuento; float importeTotal;

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

                if (nuevaNC == true)
                    connGeneric.ConsultaGeneric("Select * FROM NotaCredito WHERE IDEMPRESA = " + IDEMPRESA + " AND NRONOTAINTERNO = " + Convert.ToInt32(txtNroInternoNC.Text) + "", "NotaCredito");
                else
                    connGeneric.ConsultaGeneric("Select * FROM NotaCredito WHERE IDEMPRESA = " + IDEMPRESA + " AND NRONOTAINTERNO = " + idNRONCINTERNO + "", "NotaCredito");
                if (connGeneric.ds.Tables[0].Rows.Count == 0)
                {
                    string agregar = "INSERT INTO NotaCredito(NRONOTACRED, IDTIPONOTA, FECHA, SUCURSAL, IDCLIENTE, IDVENDEDOR, IDPERSONAL, IDFORMADEPAGO, BASICO, PORCDESC, DESCUENTOS, IMPUESTO1, IMPUESTO2, TOTAL, OBSERVACIONES, IDEMPRESA) VALUES ('" + txtNroNC.Text.Trim() + "', " + txtCodTipoNC.Text.Trim() + ", '" + FormateoFecha() + "', '" + sPtoVta + "', " + txtCodCliente.Text.Trim() + ", " + txtCodVendedor.Text.Trim() + ", " + txtCodPersonal.Text.Trim() + ", " + txtCodFormaPago.Text.Trim() + ", (Cast(replace('" + Math.Round(subTotal, 3) + "', ',', '.') as decimal(10,3))), " + 0 + ", " + descuento + " , (Cast(replace('" + Math.Round(impuesto1, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(impuesto2, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(importeTotal, 2) + "', ',', '.') as decimal(10,2))), '" + txtObservacionNC.Text + "', " + IDEMPRESA + ")";

                    this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeTotal, 2));
                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)impuesto1, 2));
                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)impuesto2, 2));
                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)subTotal, 3));

                    if (conn.InsertarGeneric(agregar))
                    {
                        MostrarDatos();
                        GuardaItemsDatos(true, 0);
                        lvwNotaDeCredito.Items[lvwNotaDeCredito.Items.Count - 1].Selected = true;
                        txtNroInternoNC.Text = lvwNotaDeCredito.Items[lvwNotaDeCredito.Items.Count - 1].Text;
                        idNRONCINTERNO = Convert.ToInt32(lvwNotaDeCredito.Items[lvwNotaDeCredito.Items.Count - 1].Text);
                    }
                    else
                        MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    GuardaItemsDatos(false, idNRONCINTERNO);
            }
            catch { conn.DesconectarBD(); connGeneric.DesconectarBD(); connGeneric.DesconectarBDLeeGeneric(); }
        }

        private void lvwNotaDeCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //timer1.Enabled = false;
                //MostrarItemsDatos();

                gboFacturasEnNC.Visible = false;

                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();                
                //nuevoRemito = false;

                idNRONCINTERNO = Convert.ToInt32(lvwNotaDeCredito.SelectedItems[0].SubItems[0].Text);
                indiceLvwNotaPedido = lvwNotaDeCredito.SelectedItems[0].Index;

                conn.LeeGeneric("SELECT * FROM NotaCredito, Cliente, ListaPrecios WHERE NotaCredito.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDLISTAPRECIO = ListaPrecios.IDLISTAPRECIO AND Cliente.IDCLIENTE = NotaCredito.IDCLIENTE AND NRONOTAINTERNO = " + Convert.ToInt32(lvwNotaDeCredito.SelectedItems[0].SubItems[0].Text) + "", "NotaCredito");

                //this.txtNroNotaPedido.Text = conn.leerGeneric["IDNOTAPEDIDO"].ToString();
                this.txtNroInternoNC.Text = conn.leerGeneric["NRONOTAINTERNO"].ToString();
                //this.cboNroSucursal.Text = conn.leerGeneric["SUCURSAL"].ToString();
                this.txtNroNC.Text = conn.leerGeneric["NRONOTACRED"].ToString();
                this.dtpFechaFactu.Value = Convert.ToDateTime(conn.leerGeneric["FECHA"].ToString());
                fechaNotaCredito = Convert.ToDateTime(conn.leerGeneric["FECHA"].ToString());

                this.txtCodTipoNC.Text = conn.leerGeneric["IDTIPONOTA"].ToString();
                iTipoNC = Convert.ToInt32(txtCodTipoNC.Text);
                if (this.txtCodTipoNC.Text.Trim() == "")
                    this.cboTipoNC.Text = "";

                this.txtCodCliente.Text = conn.leerGeneric["IDCLIENTE"].ToString();
                if (this.txtCodCliente.Text.Trim() == "")
                    this.cboCliente.Text = "";

                this.txtCodVendedor.Text = conn.leerGeneric["IDVENDEDOR"].ToString();
                if (this.txtCodVendedor.Text.Trim() == "")
                    this.cboVendedor.Text = "";

                this.txtCodFormaPago.Text = conn.leerGeneric["IDPERSONAL"].ToString();
                if (this.txtCodFormaPago.Text.Trim() == "")
                    this.cboFormaPago.Text = "";

                this.txtCodPersonal.Text = conn.leerGeneric["IDFORMADEPAGO"].ToString();
                if (this.txtCodPersonal.Text.Trim() == "")
                    this.cboPersonal.Text = "";

                //this.txtCodListaCliente.Text = conn.leerGeneric["IDLISTAPRECIO"].ToString();
                //cboListaCliente.Text = connArt.leer["DescLista"].ToString();

                this.txtObservacionNC.Text = conn.leerGeneric["OBSERVACIONES"].ToString();

                this.txtSubTotal.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["BASICO"]), 3).ToString();
                this.txtImpuesto1.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["IMPUESTO1"]), 2).ToString();
                this.txtImpuesto2.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["IMPUESTO2"]), 2).ToString();
                this.txtDescuento.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["DESCUENTOS"]), 2).ToString();
                this.txtImporteTotal.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["TOTAL"]), 2).ToString();

                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                //btnGuardar.Enabled = true;                
                MostrarItemsDatos();
                // LimpiarDetalleNotaPedido();

                //  if (fechaFacturaCompra.AddDays(1) <= DateTime.Today)                                
                //  MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { conn.DesconectarBD(); }
        }

        private void MostrarItemsDatos()
        {
            try
            {
                lvwDetalleNotaDeCredito.Items.Clear();

                double iva105;
                double iva21;

                //int iCantPedida, iCantActual, iCantRestante;
                //int CantPendiente;

                connGeneric.LeeGeneric("SELECT NotaCredito.NRONOTAINTERNO, DetalleNotaCredito.IDDETALLENOTACREDITO, Articulo.DESCRIPCION as 'Artículo', Articulo.CANT_ACTUAL, DetalleNotaCredito.CANTIDAD, DetalleNotaCredito.PRECUNITARIO as 'Precio Unitario', DetalleNotaCredito.IMPORTE as 'Importe', DetalleNotaCredito.DESCUENTO as 'Descuento', DetalleNotaCredito.PORCDESC as '% Desc', DetalleNotaCredito.SUBTOTAL as 'Subtotal', DetalleNotaCredito.IMPUESTO1 as 'Iva 10,5', DetalleNotaCredito.IMPUESTO2 as 'Iva 21', DetalleNotaCredito.OBSERVACIONES as 'Observaciones' FROM NotaCredito, DetalleNotaCredito, Articulo, Cliente, Personal WHERE NotaCredito.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND DetalleNotaCredito.IDARTICULO = Articulo.IDARTICULO AND NotaCredito.IDCLIENTE = Cliente.IDCLIENTE AND NotaCredito.IDPERSONAL = Personal.IDPERSONAL AND DetalleNotaCredito.NRONOTAINTERNO = NotaCredito.NRONOTAINTERNO AND NotaCredito.NRONOTAINTERNO =  " + Convert.ToInt32(lvwNotaDeCredito.SelectedItems[0].SubItems[0].Text) + "", "NotaCredito");

                iva105 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 10,5"].ToString());
                iva21 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 21"].ToString());

                SqlCommand cm = new SqlCommand("SELECT Articulo.Codigo, NotaCredito.NRONOTAINTERNO, DetalleNotaCredito.IDDETALLENOTACREDITO, DetalleNotaCredito.NRONOTAINTERNO, DetalleNotaCredito.IDArticulo, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL, DetalleNotaCredito.CANTIDAD, DetalleNotaCredito.PRECUNITARIO, DetalleNotaCredito.IMPORTE, DetalleNotaCredito.DESCUENTO, DetalleNotaCredito.PORCDESC, DetalleNotaCredito.SUBTOTAL, DetalleNotaCredito.IMPUESTO1 as 'Iva 10,5', DetalleNotaCredito.IMPUESTO2 as 'Iva 21', DetalleNotaCredito.OBSERVACIONES FROM NotaCredito, DetalleNotaCredito, Articulo, Cliente, Personal WHERE NotaCredito.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND DetalleNotaCredito.IDARTICULO = Articulo.IDARTICULO AND NotaCredito.IDCLIENTE = Cliente.IDCLIENTE AND NotaCredito.IDPERSONAL = Personal.IDPERSONAL AND DetalleNotaCredito.NRONOTAINTERNO = NotaCredito.NRONOTAINTERNO AND NotaCredito.NRONOTAINTERNO =" + Convert.ToInt32(lvwNotaDeCredito.SelectedItems[0].SubItems[0].Text) + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwDetalleNotaDeCredito.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDArticulo"].ToString());
                    item.SubItems.Add(dr["Codigo"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());

                    item.SubItems.Add(dr["CANTIDAD"].ToString());

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["PRECUNITARIO"]), 3).ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Subtotal"]), 3).ToString());
                    item.SubItems.Add("$ " + dr["DESCUENTO"].ToString());

                    if (dr["Iva 10,5"].ToString() != "0,0000")
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 2).ToString());
                    else
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 21"]), 2).ToString());

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPORTE"]), 2).ToString());
                    //item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.Empty, Color.LightGray, null);
                    //item.SubItems.Add(dr["CANTIDADRESTANTE"].ToString(), Color.Empty, Color.LightGray, null);

                    item.SubItems.Add(dr["OBSERVACIONES"].ToString());
                    item.SubItems.Add(dr["IDDETALLENOTACREDITO"].ToString());
                    item.SubItems.Add(dr["Iva 10,5"].ToString());
                    item.SubItems.Add(dr["Iva 21"].ToString());

                    item.ImageIndex = 2;

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleNotaDeCredito.Items.Add(item);
                }
                cm.Connection.Close();
                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
            }
            catch
            {
                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
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
                    conn.ConsultaGeneric("SELECT * FROM Articulo WHERE CODIGO = '" + this.txtCodArticulo.Text + "'", "Articulo");

                    this.cboArticulo.DataSource = conn.ds.Tables[0];
                    this.cboArticulo.ValueMember = "IdArticulo";
                    this.cboArticulo.DisplayMember = "Descripcion";
                }
                else
                {
                    cboArticulo.Text = "";
                    txtPrecio.Text = "";
                    txtCantidadArticulo.Text = "";
                    txtDescuento.Text = "0";
                }

                conn.DesconectarBD();

                if (conn.ds.Tables[0].Rows.Count < 1)
                {
                    cboArticulo.Text = "";
                    txtPrecio.Text = "";
                    txtCantidadArticulo.Text = "";
                    txtDescuento.Text = "0";
                }
                else
                {
                    conn.LeeGeneric("SELECT * FROM Articulo WHERE CODIGO = '" + this.txtCodArticulo.Text + "'", "Articulo");
                    //txtCantArticulo.Text = conn.leerGeneric["CANT_ACTUAL"].ToString();
                    txtCantidadArticulo.Text = "";
                    idArtuculo = Convert.ToInt32(conn.leerGeneric["IdArticulo"].ToString());

                    txtPrecio.Text = "$ " + Math.Round(CalculoPorcentajeNotaCredito(Convert.ToSingle(conn.leerGeneric["COSTOENLISTA"].ToString())), 3);

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

        private void txtCodCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int iTablaCant = 0;

                if (this.txtCodCliente.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("SELECT * FROM Cliente WHERE IDEMPRESA = " + IDEMPRESA + " AND IdCliente = " + Convert.ToInt32(this.txtCodCliente.Text) + "", "Cliente");

                    this.cboCliente.DataSource = conn.ds.Tables[0];
                    this.cboCliente.ValueMember = "IdCliente";
                    this.cboCliente.DisplayMember = "RazonSocial";

                    iTablaCant = conn.ds.Tables[0].Rows.Count;

                    connGeneric.DesconectarBD();
                    connGeneric.DesconectarBDLeeGeneric();

                    connGeneric.LeeGeneric("SELECT Cliente.NUMDECUIT, TipoIva.DESCRIPCION as 'TipoIva', TipoIva.IdTipoIva, ListaPrecios.IDLISTAPRECIO, ListaPrecios.DESCRIPCION as 'DescLista' FROM Cliente, TipoIva, ListaPrecios WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDTIPOIVA = TipoIva.IDTIPOIVA AND Cliente.IDLISTAPRECIO=ListaPrecios.IDLISTAPRECIO AND Cliente.IDCLIENTE = " + Convert.ToInt32(this.txtCodCliente.Text) + "", "Cliente");

                    txtCuit.Text = connGeneric.leerGeneric["NUMDECUIT"].ToString();
                    txtIva.Text = connGeneric.leerGeneric["TipoIva"].ToString();

                    iCodigoListaPrecioCliente = Convert.ToInt32(connGeneric.leerGeneric["IDLISTAPRECIO"].ToString());
                    //cboListaCliente.Text = connGeneric.leerGeneric["DescLista"].ToString();


                    /*if (txtIva.Text == "Exento")
                        txtCodTipoNC.Text = "3";
                    else*/

                    if (txtIva.Text == "Responsable inscripto" || txtIva.Text == "Responsable Inscripto")
                        txtCodTipoNC.Text = "1";
                    else
                        txtCodTipoNC.Text = "2";                                           

                }
                else
                {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    txtCodCliente.Text = "";
                    //txtCodListaCliente.Text = "";
                    //cboListaCliente.Text = "";
                }

                if (iTablaCant < 1)
                {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    txtCodCliente.Text = "";
                    //txtCodListaCliente.Text = "";
                    //cboListaCliente.Text = "";
                }

                if (this.txtCuit.Text.Trim() == "0")
                {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    txtCodCliente.Text = "";
                    //txtCodListaCliente.Text = "";
                    //cboListaCliente.Text = "";
                    MessageBox.Show("Error: Falta informacion relacionada con el Cliente (CUIT)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

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

        private void txtCodFormaPago_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodFormaPago.Text.Trim() != "")
                {
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

        private void txtCodTipoNC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodTipoNC.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("SELECT * FROM TipoNota WHERE IdTipoNota = " + Convert.ToInt32(this.txtCodTipoNC.Text) + "", "TipoNota");

                    this.cboTipoNC.DataSource = conn.ds.Tables[0];
                    this.cboTipoNC.ValueMember = "IdTipoNota";
                    this.cboTipoNC.DisplayMember = "Descripcion";
                }
                else
                    this.cboTipoNC.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboTipoNC.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
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

        private void btnCliente_Click(object sender, EventArgs e)
        {
            frmCliente formCliente = new frmCliente();
            formCliente.pasarClienteCod += new frmCliente.pasarClienteCod1(CodClient);  //Delegado1 
            formCliente.pasarClientRS += new frmCliente.pasarClienteRS(RazonS); //Delegado2

            formCliente.ShowDialog();

            //Detecta Numerador Facturación
            if (txtCodTipoNC.Text == "1")
            {
                txtNroNotaCredito.Text = Convert.ToString(NumeradorFactura(1));
                txtNroNC.Text = txtNroNotaCredito.Text;
            }
            else if (txtCodTipoNC.Text == "2")
            {
                txtNroNotaCredito.Text = Convert.ToString(NumeradorFactura(2));
                txtNroNC.Text = txtNroNotaCredito.Text;
            }
            else if (txtCodTipoNC.Text == "3")
            {
                txtNroNotaCredito.Text = Convert.ToString(NumeradorFactura(2));
                txtNroNC.Text = txtNroNotaCredito.Text;
            }
            else if (txtCodTipoNC.Text == "4")
            {
                txtNroNotaCredito.Text = Convert.ToString(NumeradorFactura(4));
                txtNroNC.Text = txtNroNotaCredito.Text;
            }
            else
            {
                txtNroNotaCredito.Text = Convert.ToString(NumeradorFactura(5));
                txtNroNC.Text = txtNroNotaCredito.Text;
            }
        }

        public void CodClient(int CodCliente)
        {
            this.txtCodCliente.Text = CodCliente.ToString();
        }

        public void RazonS(string RSCliente)
        {
            this.cboCliente.Text = RSCliente.ToString();
        }

        private void btnCodigoPersonal_Click(object sender, EventArgs e)
        {
            frmPersonal frmPerso = new frmPersonal();
            frmPerso.pasadoPerso1 += new frmPersonal.pasarPersona1(codPersonal);  //Delegado1 
            frmPerso.pasadoPerso2 += new frmPersonal.pasarPersona2(personal); //Delegado2

            txtCodFormaPago.Focus();

            frmPerso.ShowDialog();
        }

        public void codPersonal(int codPerso)
        {
            this.txtCodPersonal.Text = codPerso.ToString();
        }

        public void personal(string perso)
        {
            this.cboPersonal.Text = perso.ToString();
        }

        private void btnCodTipoFactura_Click(object sender, EventArgs e)
        {

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

        private void txtNroNC_Leave(object sender, EventArgs e)
        {
            if (ValidaNumerador(this.txtNroNC.Text.Trim()) == true)
            {
                MessageBox.Show("Error de Numerador. Ya existe el numero ingresado, el numero ha sido corregido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NuevaNC();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                //timer1.Enabled = false;

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
                string actualizar = "NRONOTACRED='" + txtNroNC.Text.Trim() + "', FECHA='" + dtpFechaFactu.Text.Trim() + "', IDTIPONOTA=" + txtCodTipoNC.Text.Trim() + " , IDFORMADEPAGO=" + txtCodFormaPago.Text.Trim() + ", IDPERSONAL=" + txtCodPersonal.Text.Trim() + ", OBSERVACIONES='" + txtObservacionNC.Text.Trim() + "'";

                if (connGeneric.ActualizaGeneric("NotaCredito", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NRONOTAINTERNO = " + Convert.ToInt32(txtNroInternoNC.Text) + ""))
                {
                    MostrarDatos();
                    MostrarItemsDatos2(Convert.ToInt32(txtNroInternoNC.Text));
                    MessageBox.Show("La información del remito ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("No se ha podido actualizar los datos de la ND.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { MessageBox.Show("Error: No se ha podido actualizar la información de la ND", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnQuitaArt_Click(object sender, EventArgs e)
        {
            try
            {
                int iIndex = 0;

                if (fechaNotaCredito.AddDays(180) <= DateTime.Today)
                    MessageBox.Show("No se puede modificar una NC de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    //timer1.Enabled = false;
                    iIndex = Convert.ToInt32(lvwDetalleNotaDeCredito.SelectedItems[0].SubItems[10].Text);  //Elemento de la base de datos
                    lvwDetalleNotaDeCredito.Items[lvwDetalleNotaDeCredito.SelectedItems[0].Index].Remove(); //Elemento del listview

                    if (connArt.EliminarArticulo("DetalleNotaCredito", " IDDETALLENOTACREDITO = " + iIndex))
                        //MostrarItemsDatos2(idNROFACTUINTERNO);

                        if (lvwDetalleNotaDeCredito.Items.Count != 0)
                        {
                            if (iIndex != 0)
                            {
                                string subTotalfactu;
                                string iva105Factu;
                                string iva21Factu;
                                string importeFactu;

                                connGeneric.DesconectarBDLeeGeneric();
                                connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleNotaCredito WHERE NRONOTAINTERNO = " + idNRONCINTERNO + "", "DetalleNotaCredito");

                                importeFactu = connGeneric.leerGeneric["Importe"].ToString();
                                iva105Factu = connGeneric.leerGeneric["Iva105"].ToString();
                                iva21Factu = connGeneric.leerGeneric["Iva21"].ToString();
                                subTotalfactu = connGeneric.leerGeneric["SubTotal"].ToString();

                                string actualizar = "BASICO=(Cast(replace('" + subTotalfactu + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + iva105Factu + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + iva21Factu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeFactu + "', ',', '.') as decimal(10,2)))";

                                this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(importeFactu), 2));
                                this.txtImpuesto1.Text = "$ " + String.Format("{0:0.000}", Decimal.Round(Convert.ToDecimal(iva105Factu), 2));
                                this.txtImpuesto2.Text = "$ " + String.Format("{0:0.000}", Decimal.Round(Convert.ToDecimal(iva21Factu), 2));
                                this.txtSubTotal.Text = "$ " + String.Format("{0:0.000}", Decimal.Round(Convert.ToDecimal(subTotalfactu), 3));

                                if (connGeneric.ActualizaGeneric("NotaCredito", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NRONOTAINTERNO = " + idNRONCINTERNO + ""))
                                {
                                    MostrarDatos();
                                    MostrarItemsDatos2(idNRONCINTERNO);
                                    // MessageBox.Show("La información de la factura ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("No se ha podido actualizar los datos de la NC.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            dSubTotal = 0.000;
                            dImpuesto1 = 0.000;
                            dImpuesto2 = 0.000;
                            dImporteTotal = 0.00;

                            char[] QuitaSimbolo = { '$', ' ' };

                            for (int i = 0; i < (lvwDetalleNotaDeCredito.Items.Count); i++)
                            {
                                dSubTotal += Math.Round(Convert.ToSingle(lvwDetalleNotaDeCredito.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo)), 3);
                                dImpuesto1 += Math.Round(Convert.ToSingle(lvwDetalleNotaDeCredito.Items[i].SubItems[11].Text.TrimStart(QuitaSimbolo)), 2);
                                dImpuesto2 += Math.Round(Convert.ToSingle(lvwDetalleNotaDeCredito.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo)), 2);
                                dImporteTotal += Math.Round(Convert.ToSingle(lvwDetalleNotaDeCredito.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo)), 2);
                            }

                            this.txtImporteTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImporteTotal, 2));
                            this.txtImpuesto1.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)dImpuesto1, 2));
                            this.txtImpuesto2.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)dImpuesto2, 2));
                            this.txtSubTotal.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)dSubTotal, 3));
                        }
                        else
                        {
                            this.txtImporteTotal.Text = "$ " + "0,00";
                            this.txtImpuesto1.Text = "$ " + "0,000";
                            this.txtImpuesto2.Text = "$ " + "0,000";
                            this.txtSubTotal.Text = "$ " + "0,000";

                            string actualizar = "BASICO=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,3))), IMPUESTOS1=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,3))), IMPUESTOS2 =(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,2)))";
                            connGeneric.ActualizaGeneric("NotaCredito", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NRONOTAINTERNO = " + idNRONCINTERNO + "");
                            MostrarDatos();
                            MostrarItemsDatos2(idNRONCINTERNO);
                        }
                }
            }
            catch { conn.DesconectarBD(); MostrarItemsDatos(); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;
                btnModificar.Enabled = false;
                //btnGuardar.Enabled = false;
                //timer1.Enabled = false;

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                if (fechaNotaCredito.AddDays(180) <= DateTime.Today)
                    MessageBox.Show("No se puede modificar una NC de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (connGeneric.EliminarGeneric("NotaCredito", " IDEMPRESA = " + IDEMPRESA + " AND NRONOTAINTERNO = " + Convert.ToInt32(this.txtNroInternoNC.Text)))
                    {
                        MostrarDatos();

                        tsBtnNuevo.Enabled = true;
                        tsBtnModificar.Enabled = true;
                        btnEliminar.Enabled = true;
                        btnModificar.Enabled = true;
                        //btnGuardar.Enabled = true;

                        this.txtCantidadArticulo.Text = "";
                        this.txtNroInternoNC.Text = "";
                        this.txtNroNC.Text = "";
                        this.txtObservacionNC.Text = "";
                        //this.cboNroSucursal.SelectedIndex = 0;
                        cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();
                        this.txtIva.Text = "";
                        this.txtCodArticulo.Text = "";
                        this.txtCodPersonal.Text = "";
                        this.txtCodCliente.Text = "";
                        this.txtCodTipoNC.Text = "";
                        this.txtCuit.Text = "";
                        this.txtDescuento.Text = "$ 0.00";
                        this.txtSubTotal.Text = "$ 0.00";
                        this.txtImpuesto1.Text = "$ 0.00";
                        this.txtImpuesto2.Text = "$ 0.00";
                        this.txtImporteTotal.Text = "$ 0.00";
                        MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //timer1.Enabled = true;
                    }
                    else
                        MessageBox.Show("Error al Eliminar. No se han eliminado los items de NC.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { MessageBox.Show("Error: Seleccione el remito a eliminar y verificar que no existan items en el detalle.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnArt_Click(object sender, EventArgs e)
        {
            frmArticulo formArt = new frmArticulo();
            formArt.pasadoArt1 += new frmArticulo.pasarArticulo1(CodVArt);  //Delegado1 
            formArt.pasadoArt2 += new frmArticulo.pasarArticulo2(NombreArt); //Delegado2
            formArt.ShowDialog();

            txtCantidadArticulo.Focus();
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

        private void tsBtnNCPorFactura_Click(object sender, EventArgs e)
        {

            if (lvwNotaDeCredito.Items.Count != 0)
            {

                gbFacturas.Visible = true;
                txtNroNotaCredito.Text = txtNroNC.Text;
                txtRazonSocial.Text = cboCliente.Text;

                if (txtCodCliente.Text.Trim() == "")
                    MostrarFacturaCliente(0);
                else
                    MostrarFacturaCliente(Convert.ToInt32(txtCodCliente.Text));
            }
        }

        private void btnCerrarRemitosPendientes_Click(object sender, EventArgs e)
        {
            gbFacturas.Visible = false;
        }

        private void MostrarFacturaCliente(int idCliente)
        {
            try
            {
                this.lvwFacturaVenta.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT NROFACTURAINTERNO as 'Código', NROFACTURA as 'Nro Fact', Sucursal, Fecha, Basico as 'Básico', DESCUENTOS as 'Desc', IMPUESTO1 as 'Iva 10,5', IMPUESTO2 as 'Iva 21', TOTAL as 'Total', FacturasVentas.OBSERVACIONES as 'Observaciones', Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, TipoFactura.Descripcion as 'Tipo' , EstadoSistema.* FROM FacturasVentas, Cliente, TipoFactura, EstadoSistema WHERE TipoFactura.IdTipoFactura=FacturasVentas.IdTipoFactura AND FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND FacturasVentas.SUCURSAL = '" + sPtoVta + "' AND EstadoSistema.IdEstado=FacturasVentas.IDESTADO AND FacturasVentas.IDCLIENTE = Cliente.IDCLIENTE AND Cliente.IDCLIENTE = " + idCliente + "  ORDER BY Código", conectaEstado);

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

                    //  if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(180) <= DateTime.Today)
                    //      item.ImageIndex = 2;
                    //  else

                    item.ImageIndex = 3;

                    item.UseItemStyleForSubItems = false;
                    lvwFacturaVenta.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                ListView.CheckedListViewItemCollection checkedItems = lvwFacturaVenta.CheckedItems;
                foreach (ListViewItem item in checkedItems)
                {
                    /*if (item.SubItems[11].Text != "No Pagado")
                    {*/
                    //Actualiza Factura

                    //string actualizar = "NROFACTURAINTERNO = " + Convert.ToInt32(item.SubItems[0].Text) + "";
                    //connArt.ActualizaArticulo("FacturasVentas", actualizar, " NRONCINTERNO = " + Convert.ToInt32(txtNroNotaCredito.Text.Trim()) + "");

                    if (item.SubItems[13].Text != "Pagado")
                    {
                        decimal ImporteActual = 0;
                        decimal NuevoImporte = 0;
                        decimal ImporteNotaCred = 0;

                        char[] QuitaSimbolo = { '$', ' ' };

                        string actualizar = "NRONCINTERNO = " + idNRONCINTERNO + ", NRONC = " + Convert.ToInt32(txtNroNC.Text.Trim()) + "";
                        connArt.ActualizaArticulo("FacturasVentas", actualizar, " NROFACTURAINTERNO = " + Convert.ToInt32(item.SubItems[0].Text) + "");   

                        connGeneric.LeeGeneric("SELECT * FROM FacturasVentas WHERE NROFACTURAINTERNO = " + Convert.ToInt32(item.SubItems[0].Text) + "", "FacturasVentas");

                        ImporteActual = Convert.ToDecimal(connGeneric.leerGeneric["PENDIENTE"].ToString());
                        ImporteNotaCred = Convert.ToDecimal(lvwNotaDeCredito.SelectedItems[0].SubItems[10].Text.TrimStart(QuitaSimbolo));

                        NuevoImporte = ImporteActual + ImporteNotaCred;

                        string actualizar2 = "PENDIENTE=(Cast(replace('" + NuevoImporte + "', ',', '.') as decimal(10,2))), PAGADO=(Cast(replace('" + ImporteNotaCred + "', ',', '.') as decimal(10,2))), IDESTADO=14";
                        connArt.ActualizaArticulo("FacturasVentas", actualizar2, " NROFACTURAINTERNO = " + Convert.ToInt32(item.SubItems[0].Text) + "");

                    
                        if (Math.Round(NuevoImporte,2).ToString() == "0,00")
                        {
                            string actualizar1 = "IDESTADO = 12";
                            connArt.ActualizaArticulo("NotaCredito", actualizar1, " NRONOTACRED = " + Convert.ToInt32(txtNroNC.Text.Trim()) + " AND sucursal = '" + sPtoVta.Trim() + "' AND IDEMPRESA = " + IDEMPRESA + "");

                            string actualizar21 = "IDESTADO = 12";
                            connArt.ActualizaArticulo("FacturasVentas", actualizar21, " NROFACTURAINTERNO = " + Convert.ToInt32(item.SubItems[0].Text) + "");
                        }
                        else
                        {
                            string actualizar1 = "IDESTADO = 13";
                            connArt.ActualizaArticulo("NotaCredito", actualizar1, " NRONOTACRED = " + Convert.ToInt32(txtNroNC.Text.Trim()) + " AND sucursal = '" + sPtoVta.Trim() + "' AND IDEMPRESA = " + IDEMPRESA + "");

                            string actualizar22 = "IDESTADO=13";
                            connArt.ActualizaArticulo("FacturasVentas", actualizar22, " NROFACTURAINTERNO = " + Convert.ToInt32(item.SubItems[0].Text) + "");
                        }


                        /*}
                        else
                            MessageBox.Show("La factura ya esta pagada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            */

                        gbFacturas.Visible = false;
                    }
                    else
                        MessageBox.Show("Error: Ya existen pagos vinculados no es posible aplicar una N.C. con facturas pagadas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {

            }
        }

        private void tsBtnReporte_Click(object sender, EventArgs e)
        {
            try
            {             

                if (lvwNotaDeCredito.Items.Count == 0)
                    MessageBox.Show("Error: No existen datos para mostrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (iTipoNC == 1)  //A
                   {
                        tipoNC = 1;
                        nroNCInt = Convert.ToInt32(lvwNotaDeCredito.SelectedItems[0].SubItems[0].Text);
                        nroNotaCredito = Convert.ToInt32(lvwNotaDeCredito.SelectedItems[0].SubItems[2].Text);
                        DGestion.Reportes.frmRPTNotaCredito frmRptNC = new DGestion.Reportes.frmRPTNotaCredito();

                        //EstadoFactura(nroComprobInt);
                        frmRptNC.ShowDialog();
                    }
                    else if (iTipoNC == 2)  //B
                    {

                        tipoNC = 2;
                        nroNCInt = Convert.ToInt32(lvwNotaDeCredito.SelectedItems[0].SubItems[0].Text);
                        nroNotaCredito = Convert.ToInt32(lvwNotaDeCredito.SelectedItems[0].SubItems[2].Text);
                        DGestion.Reportes.frmRPTNotaCredito frmRptFactu = new DGestion.Reportes.frmRPTNotaCredito();

                        //EstadoFactura(nroComprobInt);
                        frmRptFactu.ShowDialog();
                    }
                }
            }
            catch //(System.Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                MessageBox.Show("Error: No se ha seleccionado el comprobante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           


        }

        private void tsNotaCreditoFactu_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwNotaDeCredito.SelectedItems.Count == 1)
                {
                    gboFacturasEnNC.Visible = true;
                    lvwFactuEnNC.Items.Clear();

                    SqlCommand cm = new SqlCommand("SELECT NotaCredito.NRONOTACRED, FacturasVentas.NROFACTURAINTERNO, FacturasVentas.NROFACTURA, FacturasVentas.SUCURSAL, FacturasVentas.FECHA FROM FacturasVentas, NotaCredito, Empresa WHERE FacturasVentas.NRONCINTERNO = NotaCredito.NRONOTAINTERNO AND Empresa.IDEMPRESA = FacturasVentas.IDEMPRESA AND NotaCredito.NRONOTACRED = " + Convert.ToInt32(lvwNotaDeCredito.SelectedItems[0].SubItems[2].Text) + " and Empresa.IDEMPRESA = " + IDEMPRESA + " AND FacturasVentas.sucursal = '" + sPtoVta.Trim() + "' ORDER BY FacturasVentas.NROFACTURA", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwFactuEnNC.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["NROFACTURAINTERNO"].ToString());
                        item.SubItems.Add(dr["NROFACTURA"].ToString());
                        item.SubItems.Add(dr["SUCURSAL"].ToString());
                        item.SubItems.Add(dr["FECHA"].ToString());

                        item.ImageIndex = 3;

                        item.UseItemStyleForSubItems = false;
                        lvwFactuEnNC.Items.Add(item);
                    }
                    cm.Connection.Close();
                }
            }
            catch { }
        }

        private void btnCerrarFactuNC_Click(object sender, EventArgs e)
        {
            gboFacturasEnNC.Visible = false;
        }

        private void txtCodCliente_Leave(object sender, EventArgs e)
        {
            //Detecta Numerador Facturación
            if (txtCodTipoNC.Text == "1")
                txtNroNotaCredito.Text = Convert.ToString(NumeradorFactura(1));

            else if (txtCodTipoNC.Text == "2")
            {
                txtNroNotaCredito.Text = Convert.ToString(NumeradorFactura(2));
                txtNroNC.Text = txtNroNotaCredito.Text;
            }

            else if (txtCodTipoNC.Text == "3")
            {
                txtNroNotaCredito.Text = Convert.ToString(NumeradorFactura(2));
                txtNroNC.Text = txtNroNotaCredito.Text;
            }

            else if (txtCodTipoNC.Text == "4")
            {
                txtNroNotaCredito.Text = Convert.ToString(NumeradorFactura(4));
                txtNroNC.Text = txtNroNotaCredito.Text;
            }

            else
            {
                txtNroNotaCredito.Text = Convert.ToString(NumeradorFactura(5));
                txtNroNC.Text = txtNroNotaCredito.Text;
            }

        }

        private int NumeradorFactura(int iCodTipoNC)
        {
            try
            {
                int iNroNotaCredito = 0;
                bool bValidaNumerador;
                int iTipoNota;

                if (iCodTipoNC == 2)
                    iTipoNota = 3;
                else
                    iTipoNota = 2;

                SqlCommand cm = new SqlCommand("SELECT MAX(NRONOTACRED) AS 'UltamoNroNota' FROM NotaCredito, TipoNota WHERE NotaCredito.IDTIPONOTA = TipoNota.IDTIPONOTA AND NotaCredito.SUCURSAL = '" + sPtoVta + "' AND NotaCredito.IdEmpresa = " + IDEMPRESA + " AND (NotaCredito.IDTIPONOTA = " + iCodTipoNC + " OR NotaCredito.IDTIPONOTA =" + iTipoNota + ")", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                    iNroNotaCredito = Convert.ToInt32(dr["UltamoNroNota"].ToString());

                iNroNotaCredito = iNroNotaCredito + 1;

                //bValidaNumerador = ValidaNumerador(iNroFactura.ToString());

                return iNroNotaCredito;
            }
            catch { return 1; }
        }

    }
}