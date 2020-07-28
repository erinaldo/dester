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
    public partial class frmRemito : Form
    {
        public delegate void pasarCodRemito1(int CodRemito);
        public event pasarCodRemito1 pasarCodRemito;
        public delegate void pasarCodCliente1(int CodCliente);
        public event pasarCodCliente1 pasarCodCliente;

        EmpresaBD connEmpresa = new EmpresaBD();

        public string IdentidicadorNotaPedido;
        public int IdentidicadorCliente;

        public static string nroRemito;
        public static int nroRemitoInt;

        public static int OptRemito = 0;

        int IDEMPRESA;
        string sPtoVta;
        int PagAv = 7000, PagRe = 1;

        private void conPermi()
        {
            try
            {
                string sUsuarioLegueado;
                string control;
                sUsuarioLegueado = frmPrincipal.Usuario;

                SqlCommand cm = new SqlCommand("SELECT Personal.USUARIO, Personal.NOMBREYAPELLIDO, PermisoModulo.Idmodulo, PermisoControl.IdControl, PermisoControl.Control, EstadoSistema.Descripcion, PermisoUsuario.IdPersonal FROM Personal, PermisoModulo, PermisoControl, PermisoUsuario, EstadoSistema WHERE PermisoUsuario.IdPersonal = Personal.IdPersonal AND PermisoUsuario.IdModulo = PermisoModulo.IdModulo AND PermisoUsuario.IdControl = PermisoControl.IdControl AND PermisoUsuario.IdEstadoSistema = EstadoSistema.IdEstado AND PermisoUsuario.IdModulo = 3 AND Personal.USUARIO = '" + sUsuarioLegueado + "' ORDER BY IdControl", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                //control = dt.Rows[0]["Control"].ToString().Trim();

                if ((dt.Rows[0]["Descripcion"].ToString().Trim() == "Inactivo") && (dt.Rows[0]["Control"].ToString().Trim() == "Actualizar Remito"))
                {
                    btnModificar.Enabled = true;
                    tsBtnModificar.Enabled = true;
                }
                else
                {
                    btnModificar.Enabled = false;
                    tsBtnModificar.Enabled = false;
                }

                if (dt.Rows[1]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[1]["Control"].ToString().Trim() == "Eliminar Remito")
                    btnEliminar.Enabled = true;
                else
                    btnEliminar.Enabled = false;

                if (dt.Rows[2]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[2]["Control"].ToString().Trim() == "Agregar Item de Remito")
                    btnEliminar.Enabled = true;
                else
                    btnEliminar.Enabled = false;

                if (dt.Rows[3]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[3]["Control"].ToString().Trim() == "Eliminar Item de Remito")
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

        public frmRemito() {
            InitializeComponent();
        }

        //defines la propiedad        
        CGenericBD connGeneric = new CGenericBD();
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
        DateTime fechaRemito;

        int contadorNROfactNuevo;
        bool nuevoRemito = false;
        int idNroRemitoInterno;

        int indiceLvwNotaPedido;
        int idArtuculo;

        double porcGeneralLista = 0;
        double procenFleteLista = 0;
        double CostoEnLista = 0;

        double ValorUnitarioArticulo;

        int iCodigoListaPrecioCliente;
        int nroRemitoInterCreado = 0;

        private string FormateoFecha() {
            DateTimePicker dtr = new DateTimePicker();
            dtr.Value = DateTime.Now;
            return String.Format("{0:d/M/yyyy HH:mm:ss}", dtr.Value);
        }

        private double CalculoPorcentajeListaVenta(double valorArticulo) {
            try {

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
                else {
                    valorMasArticuloPorcFlete = Math.Round(((valorArticulo * procenFleteLista) / 100),3);
                    valorTotalArticulo = Math.Round((valorMasArticuloPorcFlete + valorArticulo),3);
                }

                if (porcGeneralLista < 1)
                    valorTotalArticulo = Math.Round((valorArticulo),3);
                else {
                    valorMasArticuloPorcVenta = Math.Round(((valorTotalArticulo * porcGeneralLista) / 100),3);
                    valorTotalArticulo = Math.Round((valorMasArticuloPorcVenta + valorTotalArticulo),3);
                }

                return Math.Round((valorTotalArticulo),3);
            }
            catch { return 0; }
        }

        private void GuardaItemsDatos(bool status, int nroRemitoInter) {
            try {
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

                int CantActualArticulo;
                int Cant_Pedida;
                //int Cant_Restante;

                //string Observaciones;

                porcGeneralLista = 0;
                procenFleteLista = 0;
                CostoEnLista = 0;

                if (txtCantArticulo.Text.Trim() != "") {
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

                    Cant_Pedida = Convert.ToInt32(txtCantArticulo.Text.Trim());
                    //Cant_Restante = Convert.ToInt32(txtCantRestante.Text.Trim());

                    SubTotal = Math.Round((Cant_Pedida * CalculoPorcentajeListaVenta(CostoEnLista)), 3);
                    Importe = Math.Round((CalculoPorcentajeListaVenta(CostoEnLista) * Cant_Pedida), 2);
                    ////////////////////////////////////// //////////////////////////////////////

                    lvwDetalleRemito.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(IdArticulo.ToString());
                    item.SubItems.Add(Codigo.ToString());                                                           ///ITEM 0
                    item.SubItems.Add(Descripcion.ToString());                                                      ///ITEM 1
                    item.SubItems.Add(Cant_Pedida.ToString());
                    item.SubItems.Add("$ " + Math.Round(CalculoPorcentajeListaVenta(CostoEnLista), 3).ToString());  ///ITEM 2

                    item.SubItems.Add("$ " + Math.Round(Importe, 2).ToString());                                    ///ITEM 3
                    item.SubItems.Add("0");                                                                         ///ITEM 4

                    if (IdImpuesto == 2 && cboTipoRemito.Text.Trim() != "N" && txtIva.Text.Trim() != "Exento") {
                        Impuesto1 = Math.Round(((SubTotal * 1.105) - SubTotal), 2);
                        Neto = Math.Round((Importe + Impuesto1), 3);
                        item.SubItems.Add("$ " + Math.Round(Impuesto1, 2).ToString());                              ///ITEM 5A
                        item.SubItems.Add("$ " + Math.Round(Neto, 2).ToString());                                   ///ITEM 6B

                        sumaImpuesto1 += Impuesto1;
                        txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto1, 2).ToString();

                        sumaNetos += Neto;
                        txtTotalFactur.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                    }

                    else if (IdImpuesto == 1 && cboTipoRemito.Text.Trim() != "N" && txtIva.Text.Trim() != "Exento") {
                        Impuesto2 = Math.Round(((SubTotal * 1.21) - SubTotal), 2);
                        Neto = Math.Round((Importe + Impuesto2), 3);
                        item.SubItems.Add("$ " + Math.Round(Impuesto2, 2).ToString());                              ///ITEM 5B
                        item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());                                   ///ITEM 6B    

                        sumaImpuesto2 += Impuesto2;
                        txtImpuesto2.Text = "$ " + Math.Round(sumaImpuesto2, 2).ToString();

                        sumaNetos += Neto;
                        txtTotalFactur.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                    }
                    
                    else if (IdImpuesto == 3 || cboTipoRemito.Text.Trim() == "N" || txtIva.Text.Trim() == "Exento") {
                        Impuesto1 = Math.Round(((SubTotal * 1) - SubTotal), 2);
                        Neto = Math.Round((Importe + Impuesto1), 3);
                        item.SubItems.Add("$ " + Math.Round(Impuesto1, 2).ToString());                              ///ITEM 5A
                        item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());                                   ///ITEM 6B

                        sumaImpuesto1 += Impuesto1;
                        txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto1, 2).ToString();

                        sumaNetos += Neto;
                        txtTotalFactur.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                    }

                    sumaTotales += SubTotal;
                    txtSubTotal.Text = "$ " + Math.Round(sumaTotales, 3).ToString();
                    
                    item.SubItems.Add(CantActualArticulo.ToString());                                               ///ITEM 7
                    item.SubItems.Add("0");     //Observaciones                                                     ///ITEM 8

                    //item.SubItems.Add("-");
                    //item.SubItems.Add("0");  //colocar el IDNotaPedido                                            ///ITEM 9

                    if (IdImpuesto != 2)
                        Impuesto1 = 0;
                    if (IdImpuesto != 1)
                        Impuesto2 = 0;
                    
                    item.SubItems.Add(Convert.ToDecimal(Math.Round(Impuesto1, 2)).ToString());                     ///ITEM 10
                    item.SubItems.Add(Convert.ToDecimal(Math.Round(Impuesto2, 2)).ToString());                     ///ITEM 11
                    
                    /*Cant_Restante = CantActualArticulo - Cant_Pedida;
                    if (Cant_Restante < 0)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;*/                    
                        
                    item.ImageIndex = 1;
                    lvwDetalleRemito.Items.Add(item);
                    
                    //Normalizacion de Saldos totales
                    if (lvwDetalleRemito.Items.Count != 0) {
                        dSubTotal = 0.000;
                        dImpuesto1 = 0.000;
                        dImpuesto2 = 0.000;
                        dImporteTotal = 0.00;

                        char[] QuitaSimbolo = { '$', ' ' };
                        for (int i = 0; i < (lvwDetalleRemito.Items.Count); i++) {
                            dImporteTotal += Math.Round(Convert.ToSingle(lvwDetalleRemito.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo)), 2);
                            dImpuesto1 += Math.Round(Convert.ToSingle(lvwDetalleRemito.Items[i].SubItems[11].Text.TrimStart(QuitaSimbolo)), 2);
                            dImpuesto2 += Math.Round(Convert.ToSingle(lvwDetalleRemito.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo)), 2);
                            dSubTotal += Math.Round(Convert.ToSingle(lvwDetalleRemito.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo)), 3);
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

                    if (status == false) {
                        if (fechaRemito.AddDays(180) <= DateTime.Today)
                            MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else {
                            txtNroIntRemito.Text = idNroRemitoInterno.ToString();
                            idNroRemitoInterno = nroRemitoInter;

                            connGeneric.EliminarGeneric("DetalleRemito", " NROREMITOINTERNO = " + nroRemitoInter);
                            //connGeneric.DesconectarBD();
                            char[] QuitaSimbolo = { '$', ' ' };
                            for (int i = 0; i < (lvwDetalleRemito.Items.Count); i++) {
                                string agregarItem = "INSERT INTO DetalleRemito(IDARTICULO, CANTIDAD, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, NROREMITOINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleRemito.Items[i].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), '0','0', (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[11].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))), " + nroRemitoInter + ")";

                                if (conn.InsertarGeneric(agregarItem)) {
                                    connGeneric.DesconectarBD();
                                    connGeneric.DesconectarBDLeeGeneric();
                                }

                                //////////////////////////////////////////////////////////ACTUALIZA STOCK///////////////////////////////////////////////////////////
                                 int iTotalStock;
                                 iTotalStock = CantActualArticulo - Convert.ToInt32(txtCantArticulo.Text);

                                 string actualizaStock = "CANT_ACTUAL=(Cast(replace(" + iTotalStock + ", ',', '.') as decimal(10,0)))";
                                 if (connGeneric.ActualizaGeneric("Articulo", actualizaStock, " IDARTICULO= " + IdArticulo + "")) {
                                     connGeneric.DesconectarBD();
                                     connGeneric.DesconectarBDLeeGeneric();
                                 }
                                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            }
                            //MostrarDatos();
                            MostrarItemsDatos2(nroRemitoInter);
                        }
                    }

                    else if (status == true) {

                        int nroremInt;
                        nroremInt = UltimoRemito();

                        char[] QuitaSimbolo = { '$', ' ' };
                        for (int i = 0; i < (lvwDetalleRemito.Items.Count); i++)
                        {
                            //string agregarItem = "INSERT INTO DetalleRemito(IDARTICULO, CANTIDAD, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, NROREMITOINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleRemito.Items[i].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,0))), (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), '0','0', (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[11].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))), " + Convert.ToInt32(lvwRemito.Items[lvwRemito.Items.Count - 1].SubItems[0].Text) + ")";
                            string agregarItem = "INSERT INTO DetalleRemito(IDARTICULO, CANTIDAD, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, NROREMITOINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleRemito.Items[i].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,0))), (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), '0','0', (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[11].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleRemito.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))), " + nroremInt + ")";

                            //nroRemitoInter = Convert.ToInt32(lvwRemito.Items[lvwRemito.Items.Count - 1].SubItems[0].Text);
                            nroRemitoInter = nroremInt;


                            if (conn.InsertarGeneric(agregarItem))
                            {
                                connGeneric.DesconectarBD();
                                connGeneric.DesconectarBDLeeGeneric();
                            }

                            //////////////////////////////////////////////////////////ACTUALIZA STOCK///////////////////////////////////////////////////////////
                            int iTotalStock;
                            iTotalStock = CantActualArticulo - Convert.ToInt32(txtCantArticulo.Text);

                            string actualizaStock = "CANT_ACTUAL=(Cast(replace(" + iTotalStock + ", ',', '.') as decimal(10,0)))";
                            if (connGeneric.ActualizaGeneric("Articulo", actualizaStock, " IDARTICULO= " + IdArticulo + ""))
                            {
                                connGeneric.DesconectarBD();
                                connGeneric.DesconectarBDLeeGeneric();
                            }
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
                        }
                        //MessageBox.Show("Item Actualizado/Agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);                           
                    }

                    //////////////////////////////////////////////// ACTUALIZA EL AGREGADO DE DATOS ////////////////////////////////////////////////
                    connGeneric.DesconectarBDLeeGeneric();
                    connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleRemito WHERE NROREMITOINTERNO = " + nroRemitoInter + "", "DetalleRemito");

                    subTotalfactu = Convert.ToSingle(connGeneric.leerGeneric["SubTotal"].ToString());
                    iva105Factu = Convert.ToSingle(connGeneric.leerGeneric["Iva105"].ToString());
                    iva21Factu = Convert.ToSingle(connGeneric.leerGeneric["Iva21"].ToString());
                    importeFactu = Convert.ToSingle(connGeneric.leerGeneric["Importe"].ToString());


                    string actualizar = "BASICO=(Cast(replace('" + subTotalfactu + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + iva105Factu + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + iva21Factu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeFactu + "', ',', '.') as decimal(10,2)))";

                    this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeFactu, 2));
                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)iva105Factu, 2));
                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)iva21Factu, 2));
                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)subTotalfactu, 2));

                    if (connGeneric.ActualizaGeneric("Remito", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NROREMITOINTERNO = " + nroRemitoInter + "")) {

                        MostrarDatos2(nroRemitoInter);                        
                        MostrarItemsDatos2(nroRemitoInter);

                        // MessageBox.Show("La información de la factura ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se ha podido actualizar los datos del remito.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else
                    MessageBox.Show("Error al Agregar Artículo: No se ha ingresado artículo o cantidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { MessageBox.Show("Error: Falta algun tipo de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void MostrarDatos2(int nroremito)
        {
            try
            {
                this.lvwRemito.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM Remito, Cliente WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND Remito.Sucursal='" + sPtoVta + "' AND Remito.IDCLIENTE=Cliente.IDCLIENTE AND NROREMITOINTERNO = "+ nroremito + " ORDER BY NROREMITO", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwRemito.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NROREMITOINTERNO"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["NROREMITO"].ToString());
                    item.SubItems.Add(dr["Fecha"].ToString());
                    item.SubItems.Add(dr["IdCliente"].ToString());
                    item.SubItems.Add(dr["RazonSocial"].ToString());
                    item.SubItems.Add(dr["ESTADOREMITO"].ToString());



                    if (dr["ESTADOREMITO"].ToString() == "NO FACTURADO")
                        item.ImageIndex = 3;
                    else if (dr["ESTADOREMITO"].ToString() == "FACTURADO")
                        item.ImageIndex = 5;

                    else if (dr["ESTADOREMITO"].ToString() == "ANULADO")
                        item.ImageIndex = 2;



                    item.UseItemStyleForSubItems = false;
                    lvwRemito.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }


        private int UltimoRemito()
        {
            int nroremitoint=0;

            connGeneric.LeeGeneric("SELECT MAX(NROREMITOINTERNO) as NROINTERNO FROM REMITO WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = " + sPtoVta + " ORDER BY NROINTERNO", "REMITO");

            if (connGeneric.leerGeneric["NROINTERNO"].ToString() == "")
                return 0;
            else
            {
                nroremitoint = Convert.ToInt32(connGeneric.leerGeneric["NROINTERNO"].ToString());               
                //nroremitoint = nroremitoint + 1;
                return nroremitoint;
            }
        }

        private void MostrarItemsDatos2(int NRONotaPedidoInterno) {
            try {
                lvwDetalleRemito.Items.Clear();

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                double iva105;
                double iva21;
                //int iCantPedida, iCantActual, iCantRestante;
                //int CantPendiente;

                connGeneric.LeeGeneric("SELECT Remito.NROREMITOINTERNO, DetalleRemito.IDDETALLEREMITO, Articulo.DESCRIPCION as 'Artículo', Articulo.CANT_ACTUAL, DetalleRemito.CANTPENDIENTE, DetalleRemito.CANTIDAD, DetalleRemito.PRECUNITARIO as 'Precio Unitario', DetalleRemito.IMPORTE as 'Importe', DetalleRemito.DESCUENTO as 'Descuento', DetalleRemito.PORCDESC as '% Desc', DetalleRemito.SUBTOTAL as 'Subtotal', DetalleRemito.IMPUESTO1 as 'Iva 10,5', DetalleRemito.IMPUESTO2 as 'Iva 21', DetalleRemito.OBSERVACIONES as 'Observaciones' FROM Remito, DetalleRemito, Articulo, Cliente, Personal WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND DetalleRemito.IDARTICULO = Articulo.IDARTICULO AND Remito.IDCLIENTE = Cliente.IDCLIENTE AND Remito.IDPERSONAL = Personal.IDPERSONAL AND DetalleRemito.NROREMITOINTERNO = Remito.NROREMITOINTERNO AND Remito.NROREMITOINTERNO = " + NRONotaPedidoInterno + "", "NotaPedido");

                iva105 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 10,5"].ToString());
                iva21 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 21"].ToString());

                SqlCommand cm = new SqlCommand("SELECT Articulo.Codigo, Remito.NROREMITOINTERNO, DetalleRemito.NROREMITOINTERNO, DetalleRemito.IDDETALLEREMITO, DetalleRemito.IDArticulo, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL, DetalleRemito.CANTPENDIENTE, DetalleRemito.CANTIDAD, DetalleRemito.PRECUNITARIO, DetalleRemito.IMPORTE, DetalleRemito.DESCUENTO, DetalleRemito.PORCDESC, DetalleRemito.SUBTOTAL, DetalleRemito.IMPUESTO1 as 'Iva 10,5', DetalleRemito.IMPUESTO2 as 'Iva 21', DetalleRemito.OBSERVACIONES FROM Remito, DetalleRemito, Articulo, Cliente, Personal WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND DetalleRemito.IDARTICULO = Articulo.IDARTICULO AND Remito.IDCLIENTE = Cliente.IDCLIENTE AND Remito.IDPERSONAL = Personal.IDPERSONAL AND DetalleRemito.NROREMITOINTERNO = Remito.NROREMITOINTERNO AND Remito.NROREMITOINTERNO = " + NRONotaPedidoInterno + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows) {
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
                    //   item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.Empty, Color.LightGray, null);
                    //   item.SubItems.Add(dr["CANTIDADRESTANTE"].ToString(), Color.Empty, Color.LightGray, null);


                    ///EVALUA SITUACION STOCK y PEDIDOS///
                    /* iCantPedida = Convert.ToInt32(dr["CANTIDADPEDIDA"].ToString());
                     iCantActual = Convert.ToInt32(dr["CANT_ACTUAL"].ToString());
                     iCantRestante = iCantActual - iCantPedida;

                     CantPendiente = EvaluaCantPendiente(iCantPedida, iCantRestante, iCantActual);

                     ActualizaEstadoDetalleNotaPedido(iCantRestante, CantPendiente, iCantActual, dr["Situacion"].ToString(), Convert.ToInt32(dr["IdDetalleNotaPedido"].ToString()), Convert.ToInt32(dr["IdNotaPedido"].ToString()));

                     if (iCantRestante < 0 && iCantActual > 1)
                         item.ImageIndex = 2;
                     else if (iCantRestante < 0 && iCantActual < 1)
                         item.ImageIndex = 1;
                     else
                         item.ImageIndex = 2;*/
                    //////////////////////////////////////////////////////////////


                    item.SubItems.Add(dr["OBSERVACIONES"].ToString());
                    item.SubItems.Add(dr["IDDETALLEREMITO"].ToString());
                    item.SubItems.Add(dr["Iva 10,5"].ToString());
                    item.SubItems.Add(dr["Iva 21"].ToString());

                    /* if (CantPendiente > 0)
                     {
                         item.SubItems.Add(CantPendiente.ToString(), Color.Red, Color.LightSalmon, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                         if (dr["Remitido"].ToString() == "No" && iCantActual < 1)
                             item.SubItems.Add("S/E", Color.LightSalmon, Color.Red, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
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
                     }*/

                    item.ImageIndex = 1;

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleRemito.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void btnCliente_Click(object sender, EventArgs e) {
            frmCliente formCliente = new frmCliente();
            formCliente.pasarClienteCod += new frmCliente.pasarClienteCod1(CodClient);  //Delegado1 
            formCliente.pasarClientRS += new frmCliente.pasarClienteRS(RazonS); //Delegado2

            txtCodTipoRemito.Focus();

            formCliente.ShowDialog();
        }

        public void CodClient(int CodCliente) {
            this.txtCodCliente.Text = CodCliente.ToString();
        }

        public void RazonS(string RSCliente) {
            this.cboCliente.Text = RSCliente.ToString();
        }

        private void btnPersonal_Click(object sender, EventArgs e) {
            frmPersonal frmPerso = new frmPersonal();
            frmPerso.pasadoPerso1 += new frmPersonal.pasarPersona1(CodPPerso);  //Delegado11 Rubro Articulo
            frmPerso.pasadoPerso2 += new frmPersonal.pasarPersona2(RSPerso); //Delegado2 Rubro Articulo
            txtObservacionRemito.Focus();
            frmPerso.ShowDialog();
        }

        public void CodPPerso(int CodPersonal) {
            this.txtCodPersonal.Text = CodPersonal.ToString();
        }

        public void RSPerso(string DescPersonal) {
            this.cboPersonal.Text = DescPersonal.ToString();
        }

        private void btnFormaPago_Click(object sender, EventArgs e) {
            frmFormaPago frmFPago = new frmFormaPago();
            frmFPago.pasarFPCod += new frmFormaPago.pasarFormaPagoCod1(CodFormaPago);  //Delegado11 Rubro Articulo
            frmFPago.pasarFPN += new frmFormaPago.pasarFormaPagoRS(DesFormaPago); //Delegado2 Rubro Articulo
            txtCodPersonal.Focus();
            frmFPago.ShowDialog();
        }

        public void CodFormaPago(int CodFP) {
            this.txtCodFormaPago.Text = CodFP.ToString();
        }

        public void DesFormaPago(string NTR) {
            this.cboFormaPago.Text = NTR.ToString();
        }

        private void btnTipoRemito_Click(object sender, EventArgs e) {
            frmTipoRemito frmTRemito = new frmTipoRemito();
            frmTRemito.pasarTRCod += new frmTipoRemito.pasarTipoRemitoCod1(pasarTRCod);  //Delegado1 
            frmTRemito.pasarTRN += new frmTipoRemito.pasarTipoRemitoRS(NTR); //Delegado2
            txtCodFormaPago.Focus();
            frmTRemito.ShowDialog();
        }

        public void pasarTRCod(int CodTipoRemito) {
            this.txtCodTipoRemito.Text = CodTipoRemito.ToString();
        }

        public void NTR(string NTR) {
            this.cboTipoRemito.Text = NTR.ToString();
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnAgregaArt_Click(object sender, EventArgs e) {
            if (fechaRemito.AddDays(180) <= DateTime.Today)            
                MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);            
            else
            {
                if (lvwDetalleRemito.Items.Count >= 25)
                    MessageBox.Show("Límite de cantidad de items por remito excedida. Se deberá crear un nuevo remito para continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else {                    

                    //timer1.Enabled = true;
                    GuardarTodosLosDatos();
                    txtCodigoArticulo.Text = "";
                    cboArticulo.Text = "";
                    //txtCantPedida.Text = "";
                    //txtProcDesc.Text = "";
                    txtPrecio.Text = "";
                    txtCodigoArticulo.Focus();
                }
            }
        }

        private void GuardarTodosLosDatos() {
            try {
                float subTotal;
                float impuesto1; float impuesto2;
                float descuento; float importeTotal;

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

                if (nuevoRemito == true)
                    connGeneric.ConsultaGeneric("Select * FROM Remito WHERE IDEMPRESA = " + IDEMPRESA + " AND NROREMITOINTERNO = " + Convert.ToInt32(txtNroIntRemito.Text) + "", "Remito");
                else
                    connGeneric.ConsultaGeneric("Select * FROM Remito WHERE IDEMPRESA = " + IDEMPRESA + " AND NROREMITOINTERNO = " + idNroRemitoInterno + "", "Remito");
               if (connGeneric.ds.Tables[0].Rows.Count == 0) {
                   string agregar = "INSERT INTO Remito(NROREMITO, SUCURSAL, IDTIPOREMITO, FECHA, IDCLIENTE, IDPERSONAL, IDFORMAPAGO, BASICO, PORCDESC, DESCUENTO, IMPUESTO1, IMPUESTO2, TOTAL, OBSERVACIONES, IDEMPRESA) VALUES ('" + txtNroRemito.Text.Trim() + "', '" + cboNroSucursal.Text.Trim() + "', " + txtCodTipoRemito.Text.Trim() + ", '" + FormateoFecha() + "', " + txtCodCliente.Text.Trim() + ", " + txtCodPersonal.Text.Trim() + ", " + txtCodFormaPago.Text.Trim() + ", (Cast(replace('" + Math.Round(subTotal, 3) + "', ',', '.') as decimal(10,3))), " + 0 + ", " + descuento + " , (Cast(replace('" + Math.Round(impuesto1, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(impuesto2, 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(importeTotal, 2) + "', ',', '.') as decimal(10,2))), '" + txtObservacionRemito.Text + "', " + IDEMPRESA + ")";

                    this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeTotal, 2));
                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto1, 2));
                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto2, 2));
                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)subTotal, 2));

                    if (conn.InsertarGeneric(agregar)) {
                        MostrarDatos(PagRe,PagAv);
                        GuardaItemsDatos(true, 0);

                        //lvwRemito.Items[lvwRemito.Items.Count - 1].Selected = true;
                        //txtNroIntRemito.Text = lvwRemito.Items[lvwRemito.Items.Count - 1].Text;
                        txtNroIntRemito.Text = UltimoRemito().ToString();
                        idNroRemitoInterno = UltimoRemito();
                        //idNroRemitoInterno = Convert.ToInt32(lvwRemito.Items[lvwRemito.Items.Count - 1].Text);
                    }
                    else
                        MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    GuardaItemsDatos(false, idNroRemitoInterno);

            }
            catch { conn.DesconectarBD(); connGeneric.DesconectarBD(); connGeneric.DesconectarBDLeeGeneric(); }
        }

        private void frmRemito_Load(object sender, EventArgs e) {

            conPermi();

            gpoEncabezadoRemito.Visible = false;
            gpRemitoCliente.Width = 1030;
            lvwRemito.Width = 1005;
            lvwDetalleRemito.Height = 250;

            lblCodArt.Visible = false;
            txtCodigoArticulo.Visible = false;
            cboArticulo.Visible = false;
            btnAgregaArt.Visible = false;
            btnQuitaArt.Visible = false;
            btnAgregaArt.Visible = false;
            lblCantidad.Visible = false;
            txtCantArticulo.Visible = false;
            btnBuscaArticulo.Visible = false;
            lblDescuento.Visible = false;
            lblPrecio.Visible = false;
            txtPrecio.Visible = false;
            txtProcDesc.Visible = false;

            dtpFechaRemito.Value = DateTime.Today;
            fechaRemito = DateTime.Today;

            conn.ConectarBD();
            cboBuscaRemito.SelectedIndex = 0;
            //cboNroSucursal.SelectedIndex = 0;
            cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();

            FormatoListView();
            IDEMPRESA = ConsultaEmpresa(); //Lee Empresa
            MostrarDatos(0,7000);

            if (IdentidicadorNotaPedido != null)
            {
                this.txtCodCliente.Text = IdentidicadorCliente.ToString();
                if (this.txtCodTipoRemito.Text.Trim() == "")
                    txtCodTipoRemito.Text = "1";
                if (this.txtCodFormaPago.Text.Trim() == "")
                    txtCodFormaPago.Text = "1";
                if (this.txtCodPersonal.Text.Trim() == "")
                    txtCodPersonal.Text = "1";
                pasarCodNotaPedido(Convert.ToInt32(IdentidicadorNotaPedido));   //Llamada a la funcion dee armado de remito
            }
        }

        public void MostrarDatos(int iPaginaAv, int iPaginaRe) {
            try {
                this.lvwRemito.Items.Clear();

                tsTextPag.Text = "RE " + iPaginaAv + "-" + iPaginaRe;

               SqlCommand cm = new SqlCommand("SELECT * FROM Remito, Cliente WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND Remito.Sucursal='"+ sPtoVta + "' AND Remito.IDCLIENTE=Cliente.IDCLIENTE AND Remito.NROREMITO BETWEEN " + iPaginaAv + " AND " + iPaginaRe + " ORDER BY NROREMITO", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows) {
                    lvwRemito.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NROREMITOINTERNO"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["NROREMITO"].ToString());
                    item.SubItems.Add(dr["Fecha"].ToString());
                    item.SubItems.Add(dr["IdCliente"].ToString());
                    item.SubItems.Add(dr["RazonSocial"].ToString());
                    item.SubItems.Add(dr["ESTADOREMITO"].ToString());
                    //item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Basico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    //item.SubItems.Add(dr["Descuento"].ToString());
                    //item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto1"]), 3).ToString());
                    //item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Impuesto2"]), 3).ToString());
                    //item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    //item.SubItems.Add(dr["Observaciones"].ToString());
                    //item.SubItems.Add(dr["IdVendedor"].ToString());

                    if (dr["ESTADOREMITO"].ToString() == "NO FACTURADO")
                        item.ImageIndex = 3;
                    else if (dr["ESTADOREMITO"].ToString() == "FACTURADO")
                        item.ImageIndex = 5;
                    else if (dr["ESTADOREMITO"].ToString() == "ANULADO")
                        item.ImageIndex = 2;

                    /*if (Convert.ToDateTime(item.SubItems[1].Text).AddDays(2) <= DateTime.Today)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;*/

                    /*      if (dr["SITUACION"].ToString() == "CERRADA")
                          {
                              item.ImageIndex = 7;
                              item.SubItems.Add(dr["SITUACION"].ToString(), Color.LightGreen, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                          }
                          else if (dr["SITUACION"].ToString() == "COMPLETADA")
                          {
                              item.ImageIndex = 0;
                              item.SubItems.Add(dr["SITUACION"].ToString(), Color.LightGreen, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                          }
                          else if (dr["SITUACION"].ToString() == "PENDIENTE")
                          {
                              item.ImageIndex = 2;
                              item.SubItems.Add(dr["SITUACION"].ToString(), Color.LightYellow, Color.DarkGoldenrod, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                          }
                          else if (dr["SITUACION"].ToString() == "INCOMPLETA")
                          {
                              item.ImageIndex = 1;
                              item.SubItems.Add(dr["SITUACION"].ToString(), Color.LightPink, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                          }
                          else
                          {  //CANCELADA
                              item.ImageIndex = 6;
                              item.SubItems.Add(dr["SITUACION"].ToString(), Color.LightPink, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                          }*/
                    //item.ImageIndex = 0;

                    item.UseItemStyleForSubItems = false;
                    lvwRemito.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void FormatoListView() {
            try {
                this.lvwRemito.View = View.Details;
                this.lvwRemito.LabelEdit = false;
                this.lvwRemito.AllowColumnReorder = false;
                this.lvwRemito.FullRowSelect = true;
                this.lvwRemito.GridLines = true;

                this.lvwDetalleRemito.View = View.Details;
                this.lvwDetalleRemito.LabelEdit = false;
                this.lvwDetalleRemito.AllowColumnReorder = false;
                this.lvwDetalleRemito.FullRowSelect = true;
                this.lvwDetalleRemito.GridLines = true;
            }
            catch { }
        }

        private void NuevoRemito()
        {
            try
            {
                MostrarDatos(0,7000);

                nuevoRemito = true;
                //timer1.Enabled = true;
                //btnCerrarDetalle.Enabled = false;
                //cboNroSucursal.Focus();

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

                dtpFechaRemito.Value = DateTime.Today;
                fechaRemito = DateTime.Today;

                lvwDetalleRemito.Items.Clear();
                gpoEncabezadoRemito.Visible = true;
                gpRemitoCliente.Width = 310;
                gpDetalleRemito.Visible = true;
                gpRemitoCliente.Height = 310;


                //gpoEncabezadoRemito.Visible = false;
                gpRemitoCliente.Width = 270;
                lvwRemito.Height = 280;
                lvwRemito.Width = 245;
                lvwDetalleRemito.Height = 220;

                cboBuscaRemito.SelectedIndex = 0;
                cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();

                lblCodArt.Visible = true;
                txtCodigoArticulo.Visible = true;
                cboArticulo.Visible = true;
                btnAgregaArt.Visible = true;
                btnQuitaArt.Visible = true;
                btnAgregaArt.Visible = true;
                lblCantidad.Visible = true;
                txtCantArticulo.Visible = true;
                btnBuscaArticulo.Visible = true;
                lblDescuento.Visible = true;
                lblPrecio.Visible = true;
                txtPrecio.Visible = true;
                txtProcDesc.Visible = true;

                tsBtnNuevoRemito.Enabled = true;

                lblPrecio.Visible = true;
                txtPrecio.Visible = true;
                //lblDescuento.Visible = true;
                //txtProcDesc.Visible = true;

                this.txtCantArticulo.Text = "";
                this.txtNroIntRemito.Text = "0";
                cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();
                this.txtObservacionRemito.Text = "";
                this.txtIva.Text = "";
                this.txtCodigoArticulo.Text = "";
                this.txtCodCliente.Text = "";
                this.txtCodPersonal.Text = "";
                this.txtCodTipoRemito.Text = "";
                this.txtCodFormaPago.Text = "";
                this.txtCuit.Text = "";
                this.txtDescuento.Text = "$ 0,000";
                this.txtSubTotal.Text = "$ 0,000";
                this.txtImpuesto1.Text = "$ 0,000";
                this.txtImpuesto2.Text = "$ 0,000";
                this.txtTotalFactur.Text = "$ 0,00";

                LimpiarDetalleRemito();
                Limpieza();

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                /////////////////////////////////////////// AUTONUMERICO NRO INTERNO //////////////////////////////////////////
                connGeneric.LeeGeneric("SELECT MAX(NROREMITO) as NRO FROM REMITO WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = " + sPtoVta + " ORDER BY NRO", "REMITO");

                if (connGeneric.leerGeneric["NRO"].ToString() == "")
                {
                    txtNroIntRemito.Text = "0";
                    txtNroRemito.Text = "0";
                }
                else
                {
                    //txtNroIntRemito.Text = connGeneric.leerGeneric["NRO"].ToString();
                    txtNroRemito.Text = connGeneric.leerGeneric["NRO"].ToString();
                }

                contadorNROfactNuevo = (Convert.ToInt32(txtNroRemito.Text));
                contadorNROfactNuevo = contadorNROfactNuevo + 1;

                //txtNroIntRemito.Text = contadorNROfactNuevo.ToString();
                //txtNroIntRemito.Text = this.txtNroIntRemito.Text;
                //this.txtNroRemito.Text = this.cboNroSucursal.Text.Trim() + "-" + this.txtNroIntRemito.Text;
                                
                txtNroRemito.Text = Convert.ToString(contadorNROfactNuevo);

                ValidaNumerador(this.txtNroRemito.Text.Trim());

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            catch { MessageBox.Show("Error: El nro ingresado no es numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            NuevoRemito();
        }

        private bool ValidaNumerador(string nrocomprobante)
        {
            try
            {
                SqlCommand cm = new SqlCommand("SELECT NROREMITO FROM REMITO WHERE Remito.IDEMPRESA = " + IDEMPRESA + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    if (nrocomprobante == dr["NROREMITO"].ToString().Trim())
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

        private string NumeradorRemito()
        {
            /////////////////////////////////////////// AUTONUMERICO NRO INTERNO //////////////////////////////////////////
            connGeneric.LeeGeneric("SELECT MAX(NROREMITO) as NRO FROM REMITO WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = "+ sPtoVta +" ORDER BY NRO", "REMITO");

            if (connGeneric.leerGeneric["NRO"].ToString() == "")
                txtNroIntRemito.Text = "0";
            else
                txtNroIntRemito.Text = connGeneric.leerGeneric["NRO"].ToString().Trim();

            contadorNROfactNuevo = (Convert.ToInt32(txtNroIntRemito.Text));
            contadorNROfactNuevo = contadorNROfactNuevo + 1;
            txtNroIntRemito.Text = contadorNROfactNuevo.ToString();

            //txtNroIntRemito.Text = this.txtNroIntRemito.Text;
            //this.txtNroRemito.Text = this.cboNroSucursal.Text.Trim() + "-" + this.txtNroIntRemito.Text;
            this.txtNroRemito.Text = this.txtNroIntRemito.Text;

            connGeneric.DesconectarBD();
            connGeneric.DesconectarBDLeeGeneric();

            return this.txtNroRemito.Text;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void Limpieza() {
            txtCodCliente.Text = "";
            cboCliente.Text = "";
            txtCodigoArticulo.Text = "";
            cboArticulo.Text = "";
            txtCodPersonal.Text = "";
            cboPersonal.Text = "";
            txtCantArticulo.Text = "";
            //txtCantPedida.Text = "";
            //txtCantRestante.Text = "";
            
            
        }

        private void LimpiarDetalleRemito() {
            this.txtCantArticulo.Text = "";
            this.txtCodigoArticulo.Text = "";
            //this.txtCantRestante.Text = "";
            //this.txtCantPedida.Text = "";
            this.txtPrecio.Text = "";
            //this.cboListaCliente.Text = "";
            //this.txtCodListaCliente.Text = "";
        }

        private void btnCerrar_Click(object sender, EventArgs e) {

            try
            {

                gpoEncabezadoRemito.Visible = false;
                gpRemitoCliente.Width = 1030;
                lvwRemito.Width = 1005;
                lvwDetalleRemito.Height = 250;

                lblCodArt.Visible = false;
                txtCodigoArticulo.Visible = false;
                cboArticulo.Visible = false;
                btnAgregaArt.Visible = false;
                btnQuitaArt.Visible = false;
                btnAgregaArt.Visible = false;
                lblCantidad.Visible = false;
                txtCantArticulo.Visible = false;
                btnBuscaArticulo.Visible = false;
                lblDescuento.Visible = false;
                lblPrecio.Visible = false;
                txtPrecio.Visible = false;
                txtProcDesc.Visible = false;

                btnBuscaArticulo.Visible = false;
                //lblDescuento.Visible = false;
                //txtProcDesc.Visible = false;

                //Limpieza();
                MostrarDatos2(Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[0].Text));

            }
            catch { }
        }

        private void tsBtnModificar_Click(object sender, EventArgs e) {
            nuevoRemito = false;
            //timer1.Enabled = false;
            //btnCerrarDetalle.Enabled = false;
            cboNroSucursal.Focus();

            if (lvwRemito.SelectedItems.Count == 0)
                MessageBox.Show("Error: No se ha seleccionado ningún remito", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else {
                //lvwDetalleRemito.Items.Clear();
                gpoEncabezadoRemito.Visible = true;
                gpRemitoCliente.Width = 310;
                gpDetalleRemito.Visible = true;
                gpRemitoCliente.Height = 310;

                gpRemitoCliente.Width = 270;
                lvwRemito.Height = 280;
                lvwRemito.Width = 245;
                lvwDetalleRemito.Height = 220;

                cboBuscaRemito.SelectedIndex = 0;
                //cboNroSucursal.SelectedIndex = 0;
                cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();

                lblCodArt.Visible = true;
                txtCodigoArticulo.Visible = true;
                cboArticulo.Visible = true;
                btnAgregaArt.Visible = true;
                btnQuitaArt.Visible = true;
                btnAgregaArt.Visible = true;
                lblCantidad.Visible = true;
                txtCantArticulo.Visible = true;
                btnBuscaArticulo.Visible = true;
                lblDescuento.Visible = true;
                lblPrecio.Visible = true;
                txtPrecio.Visible = true;
                txtProcDesc.Visible = true;

                tsBtnNuevoRemito.Enabled = true;

                lblPrecio.Visible = true;
                txtPrecio.Visible = true;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e) {
            try
            {
                string sEstadoRemito = "";
                string actualizar="";
                connGeneric.LeeGeneric("Select EstadoRemito FROM Remito WHERE NRORemitoInterno = " + Convert.ToInt32(txtNroIntRemito.Text) + "", "DetalleRemito");
                sEstadoRemito = connGeneric.leerGeneric["EstadoRemito"].ToString().Trim();

                if (sEstadoRemito == "NO FACTURADO")
                     actualizar = "NROREMITO='" + txtNroRemito.Text.Trim() + "', SUCURSAL='" + cboNroSucursal.Text.Trim() + "', FECHA='" + dtpFechaRemito.Text.Trim() + "', IDTIPOREMITO=" + txtCodTipoRemito.Text.Trim() + " , IDFORMAPAGO=" + txtCodFormaPago.Text.Trim() + ", IDCLIENTE="+ txtCodCliente.Text +", IDPERSONAL=" + txtCodPersonal.Text.Trim() + ", OBSERVACIONES='" + txtObservacionRemito.Text.Trim() + "'";
                else                
                     actualizar = "IDPERSONAL=" + txtCodPersonal.Text.Trim() + ", OBSERVACIONES='" + txtObservacionRemito.Text.Trim() + "'";                

                if (connGeneric.ActualizaGeneric("Remito", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NRORemitoInterno = " + Convert.ToInt32(txtNroIntRemito.Text) + ""))
                {
                    MostrarDatos2(Convert.ToInt32(this.txtNroIntRemito.Text));
                    MostrarItemsDatos2(Convert.ToInt32(txtNroIntRemito.Text));
                    MessageBox.Show("La información del remito ha sido actualizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //MessageBox.Show("Cliente y situación del remito permanecera sin cambio porque el remito ya está facturado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("No se ha podido actualizar los datos del remito.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch { MessageBox.Show("Error: No se ha podido actualizar la información del remito", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnEliminar_Click(object sender, EventArgs e) {
            try {
                tsBtnNuevoRemito.Enabled = true;
                //btnGuardar.Enabled = false;
                //timer1.Enabled = false;

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                string EstadoRemito;
                EstadoRemito = LeeEstadoRemito(Convert.ToInt32(this.txtNroIntRemito.Text));


                if (fechaRemito.AddDays(180) <= DateTime.Today || EstadoRemito == "ANULADO")
                    MessageBox.Show("No se puede modificar una factura de fecha pasada o comprobante anulado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else {
                    if (connGeneric.EliminarGeneric("Remito", " IDEMPRESA = " + IDEMPRESA + " AND NROREMITOINTERNO = " + Convert.ToInt32(this.txtNroIntRemito.Text))) {
                        MostrarDatos2(Convert.ToInt32(this.txtNroIntRemito.Text));

                        tsBtnNuevoRemito.Enabled = true;
                        //btnGuardar.Enabled = true;

                        this.txtCantArticulo.Text = "";
                        this.txtNroIntRemito.Text = "";
                        this.txtNroRemito.Text = "";
                        this.txtObservacionRemito.Text = "";
                        //this.cboNroSucursal.SelectedIndex = 0;
                        cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();
                        this.txtIva.Text = "";
                        this.txtCodigoArticulo.Text = "";
                        this.txtCodPersonal.Text = "";
                        this.txtCodCliente.Text = "";
                        this.txtCodTipoRemito.Text = "";
                        this.txtCuit.Text = "";
                        this.txtDescuento.Text = "$ 0.00";
                        this.txtSubTotal.Text = "$ 0.00";
                        this.txtImpuesto1.Text = "$ 0.00";
                        this.txtImpuesto2.Text = "$ 0.00";
                        this.txtTotalFactur.Text = "$ 0.00";
                        MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //timer1.Enabled = true;

                        actualizaStock_Anulacion_o_eliminacion(Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[0].Text));
                    }
                    else
                        MessageBox.Show("Error al Eliminar. No se han eliminado los items de factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { MessageBox.Show("Error: Seleccione el remito a eliminar y verificar que no existan items en el detalle.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnQuitaArt_Click(object sender, EventArgs e) {
            try {
                int iIndex = 0;

                if (fechaRemito.AddDays(180) <= DateTime.Today)
                    MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else {
                    //timer1.Enabled = false;

                    actualizaStock_Anulacion_o_eliminacion(idNroRemitoInterno, lvwDetalleRemito.SelectedItems[0].SubItems[1].Text.Trim());
                    iIndex = Convert.ToInt32(lvwDetalleRemito.SelectedItems[0].SubItems[10].Text);  //Elemento de la base de datos
                    lvwDetalleRemito.Items[lvwDetalleRemito.SelectedItems[0].Index].Remove(); //Elemento del listview

                    if (connArt.EliminarArticulo("DetalleRemito", " IdDetalleRemito = " + iIndex))
                        //MostrarItemsDatos2(idNROFACTUINTERNO);

                        if (lvwDetalleRemito.Items.Count != 0) {
                            if (iIndex != 0) {
                                string subTotalfactu;
                                string iva105Factu;
                                string iva21Factu;
                                string importeFactu;

                                connGeneric.DesconectarBDLeeGeneric();
                                connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleRemito WHERE NRORemitoINTERNO = " + idNroRemitoInterno + "", "DetalleRemito");

                                importeFactu = connGeneric.leerGeneric["Importe"].ToString();
                                iva105Factu = connGeneric.leerGeneric["Iva105"].ToString();
                                iva21Factu = connGeneric.leerGeneric["Iva21"].ToString();
                                subTotalfactu = connGeneric.leerGeneric["SubTotal"].ToString();

                                string actualizar = "BASICO=(Cast(replace('" + subTotalfactu + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + iva105Factu + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + iva21Factu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeFactu + "', ',', '.') as decimal(10,2)))";

                                this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(importeFactu), 2));
                                this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(iva105Factu), 2));
                                this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(iva21Factu), 2));
                                this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(subTotalfactu), 2));

                                if (connGeneric.ActualizaGeneric("Remito", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NRORemitoInterno = " + idNroRemitoInterno + "")) {
                                    MostrarDatos2(idNroRemitoInterno);
                                    MostrarItemsDatos2(idNroRemitoInterno);
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

                            for (int i = 0; i < (lvwDetalleRemito.Items.Count); i++)
                            {
                                dSubTotal += Math.Round(Convert.ToSingle(lvwDetalleRemito.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo)), 3);
                                dImpuesto1 += Math.Round(Convert.ToSingle(lvwDetalleRemito.Items[i].SubItems[11].Text.TrimStart(QuitaSimbolo)), 2);
                                dImpuesto2 += Math.Round(Convert.ToSingle(lvwDetalleRemito.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo)), 2);
                                dImporteTotal += Math.Round(Convert.ToSingle(lvwDetalleRemito.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo)), 2);
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

                            string actualizar = "BASICO=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,3))), IMPUESTOS1=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,3))), IMPUESTOS2 =(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,2)))";
                            connGeneric.ActualizaGeneric("Remito", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NRORemitoInterno = " + idNroRemitoInterno + "");
                            MostrarDatos2(idNroRemitoInterno);
                            MostrarItemsDatos2(idNroRemitoInterno);
                        }
                }
            }
            catch { conn.DesconectarBD(); MostrarItemsDatos(); }
        }

        private void btnBuscaArticulo_Click(object sender, EventArgs e)
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
            this.txtCodigoArticulo.Text = dato1.ToString().Trim();
        }

        public void NombreArt(string dato2)
        {
            this.cboArticulo.Text = dato2.ToString().Trim();
        }
        public void CostoArt(string dato3)
        {
            char[] QuitaSimbolo1 = { '$', ' ' };
            this.txtPrecio.Text = "$ " + Math.Round(CalculoPorcentajeListaVenta(Convert.ToSingle(dato3.ToString().Trim().TrimStart(QuitaSimbolo1))), 3);
        }

        private void txtCodCliente_TextChanged(object sender, EventArgs e) {
            try {

                //Thread.Sleep(1000);
                int iTablaCant = 0;

                if (this.txtCodCliente.Text.Trim() != "") {
                    conn.ConsultaGeneric("SELECT * FROM Cliente WHERE idempresa = " + IDEMPRESA + " AND IdCliente = " + Convert.ToInt32(this.txtCodCliente.Text) + " AND NROCENTRO = '" + sPtoVta + "'", "Cliente");

                    this.cboCliente.DataSource = conn.ds.Tables[0];
                    this.cboCliente.ValueMember = "IdCliente";
                    this.cboCliente.DisplayMember = "RazonSocial";

                    iTablaCant = conn.ds.Tables[0].Rows.Count;

                    connGeneric.DesconectarBD();
                    connGeneric.DesconectarBDLeeGeneric();

                    connGeneric.LeeGeneric("SELECT Cliente.NUMDECUIT, TipoIva.DESCRIPCION as 'TipoIva', TipoIva.IdTipoIva, ListaPrecios.IDLISTAPRECIO, ListaPrecios.DESCRIPCION as 'DescLista' FROM Cliente, TipoIva, ListaPrecios WHERE Cliente.IDTIPOIVA = TipoIva.IDTIPOIVA AND Cliente.IDLISTAPRECIO=ListaPrecios.IDLISTAPRECIO AND Cliente.IDCLIENTE = " + Convert.ToInt32(this.txtCodCliente.Text) + "", "Cliente");

                    txtCuit.Text = connGeneric.leerGeneric["NUMDECUIT"].ToString();
                    txtIva.Text = connGeneric.leerGeneric["TipoIva"].ToString();

                    iCodigoListaPrecioCliente = Convert.ToInt32(connGeneric.leerGeneric["IDLISTAPRECIO"].ToString());
                    //cboListaCliente.Text = connGeneric.leerGeneric["DescLista"].ToString();
                }
                else {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    //txtCodCliente.Text = "";
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

                if (this.txtCuit.Text.Trim() == "0") {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    //txtCodCliente.Text = "";
                    //txtCodListaCliente.Text = "";
                    //cboListaCliente.Text = "";
                    MessageBox.Show("Error: Falta informacion relacionada con el Cliente (CUIT)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
            }
            catch {
                cboCliente.Text = "";
                txtCuit.Text = "";
                txtIva.Text = "";
            }
        }

        private void lvwRemito_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                //timer1.Enabled = false;
                //MostrarItemsDatos();

                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                nuevoRemito = false;

                gboRemitoFactura.Visible = false;

                idNroRemitoInterno = Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[0].Text);
                indiceLvwNotaPedido = lvwRemito.SelectedItems[0].Index;

                conn.LeeGeneric("SELECT * FROM Remito, Cliente, ListaPrecios WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDLISTAPRECIO=ListaPrecios.IDLISTAPRECIO AND Cliente.IDCLIENTE = Remito.IDCLIENTE AND NROREMITOINTERNO = " + Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[0].Text) + "", "Remito");

                //this.txtNroNotaPedido.Text = conn.leerGeneric["IDNOTAPEDIDO"].ToString();
                this.txtNroIntRemito.Text = conn.leerGeneric["NROREMITOINTERNO"].ToString();
                this.cboNroSucursal.Text = conn.leerGeneric["SUCURSAL"].ToString().Trim();
                this.txtNroRemito.Text = conn.leerGeneric["NROREMITO"].ToString().Trim();
                this.dtpFechaRemito.Value = Convert.ToDateTime(conn.leerGeneric["FECHA"].ToString());
                fechaRemito = Convert.ToDateTime(conn.leerGeneric["FECHA"].ToString());

                this.txtCodCliente.Text = conn.leerGeneric["IDCLIENTE"].ToString();
                if (this.txtCodCliente.Text.Trim() == "")
                    this.cboCliente.Text = "";

                this.txtCodPersonal.Text = conn.leerGeneric["IDPERSONAL"].ToString();
                if (this.txtCodPersonal.Text.Trim() == "")
                    this.cboPersonal.Text = "";

                this.txtCodTipoRemito.Text = conn.leerGeneric["IDTIPOREMITO"].ToString();
                if (this.txtCodTipoRemito.Text.Trim() == "")
                    this.cboTipoRemito.Text = "";

                this.txtCodFormaPago.Text = conn.leerGeneric["IDFORMAPAGO"].ToString();
                if (this.txtCodFormaPago.Text.Trim() == "")
                    this.cboFormaPago.Text = "";

                //this.txtCodListaCliente.Text = conn.leerGeneric["IDLISTAPRECIO"].ToString();
                //cboListaCliente.Text = connArt.leer["DescLista"].ToString();

                this.txtObservacionRemito.Text = conn.leerGeneric["OBSERVACIONES"].ToString();

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

        private void MostrarItemsDatos()
        {
            try
            {
                lvwDetalleRemito.Items.Clear();

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                double iva105;
                double iva21;

                //int iCantPedida, iCantActual, iCantRestante;
                //int CantPendiente;

                connGeneric.LeeGeneric("SELECT Remito.NROREMITOINTERNO, DetalleRemito.IDDETALLEREMITO as 'Código', Articulo.DESCRIPCION as 'Artículo', Articulo.CANT_ACTUAL, DetalleRemito.CANTPENDIENTE, DetalleRemito.CANTIDAD, DetalleRemito.PRECUNITARIO as 'Precio Unitario', DetalleRemito.IMPORTE as 'Importe', DetalleRemito.DESCUENTO as 'Descuento', DetalleRemito.PORCDESC as '% Desc', DetalleRemito.SUBTOTAL as 'Subtotal', DetalleRemito.IMPUESTO1 as 'Iva 10,5', DetalleRemito.IMPUESTO2 as 'Iva 21', DetalleRemito.OBSERVACIONES as 'Observaciones' FROM Remito, DetalleRemito, Articulo, Cliente, Personal WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND DetalleRemito.IDARTICULO = Articulo.IDARTICULO AND Remito.IDCLIENTE = Cliente.IDCLIENTE AND Remito.IDPERSONAL = Personal.IDPERSONAL AND DetalleRemito.NROREMITOINTERNO = Remito.NROREMITOINTERNO AND Remito.NROREMITOINTERNO = " + Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[0].Text) + "", "Remito");

                iva105 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 10,5"].ToString());
                iva21 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 21"].ToString());

                SqlCommand cm = new SqlCommand("SELECT Articulo.Codigo, Remito.NROREMITOINTERNO, DetalleRemito.NROREMITOINTERNO, DetalleRemito.IDDETALLEREMITO, DetalleRemito.IDArticulo, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL, DetalleRemito.CANTPENDIENTE, DetalleRemito.CANTIDAD, DetalleRemito.PRECUNITARIO, DetalleRemito.IMPORTE, DetalleRemito.DESCUENTO, DetalleRemito.PORCDESC, DetalleRemito.SUBTOTAL, DetalleRemito.IMPUESTO1 as 'Iva 10,5', DetalleRemito.IMPUESTO2 as 'Iva 21', DetalleRemito.OBSERVACIONES FROM Remito, DetalleRemito, Articulo, Cliente, Personal WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND DetalleRemito.IDARTICULO = Articulo.IDARTICULO AND Remito.IDCLIENTE = Cliente.IDCLIENTE AND Remito.IDPERSONAL = Personal.IDPERSONAL AND DetalleRemito.NROREMITOINTERNO = Remito.NROREMITOINTERNO AND Remito.NROREMITOINTERNO = " + Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[0].Text) + "", conectaEstado);


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
                    //item.SubItems.Add(dr["CANT_ACTUAL"].ToString(), Color.Empty, Color.LightGray, null);
                    //item.SubItems.Add(dr["CANTIDADRESTANTE"].ToString(), Color.Empty, Color.LightGray, null);


                    ///EVALUA SITUACION STOCK y PEDIDOS///
                    /* iCantPedida = Convert.ToInt32(dr["CANTIDADPEDIDA"].ToString());
                     iCantActual = Convert.ToInt32(dr["CANT_ACTUAL"].ToString());
                     iCantRestante = iCantActual - iCantPedida;

                     CantPendiente = EvaluaCantPendiente(iCantPedida, iCantRestante, iCantActual);

                     ActualizaEstadoDetalleNotaPedido(iCantRestante, CantPendiente, iCantActual, dr["Situacion"].ToString(), Convert.ToInt32(dr["IdDetalleNotaPedido"].ToString()), Convert.ToInt32(dr["IdNotaPedido"].ToString()));

                     if (iCantRestante < 0 && iCantActual > 1)
                         item.ImageIndex = 2;
                     else if (iCantRestante < 0 && iCantActual < 1)
                         item.ImageIndex = 1;
                     else
                         item.ImageIndex = 2;*/
                    //////////////////////////////////////////////////////////////


                    item.SubItems.Add(dr["OBSERVACIONES"].ToString());
                    item.SubItems.Add(dr["IDDETALLEREMITO"].ToString());
                    item.SubItems.Add(dr["Iva 10,5"].ToString());
                    item.SubItems.Add(dr["Iva 21"].ToString());

                    /* if (CantPendiente > 0)
                     {
                         item.SubItems.Add(CantPendiente.ToString(), Color.Red, Color.LightSalmon, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));

                         if (dr["Remitido"].ToString() == "No" && iCantActual < 1)
                             item.SubItems.Add("S/E", Color.LightSalmon, Color.Red, new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold));
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
                     }*/

                    item.ImageIndex = 1;

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleRemito.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void txtCodPersonal_TextChanged(object sender, EventArgs e) {
            try {
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

        private void txtCodTipoRemito_TextChanged(object sender, EventArgs e) {
            try {
                if (this.txtCodTipoRemito.Text.Trim() != "") {
                    conn.ConsultaGeneric("SELECT * FROM TipoRemito WHERE IdTipoRemito = " + Convert.ToInt32(this.txtCodTipoRemito.Text) + "", "TipoRemito");

                    this.cboTipoRemito.DataSource = conn.ds.Tables[0];
                    this.cboTipoRemito.ValueMember = "IdTipoRemito";
                    this.cboTipoRemito.DisplayMember = "Descripcion";
                }
                else
                    this.cboTipoRemito.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboTipoRemito.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void txtCodFormaPago_TextChanged(object sender, EventArgs e) {
            try {
                if (this.txtCodFormaPago.Text.Trim() != "") {
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

        private void txtCodigoArticulo_TextChanged(object sender, EventArgs e) {
            try {
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
                    cboArticulo.Text = "";
                    txtCantArticulo.Text = "";
                    txtPrecio.Text = "";
                    txtDescuento.Text = "0";
                }

                conn.DesconectarBD();

                if (conn.ds.Tables[0].Rows.Count < 1)
                {
                    cboArticulo.Text = "";
                    txtCantArticulo.Text = "";
                    txtPrecio.Text = "";
                    txtDescuento.Text = "0";
                }
                else
                {
                    conn.LeeGeneric("SELECT * FROM Articulo WHERE CODIGO = '" + this.txtCodigoArticulo.Text + "'", "Articulo");
                    //txtCantArticulo.Text = conn.leerGeneric["CANT_ACTUAL"].ToString();
                    txtCantArticulo.Text = "";
                    idArtuculo = Convert.ToInt32(conn.leerGeneric["IdArticulo"].ToString());
                            
                    txtPrecio.Text = "$ " + Math.Round(CalculoPorcentajeListaVenta(Convert.ToSingle(conn.leerGeneric["COSTOENLISTA"].ToString())), 3);

                    ValorUnitarioArticulo = Convert.ToDouble(txtPrecio.Text.TrimStart(QuitaSimbolo));

                    conn.DesconectarBDLeeGeneric();
                }
            }
            catch {
                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();
            }
        }

        private void RecalculaValores(string sPrecioUnitario, string sSubtotal, string sDesceunto, string sImpuesto1, string sImpuesto2, string sImporteTotal, int iCantidad, int iNotaPedido)
        {
            //  float subTotal;
            //  float impuesto1; float impuesto2;
            //  float descuento; float importeTotal; 


            /* this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeTotal, 2));
             this.txtImpuesto1.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)impuesto1, 3));
             this.txtImpuesto2.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)impuesto2, 3));
             this.txtSubTotal.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)subTotal, 3));*/
        }

        public void pasarCodNotaPedido(int CodNotaPedido) {
            try
            {
                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();

                bool bDetectaEntregaParcial = false;
                int iCantidadAentregar = 0;
                int iCantidadPedida = 0;
                int iIDdetalleNotaPedido = 0;
                int iCantidadRestante = 0;

                int iActualItem;

                ///Variables para recalculo de valores de entregas parciales
                double PUnitario = 0;
                double subTotal = 0;
                double impuesto1 = 0;
                double impuesto2 = 0;
                double importeTotal = 0;
                //double descuento = 0;
                int idImpuesto = 0;
                string nroRemito = "";
                //////////////////////////////

                DialogResult dialogResult = MessageBox.Show("¿Agregar un nuevo remito con todos los items pendientes en Nota de Pedido?", "Remitir Mercadería", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    nroRemito = NumeradorRemito(); //Nro Remito Nuevo
                    txtNroRemito.Text = nroRemito;

                    nuevoRemito = true;
                    conn.LeeGeneric("SELECT * FROM NotaPedido WHERE NotaPedido.IDEMPRESA = " + IDEMPRESA + " AND IDNOTAPEDIDO = " + CodNotaPedido + "", "NotaPedido");

                    if (conn.leerGeneric["Situacion"].ToString() == "COMPLETADA")
                        MessageBox.Show("Esta nota de pedido ya ha sido remitada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        if (conn.leerGeneric["Situacion"].ToString() == "INCOMPLETA")
                        MessageBox.Show("Esta nota de pedido esta incompleta y no podrá remitarse.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                    {
                        //this.dtpFechaRemito.Value = Convert.ToDateTime(conn.leerGeneric["FECHANOTA"].ToString());
                        this.cboNroSucursal.Text = conn.leerGeneric["SUCURSAL"].ToString().Trim();
                        this.txtCodCliente.Text = conn.leerGeneric["IDCLIENTE"].ToString();

                        //Recalcula valores de entrega parcial//
                        //  if (Convert.ToInt32(conn.leerGeneric["CANTIDADRESTANTE"].ToString()) <= 0 && Convert.ToInt32(conn.leerGeneric["CANTIDADAENTREGA"].ToString()) > 0)
                        //  {
                        //      RecalculaValores(conn.leerGeneric["BASICO"].ToString(), conn.leerGeneric["BASICO"].ToString(), conn.leerGeneric["BASICO"].ToString(), conn.leerGeneric["BASICO"].ToString(), conn.leerGeneric["BASICO"].ToString(), CodNotaPedido);
                        //}
                        //else
                        // {
                        txtSubTotal.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["BASICO"]), 2).ToString();
                        txtDescuento.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["DESCUENTO"]), 2).ToString();
                        txtImpuesto1.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["IMPUESTO1"]), 2).ToString();
                        txtImpuesto2.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["IMPUESTO2"]), 2).ToString();
                        txtTotalFactur.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leerGeneric["TOTAL"]), 2).ToString();
                        // }

                        /////////////////////////////////////////////////////////////GUARDA ENCABEZADO REMITO/////////////////////////////////////////////////////////////
                        char[] QuitaSimbolo = { '$', ' ' };
                        string agregar = "INSERT INTO Remito(NROREMITO, SUCURSAL, IDTIPOREMITO, FECHA, IDCLIENTE, IDPERSONAL, IDFORMAPAGO, BASICO, PORCDESC, DESCUENTO, IMPUESTO1, IMPUESTO2, TOTAL, OBSERVACIONES, IDNOTAPEDIDO, IDEMPRESA) VALUES ('" + txtNroRemito.Text.Trim() + "', '" + cboNroSucursal.Text.Trim() + "', " + txtCodTipoRemito.Text.Trim() + ", '" + FormateoFecha() + "', " + txtCodCliente.Text.Trim() + ", " + txtCodPersonal.Text.Trim() + ", " + txtCodFormaPago.Text.Trim() + ", (Cast(replace('" + Math.Round(Convert.ToDecimal(txtSubTotal.Text.TrimStart(QuitaSimbolo)), 3) + "', ',', '.') as decimal(10,3))), " + 0 + ", " + 0 + " , (Cast(replace('" + Math.Round(Convert.ToDecimal(txtImpuesto1.Text.TrimStart(QuitaSimbolo)), 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(Convert.ToDecimal(txtImpuesto2.Text.TrimStart(QuitaSimbolo)), 3) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + Math.Round(Convert.ToDecimal(txtTotalFactur.Text.TrimStart(QuitaSimbolo)), 2) + "', ',', '.') as decimal(10,2))), '" + txtObservacionRemito.Text + "', " + CodNotaPedido + ", " + IDEMPRESA + ")";
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        if (conn.InsertarGeneric(agregar) == false)
                            MessageBox.Show("Falta información requerida en el encabezado de remito. Completar la información", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            MostrarDatos(0,7000);
                            txtCodigoArticulo.Enabled = false;
                            cboArticulo.Enabled = false;
                            btnBuscaArticulo.Enabled = false;
                            txtCantArticulo.Enabled = false;
                            txtProcDesc.Enabled = false;
                            txtPrecio.Enabled = false;
                            btnAgregaArt.Enabled = false;
                            int iCantExistenciaActualArticulo = 0;
                            string sCodArt = "";

                            bool iIncluyeArticuloConFaltante = true;

                            conn.DesconectarBDLeeGeneric();
                            conn.DesconectarBD();

                            lvwDetalleRemito.Items.Clear();
                            //nroRemitoInterCreado = Convert.ToInt32(lvwRemito.Items[lvwRemito.Items.Count - 1].SubItems[0].Text);
                            nroRemitoInterCreado = UltimoRemito();

                            SqlCommand cm = new SqlCommand("SELECT * FROM DetalleNotaPedido, Articulo WHERE Remitido='No' AND DetalleNotaPedido.IDARTICULO = Articulo.IDARTICULO AND DetalleNotaPedido.IDNOTAPEDIDO = " + CodNotaPedido + " ORDER BY IDDETALLENOTAPEDIDO", conectaEstado);

                            SqlDataAdapter da = new SqlDataAdapter(cm);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            foreach (DataRow dr in dt.Rows)
                            {
                                //EVALUA LA EXISTENCIA
                                sCodArt = dr["Codigo"].ToString().Trim();

                                connArt.LeeArticulo("SELECT * FROM Articulo WHERE Codigo = '" + sCodArt + "'", "Articulo");
                                iCantExistenciaActualArticulo = Convert.ToInt32(connArt.leer["CANT_ACTUAL"].ToString());
                                connArt.DesconectarBDLee();

                                //Evalua Cantidad de stock > a cantidad del pedido y pregunta si se remitira ese item
                                if (iCantExistenciaActualArticulo < Convert.ToInt32(dr["CANTIDADPEDIDA"]))
                                {
                                    DialogResult dialogResult1 = MessageBox.Show("Las existencias actuales del artículo N° " + sCodArt + " es inferior a la cantidad pedida, desea incluir la cantidad actualmente disponible?", "Remito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (dialogResult1 == DialogResult.Yes)
                                        iIncluyeArticuloConFaltante = true;
                                    if (dialogResult1 == DialogResult.No)
                                        iIncluyeArticuloConFaltante = false;
                                }


                                if (iCantExistenciaActualArticulo > 0 && iIncluyeArticuloConFaltante == true)
                                {
                                    iActualItem = 0;
                                    lvwDetalleRemito.SmallImageList = imageList1;
                                    ListViewItem item = new ListViewItem(dr["IdArticulo"].ToString());
                                    item.SubItems.Add(dr["Codigo"].ToString());
                                    item.SubItems.Add(dr["Descripcion"].ToString());
                                    item.SubItems.Add(dr["CANTIDADAENTREGA"].ToString());
                                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["PRECUNITARIO"]), 3).ToString());

                                    iIDdetalleNotaPedido = Convert.ToInt32(dr["IDDETALLENOTAPEDIDO"].ToString());
                                    //Recalcula valores de entrega parcial//

                                    if (Convert.ToInt32(Convert.ToInt32(dr["CANTIDADRESTANTE"])) <= 0 && Convert.ToInt32(dr["CANTIDADAENTREGA"]) > 0)
                                    {
                                        // RecalculaValores(Math.Round(Convert.ToDecimal(dr["PRECUNITARIO"]), 3).ToString(), Math.Round(Convert.ToDecimal(dr["Subtotal"]), 3).ToString(), dr["DESCUENTO"].ToString(), Math.Round(Convert.ToDecimal(dr["IMPUESTO1"]), 3).ToString(), Math.Round(Convert.ToDecimal(dr["IMPUESTO2"]), 3).ToString(), Math.Round(Convert.ToDecimal(dr["IMPORTE"]), 2).ToString(), Convert.ToInt32(dr["CANTIDADAENTREGA"]), CodNotaPedido);

                                        PUnitario = Math.Round(Convert.ToSingle(dr["PRECUNITARIO"]), 3);
                                        subTotal = PUnitario * Convert.ToInt32(dr["CANTIDADAENTREGA"]);
                                        CostoEnLista = Convert.ToDouble(dr["CostoEnLista"].ToString());
                                        importeTotal = Math.Round((CalculoPorcentajeListaVenta(CostoEnLista) * Convert.ToInt32(dr["CANTIDADAENTREGA"])), 3);
                                        idImpuesto = Convert.ToInt32(dr["IDIMPUESTO"].ToString());

                                        sumaTotales += subTotal;
                                        txtSubTotal.Text = "$ " + Math.Round(sumaTotales, 3).ToString();

                                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(subTotal), 3).ToString());                ///SubTotal
                                        item.SubItems.Add(dr["DESCUENTO"].ToString());


                                        if (idImpuesto == 3)
                                        {
                                            impuesto2 = Math.Round(((subTotal * 1) - subTotal), 2);
                                            Neto = Math.Round((importeTotal + impuesto2), 3);
                                            item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(impuesto2), 2).ToString());
                                            item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());
                                            sumaImpuesto2 += impuesto2;
                                            txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto2, 2).ToString();
                                            sumaNetos += Neto;
                                            txtTotalFactur.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                                        }


                                        else if (idImpuesto == 2)
                                        {
                                            impuesto2 = Math.Round(((subTotal * 1.105) - subTotal), 2);
                                            Neto = Math.Round((importeTotal + impuesto2), 3);
                                            item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(impuesto2), 2).ToString());
                                            item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());
                                            sumaImpuesto2 += impuesto2;
                                            txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto2, 2).ToString();
                                            sumaNetos += Neto;
                                            txtTotalFactur.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                                        }
                                        else if (idImpuesto == 1)
                                        {
                                            impuesto1 = Math.Round(((subTotal * 1.21) - subTotal), 2);
                                            Neto = Math.Round((importeTotal + impuesto1), 3);
                                            item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(impuesto1), 2).ToString());
                                            item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());
                                            sumaImpuesto1 += impuesto1;
                                            txtImpuesto2.Text = "$ " + Math.Round(sumaImpuesto1, 2).ToString();
                                            sumaNetos += Neto;
                                            txtTotalFactur.Text = "$ " + Math.Round(sumaNetos, 2).ToString();
                                        }

                                        //item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(importeTotal), 2).ToString());           //Importe        
                                    }

                                    else
                                    {
                                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Subtotal"]), 2).ToString());
                                        item.SubItems.Add(dr["DESCUENTO"].ToString());

                                        if (dr["IMPUESTO2"].ToString() != "0,0000")
                                            item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPUESTO2"]), 2).ToString());
                                        else
                                            item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPUESTO1"]), 2).ToString());

                                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPORTE"]), 2).ToString());
                                    }

                                    item.SubItems.Add(dr["OBSERVACIONES"].ToString());

                                    item.ImageIndex = 1;

                                    item.SubItems.Add(dr["IDDETALLENOTAPEDIDO"].ToString());
                                    item.SubItems.Add(dr["IMPUESTO2"].ToString());
                                    item.SubItems.Add(dr["IMPUESTO1"].ToString());

                                    item.UseItemStyleForSubItems = false;

                                    if (iCantExistenciaActualArticulo <= Convert.ToInt32(dr["CANTIDADAENTREGA"].ToString()))
                                    { //Si hay faltante no se remitira se deberá reasignar
                                      // DialogResult dialogResult1 = MessageBox.Show("La existencia del artículo Código: " + dr["Codigo"].ToString().Trim() + " es inferior a la cantidad pedida, desea reasignar?", "Remitir Mercadería", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                        // if (dialogResult1 == DialogResult.Yes)
                                        // {
                                        lvwDetalleRemito.Items.Add(item);
                                        bDetectaEntregaParcial = true;

                                        iCantidadPedida = Convert.ToInt32(dr["CANTIDADPEDIDA"].ToString());
                                        iCantidadRestante = Convert.ToInt32(dr["CANTIDADRESTANTE"].ToString());
                                        iCantidadAentregar = Convert.ToInt32(dr["CANTIDADAENTREGA"].ToString());

                                        //iIDdetalleNotaPedido = Convert.ToInt32(dr["IDDETALLENOTAPEDIDO"].ToString());

                                        iActualItem = (lvwDetalleRemito.Items.Count - 1);
                                        GuardaItemDatoRemito(bDetectaEntregaParcial, iCantidadPedida, iCantidadRestante, iCantidadAentregar, iIDdetalleNotaPedido, iActualItem);
                                        // }
                                        /* if (dialogResult1 == DialogResult.No)
                                         {
                                             bDetectaEntregaParcial = false;
                                             GuardaItemDatoRemito(bDetectaEntregaParcial, 0, 0, 0, iIDdetalleNotaPedido, iActualItem);
                                         }*/
                                    }
                                    else if (Convert.ToInt32(dr["CANTIDADAENTREGA"].ToString()) > 0 || Convert.ToInt32(dr["CANTIDADRESTANTE"].ToString()) >= 0)
                                    {
                                        lvwDetalleRemito.Items.Add(item);
                                        bDetectaEntregaParcial = false;
                                        iActualItem = (lvwDetalleRemito.Items.Count - 1);
                                        GuardaItemDatoRemito(bDetectaEntregaParcial, 0, 0, 0, iIDdetalleNotaPedido, iActualItem);
                                    }

                                    //////////////////////////////////////////////// ACTUALIZA EL AGREGADO DE DATOS ////////////////////////////////////////////////
                                    connGeneric.DesconectarBDLeeGeneric();
                                    connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleRemito WHERE NROREMITOINTERNO = " + nroRemitoInterCreado + "", "DetalleRemito");

                                    subTotal = Convert.ToSingle(connGeneric.leerGeneric["SubTotal"].ToString());
                                    impuesto1 = Convert.ToSingle(connGeneric.leerGeneric["Iva105"].ToString());
                                    impuesto2 = Convert.ToSingle(connGeneric.leerGeneric["Iva21"].ToString());
                                    importeTotal = Convert.ToSingle(connGeneric.leerGeneric["Importe"].ToString());

                                    string actualizar = "BASICO=(Cast(replace('" + subTotal + "', ',', '.') as decimal(10,3))), IMPUESTO1=(Cast(replace('" + impuesto1 + "', ',', '.') as decimal(10,3))), IMPUESTO2 =(Cast(replace('" + impuesto2 + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeTotal + "', ',', '.') as decimal(10,2)))";

                                    this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeTotal, 2));
                                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto1, 2));
                                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)impuesto2, 2));
                                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)subTotal, 2));

                                    connGeneric.ActualizaGeneric("Remito", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NROREMITOINTERNO = " + nroRemitoInterCreado + "");
                                    connGeneric.DesconectarBD();
                                    connGeneric.DesconectarBDLeeGeneric();
                                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    

                                }
                                else
                                {
                                    if (iCantExistenciaActualArticulo == 0)
                                        MessageBox.Show("Artículos Cod. " + sCodArt + " sin existencias dentro de la N.P. no se incluirán al remito.", "Remitir Mercadería", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                    else if (iIncluyeArticuloConFaltante == false)
                                        MessageBox.Show("Artículos Cod. " + sCodArt + " no tiene existencia suficiente para cumplir con el pedido y se ha indicado no incluirlo al remito.", "Remitir Mercadería", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                        }
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("No se ha remitido nada", "Remitir Mercadería", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
            }
        }

        private void GuardaItemDatoRemito(bool bDetectaEntregaParcial, int iCantidadPedida, int iCantidadRestante, int iCantidadAentregar, int IdDetalleNP, int Item)
        {
            try
            {
                int iTotalStock, CantActualArticulo = 0;
                int nroUltimoRemitoInterno;

                nroUltimoRemitoInterno = UltimoRemito();

                //////////////////////////GUARDA ITEMS DE DATOS DE REMITO//////////////////////////////////////
                char[] QuitaSimbolo2 = { '$', ' ' };
                //  for (int i = 0; i < (lvwDetalleRemito.Items.Count); i++)
                //  {
                string agregarItem = "INSERT INTO DetalleRemito(IDARTICULO, CANTIDAD, PRECUNITARIO, SUBTOTAL, DESCUENTO, PORCDESC, IMPUESTO1, IMPUESTO2, IMPORTE, NROREMITOINTERNO,IDDETALLENOTAPEDIDO) VALUES (" + Convert.ToInt32(lvwDetalleRemito.Items[Item].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleRemito.Items[Item].SubItems[3].Text + "', ',', '.') as decimal(10,0))), (Cast(replace('" + lvwDetalleRemito.Items[Item].SubItems[4].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleRemito.Items[Item].SubItems[5].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,3))), '0','0', (Cast(replace('" + lvwDetalleRemito.Items[Item].SubItems[12].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleRemito.Items[Item].SubItems[11].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleRemito.Items[Item].SubItems[8].Text.TrimStart(QuitaSimbolo2) + "', ',', '.') as decimal(10,2))), " + nroUltimoRemitoInterno + ", " + IdDetalleNP + ")";

                //nroRemitoInter = Convert.ToInt32(lvwRemito.Items[lvwRemito.Items.Count - 1].SubItems[0].Text);
                if (conn.InsertarGeneric(agregarItem))
                {
                    connGeneric.DesconectarBD();
                    connGeneric.DesconectarBDLeeGeneric();
                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////// ACTUALIZA ESTADOS DE EXISTENCIA DE ARTICULOS Y  DETALLE DE LA NOTA DE PEDIDO (Estado Remito y Cantidad a Entregar) ///////

                ///ACTUALIZA STOCK///               
                connGeneric.LeeGeneric("SELECT * FROM Articulo WHERE Codigo = '" + lvwDetalleRemito.Items[Item].SubItems[1].Text.Trim() + "'", "Articulo");
                CantActualArticulo = Convert.ToInt32(connGeneric.leerGeneric["CANT_ACTUAL"].ToString());
                iTotalStock = CantActualArticulo - Convert.ToInt32(lvwDetalleRemito.Items[Item].SubItems[3].Text);

                if (iTotalStock < 0)
                    iTotalStock = 0;

                string actualizaStockArticulo = "CANT_ACTUAL=(Cast(replace(" + iTotalStock + ", ',', '.') as decimal(10,0)))";
                if (connGeneric.ActualizaGeneric("Articulo", actualizaStockArticulo, " IDARTICULO= " + Convert.ToInt32(lvwDetalleRemito.Items[Item].SubItems[0].Text) + ""))
                {
                    connGeneric.DesconectarBD();
                    connGeneric.DesconectarBDLeeGeneric();
                }
                ///COLOCA ENTREGAS A CERO ///
                if (bDetectaEntregaParcial != true)
                {
                    string actualizaCantidadEntregadaDetalleNP = "CANTIDADAENTREGA='0', CANTIDADRESTANTE='0', Remitido='Si'";
                    if (connGeneric.ActualizaGeneric("DetalleNotaPedido", actualizaCantidadEntregadaDetalleNP, " IDDETALLENOTAPEDIDO= " + Convert.ToInt32(lvwDetalleRemito.Items[Item].SubItems[10].Text) + ""))
                    {
                        connGeneric.DesconectarBD();
                        connGeneric.DesconectarBDLeeGeneric();
                    }
                }
                else
                {
                    if ((CantActualArticulo < iCantidadPedida) && (iCantidadRestante < 0))
                    {
                        //EVALUA NUEVAMENTE LA EXISTENCIA
                        connGeneric.LeeGeneric("SELECT * FROM Articulo WHERE Codigo = '" + lvwDetalleRemito.Items[Item].SubItems[1].Text.Trim() + "'", "Articulo");
                        CantActualArticulo = Convert.ToInt32(connGeneric.leerGeneric["CANT_ACTUAL"].ToString());

                        int iCantidadPendienteEntrega;
                        iCantidadPendienteEntrega = iCantidadPedida - Convert.ToInt32(lvwDetalleRemito.Items[Item].SubItems[3].Text); //CantActualArticulo;
                        string actualizaCantidadEntregadaDetalleNP = "CANTIDADRESTANTE = " + (-(Convert.ToInt32(lvwDetalleRemito.Items[Item].SubItems[3].Text))) + ", CANTIDADAENTREGA = " + iCantidadPendienteEntrega + ", Remitido='No'";

                        if (connGeneric.ActualizaGeneric("DetalleNotaPedido", actualizaCantidadEntregadaDetalleNP, " IDDETALLENOTAPEDIDO= " + Convert.ToInt32(lvwDetalleRemito.Items[Item].SubItems[10].Text) + ""))
                        {
                            connGeneric.DesconectarBD();
                            connGeneric.DesconectarBDLeeGeneric();
                        }
                    }
                    else
                    {
                        string actualizaCantidadEntregadaDetalleNP11 = "CANTIDADAENTREGA='0', CANTIDADRESTANTE='0', Remitido='Si'";
                        if (connGeneric.ActualizaGeneric("DetalleNotaPedido", actualizaCantidadEntregadaDetalleNP11, " IDDETALLENOTAPEDIDO= " + Convert.ToInt32(lvwDetalleRemito.Items[Item].SubItems[10].Text) + ""))
                        {
                            connGeneric.DesconectarBD();
                            connGeneric.DesconectarBDLeeGeneric();
                        }
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
            }
            catch { }
        }

        /* private void chkActivaNotaPedido_CheckedChanged(object sender, EventArgs e)
         {
             if (chkActivaNotaPedido.Checked == true)
             {
                 frmNotaPedido frmNPedido = new frmNotaPedido();
                 frmNPedido.pasarNPCod += new frmNotaPedido.pasarNotaPedidoCod1(pasarCodNotaPedido);  //Delegado1                
                 frmNPedido.ShowDialog();
                 chkActivaNotaPedido.Checked = false;
             }
         }*/

        private void btnNotaPedido_Click(object sender, EventArgs e)
        {
            frmNotaPedido frmNPedido = new frmNotaPedido();
            frmNPedido.pasarNPCod += new frmNotaPedido.pasarNotaPedidoCod1(pasarCodNotaPedido);  //Delegado1                
            frmNPedido.ShowDialog();
        }

        private void cboBuscaRemito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtBuscarRemito.Focus();
            }
        }

        private void txtBuscarRemito_KeyPress(object sender, KeyPressEventArgs e)
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
                txtNroRemito.Focus();
            }
        }

        private void txtNroRemito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                dtpFechaRemito.Focus();
            }
        }

        private void dtpFechaRemito_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCodTipoRemito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboTipoRemito.Focus();
            }
        }

        private void cboTipoRemito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnTipoRemito.Focus();
            }
        }

        private void btnTipoRemito_KeyPress(object sender, KeyPressEventArgs e)
        {

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

        }

        private void txtCodPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnPersonal.Focus();
            }
        }

        private void cboPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtObservacionRemito.Focus();
            }
        }

        private void btnPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtObservacionRemito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodigoArticulo.Focus();
            }
        }

        private void btnNotaPedido_KeyPress(object sender, KeyPressEventArgs e)
        {

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

        private void btnBuscaArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCantArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnAgregaArt.Focus();
            }
        }

        private void txtProcDesc_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnAgregaArt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodigoArticulo.Focus();
            }
        }

        private void lvwRemito_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
               // ActualizaSaldo(Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[0].Text));

                pasarCodRemito(Int32.Parse(this.lvwRemito.SelectedItems[0].SubItems[0].Text.Trim()));  //Si doble click ejecuta delegado para pasar datos entre forms
                pasarCodCliente(Int32.Parse(this.lvwRemito.SelectedItems[0].SubItems[4].Text));  //Si doble click ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch { }
        }

        private void tsFacturacion_Click(object sender, EventArgs e)
        {
            frmFacturacion chieldFacturacion = new frmFacturacion();
            chieldFacturacion.ShowDialog();
        }

        private void txtNroRemito_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNroRemito_Leave(object sender, EventArgs e)
        {
            if (ValidaNumerador(this.txtNroRemito.Text.Trim()) == true)
            {
                //MessageBox.Show("Error de Numerador. Ya existe el numero ingresado, el numero ha sido corregido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NuevoRemito();
            }
        }


        private void tsBtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboBuscaRemito.SelectedIndex == 0)
                {
                    BuscarDatos1();
                }

                else if (cboBuscaRemito.SelectedIndex == 1)
                {
                    BuscarDatos2();
                }

                else if (cboBuscaRemito.SelectedIndex == 2)
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
                lvwRemito.Items.Clear();
                lvwDetalleRemito.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT Remito.*, Cliente.* FROM Remito, Cliente WHERE Cliente.IDEMPRESA = "+ IDEMPRESA +" AND Remito.Sucursal='" + sPtoVta + "' AND Remito.IDCLIENTE = Cliente.IDCLIENTE AND Remito.NROREMITO = '" + txtBuscarRemito.Text + "'", conectaEstado);


                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwRemito.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NROREMITOINTERNO"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["NROREMITO"].ToString());
                    item.SubItems.Add(dr["Fecha"].ToString());
                    item.SubItems.Add(dr["IdCliente"].ToString());
                    item.SubItems.Add(dr["RazonSocial"].ToString());
                    item.SubItems.Add(dr["ESTADOREMITO"].ToString());

                    if (dr["ESTADOREMITO"].ToString() == "NO FACTURADO")
                        item.ImageIndex = 3;
                    else if (dr["ESTADOREMITO"].ToString() == "FACTURADO")
                        item.ImageIndex = 5;
                    else if (dr["ESTADOREMITO"].ToString() == "ANULADO")
                        item.ImageIndex = 2;
                    item.UseItemStyleForSubItems = false;
                    lvwRemito.Items.Add(item);

                }
                cm.Connection.Close();

            }
            catch { }
        }

        public void BuscarDatos2()
        {
            try
            {
                lvwRemito.Items.Clear();
                lvwDetalleRemito.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT Remito.*, Cliente.* FROM Remito, Cliente WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Remito.Sucursal='" + sPtoVta + "' AND Remito.IDCLIENTE = Cliente.IDCLIENTE AND Remito.fecha = '" + txtBuscarRemito.Text + "'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwRemito.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NROREMITOINTERNO"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["NROREMITO"].ToString());
                    item.SubItems.Add(dr["Fecha"].ToString());
                    item.SubItems.Add(dr["IdCliente"].ToString());
                    item.SubItems.Add(dr["RazonSocial"].ToString());
                    item.SubItems.Add(dr["ESTADOREMITO"].ToString());

                    if (dr["ESTADOREMITO"].ToString() == "NO FACTURADO")
                        item.ImageIndex = 3;
                    else if (dr["ESTADOREMITO"].ToString() == "FACTURADO")
                        item.ImageIndex = 5;
                    else if (dr["ESTADOREMITO"].ToString() == "ANULADO")
                        item.ImageIndex = 2;

                    item.UseItemStyleForSubItems = false;
                    lvwRemito.Items.Add(item);

                }
                cm.Connection.Close();

            }
            catch { MessageBox.Show("Error: El formato de fecha correcto es DD/MM/AAAA.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void BuscarDatos3()
        {
            try
            {
                lvwRemito.Items.Clear();
                lvwDetalleRemito.Items.Clear();
                if (cboBuscaRemito.SelectedIndex == 2 && txtBuscarRemito.Text != "")
                {
                    SqlCommand cm = new SqlCommand("SELECT Remito.*, Cliente.* FROM Remito, Cliente WHERE Cliente.IDEMPRESA = "+IDEMPRESA+ " AND Remito.Sucursal='" + sPtoVta + "' AND Remito.IDCLIENTE = Cliente.IDCLIENTE AND Cliente.RAZONSOCIAL LIKE '%" + txtBuscarRemito.Text + "%'", conectaEstado);

                        //SELECT IdCliente, RAZONSOCIAL, LOCALIDAD, TELEFONOSCOMERCIALES, NUMDECUIT FROM Cliente WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND RAZONSOCIAL LIKE '%" + txtBuscarRemito.Text + "%' ORDER BY RAZONSOCIAL", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwRemito.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["NROREMITOINTERNO"].ToString());
                        item.SubItems.Add(dr["Sucursal"].ToString());
                        item.SubItems.Add(dr["NROREMITO"].ToString());
                        item.SubItems.Add(dr["Fecha"].ToString());
                        item.SubItems.Add(dr["IdCliente"].ToString());
                        item.SubItems.Add(dr["RazonSocial"].ToString());
                        item.SubItems.Add(dr["ESTADOREMITO"].ToString());

                        if (dr["ESTADOREMITO"].ToString() == "NO FACTURADO")
                            item.ImageIndex = 3;
                        else if (dr["ESTADOREMITO"].ToString() == "FACTURADO")
                            item.ImageIndex = 5;
                        else if (dr["ESTADOREMITO"].ToString() == "ANULADO")
                            item.ImageIndex = 2;

                        item.UseItemStyleForSubItems = false;
                        lvwRemito.Items.Add(item);

                    }
                    cm.Connection.Close();
                }
            }
            catch { }
        }
        /////////////////////////////////////////////////////FIN BLOQUE DE BUSQUEDA///////////////////////////////////////////////////////////////// 

            

        private void tsBtnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                OptRemito = 0;

                if (lvwRemito.Items.Count == 0)
                    MessageBox.Show("Error: No existen datos para mostrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    nroRemito = lvwRemito.SelectedItems[0].SubItems[2].Text;
                    nroRemitoInt = Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[0].Text);

                    DGestion.Reportes.frmRPTRemito frmRptRemito = new DGestion.Reportes.frmRPTRemito();
                    frmRptRemito.ShowDialog();
                }
            }
            catch //(System.Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                MessageBox.Show("Error: No se ha seleccionado el comprobante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsPreImpresos_Click(object sender, EventArgs e)
        {
            try
            {
                OptRemito = 1;

                if (lvwRemito.Items.Count == 0)
                    MessageBox.Show("Error: No existen datos para mostrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    nroRemito = lvwRemito.SelectedItems[0].SubItems[2].Text;
                    nroRemitoInt = Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[0].Text);

                    DGestion.Reportes.frmRPTRemito frmRptRemito = new DGestion.Reportes.frmRPTRemito();
                    frmRptRemito.ShowDialog();
                }
            }
            catch //(System.Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                MessageBox.Show("Error: No se ha seleccionado el comprobante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizaSaldo(int idRemito)
        {
            try
            {
                if (lvwDetalleRemito.Items.Count != 0)
                {
                    string subTotalfactu;
                    string iva105Factu;
                    string iva21Factu;
                    string importeFactu;

                    connGeneric.DesconectarBDLeeGeneric();
                    connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleRemito WHERE NRORemitoINTERNO = " + idRemito + "", "DetalleRemito");

                    importeFactu = connGeneric.leerGeneric["Importe"].ToString();
                    iva105Factu = connGeneric.leerGeneric["Iva105"].ToString();
                    iva21Factu = connGeneric.leerGeneric["Iva21"].ToString();
                    subTotalfactu = connGeneric.leerGeneric["SubTotal"].ToString();

                    string actualizar = "BASICO=(Cast(replace('" + subTotalfactu + "', ',', '.') as decimal(10,3))), IMPUESTOS1=(Cast(replace('" + iva105Factu + "', ',', '.') as decimal(10,3))), IMPUESTOS2 =(Cast(replace('" + iva21Factu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeFactu + "', ',', '.') as decimal(10,2)))";
                    if (connGeneric.ActualizaGeneric("Remito", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NRORemitoInterno = " + idRemito + ""))
                    {
                        MostrarDatos(0,7000);
                        MostrarItemsDatos2(idNroRemitoInterno);
                    }
                }
            }
            catch { }
        }

        private void btcActualizaEstado_Click(object sender, EventArgs e)
        {
            /*try
            {
                SqlCommand cm = new SqlCommand("SELECT Remito.ESTADOREMITO, remito.NROFACTURAINTERNO FROM Remito WHERE Remito.ESTADOREMITO = 'FACTURADO' ORDER BY NROFACTURAINTERNO", conectaEstado);

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

                this.lvwRemito.Items.Clear();
                MostrarDatos();
            }
            catch {
                //MessageBox.Show("Error: No se puede actualizar el estado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void txtBuscarRemito_TextChanged(object sender, EventArgs e)
        {
            string sCodArt;
            char[] QuitaSimbolo = { '$', ' ' };
            char[] QuitaSimbolo2 = { '*', ' ' };

            sCodArt = txtBuscarRemito.Text.TrimStart(QuitaSimbolo2);
            sCodArt = sCodArt.TrimEnd(QuitaSimbolo2);
            this.txtBuscarRemito.Text = sCodArt;
        }

        private void tsBtnRePag_Click(object sender, EventArgs e)
        {
            if (PagRe != 0 && PagRe != 1)
            {

                int iPosterior;

                iPosterior = PagRe;
                PagRe = PagRe - 1000;

                PagAv = iPosterior;

                MostrarDatos(PagRe, PagAv);
            }
            else
            {
                PagRe = 0;
                PagAv = 1000;
                MostrarDatos(PagRe, PagAv);
            }
        }

        private void tsListaPrecioProvee_Click(object sender, EventArgs e)
        {

        }

        private void tsFacturaRemito_Click(object sender, EventArgs e)
        {
            if (lvwRemito.SelectedItems.Count == 1)
            {
                gboRemitoFactura.Visible = true;
                txtNroRemitoConsulta.Text = txtNroRemito.Text;
                lvwFactuRemito.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT FacturasVentas.NROFACTURAINTERNO, FacturasVentas.NROFACTURA, FacturasVentas.SUCURSAL, FacturasVentas.FECHA FROM FacturasVentas, Remito WHERE FacturasVentas.NROFACTURAINTERNO = Remito.NROFACTURAINTERNO AND Remito.nroremito = " + Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[2].Text) + "  AND ESTADOREMITO = 'FACTURADO' ORDER BY FacturasVentas.NROFACTURA", conectaEstado);                               

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwFactuRemito.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NROFACTURAINTERNO"].ToString());
                    item.SubItems.Add(dr["SUCURSAL"].ToString());
                    item.SubItems.Add(dr["NROFACTURA"].ToString());
                    item.SubItems.Add(dr["FECHA"].ToString());

                    item.ImageIndex = 1;

                    item.UseItemStyleForSubItems = false;
                    lvwFactuRemito.Items.Add(item);
                }
                cm.Connection.Close();

            }
        }

        private void btnCerrarFactuRemito_Click(object sender, EventArgs e)
        {
            gboRemitoFactura.Visible = false;
        }

        private void btnAnularComprobante_Click(object sender, EventArgs e)
        {
            try
            {
                string sEstadoRemito;

                connGeneric.LeeGeneric("SELECT Remito.EstadoRemito FROM Remito WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND Remito.NROREMITOINTERNO = " + Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[0].Text) + "", "Remito");
                sEstadoRemito = this.connGeneric.leerGeneric["EstadoRemito"].ToString();

                if (sEstadoRemito != "ANULADO")
                {
                    int NumRemito = 0;

                    if (lvwRemito.SelectedItems.Count == 1)
                    {
                        string actualizar = "ESTADOREMITO = 'ANULADO'";
                        connGeneric.ActualizaGeneric("Remito", actualizar, " NROREMITOINTERNO = " + Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[0].Text) + "");
                    }

                    //this.lvwRemito.Items.Clear();       

                    NumRemito = (Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[0].Text));
                    actualizaStock_Anulacion_o_eliminacion(NumRemito);

                    MostrarDatos2(Convert.ToInt32(lvwRemito.SelectedItems[0].SubItems[0].Text));
                }
                else
                    MessageBox.Show("El movimiento ya se encuentra anulado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch
            {
                //MessageBox.Show("Error: No se puede actualizar el estado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsBtnAvPag_Click(object sender, EventArgs e)
        {
            int iAnterior;

            iAnterior = PagAv;
            PagAv = PagAv + 1000;

            PagRe = iAnterior;
            MostrarDatos(PagRe, PagAv);
        }

        private string LeeEstadoRemito(int NroRemitoInt)
        {
            try
            {
                string EstadoFacturaImpresion;
                conn.LeeGeneric("SELECT EstadoRemito FROM Remito WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = '" + sPtoVta.Trim() + "' AND NroRemitoInterno = " + NroRemitoInt + "", "Remito");

                EstadoFacturaImpresion = conn.leerGeneric["EstadoRemito"].ToString();

                return EstadoFacturaImpresion;
            }
            catch { return "Error"; }
        }

        private void actualizaStock_Anulacion_o_eliminacion(int nRemito)
        {
            try
            {
                double dCantidad_articulo_Remito;
                double dCantidad_Articulo_Actual;
                double iIdArticulo = 0;

                if (lvwRemito.SelectedItems.Count == 1)
                {
                    SqlCommand cm = new SqlCommand("SELECT Remito.NROREMITOINTERNO, DetalleRemito.IDDETALLEREMITO, Articulo.IDARTICULO, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL as 'AntidadArticuloDisponible', DetalleRemito.CANTIDAD as 'CantidadArtRemito' FROM Remito, DetalleRemito, Articulo WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND DetalleRemito.IDARTICULO = Articulo.IDARTICULO AND Remito.NROREMITOINTERNO = DetalleRemito.NROREMITOINTERNO AND Remito.NROREMITOINTERNO = " + nRemito + "", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        dCantidad_Articulo_Actual = Convert.ToDouble(dr["AntidadArticuloDisponible"].ToString());
                        dCantidad_articulo_Remito = Convert.ToDouble(dr["CantidadArtRemito"].ToString());
                        iIdArticulo = Convert.ToDouble(dr["IDARTICULO"].ToString());

                        dCantidad_Articulo_Actual = dCantidad_Articulo_Actual + dCantidad_articulo_Remito;

                        //Actualiza la Cantidad de Stock
                        string actualizaStock = "CANT_ACTUAL=(Cast(replace(" + dCantidad_Articulo_Actual + ", ',', '.') as decimal(10,0)))";
                        if (connGeneric.ActualizaGeneric("Articulo", actualizaStock, " IDARTICULO= " + iIdArticulo + ""))
                        {
                            connGeneric.DesconectarBD();
                            connGeneric.DesconectarBDLeeGeneric();
                        }
                        ///////////////////////////////////////////////////////////

                        dCantidad_articulo_Remito = 0;
                        dCantidad_Articulo_Actual = 0;
                    }
                    cm.Connection.Close();
                }                

            }
            catch { }
        }


        private void actualizaStock_Anulacion_o_eliminacion(int nRemitoInt, string CodArticulo)
        {
            try
            {
                double dCantidad_articulo_remito;
                double dCantidad_Articulo_Actual;
                double iIdArticulo = 0;


                //  if (lvwFacturaVenta.SelectedItems.Count == 1)
                //   {
                SqlCommand cm = new SqlCommand("SELECT Remito.NRORemitoInterno, DetalleRemito.IDDETALLEREMITO, Articulo.IDARTICULO, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL as 'AntidadArticuloDisponible', DetalleRemito.CANTIDAD as 'CantidadArtFacturado' FROM Remito, DetalleRemito, Articulo WHERE Remito.IDEMPRESA = " + IDEMPRESA + " AND DetalleRemito.IDARTICULO = Articulo.IDARTICULO AND Remito.NRORemitoInterno = DetalleRemito.NROREMITOINTERNO AND Articulo.Codigo = '" + CodArticulo + "' AND Remito.NROREMITOINTERNO = " + nRemitoInt + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    dCantidad_Articulo_Actual = Convert.ToDouble(dr["AntidadArticuloDisponible"].ToString());
                    dCantidad_articulo_remito = Convert.ToDouble(dr["CantidadArtFacturado"].ToString());
                    iIdArticulo = Convert.ToDouble(dr["IDARTICULO"].ToString());

                    dCantidad_Articulo_Actual = dCantidad_Articulo_Actual + dCantidad_articulo_remito;

                    //Actualiza la Cantidad de Stock
                    string actualizaStock = "CANT_ACTUAL=(Cast(replace(" + dCantidad_Articulo_Actual + ", ',', '.') as decimal(10,0)))";
                    if (connGeneric.ActualizaGeneric("Articulo", actualizaStock, " IDARTICULO= " + iIdArticulo + ""))
                    {
                        connGeneric.DesconectarBD();
                        connGeneric.DesconectarBDLeeGeneric();
                    }
                    ///////////////////////////////////////////////////////////

                    dCantidad_articulo_remito = 0;
                    dCantidad_Articulo_Actual = 0;
                }
                cm.Connection.Close();
            }


            //   }
            catch { }
        }


    }
}