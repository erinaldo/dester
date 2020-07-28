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
    public partial class frmRecibo : Form
    {
        CGenericBD conn = new CGenericBD();
        ArticulosBD conn1 = new ArticulosBD();
        CGenericBD connGeneric = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        EmpresaBD connEmpresa = new EmpresaBD();

        double dImporteTotal;
        double sumaTotales;

        DateTime fechaRecibo;

        int contadorNROfactNuevo;

        bool nuevoRecibo = false;
        int idNRORECIBOINTERNO;
        int indiceLvwRecibo;

        public static string nroRecibo;
        public static int NroReciboInt;
        decimal ImporteRestanteTotalRecibo;

        int IDEMPRESA;
        string sPtoVta;

        //int indiceLvwCompraProvee;

        private void conPermi()
        {
            try
            {
                string sUsuarioLegueado;
                string control;
                sUsuarioLegueado = frmPrincipal.Usuario;

                SqlCommand cm = new SqlCommand("SELECT Personal.USUARIO, Personal.NOMBREYAPELLIDO, PermisoModulo.Idmodulo, PermisoControl.IdControl, PermisoControl.Control, EstadoSistema.Descripcion, PermisoUsuario.IdPersonal FROM Personal, PermisoModulo, PermisoControl, PermisoUsuario, EstadoSistema WHERE PermisoUsuario.IdPersonal = Personal.IdPersonal AND PermisoUsuario.IdModulo = PermisoModulo.IdModulo AND PermisoUsuario.IdControl = PermisoControl.IdControl AND PermisoUsuario.IdEstadoSistema = EstadoSistema.IdEstado AND PermisoUsuario.IdModulo = 4 AND Personal.USUARIO = '" + sUsuarioLegueado + "' ORDER BY IdControl", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                //control = dt.Rows[0]["Control"].ToString().Trim();

                if ((dt.Rows[0]["Descripcion"].ToString().Trim() == "Inactivo") && (dt.Rows[0]["Control"].ToString().Trim() == "Actualiza Recibo"))
                {
                    btnModificar.Enabled = true;
                    tsBtnModificar.Enabled = true;
                }
                else
                {
                    btnModificar.Enabled = false;
                    tsBtnModificar.Enabled = false;
                }

                if (dt.Rows[1]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[1]["Control"].ToString().Trim() == "Elimina Recibo")
                    btnEliminar.Enabled = true;
                else
                    btnEliminar.Enabled = false;

                if (dt.Rows[2]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[2]["Control"].ToString().Trim() == "Ver Estado de Cuenta")
                    tsVincularFactus.Enabled = true;
                else
                    tsVincularFactus.Enabled = false;

                if (dt.Rows[3]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[3]["Control"].ToString().Trim() == "Nuevo Recibo")
                    tsBtnNuevo.Enabled = true;
                else
                    tsBtnNuevo.Enabled = false;

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


        public frmRecibo()
        {
            InitializeComponent();
        }

        private void GuardaItemsDatos(bool status, int nroReciboInter)
        {
            try
            {
                //int iIdDetalleRecibo=0;
                int iIndex = 0;
                int iIdtipoPago = 0;
                string sTipoPago = "";
                int iIdSubtipoPago = 0;
                string sSubTipoPago = "";
                double Importe = 0;
                long iNum = 0;
                string Observaciones;

                char[] QuitaSimbolo1 = { '$', ' ' };

                if (txtImporteDetalleRecibo.Text.Trim() != "$ 0,00")
                {
                    conn.DesconectarBD(); conn.DesconectarBDLeeGeneric();
                    conn.DesconectarBD();

                    iIdtipoPago = Convert.ToInt32(txtCodTipoPago.Text);
                    sTipoPago = cboTipoPago.Text;
                    iIdSubtipoPago = Convert.ToInt32(txtCodSubTipoPago.Text);
                    sSubTipoPago = cboSubtipoPago.Text;

                    if (txtNro.Text == "")
                        iNum = 0;
                    else
                        iNum = Convert.ToInt32(txtNro.Text);

                    Importe = Math.Round(Convert.ToDouble(txtImporteDetalleRecibo.Text.TrimStart(QuitaSimbolo1)), 2);

                    ////////////////////////////////////// //////////////////////////////////////
                    iIndex = lvwDetalleRecibo.Items.Count;

                    lvwDetalleRecibo.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(iIndex.ToString());
                    item.SubItems.Add(iIdtipoPago.ToString());
                    item.SubItems.Add(sTipoPago.ToString());
                    item.SubItems.Add(iIdSubtipoPago.ToString());
                    item.SubItems.Add(sSubTipoPago.ToString());
                    item.SubItems.Add(iNum.ToString());
                    item.SubItems.Add("$ " + Importe.ToString());
                    item.SubItems.Add("0");

                    item.ImageIndex = 3;

                    lvwDetalleRecibo.Items.Add(item);

                    //Normalizacion de Saldos totales
                    if (lvwDetalleRecibo.Items.Count != 0)
                    {
                        dImporteTotal = 0.00;

                        char[] QuitaSimbolo = { '$', ' ' };
                        for (int i = 0; i < (lvwDetalleRecibo.Items.Count); i++)
                            dImporteTotal += Math.Round(Convert.ToSingle(lvwDetalleRecibo.Items[i].SubItems[6].Text.TrimStart(QuitaSimbolo)), 2);

                        this.txtTotalRecibo.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImporteTotal, 2));
                    }
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    ////////////////////////////////////////////////// CARGA ITEMS DE FACTURA ///////////////////////////////////////////////////////   

                    ///LEE IMPORTE RESTANTE///
                    double ImporteRestanteActual=0;

                    if (nroReciboInter != 0)
                    {                        
                        conn.LeeGeneric("Select Recibos.ImporteRestante FROM Recibos WHERE IDEMPRESA = " + IDEMPRESA + " AND NROReciboInterno = " + nroReciboInter + "", "Recibos");
                        ImporteRestanteActual = Convert.ToDouble(conn.leerGeneric["ImporteRestante"].ToString());
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////////


                    double importeFactu;

                    if (status == false)
                    {
                        if (fechaRecibo.AddDays(360) <= DateTime.Today)
                            MessageBox.Show("No se puede modificar un recibo de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            txtNroInternoRecibo.Text = idNRORECIBOINTERNO.ToString();
                            idNRORECIBOINTERNO = nroReciboInter;

                            conn.EliminarGeneric("DetalleRecibos", " NRORECIBOINTERNO = " + nroReciboInter);
                            char[] QuitaSimbolo = { '$', ' ' };

                            for (int i = 0; i < (lvwDetalleRecibo.Items.Count); i++)
                            {
                                string agregarItem = "INSERT INTO DetalleRecibos(IDTIPOPAGO, IDSUBTIPODEPAGO, NUMERO, IMPORTE, NRORECIBOINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleRecibo.Items[i].SubItems[1].Text) + ", (Cast(replace('" + lvwDetalleRecibo.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,2))), (Cast(replace('" + lvwDetalleRecibo.Items[i].SubItems[5].Text + "', ',', '.') as decimal(10,2))), (Cast(replace('" + lvwDetalleRecibo.Items[i].SubItems[6].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))), " + nroReciboInter + ")";

                                if (conn.InsertarGeneric(agregarItem))
                                {
                                    conn.DesconectarBD();
                                    conn.DesconectarBDLeeGeneric();
                                }
                            }
                            //MostrarDatos();
                            MostrarItemsDatos2(nroReciboInter);
                        }
                    }

                    else if (status == true)
                    {
                        char[] QuitaSimbolo = { '$', ' ' };
                        for (int i = 0; i < (lvwDetalleRecibo.Items.Count); i++)
                        {
                            string agregarItem = "INSERT INTO DetalleRecibos(IDTIPOPAGO, IDSUBTIPODEPAGO, NUMERO, IMPORTE, NRORECIBOINTERNO) VALUES (" + Convert.ToInt32(lvwDetalleRecibo.Items[i].SubItems[1].Text) + ", (Cast(replace('" + lvwDetalleRecibo.Items[i].SubItems[3].Text + "', ',', '.') as decimal(10,2))), (Cast(replace('" + lvwDetalleRecibo.Items[i].SubItems[5].Text + "', ',', '.') as decimal(10,2))), (Cast(replace('" + lvwDetalleRecibo.Items[i].SubItems[6].Text.TrimStart(QuitaSimbolo) + "', ',', '.') as decimal(10,2))), " + Convert.ToInt32(lvwRecibos.Items[lvwRecibos.Items.Count - 1].SubItems[0].Text) + ")";
                            nroReciboInter = Convert.ToInt32(lvwRecibos.Items[lvwRecibos.Items.Count - 1].SubItems[0].Text);

                            if (conn.InsertarGeneric(agregarItem))
                            {
                                conn.DesconectarBD();
                                conn.DesconectarBDLeeGeneric();
                            }
                        }
                        //MessageBox.Show("Item Actualizado/Agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);                           
                    }

                    //////////////////////////////////////////////// ACTUALIZA EL AGREGADO DE DATOS ////////////////////////////////////////////////
                    conn.DesconectarBDLeeGeneric();
                    conn.LeeGeneric("Select  Sum(IMPORTE) as 'Importe' FROM DetalleRecibos WHERE NRORECIBOINTERNO = " + nroReciboInter + "", "DetalleRecibos");

                    importeFactu = Convert.ToSingle(conn.leerGeneric["Importe"].ToString());

                    string actualizar = "TOTAL=(Cast(replace('" + importeFactu + "', ',', '.') as decimal(10,2)))";
                    this.txtTotalRecibo.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeFactu, 2));

                    if (conn.ActualizaGeneric("Recibos", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NROReciboInterno = " + nroReciboInter + ""))
                    {
                        MostrarDatos();
                        MostrarItemsDatos2(nroReciboInter);
                        // MessageBox.Show("La información de la factura ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("No se ha podido actualizar los datos de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //Actualiza Importe Restante Recibo x Item de recibo agregado
                    if (nroReciboInter != 0)
                    {
                        conn.DesconectarBDLeeGeneric();
                        conn.DesconectarBD();
                        string actualizar3 = "ImporteRestante = (Cast(replace('" + (ImporteRestanteActual + Importe) + "', ',', '.') as decimal(10,2)))";
                        conn.ActualizaGeneric("Recibos", actualizar3, " NROReciboInterno = " + nroReciboInter + "");
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else
                    MessageBox.Show("Falta Información.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                
                GuardaMovimientoTesoreria(nroReciboInter, txtNroRecibo.Text);

            }
            catch { MessageBox.Show("Error de Datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void MostrarItemsDatos2(int NRORECIBOINTERNO)
        {
            try
            {
                lvwDetalleRecibo.Items.Clear();

                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();

                SqlCommand cm = new SqlCommand("SELECT RECIBOS.NRORECIBOINTERNO, DetalleRecibos.IDDETALLERECIBO, DetalleRecibos.IDTIPOPAGO, TipoPago.DESCRIPCION as 'Descrip TipoPago', DetalleRecibos.IDSUBTIPODEPAGO, SubtipoPago.DESCRIPCION as 'Descrip SubTipoPago', DetalleRecibos.NUMERO, DetalleRecibos.IMPORTE FROM Recibos, DetalleRecibos, TipoPago, SubtipoPago WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND DetalleRecibos.IDSUBTIPODEPAGO = SubtipoPago.IDSUBTIPOPAGO AND DetalleRecibos.IDTIPOPAGO = TipoPago.IDTIPOPAGO AND DetalleRecibos.NRORECIBOINTERNO = Recibos.NRORECIBOINTERNO AND Recibos.NRORECIBOINTERNO = " + NRORECIBOINTERNO + " AND RECIBOS.SUCURSAL='"+ sPtoVta +"'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwDetalleRecibo.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDDETALLERECIBO"].ToString());
                    item.SubItems.Add(dr["IDTIPOPAGO"].ToString());
                    item.SubItems.Add(dr["Descrip TipoPago"].ToString());
                    item.SubItems.Add(dr["IDSUBTIPODEPAGO"].ToString());
                    item.SubItems.Add(dr["Descrip SubTipoPago"].ToString());
                    item.SubItems.Add(dr["NUMERO"].ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPORTE"]), 2).ToString());
                    item.SubItems.Add(dr["NRORECIBOINTERNO"].ToString());

                    item.ImageIndex = 2;

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleRecibo.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void MostrarItemsDatos()
        {
            try
            {
                lvwDetalleRecibo.Items.Clear();

                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();

                SqlCommand cm = new SqlCommand("SELECT RECIBOS.NRORECIBOINTERNO, DetalleRecibos.IDDETALLERECIBO, DetalleRecibos.IDTIPOPAGO, TipoPago.DESCRIPCION as 'Descrip TipoPago', DetalleRecibos.IDSUBTIPODEPAGO, SubtipoPago.DESCRIPCION as 'Descrip SubTipoPago', DetalleRecibos.NUMERO, DetalleRecibos.IMPORTE FROM Recibos, DetalleRecibos, TipoPago, SubtipoPago WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND DetalleRecibos.IDSUBTIPODEPAGO = SubtipoPago.IDSUBTIPOPAGO AND DetalleRecibos.IDTIPOPAGO = TipoPago.IDTIPOPAGO AND DetalleRecibos.NRORECIBOINTERNO = Recibos.NRORECIBOINTERNO AND Recibos.NRORECIBOINTERNO = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + " AND RECIBOS.SUCURSAL='" + sPtoVta + "'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwDetalleRecibo.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDDETALLERECIBO"].ToString());
                    item.SubItems.Add(dr["IDTIPOPAGO"].ToString());
                    item.SubItems.Add(dr["Descrip TipoPago"].ToString());
                    item.SubItems.Add(dr["IDSUBTIPODEPAGO"].ToString());
                    item.SubItems.Add(dr["Descrip SubTipoPago"].ToString());
                    item.SubItems.Add(dr["NUMERO"].ToString());
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr["IMPORTE"]), 2).ToString());
                    item.SubItems.Add(dr["NRORECIBOINTERNO"].ToString());                    

                    item.ImageIndex = 2;

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleRecibo.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void frmRecibo_Load(object sender, EventArgs e)
        {
            try
            {
                conPermi();

                gpoComprasAProveedor.Visible = false;
                gpRecibos.Width = 976;
                lvwRecibos.Width = 959;

                lvwDetalleRecibo.Height = 265;

                txtCodTipoPago.Visible = false;
                cboTipoPago.Visible = false;
                txtCodSubTipoPago.Visible = false;
                cboSubtipoPago.Visible = false;
                txtNro.Visible = false;
                txtImporteDetalleRecibo.Visible = false;
                btnTipoPago.Visible = false;
                btnSubTipoPago.Visible = false;
                btnAgregaArt.Visible = false;
                btnQuitaArt.Visible = false;
                label8.Visible = false;
                label14.Visible = false;
                label11.Visible = false;
                label12.Visible = false;

                dtpFechaRecibo.Value = DateTime.Today;
                fechaRecibo = DateTime.Today;

                cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();

                conn.ConectarBD();
                cboBuscaRecibo.SelectedIndex = 0;

                FormatoListView();
                IDEMPRESA = ConsultaEmpresa(); //Lee Empresa
                MostrarDatos();
            }
            catch { }
        }

        private void FormatoListView()
        {
            lvwRecibos.View = View.Details;
            lvwRecibos.LabelEdit = true;
            lvwRecibos.AllowColumnReorder = true;
            lvwRecibos.FullRowSelect = true;
            lvwRecibos.GridLines = true;

            lvwDetalleRecibo.View = View.Details;
            lvwDetalleRecibo.LabelEdit = true;
            lvwDetalleRecibo.AllowColumnReorder = true;
            lvwDetalleRecibo.FullRowSelect = true;
            lvwDetalleRecibo.GridLines = true;
        }

        private void MostrarDatos()
        {
            try
            {
                lvwRecibos.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM Recibos, Cliente WHERE Recibos.Sucursal = '"+ sPtoVta +"' AND Recibos.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND Recibos.IDCLIENTE = Cliente.IDCLIENTE  ORDER BY NRORECIBOINTERNO", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwRecibos.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NRORECIBOINTERNO"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["NRORECIBO"].ToString());
                    item.SubItems.Add(dr["FECHA"].ToString());

                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["IDCLIENTE"]), 3).ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString(), Color.Empty, Color.LightGray, null);

                    item.SubItems.Add(dr["IDPERSONAL"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["IDVENDEDOR"]), 3).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["TOTAL"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());

                    item.SubItems.Add(dr["IMPORTERESTANTE"].ToString());

                    if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(360) <= DateTime.Today)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;
                    item.UseItemStyleForSubItems = false;
                    lvwRecibos.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        private void NuevoRecibo()
        {
            try
            {
                dtpFechaRecibo.Value = DateTime.Today;
                fechaRecibo = DateTime.Today;

                lvwDetalleRecibo.Items.Clear();
                gpoComprasAProveedor.Visible = true;
                gpRecibos.Width = 261;
                lvwRecibos.Width = 250;
                lvwDetalleRecibo.Height = 240;

                cboBuscaRecibo.SelectedIndex = 0;

                //btnEliminar.Enabled = false;
                //tsBtnModificar.Enabled = true;
                //tsBtnNuevo.Enabled = true;
                //btnModificar.Enabled = false;
                //btnGuardar.Enabled = true;

                txtCodTipoPago.Visible = true;
                cboTipoPago.Visible = true;
                txtCodSubTipoPago.Visible = true;
                cboSubtipoPago.Visible = true;
                txtNro.Visible = true;
                txtImporteDetalleRecibo.Visible = true;
                btnTipoPago.Visible = true;
                btnSubTipoPago.Visible = true;
                btnAgregaArt.Visible = true;
                btnQuitaArt.Visible = true;
                label8.Visible = true;
                label14.Visible = true;
                label11.Visible = true;
                label12.Visible = true;

                txtCodSubTipoPago.Enabled = true;
                cboSubtipoPago.Enabled = true;
                btnSubTipoPago.Enabled = true;
                txtNro.Enabled = true;

                this.txtNroInternoRecibo.Text = "0";
                this.txtNroRecibo.Text = "";
                this.txtObservacionFactura.Text = "";
                this.txtIva.Text = "";
                this.txtCodPersonal.Text = "";
                this.txtCodCliente.Text = "";
                this.txtCodTipoPago.Text = "";
                this.txtCuit.Text = "";

                txtCodVendedor.Text = "";
                cboVendedor.Text = "";

                txtCodTipoPago.Text = "";
                cboTipoPago.Text = "";
                txtCodSubTipoPago.Text = "";
                cboSubtipoPago.Text = "";
                txtNro.Text = "";
                txtImporteDetalleRecibo.Text = "$ 0,00";
                txtTotalRecibo.Text = "$ 0,00";

                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();

                /////////////////////////////////////////// AUTONUMERICO NRO INTERNO //////////////////////////////////////////
                /*conn.LeeGeneric("SELECT MAX(NRORECIBOINTERNO) as 'NRO' FROM Recibos WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = '" + sPtoVta + "' ORDER BY NRO", "Recibos");

                if (conn.leerGeneric["NRO"].ToString() == "")
                    txtNroInternoRecibo.Text = "0";
                else
                    txtNroInternoRecibo.Text = conn.leerGeneric["NRO"].ToString();

                contadorNROfactNuevo = (Convert.ToInt32(txtNroInternoRecibo.Text));
                contadorNROfactNuevo = contadorNROfactNuevo + 1;
                txtNroInternoRecibo.Text = contadorNROfactNuevo.ToString();

                this.txtNroRecibo.Text = frmPrincipal.PtoVenta.Trim() + "-" + this.txtNroInternoRecibo.Text.Trim();

                ValidaNumerador(this.txtNroRecibo.Text.Trim());

                txtNroRecibo.Focus();

                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();*/
                

                /////////////////////////////////////////// AUTONUMERICO NRO INTERNO //////////////////////////////////////////
                conn.LeeGeneric("SELECT MAX(NRORECIBO) as 'NRO' FROM Recibos WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = '" + sPtoVta.Trim() + "' ORDER BY NRO", "Recibos");

                if (conn.leerGeneric["NRO"].ToString() == "")
                {
                    txtNroInternoRecibo.Text = "0";
                    txtNroRecibo.Text = "0";
                }
                else
                {
                    //txtNroIntRemito.Text = connGeneric.leerGeneric["NRO"].ToString();
                    txtNroRecibo.Text = conn.leerGeneric["NRO"].ToString();
                }

                contadorNROfactNuevo = (Convert.ToInt32(txtNroRecibo.Text));
                contadorNROfactNuevo = contadorNROfactNuevo + 1;

                txtNroRecibo.Text = Convert.ToString(contadorNROfactNuevo);

                ValidaNumerador(this.txtNroRecibo.Text.Trim());

                txtNroRecibo.Focus();

                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

            }
            catch { MessageBox.Show("Error: El nro ingresado no es numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                nuevoRecibo = true;
                NuevoRecibo();

            }
            catch { MessageBox.Show("Error: El nro ingresado no es numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private bool ValidaNumerador(string nrocomprobante)
        {
            try
            {
                SqlCommand cm = new SqlCommand("SELECT NRORECIBO FROM Recibos WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND RECIBOS.SUCURSAL='" + sPtoVta + "'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    if (nrocomprobante == dr["NRORECIBO"].ToString().Trim())
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
            gpoComprasAProveedor.Visible = false;
            gpRecibos.Width = 976;
            lvwRecibos.Width = 959;

            lvwDetalleRecibo.Height = 265;

            dtpFechaRecibo.Value = DateTime.Today;
            fechaRecibo = DateTime.Today;

            //tsBtnNuevo.Enabled = true;
            //tsBtnModificar.Enabled = true;

            txtCodTipoPago.Visible = false;
            cboTipoPago.Visible = false;
            txtCodSubTipoPago.Visible = false;
            cboSubtipoPago.Visible = false;
            txtNro.Visible = false;
            txtImporteDetalleRecibo.Visible = false;
            btnTipoPago.Visible = false;
            btnSubTipoPago.Visible = false;
            btnAgregaArt.Visible = false;
            btnQuitaArt.Visible = false;
            label8.Visible = false;
            label14.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
        }

        private void tsBtnModificar_Click(object sender, EventArgs e)
        {
            nuevoRecibo = false;
            // timer1.Enabled = false;

            if (lvwRecibos.SelectedItems.Count == 0)
                MessageBox.Show("Error: No se ha seleccionado ningún recibo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                gpoComprasAProveedor.Visible = true;
                gpRecibos.Width = 261;
                lvwRecibos.Width = 250;
                lvwDetalleRecibo.Height = 240;

                //btnEliminar.Enabled = true;
                //tsBtnModificar.Enabled = false;
                //tsBtnNuevo.Enabled = true;
                //btnModificar.Enabled = true;
                //btnGuardar.Enabled = false;

                txtCodTipoPago.Visible = true;
                cboTipoPago.Visible = true;
                txtCodSubTipoPago.Visible = true;
                cboSubtipoPago.Visible = true;
                txtNro.Visible = true;
                txtImporteDetalleRecibo.Visible = true;
                btnTipoPago.Visible = true;
                btnTipoPago.Visible = true;
                btnAgregaArt.Visible = true;
                btnQuitaArt.Visible = true;
                label8.Visible = true;
                label14.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                btnSubTipoPago.Visible = true;
            }
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            frmCliente formCliente = new frmCliente();
            formCliente.pasarClienteCod += new frmCliente.pasarClienteCod1(CodClient);  //Delegado1 
            formCliente.pasarClientRS += new frmCliente.pasarClienteRS(RazonS); //Delegado2
            txtCodPersonal.Focus();
            formCliente.ShowDialog();
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
                if (this.txtCodCliente.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("SELECT * FROM Cliente WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND IdCliente = " + Convert.ToInt32(this.txtCodCliente.Text) + "", "Cliente");

                    this.cboCliente.DataSource = conn.ds.Tables[0];
                    this.cboCliente.ValueMember = "IdCliente";
                    this.cboCliente.DisplayMember = "RazonSocial";


                    iTablaCant = conn.ds.Tables[0].Rows.Count;

                    conn.DesconectarBD();
                    conn.DesconectarBDLeeGeneric();

                    conn.LeeGeneric("SELECT Cliente.NUMDECUIT, Cliente.IDPERSONAL, TipoIva.DESCRIPCION as 'TipoIva', TipoIva.IdTipoIva, ListaPrecios.IDLISTAPRECIO, ListaPrecios.DESCRIPCION as 'DescLista' FROM Cliente, TipoIva, ListaPrecios WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDTIPOIVA = TipoIva.IDTIPOIVA AND Cliente.IDLISTAPRECIO=ListaPrecios.IDLISTAPRECIO AND Cliente.IDCLIENTE = " + Convert.ToInt32(this.txtCodCliente.Text) + "", "Cliente");

                    txtCuit.Text = conn.leerGeneric["NUMDECUIT"].ToString();
                    txtIva.Text = conn.leerGeneric["TipoIva"].ToString();
                    txtCodPersonal.Text = conn.leerGeneric["IDPERSONAL"].ToString();

                    //iCodigoListaPrecioCliente = Convert.ToInt32(connGeneric.leerGeneric["IDLISTAPRECIO"].ToString());
                    //cboListaCliente.Text = connGeneric.leerGeneric["DescLista"].ToString();
                }
                else
                {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    //txtCodCliente.Text = "";
                }

                if (iTablaCant < 1)
                {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    //txtCodCliente.Text = "";
                }

                if (this.txtCuit.Text.Trim() == "0")
                {
                    cboCliente.Text = "";
                    txtCuit.Text = "";
                    txtIva.Text = "";
                    //txtCodCliente.Text = "";
                    MessageBox.Show("Error: Falta informacion relacionada con el Cliente (CUIT)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();
            }
            catch
            {
                cboCliente.Text = "";
                txtCuit.Text = "";
                txtIva.Text = "";
            }
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnCodPersonal_Click(object sender, EventArgs e)
        {
            frmPersonal frmPerso = new frmPersonal();
            frmPerso.pasadoPerso1 += new frmPersonal.pasarPersona1(CodPPerso);  //Delegado11 Rubro Articulo
            frmPerso.pasadoPerso2 += new frmPersonal.pasarPersona2(RSPerso); //Delegado2 Rubro Articulo
            txtCodVendedor.Focus();
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

        private void btnTipoPago_Click(object sender, EventArgs e)
        {
            frmTipoPago formTipoPago = new frmTipoPago();
            formTipoPago.pasarTPCod += new frmTipoPago.pasarTipoPagoCod1(CodTPago);  //Delegado1 
            formTipoPago.pasarTPN += new frmTipoPago.pasarTipoPagoRS(DescTipoPago); //Delegado2

            if (txtCodSubTipoPago.Enabled == true)
                txtCodSubTipoPago.Focus();
            else
                txtImporteDetalleRecibo.Focus();

            formTipoPago.ShowDialog();
        }

        public void CodTPago(int dato1)
        {
            this.txtCodTipoPago.Text = dato1.ToString();
        }

        public void DescTipoPago(string dato2)
        {
            this.cboTipoPago.Text = dato2.ToString();
        }

        private void btnCodVendedor_Click(object sender, EventArgs e)
        {
            frmVendedor formVende = new frmVendedor();
            formVende.pasarVendeCod += new frmVendedor.pasarVendeCod1(CodVende);  //Delegado1 
            formVende.pasarVendeN += new frmVendedor.pasarVendeRS(NombreVende); //Delegado2
            txtObservacionFactura.Focus();
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

        private void btnSubTipoPago_Click(object sender, EventArgs e)
        {
            frmSubTipoPago formSubTipoPago = new frmSubTipoPago();
            formSubTipoPago.pasarSubTPCod += new frmSubTipoPago.pasarSubTipoPagoCod1(CodSunTPago);  //Delegado1 
            formSubTipoPago.pasarSubTPN += new frmSubTipoPago.pasarSubTipoPagoRS(DescSubTipoPago); //Delegado2
            txtNro.Focus();
            formSubTipoPago.ShowDialog();
        }

        public void CodSunTPago(int dato1)
        {
            this.txtCodSubTipoPago.Text = dato1.ToString();
        }

        public void DescSubTipoPago(string dato2)
        {
            this.cboSubtipoPago.Text = dato2.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (fechaRecibo.AddDays(360) <= DateTime.Today)
                MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                GuardarTodosLosDatos();
        }

        private void GuardarTodosLosDatos()
        {
            try
            {
                float importeTotal;

                //Quita Simbolos para guardar los datos en formato numéricos
                char[] QuitaSimbolo = { '$', ' ' };
                importeTotal = Convert.ToSingle(this.txtImporteDetalleRecibo.Text.TrimStart(QuitaSimbolo));
                /////////////////////////////////////////////////////////////////////////////////

                conn.DesconectarBD();
                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();

                if (nuevoRecibo == true)
                    conn.ConsultaGeneric("Select * FROM Recibos WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND NRORECIBOINTERNO = " + Convert.ToInt32(txtNroInternoRecibo.Text) + " AND RECIBOS.SUCURSAL='" + sPtoVta + "'", "Recibos");
                else
                    conn.ConsultaGeneric("Select * FROM Recibos WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND NRORECIBOINTERNO = " + idNRORECIBOINTERNO + " AND RECIBOS.SUCURSAL='" + sPtoVta + "'", "Recibos");

                if (conn.ds.Tables[0].Rows.Count == 0)
                {
                    string agregar = "INSERT INTO Recibos(NRORECIBO, FECHA, SUCURSAL, IDCLIENTE, IDPERSONAL, IDVENDEDOR, TOTAL, OBSERVACIONES, IMPORTERESTANTE, IDEMPRESA) VALUES ('" + txtNroRecibo.Text.Trim() + "', '" + dtpFechaRecibo.Text.Trim() + "', '"+ sPtoVta +"', " + txtCodCliente.Text.Trim() + ", " + txtCodPersonal.Text.Trim() + ", " + txtCodVendedor.Text.Trim() + ", (Cast(replace('" + importeTotal + "', ',', '.') as decimal(10,2))), '" + txtObservacionFactura.Text + "', (Cast(replace('" + importeTotal + "', ',', '.') as decimal(10,2))), " + IDEMPRESA + ")";

                    this.txtTotalRecibo.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)importeTotal, 2));

                    if (conn.InsertarGeneric(agregar))
                    {

                        MostrarDatos();
                        GuardaItemsDatos(true, 0);

                        lvwRecibos.Items[lvwRecibos.Items.Count - 1].Selected = true;
                        txtNroInternoRecibo.Text = lvwRecibos.Items[lvwRecibos.Items.Count - 1].Text;
                        idNRORECIBOINTERNO = Convert.ToInt32(lvwRecibos.Items[lvwRecibos.Items.Count - 1].Text);
                        //indiceLvwCompraProvee = lvwProveeCompras.Items[0].Index;
                        //MessageBox.Show("Factura Guardada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    GuardaItemsDatos(false, idNRORECIBOINTERNO);
            }
            catch { conn.DesconectarBD(); conn.DesconectarBD(); conn.DesconectarBDLeeGeneric(); }
        }

        private void btnAgregaArt_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCodSubTipoPago.Text.Trim() == "" || cboSubtipoPago.Text.Trim() == "" || txtNro.Text.Trim() == "" && cboTipoPago.Text.Trim() == "CHEQUE")
                        MessageBox.Show("Falta informacion del banco emisor o nro. de cheque. Recibo no generado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    if (LeeEstadoRecibo() > 1000)
                    {
                        MessageBox.Show("faltan vincular Recibo/Factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        MostrarItemsDatos();
                        gbFactus.Visible = true;
                    }
                    else
                    {
                        if (fechaRecibo.AddDays(360) <= DateTime.Today)
                            MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                        {
                            //timer1.Enabled = true;
                            GuardarTodosLosDatos();
                            txtCodTipoPago.Text = "";
                            cboTipoPago.Text = "";
                            txtCodSubTipoPago.Text = "";
                            cboSubtipoPago.Text = "";
                            txtNro.Text = "";
                            txtImporteDetalleRecibo.Text = "$ 0,00";

                            txtCodTipoPago.Focus();
                        }
                    }
                }              
            }
            catch { }
        }

        private int LeeEstadoRecibo()
        {
            try
            {
                SqlCommand cm = new SqlCommand("SELECT Recibos.IdEstado FROM Recibos, Empresa WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND Empresa.IDEMPRESA = " + IDEMPRESA + " AND Recibos.IDESTADO = 17 AND RECIBOS.SUCURSAL='" + sPtoVta + "' ORDER BY Recibos.NRORECIBOINTERNO", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                //int EstadoRecibo = 0;
                
                //foreach (DataRow dr in dt.Rows)
                //{
                    //if (Convert.ToInt32(dr["IdEstado"].ToString()) == 17)
                    //{
                      //  EstadoRecibo = Convert.ToInt32(dr["IdEstado"].ToString());                        

                        return dt.Rows.Count;
                        //return EstadoRecibo;
                    //}
                //}
                //return 0;
            }
            catch { return 2; }
        }

        private void txtCodTipoPago_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodTipoPago.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("Select IDTipoPago As 'Código', Descripcion AS 'Tipo Pago' FROM TipoPago WHERE IDTipoPago = " + this.txtCodTipoPago.Text + "", "TipoPago");

                    this.cboTipoPago.DataSource = conn.ds.Tables[0];
                    this.cboTipoPago.ValueMember = "Código";
                    this.cboTipoPago.DisplayMember = "Tipo Pago";
                }
                else
                    cboTipoPago.Text = "";
            }
            catch { }
        }

        private void txtCodSubTipoPago_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodSubTipoPago.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("Select IDSubTipoPago As 'Código', Descripcion AS 'SubTipo Pago' FROM SubTipoPago WHERE IDSubTipoPago = " + this.txtCodSubTipoPago.Text + "", "SubTipoPago");

                    this.cboSubtipoPago.DataSource = conn.ds.Tables[0];
                    this.cboSubtipoPago.ValueMember = "Código";
                    this.cboSubtipoPago.DisplayMember = "SubTipo Pago";
                }
                else
                    cboSubtipoPago.Text = "";
            }
            catch { }
        }

        private void txtImporteTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtImporteTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == ".")
            {
                e.Handled = true;
                this.txtImporteDetalleRecibo.Text += ",";
                SendKeys.Send("{END}");
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnAgregaArt.Focus();
            }
        }

        private void txtImporteTotal_Enter(object sender, EventArgs e)
        {
            //char[] QuitaSimbolo = { '$', ' ' };
            //this.txtImporteTotal.Text = this.txtImporteTotal.Text.TrimStart(QuitaSimbolo);
            this.txtImporteDetalleRecibo.Text = "";
        }

        private void lvwRecibos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //timer1.Enabled = false;                
                MostrarItemsDatos();

                conn1.DesconectarBDLee();
                conn1.DesconectarBD();

                nuevoRecibo = false;

                idNRORECIBOINTERNO = Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text);
                indiceLvwRecibo = lvwRecibos.SelectedItems[0].Index;

                conn1.LeeArticulo("SELECT * FROM Recibos WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND Sucursal = '"+ sPtoVta.Trim() +"' AND NroReciboInterno = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + "", "Recibos");

                this.txtNroInternoRecibo.Text = conn1.leer["NroReciboInterno"].ToString();
                this.txtNroRecibo.Text = conn1.leer["NRORECIBO"].ToString();
                this.dtpFechaRecibo.Value = Convert.ToDateTime(conn1.leer["FECHA"].ToString());

                fechaRecibo = Convert.ToDateTime(conn1.leer["FECHA"].ToString());

                this.txtCodCliente.Text = conn1.leer["IDCLIENTE"].ToString();
                if (this.txtCodCliente.Text.Trim() == "")
                    this.cboCliente.Text = "";

                this.txtCodPersonal.Text = conn1.leer["IDPERSONAL"].ToString();
                if (this.txtCodPersonal.Text.Trim() == "")
                    this.cboPersonal.Text = "";

                this.txtCodVendedor.Text = conn1.leer["IDVENDEDOR"].ToString();
                if (this.txtCodVendedor.Text.Trim() == "")
                    this.cboVendedor.Text = "";

                this.txtObservacionFactura.Text = conn1.leer["OBSERVACIONES"].ToString();

                this.txtTotalRecibo.Text = "$ " + Math.Round(Convert.ToDecimal(conn1.leer["TOTAL"]), 2).ToString();   

                //btnEliminar.Enabled = true;
                //btnModificar.Enabled = true;
                //btnGuardar.Enabled = true;
                
                lblImporteRecibo.ForeColor= Color.DarkGreen;
                lblImporteRecibo.Text = "$ " + Math.Round(Convert.ToDecimal(lvwRecibos.SelectedItems[0].SubItems[8].Text),2).ToString();
                lblSaldoResto.Text = "$ " + Math.Round(Convert.ToDecimal(conn1.leer["IMPORTERESTANTE"]), 2).ToString();
                ImporteRestanteTotalRecibo = Math.Round(Convert.ToDecimal(conn1.leer["IMPORTERESTANTE"]), 2);

                lblRazonSocial.Text = lvwRecibos.SelectedItems[0].SubItems[5].Text;
                //lblImporteRestante.Text = "$ " + Math.Round(Convert.ToDecimal(lvwRecibos.SelectedItems[0].SubItems[9].Text),2);

                //decimal prueba;
                //prueba = Math.Round(Convert.ToDecimal(conn1.leer["IMPORTERESTANTE"]), 2);

                if (Math.Round(Convert.ToDecimal(conn1.leer["IMPORTERESTANTE"]), 2).ToString() == "0,00")
                {                    
                    lblImporteRestante.ForeColor = Color.Red;
                    lblImporteRestante.Text = "Recibo Imputado";
                }
                else
                {
                    lblImporteRestante.ForeColor = Color.DarkGreen;
                    lblImporteRestante.Text = "Recibo a Imputar";
                }

                conn1.DesconectarBDLee();
                conn1.DesconectarBD();

                MostrarItemsDatos();
                lvwComprobEmitido.Items.Clear();

                gpbRecNoImputado.Visible = false;

                //  if (fechaFacturaCompra.AddDays(1) <= DateTime.Today)                                
                //      MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MostrarTodoLosDatos();
                ActualizaFacturasPendientes(Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[4].Text));
            }
            catch { conn.DesconectarBD(); }
        }

        private void txtImporteTotal_Leave(object sender, EventArgs e)
        {
            this.txtImporteDetalleRecibo.Text = "$ " + this.txtImporteDetalleRecibo.Text.Trim();
            this.txtImporteDetalleRecibo.Text = this.txtImporteDetalleRecibo.Text.Trim();
        }

        private void btnQuitaArt_Click(object sender, EventArgs e)
        {
            try
            {
                int iIndex = 0;
                int iIndexRecibo = 0;
                double ImporteTotalDetalleRecibo=0;

                if (fechaRecibo.AddDays(360) <= DateTime.Today)
                    MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    double ImporteRestanteActual;
                    char[] QuitaSimbolo2 = { '$', ' ' };

                    //timer1.Enabled = false;
                    iIndex = Convert.ToInt32(lvwDetalleRecibo.SelectedItems[0].SubItems[0].Text);  //Elemento de la base de datos
                    ImporteTotalDetalleRecibo = Convert.ToDouble(lvwDetalleRecibo.SelectedItems[0].SubItems[6].Text.TrimStart(QuitaSimbolo2));  //Elemento de la base de datos
                    iIndexRecibo = Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text);  //Elemento de la base de datos
                    lvwDetalleRecibo.Items[lvwDetalleRecibo.SelectedItems[0].Index].Remove(); //Elemento del listview

                    //Actualiza Importe Restante Recibo x Item de recibo agregado                    
                    conn.LeeGeneric("Select Recibos.ImporteRestante FROM Recibos WHERE IDEMPRESA = " + IDEMPRESA + " AND NROReciboInterno = " + iIndexRecibo + " AND RECIBOS.SUCURSAL='" + sPtoVta + "'", "Recibos");
                    ImporteRestanteActual = Convert.ToDouble(conn.leerGeneric["ImporteRestante"].ToString());
                    
                    string actualizar3 = "ImporteRestante = " + (ImporteRestanteActual - ImporteTotalDetalleRecibo) + "";
                    conn.ActualizaGeneric("Recibos", actualizar3, " NROReciboInterno = " + iIndexRecibo + "");
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    if (conn1.EliminarArticulo("DetalleRecibos", " IDDETALLERECIBO = " + iIndex))
                        //MostrarItemsDatos2(idNROFACTUINTERNO);

                        if (lvwDetalleRecibo.Items.Count != 0)
                        {
                            if (iIndex != 0)
                            {
                                string importeRecibo;

                                conn1.DesconectarBDLee();
                                conn1.LeeArticulo("Select  Sum(IMPORTE) as 'Importe' FROM DetalleRecibos WHERE NRORECIBOINTERNO = " + idNRORECIBOINTERNO + "", "DetalleRecibos");

                                importeRecibo = conn1.leer["Importe"].ToString();

                                string actualizar = "TOTAL=(Cast(replace('" + importeRecibo + "', ',', '.') as decimal(10,2)))";

                                this.txtTotalRecibo.Text = "$ " + String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(importeRecibo), 2));

                                if (conn1.ActualizaArticulo("Recibos", actualizar, " Recibos.IDEMPRESA = " + IDEMPRESA + " AND NROReciboInterno = " + idNRORECIBOINTERNO + ""))
                                {
                                    MostrarDatos();
                                    MostrarItemsDatos2(idNRORECIBOINTERNO);
                                    // MessageBox.Show("La información de la factura ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("No se ha podido actualizar los datos de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            dImporteTotal = 0.00;

                            char[] QuitaSimbolo = { '$', ' ' };
                            for (int i = 0; i < (lvwDetalleRecibo.Items.Count); i++)
                            {
                                dImporteTotal += Math.Round(Convert.ToSingle(lvwDetalleRecibo.Items[i].SubItems[6].Text.TrimStart(QuitaSimbolo)), 2);
                            }

                            this.txtTotalRecibo.Text = "$ " + String.Format("{0:0.00}", Decimal.Round((decimal)dImporteTotal, 2));
                        }
                        else
                        {
                            this.txtTotalRecibo.Text = "$ " + "0,00";

                            string actualizar = "TOTAL=(Cast(replace('" + "0,00" + "', ',', '.') as decimal(10,2)))";
                            conn1.ActualizaArticulo("Recibos", actualizar, " Recibos.IDEMPRESA = " + IDEMPRESA + " AND NROReciboInterno = " + idNRORECIBOINTERNO + "");
                            MostrarDatos();
                            MostrarItemsDatos2(idNRORECIBOINTERNO);
                        }
                }
            }
            catch { conn.DesconectarBD(); MostrarItemsDatos(); }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                //timer1.Enabled = false;
                string actualizar = "NRORECIBO='" + txtNroRecibo.Text.Trim() + "', FECHA='" + dtpFechaRecibo.Text.Trim() + "', IDCLIENTE=" + txtCodCliente.Text.Trim() + " , IDVENDEDOR=" + txtCodVendedor.Text.Trim() + ", IDPERSONAL=" + txtCodPersonal.Text.Trim() + ", OBSERVACIONES='" + txtObservacionFactura.Text.Trim() + "'";

                if (conn1.ActualizaArticulo("Recibos", actualizar, " Recibos.IDEMPRESA = " + IDEMPRESA + " AND NROReciboInterno = " + Convert.ToInt32(txtNroInternoRecibo.Text) + ""))
                {
                    MostrarDatos();
                    MostrarItemsDatos2(Convert.ToInt32(txtNroInternoRecibo.Text));
                    MessageBox.Show("La información del Recibo ha sido actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("No se ha podido actualizar los datos del Recibo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { MessageBox.Show("Error: No se ha podido actualizar la información del Recibo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                //tsBtnModificar.Enabled = true;
                //tsBtnNuevo.Enabled = true;
                //btnModificar.Enabled = false;
                //btnGuardar.Enabled = false;
                //timer1.Enabled = false;

                connGeneric.DesconectarBD();
                connGeneric.DesconectarBDLeeGeneric();

                if (fechaRecibo.AddDays(360) <= DateTime.Today)
                    MessageBox.Show("No se puede eliminar el recibo de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (connGeneric.EliminarGeneric("Recibos", " Recibos.IDEMPRESA = " + IDEMPRESA + " AND NRORECIBOINTERNO = " + Convert.ToInt32(this.txtNroInternoRecibo.Text)))
                    {
                        MostrarDatos();

                        //tsBtnNuevo.Enabled = true;
                        //tsBtnModificar.Enabled = false;
                        //btnEliminar.Enabled = true;
                        //btnModificar.Enabled = false;
                        //btnGuardar.Enabled = true;

                        this.txtNroInternoRecibo.Text = "";
                        this.txtNroRecibo.Text = "";
                        this.txtObservacionFactura.Text = "";
                        this.txtIva.Text = "";
                        this.txtCodPersonal.Text = "";
                        this.txtCodCliente.Text = "";
                        this.txtCodTipoPago.Text = "";
                        this.txtCuit.Text = "";

                        txtCodVendedor.Text = "";
                        cboVendedor.Text = "";

                        txtCodTipoPago.Text = "";
                        cboTipoPago.Text = "";
                        txtCodSubTipoPago.Text = "";
                        cboSubtipoPago.Text = "";
                        txtNro.Text = "";
                        txtImporteDetalleRecibo.Text = "$ 0,00";
                        txtTotalRecibo.Text = "$ 0,00";

                        MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //timer1.Enabled = true;
                    }
                    else
                        MessageBox.Show("Error al Eliminar, seleccionar recibo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { MessageBox.Show("Error: Seleccione el recibo a eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cboBuscaProveeCompras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtBuscarReciboCliente.Focus();
            }
        }

        private void txtBuscarArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtNroRecibo.Focus();
            }
        }

        private void txtNroRecibo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                dtpFechaRecibo.Focus();
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
                btnCodPersonal.Focus();
            }
        }

        private void btnCodPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {

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
                btnCodVendedor.Focus();
            }
        }

        private void btnCodVendedor_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtObservacionFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodTipoPago.Focus();
            }
        }

        private void txtCodTipoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboTipoPago.Focus();
            }
        }

        private void cboTipoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnTipoPago.Focus();
            }
        }

        private void btnTipoPago_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCodSubTipoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboSubtipoPago.Focus();
            }
        }

        private void cboSubtipoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnSubTipoPago.Focus();
            }
        }

        private void btnSubTipoPago_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtNro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtImporteDetalleRecibo.Focus();
            }
        }

        private void btnAgregaArt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodTipoPago.Focus();
            }
        }

        private void cboTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtCodTipoPago.Text == "2" && cboTipoPago.Text == "EFECTIVO")
            {
                txtCodSubTipoPago.Enabled = false;
                cboSubtipoPago.Enabled = false;
                btnSubTipoPago.Enabled = false;
                txtNro.Enabled = false;
                txtCodSubTipoPago.Text = "3";
                cboSubtipoPago.Text = "-";
                txtImporteDetalleRecibo.Focus();
            }
            else
            {
                txtCodSubTipoPago.Enabled = true;
                cboSubtipoPago.Enabled = true;
                btnSubTipoPago.Enabled = true;
                txtNro.Enabled = true;
            }
        }

        private void tsVincularFactus_Click(object sender, EventArgs e)
        {
            try
            {
                ActualizaFacturasPendientes(Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[4].Text));

                if (lvwRecibos.SelectedItems.Count != 0)
                    gbFactus.Visible = true;
                else
                    gbFactus.Visible = false;


                //lblImporteRecibo.Text = "$ " + Math.Round(Convert.ToDecimal(lvwRecibos.SelectedItems[0].SubItems[7].Text), 2);

                //if (lvwRecibos.SelectedItems.Count == 0)
                //    MostrarDatosFactu();
                //else

                //if (txtCodCliente.Text.Trim() == "")

                MostrarDatosCompNoVinculados(0);

            }
            catch { MessageBox.Show("No se ha seleccionado el comprobante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void MostrarDatosCompVinculados(int iOpcion)
        {
            try
            {
                if (iOpcion == 0)
                {
                    lvwComprobEmitido.Items.Clear();
                }
                                
                double dSaldoVinculado=0;
                decimal dMontoPend;
                decimal bMontoTotal;
                decimal dPagado;

                string sNRecibo;

                decimal dPendiente;
                decimal dSaldo;

                if (lvwRecibos.SelectedItems.Count == 0)
                    MessageBox.Show("Error: No se ha seleccionado ningún recibo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    lvwComprobEmitido.CheckBoxes = false;
                    lvwComprobEmitido.Columns[0].Width = 28;

                    //VISUALIZA FACTURAS Vinculadas
                    SqlCommand cm = new SqlCommand("SELECT NROFACTURAINTERNO as 'Código', NROFACTURA as 'Nro Fact', Fecha, Basico as 'Básico', DESCUENTOS as 'Desc', IMPUESTO1 as 'Iva 10,5', IMPUESTO2 as 'Iva 21', TOTAL as 'Total', Pagado, Pendiente, Saldo, OBSERVACIONES as 'Observaciones', NRORECIBOINTERNO, IDESTADO, IdentificaComprob.IDENTIFICADOR, IdentificaComprob.IDIDENTIFICADOR FROM FacturasVentas, IdentificaComprob WHERE FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND IDCLIENTE = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[4].Text) + " AND IdentificaComprob.IDIDENTIFICADOR = FacturasVentas.IDIDENTIFICADOR AND NRORECIBOINTERNO = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + " AND FacturasVentas.Sucursal='" + sPtoVta+ "' ORDER BY NROFACTURAINTERNO", conectaEstado);

                  //  SqlCommand cm = new SqlCommand("SELECT Recibos.NRORECIBO, Recibos.TOTAL, FacturasVentas.NROFACTURAINTERNO as 'Código', NROFACTURA as 'Nro Fact', FacturasVentas.Fecha, Basico as 'Básico', DESCUENTOS as 'Desc', IMPUESTO1 as 'Iva 10,5', IMPUESTO2 as 'Iva 21', FacturasVentas.TOTAL as 'Total', Pagado, FacturasVentas.OBSERVACIONES as 'Observaciones', FacturasVentas.NRORECIBOINTERNO, IDESTADO, IdentificaComprob.IDENTIFICADOR, IdentificaComprob.IDIDENTIFICADOR FROM FacturasVentas, IdentificaComprob, Recibos WHERE FacturasVentas.IDCLIENTE = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[3].Text) + " AND IdentificaComprob.IDIDENTIFICADOR = FacturasVentas.IDIDENTIFICADOR AND Recibos.NRORECIBO = '" + lvwRecibos.SelectedItems[0].SubItems[1].Text + "' AND FacturasVentas.NROFACTURAINTERNO = Recibos.NROFACTURAINTERNO ORDER BY FacturasVentas.NROFACTURAINTERNO", conectaEstado);
                    

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        conn.LeeGeneric("SELECT * FROM Recibos WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND NRORECIBOINTERNO  = " + Convert.ToInt32(dr["NRORECIBOINTERNO"].ToString()) + " AND RECIBOS.SUCURSAL='" + sPtoVta + "'", "Recibos");
                        sNRecibo = conn.leerGeneric["NRORECIBO"].ToString();
                        conn.DesconectarBDLeeGeneric();

                        lvwComprobEmitido.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["Código"].ToString());
                        item.SubItems.Add(dr["Nro Fact"].ToString());
                        item.SubItems.Add(dr["IDENTIFICADOR"].ToString());
                        item.SubItems.Add(dr["Fecha"].ToString());

                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Básico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                        item.SubItems.Add(dr["Desc"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 3).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Iva 21"]), 3).ToString());

                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));

                        /*if (CalculaComprob() == 1)
                            item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.LightGreen, Color.Green, new System.Drawing.Font(
                            "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                        else if (CalculaComprob() == 2)
                            item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Yellow, Color.LightYellow, new System.Drawing.Font(
                            "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                        else if (CalculaComprob() == 3)
                            item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Red, Color.LightPink, new System.Drawing.Font(
                            "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));*/

                        item.SubItems.Add(dr["Observaciones"].ToString());
                        item.SubItems.Add(dr["NRORECIBOINTERNO"].ToString());

                        /*
                         if (Convert.ToInt32(dr["NRORECIBOINTERNO"].ToString()) == 0)
                             item.ImageIndex = 3;
                         else
                             item.ImageIndex = 2;
                         */


                        ///Estado de Comprobantes///
                        if (dr["IDESTADO"].ToString() == "12")
                        {
                            item.SubItems.Add("Pagado", Color.Green, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                            item.ImageIndex = 2;
                        }

                        if (dr["IDESTADO"].ToString() == "13")
                        {
                            item.SubItems.Add("Pago Parcial", Color.Yellow, Color.DarkGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                            item.ImageIndex = 4;
                        }

                        if (dr["IDESTADO"].ToString() == "14")
                        {
                            item.SubItems.Add("No Pagado", Color.Red, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                            item.ImageIndex = 3;
                        }

                        if (dr["IDESTADO"].ToString() == "15")
                        {
                            item.SubItems.Add("Pago Anticipado", Color.Blue, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                            item.ImageIndex = 4;
                        }

                        if (dr["IDESTADO"].ToString() == "19") //ANULADO
                        {
                            item.SubItems.Add("Anulado", Color.Red, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                            item.ImageIndex = 3;
                        }


                        dSaldoVinculado = dSaldoVinculado + Math.Round(Convert.ToDouble(item.SubItems[8].Text),2);
                        lblSaldo.Text= "$ " + dSaldoVinculado.ToString();                        
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        item.SubItems.Add(sNRecibo, Color.Blue, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));

                        ///MONTOS PAGADO Y PENDIENTES///
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Pagado"]), 2).ToString(), Color.Blue, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));

                        dPagado = Decimal.Round((decimal)dr["Pagado"], 2);
                        bMontoTotal = Decimal.Round((decimal)dr["Total"], 2);
                        dMontoPend = bMontoTotal - dPagado;

                        dPendiente = Decimal.Round((decimal)dr["Pendiente"], 2);
                        dSaldo = Decimal.Round((decimal)dr["Saldo"], 2);

                        item.SubItems.Add(Math.Round(dPendiente, 2).ToString(), Color.Blue, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                        item.SubItems.Add(Math.Round(dSaldo, 2).ToString(), Color.Blue, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        item.UseItemStyleForSubItems = false;
                        lvwComprobEmitido.Items.Add(item);
                    }
                    cm.Connection.Close();
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //VISUALIZA NOTAS DE CREDITO Vinculadas
                  /*  SqlCommand cm1 = new SqlCommand("SELECT NRONOTAINTERNO as 'Código', NRONOTACRED as 'Nro NC', Fecha, Basico as 'Básico', DESCUENTOS as 'Desc', IMPUESTO1 as 'Iva 10,5', IMPUESTO2 as 'Iva 21', TOTAL as 'Total', OBSERVACIONES as 'Observaciones', NRORECIBOINTERNO, IDESTADO, IdentificaComprob.IDENTIFICADOR, IdentificaComprob.IDIDENTIFICADOR FROM NotaCredito, IdentificaComprob WHERE IDCLIENTE = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[3].Text) + " AND NRORECIBOINTERNO = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + " AND IdentificaComprob.IDIDENTIFICADOR = NotaCredito.IDIDENTIFICADOR ORDER BY NRONOTAINTERNO", conectaEstado);

                    SqlDataAdapter da1 = new SqlDataAdapter(cm1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        lvwComprobEmitido.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr1["Código"].ToString());
                        item.SubItems.Add(dr1["Nro NC"].ToString());
                        item.SubItems.Add(dr1["IDENTIFICADOR"].ToString());
                        item.SubItems.Add(dr1["Fecha"].ToString());

                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr1["Básico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                        item.SubItems.Add(dr1["Desc"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr1["Iva 10,5"]), 3).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr1["Iva 21"]), 3).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr1["Total"]), 2).ToString(), Color.Empty, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));

                        item.SubItems.Add(dr1["Observaciones"].ToString());
                        item.SubItems.Add(dr1["NRORECIBOINTERNO"].ToString());

                        if (Convert.ToInt32(dr1["NRORECIBOINTERNO"].ToString()) == 0)
                            item.ImageIndex = 3;
                        else
                            item.ImageIndex = 2;

                        ///Estado de Comprobantes
                        if (dr1["IDESTADO"].ToString() == "12")
                            item.SubItems.Add("Pagado", Color.Green, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));


                        if (dr1["IDESTADO"].ToString() == "13")
                            item.SubItems.Add("Pago Parcial", Color.Yellow, Color.DarkGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));


                        if (dr1["IDESTADO"].ToString() == "14")
                            item.SubItems.Add("No Pagado", Color.Red, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));


                        if (dr1["IDESTADO"].ToString() == "15")
                            item.SubItems.Add("Pago Anticipado", Color.Blue, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));

                        dSaldoVinculado = dSaldoVinculado + Math.Round(Convert.ToDouble(item.SubItems[8].Text), 2);
                        lblSaldo.Text = "$ " + dSaldoVinculado.ToString();

                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        item.UseItemStyleForSubItems = false;
                        lvwComprobEmitido.Items.Add(item);
                    }
                    cm1.Connection.Close();
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                    //VISUALIZA NOTAS DE DEBITO NO VINCUDADAS
                    SqlCommand cm2 = new SqlCommand("SELECT NRONOTAINTERNO as 'Código', NRONOTADEBITO as 'Nro ND', Fecha, Basico as 'Básico', DESCUENTOS as 'Desc', IMPUESTO1 as 'Iva 10,5', IMPUESTO2 as 'Iva 21', TOTAL as 'Total', OBSERVACIONES as 'Observaciones', NRORECIBOINTERNO, IDESTADO, IdentificaComprob.IDENTIFICADOR, IdentificaComprob.IDIDENTIFICADOR FROM NotaDebito, IdentificaComprob WHERE IDCLIENTE = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[3].Text) + " AND NRORECIBOINTERNO = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + " AND IdentificaComprob.IDIDENTIFICADOR = NotaDebito.IDIDENTIFICADOR ORDER BY NRONOTAINTERNO", conectaEstado);

                    SqlDataAdapter da2 = new SqlDataAdapter(cm2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        lvwComprobEmitido.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr2["Código"].ToString());
                        item.SubItems.Add(dr2["Nro ND"].ToString());
                        item.SubItems.Add(dr2["IDENTIFICADOR"].ToString());
                        item.SubItems.Add(dr2["Fecha"].ToString());

                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr2["Básico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                        item.SubItems.Add(dr2["Desc"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr2["Iva 10,5"]), 3).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr2["Iva 21"]), 3).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr2["Total"]), 2).ToString(), Color.Empty, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));

                        item.SubItems.Add(dr2["Observaciones"].ToString());
                        item.SubItems.Add(dr2["NRORECIBOINTERNO"].ToString());

                        if (Convert.ToInt32(dr2["NRORECIBOINTERNO"].ToString()) == 0)
                            item.ImageIndex = 3;
                        else
                            item.ImageIndex = 2;

                        ///Estado de Comprobantes
                        if (dr2["IDESTADO"].ToString() == "12")
                            item.SubItems.Add("Pagado", Color.Green, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));


                        if (dr2["IDESTADO"].ToString() == "13")
                            item.SubItems.Add("Pago Parcial", Color.Yellow, Color.DarkGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));


                        if (dr2["IDESTADO"].ToString() == "14")
                            item.SubItems.Add("No Pagado", Color.Red, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));


                        if (dr2["IDESTADO"].ToString() == "15")
                            item.SubItems.Add("Pago Anticipado", Color.Blue, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));

                        dSaldoVinculado = dSaldoVinculado + Math.Round(Convert.ToDouble(item.SubItems[8].Text), 2);
                        lblSaldo.Text = "$ " + dSaldoVinculado.ToString();

                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        item.UseItemStyleForSubItems = false;
                        lvwComprobEmitido.Items.Add(item);
                    }
                    cm2.Connection.Close();*/
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
            }
            catch { }
        }

        private void btnCerrarVinculaFactu_Click(object sender, EventArgs e)
        {
            gbFactus.Visible = false;
        }

        private void btnVerFactus_Click(object sender, EventArgs e)
        {
            lblSaldo.Text = "$ " + "0,00";
            lblEtiquetaSaldo.Text = "Suma de Saldos Vinculados:";
            MostrarDatosCompVinculados(0);
        }

        private void MostrarDatosCompNoVinculados(int isituacion)
        {
            try
            {
                lvwComprobEmitido.Items.Clear();

                decimal dMontoPend;
                decimal bMontoTotal;
                decimal dPagado;

                string situacion;
                string sNRecibo;

                decimal dPendiente;
                decimal dSaldo;

                if (lvwRecibos.SelectedItems.Count == 0)
                    MessageBox.Show("Error: No se ha seleccionado ningún recibo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {

                    if (isituacion == 0)
                    {
                        situacion = "AND (NRORECIBOINTERNO = 0 OR IDESTADO = 13)";
                        //situacion = "";
                        lvwComprobEmitido.CheckBoxes = true;
                        lvwComprobEmitido.Columns[0].Width = 45;
                    }
                    else
                    {
                        situacion = "";
                        lvwComprobEmitido.CheckBoxes = true;
                        lvwComprobEmitido.Columns[0].Width = 45;
                        //lvwComprobEmitido.Columns[0].Width = 32;
                    }
                  
                    //VISUALIZA FACTURAS NO VINCULADAS
                    SqlCommand cm = new SqlCommand("SELECT NROFACTURAINTERNO as 'Código', NROFACTURA as 'Nro Fact', Fecha, Basico as 'Básico', DESCUENTOS as 'Desc', IMPUESTO1 as 'Iva 10,5', IMPUESTO2 as 'Iva 21', TOTAL as 'Total', Pagado, Pendiente, Saldo, OBSERVACIONES as 'Observaciones', NRORECIBOINTERNO, IDESTADO, IdentificaComprob.IDENTIFICADOR, IdentificaComprob.IDIDENTIFICADOR FROM FacturasVentas, IdentificaComprob WHERE FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND IDCLIENTE = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[4].Text) + " " + situacion + " AND IdentificaComprob.IDIDENTIFICADOR = FacturasVentas.IDIDENTIFICADOR AND FacturasVentas.Sucursal='" + sPtoVta+ "' ORDER BY NROFACTURAINTERNO", conectaEstado);                              

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    
                    foreach (DataRow dr in dt.Rows)
                    {
                        conn.LeeGeneric("SELECT * FROM Recibos WHERE IDEMPRESA = " + IDEMPRESA + " AND NRORECIBOINTERNO  = " + Convert.ToInt32(dr["NRORECIBOINTERNO"].ToString()) + " AND RECIBOS.SUCURSAL='" + sPtoVta + "'", "Recibos");
                        if (conn.leerGeneric.HasRows == true)
                        {
                            sNRecibo = conn.leerGeneric["NRORECIBO"].ToString();
                            conn.DesconectarBDLeeGeneric();
                        }
                        else
                        {
                            sNRecibo = "N° -----";
                            conn.DesconectarBDLeeGeneric();
                        }

                        lvwComprobEmitido.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["Código"].ToString());
                        item.SubItems.Add(dr["Nro Fact"].ToString());
                        item.SubItems.Add(dr["IDENTIFICADOR"].ToString());
                        item.SubItems.Add(dr["Fecha"].ToString());

                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Básico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                        item.SubItems.Add(dr["Desc"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Iva 10,5"]), 3).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Iva 21"]), 3).ToString());

                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Total"]), 2).ToString(), Color.Empty, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));

                        item.SubItems.Add(dr["Observaciones"].ToString());
                        item.SubItems.Add(dr["NRORECIBOINTERNO"].ToString());

                       /*
                        if (Convert.ToInt32(dr["NRORECIBOINTERNO"].ToString()) == 0)
                            item.ImageIndex = 3;
                        else
                            item.ImageIndex = 2;
                        */

                        ///Estado de Comprobantes///
                        if (dr["IDESTADO"].ToString() == "12")
                        {
                            item.SubItems.Add("Pagado", Color.Green, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                            item.ImageIndex = 2;
                        }

                        if (dr["IDESTADO"].ToString() == "13")
                        {
                            item.SubItems.Add("Pago Parcial", Color.Yellow, Color.DarkGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                            item.ImageIndex = 4;
                        }

                        if (dr["IDESTADO"].ToString() == "14")
                        {
                            item.SubItems.Add("No Pagado", Color.Red, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold)); 
                            item.ImageIndex = 3;
                        }

                        if (dr["IDESTADO"].ToString() == "15")
                        {
                            item.SubItems.Add("Pago Anticipado", Color.Blue, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                            item.ImageIndex = 4;
                        }

                        if (dr["IDESTADO"].ToString() == "19") //ANULADO
                        {
                            item.SubItems.Add("Anulado", Color.Red, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                            item.ImageIndex = 3;
                        }
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        item.SubItems.Add(sNRecibo, Color.Blue, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));

                        ///MONTOS PAGADO Y PENDIENTES///
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Pagado"]), 2).ToString(), Color.Blue, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));

                        dPagado = Decimal.Round((decimal)dr["Pagado"], 2);
                        bMontoTotal = Decimal.Round((decimal)dr["Total"], 2);
                        dMontoPend = bMontoTotal - dPagado;

                        dPendiente = Decimal.Round((decimal)dr["Pendiente"], 2);
                        dSaldo = Decimal.Round((decimal)dr["Saldo"], 2);

                        item.SubItems.Add(Math.Round(dPendiente, 2).ToString(), Color.Blue, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                        item.SubItems.Add(Math.Round(dSaldo, 2).ToString(), Color.Blue, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));

                        //item.SubItems.Add(Math.Round(dMontoPend, 2).ToString(), Color.Blue, Color.LightGray, new System.Drawing.Font("Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                        /////////////////////////////

                        item.UseItemStyleForSubItems = false;
                        lvwComprobEmitido.Items.Add(item);
                    }
                    cm.Connection.Close();


                    //VISUALIZA NOTAS DE CREDITO NO VINCUDADAS
               /*     SqlCommand cm1 = new SqlCommand("SELECT NRONOTAINTERNO as 'Código', NRONOTACRED as 'Nro NC', Fecha, Basico as 'Básico', DESCUENTOS as 'Desc', IMPUESTO1 as 'Iva 10,5', IMPUESTO2 as 'Iva 21', TOTAL as 'Total', OBSERVACIONES as 'Observaciones', NRORECIBOINTERNO, IDESTADO, IdentificaComprob.IDENTIFICADOR, IdentificaComprob.IDIDENTIFICADOR FROM NotaCredito, IdentificaComprob WHERE IDCLIENTE = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[3].Text) + " AND NRORECIBOINTERNO = 0 AND IdentificaComprob.IDIDENTIFICADOR = NotaCredito.IDIDENTIFICADOR ORDER BY NRONOTAINTERNO", conectaEstado);

                    SqlDataAdapter da1 = new SqlDataAdapter(cm1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        lvwComprobEmitido.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr1["Código"].ToString());
                        item.SubItems.Add(dr1["Nro NC"].ToString());
                        item.SubItems.Add(dr1["IDENTIFICADOR"].ToString());
                        item.SubItems.Add(dr1["Fecha"].ToString());

                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr1["Básico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                        item.SubItems.Add(dr1["Desc"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr1["Iva 10,5"]), 3).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr1["Iva 21"]), 3).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr1["Total"]), 2).ToString(), Color.Empty, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));

                        item.SubItems.Add(dr1["Observaciones"].ToString());
                        item.SubItems.Add(dr1["NRORECIBOINTERNO"].ToString());

                        if (Convert.ToInt32(dr1["NRORECIBOINTERNO"].ToString()) == 0)
                            item.ImageIndex = 3;
                        else
                            item.ImageIndex = 2;

                        ///Estado de Comprobantes
                        if (dr1["IDESTADO"].ToString() == "12")
                            item.SubItems.Add("Pagado", Color.Green, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));


                        if (dr1["IDESTADO"].ToString() == "13")
                            item.SubItems.Add("Pago Parcial", Color.Yellow, Color.DarkGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));


                        if (dr1["IDESTADO"].ToString() == "14")
                            item.SubItems.Add("No Pagado", Color.Red, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));


                        if (dr1["IDESTADO"].ToString() == "15")
                            item.SubItems.Add("Pago Anticipado", Color.Blue, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        item.UseItemStyleForSubItems = false;
                        lvwComprobEmitido.Items.Add(item);
                    }
                    cm1.Connection.Close();


                    //VISUALIZA NOTAS DE DEBITO NO VINCUDADAS
                    SqlCommand cm2 = new SqlCommand("SELECT NRONOTAINTERNO as 'Código', NRONOTADEBITO as 'Nro ND', Fecha, Basico as 'Básico', DESCUENTOS as 'Desc', IMPUESTO1 as 'Iva 10,5', IMPUESTO2 as 'Iva 21', TOTAL as 'Total', OBSERVACIONES as 'Observaciones', NRORECIBOINTERNO, IDESTADO, IdentificaComprob.IDENTIFICADOR, IdentificaComprob.IDIDENTIFICADOR FROM NotaDebito, IdentificaComprob WHERE IDCLIENTE = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[3].Text) + " AND NRORECIBOINTERNO = 0 AND IdentificaComprob.IDIDENTIFICADOR = NotaDebito.IDIDENTIFICADOR ORDER BY NRONOTAINTERNO", conectaEstado);

                    SqlDataAdapter da2 = new SqlDataAdapter(cm2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        lvwComprobEmitido.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr2["Código"].ToString());
                        item.SubItems.Add(dr2["Nro ND"].ToString());
                        item.SubItems.Add(dr2["IDENTIFICADOR"].ToString());
                        item.SubItems.Add(dr2["Fecha"].ToString());

                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr2["Básico"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                        item.SubItems.Add(dr2["Desc"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr2["Iva 10,5"]), 3).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr2["Iva 21"]), 3).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr2["Total"]), 2).ToString(), Color.Empty, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));

                        item.SubItems.Add(dr2["Observaciones"].ToString());
                        item.SubItems.Add(dr2["NRORECIBOINTERNO"].ToString());
                        if (Convert.ToInt32(dr2["NRORECIBOINTERNO"].ToString()) == 0)
                            item.ImageIndex = 3;
                        else
                            item.ImageIndex = 2;

                        ///Estado de Comprobantes
                        if (dr2["IDESTADO"].ToString() == "12")
                            item.SubItems.Add("Pagado", Color.Green, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));


                        if (dr2["IDESTADO"].ToString() == "13")
                            item.SubItems.Add("Pago Parcial", Color.Yellow, Color.DarkGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));


                        if (dr2["IDESTADO"].ToString() == "14")
                            item.SubItems.Add("No Pagado", Color.Red, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));


                        if (dr2["IDESTADO"].ToString() == "15")
                            item.SubItems.Add("Pago Anticipado", Color.Blue, Color.LightGray, new System.Drawing.Font(
                        "Microsoft Sans Serif", 8, System.Drawing.FontStyle.Bold));
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        item.UseItemStyleForSubItems = false;
                        lvwComprobEmitido.Items.Add(item);
                    }
                    cm2.Connection.Close();*/
                }
            }
            catch { conn.DesconectarBDLeeGeneric(); }
        }

        private void MostrarTodoLosDatos()
        {            
            MostrarDatosCompNoVinculados(1);
            //MostrarDatosCompVinculados(1);            
        }

        private void btnFactuNoVinculada_Click(object sender, EventArgs e)
        {
            lblSaldo.Text = "$ " + "0,00";
            lblEtiquetaSaldo.Text = "Suma de Saldos a Vincular:";
            MostrarDatosCompNoVinculados(0);
        }

        private void btnVinculaFactu_Click(object sender, EventArgs e)
        {
            ArmaSaldoComprobanteSeleccionado();
            //ActualizaFacturasPendientes(Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[3].Text));
            MostrarDatosCompNoVinculados(1);
        }       

        private void ArmaSaldoComprobanteSeleccionado()
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Se va a vincular el/los comprobante(s) seleccionado(s) al recibo seleccionado, ¿Es esto correcto? La operación no podrá deshacerse", "Vinculo de Comprobante(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (lvwRecibos.SelectedItems.Count == 0)
                        MessageBox.Show("Error: No se ha seleccionado ningún recibo a vincular", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {                        

                        char[] QuitaSimbolo = { '$', ' ' };

                        ListView.CheckedListViewItemCollection checkedItems = lvwComprobEmitido.CheckedItems;
                        foreach (ListViewItem item in checkedItems)
                        {
                            bool bDeudaCancelada = false;
                            decimal dImporteRecibo = 0;
                            decimal dTotalFactu = 0;
                            decimal dMontoPendiente = 0;
                            decimal dSaldo = 0;
                            decimal dSaldoComprobante = 0;
                            string dEstado = "";
                            string sNumComprobante = "";
                            int iIDCliente = 1;
                            int IDInternoComprobante = 1;
                            int IDInternoRecibo = 1;

                            decimal dMontoPagado = 0;                                                       

                            if (item.SubItems[2].Text == "FC")
                            {
                                if (item.SubItems[11].Text != "Pagado")
                                {
                                    conn1.LeeArticulo("SELECT * FROM Recibos WHERE IDEMPRESA = " + IDEMPRESA + " AND NroReciboInterno = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + " AND RECIBOS.SUCURSAL='" + sPtoVta + "'", "Recibos");
                                    dImporteRecibo = Math.Round(Convert.ToDecimal(conn1.leer["IMPORTERESTANTE"]), 2);
                                    conn1.DesconectarBDLee();

                                    dTotalFactu = Decimal.Round(Convert.ToDecimal(item.SubItems[8].Text), 2);
                                    dMontoPendiente = Decimal.Round(Convert.ToDecimal(item.SubItems[14].Text), 2);
                                    dEstado = item.SubItems[11].Text;
                                    sNumComprobante = item.SubItems[1].Text;
                                    iIDCliente = Convert.ToInt32(lvwRecibos.Items[0].SubItems[4].Text);
                                    ///////////////////////

                                    IDInternoComprobante = Convert.ToInt32(item.SubItems[0].Text);
                                    IDInternoRecibo = Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text);

                                   // dImporteRestanteRecibo = Math.Round(Convert.ToDecimal(lvwRecibos.SelectedItems[0].SubItems[9].Text), 2).ToString();

                                    if (dEstado == "No Pagado" || dEstado == "Pago Parcial")
                                    {
                                        if (dImporteRecibo == 0)
                                            MessageBox.Show("El recibo seleccionado no tiene saldo o bien ya ha sido vinculado a una factura anterior.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        else
                                        {

                                            if (dEstado == "Pago Parcial")
                                            {
                                                dTotalFactu = Decimal.Round(Convert.ToDecimal(item.SubItems[14].Text), 2);
                                                dMontoPendiente = dTotalFactu + dImporteRecibo;
                                                dSaldoComprobante = dMontoPendiente;
                                            }

                                            else
                                            {
                                                dMontoPendiente = dImporteRecibo - dTotalFactu;
                                                dSaldoComprobante = dMontoPendiente;
                                            }                          

                                            if (dSaldoComprobante <= 0)
                                                dSaldoComprobante = 0;                                                                                           

                                            if (dImporteRecibo <= dTotalFactu || dEstado == "Pago Parcial")
                                                dMontoPagado = dImporteRecibo;
                                            else
                                                dMontoPagado = dImporteRecibo - dTotalFactu;

                                            if (dMontoPendiente >= 0) {
                                                dMontoPendiente = 0;
                                                bDeudaCancelada = true;
                                            }
                                            else {
                                                dSaldo = dMontoPendiente;
                                                bDeudaCancelada = false;
                                            }

                                            //Actualiza Factura
                                            string actualizar = "NROReciboInterno = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + ", Pagado = (Cast(replace('" + dMontoPagado + "', ',', '.') as decimal(10,2))), Pendiente=(Cast(replace('" + dMontoPendiente + "', ',', '.') as decimal(10,2))), Saldo=(Cast(replace('" + dSaldoComprobante + "', ',', '.') as decimal(10,2)))";
                                            conn1.ActualizaArticulo("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + Convert.ToInt32(item.SubItems[0].Text) + "");
                                            
                                            //Actualiza Recibo
                                            string actualizar2 = "NROFacturaInterno = " + Convert.ToInt32(item.SubItems[0].Text) + ", IMPORTERESTANTE = (Cast(replace('" + dSaldoComprobante + "', ',', '.') as decimal(10, 2)))";
                                            conn1.ActualizaArticulo("Recibos", actualizar2, " IDEMPRESA = " + IDEMPRESA + " AND NroReciboInterno = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + "");                                            

                                            if (bDeudaCancelada == true) {
                                                string actualizar1 = "IdEstado = " + 12 + ""; //Pagado
                                                conn1.ActualizaArticulo("FacturasVentas", actualizar1, " IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + IDInternoComprobante + "");
                                            }
                                            else {
                                                string actualizar1 = "IdEstado = " + 13 + "";   //Pago Parcial
                                                conn1.ActualizaArticulo("FacturasVentas", actualizar1, " IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + IDInternoComprobante + "");
                                            }

                                            lblImporteRecibo.Text = "$ " + Math.Round(Convert.ToDecimal(lvwRecibos.SelectedItems[0].SubItems[8].Text), 2).ToString();
                                            lblSaldoResto.Text = "$ " + dSaldoComprobante.ToString();
                                            ImporteRestanteTotalRecibo = dSaldoComprobante;

                                            if (dSaldoComprobante == 0)
                                            {
                                                lblImporteRestante.ForeColor = Color.Red;
                                                lblImporteRestante.Text = "Recibo Imputado";

                                                string actualizar3 = "IdEstado = " + 18 + ""; //Recibo Vinculado
                                                conn1.ActualizaArticulo("Recibos", actualizar3, " IDEMPRESA = " + IDEMPRESA + " AND NROReciboInterno = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + "");
                                            }
                                            else
                                            {
                                                lblImporteRestante.ForeColor = Color.DarkGreen;
                                                lblImporteRestante.Text = "Recibo a Imputar";
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    lvwComprobEmitido.CheckBoxes = false;
                                    lvwComprobEmitido.Columns[0].Width = 32;
                                    MessageBox.Show("El/los movimiento(s) seleccionado(s) se encuentra(n) cerrado(s) y ya no puede(n) ser modificado(s).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }

                            /*  else if (item.SubItems[2].Text == "NC")
                                {
                                    string actualizar = "NROReciboInterno = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + "";
                                    conn1.ActualizaArticulo("NotaCredito", actualizar, " NRONOTAINTERNO = " + Convert.ToInt32(item.SubItems[0].Text) + "");
                                    CalculaComprob("NC", Convert.ToInt32(item.SubItems[0].Text), Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text), bDeudaCancelada);
                                }

                                else if (item.SubItems[2].Text == "ND")
                                {
                                    string actualizar = "NROReciboInterno = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + "";
                                    conn1.ActualizaArticulo("NotaDebito", actualizar, " NRONOTAINTERNO = " + Convert.ToInt32(item.SubItems[0].Text) + "");
                                    CalculaComprob("ND", Convert.ToInt32(item.SubItems[0].Text), Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text), bDeudaCancelada);
                                } */
                        }

                        conn1.DesconectarBD();

                        lblSaldo.Text = "$ " + "0,00";
                        MostrarDatosCompVinculados(0);
                    }
                }
            }
            catch { }        
        }

        private void ActualizaFacturasPendientes(int iIdCliente)
        {
            try
            {
                decimal dTotalFactu;
                decimal dPendiente;
                decimal dPagado;

                SqlCommand cm = new SqlCommand("SELECT * FROM FacturasVentas WHERE FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND IDCLIENTE = " + iIdCliente + " AND FacturasVentas.Sucursal = '" + sPtoVta + "' ORDER BY NROFACTURAINTERNO", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                   /*if (Convert.ToInt32(dr["IDESTADO"].ToString()) == 12)
                   {
                       dPagado = 0;
                       string actualizar = "PENDIENTE = (Cast(replace('" + dPagado + "', ',', '.') as decimal(10,2)))";
                        conn.ActualizaGeneric("FacturasVentas", actualizar, " NROFACTURAINTERNO = " + Convert.ToInt32(dr["NROFACTURAINTERNO"].ToString()) + ""); 
                   }*/

                    if ((Convert.ToInt32(dr["NRORECIBOINTERNO"].ToString())) == 0 && (dr["PAGADO"].ToString().Trim() == "0,0000"))
                   {
                       dTotalFactu = Decimal.Round(Convert.ToDecimal(dr["TOTAL"].ToString()), 2);
                       dPagado = Decimal.Round(Convert.ToDecimal(dr["PAGADO"].ToString()), 2);
                       dPendiente = -(dTotalFactu - dPagado);

                       //Actualiza Factura
                       string actualizar = "PENDIENTE = (Cast(replace('" + dPendiente + "', ',', '.') as decimal(10,2)))";
                       conn.ActualizaGeneric("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + Convert.ToInt32(dr["NROFACTURAINTERNO"].ToString()) + "");                       
                   }      
                   
                }
                cm.Connection.Close();

                //ArmaSaldoNoPagado(iIdCliente);
            }
            catch { MessageBox.Show("Error Inesperado, consultar a soporte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);  }
        }

        private void ArmaSaldoNoPagado(int iIdCliente)
        {
            try
            {
                decimal dTotalFactu = 0;
                decimal dNoPagado = 0;
                decimal dSaldo = 0;
                decimal dSaldoPendiente = 0;
                //decimal dSaldoPendiente = 0;
                decimal[] dFactuNoPagada = new decimal[200];
               
                int i = 0;
                SqlCommand cm = new SqlCommand("SELECT * FROM FacturasVentas WHERE IDEMPRESA = " + IDEMPRESA + " AND IDCLIENTE = " + iIdCliente + " OR (IDESTADO = 14 AND IDESTADO = 13) AND FacturasVentas.Sucursal = '"+ sPtoVta +"' ORDER BY NROFACTURAINTERNO", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    if ((Convert.ToInt32(dr["NRORECIBOINTERNO"].ToString()) == 0)) //&& dr["PAGADO"].ToString().Trim() == "0,0000")) //No Pagado
                    {
                        dTotalFactu = Decimal.Round(Convert.ToDecimal(dr["TOTAL"].ToString()), 2);
                        //dSaldoPendiente = Decimal.Round(Convert.ToDecimal(dr[""].ToString()), 2);

                        dFactuNoPagada[i] = dTotalFactu;
                        dNoPagado = dNoPagado + dFactuNoPagada[i];

                        if (i == 0)
                        {
                            dSaldo = dFactuNoPagada[i];
                            //Actualiza Factura
                            string actualizar = "Saldo=(Cast(replace('" + dSaldo + "', ',', '.') as decimal(10,2)))";
                            conn1.ActualizaArticulo("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + Convert.ToInt32(dr["NROFACTURAINTERNO"].ToString()) + "");
                        }
                        else
                        {
                            //Actualiza Factura
                            string actualizar = "Saldo=(Cast(replace('" + dNoPagado + "', ',', '.') as decimal(10,2)))";
                            conn1.ActualizaArticulo("FacturasVentas", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + Convert.ToInt32(dr["NROFACTURAINTERNO"].ToString()) + "");
                        }
                        i++;
                    }
                   /* else //Si es pago parcial
                    {   
                        dFactuNoPagada[i] = Decimal.Round(Convert.ToDecimal(dr["PENDIENTE"].ToString()), 2);
                        dNoPagado = dNoPagado + dFactuNoPagada[i];
                        
                        //Actualiza Factura
                        /*
                        string actualizar = "Saldo=(Cast(replace('" + dNoPagado + "', ',', '.') as decimal(10,2)))";
                        conn1.ActualizaArticulo("FacturasVentas", actualizar, " NROFACTURAINTERNO = " + Convert.ToInt32(dr["NROFACTURAINTERNO"].ToString()) + "");
                        
                        i++;
                    }                 */           
                }
                cm.Connection.Close();
            }

            catch { MessageBox.Show("Error Inesperado, consultar a soporte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void CalculaComprob(string comprobante, int nroComprobante, int NroRecibo, bool deuda)
        {
            try
            {
                double dTotalRecibo = 0;
                //double dTotaFactura = 0;
                char[] QuitaSimbolo = { '$', ' ' };

                dTotalRecibo = Convert.ToDouble(lvwRecibos.SelectedItems[0].SubItems[8].Text);      

                //FACTURA
                if (comprobante == "FC")
                {
                /*    if (dTotalRecibo > Convert.ToDouble(lblSaldo.Text.TrimStart(QuitaSimbolo))) //Pagado Anticipado
                    {
                        string actualizar = "IdEstado = " + 15 + "";
                        conn1.ActualizaArticulo("FacturasVentas", actualizar, " NROFACTURAINTERNO = " + nroComprobante + "");
                    }*/

                    if (dTotalRecibo < Convert.ToDouble(lblSaldo.Text.TrimStart(QuitaSimbolo))) //Pago Parcial
                    {
                        if (deuda == true)
                        {
                            string actualizar = "IdEstado = " + 12 + "";
                            conn1.ActualizaArticulo("FacturasVentas", actualizar, " FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + nroComprobante + "");
                        }

                        else
                        {
                            string actualizar = "IdEstado = " + 13 + "";
                            conn1.ActualizaArticulo("FacturasVentas", actualizar, " FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + nroComprobante + "");
                        }
                    }

                    else if (dTotalRecibo >= Convert.ToDouble(lblSaldo.Text.TrimStart(QuitaSimbolo))) //Pagado
                    {
                        string actualizar = "IdEstado = " + 12 + "";
                        conn1.ActualizaArticulo("FacturasVentas", actualizar, " FacturasVentas.IDEMPRESA = " + IDEMPRESA + " AND NROFACTURAINTERNO = " + nroComprobante + "");
                    }
                }

                //NOTA DE CREDITO
              /*  else if (comprobante == "NC")
                {
                    if (dTotalRecibo > Convert.ToDouble(lblSaldo.Text.TrimStart(QuitaSimbolo))) //Pagado Anticipado
                    {
                        string actualizar = "IdEstado = " + 15 + "";
                        conn1.ActualizaArticulo("NotaCredito", actualizar, " NRONOTAINTERNO = " + nroComprobante + "");
                    }

                    else if (dTotalRecibo < Convert.ToDouble(lblSaldo.Text.TrimStart(QuitaSimbolo))) //Pago Parcial
                    {
                        string actualizar = "IdEstado = " + 13 + "";
                        conn1.ActualizaArticulo("NotaCredito", actualizar, " NRONOTAINTERNO = " + nroComprobante + "");
                    }

                    else if (dTotalRecibo == Convert.ToDouble(lblSaldo.Text.TrimStart(QuitaSimbolo))) //Pagado
                    {
                        string actualizar = "IdEstado = " + 12 + "";
                        conn1.ActualizaArticulo("NotaCredito", actualizar, " NRONOTAINTERNO = " + nroComprobante + "");
                    }
                }

                //NOTA DE DEBITO
                else if (comprobante == "ND")
                {
                    if (dTotalRecibo > Convert.ToDouble(lblSaldo.Text.TrimStart(QuitaSimbolo))) //Pagado Anticipado
                    {
                        string actualizar = "IdEstado = " + 15 + "";
                        conn1.ActualizaArticulo("NotaDebito", actualizar, " NRONOTAINTERNO = " + nroComprobante + "");
                    }

                    else if (dTotalRecibo < Convert.ToDouble(lblSaldo.Text.TrimStart(QuitaSimbolo))) //Pago Parcial
                    {
                        string actualizar = "IdEstado = " + 13 + "";
                        conn1.ActualizaArticulo("NotaDebito", actualizar, " NRONOTAINTERNO = " + nroComprobante + "");
                    }

                    else if (dTotalRecibo == Convert.ToDouble(lblSaldo.Text.TrimStart(QuitaSimbolo))) //Pagado
                    {
                        string actualizar = "IdEstado = " + 12 + "";
                        conn1.ActualizaArticulo("NotaDebito", actualizar, " NRONOTAINTERNO = " + nroComprobante + "");
                    }
                }*/


                /*SqlCommand cm = new SqlCommand("SELECT * FROM FacturasVentas, IdentificaComprob WHERE IDCLIENTE = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[3].Text) + " AND NRORECIBOINTERNO = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + " AND  IdentificaComprob.IDIDENTIFICADOR = FacturasVentas.IDIDENTIFICADOR ORDER BY NROFACTURAINTERNO", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)                
                    dTotaFactura = dTotaFactura + Convert.ToDouble(dr["TOTAL"].ToString());    */     

                //cm.Connection.Close();

                /*conn.LeeGeneric("SELECT * FROM FacturasVentas, IdentificaComprob WHERE IDCLIENTE = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[3].Text) + " AND NRORECIBOINTERNO = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + " AND  IdentificaComprob.IDIDENTIFICADOR = FacturasVentas.IDIDENTIFICADOR ORDER BY NROFACTURAINTERNO", "Cliente");

                dTotaFactura = Convert.ToDouble(conn.leerGeneric["TOTAL"].ToString());
                conn.DesconectarBDLeeGeneric();*/
               


                //return 0;
            }
            catch { }
        }

        private void lvwComprobEmitido_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                double dSaldo = 0;
                double dRecibo = 0;
                double dFactura = 0;
                double dTotalRecibo = 0;

                char[] QuitaSimbolo1 = { '$', ' ' };
                char[] QuitaSimbolo2 = { '$', ' ' };
                char[] QuitaSimbolo3 = { '$', ' ' };

                
                ListView.CheckedListViewItemCollection checkedItems = lvwComprobEmitido.CheckedItems;
                foreach (ListViewItem item in checkedItems)
                    dSaldo = dSaldo + Convert.ToDouble(item.SubItems[14].Text);         

                if (dSaldo == 0.0)
                    lblSaldo.Text = "$ " + "0,00";
                else
                    lblSaldo.Text = "$ " + dSaldo.ToString();

                dFactura = Convert.ToDouble(lblSaldo.Text.TrimStart(QuitaSimbolo1));
                dRecibo = Convert.ToDouble(lblImporteRecibo.Text.TrimStart(QuitaSimbolo2));
                dTotalRecibo = Convert.ToDouble(lblImporteRecibo.Text.TrimStart(QuitaSimbolo2));

                dRecibo = dSaldo + (double)ImporteRestanteTotalRecibo;

                if ((checkedItems.Count == 0) || (lblImporteRestante.Text == "Recibo Imputado"))
                    lblSaldoResto.Text = "$ " + ImporteRestanteTotalRecibo;
                else if (lblImporteRestante.Text != "Recibo Imputado")
                    lblSaldoResto.Text = "$ " + Math.Round(dRecibo, 2).ToString();


                if (dRecibo > 0) {
                    lblSaldo.ForeColor = Color.DarkOliveGreen;
                    lblImporteRecibo.ForeColor = Color.DarkOliveGreen;

                    lblSRestante.ForeColor = Color.DarkOliveGreen;
                    lblSaldoResto.ForeColor = Color.DarkOliveGreen;
                }

                else if (dRecibo < 0) {
                    lblSaldo.ForeColor = Color.DarkOliveGreen;
                    lblImporteRecibo.ForeColor = Color.DarkOliveGreen;

                    lblSRestante.ForeColor = Color.Red;
                    lblSaldoResto.ForeColor = Color.Red;
                }

                else if (dRecibo == 0) {
                    lblSaldo.ForeColor = Color.DarkGreen;
                    lblImporteRecibo.ForeColor = Color.DarkGreen;

                    lblSRestante.ForeColor = Color.DarkGreen;
                    lblSaldoResto.ForeColor = Color.DarkGreen;
                }
            }
            catch { }
        }

        private void txtNroRecibo_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtNroRecibo_Leave(object sender, EventArgs e)
        {
            if (ValidaNumerador(txtNroRecibo.Text.Trim()) == true)
            {
                MessageBox.Show("Error de Numerador. Ya existe el numero ingresado, el numero ha sido corregido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NuevoRecibo();
            }
        }

        private void btnTodosComprob_Click(object sender, EventArgs e)
        {           
            MostrarTodoLosDatos();
        }

        private void lvwRecibos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                //timer1.Enabled = false;                
                MostrarItemsDatos();

                conn1.DesconectarBDLee();
                conn1.DesconectarBD();

                nuevoRecibo = false;

                idNRORECIBOINTERNO = Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text);
                indiceLvwRecibo = lvwRecibos.SelectedItems[0].Index;

                conn1.LeeArticulo("SELECT * FROM Recibos WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND Sucursal = '"+ sPtoVta.Trim() +"' AND NroReciboInterno = " + Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text) + " AND Recibos.Sucursal = '" + sPtoVta + "'", "Recibos");

                this.txtNroInternoRecibo.Text = conn1.leer["NroReciboInterno"].ToString();
                this.txtNroRecibo.Text = conn1.leer["NRORECIBO"].ToString();
                this.dtpFechaRecibo.Value = Convert.ToDateTime(conn1.leer["FECHA"].ToString());

                fechaRecibo = Convert.ToDateTime(conn1.leer["FECHA"].ToString());

                this.txtCodCliente.Text = conn1.leer["IDCLIENTE"].ToString();
                if (this.txtCodCliente.Text.Trim() == "")
                    this.cboCliente.Text = "";

                this.txtCodPersonal.Text = conn1.leer["IDPERSONAL"].ToString();
                if (this.txtCodPersonal.Text.Trim() == "")
                    this.cboPersonal.Text = "";

                this.txtCodVendedor.Text = conn1.leer["IDVENDEDOR"].ToString();
                if (this.txtCodVendedor.Text.Trim() == "")
                    this.cboVendedor.Text = "";

                this.txtObservacionFactura.Text = conn1.leer["OBSERVACIONES"].ToString();

                this.txtTotalRecibo.Text = "$ " + Math.Round(Convert.ToDecimal(conn1.leer["TOTAL"]), 2).ToString();                

                //btnEliminar.Enabled = true;
                //btnModificar.Enabled = true;
                //btnGuardar.Enabled = true;

                lblImporteRecibo.ForeColor = Color.DarkGreen;
                lblImporteRecibo.Text = "$ " + Math.Round(Convert.ToDecimal(lvwRecibos.SelectedItems[0].SubItems[8].Text), 2).ToString();
                lblSaldoResto.Text = "$ " + Math.Round(Convert.ToDecimal(conn1.leer["IMPORTERESTANTE"]), 2).ToString();

                ImporteRestanteTotalRecibo = Math.Round(Convert.ToDecimal(conn1.leer["IMPORTERESTANTE"]), 2);

                lblRazonSocial.Text = lvwRecibos.SelectedItems[0].SubItems[5].Text;

                string Prueba;
                Prueba = Math.Round(Convert.ToDecimal(conn1.leer["IMPORTERESTANTE"]), 2).ToString();

                if (Math.Round(Convert.ToDecimal(conn1.leer["IMPORTERESTANTE"]), 2).ToString() == "0,00")
                {   
                    lblImporteRestante.ForeColor = Color.Red;
                    lblImporteRestante.Text = "Recibo Imputado";
                }
                else
                {
                    lblImporteRestante.ForeColor = Color.DarkGreen;
                    lblImporteRestante.Text = "Recibo a Imputar";
                }
                conn1.DesconectarBDLee();
                conn1.DesconectarBD();

                MostrarItemsDatos();
                lvwComprobEmitido.Items.Clear();

                //  if (fechaFacturaCompra.AddDays(1) <= DateTime.Today)                                
                //      MessageBox.Show("No se puede modificar una factura de fecha pasada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MostrarTodoLosDatos();
                ActualizaFacturasPendientes(Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[4].Text));
            }
            catch { conn.DesconectarBD(); }
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboBuscaRecibo.SelectedIndex == 0) //N° comprobante                
                    BuscarDatos1();

                else if (cboBuscaRecibo.SelectedIndex == 1) //Id Cliente
                    BuscarDatos2();

                else if (cboBuscaRecibo.SelectedIndex == 2) //Razon Social
                    BuscarDatos3();

                else if (cboBuscaRecibo.SelectedIndex == 3) //Fecha Recibo
                    BuscarDatos4();                
            }
            catch { }
        }

        /// ///////////////////////////////////////////////////BLOQUE DE BUSQUEDA//////////////////////////////////////////////////////////////
        public void BuscarDatos1()
        {
            try
            {
                lvwRecibos.Items.Clear();
                lvwDetalleRecibo.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT Recibos.*, Cliente.* FROM Recibos, Cliente WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND Recibos.Sucursal = '" + sPtoVta + "' AND Recibos.IDCLIENTE = Cliente.IDCLIENTE AND Recibos.NRORECIBO LIKE '" + txtBuscarReciboCliente.Text.Trim() + "%'", conectaEstado);
                               

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwRecibos.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NRORECIBOINTERNO"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["NRORECIBO"].ToString());
                    item.SubItems.Add(dr["FECHA"].ToString());

                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["IDCLIENTE"]), 3).ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString(), Color.Empty, Color.LightGray, null);

                    item.SubItems.Add(dr["IDPERSONAL"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["IDVENDEDOR"]), 3).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["TOTAL"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());

                    item.SubItems.Add(dr["IMPORTERESTANTE"].ToString());

                    if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(360) <= DateTime.Today)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;
                    item.UseItemStyleForSubItems = false;
                    lvwRecibos.Items.Add(item);
                }

                cm.Connection.Close();
            }
            catch { }
        }

        public void BuscarDatos2()
        {
             try
             {
                 lvwRecibos.Items.Clear();
                 lvwDetalleRecibo.Clear();

                 SqlCommand cm = new SqlCommand("SELECT Recibos.*, Cliente.* FROM Recibos, Cliente WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND RECIBOS.SUCURSAL='" + sPtoVta + "' AND  Cliente.IDEMPRESA = " + IDEMPRESA + " AND Recibos.idcliente=Cliente.idcliente AND Cliente.idcliente = " + txtBuscarReciboCliente.Text + " ORDER BY RAZONSOCIAL", conectaEstado);

                 SqlDataAdapter da = new SqlDataAdapter(cm);
                 DataTable dt = new DataTable();
                 da.Fill(dt);

                 foreach (DataRow dr in dt.Rows)
                 {
                    lvwRecibos.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NRORECIBOINTERNO"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["NRORECIBO"].ToString());
                    item.SubItems.Add(dr["FECHA"].ToString());

                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["IDCLIENTE"]), 3).ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString(), Color.Empty, Color.LightGray, null);

                    item.SubItems.Add(dr["IDPERSONAL"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["IDVENDEDOR"]), 3).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["TOTAL"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());

                    item.SubItems.Add(dr["IMPORTERESTANTE"].ToString());

                    if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(360) <= DateTime.Today)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;
                    item.UseItemStyleForSubItems = false;
                    lvwRecibos.Items.Add(item);
                }

                 cm.Connection.Close();
             }
             catch { } 
        }

        public void BuscarDatos3()
        {
            try
            {
                lvwRecibos.Items.Clear();
                lvwDetalleRecibo.Clear();

                if ((cboBuscaRecibo.SelectedIndex == 2 && txtBuscarReciboCliente.Text == "") || txtBuscarReciboCliente.Text == "*")
                {
                    if (txtBuscarReciboCliente.Text == "*")
                        txtBuscarReciboCliente.Text = "";

                    SqlCommand cm = new SqlCommand("SELECT Recibos.*, Cliente.* FROM Recibos, Cliente WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND RECIBOS.SUCURSAL='" + sPtoVta + "' AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.idcliente=Recibos.IdCliente AND Cliente.RAZONSOCIAL LIKE '" + txtBuscarReciboCliente.Text + "%' ORDER BY RAZONSOCIAL " + txtBuscarReciboCliente.Text.Trim() + "", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwRecibos.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["NRORECIBOINTERNO"].ToString());
                        item.SubItems.Add(dr["Sucursal"].ToString());
                        item.SubItems.Add(dr["NRORECIBO"].ToString());
                        item.SubItems.Add(dr["FECHA"].ToString());

                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["IDCLIENTE"]), 3).ToString());
                        item.SubItems.Add(dr["RAZONSOCIAL"].ToString(), Color.Empty, Color.LightGray, null);

                        item.SubItems.Add(dr["IDPERSONAL"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["IDVENDEDOR"]), 3).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["TOTAL"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                        item.SubItems.Add(dr["Observaciones"].ToString());

                        item.SubItems.Add(dr["IMPORTERESTANTE"].ToString());

                        if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(360) <= DateTime.Today)
                            item.ImageIndex = 1;
                        else
                            item.ImageIndex = 0;
                        item.UseItemStyleForSubItems = false;
                        lvwRecibos.Items.Add(item);
                    }
                    cm.Connection.Close();
                }

                else if (cboBuscaRecibo.SelectedIndex == 2 && txtBuscarReciboCliente.Text != "")
                {
                    SqlCommand cm = new SqlCommand("SELECT Recibos.*, Cliente.* FROM Recibos, Cliente WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND RECIBOS.SUCURSAL='" + sPtoVta + "' AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IdCliente = Recibos.IDCliente AND Cliente.RAZONSOCIAL LIKE '" + txtBuscarReciboCliente.Text + "%' ORDER BY RAZONSOCIAL", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwRecibos.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["NRORECIBOINTERNO"].ToString());
                        item.SubItems.Add(dr["Sucursal"].ToString());
                        item.SubItems.Add(dr["NRORECIBO"].ToString());
                        item.SubItems.Add(dr["FECHA"].ToString());

                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["IDCLIENTE"]), 3).ToString());
                        item.SubItems.Add(dr["RAZONSOCIAL"].ToString(), Color.Empty, Color.LightGray, null);

                        item.SubItems.Add(dr["IDPERSONAL"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["IDVENDEDOR"]), 3).ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["TOTAL"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                        item.SubItems.Add(dr["Observaciones"].ToString());

                        item.SubItems.Add(dr["IMPORTERESTANTE"].ToString());

                        if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(360) <= DateTime.Today)
                            item.ImageIndex = 1;
                        else
                            item.ImageIndex = 0;
                        item.UseItemStyleForSubItems = false;
                        lvwRecibos.Items.Add(item);
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
                lvwRecibos.Items.Clear();
                lvwDetalleRecibo.Clear();

                SqlCommand cm = new SqlCommand("SELECT Recibos.*, Cliente.* FROM Recibos, Cliente WHERE Recibos.IDEMPRESA = " + IDEMPRESA + " AND RECIBOS.SUCURSAL='" + sPtoVta + "' AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND Recibos.IDCLIENTE = Cliente.IDCLIENTE AND Cliente.RazonSocial LIKE '" + txtBuscarReciboCliente.Text.Trim() + "%'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwRecibos.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NRORECIBOINTERNO"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["NRORECIBO"].ToString());
                    item.SubItems.Add(dr["FECHA"].ToString());

                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["IDCLIENTE"]), 3).ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString(), Color.Empty, Color.LightGray, null);

                    item.SubItems.Add(dr["IDPERSONAL"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["IDVENDEDOR"]), 3).ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["TOTAL"]), 3).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["Observaciones"].ToString());

                    item.SubItems.Add(dr["IMPORTERESTANTE"].ToString());

                    if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(360) <= DateTime.Today)
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;
                    item.UseItemStyleForSubItems = false;
                    lvwRecibos.Items.Add(item);
                }

                cm.Connection.Close();
            }
            catch { }
        }

        private void tsBtnReporte_Click(object sender, EventArgs e)
        {
            /*try
            {
                if (lvwRecibos.Items.Count == 0)
                    MessageBox.Show("Error: No existen datos para mostrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DGestion.Reportes.frmRPTRecibo frmRptReciboRealizados = new DGestion.Reportes.frmRPTRecibo();
                    frmRptReciboRealizados.ShowDialog();
                }
            }
            catch 
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                MessageBox.Show("Error: No se ha seleccionado el comprobante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/

            try
            {
                if (lvwRecibos.Items.Count == 0)
                    MessageBox.Show("Error: No existen datos para mostrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    nroRecibo = lvwRecibos.SelectedItems[0].SubItems[2].Text;
                    NroReciboInt = Convert.ToInt32(lvwRecibos.SelectedItems[0].SubItems[0].Text);

                    DGestion.Reportes.frmRPTRecibo frmRptRecibo = new DGestion.Reportes.frmRPTRecibo();
                    frmRptRecibo.ShowDialog();
                }
            }
            catch //(System.Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                MessageBox.Show("Error: No se ha seleccionado el comprobante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gpoComprasAProveedor_Enter(object sender, EventArgs e)
        {

        }

        private void tsRecNoImputado_Click(object sender, EventArgs e)
        {
            try { 
            btnVeComprobante.Enabled = false;
            btnComprobNoVinculados.Enabled = false;

            gpbRecNoImputado.Visible = true;


            //if (lvwReciboNoImputado.SelectedItems.Count == 1)
            //{
                gpbRecNoImputado.Visible = true;
                lvwReciboNoImputado.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT Recibos.*, Cliente.RazonSocial FROM Recibos, Cliente WHERE IMPORTERESTANTE >'0,00' AND CLIENTE.IDEMPRESA=" + IDEMPRESA + " AND RECIBOS.IDEMPRESA=" + IDEMPRESA+" AND SUCURSAL ="+ sPtoVta.Trim() +" AND RECIBOS.IDCLIENTE = CLIENTE.IDCLIENTE ORDER BY NRORECIBO", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwReciboNoImputado.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NRORECIBOINTERNO"].ToString());
                    item.SubItems.Add(dr["SUCURSAL"].ToString());
                    item.SubItems.Add(dr["NRORECIBO"].ToString());
                    item.SubItems.Add(dr["FECHA"].ToString());
                    item.SubItems.Add(dr["IMPORTERESTANTE"].ToString());
                    item.SubItems.Add(dr["IDESTADO"].ToString());
                    item.SubItems.Add(dr["RazonSocial"].ToString());


                    if (item.SubItems[5].Text == "17")
                        item.ImageIndex = 5;
                    else
                        item.ImageIndex = 6;
                    

                    item.UseItemStyleForSubItems = false;
                    lvwReciboNoImputado.Items.Add(item);
                }
                cm.Connection.Close();
            }
          catch 
            {
                MessageBox.Show("No hay información para mostrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gpbRecNoImputado.Visible = false;
            }
        }

        private void btnCerrarRecNoImputado_Click(object sender, EventArgs e)
        {
            btnVeComprobante.Enabled = true;
            btnComprobNoVinculados.Enabled = true;
            gpbRecNoImputado.Visible = false;
        }

        private void lvwComprobEmitido_SelectedIndexChanged(object sender, EventArgs e)
        {
            gpbRecNoImputado.Visible = false;
        }
        /////////////////////////////////////////////////////FIN BLOQUE DE BUSQUEDA///////////////////////////////////////////////////////////////// 

        private void GuardaMovimientoTesoreria(int nroReciboInter, string numRecibo)
        {        
            ////////////////////////////////////////PROCEDIMIENTO TESORERIA///////////////////////////////////////////////
            try
            {
                int iUltimoIdCaja;

                connGeneric.LeeGeneric("SELECT MAX(NroCaja) as NroUltimaCajaAbierta FROM TesoreriaCaja WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = " + sPtoVta + " AND IdEstadoCaja=20", "TesoreriaCaja");
                iUltimoIdCaja = Convert.ToInt32(connGeneric.leerGeneric["NroUltimaCajaAbierta"].ToString());

                if (frmPrincipal.PtoVenta == "0001" && IDEMPRESA == 1)
                {
                    if (frmPrincipal.PtoVenta == "0001" && IDEMPRESA == 1)
                    {
                        if (txtCodTipoPago.Text == "1")  //CHEQUE
                        {
                            char[] QuitaSimbolo = { '$', ' ' };
                            TesoreriaMovimientoCaja caja1 = new TesoreriaMovimientoCaja();
                            caja1.IngresoCaja(sPtoVta, 1, "BANCO-CHEQUE", Convert.ToDecimal(txtImporteDetalleRecibo.Text.TrimStart((QuitaSimbolo))), 0, frmPrincipal.Usuario, iUltimoIdCaja, 0, nroReciboInter, "0", numRecibo);
                        }
                        else if (txtCodTipoPago.Text == "2") //EFECTIVO
                        {
                            char[] QuitaSimbolo = { '$', ' ' };
                            TesoreriaMovimientoCaja caja1 = new TesoreriaMovimientoCaja();
                            caja1.IngresoCaja(sPtoVta, 2, "EFECTIVO", Convert.ToDecimal(txtImporteDetalleRecibo.Text.TrimStart((QuitaSimbolo))), 0, frmPrincipal.Usuario, iUltimoIdCaja, 0, nroReciboInter, "0", numRecibo);
                        }
                        if (txtCodTipoPago.Text == "6") //TRANSFERENCIA
                        {
                            char[] QuitaSimbolo = { '$', ' ' };
                            TesoreriaMovimientoCaja caja1 = new TesoreriaMovimientoCaja();
                            caja1.IngresoCaja(sPtoVta, 6, "BANCO-TRANSF", Convert.ToDecimal(txtImporteDetalleRecibo.Text.TrimStart((QuitaSimbolo))), 0, frmPrincipal.Usuario, iUltimoIdCaja, 0, nroReciboInter, "0", numRecibo);
                        }
                        if (txtCodTipoPago.Text == "7") //TARJETA
                        {
                            char[] QuitaSimbolo = { '$', ' ' };
                            TesoreriaMovimientoCaja caja1 = new TesoreriaMovimientoCaja();
                            caja1.IngresoCaja(sPtoVta, 7, "BANCO-TARJETA", Convert.ToDecimal(txtImporteDetalleRecibo.Text.TrimStart((QuitaSimbolo))), 0, frmPrincipal.Usuario, iUltimoIdCaja, 0, nroReciboInter, "0", numRecibo);
                        }
                    }
                }


                else
                {
                    if (txtCodTipoPago.Text == "1")  //CHEQUE
                    {
                        char[] QuitaSimbolo = { '$', ' ' };
                        TesoreriaMovimientoCaja caja1 = new TesoreriaMovimientoCaja();
                        caja1.IngresoCaja(sPtoVta, 1, "BANCO-CHEQUE", Convert.ToDecimal(txtImporteDetalleRecibo.Text.TrimStart((QuitaSimbolo))), 0, frmPrincipal.Usuario, iUltimoIdCaja, 0, nroReciboInter, "0", numRecibo);
                    }

                    else if (txtCodTipoPago.Text == "2") //EFECTIVO
                    {
                        char[] QuitaSimbolo = { '$', ' ' };
                        TesoreriaMovimientoCaja caja1 = new TesoreriaMovimientoCaja();
                        caja1.IngresoCaja(sPtoVta, 2, "EFECTIVO", Convert.ToDecimal(txtImporteDetalleRecibo.Text.TrimStart((QuitaSimbolo))), 0, frmPrincipal.Usuario, iUltimoIdCaja, 0, nroReciboInter, "0", numRecibo);
                    }

                    if (txtCodTipoPago.Text == "6") //TRANSFERENCIA
                    {
                        char[] QuitaSimbolo = { '$', ' ' };
                        TesoreriaMovimientoCaja caja1 = new TesoreriaMovimientoCaja();
                        caja1.IngresoCaja(sPtoVta, 6, "BANCO-TRANSF", Convert.ToDecimal(txtImporteDetalleRecibo.Text.TrimStart((QuitaSimbolo))), 0, frmPrincipal.Usuario, iUltimoIdCaja, 0, nroReciboInter, "0", numRecibo);
                    }

                    if (txtCodTipoPago.Text == "7") //TARJETA
                    {
                        char[] QuitaSimbolo = { '$', ' ' };
                        TesoreriaMovimientoCaja caja1 = new TesoreriaMovimientoCaja();
                        caja1.IngresoCaja(sPtoVta, 7, "BANCO-TARJETA", Convert.ToDecimal(txtImporteDetalleRecibo.Text.TrimStart((QuitaSimbolo))), 0, frmPrincipal.Usuario, iUltimoIdCaja, 0, nroReciboInter, "0", numRecibo);
                    }

                    //OTROS
                    if (txtCodTipoPago.Text == "3") //RET. GANANCIA
                    {
                        char[] QuitaSimbolo = { '$', ' ' };
                        TesoreriaMovimientoCaja caja1 = new TesoreriaMovimientoCaja();
                        caja1.IngresoCaja(sPtoVta, 3, "BANCO-RET.GAN", 0, Convert.ToDecimal(txtImporteDetalleRecibo.Text.TrimStart((QuitaSimbolo))), frmPrincipal.Usuario, iUltimoIdCaja, 0, nroReciboInter, "0", numRecibo);
                    }

                    if (txtCodTipoPago.Text == "4") //RET. I.B.
                    {
                        char[] QuitaSimbolo = { '$', ' ' };
                        TesoreriaMovimientoCaja caja1 = new TesoreriaMovimientoCaja();
                        caja1.IngresoCaja(sPtoVta, 4, "BANCO-RET.I.B", 0, Convert.ToDecimal(txtImporteDetalleRecibo.Text.TrimStart((QuitaSimbolo))), frmPrincipal.Usuario, iUltimoIdCaja, 0, nroReciboInter, "0", numRecibo);
                    }

                    if (txtCodTipoPago.Text == "5") //RET. I.V.A.
                    {
                        char[] QuitaSimbolo = { '$', ' ' };
                        TesoreriaMovimientoCaja caja1 = new TesoreriaMovimientoCaja();
                        caja1.IngresoCaja(sPtoVta, 5, "BANCO-RET.IVA", 0, Convert.ToDecimal(txtImporteDetalleRecibo.Text.TrimStart((QuitaSimbolo))), frmPrincipal.Usuario, iUltimoIdCaja, 0, nroReciboInter, "0", numRecibo);
                    }
                    ///////////////////////////////////////
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            catch { }
        }
    }

}