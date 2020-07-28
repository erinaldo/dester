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

namespace DGestion
{
    public partial class frmProveedCompra : Form
    {
        public frmProveedCompra() {
            InitializeComponent();
        }

        ArticulosBD conn = new ArticulosBD();
        CGenericBD connGeneric = new CGenericBD();
        EmpresaBD connEmpresa = new EmpresaBD();
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
        DateTime fechaFacturaCompra;

        int contadorNROfactNuevo;

        bool nuevaFactu=false;
        int idNROFACTUINTERNO;

        int indiceLvwCompraProvee;

        int IDEMPRESA;
        
        private int ConsultaEmpresa()
        {
            try{
                int IdEmpresa;
                connEmpresa.ObtenerEmpresaActiva("SELECT * FROM Empresa WHERE RazonSocial = '" + frmPrincipal.Empresa + "'", "Empresa");
                IdEmpresa = Convert.ToInt32(connEmpresa.leerEmpresa["IdEmpresa"].ToString());

                connEmpresa.DesconectarBDLeeEmpresa();

                return IdEmpresa;
            }
            catch { return 0; }
        }

        private void GuardaItemsDatos(bool status, int nroFactuInter) {
            try {
                int IdArticulo;
                int IdImpuesto;
                string Codigo;
                string Descripcion;
                string CodigoFabrica;
                double CostoUnitarioArticulo=0;
                string ProcCostoEnLista;
                string CostoEnLista;
                int Cantidad=0;
                double SubTotal=0;
                double Importe=0;
                int CantActualArticulo;
                //string Observaciones;

                double precioIngresado=0;

                if (txtCantidad.Text.Trim() != "")
                {
                    connGeneric.DesconectarBD(); connGeneric.DesconectarBDLeeGeneric();
                    conn.DesconectarBD(); conn.DesconectarBDLee();

                    conn.LeeArticulo("SELECT * FROM Articulo WHERE CODIGO = '" + txtCodArticulo.Text.Trim() + "'", "Articulo");

                    IdArticulo = Convert.ToInt32(conn.leer["IDARTICULO"].ToString());
                    Codigo = conn.leer["Codigo"].ToString().Trim();
                    Descripcion = conn.leer["Descripcion"].ToString();
                    CodigoFabrica = conn.leer["Codigo"].ToString().Trim();

                    char[] QuitaSimboloCosto = { '$', ' ' };
                    precioIngresado = Math.Round(Convert.ToSingle(txtPrecio.Text.TrimStart(QuitaSimboloCosto)), 3);

                    CostoUnitarioArticulo = Math.Round(precioIngresado,3);//Convert.ToDouble(conn.leer["Costo"].ToString());
                    ProcCostoEnLista = conn.leer["ProcCostoEnLista"].ToString();
                    CostoEnLista = conn.leer["CostoEnLista"].ToString();
                    Cantidad = Convert.ToInt32(txtCantidad.Text.Trim());
                    CantActualArticulo = Convert.ToInt32(conn.leer["CANT_ACTUAL"].ToString());
                    IdImpuesto = Convert.ToInt32(conn.leer["IDIMPUESTO"].ToString());

                    SubTotal = Math.Round((Cantidad * CostoUnitarioArticulo), 3);
                    Importe = Math.Round((CostoUnitarioArticulo * Cantidad), 2);

                    ////////////////////////////////////// //////////////////////////////////////
                    lvwDetalleProveeCompras.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(IdArticulo.ToString());
                    item.SubItems.Add(Codigo.ToString().Trim());
                    item.SubItems.Add(Descripcion.ToString().Trim());
                    item.SubItems.Add(Cantidad.ToString());
                    item.SubItems.Add("$ " + CostoUnitarioArticulo.ToString());

                    item.SubItems.Add("$ " + Importe.ToString());
                    item.SubItems.Add("0");

                    if (IdImpuesto == 3)
                    {
                        Impuesto1 = Math.Round(((SubTotal * 1) - SubTotal), 2);
                        Neto = Math.Round((Importe + Impuesto1), 3);
                        item.SubItems.Add("$ " + Math.Round(Impuesto1, 2).ToString());
                        item.SubItems.Add("$ " + Math.Round(Neto, 3).ToString());

                        sumaImpuesto1 += Impuesto1;
                        txtImpuesto1.Text = "$ " + Math.Round(sumaImpuesto1, 2).ToString();

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
                    txtSubTotal.Text = "$ " + Math.Round(sumaTotales, 2).ToString();

                    item.SubItems.Add("-");
                    item.SubItems.Add("0");  //colocar el IDFACTURA

                    if (IdImpuesto != 2)
                        Impuesto1 = 0;
                    if (IdImpuesto != 1)
                        Impuesto2 = 0;

                    item.SubItems.Add(Convert.ToDecimal(Math.Round(Impuesto1, 2)).ToString());                     ///ITEM 10
                    item.SubItems.Add(Convert.ToDecimal(Math.Round(Impuesto2, 2)).ToString());                    ///ITEM 11

                    if (CantActualArticulo == 0)
                        item.ImageIndex = 3;
                    else
                        item.ImageIndex = 2;

                    lvwDetalleProveeCompras.Items.Add(item);

                    //Normalizacion de Saldos totales
                    if (lvwDetalleProveeCompras.Items.Count != 0)
                    {
                        dSubTotal = 0.000;
                        dImpuesto1 = 0.000;
                        dImpuesto2 = 0.000;
                        dImporteTotal = 0.00;

                        char[] QuitaSimbolo = { '$', ' ' };
                        for (int i = 0; i < (lvwDetalleProveeCompras.Items.Count); i++)
                        {
                            dSubTotal += Math.Round(Convert.ToSingle(lvwDetalleProveeCompras.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo)), 3);
                            dImpuesto1 += Math.Round(Convert.ToSingle(lvwDetalleProveeCompras.Items[i].SubItems[11].Text.TrimStart(QuitaSimbolo)), 2);
                            dImpuesto2 += Math.Round(Convert.ToSingle(lvwDetalleProveeCompras.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo)), 2);
                            dImporteTotal += Math.Round(Convert.ToSingle(lvwDetalleProveeCompras.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo)), 2);
                        }
                        this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImporteTotal, 2));
                        this.txtImpuesto1.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)dImpuesto1, 3));
                        this.txtImpuesto2.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)dImpuesto2, 3));
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
                        if (fechaFacturaCompra.AddDays(365) <= DateTime.Today)
                            MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            txtNroInternoFact.Text = idNROFACTUINTERNO.ToString();
                            idNROFACTUINTERNO = nroFactuInter;

                            connGeneric.EliminarGeneric("DetalleFacturaCompras", " NROFACTURAINTERNO = " + nroFactuInter);
                            char[] QuitaSimbolo = { '$', ' ' };

                            for (int i = 0; i < (lvwDetalleProveeCompras.Items.Count); i++)
                            {
                                string agregarItem = "INSERT INTO DetalleFacturaCompras(IDARTICULO, CANTIDAD, PRECUNITARIO, IMPORTE, DESCUENTO, PORCDESC, SUBTOTAL, IMPUESTO1, IMPUESTO2, OBSERVACIONES, NROFACTURAINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleProveeCompras.Items[i].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleProveeCompras.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleProveeCompras.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleProveeCompras.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), '0','0', (Cast(replace('" + lvwDetalleProveeCompras.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleProveeCompras.Items[i].SubItems[11].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleProveeCompras.Items[i].SubItems[12].Text + "', ',', '.') as decimal(10,2))), '" + txtObserva.Text.Trim()+ "', " + nroFactuInter + ")";

                                if (conn.InsertarArticulo(agregarItem))
                                {
                                    connGeneric.DesconectarBD();
                                    connGeneric.DesconectarBDLeeGeneric();
                                }

                                //////////////////////////////////////////////////////////ACTUALIZA STOCK///////////////////////////////////////////////////////////
                                int iTotalStock;
                                iTotalStock = Convert.ToInt32(lvwDetalleProveeCompras.Items[i].SubItems[3].Text) + CantActualArticulo;

                                string actualizaStock = "CANT_ACTUAL=(Cast(replace('" + iTotalStock + "', ',', '.') as decimal(10,2)))";
                                if (connGeneric.ActualizaGeneric("Articulo", actualizaStock, " IDARTICULO= " + Convert.ToInt32(lvwDetalleProveeCompras.Items[i].SubItems[0].Text) + ""))
                                {
                                    connGeneric.DesconectarBD();
                                    connGeneric.DesconectarBDLeeGeneric();
                                }
                                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////   
                            }
                            //MostrarDatos();
                            MostrarItemsDatos2(nroFactuInter);
                        }
                    }

                    else if (status == true)
                    {
                        char[] QuitaSimbolo = { '$', ' ' };
                        for (int i = 0; i < (lvwDetalleProveeCompras.Items.Count); i++)
                        {
                            string agregarItem = "INSERT INTO DetalleFacturaCompras(IDARTICULO, CANTIDAD, PRECUNITARIO, IMPORTE, DESCUENTO, PORCDESC, SUBTOTAL, IMPUESTO1, IMPUESTO2, OBSERVACIONES, NROFACTURAINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleProveeCompras.Items[i].SubItems[0].Text) + ", (Cast(replace('" + lvwDetalleProveeCompras.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleProveeCompras.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleProveeCompras.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), '0','0', (Cast(replace('" + lvwDetalleProveeCompras.Items[i].SubItems[5].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleProveeCompras.Items[i].SubItems[11].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,3))), (Cast(replace('" + lvwDetalleProveeCompras.Items[i].SubItems[12].Text + "', ',', '.') as decimal(10,2))), '" + txtObserva.Text.Trim() + "', " + Convert.ToInt32(lvwProveeCompras.Items[lvwProveeCompras.Items.Count - 1].SubItems[0].Text) + ")";

                            nroFactuInter = Convert.ToInt32(lvwProveeCompras.Items[lvwProveeCompras.Items.Count - 1].SubItems[0].Text);

                            if (conn.InsertarArticulo(agregarItem))
                            {
                                connGeneric.DesconectarBD();
                                connGeneric.DesconectarBDLeeGeneric();
                            }

                            //////////////////////////////////////////////////////////ACTUALIZA STOCK///////////////////////////////////////////////////////////
                            int iTotalStock;
                            iTotalStock = Convert.ToInt32(lvwDetalleProveeCompras.Items[i].SubItems[3].Text) + CantActualArticulo;

                            string actualizaStock = "CANT_ACTUAL=(Cast(replace('" + iTotalStock + "', ',', '.') as decimal(10,2)))";
                            if (connGeneric.ActualizaGeneric("Articulo", actualizaStock, " IDARTICULO= " + Convert.ToInt32(lvwDetalleProveeCompras.Items[i].SubItems[0].Text) + ""))
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
                    connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleFacturaCompras WHERE NROFACTURAINTERNO = " + nroFactuInter + "", "DetalleFacturaCompras");

                    subTotalfactu = Convert.ToSingle(connGeneric.leerGeneric["SubTotal"].ToString());
                    iva105Factu = Convert.ToSingle(connGeneric.leerGeneric["Iva105"].ToString());
                    iva21Factu = Convert.ToSingle(connGeneric.leerGeneric["Iva21"].ToString());
                    importeFactu = Convert.ToSingle(connGeneric.leerGeneric["Importe"].ToString());

                    string actualizar = "BASICO=(Cast(replace('" + subTotalfactu + "', ',', '.') as decimal(10,3))), IMPUESTOS1=(Cast(replace('" + iva105Factu + "', ',', '.') as decimal(10,3))), IMPUESTOS2 =(Cast(replace('" + iva21Factu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeFactu + "', ',', '.') as decimal(10,2)))";

                    this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeFactu, 2));
                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)iva105Factu, 3));
                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)iva21Factu, 3));
                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)subTotalfactu, 3));

                    if (connGeneric.ActualizaGeneric("FacturasCompras", actualizar, " NROFacturaInterno = " + nroFactuInter + " AND IDEMPRESA = " + IDEMPRESA + ""))
                    {
                        MostrarDatos();
                        MostrarItemsDatos2(nroFactuInter);
                        // MessageBox.Show("La información de la factura ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se ha podido actualizar los datos de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else
                    MessageBox.Show("Error al Agregar Artículo: No se ha ingresado artículo o cantidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { MessageBox.Show("Error falta tipo de impuesto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void frmProveedCompra_Load(object sender, EventArgs e) {
            try {
                gpoComprasAProveedor.Visible = false;
                gpFactuProvee.Width = 983;
                lvwProveeCompras.Width = 950;

                lvwDetalleProveeCompras.Height = 300;

                lblCodArt.Visible = false;
                txtCodArticulo.Visible = false;
                cboArticulo.Visible = false;
                btnAgregaArt.Visible = false;
                btnQuitaArt.Visible = false;
                lblCantidad.Visible = false;
                txtCantidad.Visible= false;
                btnArt.Visible = false;
                //btnGuardar.Visible = false;

                lblPrecio.Visible = false;
                txtPrecio.Visible = false;
                lblDescuento.Visible = false;
                txtProcDesc.Visible = false;

                lblObserva.Visible = false;
                txtObserva.Visible = false;

                lblObserva.Visible = false;
                txtObserva.Visible = false;

                dtpFechaFactu.Value = DateTime.Today;
                fechaFacturaCompra = DateTime.Today;
                
                conn.ConectarBD();
                this.cboBuscaProveeCompras.SelectedIndex = 0;
                
                FormatoListView();

                IDEMPRESA = ConsultaEmpresa(); //Lee Empresa

                MostrarDatos();
            }
            catch { }
            this.reportViewer1.RefreshReport();
        }

        private void FormatoListView() {

            lvwProveeCompras.View = View.Details;
            lvwProveeCompras.LabelEdit = true;
            lvwProveeCompras.AllowColumnReorder = true;
            lvwProveeCompras.FullRowSelect = true;
            lvwProveeCompras.GridLines = true;

            lvwDetalleProveeCompras.View = View.Details;
            lvwDetalleProveeCompras.LabelEdit = true;
            lvwDetalleProveeCompras.AllowColumnReorder = true;
            lvwDetalleProveeCompras.FullRowSelect = true;
            lvwDetalleProveeCompras.GridLines = true;
            
        }

        private void MostrarDatos() {
            try {
                lvwProveeCompras.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT NROFACTURAINTERNO as 'Código', NROFACTURA as 'Nro Fact', fecha, proveedor.RAZONSOCIAL, Basico as 'Básico', DESCUENTOS as 'Desc', IMPUESTOS1 as 'Iva 10,5', IMPUESTOS2 as 'Iva 21', TOTAL as 'Total', .FacturasCompras.OBSERVACIONES as 'Observaciones' FROM FacturasCompras, Proveedor WHERE FacturasCompras.IDEMPRESA = " + IDEMPRESA + " AND proveedor.IDEMPRESA = " + IDEMPRESA + " AND Proveedor.IDPROVEEDOR = FacturasCompras.IDPROVEEDOR ORDER BY NROFACTURAINTERNO", conectaEstado);
                                
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows) {
                    lvwProveeCompras.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Código"].ToString());
                    item.SubItems.Add(dr["Nro Fact"].ToString());
                    item.SubItems.Add(dr["Fecha"].ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString());                    
                    
                    /*item.SubItems.Add("$ " + dr["Básico"].ToString());
                    item.SubItems.Add(dr["Desc"].ToString());
                    item.SubItems.Add("$ " + dr["Iva 10,5"].ToString());
                    item.SubItems.Add("$ " + dr["Iva 21"].ToString());
                    item.SubItems.Add("$ " + dr["Total"].ToString());
                    item.SubItems.Add(dr["Observaciones"].ToString());*/

                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Básico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Desc"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 3).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Iva 21"]), 3).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());
                    

                    if (Convert.ToDateTime(item.SubItems[2].Text).AddDays(365) <= DateTime.Today)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;
                    item.UseItemStyleForSubItems = false; 
                    lvwProveeCompras.Items.Add(item);
                }
                cm.Connection.Close();            
            }
            catch { }
        }
        
        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnProveedor_Click(object sender, EventArgs e) {
            frmProveedores formProveed = new frmProveedores();
            formProveed.pasadoProvee1 += new frmProveedores.pasarProvee1(CodProvee);  //Delegado11 Rubro Articulo
            formProveed.pasadoProvee2 += new frmProveedores.pasarProvee2(RSProvee); //Delegado2 Rubro Articulo

            txtCodPersonal.Focus();

            formProveed.ShowDialog();
        }

        //Metodos de delegado Proveedor
        public void CodProvee(int dato1) {
            this.txtCodProveedor.Text = dato1.ToString();
        }

        public void RSProvee(string dato2) {
            this.cboProveedor.Text = dato2.ToString();
        }
        //////////////////////

        private void btnCodTipoFactura_Click(object sender, EventArgs e) {
            frmTipoFactura frmTipoFact = new frmTipoFactura();
            frmTipoFact.pasadoTipoFactu1 += new frmTipoFactura.pasarTipoFactura1(CodTipoFactu);  //Delegado11 Rubro Articulo
            frmTipoFact.pasadoTipoFactu2 += new frmTipoFactura.pasarTipoFactura2(DTipoFactu); //Delegado2 Rubro Articulo

            txtObservacionFactura.Focus();

            frmTipoFact.ShowDialog();
        }

        //Metodos de delegado Proveedor
        public void CodTipoFactu(int dato1) {
            this.txtCodTipoFactura.Text = dato1.ToString();
        }

        public void DTipoFactu(string dato2) {
            this.cboTipoFactura.Text = dato2.ToString();
        }

        private void btnCodigoPersonal_Click(object sender, EventArgs e) {
            frmPersonal frmPerso = new frmPersonal();
            frmPerso.pasadoPerso1 += new frmPersonal.pasarPersona1(CodPPerso);  //Delegado11 Rubro Articulo
            frmPerso.pasadoPerso2 += new frmPersonal.pasarPersona2(RSPerso); //Delegado2 Rubro Articulo

            txtCodTipoFactura.Focus();

            frmPerso.ShowDialog();
        }

        //Metodos de delegado Proveedor
        public void CodPPerso(int dato1) {
            this.txtCodPersonal.Text = dato1.ToString();
        }

        public void RSPerso(string dato2) {
            this.cboPersonal.Text = dato2.ToString();
        }
     
        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            NuevaFactura(); 
        }

        private void NuevaFactura()
        {
            try
            {
                nuevaFactu = true;
                timer1.Enabled = true;

                dSubTotal = 0.00;
                dImpuesto1 = 0.00;
                dImpuesto2 = 0.00;
                dImporteTotal = 0.00;

                dtpFechaFactu.Value = DateTime.Today;
                fechaFacturaCompra = DateTime.Today;

                lvwDetalleProveeCompras.Items.Clear();
                gpoComprasAProveedor.Visible = true;
                gpFactuProvee.Width = 261;
                lvwProveeCompras.Width = 234;
                lvwDetalleProveeCompras.Height = 240;

                this.cboBuscaProveeCompras.SelectedIndex = 0;

                lblCodArt.Visible = true;
                txtCodArticulo.Visible = true;
                cboArticulo.Visible = true;
                btnAgregaArt.Visible = true;
                btnQuitaArt.Visible = true;
                lblCantidad.Visible = true;
                txtCantidad.Visible = true;
                btnArt.Visible = true;

                btnEliminar.Enabled = false;
                //tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;
                //btnModificar.Enabled = false;
                //btnGuardar.Enabled = true;
                //btnGuardar.Visible = true;

                lblPrecio.Visible = true;
                txtPrecio.Visible = true;
                lblDescuento.Visible = true;
                txtProcDesc.Visible = true;

                lblObserva.Visible = true;
                txtObserva.Visible = true;

                lblObserva.Visible = true;
                txtObserva.Visible = true;

                this.txtCantidad.Text = "";
                this.txtNroInternoFact.Text = "";
                this.txtNroFactura.Text = "";
                this.txtObservacionFactura.Text = "";
                this.cboNroSucursal.SelectedIndex = 0;
                this.txtIva.Text = "";
                this.txtCodArticulo.Text = "";
                this.txtCodPersonal.Text = "";
                this.txtCodProveedor.Text = "";
                this.txtCodTipoFactura.Text = "";
                this.txtCuit.Text = "";
                this.txtDescuento.Text = "$ 0.000";
                this.txtSubTotal.Text = "$ 0.000";
                this.txtImpuesto1.Text = "$ 0.000";
                this.txtImpuesto2.Text = "$ 0.000";
                this.txtTotalFactur.Text = "$ 0.00";
                this.txtObserva.Text = "";

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                /////////////////////////////////////////// AUTONUMERICO NRO INTERNO //////////////////////////////////////////
                connGeneric.LeeGeneric("SELECT MAX(NROFACTURAINTERNO)  as Codigo FROM FacturasCompras", "FacturasCompras");

                if (connGeneric.leerGeneric["Codigo"].ToString() == "")
                    txtNroInternoFact.Text = "0";
                else
                    txtNroInternoFact.Text = connGeneric.leerGeneric["Codigo"].ToString();

                contadorNROfactNuevo = (Convert.ToInt32(txtNroInternoFact.Text));
                contadorNROfactNuevo = contadorNROfactNuevo + 1;
                txtNroInternoFact.Text = contadorNROfactNuevo.ToString();

                ValidaNumerador(this.txtNroFactura.Text.Trim());

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                /////////////////////////////////////////// AUTONUMERICO NRO FACTURA //////////////////////////////////////////
                /*  connGeneric.LeeGeneric("SELECT MAX(NROFACTURA)  as NroFactu FROM FacturasCompras", "FacturasCompras");

                  if (connGeneric.leerGeneric["NroFactu"].ToString() == "")
                      txtNroFactura.Text = "0";
                  else
                      txtNroFactura.Text = connGeneric.leerGeneric["NroFactu"].ToString();

                  nuevaFacturaProvee = (Convert.ToInt32(txtNroFactura.Text));
                  nuevaFacturaProvee = nuevaFacturaProvee + 1;
                  txtNroFactura.Text = nuevaFacturaProvee.ToString(); */
                ///////////////////////////////////////////////////////////////////////////////////////////////////

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();
                //////////////////////////////////////////////////////////////////////////////////////////////////

            }
            catch { MessageBox.Show("Error: El nro ingresado no es numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private bool ValidaNumerador(string nrocomprobante)
        {
            try
            {
                SqlCommand cm = new SqlCommand("SELECT NROFACTURA FROM FacturasCompras WHERE IDEMPRESA = " + IDEMPRESA + "", conectaEstado);

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

        private void btnCerrar_Click(object sender, EventArgs e) {
            gpoComprasAProveedor.Visible = false;
            gpFactuProvee.Width = 983;
            lvwProveeCompras.Width = 950;
            lvwDetalleProveeCompras.Height = 300;

            lblCodArt.Visible = false;
            txtCodArticulo.Visible = false;
            cboArticulo.Visible = false;
            btnAgregaArt.Visible = false;
            btnQuitaArt.Visible = false;
            lblCantidad.Visible = false;
            txtCantidad.Visible = false;
            btnArt.Visible = false;

            lblPrecio.Visible = false;
            txtPrecio.Visible = false;
            lblDescuento.Visible = false;
            txtProcDesc.Visible = false;

            tsBtnNuevo.Enabled = true;
            //tsBtnModificar.Enabled = true;
            //btnEliminar.Enabled = false;
            //btnModificar.Enabled = true;
            //btnGuardar.Enabled = true;
            //btnGuardar.Visible = false;

            lblObserva.Visible = false;
            txtObserva.Visible = false;
        }

        private void btnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e) {
            try {
                timer1.Enabled = false;
                string actualizar = "NROFACTURA='" + txtNroFactura.Text.Trim() + "', SUCURSAL='" + cboNroSucursal.Text.Trim() + "', FECHA='" + dtpFechaFactu.Text.Trim() + "', IDTIPOFACTURA=" + txtCodTipoFactura.Text.Trim() + " , IDPROVEEDOR=" + txtCodProveedor.Text.Trim() + ", IDPERSONAL=" + txtCodPersonal.Text.Trim() + ", OBSERVACIONES='" + txtObservacionFactura.Text.Trim() + "'";

                if (connGeneric.ActualizaGeneric("FacturasCompras", actualizar, " NROFacturaInterno = " + Convert.ToInt32(txtNroInternoFact.Text) + " AND IDEMPRESA = " + IDEMPRESA + "")) {
                    MostrarDatos();
                    MostrarItemsDatos2(Convert.ToInt32(txtNroInternoFact.Text));
                    MessageBox.Show("La información de la factura ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("No se ha podido actualizar los datos de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { MessageBox.Show("Error: No se ha podido actualizar la información de la factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnEliminar_Click(object sender, EventArgs e) {
            try {
                tsBtnNuevo.Enabled = true;
                //btnGuardar.Enabled = false;
                timer1.Enabled = false;

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                if (fechaFacturaCompra.AddDays(365) <= DateTime.Today)
                    MessageBox.Show("No se puede eliminar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else {
                    if (connGeneric.EliminarGeneric("FacturasCompras", " IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + Convert.ToInt32(this.txtNroInternoFact.Text))) {
                        MostrarDatos();

                        tsBtnNuevo.Enabled = true;
                        btnEliminar.Enabled = true;
                        //btnGuardar.Enabled = true;

                        this.txtCantidad.Text = "";
                        this.txtNroInternoFact.Text = "";
                        this.txtNroFactura.Text = "";
                        this.txtObservacionFactura.Text = "";
                        this.cboNroSucursal.SelectedIndex = 0;
                        this.txtIva.Text = "";
                        this.txtCodArticulo.Text = "";
                        this.txtCodPersonal.Text = "";
                        this.txtCodProveedor.Text = "";
                        this.txtCodTipoFactura.Text = "";
                        this.txtCuit.Text = "";
                        this.txtDescuento.Text = "$ 0.00";
                        this.txtSubTotal.Text = "$ 0.00";
                        this.txtImpuesto1.Text = "$ 0.00";
                        this.txtImpuesto2.Text = "$ 0.00";
                        this.txtTotalFactur.Text = "$ 0.00";
                        this.txtObserva.Text="";
                        MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        timer1.Enabled = true;

                        actualizaStock_Anulacion_o_eliminacion(idNROFACTUINTERNO, lvwDetalleProveeCompras.Items[0].SubItems[1].Text.Trim());
                    }
                    else
                        MessageBox.Show("Error al Eliminar, seleccionar factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { MessageBox.Show("Error: Seleccione la factura a eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lvwProveeCompras_SelectedIndexChanged(object sender, EventArgs e)  {
            try {
                timer1.Enabled = false;
                //MostrarItemsDatos();

                conn.DesconectarBDLee();
                conn.DesconectarBD();

                nuevaFactu = false;

                idNROFACTUINTERNO = Convert.ToInt32(lvwProveeCompras.SelectedItems[0].SubItems[0].Text);
                indiceLvwCompraProvee = lvwProveeCompras.SelectedItems[0].Index;

                conn.LeeArticulo("SELECT * FROM FacturasCompras WHERE NroFacturaInterno = " + Convert.ToInt32(lvwProveeCompras.SelectedItems[0].SubItems[0].Text) + " AND IDEMPRESA = "+ IDEMPRESA +"", "FacturasCompras");

                this.txtNroInternoFact.Text = conn.leer["NroFacturaInterno"].ToString();
                this.txtNroFactura.Text = conn.leer["NROFACTURA"].ToString();
                this.cboNroSucursal.Text = conn.leer["SUCURSAL"].ToString();
                this.dtpFechaFactu.Value = Convert.ToDateTime(conn.leer["FECHA"].ToString());

                fechaFacturaCompra = Convert.ToDateTime(conn.leer["FECHA"].ToString());

                this.txtCodProveedor.Text = conn.leer["IDPROVEEDOR"].ToString();
                if (this.txtCodProveedor.Text.Trim() == "")
                    this.cboProveedor.Text = "";

                this.txtCodTipoFactura.Text = conn.leer["IDTIPOFACTURA"].ToString();
                if (this.txtCodTipoFactura.Text.Trim() == "")
                    this.cboTipoFactura.Text = "";

                this.txtCodPersonal.Text = conn.leer["IDPERSONAL"].ToString();
                if (this.txtCodPersonal.Text.Trim() == "")
                    this.cboPersonal.Text = "";

                this.txtObservacionFactura.Text = conn.leer["OBSERVACIONES"].ToString();

                this.txtSubTotal.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leer["BASICO"]), 3).ToString();
                this.txtImpuesto1.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leer["IMPUESTOS1"]), 3).ToString();
                this.txtImpuesto2.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leer["IMPUESTOS2"]), 3).ToString();
                this.txtDescuento.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leer["DESCUENTOS"]), 3).ToString();
                this.txtTotalFactur.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leer["TOTAL"]), 2).ToString();
                
                conn.DesconectarBDLee();
                conn.DesconectarBD();

                btnEliminar.Enabled = true;
                //btnGuardar.Enabled = true;
                MostrarItemsDatos();

              //  if (fechaFacturaCompra.AddDays(1) <= DateTime.Today)                                
              //  MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
            catch { conn.DesconectarBD(); } 
        }

        private void MostrarItemsDatos()
        {
            try
            {
                lvwDetalleProveeCompras.Items.Clear();

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                double iva105;
                double iva21;

                connGeneric.LeeGeneric("SELECT DetalleFacturaCompras.IDDETALLEFACTURACOMPRAS as 'Código', Articulo.DESCRIPCION as 'Artículo', Articulo.CANT_ACTUAL, DetalleFacturaCompras.CANTIDAD as 'Cant', DetalleFacturaCompras.PRECUNITARIO as 'Precio Unitario', DetalleFacturaCompras.IMPORTE as 'Importe', DetalleFacturaCompras.DESCUENTO as 'Descuento', DetalleFacturaCompras.PORCDESC as '% Desc', DetalleFacturaCompras.SUBTOTAL as 'Subtotal', DetalleFacturaCompras.IMPUESTO1 as 'Iva 10,5', DetalleFacturaCompras.IMPUESTO2 as 'Iva 21', DetalleFacturaCompras.OBSERVACIONES as 'Observaciones' FROM FacturasCompras, DetalleFacturaCompras, Articulo, Proveedor, TipoFactura,Personal WHERE DetalleFacturaCompras.IDARTICULO = Articulo.IDARTICULO AND FacturasCompras.IDTIPOFACTURA = TipoFactura.IDTIPOFACTURA AND FacturasCompras.IDPROVEEDOR = Proveedor.IDPROVEEDOR AND FacturasCompras.IDPERSONAL = Personal.IDPERSONAL AND DetalleFacturaCompras.NROFACTURAINTERNO = FacturasCompras.NROFACTURAINTERNO AND FacturasCompras.NROFACTURAINTERNO = " + Convert.ToInt32(lvwProveeCompras.SelectedItems[0].SubItems[0].Text) + "", "FacturasCompras");

                iva105 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 10,5"].ToString());
                iva21 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 21"].ToString());

                SqlCommand cm = new SqlCommand("SELECT Articulo.Codigo as 'Código', DetalleFacturaCompras.IdDetalleFacturaCompras, DetalleFacturaCompras.IDArticulo as 'Código Artículo', Articulo.DESCRIPCION as 'Artículo', Articulo.CANT_ACTUAL, DetalleFacturaCompras.CANTIDAD as 'Cant', DetalleFacturaCompras.PRECUNITARIO as 'Precio Unitario', DetalleFacturaCompras.IMPORTE as 'Importe', DetalleFacturaCompras.DESCUENTO as 'Descuento', DetalleFacturaCompras.PORCDESC as '% Desc', DetalleFacturaCompras.SUBTOTAL as 'Subtotal', DetalleFacturaCompras.IMPUESTO1 as 'Iva 10,5', DetalleFacturaCompras.IMPUESTO2 as 'Iva 21', DetalleFacturaCompras.OBSERVACIONES as 'Observaciones' FROM FacturasCompras, DetalleFacturaCompras, Articulo, Proveedor, TipoFactura,Personal WHERE DetalleFacturaCompras.IDARTICULO = Articulo.IDARTICULO AND FacturasCompras.IDTIPOFACTURA = TipoFactura.IDTIPOFACTURA AND FacturasCompras.IDPROVEEDOR = Proveedor.IDPROVEEDOR AND FacturasCompras.IDPERSONAL = Personal.IDPERSONAL AND DetalleFacturaCompras.NROFACTURAINTERNO = FacturasCompras.NROFACTURAINTERNO AND FacturasCompras.NROFACTURAINTERNO = " + Convert.ToInt32(lvwProveeCompras.SelectedItems[0].SubItems[0].Text) + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwDetalleProveeCompras.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Código Artículo"].ToString());
                    item.SubItems.Add(dr["Código"].ToString());
                    item.SubItems.Add(dr["Artículo"].ToString());
                    item.SubItems.Add(dr["Cant"].ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Precio Unitario"]), 3).ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Subtotal"]), 3).ToString());
                    item.SubItems.Add(dr["Descuento"].ToString());

                    if (dr["Iva 10,5"].ToString() != "0,0000")
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 3).ToString());
                    else
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 21"]), 3).ToString());

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPORTE"]), 2).ToString());

                    item.SubItems.Add(dr["Observaciones"].ToString());
                    item.SubItems.Add(dr["IdDetalleFacturaCompras"].ToString());
                    item.SubItems.Add(dr["Iva 10,5"].ToString());
                    item.SubItems.Add(dr["Iva 21"].ToString());

                    item.SubItems.Add(dr["CANT_ACTUAL"].ToString());

                    if (dr["CANT_ACTUAL"].ToString() == "0")
                        item.ImageIndex = 3;
                    else
                        item.ImageIndex = 2;

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleProveeCompras.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void txtCodProveedor_TextChanged(object sender, EventArgs e) {
            try {
                if (this.txtCodProveedor.Text.Trim() != "")
                {
                    connGeneric.ConsultaGeneric("Select IDProveedor As 'Código', RazonSocial AS 'Razón Social' FROM Proveedor WHERE IDProveedor = " + this.txtCodProveedor.Text + "", "Proveedor");

                    this.cboProveedor.DataSource = connGeneric.ds.Tables[0];
                    this.cboProveedor.ValueMember = "Código";
                    this.cboProveedor.DisplayMember = "Razón Social";

                    connGeneric.DesconectarBD();
                    connGeneric.DesconectarBDLeeGeneric();

                    connGeneric.LeeGeneric("SELECT Proveedor.NRODECUIT, TipoIva.DESCRIPCION, TipoIva.IdTipoIva FROM Proveedor, TipoIva WHERE Proveedor.IDTIPOIVA = TipoIva.IDTIPOIVA AND Proveedor.IDPROVEEDOR = " + this.txtCodProveedor.Text + "", "Proveedor");

                    txtCuit.Text = connGeneric.leerGeneric["NRODECUIT"].ToString();
                    txtIva.Text = connGeneric.leerGeneric["DESCRIPCION"].ToString();

                    txtCodTipoFactura.Text = connGeneric.leerGeneric["IdTipoIva"].ToString();

                    if (connGeneric.leerGeneric["IdTipoIva"].ToString() == "1")
                        cboTipoFactura.Text = "A";
                    else
                        cboTipoFactura.Text = "C";
                }
                else
                {
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    txtCodTipoFactura.Text = "";
                    cboTipoFactura.Text = "";
                    txtCodProveedor.Text = "";
                    cboProveedor.Text = "";
                }
            }
            catch { 
                MessageBox.Show("Error: Falta informacion relacionada con el proveedor (CUIT)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                    txtCuit.Text="";
                    txtIva.Text ="";
                    txtCodTipoFactura.Text = "";
                    cboTipoFactura.Text = "";
                    txtCodProveedor.Text="";
                    cboProveedor.Text = "";                    
            }
        }

        private void txtCodPersonal_TextChanged(object sender, EventArgs e) {
            try {
                if (this.txtCodPersonal.Text.Trim() != "") {
                    connGeneric.ConsultaGeneric("Select IDPERSONAL, NOMBREYAPELLIDO FROM Personal WHERE IDPersonal = " + this.txtCodPersonal.Text + "", "Personal");

                    this.cboPersonal.DataSource = connGeneric.ds.Tables[0];
                    this.cboPersonal.ValueMember = "IDPERSONAL";
                    this.cboPersonal.DisplayMember = "NOMBREYAPELLIDO";
                }
                else {
                    cboPersonal.Text = "";
                    txtCantidad.Text = "0";
                    txtProcDesc.Text = "0,00 %";
                    txtPrecio.Text = "$ 0,00";
                    txtObserva.Text = "";
                }
            }
            catch { }
        }

        private void txtCodArticulo_TextChanged(object sender, EventArgs e) {
           
           try {

                string sCodArt;
                char[] QuitaSimbolo = { '$', ' ' };
                char[] QuitaSimbolo2 = { '*', ' ' };

                sCodArt = txtCodArticulo.Text.TrimStart(QuitaSimbolo2);
                sCodArt = sCodArt.TrimEnd(QuitaSimbolo2);

                this.txtCodArticulo.Text = sCodArt;

                if (this.txtCodArticulo.Text.Trim() != "")
                {
                    connGeneric.LeeGeneric("Select * FROM Articulo WHERE CODIGO = '" + this.txtCodArticulo.Text.Trim() + "'", "Articulo");
                    //this.txtCodArticulo.Text = connGeneric.leerGeneric["CODIGO"].ToString();
                    this.cboArticulo.Text = connGeneric.leerGeneric["DESCRIPCION"].ToString();
                    this.txtPrecio.Text = "$ " + Math.Round(Convert.ToDecimal(connGeneric.leerGeneric["COSTO"].ToString()), 3);



                    connGeneric.DesconectarBDLeeGeneric();
                }
                else
                {
                    cboArticulo.Text = "";
                    txtCantidad.Text = "";
                    txtPrecio.Text = "";
                    txtDescuento.Text = "0";
                    txtObserva.Text="";
                }
            }
            catch { connGeneric.DesconectarBDLeeGeneric(); }   
        }

        private void btnArt_Click(object sender, EventArgs e) {

            txtCodArticulo.Text = "";
            cboArticulo.Text = "";
            txtCantidad.Text = "";
            txtProcDesc.Text = "";
            txtPrecio.Text = "";
            txtObserva.Text = "";

            frmArticulo frmArt = new frmArticulo();
            frmArt.pasadoArt1 += new frmArticulo.pasarArticulo1(CodPArt);  //Delegado1 Familia Articulo
            frmArt.pasadoArt2 += new frmArticulo.pasarArticulo2(RSArt); //Delegado2 Familia Articulo
            frmArt.pasadoArt3 += new frmArticulo.pasarArticulo3(Costo); //Delegado2 Familia Articulo

            txtCantidad.Focus();

            frmArt.ShowDialog();
        }

        //Metodos de delegado Rubro Proveedor
        public void CodPArt(string dato1) {
            this.txtCodArticulo.Text = dato1.ToString();
        }

        public void RSArt(string dato2) {
            this.cboArticulo.Text = dato2.ToString();
        }

        public void Costo(string dato3) {
            this.txtPrecio.Text = dato3.ToString();
        }

        private void btnAgregaArt_Click(object sender, EventArgs e) {            
            if (fechaFacturaCompra.AddDays(365) <= DateTime.Today)
                MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else {

                timer1.Enabled = true;
                GuardarTodosLosDatos();                
                txtCodArticulo.Text = "";
                cboArticulo.Text = "";
                txtCantidad.Text = "";
                txtProcDesc.Text = "";
                txtPrecio.Text = "";
                txtObserva.Text = "";
            }
        }

        private void btnQuitaArt_Click(object sender, EventArgs e) {
            try {
                int iIndex = 0;

                if (fechaFacturaCompra.AddDays(365) <= DateTime.Today)
                    MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else {
                    timer1.Enabled = false;


                    actualizaStock_Anulacion_o_eliminacion(idNROFACTUINTERNO, lvwDetalleProveeCompras.SelectedItems[0].SubItems[1].Text.Trim());

                    iIndex = Convert.ToInt32(lvwDetalleProveeCompras.SelectedItems[0].SubItems[10].Text);  //Elemento de la base de datos
                    lvwDetalleProveeCompras.Items[lvwDetalleProveeCompras.SelectedItems[0].Index].Remove(); //Elemento del listview

                    if (conn.EliminarArticulo("DetalleFacturaCompras", " IdDetalleFacturaCompras = " + iIndex))
                        //MostrarItemsDatos2(idNROFACTUINTERNO);
                    
                    if (lvwDetalleProveeCompras.Items.Count != 0) {      
                        if (iIndex != 0) {
                            string subTotalfactu;
                            string iva105Factu;
                            string iva21Factu;
                            string importeFactu;

                            connGeneric.DesconectarBDLeeGeneric();
                            connGeneric.LeeGeneric("Select  Sum(SUBTOTAL) as 'SubTotal', Sum(IMPORTE) as 'Importe', Sum(Impuesto1) as 'Iva105', Sum(IMPUESTO2) as 'Iva21' FROM DetalleFacturaCompras WHERE NROFACTURAINTERNO = " + idNROFACTUINTERNO + "", "DetalleFacturaCompras");

                            importeFactu = connGeneric.leerGeneric["Importe"].ToString();
                            iva105Factu = connGeneric.leerGeneric["Iva105"].ToString();
                            iva21Factu = connGeneric.leerGeneric["Iva21"].ToString();
                            subTotalfactu = connGeneric.leerGeneric["SubTotal"].ToString();

                            string actualizar = "BASICO=(Cast(replace('" + subTotalfactu + "', ',', '.') as decimal(10,3))), IMPUESTOS1=(Cast(replace('" + iva105Factu + "', ',', '.') as decimal(10,3))), IMPUESTOS2 =(Cast(replace('" + iva21Factu + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + importeFactu + "', ',', '.') as decimal(10,2)))";
                                                       

                            this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(importeFactu), 2));
                            this.txtImpuesto1.Text = "$ " + String.Format("{0:0.000}", Decimal.Round(Convert.ToDecimal(iva105Factu), 3));
                            this.txtImpuesto2.Text = "$ " + String.Format("{0:0.000}", Decimal.Round(Convert.ToDecimal(iva21Factu), 3));
                            this.txtSubTotal.Text = "$ " + String.Format("{0:0.000}", Decimal.Round(Convert.ToDecimal(subTotalfactu), 3));

                            if (connGeneric.ActualizaGeneric("FacturasCompras", actualizar, " NROFacturaInterno = " + idNROFACTUINTERNO + " AND IDEMPRESA = " + IDEMPRESA +"")) {
                                MostrarDatos();
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

                        for (int i = 0; i < (lvwDetalleProveeCompras.Items.Count); i++) {
                            dSubTotal += Math.Round(Convert.ToSingle(lvwDetalleProveeCompras.Items[i].SubItems[8].Text.TrimStart(QuitaSimbolo)), 3);
                            dImpuesto1 += Math.Round(Convert.ToSingle(lvwDetalleProveeCompras.Items[i].SubItems[11].Text.TrimStart(QuitaSimbolo)), 3);
                            dImpuesto2 += Math.Round(Convert.ToSingle(lvwDetalleProveeCompras.Items[i].SubItems[12].Text.TrimStart(QuitaSimbolo)), 3);
                            dImporteTotal += Math.Round(Convert.ToSingle(lvwDetalleProveeCompras.Items[i].SubItems[4].Text.TrimStart(QuitaSimbolo)), 2);
                        }

                        this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImporteTotal, 2));
                        this.txtImpuesto1.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)dImpuesto1, 3));
                        this.txtImpuesto2.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)dImpuesto2, 3));
                        this.txtSubTotal.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)dSubTotal, 3));
                    }
                    else {
                        this.txtTotalFactur.Text = "$ " + "0,00";
                        this.txtImpuesto1.Text = "$ " + "0,000";
                        this.txtImpuesto2.Text = "$ " + "0,000";
                        this.txtSubTotal.Text = "$ " + "0,000";

                        string actualizar = "BASICO=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,3))), IMPUESTOS1=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,3))), IMPUESTOS2 =(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,3))), TOTAL=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,2)))";
                        connGeneric.ActualizaGeneric("FacturasCompras", actualizar, " NROFacturaInterno = " + idNROFACTUINTERNO + " AND IDEMPRESA = " + IDEMPRESA + "");
                        MostrarDatos();
                        MostrarItemsDatos2(idNROFACTUINTERNO);
                    }
                }
            }
            catch { conn.DesconectarBD(); MostrarItemsDatos(); }
        }

        private void tsBtnModificar_Click(object sender, EventArgs e) {
            
            nuevaFactu = false;
            timer1.Enabled = false;

            if (lvwProveeCompras.SelectedItems.Count == 0)
                MessageBox.Show("Error: No se ha seleccionado ninguna factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                gpoComprasAProveedor.Visible = true;
                gpFactuProvee.Width = 261;
                lvwProveeCompras.Width = 234;
                lvwDetalleProveeCompras.Height = 240;

                lblCodArt.Visible = true;
                txtCodArticulo.Visible = true;
                cboArticulo.Visible = true;
                btnAgregaArt.Visible = true;
                btnQuitaArt.Visible = true;
                lblCantidad.Visible = true;
                txtCantidad.Visible = true;
                btnArt.Visible = true;

                btnEliminar.Enabled = true;                
                tsBtnNuevo.Enabled = true;                
                //btnGuardar.Enabled = true;
                //btnGuardar.Visible = true;

                lblPrecio.Visible = true;
                txtPrecio.Visible = true;
                lblDescuento.Visible = true;
                txtProcDesc.Visible = true;

                lblObserva.Visible = true;
                txtObserva.Visible = true;
            }
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e)
        {
            //Modulo de Consulta y Reporte
        }

        private void GuardarTodosLosDatos() {
            try {
                    float subTotal;
                    float impuesto1; float impuesto2;
                    float descuento; float importeTotal;

                    //Quita Simbolos para guardar los datos en formato numéricos
                    char[] QuitaSimbolo = { '$', ' ' };
                    subTotal = Convert.ToSingle(this.txtTotalFactur.Text.TrimStart(QuitaSimbolo));
                    impuesto1 = Convert.ToSingle(this.txtImpuesto1.Text.TrimStart(QuitaSimbolo));
                    impuesto2 = Convert.ToSingle(this.txtImpuesto2.Text.TrimStart(QuitaSimbolo));
                    importeTotal = Convert.ToSingle(this.txtSubTotal.Text.TrimStart(QuitaSimbolo));
                    descuento = Convert.ToSingle(this.txtDescuento.Text.TrimStart(QuitaSimbolo));
                    /////////////////////////////////////////////////////////////////////////////////

                    connGeneric.DesconectarBD();
                    connGeneric.DesconectarBDLeeGeneric();
                    conn.DesconectarBD();

                if (nuevaFactu == true)
                    connGeneric.ConsultaGeneric("Select * FROM FacturasCompras WHERE NROFACTURAINTERNO = " + Convert.ToInt32(txtNroInternoFact.Text) + " AND IDEMPRESA = " + IDEMPRESA + "", "FacturasCompras");
                else
                    connGeneric.ConsultaGeneric("Select * FROM FacturasCompras WHERE NROFACTURAINTERNO = " + idNROFACTUINTERNO + " AND IDEMPRESA = " + IDEMPRESA + "", "FacturasCompras");

                if (connGeneric.ds.Tables[0].Rows.Count == 0)
                {
                    string agregar = "INSERT INTO FacturasCompras(NROFACTURA, SUCURSAL, FECHA, IDTIPOFACTURA, IDPROVEEDOR, IDPERSONAL, BASICO, DESCUENTOS, IMPUESTOS1, IMPUESTOS2, TOTAL, OBSERVACIONES, IDEMPRESA) VALUES ('" + txtNroFactura.Text.Trim() + "', '" + cboNroSucursal.Text.Trim() + "', '" + dtpFechaFactu.Text.Trim() + "', " + txtCodTipoFactura.Text.Trim() + ", " + txtCodProveedor.Text.Trim() + ", " + txtCodPersonal.Text.Trim() + ", (Cast(replace('" + subTotal + "', ',', '.') as decimal(10,3))), " + descuento + " , (Cast(replace('" + impuesto1 + "', ',', '.') as decimal(10,3))), (Cast(replace('" + impuesto2 + "', ',', '.') as decimal(10,3))), (Cast(replace('" + importeTotal + "', ',', '.') as decimal(10,2))), '" + txtObservacionFactura.Text + "', " + IDEMPRESA +")";

                    this.txtTotalFactur.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeTotal, 2));
                    this.txtImpuesto1.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)impuesto1, 3));
                    this.txtImpuesto2.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)impuesto2, 3));
                    this.txtSubTotal.Text = "$ " + String.Format("{0:0.000}", Decimal.Round((decimal)subTotal, 3));

                    if (conn.InsertarArticulo(agregar))
                    {
                        MostrarDatos();
                        GuardaItemsDatos(true, 0);

                        lvwProveeCompras.Items[lvwProveeCompras.Items.Count - 1].Selected = true;
                        txtNroInternoFact.Text = lvwProveeCompras.Items[lvwProveeCompras.Items.Count - 1].Text;
                        idNROFACTUINTERNO = Convert.ToInt32(lvwProveeCompras.Items[lvwProveeCompras.Items.Count - 1].Text);
                        //indiceLvwCompraProvee = lvwProveeCompras.Items[0].Index;
                        //MessageBox.Show("Factura Guardada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    GuardaItemsDatos(false, idNROFACTUINTERNO);                

            }
            catch { conn.DesconectarBD(); connGeneric.DesconectarBD(); connGeneric.DesconectarBDLeeGeneric(); }         
        }

        private void lvwDetalleProveeCompras_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                conn.DesconectarBDLee();
                conn.DesconectarBD();

                conn.LeeArticulo("SELECT * FROM ARTICULO WHERE IDARTICULO = " + Convert.ToInt32(lvwDetalleProveeCompras.SelectedItems[0].SubItems[0].Text) + "", "ARTICULO");

                this.txtCodArticulo.Text = conn.leer["CODIGO"].ToString();
                this.cboArticulo.Text = conn.leer["DESCRIPCION"].ToString();

                this.txtCantidad.Text = this.lvwDetalleProveeCompras.SelectedItems[0].SubItems[3].Text;
                this.txtProcDesc.Text = this.lvwDetalleProveeCompras.SelectedItems[0].SubItems[6].Text;
                
                this.txtPrecio.Text = conn.leer["COSTO"].ToString();
                //this.dtpFechaFactu.Value = Convert.ToDateTime(conn.leer["FECHA"].ToString());

                conn.DesconectarBDLee();
                conn.DesconectarBD();

                conn.LeeArticulo("SELECT * FROM DetalleFacturaCompras WHERE IdDetalleFacturaCompras = " + Convert.ToInt32(lvwDetalleProveeCompras.SelectedItems[0].SubItems[10].Text) + "", "DetalleFacturaCompras");
                this.txtObserva.Text = conn.leer["OBSERVACIONES"].ToString();

                conn.DesconectarBDLee();
                conn.DesconectarBD();

                btnEliminar.Enabled = true;
                //btnGuardar.Enabled = true;
                MostrarItemsDatos();
            }
            catch { conn.DesconectarBD(); } 
        }

        private void txtProcDesc_Enter(object sender, EventArgs e)
        {
            char[] QuitaSimbolo = { '%', ' ' };
            this.txtProcDesc.Text = this.txtProcDesc.Text.TrimEnd(QuitaSimbolo);
        }

        private void txtPrecio_Enter(object sender, EventArgs e)
        {
           char[] QuitaSimbolo = { '$', ' ' };
           this.txtPrecio.Text = this.txtPrecio.Text.TrimStart(QuitaSimbolo);
        }

        private void txtProcDesc_Leave(object sender, EventArgs e)
        {
            this.txtProcDesc.Text = "% " + this.txtProcDesc.Text.Trim();
            this.txtProcDesc.Text = this.txtProcDesc.Text.Trim();
        }

        private void txtPrecio_Leave(object sender, EventArgs e) {
            try {
                this.txtPrecio.Text = "$ " + this.txtPrecio.Text.Trim();
                this.txtPrecio.Text = this.txtPrecio.Text.Trim();

                double precioArticuloActual;
                double precioIngresado;

                char[] QuitaSimbolo = { '$', ' ' };
                precioIngresado = Math.Round(Convert.ToSingle(txtPrecio.Text.TrimStart(QuitaSimbolo)), 3);

                connGeneric.DesconectarBDLeeGeneric();
                connGeneric.LeeGeneric("SELECT * FROM Articulo WHERE CODIGO = '" + txtCodArticulo.Text.Trim() + "'", "Articulo");
                precioArticuloActual = Math.Round(Convert.ToSingle(connGeneric.leerGeneric["COSTO"].ToString()), 3);

                if (precioIngresado > precioArticuloActual) {
                    DialogResult result = MessageBox.Show("El precio ingresado es mayor al precio actual ¿Desea cambiarlo? Si es asi recuerde volver a pulsar en agregar artículo para confirmar el cambio de precio.", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    switch (result) {
                        case DialogResult.Yes: {
                                ///ACTUALIZA PRECIO - Resume para "[Yes]";///
                                string actualizar = "COSTO=(Cast(replace('" + precioIngresado + "', ',', '.') as decimal(10,2)))";
                                connGeneric.ActualizaGeneric("Articulo", actualizar, " CODIGO = '" + txtCodArticulo.Text.Trim() + "'");
                                break;
                            }
                        case DialogResult.No: {
                                this.Text = "[Cancel]";
                                break;
                            }
                    }
                }
                /*else if (precioIngresado < precioArticuloActual) {
                    
                    string actualizar = "COSTO=(Cast(replace('" + precioIngresado + "', '.', ',') as decimal(10,2)))";
                    connGeneric.ActualizaGeneric("Articulo", actualizar, " CODIGO = '" + txtCodArticulo.Text.Trim() + "'");
                }*/
            }
            catch { } 
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {                      

        }

        private void MostrarItemsDatos2(int NROFACTURAINTERNO) {
            try {
                lvwDetalleProveeCompras.Items.Clear();

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                double iva105;
                double iva21;

                connGeneric.LeeGeneric("SELECT DetalleFacturaCompras.IDDETALLEFACTURACOMPRAS as 'Código', Articulo.DESCRIPCION as 'Artículo', Articulo.CANT_ACTUAL, DetalleFacturaCompras.CANTIDAD as 'Cant', DetalleFacturaCompras.PRECUNITARIO as 'Precio Unitario', DetalleFacturaCompras.IMPORTE as 'Importe', DetalleFacturaCompras.DESCUENTO as 'Descuento', DetalleFacturaCompras.PORCDESC as '% Desc', DetalleFacturaCompras.SUBTOTAL as 'Subtotal', DetalleFacturaCompras.IMPUESTO1 as 'Iva 10,5', DetalleFacturaCompras.IMPUESTO2 as 'Iva 21', DetalleFacturaCompras.OBSERVACIONES as 'Observaciones' FROM FacturasCompras, DetalleFacturaCompras, Articulo, Proveedor, TipoFactura,Personal WHERE DetalleFacturaCompras.IDARTICULO = Articulo.IDARTICULO AND FacturasCompras.IDTIPOFACTURA = TipoFactura.IDTIPOFACTURA AND FacturasCompras.IDPROVEEDOR = Proveedor.IDPROVEEDOR AND FacturasCompras.IDPERSONAL = Personal.IDPERSONAL AND DetalleFacturaCompras.NROFACTURAINTERNO = FacturasCompras.NROFACTURAINTERNO AND FacturasCompras.NROFACTURAINTERNO = " + NROFACTURAINTERNO + "", "FacturasCompras");

                iva105 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 10,5"].ToString());
                iva21 = Convert.ToSingle(this.connGeneric.leerGeneric["Iva 21"].ToString());

                SqlCommand cm = new SqlCommand("SELECT Articulo.Codigo as 'Código', DetalleFacturaCompras.IdDetalleFacturaCompras, DetalleFacturaCompras.IDArticulo as 'Código Artículo', Articulo.DESCRIPCION as 'Artículo', Articulo.CANT_ACTUAL, DetalleFacturaCompras.CANTIDAD as 'Cant', DetalleFacturaCompras.PRECUNITARIO as 'Precio Unitario', DetalleFacturaCompras.IMPORTE as 'Importe', DetalleFacturaCompras.DESCUENTO as 'Descuento', DetalleFacturaCompras.PORCDESC as '% Desc', DetalleFacturaCompras.SUBTOTAL as 'Subtotal', DetalleFacturaCompras.IMPUESTO1 as 'Iva 10,5', DetalleFacturaCompras.IMPUESTO2 as 'Iva 21', DetalleFacturaCompras.OBSERVACIONES as 'Observaciones' FROM FacturasCompras, DetalleFacturaCompras, Articulo, Proveedor, TipoFactura,Personal WHERE DetalleFacturaCompras.IDARTICULO = Articulo.IDARTICULO AND FacturasCompras.IDTIPOFACTURA = TipoFactura.IDTIPOFACTURA AND FacturasCompras.IDPROVEEDOR = Proveedor.IDPROVEEDOR AND FacturasCompras.IDPERSONAL = Personal.IDPERSONAL AND DetalleFacturaCompras.NROFACTURAINTERNO = FacturasCompras.NROFACTURAINTERNO AND FacturasCompras.NROFACTURAINTERNO = " + NROFACTURAINTERNO + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows) {
                    lvwDetalleProveeCompras.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Código Artículo"].ToString());
                    item.SubItems.Add(dr["Código"].ToString());
                    item.SubItems.Add(dr["Artículo"].ToString());
                    item.SubItems.Add(dr["Cant"].ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Precio Unitario"]), 3).ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Subtotal"]), 3).ToString());
                    item.SubItems.Add(dr["Descuento"].ToString());

                    if (dr["Iva 10,5"].ToString() != "0,0000")
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 3).ToString());
                    else
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["Iva 21"]), 3).ToString());

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPORTE"]), 2).ToString());
                    item.SubItems.Add(dr["Observaciones"].ToString());
                    item.SubItems.Add(dr["IdDetalleFacturaCompras"].ToString());
                    item.SubItems.Add(dr["Iva 10,5"].ToString());
                    item.SubItems.Add(dr["Iva 21"].ToString());
                    item.SubItems.Add(dr["CANT_ACTUAL"].ToString());

                    if  (dr["CANT_ACTUAL"].ToString() == "0")
                        item.ImageIndex = 3;
                      else
                        item.ImageIndex = 2;

                    item.UseItemStyleForSubItems = false; 
                    lvwDetalleProveeCompras.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void txtNroFactura_TextChanged(object sender, EventArgs e)
        {        
            // Determine if the TextBox has a digit character.
         /*   string text = txtNroFactura.Text;
            bool hasDigit = false;
            foreach (char letter in text)
            {
                if (char.IsDigit(letter))
                {
                    hasDigit = true;
                    break;
                }
            }
            // Call SetError or Clear on the ErrorProvider.
            if (!hasDigit)
            {
                this.epNumeroFactu.SetError(txtNroFactura, "Needs to contain a digit");
            }
            else
            {
                epNumeroFactu.Clear();
            }*/
        }

        private void txtCodArticulo_Enter(object sender, EventArgs e)
        {            
            this.txtCodArticulo.Text = this.txtCodArticulo.Text.Trim();
        }

        private void lvwProveeCompras_MouseDoubleClick(object sender, MouseEventArgs e) {
            try {
                MostrarItemsDatos();

                conn.DesconectarBDLee();
                conn.DesconectarBD();

                nuevaFactu = false;

                idNROFACTUINTERNO = Convert.ToInt32(lvwProveeCompras.SelectedItems[0].SubItems[0].Text);
                indiceLvwCompraProvee = lvwProveeCompras.SelectedItems[0].Index;

                conn.LeeArticulo("SELECT * FROM FacturasCompras WHERE NroFacturaInterno = " + Convert.ToInt32(lvwProveeCompras.SelectedItems[0].SubItems[0].Text) + " AND IDEMPRESA = "+ IDEMPRESA +"", "FacturasCompras");

                this.txtNroInternoFact.Text = conn.leer["NroFacturaInterno"].ToString();
                this.txtNroFactura.Text = conn.leer["NROFACTURA"].ToString();
                this.cboNroSucursal.Text = conn.leer["SUCURSAL"].ToString();
                this.dtpFechaFactu.Value = Convert.ToDateTime(conn.leer["FECHA"].ToString());

                fechaFacturaCompra = Convert.ToDateTime(conn.leer["FECHA"].ToString());

                this.txtCodProveedor.Text = conn.leer["IDPROVEEDOR"].ToString();
                if (this.txtCodProveedor.Text.Trim() == "")
                    this.cboProveedor.Text = "";

                this.txtCodTipoFactura.Text = conn.leer["IDTIPOFACTURA"].ToString();
                if (this.txtCodTipoFactura.Text.Trim() == "")
                    this.cboTipoFactura.Text = "";

                this.txtCodPersonal.Text = conn.leer["IDPERSONAL"].ToString();
                if (this.txtCodPersonal.Text.Trim() == "")
                    this.cboPersonal.Text = "";

                this.txtObservacionFactura.Text = conn.leer["OBSERVACIONES"].ToString();

                /*this.txtSubTotal.Text = "$ " + conn.leer["BASICO"].ToString();
                this.txtImpuesto1.Text = "$ " + conn.leer["IMPUESTOS1"].ToString();
                this.txtImpuesto2.Text = "$ " + conn.leer["IMPUESTOS2"].ToString();
                this.txtDescuento.Text = "$ " + conn.leer["DESCUENTOS"].ToString();
                this.txtTotalFactur.Text = "$ " + conn.leer["TOTAL"].ToString();*/


                this.txtSubTotal.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leer["BASICO"]), 3).ToString();
                this.txtImpuesto1.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leer["IMPUESTOS1"]), 3).ToString();
                this.txtImpuesto2.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leer["IMPUESTOS2"]), 3).ToString();
                this.txtDescuento.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leer["DESCUENTOS"]), 3).ToString();
                this.txtTotalFactur.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leer["TOTAL"]), 2).ToString();

                conn.DesconectarBDLee();
                conn.DesconectarBD();

                btnEliminar.Enabled = true;
                //btnGuardar.Enabled = true;
                MostrarItemsDatos();

                //  if (fechaFacturaCompra.AddDays(1) <= DateTime.Today)                                
                //      MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch { conn.DesconectarBD(); }
        }

        private void lvwProveeCompras_MouseClick(object sender, MouseEventArgs e)
        {             

        }

        private void txtSucursal_Enter(object sender, EventArgs e)
        {
            this.cboNroSucursal.Text = this.cboNroSucursal.Text.Trim();
        }

        private void txtNroFactura_Enter(object sender, EventArgs e)
        {
            this.txtNroFactura.Text = this.txtNroFactura.Text.Trim();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            try {
                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                /////////////////////////////////////////// AUTONUMERICO NRO INTERNO //////////////////////////////////////////
                connGeneric.LeeGeneric("SELECT MAX(NROFACTURAINTERNO)  as Codigo FROM FacturasCompras", "FacturasCompras");

                if (connGeneric.leerGeneric["Codigo"].ToString() == "")
                    txtNroInternoFact.Text = "0";
                else
                    txtNroInternoFact.Text = connGeneric.leerGeneric["Codigo"].ToString();

                contadorNROfactNuevo = (Convert.ToInt32(txtNroInternoFact.Text));
                contadorNROfactNuevo = contadorNROfactNuevo + 1;
                txtNroInternoFact.Text = contadorNROfactNuevo.ToString();

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();  
                //////////////////////////////////////////////////////////////////////////////////////////////////
            }
            catch {}
        }

        private void txtCodTipoFactura_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodTipoFactura.Text.Trim() != "")
                {
                    connGeneric.ConsultaGeneric("Select IDTipoFactura As 'Código', Descripcion AS 'Tipo Factura' FROM TipoFactura WHERE IDTipoFactura = " + this.txtCodTipoFactura.Text + "", "TipoFactura");

                    this.cboTipoFactura.DataSource = connGeneric.ds.Tables[0];
                    this.cboTipoFactura.ValueMember = "Código";
                    this.cboTipoFactura.DisplayMember = "Tipo Factura";
                }
                else
                    cboTipoFactura.Text = "";
            }
            catch { }
        }

        private void cboBuscaProveeCompras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                this.txtBuscarArticulo.Focus();
            }
        }

        private void txtBuscarArticulo_KeyPress(object sender, KeyPressEventArgs e)
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
                txtNroFactura.Focus();
            }
        }

        private void txtNroFactura_KeyPress(object sender, KeyPressEventArgs e)
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
                txtCodProveedor.Focus();
            }
        }

        private void txtCodProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboProveedor.Focus();
            }
        }

        private void cboProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnProveedor.Focus();
            }
        }

        private void btnProveedor_KeyPress(object sender, KeyPressEventArgs e)
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
                txtCantidad.Focus();
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNroFactura_Leave(object sender, EventArgs e)
        {
            if (ValidaNumerador(this.txtNroFactura.Text.Trim()) == true)
            {
                MessageBox.Show("Error de Numerador. Ya existe el numero ingresado, el numero ha sido corregido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NuevaFactura();
            }
        }

        private void tsBtnReporte_Click(object sender, EventArgs e)
        {
            gpComprasAproveedores.Visible = true;

            try
            {
                if (this.lvwProveeCompras.Items.Count == 0)
                    MessageBox.Show("Error: No hay datos para mostrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    //if (chkTodaLaCtaCte.Checked == false && optCtaCte.Checked == true)
                    //{
                      //  this.ctaCteClienteTableAdapter.Fill(this.DGestionDTGeneral.CtaCteCliente); ///CtaCte del Cliente
                       // this.rptClienteCtaCte.RefreshReport();

                        if (btnVerReporte.Text == "     Reporte")
                            btnVerReporte.Text = "   Salir";
                        else
                            btnVerReporte.Text = "     Reporte";

                      /*  if (rptClienteCtaCte.Visible == true)
                        {
                            rptClienteCtaCte.Visible = false;
                            rptClienteTodaCtaCte.Visible = false;
                        }
                        else
                        {
                            rptClienteCtaCte.Visible = true;
                            rptClienteTodaCtaCte.Visible = false;
                        }*/
                    //}

                    
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btCerrar_Click(object sender, EventArgs e)
        {
            gpComprasAproveedores.Visible = false;
        }

        private void btnVerReporte_Click(object sender, EventArgs e)
        {

        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == ".")
            {
                e.Handled = true;
                this.txtPrecio.Text += ",";
                SendKeys.Send("{END}");
            }
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

        private void actualizaStock_Anulacion_o_eliminacion(int nFacturaInt, string CodArticulo)
        {
            try
            {
                double dCantidad_articulo_facturaCompra;
                double dCantidad_Articulo_Actual;
                double iIdArticulo = 0;


                //  if (lvwFacturaVenta.SelectedItems.Count == 1)
                //   {
                SqlCommand cm = new SqlCommand("SELECT FacturasCompras.NROFACTURAINTERNO, DetalleFacturaCompras.IDDETALLEFACTURACOMPRAS, Articulo.IDARTICULO, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL as 'AntidadArticuloDisponible', DetalleFacturaCompras.CANTIDAD as 'CantidadArtFacturado' FROM FacturasCompras, DetalleFacturaCompras, Articulo WHERE FacturasCompras.IDEMPRESA = " + IDEMPRESA + " AND DetalleFacturaCompras.IDARTICULO = Articulo.IDARTICULO AND FacturasCompras.NROFACTURAINTERNO = DetalleFacturaCompras.NROFACTURAINTERNO AND Articulo.Codigo = '" + CodArticulo + "' AND FacturasCompras.NROFACTURAINTERNO = " + nFacturaInt + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    dCantidad_Articulo_Actual = Convert.ToDouble(dr["AntidadArticuloDisponible"].ToString());
                    dCantidad_articulo_facturaCompra = Convert.ToDouble(dr["CantidadArtFacturado"].ToString());
                    iIdArticulo = Convert.ToDouble(dr["IDARTICULO"].ToString());

                    dCantidad_Articulo_Actual = dCantidad_Articulo_Actual - dCantidad_articulo_facturaCompra;

                    //Actualiza la Cantidad de Stock
                    string actualizaStock = "CANT_ACTUAL=(Cast(replace(" + dCantidad_Articulo_Actual + ", ',', '.') as decimal(10,0)))";
                    if (connGeneric.ActualizaGeneric("Articulo", actualizaStock, " IDARTICULO= " + iIdArticulo + ""))
                    {
                        connGeneric.DesconectarBD();
                        connGeneric.DesconectarBDLeeGeneric();
                    }
                    ///////////////////////////////////////////////////////////

                    dCantidad_articulo_facturaCompra = 0;
                    dCantidad_Articulo_Actual = 0;
                }
                cm.Connection.Close();
            }


            //   }
            catch { }
        }


     
    }
}