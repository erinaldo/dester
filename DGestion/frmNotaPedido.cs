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
using System.Threading;

namespace DGestion
{
    public partial class frmNotaPedido : Form
    {
        public delegate void pasarNotaPedidoCod1(int CodNotaPedido);
        public event pasarNotaPedidoCod1 pasarNPCod;

        public static int nroNotaPedido;

        public frmNotaPedido()
        {
            InitializeComponent();
        }

        CGenericBD connGeneric = new CGenericBD();
        EmpresaBD connEmpresa = new EmpresaBD();
        CGenericBD conn = new CGenericBD();
        ArticulosBD connArt = new ArticulosBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

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
        DateTime fechaNotaPedido;

        int contadorNROfactNuevo;
        bool nuevaNotaPedido = false;
        int idNroNotaPedidoInterno;

        int indiceLvwNotaPedido;
        int idArtuculo;

        double porcGeneralLista = 0;
        double procenFleteLista = 0;
        double CostoEnLista = 0;

        double ValorUnitarioArticulo;

        int iCantRefresh = 0;

        int IDEMPRESA;
        string sPtoVta;

        private void conPermi()
        {
            try
            {
                string sUsuarioLegueado;
                sUsuarioLegueado = frmPrincipal.Usuario;

                SqlCommand cm = new SqlCommand("SELECT Personal.USUARIO, Personal.NOMBREYAPELLIDO, PermisoModulo.Idmodulo, PermisoControl.IdControl, PermisoControl.Control, EstadoSistema.Descripcion, PermisoUsuario.IdPersonal FROM Personal, PermisoModulo, PermisoControl, PermisoUsuario, EstadoSistema WHERE PermisoUsuario.IdPersonal = Personal.IdPersonal AND PermisoUsuario.IdModulo = PermisoModulo.IdModulo AND PermisoUsuario.IdControl = PermisoControl.IdControl AND PermisoUsuario.IdEstadoSistema = EstadoSistema.IdEstado AND PermisoUsuario.IdModulo = 2 AND Personal.USUARIO = '" + sUsuarioLegueado + "'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows[0]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[0]["Control"].ToString().Trim() == "Actualizar Nota Pedido")
                {
                    btnModificar.Enabled = true;
                    tsBtnModificar.Enabled = true;
                }
                else
                {
                    btnModificar.Enabled = false;
                    tsBtnModificar.Enabled = false;
                }

                if (dt.Rows[1]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[1]["Control"].ToString().Trim() == "Eliminar Nota Pedido")
                    btnEliminar.Enabled = true;
                else
                    btnEliminar.Enabled = false;

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

        private string FormateoFecha()
        {
            DateTimePicker dtr = new DateTimePicker();
            dtr.Value = DateTime.Now;
            return String.Format("{0:d/M/yyyy HH:mm:ss}", dtr.Value);
        }

        private double CalculoPorcentajeListaVenta(double valorArticulo)
        {
            double valorMasArticuloPorcVenta;
            double valorMasArticuloPorcFlete;
            double valorTotalArticulo;

            connGeneric.DesconectarBD(); connGeneric.DesconectarBDLeeGeneric();
            conn.DesconectarBD(); conn.DesconectarBDLeeGeneric();

            //////BUSCO LOS PORCENTAJES DE LA LISTA DE PRECIO DE VENTA///////
            if (txtCodListaCliente.Text.Trim() != "")
            {
                conn.LeeGeneric("SELECT * FROM ListaPrecios WHERE IDLISTAPRECIO = " + Convert.ToInt32(txtCodListaCliente.Text) + "", "ListaPrecios");
                porcGeneralLista = Convert.ToDouble(conn.leerGeneric["Porcentaje"].ToString());
                procenFleteLista = Convert.ToDouble(conn.leerGeneric["Porcflete"].ToString());
            }
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
            //valorTotalArticulo = valorArticulo + (valorMasArticuloPorcVenta + valorMasArticuloPorcFlete);

            return Math.Round(valorTotalArticulo,3);
        }

        private int EvaluaCantPendiente(int iCantEntrega, int iCantPedida, int iCantRestante, int iExistencia, string Remitido)
        {
            int iFaltante;

            if (Remitido == "Si")
                return 0;
            else
            {
                if (iExistencia == 0 || iExistencia < iCantPedida)
                {
                    //iFaltante = (iCantPedida  - iExistencia);
                    if (iExistencia == 0)
                        iFaltante = (iCantPedida + iCantRestante);

                    else if (iExistencia < iCantPedida)
                        iFaltante = (iCantPedida - iExistencia);

                    else
                        iFaltante = (iCantPedida + iCantRestante) - iExistencia;

                    if (iFaltante < 0)
                        iFaltante = 0;
                    // else if (iFaltante > 0)
                    //    iFaltante = iCantPedida - iExistencia;
                    //return (iCantPedida - iCantEntrega);

                    return iFaltante;
                }

                else
                {
                    if (iCantRestante >= 0)
                        return 0;
                    else
                        if ((-(iExistencia - iCantPedida) < 0))
                        return 0;
                    else
                        return -(iExistencia - iCantPedida);
                }
            }
        }

        private int EvaluaCantidadEntrega(int iCantEntrega, int iCantPedida, int iCantRestante, int iExistencia, string Remitido)
        {
            if (Remitido == "No")
            {
                if (iExistencia == 0)
                    return 0;
                if (iCantRestante == 0)
                    return iCantEntrega;

                else if (iCantPedida > iExistencia)
                    return (iExistencia);//(iCantEntrega);

                else
                    return (iCantEntrega);
            }
            else
                return 0;
        }

        private void GuardaItemsDatos(bool status, int nroNotaInter)
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
                double Importe = 0;

                int CantActualArticulo;  //Stock
                int Cant_Pedida;        //Mercaderia Solicitada
                int Cant_Restante;      //Mercaderia pendiente por stock

                int Cant_para_Entregar;  //Mercaderia para entregar

                //string Observaciones;

                porcGeneralLista = 0;
                procenFleteLista = 0;
                CostoEnLista = 0;

                if (txtCantArticulo.Text.Trim() != "")
                {
                    connGeneric.DesconectarBD(); connGeneric.DesconectarBDLeeGeneric();
                    conn.DesconectarBD(); conn.DesconectarBDLeeGeneric();

                    //////BUSCO VALORES DEL ARTICULO///////
                    conn.LeeGeneric("SELECT * FROM Articulo WHERE CODIGO = '" + txtCodigoArticulo.Text.Trim() + "'", "Articulo");
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

                    Cant_Pedida = Convert.ToInt32(txtCantPedida.Text.Trim());
                    Cant_Restante = Convert.ToInt32(txtCantRestante.Text.Trim());

                    SubTotal = Math.Round((Cant_Pedida * CalculoPorcentajeListaVenta(CostoEnLista)), 3);
                    Importe = Math.Round((CalculoPorcentajeListaVenta(CostoEnLista) * Cant_Pedida), 2);
                    ////////////////////////////////////// //////////////////////////////////////

                    lvwDetalleNotaPedido.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(IdArticulo.ToString());
                    item.SubItems.Add(Codigo.ToString());
                    item.SubItems.Add(Descripcion.ToString());
                    item.SubItems.Add(Cant_Pedida.ToString());
                    item.SubItems.Add("$ " + Math.Round(CalculoPorcentajeListaVenta(CostoEnLista), 3).ToString());

                    item.SubItems.Add("$ " + Math.Round(Importe, 2).ToString());
                    item.SubItems.Add("0");

                    if (IdImpuesto == 3)
                    {
                        Impuesto2 = Math.Round(((SubTotal * 1) - SubTotal), 2);
                        Neto = Math.Round((Importe + Impuesto2), 3);
                        item.SubItems.Add("$ " + Math.Round(Impuesto2, 2).ToString());
                        item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());

                        sumaImpuesto2 += Impuesto2;
                        txtImpuesto2.Text = "$ " + Math.Round(sumaImpuesto2, 2).ToString();

                        sumaNetos += Neto;
                        txtTotalFactur.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                    }

                    else if (IdImpuesto == 2)
                    {
                        Impuesto1 = Math.Round(((SubTotal * 1.105) - SubTotal), 2);
                        Neto = Math.Round((Importe + Impuesto1), 3);
                        item.SubItems.Add("$ " + Math.Round(Impuesto1, 2).ToString());
                        item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());

                        sumaImpuesto1 += Impuesto1;
                        txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto1, 2).ToString();

                        sumaNetos += Neto;
                        txtTotalFactur.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                    }

                    else if (IdImpuesto == 1)
                    {
                        Impuesto2 = Math.Round(((SubTotal * 1.21) - SubTotal), 2);
                        Neto = Math.Round((Importe + Impuesto2), 3);
                        item.SubItems.Add("$ " + Math.Round(Impuesto2, 2).ToString());
                        item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());

                        sumaImpuesto2 += Impuesto2;
                        txtImpuesto2.Text = "$ " + Math.Round(sumaImpuesto2, 2).ToString();

                        sumaNetos += Neto;
                        txtTotalFactur.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                    }

                    sumaTotales += SubTotal;
                    txtSubTotal.Text = "$ " + Math.Round(sumaTotales, 3).ToString();

                    item.SubItems.Add(CantActualArticulo.ToString());
                    item.SubItems.Add(Cant_Restante.ToString());

                    //item.SubItems.Add("-");
                    item.SubItems.Add("0");  //colocar el IDNotaPedido

                    if (IdImpuesto != 2)
                        Impuesto1 = 0;
                    if (IdImpuesto != 1)
                        Impuesto2 = 0;

                    item.SubItems.Add(Math.Round(Impuesto1, 2).ToString());
                    item.SubItems.Add(Math.Round(Impuesto2, 2).ToString());

                    Cant_Restante = CantActualArticulo - Cant_Pedida;
                    if (Cant_Restante < 0)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;

                    ///EVALUA CANTIDAD A ENTREGAR
                    if (CantActualArticulo < Cant_Pedida)
                        Cant_para_Entregar = Cant_Pedida - (-(Cant_Restante));
                    else
                        Cant_para_Entregar = Cant_Pedida;
                    //////////////////////////////////////////////////////

                    item.SubItems.Add(Cant_para_Entregar.ToString());
                    lvwDetalleNotaPedido.Items.Add(item);

                    //Normalizacion de Saldos totales
                    if (lvwDetalleNotaPedido.Items.Count != 0)
                    {
                        dSubTotal = 0.000;
                        dImpuesto1 = 0.000;
                        dImpuesto2 = 0.000;
                        dImporteTotal = 0.00;

                        char[] QuitaSimbolo = { '$', ' ' };
                        for (int i = 0; i < (lvwDetalleNotaPedido.Items.Count); i++)
                        {
                            dImporteTotal += Math.Round(Convert.ToSingle(lvwDetalleNotaPedido.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo)), 2);
                            dImpuesto1 += Math.Round(Convert.ToSingle(lvwDetalleNotaPedido.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo)), 2);
                            dImpuesto2 += Math.Round(Convert.ToSingle(lvwDetalleNotaPedido.Items[i].SubItems[13].Text.TrimStart(QuitaSimbolo)), 2);
                            dSubTotal += Math.Round(Convert.ToSingle(lvwDetalleNotaPedido.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo)), 3);
                        }
                        this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImporteTotal, 2));
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
                        if (fechaNotaPedido.AddDays(15) <= DateTime.Today)
                            MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            txtNroInternoNota.Text = idNroNotaPedidoInterno.ToString();
                            idNroNotaPedidoInterno = nroNotaInter;

                            connGeneric.EliminarGeneric("DetalleNotaPedido", " IDNotaPedido = " + nroNotaInter);
                            char[] QuitaSimbolo = { '$', ' ' };

                            for (int i = 0; i < (lvwDetalleNotaPedido.Items.Count); i++)
                            {
                                string agregarItem = "INSERT INTO DetalleNotaPedido(IDARTICULO, CANTIDADPEDIDA, CANTIDADRESTANTE, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, IDNOTAPEDIDO, CANTIDADAENTREGA) VALUES (" + Convert.ToInt32(lvwDetalleNotaPedido.Items[i].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[10].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), '0','0', (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))), (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[13].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))), " + nroNotaInter + ", (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[14].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))))";

                                if (conn.InsertarGeneric(agregarItem))
                                {
                                    connGeneric.DesconectarBD();
                                    connGeneric.DesconectarBDLeeGeneric();
                                }
                            }
                            MostrarItemsDatos2(nroNotaInter);
                            MostrarDatos();
                        }
                    }

                    else if (status == true)
                    {
                        char[] QuitaSimbolo = { '$', ' ' };
                        for (int i = 0; i < (lvwDetalleNotaPedido.Items.Count); i++)
                        {
                            string agregarItem = "INSERT INTO DetalleNotaPedido(IDARTICULO, CANTIDADPEDIDA, CANTIDADRESTANTE, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, IDNOTAPEDIDO, CANTIDADAENTREGA) VALUES (" + Convert.ToInt32(lvwDetalleNotaPedido.Items[i].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[10].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), '0','0', (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))), (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[13].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), " + Convert.ToInt32(lvwNotaPedido.Items[lvwNotaPedido.Items.Count - 1].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleNotaPedido.Items[i].SubItems[14].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))))";

                            nroNotaInter = Convert.ToInt32(lvwNotaPedido.Items[lvwNotaPedido.Items.Count - 1].SubItems[0].Text);

                            if (conn.InsertarGeneric(agregarItem))
                            {
                                connGeneric.DesconectarBD();
                                connGeneric.DesconectarBDLeeGeneric();
                            }
                        }
                        //MessageBox.Show("Item Actualizado/Agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);                           

                        MostrarItemsDatos2(nroNotaInter);
                        MostrarDatos();
                    }

                    //////////////////////////////////////////////// ACTUALIZA EL AGREGADO DE DATOS ////////////////////////////////////////////////
                    connGeneric.DesconectarBDLeeGeneric();
                    connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleNotaPedido WHERE IDNOTAPEDIDO = " + nroNotaInter + "", "DetalleNotaPedido");

                    subTotalfactu = Convert.ToSingle(connGeneric.leerGeneric["SubTotal"].ToString());
                    iva105Factu = Convert.ToSingle(connGeneric.leerGeneric["Iva105"].ToString());
                    iva21Factu = Convert.ToSingle(connGeneric.leerGeneric["Iva21"].ToString());
                    importeFactu = Convert.ToSingle(connGeneric.leerGeneric["Importe"].ToString());

                    string actualizar = "BASICO=(Cast(replace('" + subTotalfactu + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + iva105Factu + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + iva21Factu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeFactu + "', ',', '.') as decimal(10,2))), SITUACION='PENDIENTE'";

                    this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeFactu, 2));
                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)iva105Factu, 2));
                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)iva21Factu, 2));
                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)subTotalfactu, 2));

                    if (connGeneric.ActualizaGeneric("NotaPedido", actualizar, " IDNOTAPEDIDO = " + nroNotaInter + " AND IDEMPRESA = " + IDEMPRESA + ""))
                    {

                        MostrarItemsDatos2(nroNotaInter);
                        MostrarDatos();
                        // MessageBox.Show("La información de la factura ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se ha podido actualizar los datos de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else
                    MessageBox.Show("Error al Agregar Artículo: No se ha ingresado artículo o cantidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { MessageBox.Show("Error: Falta algún tipo de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void MostrarItemsDatos2(int NRONotaPedidoInterno)
        {
            try
            {
                lvwDetalleNotaPedido.Items.Clear();

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                double iva105;
                double iva21;
                int iCantPedida, iCantActual, iCantRestante, iCantAEntregar;
                string sEstadoRemito;

                int CantPendiente;
                int iCantEntrega;

                connGeneric.LeeGeneric("SELECT NotaPedido.IdNotaPedido, DetalleNotaPedido.IDDETALLENOTAPEDIDO as 'Código', NotaPedido.Situacion, Articulo.DESCRIPCION as 'Artículo', DetalleNotaPedido.CANTIDADPEDIDA as 'Cantidad Pedida', DetalleNotaPedido.CANTIDADAENTREGA, DetalleNotaPedido.PRECUNITARIO as 'Precio Unitario', DetalleNotaPedido.IMPORTE as 'Importe', DetalleNotaPedido.DESCUENTO as 'Descuento', DetalleNotaPedido.PORCDESC as '% Desc', DetalleNotaPedido.SUBTOTAL as 'Subtotal', DetalleNotaPedido.IMPUESTO1 as 'Iva 10,5', DetalleNotaPedido.IMPUESTO2 as 'Iva 21', Articulo.CANT_ACTUAL, DetalleNotaPedido.CantidadRestante, DetalleNotaPedido.Remitido FROM NotaPedido, DetalleNotaPedido, Articulo, Cliente, Vendedor WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND NotaPedido.IDEMPRESA = " + IDEMPRESA + " AND DetalleNotaPedido.IDARTICULO = Articulo.IDARTICULO AND NotaPedido.IDCLIENTE = Cliente.IDCLIENTE AND NotaPedido.IDVENDEDOR = Vendedor.IDVENDEDOR AND DetalleNotaPedido.IDNOTAPEDIDO = NotaPedido.IDNOTAPEDIDO AND NotaPedido.IdNotaPedido = " + NRONotaPedidoInterno + "", "NotaPedido");

                iva105 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 10,5"].ToString());
                iva21 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 21"].ToString());

                SqlCommand cm = new SqlCommand("SELECT Articulo.Codigo as 'Código', NotaPedido.IdNotaPedido, NotaPedido.Situacion, DetalleNotaPedido.IdDetalleNotaPedido, DetalleNotaPedido.IDArticulo as 'Código Artículo', Articulo.DESCRIPCION as 'Artículo', DetalleNotaPedido.CANTIDADPEDIDA as 'Cantidad Pedida', DetalleNotaPedido.CANTIDADAENTREGA, DetalleNotaPedido.PRECUNITARIO as 'Precio Unitario', DetalleNotaPedido.IMPORTE as 'Importe', DetalleNotaPedido.DESCUENTO as 'Descuento', DetalleNotaPedido.PORCDESC as '% Desc', DetalleNotaPedido.SUBTOTAL as 'Subtotal', DetalleNotaPedido.IMPUESTO1 as 'Iva 10,5', DetalleNotaPedido.IMPUESTO2 as 'Iva 21', Articulo.CANT_ACTUAL, DetalleNotaPedido.CantidadRestante, DetalleNotaPedido.Remitido FROM NotaPedido, DetalleNotaPedido, Articulo, Cliente, Vendedor WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND NotaPedido.IDEMPRESA = " + IDEMPRESA + " AND DetalleNotaPedido.IDARTICULO = Articulo.IDARTICULO AND NotaPedido.IDCliente = Cliente.IDCliente AND NotaPedido.IDVendedor = Vendedor.IDVendedor AND DetalleNotaPedido.IDNotaPedido = NotaPedido.IDNotaPedido AND NotaPedido.IDNotaPedido = " + NRONotaPedidoInterno + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwDetalleNotaPedido.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Código Artículo"].ToString());
                    item.SubItems.Add(dr["Código"].ToString());
                    item.SubItems.Add(dr["Artículo"].ToString());

                    item.SubItems.Add(dr["Cantidad Pedida"].ToString(), Color.Black, Color.DarkGray, new System.Drawing.Font(
                    "Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Precio Unitario"]), 3).ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Subtotal"]), 2).ToString());
                    item.SubItems.Add(dr["Descuento"].ToString());

                    if (dr["Iva 10,5"].ToString() != "0,0000")
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 2).ToString());
                    else
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 21"]), 2).ToString());

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Importe"]), 2).ToString());

                    //Funcion de Verificacion de cantidades pedida de articulo para detarminar el stock superado
                    if (Convert.ToInt32(dr["CANT_ACTUAL"].ToString()) <= 0)
                        item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.LightSalmon, Color.Red, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                    else if (Convert.ToInt32(dr["Cantidad Pedida"].ToString()) > Convert.ToInt32(dr["CANT_ACTUAL"].ToString()))
                        item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.Yellow, Color.Red, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                    else
                    {
                        if (EvaluaSituacionStock(Convert.ToInt32(dr["Código Artículo"].ToString()), Convert.ToInt32(dr["CANT_ACTUAL"].ToString())))
                            item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.Red, Color.LightSalmon, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
                        else
                            item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.DarkGreen, Color.LightGreen, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
                    }
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    item.SubItems.Add(dr["CANTIDADRESTANTE"].ToString(), Color.Empty, Color.LightGray, null);

                    ///EVALUA SITUACION STOCK y PEDIDOS///
                    iCantPedida = Convert.ToInt32(dr["Cantidad Pedida"].ToString());
                    iCantActual = Convert.ToInt32(dr["CANT_ACTUAL"].ToString());
                    iCantRestante = Convert.ToInt32(dr["CANTIDADRESTANTE"].ToString()); //iCantActual - iCantPedida;
                    iCantAEntregar = Convert.ToInt32(dr["CANTIDADAENTREGA"].ToString());
                    sEstadoRemito = (dr["REMITIDO"].ToString());
                    iCantEntrega = Convert.ToInt32(dr["CANTIDADAENTREGA"].ToString());
                    CantPendiente = EvaluaCantPendiente(iCantEntrega, iCantPedida, iCantRestante, iCantActual, sEstadoRemito);

                    ActualizaEstadoDetalleNotaPedido(iCantRestante, CantPendiente, iCantActual, iCantAEntregar, iCantPedida, sEstadoRemito, dr["Situacion"].ToString(), Convert.ToInt32(dr["IdDetalleNotaPedido"].ToString()), Convert.ToInt32(dr["IdNotaPedido"].ToString()));

                    if (iCantAEntregar > 0)
                        item.ImageIndex = 2;
                    else if (iCantRestante < 1)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 2;
                    //////////////////////////////////////////////////////////////

                    //item.SubItems.Add(dr["OBSERVACIONES"].ToString());
                    item.SubItems.Add(dr["IdDetalleNotaPedido"].ToString());
                    item.SubItems.Add(dr["Iva 10,5"].ToString());
                    item.SubItems.Add(dr["Iva 21"].ToString());

                    item.SubItems.Add(EvaluaCantidadEntrega(iCantEntrega, iCantPedida, iCantRestante, iCantActual, sEstadoRemito).ToString(), Color.Black, Color.DarkGray, new System.Drawing.Font(
                    "Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                    if (iCantAEntregar >= 0 && dr["Remitido"].ToString() == "No")
                    {
                        item.SubItems.Add(CantPendiente.ToString(), Color.Red, Color.LightSalmon, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                        if (dr["Remitido"].ToString() == "No" && iCantActual < 1)
                        {
                            item.SubItems.Add("S/E", Color.LightSalmon, Color.Red, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
                            item.ImageIndex = 1;
                        }
                        else
                            item.SubItems.Add(dr["Remitido"].ToString(), Color.LightSalmon, Color.Red, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
                    }
                    else
                    {
                        item.SubItems.Add(CantPendiente.ToString(), Color.Green, Color.LightGreen, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                        if (dr["Remitido"].ToString() == "No")
                            item.SubItems.Add(dr["Remitido"].ToString(), Color.Red, Color.LightSalmon, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
                        else
                        {
                            item.SubItems.Add(dr["Remitido"].ToString(), Color.LightGreen, Color.Green, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
                            item.ImageIndex = 0;
                        }
                    }

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleNotaPedido.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {

                MostrarDatos();

                nuevaNotaPedido = true;
                timer1.Enabled = true;
                btnCerrarDetalle.Enabled = false;
                cboNroSucursal.Focus();

                dSubTotal = 0.000;
                dImpuesto1 = 0.000;
                dImpuesto2 = 0.000;
                dImporteTotal = 0.00;

                sumaImpuesto1 = 0;
                sumaImpuesto2 = 0;
                sumaNetos = 0;
                sumaTotales = 0;

                Impuesto1 = 0;
                Impuesto2 = 0;
                Neto = 0;

                dtpFechaFactu.Value = DateTime.Today;
                fechaNotaPedido = DateTime.Today;

                lvwDetalleNotaPedido.Items.Clear();
                gpoNotaDePedido.Visible = true;
                gpNotaPedido.Width = 210;
                lvwNotaPedido.Width = 190;
                lvwDetalleNotaPedido.Height = 240;

                gpoDetalleNota.Visible = true;
                gpNotaPedido.Height = 260;
                lvwNotaPedido.Height = 235;

                cboBuscaNotaPedido.SelectedIndex = 0;
                //cboNroSucursal.SelectedIndex = 0;

                cboNroSucursal.Text = frmPrincipal.PtoVenta;

                lblCodArt.Visible = true;
                txtCodigoArticulo.Visible = true;
                cboArticulo.Visible = true;
                btnAgregaArt.Visible = true;
                btnQuitaArt.Visible = true;
                lblCantActual.Visible = true;
                txtCantArticulo.Visible = true;
                lblCantPedida.Visible = true;
                txtCantPedida.Visible = true;
                lblCantRestante.Visible = true;
                txtCantRestante.Visible = true;
                btnBuscaArticulo.Visible = true;

                tsBtnNuevo.Enabled = true;
                lblPrecio.Visible = true;
                txtPrecio.Visible = true;
                //lblDescuento.Visible = true;
                //txtProcDesc.Visible = true;

                this.txtCantArticulo.Text = "";
                this.txtNroInternoNota.Text = "0";                
                //this.cboNroSucursal.SelectedIndex=0;
                this.txtObservacionNotaPedido.Text = "";
                this.txtIva.Text = "";
                this.txtCodigoArticulo.Text = "";
                this.txtCodCliente.Text = "";
                this.txtCodVendedor.Text = "";
                this.txtCuit.Text = "";
                this.txtDescuento.Text = "$ 0,000";
                this.txtSubTotal.Text = "$ 0,000";
                this.txtImpuesto1.Text = "$ 0,000";
                this.txtImpuesto2.Text = "$ 0,000";
                this.txtTotalFactur.Text = "$ 0,00";

                LimpiarDetalleNotaPedido();
                Limpieza();

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                /////////////////////////////////////////// AUTONUMERICO NRO INTERNO //////////////////////////////////////////
                connGeneric.LeeGeneric("SELECT MAX(NRONOTAPEDIDO) as NRO FROM NOTAPEDIDO WHERE IDEMPRESA=" + IDEMPRESA + " AND SUCURSAL = " + sPtoVta + " ORDER BY NRO", "NOTAPEDIDO");

                if (connGeneric.leerGeneric["NRO"].ToString() == "")
                {
                    txtNroInternoNota.Text = "0";
                    txtNroNPedido.Text = "0";
                }
                else
                {
                    //txtNroIntRemito.Text = connGeneric.leerGeneric["NRO"].ToString();
                    txtNroNPedido.Text = connGeneric.leerGeneric["NRO"].ToString();
                }

                contadorNROfactNuevo = (Convert.ToInt32(txtNroNPedido.Text));
                contadorNROfactNuevo = contadorNROfactNuevo + 1;

                txtNroNPedido.Text = Convert.ToString(contadorNROfactNuevo);


                /*if (connGeneric.leerGeneric["NRO"].ToString() == "")
                    txtNroInternoNota.Text = "0";
                else
                    txtNroInternoNota.Text = connGeneric.leerGeneric["NRO"].ToString();

                contadorNROfactNuevo = (Convert.ToInt32(txtNroInternoNota.Text));
                contadorNROfactNuevo = contadorNROfactNuevo + 1;
                txtNroInternoNota.Text = contadorNROfactNuevo.ToString();

                //txtNroIntRemito.Text = this.txtNroIntRemito.Text;
                //this.txtNroNPedido.Text = this.cboNroSucursal.Text.Trim() + "-" + this.txtNroInternoNota.Text;
                this.txtNroNPedido.Text =  this.txtNroInternoNota.Text;

                ValidaNumerador(this.txtNroNPedido.Text);

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();*/
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                
                /////////////////////////////////////////// AUTONUMERICO NRO INTERNO //////////////////////////////////////////
                /*connGeneric.LeeGeneric("SELECT MAX(IDNOTAPEDIDO) as NRO FROM NOTAPEDIDO", "NOTAPEDIDO");

                if (connGeneric.leerGeneric["NRO"].ToString() == "")
                    txtNroInternoNota.Text = "0";
                else
                    txtNroInternoNota.Text = connGeneric.leerGeneric["NRO"].ToString();

                contadorNROfactNuevo = (Convert.ToInt32(txtNroInternoNota.Text));
                contadorNROfactNuevo = contadorNROfactNuevo + 1;
                txtNroInternoNota.Text = contadorNROfactNuevo.ToString();

                txtNroNotaPedido.Text = this.txtNroInternoNota.Text;

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();*/
            }
            catch { MessageBox.Show("Error: El nro ingresado no es numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private bool ValidaNumerador(string nrocomprobante)
        {
            try
            {
                SqlCommand cm = new SqlCommand("SELECT NRONOTAPEDIDO FROM NOTAPEDIDO WHERE NOTAPEDIDO.IDEMPRESA = " + IDEMPRESA + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    if (nrocomprobante == dr["NRONOTAPEDIDO"].ToString().Trim())
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

        private void Limpieza()
        {
            txtCodCliente.Text = "";
            cboCliente.Text = "";
            txtCodigoArticulo.Text = "";
            cboArticulo.Text = "";
            txtCodVendedor.Text = "";
            cboVendedor.Text = "";
            txtCantArticulo.Text = "";
            txtCantPedida.Text = "";
            txtCantRestante.Text = "";
        }

        //Metodos de delegado Tipo Impuesto
        public void CodClient(int dato1)
        {
            this.txtCodCliente.Text = dato1.ToString();
        }

        public void RazonS(string dato2)
        {
            this.cboCliente.Text = dato2.ToString();
        }

        private void btnArticulo_Click(object sender, EventArgs e)
        {
            frmArticulo formArt = new frmArticulo();
            formArt.pasadoArt1 += new frmArticulo.pasarArticulo1(CodVArt);  //Delegado1 
            formArt.pasadoArt2 += new frmArticulo.pasarArticulo2(NombreArt); //Delegado2
            txtCantPedida.Focus();
            formArt.ShowDialog();
        }

        //Metodos de delegado Tipo Impuesto
        public void CodVArt(string dato1)
        {
            this.txtCodigoArticulo.Text = dato1.ToString();
        }

        public void NombreArt(string dato2)
        {
            this.cboArticulo.Text = dato2.ToString();
        }

        //Metodos de delegado Tipo Impuesto
        public void CodVende(int dato1)
        {
            this.txtCodVendedor.Text = dato1.ToString();
        }

        public void NombreVende(string dato2)
        {
            this.cboVendedor.Text = dato2.ToString();
        }

        private void txtCodigoArticulo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sCodArt;
                char[] QuitaSimbolo = { '$', ' ' };
                char[] QuitaSimbolo2 = { '*', ' ' };

                sCodArt = txtCodigoArticulo.Text.TrimStart(QuitaSimbolo2);
                sCodArt = sCodArt.TrimEnd(QuitaSimbolo2);
                this.txtCodigoArticulo.Text = sCodArt;

                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();

                if (this.txtCodigoArticulo.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("SELECT * FROM Articulo WHERE CODIGO = '" + this.txtCodigoArticulo.Text + "'", "Articulo");

                    this.cboArticulo.DataSource = conn.ds.Tables[0];
                    this.cboArticulo.ValueMember = "IdArticulo";
                    this.cboArticulo.DisplayMember = "Descripcion";
                }
                else
                {
                    this.cboArticulo.Text = "";
                    txtCantPedida.Text = "";
                    txtPrecio.Text = "";
                    txtDescuento.Text = "0";
                }
                

                conn.DesconectarBD();

                if (conn.ds.Tables[0].Rows.Count < 1)
                {
                    cboArticulo.Text = "";
                    txtCantArticulo.Text = "";
                }
                else
                {
                    conn.LeeGeneric("SELECT * FROM Articulo WHERE CODIGO = '" + this.txtCodigoArticulo.Text + "'", "Articulo");
                    txtCantArticulo.Text = conn.leerGeneric["CANT_ACTUAL"].ToString();
                    idArtuculo = Convert.ToInt32(conn.leerGeneric["IdArticulo"].ToString());

                    txtPrecio.Text = "$ " + Math.Round(CalculoPorcentajeListaVenta(Convert.ToSingle(conn.leerGeneric["COSTOENLISTA"].ToString())), 3);

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

        private void frmPedidoNota_Load(object sender, EventArgs e)
        {
            conPermi();

            gpoNotaDePedido.Visible = false;
            gpNotaPedido.Width = 950;
            lvwNotaPedido.Width = 940;
            lvwDetalleNotaPedido.Height = 290;

            lblCodArt.Visible = false;
            txtCodigoArticulo.Visible = false;
            cboArticulo.Visible = false;
            btnAgregaArt.Visible = false;
            btnQuitaArt.Visible = false;
            btnAgregaArt.Visible = false;
            lblCantActual.Visible = false;
            txtCantArticulo.Visible = false;

            txtCantPedida.Visible = false;
            lblCantPedida.Visible = false;

            lblCantRestante.Visible = false;
            txtCantRestante.Visible = false;

            lblPrecio.Visible = false;
            txtPrecio.Visible = false;

            btnBuscaArticulo.Visible = false;
            //lblDescuento.Visible = false;
            //txtProcDesc.Visible = false;

            dtpFechaFactu.Value = DateTime.Today;
            fechaNotaPedido = DateTime.Today;

            conn.ConectarBD();
            cboBuscaNotaPedido.SelectedIndex = 0;

            cboNroSucursal.Text = frmPrincipal.PtoVenta; //Recupera el Pto. de Venta seleccionado
            //cboNroSucursal.SelectedIndex = 0;

            FormatoListView();
            IDEMPRESA = ConsultaEmpresa(); //Lee Empresa

            MostrarDatos();
        }

        public void MostrarDatos()
        {
            try
            {
                this.lvwNotaPedido.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT * FROM NotaPedido, Cliente WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND NotaPedido.IDEMPRESA = " + IDEMPRESA + " AND NotaPedido.SUCURSAL = '" + sPtoVta + "' AND NotaPedido.IDCLIENTE=Cliente.IDCLIENTE", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwNotaPedido.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IdNotaPedido"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["NRONOTAPEDIDO"].ToString());
                    item.SubItems.Add(dr["FechaNota"].ToString());
                    item.SubItems.Add(dr["IdCliente"].ToString());
                    item.SubItems.Add(dr["RazonSocial"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Basico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Descuento"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto1"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto2"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());
                    item.SubItems.Add(dr["IdVendedor"].ToString());

                    /*if (Convert.ToDateTime(item.SubItems[1].Text).AddDays(2) <= DateTime.Today)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;*/

                    if (dr["SITUACION"].ToString() == "CERRADA")
                    {
                        item.ImageIndex = 7;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    else if (dr["SITUACION"].ToString() == "COMPLETADA")
                    {
                        item.ImageIndex = 0;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    else if (dr["SITUACION"].ToString() == "PENDIENTE")
                    {
                        item.ImageIndex = 2;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkOrange, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    else if (dr["SITUACION"].ToString() == "INCOMPLETA")
                    {
                        item.ImageIndex = 1;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    else
                    {  //CANCELADA
                        item.ImageIndex = 6;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    item.UseItemStyleForSubItems = false;
                    lvwNotaPedido.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void FormatoListView()
        {
            try
            {
                this.lvwNotaPedido.View = View.Details;
                this.lvwNotaPedido.LabelEdit = false;
                this.lvwNotaPedido.AllowColumnReorder = false;
                this.lvwNotaPedido.FullRowSelect = true;
                this.lvwNotaPedido.GridLines = true;

                this.lvwDetalleNotaPedido.View = View.Details;
                this.lvwDetalleNotaPedido.LabelEdit = false;
                this.lvwDetalleNotaPedido.AllowColumnReorder = false;
                this.lvwDetalleNotaPedido.FullRowSelect = true;
                this.lvwDetalleNotaPedido.GridLines = true;

                this.lvwPedidoClientes.View = View.Details;
                this.lvwPedidoClientes.LabelEdit = false;
                this.lvwPedidoClientes.AllowColumnReorder = false;
                this.lvwPedidoClientes.FullRowSelect = true;
                this.lvwPedidoClientes.GridLines = true;
            }
            catch { }
        }

        private void tsBtnModificar_Click(object sender, EventArgs e)
        {
            nuevaNotaPedido = false;
            timer1.Enabled = false;
            btnCerrarDetalle.Enabled = false;
            cboNroSucursal.Focus();

            if (lvwNotaPedido.SelectedItems.Count == 0)
                MessageBox.Show("Error: No se ha seleccionado ninguna factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                gpoNotaDePedido.Visible = true;
                gpNotaPedido.Width = 210;
                lvwNotaPedido.Width = 190;
                lvwDetalleNotaPedido.Height = 240;

                gpoDetalleNota.Visible = true;
                gpNotaPedido.Height = 260;
                lvwNotaPedido.Height = 235;

                cboBuscaNotaPedido.SelectedIndex = 0;

                lblCodArt.Visible = true;
                txtCodigoArticulo.Visible = true;
                cboArticulo.Visible = true;
                btnAgregaArt.Visible = true;
                btnQuitaArt.Visible = true;
                lblCantActual.Visible = true;
                txtCantArticulo.Visible = true;
                lblCantPedida.Visible = true;
                txtCantPedida.Visible = true;
                lblCantRestante.Visible = true;
                txtCantRestante.Visible = true;
                btnBuscaArticulo.Visible = true;

                tsBtnNuevo.Enabled = true;

                lblPrecio.Visible = true;
                txtPrecio.Visible = true;
            }

        }

        private void txtCantPedida_TextChanged(object sender, EventArgs e)
        {
            int cantPedida, cantRestante, cantActual;
            double totalPedido;

            char[] QuitaSimbolo = { '$', ' ' };

            try
            {
                if (txtCantPedida.Text.Trim() != "")
                {
                    cantPedida = Convert.ToInt32(txtCantPedida.Text);
                    cantActual = Convert.ToInt32(txtCantArticulo.Text);
                    cantRestante = cantActual - cantPedida;
                    txtCantRestante.Text = cantRestante.ToString();

                    totalPedido = ValorUnitarioArticulo * cantPedida;
                    txtPrecio.Text = "$ " + String.Format("{0:0.000}", (decimal)totalPedido);
                }
                else
                {
                    txtCantRestante.Text = "";
                    txtPrecio.Text = "$ " + "0,000";
                }
            }
            catch { }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            gpoNotaDePedido.Visible = false;
            gpNotaPedido.Width = 950;
            lvwNotaPedido.Width = 940;
            lvwDetalleNotaPedido.Height = 290;
            btnCerrarDetalle.Enabled = true;

            lblCodArt.Visible = false;
            txtCodigoArticulo.Visible = false;
            cboArticulo.Visible = false;
            btnAgregaArt.Visible = false;
            btnQuitaArt.Visible = false;
            btnAgregaArt.Visible = false;
            lblCantActual.Visible = false;
            txtCantArticulo.Visible = false;

            txtCantPedida.Visible = false;
            lblCantPedida.Visible = false;

            lblCantRestante.Visible = false;
            txtCantRestante.Visible = false;

            lblPrecio.Visible = false;
            txtPrecio.Visible = false;

            btnBuscaArticulo.Visible = false;
            //lblDescuento.Visible = false;
            //txtProcDesc.Visible = false;
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            frmCliente formCliente = new frmCliente();
            formCliente.pasarClienteCod += new frmCliente.pasarClienteCod1(CodClient);  //Delegado1 
            formCliente.pasarClientRS += new frmCliente.pasarClienteRS(RazonS); //Delegado2
            formCliente.ShowDialog();
        }

        private void txtCodCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodCliente.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("SELECT * FROM Cliente WHERE IdCliente = " + Convert.ToInt32(this.txtCodCliente.Text) + " AND idempresa=" + IDEMPRESA + "", "Cliente");

                    this.cboCliente.DataSource = conn.ds.Tables[0];
                    this.cboCliente.ValueMember = "IdCliente";
                    this.cboCliente.DisplayMember = "RazonSocial";

                    connGeneric.DesconectarBD();
                    connGeneric.DesconectarBDLeeGeneric();

                    connGeneric.LeeGeneric("SELECT Cliente.NUMDECUIT, TipoIva.DESCRIPCION as 'TipoIva', TipoIva.IdTipoIva, ListaPrecios.IDLISTAPRECIO, ListaPrecios.DESCRIPCION as 'DescLista' FROM Cliente, TipoIva, ListaPrecios WHERE Cliente.idempresa=" + IDEMPRESA + " AND Cliente.IDTIPOIVA = TipoIva.IDTIPOIVA AND Cliente.IDLISTAPRECIO=ListaPrecios.IDLISTAPRECIO AND Cliente.IDCLIENTE = " + Convert.ToInt32(this.txtCodCliente.Text) + "", "Cliente");

                    txtCuit.Text = connGeneric.leerGeneric["NUMDECUIT"].ToString();
                    txtIva.Text = connGeneric.leerGeneric["TipoIva"].ToString();

                    txtCodListaCliente.Text = connGeneric.leerGeneric["IDLISTAPRECIO"].ToString();
                    cboListaCliente.Text = connGeneric.leerGeneric["DescLista"].ToString();
                }
                else
                {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    //txtCodCliente.Text = "";
                    txtCodListaCliente.Text = "";
                    cboListaCliente.Text = "";
                }

                if (conn.ds.Tables[0].Rows.Count < 1)
                {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    //txtCodCliente.Text = "";
                    txtCodListaCliente.Text = "";
                    cboListaCliente.Text = "";
                }

                if (this.txtCuit.Text.Trim() == "0")
                {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    //txtCodCliente.Text = "";
                    txtCodListaCliente.Text = "";
                    cboListaCliente.Text = "";
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

        private void btnVendedor_Click(object sender, EventArgs e)
        {
            frmVendedor formVende = new frmVendedor();
            formVende.pasarVendeCod += new frmVendedor.pasarVendeCod1(CodVende);  //Delegado1 
            formVende.pasarVendeN += new frmVendedor.pasarVendeRS(NombreVende); //Delegado2
            formVende.ShowDialog();
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

        private void btnAgregaArt_Click(object sender, EventArgs e)
        {
            if (fechaNotaPedido.AddDays(15) <= DateTime.Today)
                MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (lvwDetalleNotaPedido.Items.Count >= 25)
                    MessageBox.Show("Límite de cantidad de items por remito excedida. Se deberá crear un nuevo remito para continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    timer1.Enabled = true;
                    GuardarTodosLosDatos();
                    txtCodigoArticulo.Text = "";
                    cboArticulo.Text = "";
                    txtCantPedida.Text = "";
                    txtCantArticulo.Text = "";
                    //txtProcDesc.Text = "";
                    txtPrecio.Text = "";
                    txtCodigoArticulo.Focus();
                }
            }
        }

        private void GuardarTodosLosDatos()
        {
            try
            {
                float subTotal;
                float impuesto1;
                float impuesto2;
                float descuento;
                float importeTotal;

                //Quita Simbolos para guardar los datos en formato numéricos
                char[] QuitaSimbolo = { '$', ' ' };
                importeTotal = Convert.ToSingle(this.txtTotalFactur.Text.TrimStart(QuitaSimbolo));
                impuesto1 = Convert.ToSingle(this.txtImpuesto1.Text.TrimStart(QuitaSimbolo));
                impuesto2 = Convert.ToSingle(this.txtImpuesto2.Text.TrimStart(QuitaSimbolo));
                subTotal = Convert.ToSingle(this.txtSubTotal.Text.TrimStart(QuitaSimbolo));
                descuento = Convert.ToSingle(this.txtDescuento.Text.TrimStart(QuitaSimbolo));
                /////////////////////////////////////////////////////////////////////////////////

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                if (nuevaNotaPedido == true)
                    connGeneric.ConsultaGeneric("Select * FROM NotaPedido WHERE IDEMPRESA = " + IDEMPRESA + " AND IDNOTAPEDIDO = " + Convert.ToInt32(txtNroInternoNota.Text) + "", "NotaPedido");
                else
                    connGeneric.ConsultaGeneric("Select * FROM NotaPedido WHERE IDEMPRESA = " + IDEMPRESA + " AND IDNOTAPEDIDO = " + idNroNotaPedidoInterno + "", "NotaPedido");
                if (connGeneric.ds.Tables[0].Rows.Count == 0)
                {
                    string agregar = "INSERT INTO NotaPedido(NRONOTAPEDIDO, FECHANOTA, SUCURSAL, IDCLIENTE, IDVENDEDOR, BASICO, DESCUENTO, IMPUESTO1, IMPUESTO2, TOTAL, OBSERVACIONES, IDEMPRESA) VALUES ('" + txtNroNPedido.Text.Trim() + "', '" + FormateoFecha() + "', '" + cboNroSucursal.Text.Trim() + "', " + txtCodCliente.Text.Trim() + "," + txtCodVendedor.Text.Trim() + " , (Cast(replace('" + Math.Round(subTotal, 3) + "', ',', '.') as decimal(10,3))), " + descuento + " , (Cast(replace('" + Math.Round(impuesto1, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(impuesto2, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(importeTotal, 2) + "', ',', '.') as decimal(10,2))), '" + txtObservacionNotaPedido.Text + "', " + IDEMPRESA + ")";

                    this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeTotal, 2));
                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto1, 2));
                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto2, 2));
                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)subTotal, 2));

                    if (conn.InsertarGeneric(agregar))
                    {
                        MostrarDatos();
                        GuardaItemsDatos(true, 0);
                        lvwNotaPedido.Items[lvwNotaPedido.Items.Count - 1].Selected = true;
                        txtNroInternoNota.Text = lvwNotaPedido.Items[lvwNotaPedido.Items.Count - 1].Text;
                        idNroNotaPedidoInterno = Convert.ToInt32(lvwNotaPedido.Items[lvwNotaPedido.Items.Count - 1].Text);
                    }
                    else
                        MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    GuardaItemsDatos(false, idNroNotaPedidoInterno);
            }
            catch { conn.DesconectarBD(); connGeneric.DesconectarBD(); connGeneric.DesconectarBDLeeGeneric(); }
        }

        private void lvwDetalleNotaPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connArt.DesconectarBDLee();
                connArt.DesconectarBD();

                txtCodArtic.Text = lvwDetalleNotaPedido.SelectedItems[0].SubItems[1].Text;

                connArt.LeeArticulo("SELECT * FROM ARTICULO WHERE IDARTICULO = " + Convert.ToInt32(lvwDetalleNotaPedido.SelectedItems[0].SubItems[0].Text) + "", "ARTICULO");

                this.txtCodigoArticulo.Text = connArt.leer["CODIGO"].ToString();
                this.cboArticulo.Text = connArt.leer["DESCRIPCION"].ToString();

                this.txtCantArticulo.Text = connArt.leer["CANT_ACTUAL"].ToString();
                this.txtCantPedida.Text = this.lvwDetalleNotaPedido.SelectedItems[0].SubItems[3].Text;
                //this.txtCantRestante.Text = this.lvwDetalleNotaPedido.SelectedItems[0].SubItems[10].Text;

                //this.txtProcDesc.Text = this.lvwDetalleNotaPedido.SelectedItems[0].SubItems[6].Text;
                this.txtPrecio.Text = "$ " + CalculoPorcentajeListaVenta(Math.Round(Convert.ToSingle(conn.leerGeneric["COSTOENLISTA"].ToString()), 3));
                //this.dtpFechaFactu.Value = Convert.ToDateTime(connArt.leer["FECHA"].ToString());

                connArt.DesconectarBDLee();
                connArt.DesconectarBD();

                //btnGuardar.Enabled = true;
                //MostrarItemsDatos();                

            }
            catch { conn.DesconectarBD(); }
        }

        private void lvwNotaPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                MostrarItemsDatos();

                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                nuevaNotaPedido = false;

                idNroNotaPedidoInterno = Convert.ToInt32(lvwNotaPedido.SelectedItems[0].SubItems[0].Text);
                indiceLvwNotaPedido = lvwNotaPedido.SelectedItems[0].Index;

                conn.LeeGeneric("SELECT * FROM NotaPedido, Cliente, ListaPrecios WHERE NotaPedido.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDLISTAPRECIO=ListaPrecios.IDLISTAPRECIO AND Cliente.IDCLIENTE = NotaPedido.IDCLIENTE AND IdNotaPedido = " + Convert.ToInt32(lvwNotaPedido.SelectedItems[0].SubItems[0].Text) + "", "NotaPedido");

                this.txtNroNotaPedido.Text = conn.leerGeneric["IDNOTAPEDIDO"].ToString();
                this.txtNroInternoNota.Text = conn.leerGeneric["IDNOTAPEDIDO"].ToString();
                this.txtNroNPedido.Text = conn.leerGeneric["NRONOTAPEDIDO"].ToString();
                this.cboNroSucursal.Text = conn.leerGeneric["SUCURSAL"].ToString();
                this.dtpFechaFactu.Value = Convert.ToDateTime(conn.leerGeneric["FECHANOTA"].ToString());
                fechaNotaPedido = Convert.ToDateTime(conn.leerGeneric["FECHANOTA"].ToString());

                this.txtCodCliente.Text = conn.leerGeneric["IDCLIENTE"].ToString();
                if (this.txtCodCliente.Text.Trim() == "")
                    this.cboCliente.Text = "";

                this.txtCodVendedor.Text = conn.leerGeneric["IDVENDEDOR"].ToString();
                if (this.txtCodVendedor.Text.Trim() == "")
                    this.cboVendedor.Text = "";

                this.txtCodListaCliente.Text = conn.leerGeneric["IDLISTAPRECIO"].ToString();
                //cboListaCliente.Text = connArt.leer["DescLista"].ToString();

                this.txtObservacionNotaPedido.Text = conn.leerGeneric["OBSERVACIONES"].ToString();

                this.txtSubTotal.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["BASICO"]), 2).ToString();
                this.txtImpuesto1.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["IMPUESTO1"]), 2).ToString();
                this.txtImpuesto2.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["IMPUESTO2"]), 2).ToString();
                this.txtDescuento.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["DESCUENTO"]), 2).ToString();
                this.txtTotalFactur.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["TOTAL"]), 2).ToString();

                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                //btnGuardar.Enabled = true;                
                MostrarItemsDatos();
                // LimpiarDetalleNotaPedido();

                //  if (fechaFacturaCompra.AddDays(1) <= DateTime.Today)                                
                //  MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);   

            }
            catch { conn.DesconectarBD(); }
        }

        private void LimpiarDetalleNotaPedido()
        {
            this.txtCantArticulo.Text = "";
            this.txtCodigoArticulo.Text = "";
            this.txtCantRestante.Text = "";
            this.txtCantPedida.Text = "";
            this.txtPrecio.Text = "";
            this.cboListaCliente.Text = "";
            this.txtCodListaCliente.Text = "";
        }

        private void MostrarItemsDatos()
        {
            try
            {
                lvwDetalleNotaPedido.Items.Clear();

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                double iva105;
                double iva21;

                int iCantPedida, iCantActual, iCantRestante, iCantAEntregar;

                int CantPendiente;
                string sEstadoRemito;
                int iCantEntrega;

                connGeneric.LeeGeneric("SELECT NotaPedido.IdNotaPedido, DetalleNotaPedido.IdDetalleNotaPedido as 'Código', NotaPedido.Situacion, Articulo.DESCRIPCION as 'Artículo', Articulo.CANT_ACTUAL, DetalleNotaPedido.CANTIDADPEDIDA as 'Cant. Pedida', DetalleNotaPedido.CANTIDADRESTANTE as 'CantRestante', DetalleNotaPedido.CANTIDADAENTREGA, DetalleNotaPedido.PRECUNITARIO as 'Precio Unitario', DetalleNotaPedido.IMPORTE as 'Importe', DetalleNotaPedido.DESCUENTO as 'Descuento', DetalleNotaPedido.PORCDESC as '% Desc', DetalleNotaPedido.SUBTOTAL as 'Subtotal', DetalleNotaPedido.IMPUESTO1 as 'Iva 10,5', DetalleNotaPedido.IMPUESTO2 as 'Iva 21', DetalleNotaPedido.OBSERVACIONES as 'Observaciones', DetalleNotaPedido.Remitido FROM NotaPedido, DetalleNotaPedido, Articulo, Cliente, Vendedor WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND NotaPedido.IDEMPRESA = " + IDEMPRESA + " AND DetalleNotaPedido.IDARTICULO = Articulo.IDARTICULO AND NotaPedido.IDCLIENTE = Cliente.IDCLIENTE AND NotaPedido.IDVENDEDOR = Vendedor.IDVENDEDOR AND DetalleNotaPedido.IDNOTAPEDIDO = NotaPedido.IDNOTAPEDIDO AND NotaPedido.IDNOTAPEDIDO = " + Convert.ToInt32(lvwNotaPedido.SelectedItems[0].SubItems[0].Text) + "", "NotaPedido");

                iva105 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 10,5"].ToString());
                iva21 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 21"].ToString());

                SqlCommand cm = new SqlCommand("SELECT Articulo.Codigo, NotaPedido.IdNotaPedido, NotaPedido.Situacion, DetalleNotaPedido.IdDetalleNotaPedido, DetalleNotaPedido.IDArticulo, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL, DetalleNotaPedido.CANTIDADPEDIDA, DetalleNotaPedido.CANTIDADAENTREGA, DetalleNotaPedido.PRECUNITARIO, DetalleNotaPedido.CANTIDADRESTANTE, DetalleNotaPedido.IMPORTE, DetalleNotaPedido.DESCUENTO, DetalleNotaPedido.PORCDESC, DetalleNotaPedido.SUBTOTAL, DetalleNotaPedido.IMPUESTO1 as 'Iva 10,5', DetalleNotaPedido.IMPUESTO2 as 'Iva 21', DetalleNotaPedido.OBSERVACIONES, DetalleNotaPedido.Remitido FROM NotaPedido, DetalleNotaPedido, Articulo, Cliente, Vendedor WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND NotaPedido.IDEMPRESA = " + IDEMPRESA + " AND DetalleNotaPedido.IDARTICULO = Articulo.IDARTICULO AND NotaPedido.IDCLIENTE = Cliente.IDCLIENTE AND NotaPedido.IDVENDEDOR = Vendedor.IDVENDEDOR AND DetalleNotaPedido.IDNOTAPEDIDO = NotaPedido.IdNotaPedido AND NotaPedido.IdNotaPedido = " + Convert.ToInt32(lvwNotaPedido.SelectedItems[0].SubItems[0].Text) + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwDetalleNotaPedido.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDArticulo"].ToString());
                    item.SubItems.Add(dr["Codigo"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());

                    item.SubItems.Add(dr["CANTIDADPEDIDA"].ToString(), Color.Black, Color.DarkGray, new System.Drawing.Font(
                    "Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["PRECUNITARIO"]), 3).ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Subtotal"]), 2).ToString());
                    item.SubItems.Add(dr["DESCUENTO"].ToString());

                    if (dr["Iva 10,5"].ToString() != "0,0000")
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 2).ToString());
                    else
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 21"]), 2).ToString());

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPORTE"]), 2).ToString());


                    //Funcion de Verificacion de cantidades pedida de articulo para detarminar el stock superado
                    if (Convert.ToInt32(dr["CANT_ACTUAL"].ToString()) <= 0)
                        item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.LightSalmon, Color.Red, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                    else if (Convert.ToInt32(dr["CANTIDADPEDIDA"].ToString()) > Convert.ToInt32(dr["CANT_ACTUAL"].ToString()))
                        item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.Yellow, Color.Red, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                    else
                    {
                        if (EvaluaSituacionStock(Convert.ToInt32(dr["IDArticulo"].ToString()), Convert.ToInt32(dr["CANT_ACTUAL"].ToString())))
                            item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.Red, Color.LightSalmon, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
                        else
                            item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.DarkGreen, Color.LightGreen, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
                    }
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    item.SubItems.Add(dr["CANTIDADRESTANTE"].ToString(), Color.Empty, Color.LightGray, null);

                    ///EVALUA SITUACION STOCK y PEDIDOS///
                    iCantPedida = Convert.ToInt32(dr["CANTIDADPEDIDA"].ToString());
                    iCantActual = Convert.ToInt32(dr["CANT_ACTUAL"].ToString());
                    iCantRestante = Convert.ToInt32(dr["CANTIDADRESTANTE"].ToString()); //iCantActual - iCantPedida;
                    iCantAEntregar = Convert.ToInt32(dr["CANTIDADAENTREGA"].ToString());
                    sEstadoRemito = (dr["REMITIDO"].ToString());
                    iCantEntrega = Convert.ToInt32(dr["CANTIDADAENTREGA"].ToString());
                    CantPendiente = EvaluaCantPendiente(iCantEntrega, iCantPedida, iCantRestante, iCantActual, sEstadoRemito);
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    ActualizaEstadoDetalleNotaPedido(iCantRestante, CantPendiente, iCantActual, iCantAEntregar, iCantPedida, sEstadoRemito, dr["Situacion"].ToString(), Convert.ToInt32(dr["IdDetalleNotaPedido"].ToString()), Convert.ToInt32(dr["IdNotaPedido"].ToString()));


                    if (iCantAEntregar > 0 && iCantActual > 0)
                        item.ImageIndex = 2;
                    else if (iCantRestante < 1)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 2;
                    //////////////////////////////////////////////////////////////


                    //item.SubItems.Add(dr["OBSERVACIONES"].ToString());
                    item.SubItems.Add(dr["IdDetalleNotaPedido"].ToString());
                    item.SubItems.Add(dr["Iva 10,5"].ToString());
                    item.SubItems.Add(dr["Iva 21"].ToString());



                    item.SubItems.Add(EvaluaCantidadEntrega(iCantEntrega, iCantPedida, iCantRestante, iCantActual, sEstadoRemito).ToString(), Color.Black, Color.DarkGray, new System.Drawing.Font(
                    "Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                    if (iCantAEntregar >= 0 && dr["Remitido"].ToString() == "No")
                    {
                        item.SubItems.Add(CantPendiente.ToString(), Color.Red, Color.LightSalmon, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                        if (dr["Remitido"].ToString() == "No" && iCantActual < 1)
                        {
                            item.SubItems.Add("S/E", Color.LightSalmon, Color.Red, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
                            item.ImageIndex = 1;
                        }
                        else
                            item.SubItems.Add(dr["Remitido"].ToString(), Color.LightSalmon, Color.Red, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
                    }
                    else
                    {
                        item.SubItems.Add(CantPendiente.ToString(), Color.Green, Color.LightGreen, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                        if (dr["Remitido"].ToString() == "No")
                            item.SubItems.Add(dr["Remitido"].ToString(), Color.Red, Color.LightSalmon, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
                        else
                        {
                            item.SubItems.Add(dr["Remitido"].ToString(), Color.LightGreen, Color.Green, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
                            item.ImageIndex = 0;
                        }
                    }


                    item.UseItemStyleForSubItems = false;
                    lvwDetalleNotaPedido.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private bool EvaluaSituacionStock(int iArticulo, int iCantActual)
        {
            connGeneric.DesconectarBD();
            connGeneric.DesconectarBDLeeGeneric();

            connGeneric.LeeGeneric("SELECT Sum(CANTIDADPEDIDA) as 'Cantidad para Entregar' FROM DetalleNotaPedido WHERE DetalleNotaPedido.IDARTICULO = " + iArticulo + "", "DetalleNotaPedido");

            if (Convert.ToInt32(connGeneric.leerGeneric["Cantidad para Entregar"].ToString()) > iCantActual)
                return true;  //Cantidad de Demanda Excedida del Stock actual
            else
                return false; //Cantidad de Demanda Excedida del Stock actual
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                tsBtnNuevo.Enabled = true;

                //btnGuardar.Enabled = false;
                timer1.Enabled = false;

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                if (fechaNotaPedido.AddDays(15) <= DateTime.Today)
                    MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (connGeneric.EliminarGeneric("NotaPedido", " IDEMPRESA = " + IDEMPRESA + " AND IDNOTAPEDIDO = " + Convert.ToInt32(this.txtNroInternoNota.Text)))
                    {

                        MostrarDatos();

                        tsBtnNuevo.Enabled = true;

                        //btnGuardar.Enabled = true;

                        this.txtCantPedida.Text = "";
                        this.txtCantRestante.Text = "";
                        this.txtCantArticulo.Text = "";
                        this.txtNroInternoNota.Text = "";
                        this.txtNroNotaPedido.Text = "";
                        this.txtNroNPedido.Text = "";
                        this.txtObservacionNotaPedido.Text = "";
                        this.cboNroSucursal.SelectedIndex = 0;
                        this.cboNroSucursal.Text = frmPrincipal.PtoVenta;
                        this.txtIva.Text = "";
                        this.txtCodigoArticulo.Text = "";
                        this.txtCodVendedor.Text = "";
                        this.txtCodCliente.Text = "";
                        this.txtCuit.Text = "";
                        this.txtDescuento.Text = "$ 0,000";
                        this.txtSubTotal.Text = "$ 0,000";
                        this.txtImpuesto1.Text = "$ 0,000";
                        this.txtImpuesto2.Text = "$ 0,000";
                        this.txtTotalFactur.Text = "$ 0,00";
                        MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        timer1.Enabled = true;
                    }
                    else
                        MessageBox.Show("Error al Eliminar, seleccionar factura. Verificar existencia de items de N.P.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { }// MessageBox.Show("Error: Seleccione la factura a eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
        }

        private void lvwNotaPedido_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                nuevaNotaPedido = false;

                idNroNotaPedidoInterno = Convert.ToInt32(lvwNotaPedido.SelectedItems[0].SubItems[0].Text);
                indiceLvwNotaPedido = lvwNotaPedido.SelectedItems[0].Index;

                conn.LeeGeneric("SELECT * FROM Cliente, ListaPrecios, NotaPedido WHERE NotaPedido.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDLISTAPRECIO=ListaPrecios.IDLISTAPRECIO AND Cliente.IDCLIENTE = NotaPedido.IDCLIENTE AND IdNotaPedido = " + Convert.ToInt32(lvwNotaPedido.SelectedItems[0].SubItems[0].Text) + "", "NotaPedido");

                this.txtNroInternoNota.Text = conn.leerGeneric["IdNotaPedido"].ToString();

                this.cboNroSucursal.Text = conn.leerGeneric["SUCURSAL"].ToString();
                this.dtpFechaFactu.Value = Convert.ToDateTime(conn.leerGeneric["FECHANOTA"].ToString());

                fechaNotaPedido = Convert.ToDateTime(conn.leerGeneric["FECHANOTA"].ToString());

                this.txtCodCliente.Text = conn.leerGeneric["IDCLIENTE"].ToString();
                if (this.txtCodCliente.Text.Trim() == "")
                    this.cboCliente.Text = "";

                this.txtCodVendedor.Text = conn.leerGeneric["IDVENDEDOR"].ToString();
                if (this.txtCodVendedor.Text.Trim() == "")
                    this.cboVendedor.Text = "";

                this.txtCodListaCliente.Text = conn.leerGeneric["IDLISTAPRECIO"].ToString();
                //this.cboListaCliente.Text = conn.leerGeneric["DescLista"].ToString();

                this.txtObservacionNotaPedido.Text = conn.leerGeneric["OBSERVACIONES"].ToString();

                this.txtSubTotal.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["BASICO"]), 2).ToString();
                this.txtImpuesto1.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["IMPUESTO1"]), 2).ToString();
                this.txtImpuesto2.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["IMPUESTO2"]), 2).ToString();
                this.txtDescuento.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["DESCUENTO"]), 2).ToString();
                this.txtTotalFactur.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["TOTAL"]), 2).ToString();

                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                //btnGuardar.Enabled = true;

                MostrarItemsDatos();
                MostrarItemsDatos();                

                pasarNPCod(Int32.Parse(this.lvwNotaPedido.SelectedItems[0].SubItems[0].Text));  //Si doble click ejecuta delegado para pasar datos entre forms

                MostrarItemsDatos();
                MostrarDatos();
                this.Close();
                //LimpiarDetalleNotaPedido();
                //if (fechaFacturaCompra.AddDays(1) <= DateTime.Today)                                
                //MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch { conn.DesconectarBD(); }
        }

        private void btnQuitaArt_Click(object sender, EventArgs e)
        {
            try
            {
                int iIndex = 0;

                if (fechaNotaPedido.AddDays(15) <= DateTime.Today)
                    MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    timer1.Enabled = false;
                    iIndex = Convert.ToInt32(lvwDetalleNotaPedido.SelectedItems[0].SubItems[11].Text);  //Elemento de la base de datos
                    lvwDetalleNotaPedido.Items[lvwDetalleNotaPedido.SelectedItems[0].Index].Remove(); //Elemento del listview

                    if (conn.EliminarGeneric("DetalleNotaPedido", " IdDetalleNotaPedido = " + iIndex))
                        //MostrarItemsDatos2(idNROFACTUINTERNO);

                        if (lvwDetalleNotaPedido.Items.Count != 0)
                        {
                            if (iIndex != 0)
                            {
                                string subTotalfactu;
                                string iva105Factu;
                                string iva21Factu;
                                string importeFactu;

                                connGeneric.DesconectarBDLeeGeneric();
                                connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleNotaPedido WHERE IdNotaPedido = " + idNroNotaPedidoInterno + "", "DetalleNotaPedido");

                                importeFactu = connGeneric.leerGeneric["Importe"].ToString();
                                iva105Factu = connGeneric.leerGeneric["Iva105"].ToString();
                                iva21Factu = connGeneric.leerGeneric["Iva21"].ToString();
                                subTotalfactu = connGeneric.leerGeneric["SubTotal"].ToString();

                                string actualizar = "BASICO=(Cast(replace('" + subTotalfactu + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + iva105Factu + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + iva21Factu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeFactu + "', ',', '.') as decimal(10,3)))";

                                this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(importeFactu), 2));
                                this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(iva105Factu), 2));
                                this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(iva21Factu), 2));
                                this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(subTotalfactu), 2));

                                if (connGeneric.ActualizaGeneric("Notapedido", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND IdNotaPedido = " + idNroNotaPedidoInterno + ""))
                                {
                                    MostrarItemsDatos2(idNroNotaPedidoInterno);
                                    MostrarDatos();
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
                            for (int i = 0; i < (lvwDetalleNotaPedido.Items.Count); i++)
                            {
                                dImporteTotal += Math.Round(Convert.ToSingle(lvwDetalleNotaPedido.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo)), 2);
                                dImpuesto1 += Math.Round(Convert.ToSingle(lvwDetalleNotaPedido.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo)), 2);
                                dImpuesto2 += Math.Round(Convert.ToSingle(lvwDetalleNotaPedido.Items[i].SubItems[13].Text.TrimStart(QuitaSimbolo)), 2);
                                dSubTotal += Math.Round(Convert.ToSingle(lvwDetalleNotaPedido.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo)), 2);
                            }

                            this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImporteTotal, 2));
                            this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImpuesto1, 2));
                            this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImpuesto2, 2));
                            this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dSubTotal, 2));
                        }
                        else
                        {
                            this.txtTotalFactur.Text = "$ " + "0,00";
                            this.txtImpuesto1.Text = "$ " + "0,00";
                            this.txtImpuesto2.Text = "$ " + "0,00";
                            this.txtSubTotal.Text = "$ " + "0,00";

                            string actualizar = "BASICO=(Cast(replace('" + "0,000" + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + "0,000" + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + "0,000" + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,2)))";
                            connGeneric.ActualizaGeneric("NotaPedido", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND IDNotaPedido = " + idNroNotaPedidoInterno + "");
                            MostrarItemsDatos2(idNroNotaPedidoInterno);
                            MostrarDatos();
                            LimpiarDetalleNotaPedido();
                        }
                }
            }
            catch { conn.DesconectarBD(); MostrarItemsDatos(); }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                string actualizar = "NRONOTAPEDIDO='" + txtNroNPedido.Text.Trim() + "', FECHANOTA='" + dtpFechaFactu.Text.Trim() + "', SUCURSAL='" + cboNroSucursal.Text.Trim() + "', IDCLIENTE=" + txtCodCliente.Text.Trim() + ", IDVENDEDOR=" + txtCodVendedor.Text.Trim() + ", OBSERVACIONES='" + txtObservacionNotaPedido.Text.Trim() + "'";

                if (connGeneric.ActualizaGeneric("NotaPedido", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND IDNotaPedido = " + Convert.ToInt32(txtNroInternoNota.Text) + ""))
                {
                    MostrarDatos();
                    MostrarItemsDatos2(Convert.ToInt32(txtNroInternoNota.Text));
                    MessageBox.Show("La información de la factura ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("No se ha podido actualizar los datos de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { MessageBox.Show("Error: No se ha podido actualizar la información de la factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboBuscaNotaPedido.SelectedIndex == 0)
                {
                    BuscarDatos1();
                }

                else if (cboBuscaNotaPedido.SelectedIndex == 1)
                {
                    BuscarDatos2();
                }

                else if (cboBuscaNotaPedido.SelectedIndex == 2)
                {
                    BuscarDatos3();
                }
            }
            catch { }
        }

        public void BuscarDatos1()
        {
            try
            {
                lvwNotaPedido.Items.Clear();
                lvwDetalleNotaPedido.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT NotaPedido.*, Cliente.* FROM NotaPedido, Cliente WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND NotaPedido.IDCLIENTE = Cliente.IDCLIENTE AND NotaPedido.Sucursal ='"+ sPtoVta +"' AND NotaPedido.NRONOTAPEDIDO = '" + txtBuscarArticulo.Text + "'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwNotaPedido.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IdNotaPedido"].ToString());
                    item.SubItems.Add(dr["NRONOTAPEDIDO"].ToString());
                    item.SubItems.Add(dr["FechaNota"].ToString());
                    item.SubItems.Add(dr["IdCliente"].ToString());
                    item.SubItems.Add(dr["RazonSocial"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Basico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Descuento"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto1"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto2"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());
                    item.SubItems.Add(dr["IdVendedor"].ToString());

                    /*if (Convert.ToDateTime(item.SubItems[1].Text).AddDays(2) <= DateTime.Today)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;*/

                    if (dr["SITUACION"].ToString() == "CERRADA")
                    {
                        item.ImageIndex = 7;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    else if (dr["SITUACION"].ToString() == "COMPLETADA")
                    {
                        item.ImageIndex = 0;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    else if (dr["SITUACION"].ToString() == "PENDIENTE")
                    {
                        item.ImageIndex = 2;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkOrange, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    else if (dr["SITUACION"].ToString() == "INCOMPLETA")
                    {
                        item.ImageIndex = 1;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    else
                    {  //CANCELADA
                        item.ImageIndex = 6;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    item.UseItemStyleForSubItems = false;
                    lvwNotaPedido.Items.Add(item);
                }
                cm.Connection.Close();

            }
            catch { }
        }

        public void BuscarDatos2()
        {
            try
            {
                lvwNotaPedido.Items.Clear();
                lvwDetalleNotaPedido.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT NotaPedido.*, Cliente.* FROM NotaPedido, Cliente WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND NotaPedido.IDCLIENTE = Cliente.IDCLIENTE AND NotaPedido.Sucursal ='" + sPtoVta + "' AND NotaPedido.fechanota = '" + txtBuscarArticulo.Text + "'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwNotaPedido.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IdNotaPedido"].ToString());
                    item.SubItems.Add(dr["NRONOTAPEDIDO"].ToString());
                    item.SubItems.Add(dr["FechaNota"].ToString());
                    item.SubItems.Add(dr["IdCliente"].ToString());
                    item.SubItems.Add(dr["RazonSocial"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Basico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Descuento"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto1"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto2"]), 2).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());
                    item.SubItems.Add(dr["IdVendedor"].ToString());

                    /*if (Convert.ToDateTime(item.SubItems[1].Text).AddDays(2) <= DateTime.Today)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;*/

                    if (dr["SITUACION"].ToString() == "CERRADA")
                    {
                        item.ImageIndex = 7;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    else if (dr["SITUACION"].ToString() == "COMPLETADA")
                    {
                        item.ImageIndex = 0;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    else if (dr["SITUACION"].ToString() == "PENDIENTE")
                    {
                        item.ImageIndex = 2;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkOrange, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    else if (dr["SITUACION"].ToString() == "INCOMPLETA")
                    {
                        item.ImageIndex = 1;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    else
                    {  //CANCELADA
                        item.ImageIndex = 6;
                        item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    }
                    item.UseItemStyleForSubItems = false;
                    lvwNotaPedido.Items.Add(item);

                }
                cm.Connection.Close();

            }
            catch { MessageBox.Show("Error: El formato de fecha correcto es DD/MM/AAAA.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void BuscarDatos3()
        {
            try
            {
                lvwNotaPedido.Items.Clear();
                lvwDetalleNotaPedido.Items.Clear();
                if (cboBuscaNotaPedido.SelectedIndex == 2 && txtBuscarArticulo.Text != "")
                {
                    SqlCommand cm = new SqlCommand("SELECT NotaPedido.*, Cliente.* FROM NotaPedido, Cliente WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND NotaPedido.Sucursal ='" + sPtoVta + "' AND NotaPedido.IDCLIENTE = Cliente.IDCLIENTE AND Cliente.RAZONSOCIAL LIKE '%" + txtBuscarArticulo.Text + "%'", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwNotaPedido.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["IdNotaPedido"].ToString());
                        item.SubItems.Add(dr["NRONOTAPEDIDO"].ToString());
                        item.SubItems.Add(dr["FechaNota"].ToString());
                        item.SubItems.Add(dr["IdCliente"].ToString());
                        item.SubItems.Add(dr["RazonSocial"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Basico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                        item.SubItems.Add(dr["Descuento"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto1"]), 2).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto2"]), 2).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                        item.SubItems.Add(dr["Observaciones"].ToString());
                        item.SubItems.Add(dr["IdVendedor"].ToString());

                        /*if (Convert.ToDateTime(item.SubItems[1].Text).AddDays(2) <= DateTime.Today)
                            item.ImageIndex = 1;
                        else
                            item.ImageIndex = 0;*/

                        if (dr["SITUACION"].ToString() == "CERRADA")
                        {
                            item.ImageIndex = 7;
                            item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                        }
                        else if (dr["SITUACION"].ToString() == "COMPLETADA")
                        {
                            item.ImageIndex = 0;
                            item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                        }
                        else if (dr["SITUACION"].ToString() == "PENDIENTE")
                        {
                            item.ImageIndex = 2;
                            item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkOrange, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                        }
                        else if (dr["SITUACION"].ToString() == "INCOMPLETA")
                        {
                            item.ImageIndex = 1;
                            item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                        }
                        else
                        {  //CANCELADA
                            item.ImageIndex = 6;
                            item.SubItems.Add(dr["SITUACION"].ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                        }
                        item.UseItemStyleForSubItems = false;
                        lvwNotaPedido.Items.Add(item);
                    }
                    cm.Connection.Close();
                }
            }
            catch { }
        }
        /////////////////////////////////////////////////////FIN BLOQUE DE BUSQUEDA/////////////////////////////////////////////////////////////////
        
        private void btnListaPrecioCliente_Click(object sender, EventArgs e)
        {
            frmListaPrecioVenta formListaPrecioVenta = new frmListaPrecioVenta();
            formListaPrecioVenta.pasarListaVendeCod1 += new frmListaPrecioVenta.pasarListaVendeCod(CodLista);  //Delegado1 
            formListaPrecioVenta.pasarListaVendeDesc1 += new frmListaPrecioVenta.pasarListaVendeDes(DescLista); //Delegado2
            formListaPrecioVenta.ShowDialog();
        }

        //Metodos de delegado Proveedor
        public void CodLista(int dato1)
        {
            this.txtCodListaCliente.Text = dato1.ToString();
        }

        public void DescLista(string dato2)
        {
            this.cboListaCliente.Text = dato2.ToString();
        }
        /// //////////////////////////////////////////////////////////

        private void txtCodListaCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodListaCliente.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("SELECT * FROM ListaPrecios WHERE ListaPrecios.IdListaPrecio = " + Convert.ToInt32(this.txtCodListaCliente.Text) + "", "ListaPrecios");

                    this.cboListaCliente.DataSource = conn.ds.Tables[0];
                    this.cboListaCliente.ValueMember = "IdListaPrecio";
                    this.cboListaCliente.DisplayMember = "Descripcion";
                }
                else
                    this.cboListaCliente.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboListaCliente.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void btnCerrarDetalle_Click(object sender, EventArgs e)
        {
            gpoDetalleNota.Visible = false;
            gpNotaPedido.Height = 590;
            lvwNotaPedido.Height = 560;
            btnCerrarDetalle.Enabled = false;
            btnActicarDetallesNP.Visible = true;
        }

        private void ActualizaEstadoDetalleNotaPedido(int cantidadRestante, int cantPendiente, int cantActual, int cantAentregar, int cantPedida, string EstadoRemito, string sSituacion, int idDetalleNotaPedido, int idNotaPedido)
        {
            try
            {
                int i = 0;
                int[] iCantRest = new int[25];
                int[] icantParaEntregar = new int[25];
                string[] sCantRemitido = new string[25];
                int[] iStockPedido = new int[25];

                int icantPendientes = 0;
                int icantNoPendientes = 0;
                int icantRemitida = 0;
                int icantNoRemitida = 0;
                int icantTotaParaEntrega = 0;
                int ifaltante = 0;
                int iItemsCount = 0;
                int iEstadoStock = 0;


                ///EVALUA CANTIDAD DE ARTICULO REMITIDO EN LA NOTA DE PEDIDO PENDIENTE
                int iCantRemitida = 0;

                SqlCommand cm2 = new SqlCommand("SELECT Remito.NROREMITOINTERNO, DetalleRemito.IDDETALLEREMITO, DetalleRemito.Cantidad FROM Remito, DetalleRemito WHERE Remito.NROREMITOINTERNO = DetalleRemito.NROREMITOINTERNO AND Remito.IdNotaPedido = " + idNotaPedido + "  AND DetalleRemito.IdDetalleNotaPedido = " + idDetalleNotaPedido + "", conectaEstado);


                SqlDataAdapter da2 = new SqlDataAdapter(cm2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                foreach (DataRow dr2 in dt2.Rows)
                    iCantRemitida = iCantRemitida + Convert.ToInt32(dr2["Cantidad"].ToString());

                ///ACTUALIZA CANTIDAD A ENTREGAR EN FUNCION A LA REPOSICION DEL STOCK INGRESADA
                if ((cantidadRestante <= 0) && (cantAentregar == 0) && (cantActual > 0) && (EstadoRemito == "No"))
                {
                    string actualizar = "CantidadAentrega = " + (-cantidadRestante) + ", CantidadRestante = " + (cantPedida + cantidadRestante) + "";
                    conn.ActualizaGeneric("DetalleNotaPedido", actualizar, "IdDetalleNotaPedido = " + idDetalleNotaPedido + "");
                }

                else if ((cantidadRestante <= 0) && (cantAentregar >= 0) && (cantPendiente < cantPedida) && (EstadoRemito == "No"))
                {
                    string actualizar = "CantidadAentrega = " + (cantPedida - cantPendiente) + ", CantidadRestante = " + (-cantPendiente) + "";
                    conn.ActualizaGeneric("DetalleNotaPedido", actualizar, "IdDetalleNotaPedido = " + idDetalleNotaPedido + "");
                }

                if ((cantActual > cantAentregar) && (iCantRemitida > 0) && (EstadoRemito == "No"))
                {
                    string actualizar = "CantidadAentrega = " + ((cantPedida - cantPendiente) - iCantRemitida) + ", CantidadRestante = " + (-cantPendiente) + "";
                    conn.ActualizaGeneric("DetalleNotaPedido", actualizar, "IdDetalleNotaPedido = " + idDetalleNotaPedido + "");
                }
                ///////////////////////////////////////////////////////////////////////////////////////////

                ////EVALUA NOTA DE PEDIDO Y LE COLOCA EL ESTADO DE ACUERDO AL STOCK////
                SqlCommand cm = new SqlCommand("SELECT * FROM NotaPedido, DetalleNotaPedido, Articulo WHERE  NotaPedido.IDEMPRESA = " + IDEMPRESA + " AND NotaPedido.IDNOTAPEDIDO = DetalleNotaPedido.IdNotaPedido AND NOTAPEDIDO.IdNotaPedido = " + idNotaPedido + " AND Articulo.IDARTICULO = DetalleNotaPedido.IDARTICULO AND DetalleNotaPedido.Remitido = 'No'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    iCantRest[i] = Convert.ToInt32(dr["CANTIDADRESTANTE"].ToString());
                    sCantRemitido[i] = dr["REMITIDO"].ToString();
                    icantParaEntregar[i] = Convert.ToInt32(dr["CantidadAEntrega"].ToString());
                    iStockPedido[i] = Convert.ToInt32(dr["CANT_ACTUAL"].ToString());

                    if (iCantRest[i] == 0)
                        iCantRest[i] = 0;
                    else if (iCantRest[i] > 0)
                        ifaltante = 1;
                    else
                        ifaltante = -1;

                    if (icantParaEntregar[i] > 0)
                        icantTotaParaEntrega = icantTotaParaEntrega + 1;

                    ///VERIFICA EL ESTADO DE STOCK GENERAL PARA VER SI TENGO ALGO PARA ENVIAR
                    if (iStockPedido[i] > 0)
                        iEstadoStock = iEstadoStock + 1;
                    ///

                    i++;
                }

                for (int j = 0; j <= (dt.Rows.Count - 1); j++)
                {
                    iItemsCount = dt.Rows.Count;

                    if (iCantRest[j] == 0)
                        icantNoPendientes = icantNoPendientes + 1;

                    else if (iCantRest[j] != 0)
                        icantPendientes = icantPendientes + 1;

                    if (sCantRemitido[j] == "Si")
                        icantRemitida = icantRemitida + 1;

                    else if (sCantRemitido[j] == "No")
                        icantNoRemitida = icantNoRemitida + 1;
                }

                if (sSituacion != "COMPLETADA")
                {

                    if ((icantRemitida == iItemsCount))
                    {
                        string actualizarNota1 = "SITUACION='COMPLETADA'";
                        connGeneric.ActualizaGeneric("NotaPedido", actualizarNota1, " IDEMPRESA = " + IDEMPRESA + " AND IdNotaPedido = " + idNotaPedido + "");
                    }

                    else if ((icantRemitida != iItemsCount) && (icantTotaParaEntrega >= 0) && (iEstadoStock > 0))
                    {
                        string actualizarNota2 = "SITUACION='PENDIENTE'";
                        connGeneric.ActualizaGeneric("NotaPedido", actualizarNota2, " IDEMPRESA = " + IDEMPRESA + " AND IdNotaPedido = " + idNotaPedido + "");
                    }

                    else
                    {
                        string actualizarNota1 = "SITUACION='INCOMPLETA'";
                        connGeneric.ActualizaGeneric("NotaPedido", actualizarNota1, " IDEMPRESA = " + IDEMPRESA + " AND IdNotaPedido = " + idNotaPedido + "");
                    }

                }

                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();
            }
            catch { conn.DesconectarBD(); conn.DesconectarBDLeeGeneric(); MostrarItemsDatos(); }
        }

        private void btnActicarDetallesNP_Click(object sender, EventArgs e)
        {
            gpoNotaDePedido.Visible = false;
            gpNotaPedido.Width = 950;
            lvwNotaPedido.Width = 940;
            lvwDetalleNotaPedido.Height = 290;

            gpoDetalleNota.Visible = true;
            gpNotaPedido.Height = 260;
            lvwNotaPedido.Height = 235;


            btnActicarDetallesNP.Visible = false;
            btnCerrarDetalle.Visible = true;
            btnCerrarDetalle.Enabled = true;
        }

        /// INTERFAZ DEL RECIONALIZADOR DE STOCK
        private void btnCerrarRacionalizador_Click(object sender, EventArgs e)
        {
            gpbRacionalizador.Visible = false;
            gpNP.Enabled = true;
            txtCodArtic.Text = "";
            lblCantClientePedido.Text = "0";
            lblTotalPedidoArticulo.Text = "0";
            lblExistenciaArtic.Text = "0";
            txtCantTotalArticulo.Text = "0";
            txtProporcionManualPedido.Text = "";
        }

        private void tsRacionalizador_Click(object sender, EventArgs e)
        {

            gpbRacionalizador.Visible = true;
            gpNP.Enabled = false;
        }

        private void btnBuscaArtNPCliente_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("En Construccion");
            }
            catch { }
        }

        private void txtCodArtic_TextChanged(object sender, EventArgs e)
        {
            try
            {

                int iCantPedidoArticulo = 0;
                int Existencia = 0;

                lvwPedidoClientes.Items.Clear();

                char[] QuitaSimbolo = { '$', ' ' };
                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();

                if (this.txtCodArtic.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("SELECT * FROM Articulo WHERE CODIGO = '" + this.txtCodArtic.Text + "'", "Articulo");

                    this.cboDescArticulo.DataSource = conn.ds.Tables[0];
                    this.cboDescArticulo.ValueMember = "IdArticulo";
                    this.cboDescArticulo.DisplayMember = "Descripcion";
                }
                else
                    this.cboDescArticulo.Text = "";

                conn.DesconectarBD();

                if (conn.ds.Tables[0].Rows.Count < 1)
                {
                    cboDescArticulo.Text = "";
                    lblCantClientePedido.Text = "0";
                    txtCantTotalArticulo.Text = "";
                    txtProporcionManualPedido.Text = "";
                    lblExistenciaArtic.Text = "0";
                }
                else
                {
                    conn.LeeGeneric("SELECT * FROM Articulo WHERE CODIGO = '" + this.txtCodArtic.Text + "'", "Articulo");
                    txtCantTotalArticulo.Text = conn.leerGeneric["CANT_ACTUAL"].ToString();
                    conn.DesconectarBDLeeGeneric();
                }

                SqlCommand cm = new SqlCommand("SELECT NotaPedido.FechaNota, DetalleNotaPedido.IDDETALLENOTAPEDIDO, Articulo.Codigo, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL, Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, NotaPedido.IdNotaPedido, NotaPedido.Situacion,  DetalleNotaPedido.IdDetalleNotaPedido, DetalleNotaPedido.IDArticulo, DetalleNotaPedido.CANTIDADPEDIDA, DetalleNotaPedido.CANTIDADAENTREGA, DetalleNotaPedido.PRECUNITARIO, DetalleNotaPedido.CANTIDADRESTANTE, DetalleNotaPedido.REASIGNADO, DetalleNotaPedido.Remitido FROM NotaPedido, DetalleNotaPedido, Articulo, Cliente, Vendedor WHERE  NotaPedido.IDEMPRESA = " + IDEMPRESA + " AND DetalleNotaPedido.IDARTICULO = Articulo.IDARTICULO AND NotaPedido.IDCLIENTE = Cliente.IDCLIENTE AND NotaPedido.IDVENDEDOR = Vendedor.IDVENDEDOR AND DetalleNotaPedido.IDNOTAPEDIDO = NotaPedido.IdNotaPedido AND Remitido = 'No' AND Articulo.CODIGO = '" + this.txtCodArtic.Text + "'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwPedidoClientes.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDDETALLENOTAPEDIDO"].ToString());
                    item.SubItems.Add(dr["IDCLIENTE"].ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString());
                    item.SubItems.Add(dr["FECHANOTA"].ToString());
                    item.SubItems.Add(dr["CANTIDADPEDIDA"].ToString(), Color.Black, Color.DarkGray, new System.Drawing.Font(
                    "Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                    item.SubItems.Add(dr["CANTIDADAENTREGA"].ToString(), Color.Black, Color.DarkGray, new System.Drawing.Font(
                    "Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                    //item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.Black, Color.DarkGray, new System.Drawing.Font(
                    //"Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                    item.SubItems.Add("0", Color.Black, Color.DarkGray, new System.Drawing.Font(
                    "Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                    lblExistenciaArtic.Text = dr["CANT_ACTUAL"].ToString();
                    Existencia = Convert.ToInt32(lblExistenciaArtic.Text);

                    item.ImageIndex = 21;

                    item.UseItemStyleForSubItems = false;
                    lvwPedidoClientes.Items.Add(item);

                    iCantPedidoArticulo = iCantPedidoArticulo + Convert.ToInt32(dr["CANTIDADPEDIDA"]);

                }
                cm.Connection.Close();

                lblTotalPedidoArticulo.Text = iCantPedidoArticulo.ToString();

                if (Existencia < iCantPedidoArticulo)
                    lblExistenciaArtic.ForeColor = Color.Red;
                else
                    lblExistenciaArtic.ForeColor = Color.Green;
            }
            catch
            {
                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();
            }
        }

        private void lvwPedidoClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(lvwPedidoClientes.SelectedItems[0].SubItems[5].Text) >= 0)
                    txtProporcionManualPedido.Text = lvwPedidoClientes.SelectedItems[0].SubItems[5].Text;
                if (Convert.ToInt32(lvwPedidoClientes.SelectedItems[0].SubItems[6].Text) >= 0)
                    txtProporcionManualPedido.Text = lvwPedidoClientes.SelectedItems[0].SubItems[6].Text;

                lblCantClientePedido.Text = this.txtProporcionManualPedido.Text;
            }
            catch { }
        }

        private void btnAgregaCantidad_Click(object sender, EventArgs e)
        {
            try
            {
                int iAgregaCant = 0, iCantArt = 0;
                int iTotal = 0;

                if (Convert.ToInt32(lvwPedidoClientes.SelectedItems[0].SubItems[5].Text) > 0)
                {

                    iCantArt = Convert.ToInt32(txtProporcionManualPedido.Text);

                    if ((iCantArt + iAgregaCant) <= iCantArt)
                        iAgregaCant = iAgregaCant + 1;
                    else
                        iAgregaCant = iAgregaCant - 1;

                    iTotal = iCantArt + iAgregaCant;
                    txtProporcionManualPedido.Text = iTotal.ToString();

                    lvwPedidoClientes.SelectedItems[0].SubItems[6].Text = txtProporcionManualPedido.Text;

                    lvwPedidoClientes.SelectedItems[0].SubItems[5].Text = (Convert.ToInt32(lvwPedidoClientes.SelectedItems[0].SubItems[5].Text) - 1).ToString();
                }
            }
            catch { }
        }

        private void btnQuitaCantidad_Click(object sender, EventArgs e)
        {
            try
            {
                int iagregaCant = 0;

                if (Convert.ToInt32(lvwPedidoClientes.SelectedItems[0].SubItems[6].Text) > 0)
                {
                    iagregaCant = Convert.ToInt32(txtProporcionManualPedido.Text);

                    //                    if (iagregaCant == 0)
                    //                        iagregaCant = 0;
                    //                    else
                    iagregaCant = iagregaCant - 1;

                    txtProporcionManualPedido.Text = iagregaCant.ToString();

                    lvwPedidoClientes.SelectedItems[0].SubItems[5].Text = (Convert.ToInt32(lvwPedidoClientes.SelectedItems[0].SubItems[5].Text) + 1).ToString();
                    lvwPedidoClientes.SelectedItems[0].SubItems[6].Text = txtProporcionManualPedido.Text;
                }
            }
            catch { }
        }

        private void Remito_Click(object sender, EventArgs e)
        {

        }

        private void txtBuscarArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboNroSucursal.Focus();
            }
        }

        private void cboBuscaNotaPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtBuscarArticulo.Focus();
            }
        }

        private void cboNroSucursal_KeyPress(object sender, KeyPressEventArgs e)
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
                txtCodListaCliente.Focus();
            }
        }

        private void txtCodListaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboListaCliente.Focus();
            }
        }

        private void cboListaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtObservacionNotaPedido.Focus();
            }
        }

        private void txtObservacionNotaPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodigoArticulo.Focus();
            }
        }

        private void txtCodigoArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboArticulo.Focus();
            }
        }

        private void cboArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnBuscaArticulo.Focus();
            }
        }

        private void txtCantPedida_KeyPress(object sender, KeyPressEventArgs e)
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
                txtCodigoArticulo.Focus();
            }
        }

        private void Referencias_Click(object sender, EventArgs e)
        {
            gbLeyenda.Visible = true;
        }

        private void btnCerrarLeyenda_Click(object sender, EventArgs e)
        {
            gbLeyenda.Visible = false;
        }

        private void txtProporcionManualPedido_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtProporcionManualPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show("Utilizar los botones de nivel para adicionar o restar cantidad.", "Reasignador", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {

        }

        private void tsRemito_Click(object sender, EventArgs e)
        {
            try
            {
                //ActualizaSaldo(Convert.ToInt32(lvwNotaPedido.SelectedItems[0].SubItems[0].Text));

                if (lvwNotaPedido.SelectedItems.Count == 0)
                    MessageBox.Show("Error: No se ha seleccionado ningún N.P.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    //ValidaRemito();
                    if (ValidaNotaPedidoSeleccionada(Convert.ToInt32(lvwNotaPedido.SelectedItems[0].SubItems[0].Text.Trim())) == true)
                    {
                        frmRemito frmRemito = new frmRemito();
                        frmRemito.IdentidicadorNotaPedido = idNroNotaPedidoInterno.ToString().Trim();
                        frmRemito.IdentidicadorCliente = Convert.ToInt32(lvwNotaPedido.SelectedItems[0].SubItems[4].Text.Trim());
                        //this.Close();
                        frmRemito.ShowDialog();

                        MostrarItemsDatos();
                        MostrarDatos();
                    }
                    else
                        MessageBox.Show("Se debe remitir las notas de pedido en orden correlativo por antiguedad y pendientes de entrega", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                MostrarItemsDatos();
                MostrarDatos();
            }
        }

        private bool ValidaNotaPedidoSeleccionada(int iNotaSeleccionada)
        {
            //timer1.Enabled = true;
            long lNotaMinima;

            conn.LeeGeneric("SELECT MIN(IDNOTAPEDIDO) as 'NNotaMinima' FROM NotaPedido WHERE IDEMPRESA = " + IDEMPRESA + " AND SITUACION = 'PENDIENTE'", "NotaPedido");
            lNotaMinima = Convert.ToInt32(conn.leerGeneric["NNotaMinima"].ToString());

            if (iNotaSeleccionada == lNotaMinima)
                return true;
            else
                return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //MostrarItemsDatos();
            //MostrarDatos();
        }

        private void tsBtnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwNotaPedido.Items.Count == 0)
                    MessageBox.Show("Error: No existen datos para mostrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    nroNotaPedido = Convert.ToInt32(lvwNotaPedido.SelectedItems[0].SubItems[0].Text);
                    //nroRemitoInt = Convert.ToInt32(lvwNotaPedido.SelectedItems[0].SubItems[0].Text);

                    DGestion.Reportes.frmRPTNotaPedido frmRptNotaPedido = new DGestion.Reportes.frmRPTNotaPedido();
                    frmRptNotaPedido.ShowDialog();
                }
            }
            catch //(System.Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                MessageBox.Show("Error: No se ha seleccionado el comprobante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizaSaldo(int idNotaPedidoInterno)
        {
            try
            {
                if (lvwDetalleNotaPedido.Items.Count != 0)
                {
                    string subTotalfactu;
                    string iva105Factu;
                    string iva21Factu;
                    string importeFactu;

                    string ImporteArtuculo;
                                    
                    SqlCommand cm = new SqlCommand("Select * FROM ListaPrecioArt WHERE IDARTICULO = " + IdArticulo + "", conectaEstado);
                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        // if (nrocomprobante == dr["NROREMITO"].ToString().Trim())

                    }

                    cm.Connection.Close();                        

                    ImporteArtuculo = connGeneric.leerGeneric["Precio"].ToString();


                    connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleNotaPedido WHERE IDNOTAPEDIDO = " + idNroNotaPedidoInterno + "", "DetalleNotaPedido");


                    importeFactu = connGeneric.leerGeneric["Importe"].ToString();
                    iva105Factu = connGeneric.leerGeneric["Iva105"].ToString();
                    iva21Factu = connGeneric.leerGeneric["Iva21"].ToString();
                    subTotalfactu = connGeneric.leerGeneric["SubTotal"].ToString();

                    string actualizar = "BASICO=(Cast(replace('" + subTotalfactu + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + iva105Factu + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + iva21Factu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeFactu + "', ',', '.') as decimal(10,2)))";
                    if (connGeneric.ActualizaGeneric("NotaPedido", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND IDNOTAPEDIDO = " + idNroNotaPedidoInterno + " AND Situacion = 'Pendiente' AND Situacion = 'Incompleta'"))
                    {
                        MostrarDatos();
                        MostrarItemsDatos2(idNroNotaPedidoInterno);
                    }
                }
            }
            catch {
                connGeneric.DesconectarBD();
            }
        }

        private void tsbtnReporteGenerico_Click(object sender, EventArgs e)
        {

        }
    } 

}