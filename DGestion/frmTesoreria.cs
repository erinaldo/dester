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
    public partial class frmTesoreria : Form
    {
        CGenericBD connGeneric = new CGenericBD();
        EmpresaBD connEmpresa = new EmpresaBD();        
        ArticulosBD connArt = new ArticulosBD();


        Permisos connPermi = new Permisos();
        string sUsuarioLegueado;

        string sPtoVta;
        int IDEMPRESA;

        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        decimal TotalBanco;
        decimal TotalEfectivo;
        decimal TotalGasto;

        public frmTesoreria()
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
                sPtoVta = frmPrincipal.PtoVenta.Trim();

                return IdEmpresa;
            }
            catch { return 0; }
        }

        private void conPermi()
        {
            try
            {                
                sUsuarioLegueado = frmPrincipal.Usuario;

                /*
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

                cm.Connection.Close();
                */
            }
            catch { }
        }

        private string FormateoFecha()
        {
            DateTimePicker dtr = new DateTimePicker();
            dtr.Value = DateTime.Now;
            return String.Format("{0:d/M/yyyy HH:mm:ss}", dtr.Value);
        }

        private void tsCaja_Click(object sender, EventArgs e)
        {
            gpbConfigCaja.Visible = true;
            MostrarListaCaja();
        }

        private void tsMovimientos_Click(object sender, EventArgs e)
        {
            gpbMovimiento.Visible = true;
            gpbCajas.Visible = true;
        }       

        private void tsSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTesoreria_Load(object sender, EventArgs e)
        {
            FormatoListView();
            IDEMPRESA = ConsultaEmpresa(); //Lee Empresa
            conPermi();

            MostrarCaja();
            MostrarListaCaja();

            UltimosSaldosCaja();
        }

        private void FormatoListView()
        {
            lvwMovimientoCaja.View = View.Details;
            lvwMovimientoCaja.LabelEdit = true;
            lvwMovimientoCaja.AllowColumnReorder = true;
            lvwMovimientoCaja.FullRowSelect = true;
            lvwMovimientoCaja.GridLines = true;

            lvwCaja.View = View.Details;
            lvwCaja.LabelEdit = true;
            lvwCaja.AllowColumnReorder = true;
            lvwCaja.FullRowSelect = true;
            lvwCaja.GridLines = true;

            lvwListaCaja.View = View.Details;
            lvwListaCaja.LabelEdit = true;
            lvwListaCaja.AllowColumnReorder = true;
            lvwListaCaja.FullRowSelect = true;
            lvwListaCaja.GridLines = true;
        }        

        private void btnAceptarNuevaCaja_Click(object sender, EventArgs e)
        {
            //Validacion de Creacion de Caja, verifica que esten las cajas cerradas

            int iIdEstadoCaja;
            TesoreriaMovimientoCaja valida = new TesoreriaMovimientoCaja();
            iIdEstadoCaja = valida.ValidaCreacionCaja(txtNombreCaja.Text);

            if (iIdEstadoCaja == 20)
                MessageBox.Show("Existe caja abierta con el nombre seleccionado, deberá cerrar la caja anterior para crear una nueva.", "Estado de Caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (iIdEstadoCaja == 21)
                MessageBox.Show("Existen error en la caja seleccionada, consultar al administrador de sistema.", "Error de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (iIdEstadoCaja == 0)
            {
                gpbConfigCaja.Enabled = true;

                string agregarCaja = "INSERT INTO TesoreriaCaja(Sucursal, NombreCaja, Usuario, FechaApertura, SaldoApertura, FechaCreacion, IdEstadoCaja, IdEmpresa) VALUES ('" + sPtoVta.Trim() + "', '" + txtNombreCaja.Text.Trim() + "', '" + sUsuarioLegueado.Trim() + "', '" + FormateoFecha() + "', '0', '" + FormateoFecha() + "', " + 20 + " ," + IDEMPRESA + ")";
                connGeneric.InsertarGeneric(agregarCaja);

                gpbCreacion.Visible = false;
                MostrarListaCaja();
                MostrarCaja();
            }
        }

        private void btnCerrarCreacionCaja_Click(object sender, EventArgs e)
        {
            gpbCreacion.Visible = false;
            gpbConfigCaja.Enabled = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            gpbCreacion.Visible = true;
            //gpbConfigCaja.Enabled = false;
            txtNombreCaja.Text = "MOSTRADOR";
            txtNroCaja.Text =UltimaNroCaja().ToString();
        }

        private void btnCerrarMovimientoCaja_Click(object sender, EventArgs e)
        {
            gpbMovimiento.Visible = false;
            gpbCajas.Visible = false;
        }
               

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            gpbConfigCaja.Visible = false;
        }

        private void MostrarCaja()
        {
            try
            {
                lvwCaja.Items.Clear();                               

                SqlCommand cm = new SqlCommand("SELECT * FROM TesoreriaCaja WHERE TesoreriaCaja.IdEmpresa = " + IDEMPRESA + " AND TesoreriaCaja.Sucursal = '" + sPtoVta + "' ORDER BY TesoreriaCaja.NroCaja desc", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwCaja.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NroCaja"].ToString());
                    item.SubItems.Add(dr["NombreCaja"].ToString());

                    if (dr["IdEstadoCaja"].ToString() == "20")
                    {
                        item.SubItems.Add("Caja Abierta");
                        item.ImageIndex = 3;
                    }
                    else
                    {
                        item.SubItems.Add("Caja Cerrada");
                        item.ImageIndex = 2;
                    }

                    item.SubItems.Add(dr["FechaApertura"].ToString());
                    item.SubItems.Add(dr["SaldoApertura"].ToString());

                    item.SubItems.Add(dr["FechaCierre"].ToString());
                    item.SubItems.Add(dr["SaldoCierre"].ToString());
                    item.SubItems.Add(dr["IngresoTotal"].ToString());

                    item.SubItems.Add(dr["Usuario"].ToString());                    

                    /*if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(180) <= DateTime.Today)
                        item.ImageIndex = 2;
                    else
                        item.ImageIndex = 0;*/
                        
                    item.UseItemStyleForSubItems = false;
                    lvwCaja.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch //(System.Exception excep)
            {
                //Auditoria
                //AuditoriaSistema AS3 = new AuditoriaSistema();
                //AS3.SistemaProcesoAuditor_0003("Proc. MostrarDatos() ", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////            
            }
        }

        private void MostrarListaCaja()
        {
            try
            {
                lvwListaCaja.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM TesoreriaCaja WHERE TesoreriaCaja.IdEmpresa = " + IDEMPRESA + " AND TesoreriaCaja.Sucursal = '" + sPtoVta + "' ORDER BY TesoreriaCaja.NroCaja", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwListaCaja.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NroCaja"].ToString());
                    item.SubItems.Add(dr["NombreCaja"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());                    
                    item.SubItems.Add(dr["FechaCreacion"].ToString());

                    item.ImageIndex = 0;

                    item.UseItemStyleForSubItems = false;
                    lvwListaCaja.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch //(System.Exception excep)
            {
                //Auditoria
                //AuditoriaSistema AS3 = new AuditoriaSistema();
                //AS3.SistemaProcesoAuditor_0003("Proc. MostrarDatos() ", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////            
            }
        }

        private int UltimaNroCaja()
        {
            int nroCaja = 0;

            connGeneric.LeeGeneric("SELECT MAX(NROCAJA) as NroCaja FROM TesoreriaCaja WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = " + sPtoVta + " ORDER BY NroCaja", "Caja");

            if (connGeneric.leerGeneric["NroCaja"].ToString() == "")
                return 1;
            else
            {
                nroCaja = Convert.ToInt32(connGeneric.leerGeneric["NroCaja"].ToString());                
                nroCaja = nroCaja + 1;
                return nroCaja;
            }
        }


        private void UltimosSaldosCaja()
        {
            try
            {
                int nroCaja = 0;

                connGeneric.LeeGeneric("SELECT MAX(NROCAJA) as NroCaja FROM TesoreriaCaja WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = " + sPtoVta + " ORDER BY NroCaja", "Caja");
                nroCaja = Convert.ToInt32(connGeneric.leerGeneric["NROCAJA"].ToString());
                connGeneric.LeeGeneric("SELECT TesoreriaCaja.SaldoApertura, TesoreriaCaja.SaldoCierre FROM TesoreriaCaja WHERE IDEMPRESA = " + IDEMPRESA + " AND SUCURSAL = " + sPtoVta + " AND NroCaja = " + nroCaja + " ORDER BY NroCaja", "Caja");

                lblSaldoApertura.Text = "$ " + Math.Round(Convert.ToDecimal(connGeneric.leerGeneric["SaldoApertura"].ToString()), 2);
                lblSaldoCierre.Text = "$ " + Math.Round(Convert.ToDecimal(connGeneric.leerGeneric["SaldoCierre"].ToString()), 2);
            }
            catch { }
        }

        private void tsAbrirCaja_Click(object sender, EventArgs e)
        {
            try
            {
                //if (frmPrincipal.PtoVenta == "0001" & IDEMPRESA == 1)
                //{
                    if (lvwCaja.SelectedItems[0].SubItems[2].Text == "Caja Cerrada")
                    {
                        int UltimaCajaVerficada;
                        UltimaCajaVerficada=UltimaNroCaja();


                        if ((UltimaCajaVerficada-1) == Convert.ToInt32(lvwCaja.SelectedItems[0].SubItems[0].Text))
                        {
                            //decimal dUltimoSaldo; 
                            //dUltimoSaldo = BuscaUltimoSaldo(Convert.ToInt32(lvwCaja.SelectedItems[0].SubItems[0].Text), lvwCaja.SelectedItems[0].SubItems[1].Text); //Busa el Ultimo saldo

                            //Crea Nueva Caja
                            string agregarCaja = "INSERT INTO TesoreriaCaja(Sucursal, NombreCaja, Usuario, FechaApertura, SaldoApertura, FechaCreacion, IdEstadoCaja, IdEmpresa) VALUES ('" + sPtoVta.Trim() + "', '" + lvwCaja.SelectedItems[0].SubItems[1].Text + "', '" + sUsuarioLegueado.Trim() + "', '" + FormateoFecha() + "', Cast(replace('" + lvwCaja.SelectedItems[0].SubItems[6].Text + "', ',', '.') as decimal(10, 2)), '" + FormateoFecha() + "', " + 20 + " ," + IDEMPRESA + ")";
                            connGeneric.InsertarGeneric(agregarCaja);

                            int iUltimaCaja;
                            iUltimaCaja = UltimaNroCaja();

                            //Actualiza Estado
                            string actualizar = "IdEstadoCaja = '20', FechaApertura = '" + FormateoFecha() + "'";
                            connGeneric.ActualizaGeneric("TesoreriaCaja", actualizar, " NroCaja = " + iUltimaCaja + " ");                            

                            MostrarCaja();

                            lblSaldoApertura.Text = "$ " + "0,00";
                            lblSaldoCierre.Text = "$ " + "0,00";

                            UltimosSaldosCaja();
                        }
                        else
                            MessageBox.Show("El orden correlativo en la selección de caja es incorrecto para el cálculo del saldo apertura. Debe seleccionar la ultima caja que se cerró.", "Estado de Caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show("La caja ya se encuentra abierta.", "Estado de Caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                //else
                //    MessageBox.Show("Empresa y Pto. Vta. No Habilitado para operar.", "Estado de Caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { MessageBox.Show("No existe una caja inicial para operar.", "Estado de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);  }
        }

        private void tsCerrarCaja_Click(object sender, EventArgs e)
        {
            try
            {
                //if (frmPrincipal.PtoVenta == "0001" & IDEMPRESA == 1)
                //{
                if (lvwCaja.SelectedItems[0].SubItems[2].Text == "Caja Abierta")
                {
                    decimal iIngresoTotalEfectivo = 0;
                    decimal iIngresoTotalBanco = 0;
                    decimal iEgresoTotal = 0;

                    decimal dSaldoApertura = 0;
                    decimal iTotal = 0;

                    iIngresoTotalEfectivo = TotalEfectivo;
                    iIngresoTotalBanco = TotalBanco;
                    iEgresoTotal = TotalGasto;

                    dSaldoApertura = Convert.ToDecimal(lvwCaja.SelectedItems[0].SubItems[4].Text);

                    //iTotal = (dSaldoApertura) + ((TotalEfectivo + TotalBanco) - iEgresoTotal);
                    iTotal = (dSaldoApertura) + ((TotalEfectivo) - iEgresoTotal);

                    string actualizar = "IdEstadoCaja = '21', FechaCierre = '" + FormateoFecha() + "', SaldoCierre =  (Cast(replace('" + Math.Round(iTotal, 2) + "', ',', '.') as decimal(10, 2)))";
                    connGeneric.ActualizaGeneric("TesoreriaCaja", actualizar, " NroCaja = " + lvwCaja.SelectedItems[0].SubItems[0].Text + " AND Sucursal ='" + frmPrincipal.PtoVenta + "' AND IDEMPRESA =" + IDEMPRESA + "");

                    MostrarCaja();

                    lblSaldoApertura.Text = "$ " + "0,00";
                    lblSaldoCierre.Text = "$ " + "0,00";

                    UltimosSaldosCaja();
                }
                else
                    MessageBox.Show("La caja ya se encuentra Cerrada.", "Estado de Caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                //else
                //    MessageBox.Show("Empresa y Pto. Vta. No Habilitado para operar.", "Estado de Caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { }
        }

        private decimal BuscaUltimoSaldo(int iNroUltimaCaja, string sNombreCaja)
        {
            return 0;
        }

        private void tsBtnRetiros_Click(object sender, EventArgs e)
        {
            try
            {
                gpbIngresoEfectivo.Visible = true;
                txtNroCajaIngresoEfectivo.Text = lvwCaja.SelectedItems[0].SubItems[0].Text;
            }
            catch { }
        }

        private void tsBtnGastos_Click(object sender, EventArgs e)
        {
            try
            {
                gpbGastosSueltos.Visible = true;
                txtNroCajaIngresoGasto.Text = lvwCaja.SelectedItems[0].SubItems[0].Text;
            }
            catch { }
        }

        private void tsBtnEgresos_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("EN CONSTRUCCIÓN. . .", "¿Qué ha pasado?", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            try
            {

                txtNroCajaConfig.Text = lvwCaja.SelectedItems[0].SubItems[0].Text;
                gbConfigEgreso.Visible = true;

            }
            catch { MessageBox.Show("No se ha seleccionado una caja", "Egresos", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }

        private void tsBtnReportes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("EN CONSTRUCCIÓN. . .", "¿Qué ha pasado?", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void lvwCaja_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTotalesEfectivo.Text = "0";
            lblTotalesGastos.Text = "0";
            lblTotalesBanco.Text = "0";

            TotalBanco=0;
            TotalEfectivo=0;
            TotalGasto=0;

            muestraMovimientoTesoreria();


        }

        private void muestraMovimientoTesoreria()
        {
            try
            {
                lvwMovimientoCaja.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT TesoreriaCaja.*, TesoreriaMovimiento.IdMov, TesoreriaMovimiento.Sucursal, TesoreriaMovimiento.IdTipoMov, TesoreriaMovimiento.TipoMov, TesoreriaMovimiento.FechaMov, TesoreriaMovimiento.IngresoMov, TesoreriaMovimiento.EgresoMov, TesoreriaMovimiento.Usuario as 'UsuarioTeso', TesoreriaMovimiento.NroCaja , TesoreriaMovimiento.IdEmpresa, TesoreriaMovimiento.Observacion, TesoreriaMovimiento.NroFacturaInterno, TesoreriaMovimiento.NroReciboInterno, TesoreriaMovimiento.NroFactu, TesoreriaMovimiento.NroRecibo FROM TesoreriaCaja, TesoreriaMovimiento WHERE TesoreriaCaja.NroCaja = TesoreriaMovimiento.NroCaja AND TesoreriaMovimiento.IDEMPRESA = " + IDEMPRESA + " AND TesoreriaCaja.IDEMPRESA = " + IDEMPRESA + " AND TesoreriaMovimiento.SUCURSAL = '" + sPtoVta + "' AND TesoreriaMovimiento.NroCaja = "+ lvwCaja.SelectedItems[0].SubItems[0].Text + " ORDER BY TesoreriaMovimiento.FechaMov", conectaEstado);
                
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwMovimientoCaja.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IdMov"].ToString());
                    item.SubItems.Add(dr["Sucursal"].ToString());
                    item.SubItems.Add(dr["IdTipoMov"].ToString());
                    item.SubItems.Add(dr["TipoMov"].ToString());
                    item.SubItems.Add(dr["FechaMov"].ToString());
                    item.SubItems.Add(dr["IngresoMov"].ToString());
                    item.SubItems.Add(dr["EgresoMov"].ToString());
                    item.SubItems.Add(dr["UsuarioTeso"].ToString());

                    item.SubItems.Add(dr["NroCaja"].ToString());
                    item.SubItems.Add(dr["IDEmpresa"].ToString());
                    item.SubItems.Add(dr["Observacion"].ToString());
                    item.SubItems.Add(dr["NROFACTURAINTERNO"].ToString());
                    item.SubItems.Add(dr["NroReciboInterno"].ToString());
                    item.SubItems.Add(dr["NroFactu"].ToString());
                    item.SubItems.Add(dr["NroRecibo"].ToString());

                    /*if (Convert.ToDateTime(item.SubItems[3].Text).AddDays(180) <= DateTime.Today)
                        item.ImageIndex = 2;
                    else
                        item.ImageIndex = 0;*/

                    if (dr["TipoMov"].ToString() == "EFECTIVO")
                    {
                        item.ImageIndex = 1;
                        TotalEfectivo = TotalEfectivo + Convert.ToDecimal(dr["IngresoMov"].ToString());
                        lblTotalesEfectivo.Text = Math.Round(TotalEfectivo,2).ToString();
                    }

                    else if (dr["TipoMov"].ToString() == "BANCO-CHEQUE" || dr["TipoMov"].ToString() == "BANCO-TRANSF" || dr["TipoMov"].ToString() == "BANCO-TARJETA" || dr["TipoMov"].ToString() == "BANCO-RET.GAN" || dr["TipoMov"].ToString() == "BANCO-RET.I.B" || dr["TipoMov"].ToString() == "BANCO-RET.IVA")
                    {

                        if (dr["TipoMov"].ToString() == "BANCO-CHEQUE" || dr["TipoMov"].ToString() == "BANCO-TRANSF" || dr["TipoMov"].ToString() == "BANCO-TARJETA")
                        {
                            item.ImageIndex = 0;
                            TotalBanco = TotalBanco + Convert.ToDecimal(dr["IngresoMov"].ToString());
                            lblTotalesBanco.Text = Math.Round(TotalBanco,2).ToString();
                        }
                        else if (dr["TipoMov"].ToString() == "BANCO-RET.GAN" || dr["TipoMov"].ToString() == "BANCO-RET.I.B" || dr["TipoMov"].ToString() == "BANCO-RET.IVA")
                        {
                            item.ImageIndex = 0;
                            TotalBanco = TotalBanco - Convert.ToDecimal(dr["EgresoMov"].ToString());
                            lblTotalesBanco.Text = Math.Round(TotalBanco,2).ToString();
                        }
                    }

                    else if (dr["TipoMov"].ToString() == "GASTO SUELTO")
                    {
                        item.ImageIndex = 4;
                        TotalGasto = TotalGasto + Convert.ToDecimal(dr["EgresoMov"].ToString());
                        lblTotalesGastos.Text = Math.Round(TotalGasto,2).ToString();
                    }
                    

                    item.UseItemStyleForSubItems = false;
                    lvwMovimientoCaja.Items.Add(item);
                    
                }

                decimal dTotalCaja;
                decimal dSaldoAp;
                decimal dEgreso;
                decimal dTotalCajaEfectivo;

                dSaldoAp = Convert.ToDecimal(lvwCaja.SelectedItems[0].SubItems[4].Text);                
                dEgreso = Convert.ToDecimal(lblTotalesGastos.Text);

                dTotalCaja = ((dSaldoAp) + ((TotalEfectivo + TotalBanco) - dEgreso));
                lblTotalCaja.Text = Math.Round(dTotalCaja,2).ToString();

                lblSaldoApertura.Text = Math.Round(Convert.ToDecimal(lblTotalCaja.Text),2).ToString();
                
                dTotalCajaEfectivo = ((dSaldoAp) + ((TotalEfectivo) - dEgreso));
                lblSaldoAperturaEfectivo.Text = Math.Round(dTotalCajaEfectivo,2).ToString();

                 cm.Connection.Close();

                lblSaldoCierre.Text = "$ " + Math.Round(Convert.ToDecimal(lvwCaja.SelectedItems[0].SubItems[6].Text),2);

                /*if (txtObservacionFactura.Text.Trim() == "Pago Contado")
                   btnCerrarFactura.Visible = false;*/
            }
            catch //(System.Exception excep)
            { lblSaldoCierre.Text = "-"; }

        }

        private void btnCerrarGastosSueltos_Click(object sender, EventArgs e)
        {
            gpbGastosSueltos.Visible = false;
        }

        private void btnAceptarGastoSuelto_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwCaja.SelectedItems[0].SubItems[2].Text == "Caja Abierta")
                {

                    gpbConfigCaja.Enabled = true;

                    string agregarCaja = "INSERT INTO TesoreriaMovimiento(Sucursal, IdtipoMov, TipoMov, FechaMov, EgresoMov, Usuario, NroCaja, IdEmpresa, Observacion) VALUES ('" + sPtoVta.Trim() + "', 8, 'GASTO SUELTO', '" + FormateoFecha() + "',  '" + txtImporteGasto.Text + "', '" + sUsuarioLegueado.Trim() + "', " + txtNroCajaIngresoGasto.Text.Trim() + ", " + IDEMPRESA + ", '" + txtDescripcionGastoSuelto.Text + "')";
                    connGeneric.InsertarGeneric(agregarCaja);

                    lblTotalesEfectivo.Text = "0";
                    lblTotalesGastos.Text = "0";
                    lblTotalesBanco.Text = "0";

                    TotalBanco = 0;
                    TotalEfectivo = 0;
                    TotalGasto = 0;

                    muestraMovimientoTesoreria();
                    gpbGastosSueltos.Visible = false;
                }
                else
                    MessageBox.Show("La caja ya se encuentra Cerrada.", "Estado de Caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { }
        }

        private void lvwMovimientoCaja_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void tsBtnObservacion_Click(object sender, EventArgs e)
        {
            gpbObservacionMov.Visible = true;
        }

        private void btnCerrarObservacion_Click(object sender, EventArgs e)
        {
            gpbObservacionMov.Visible = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwCaja.SelectedItems[0].SubItems[2].Text == "Caja Abierta")
                {
                    //Actualiza Observacion
                    string actualizar = "Observacion = '" + txtObservacionMov.Text + "'";
                    connGeneric.ActualizaGeneric("TesoreriaMovimiento", actualizar, "IdMov = " + Convert.ToInt32(txtIdMov.Text) + "");
                    gpbObservacionMov.Visible = false;

                    lblTotalesEfectivo.Text = "0";
                    lblTotalesGastos.Text = "0";
                    lblTotalesBanco.Text = "0";

                    TotalBanco = 0;
                    TotalEfectivo = 0;
                    TotalGasto = 0;

                    muestraMovimientoTesoreria();
                }
                else
                    MessageBox.Show("La caja ya se encuentra Cerrada.", "Estado de Caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { }
        }

        private void lvwMovimientoCaja_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtIdMov.Text = lvwMovimientoCaja.SelectedItems[0].SubItems[0].Text;
                txtObservacionMov.Text = lvwMovimientoCaja.SelectedItems[0].SubItems[10].Text;
            }
            catch { }
        }

        private void btnCerrarIngresoEfectivo_Click(object sender, EventArgs e)
        {
            gpbIngresoEfectivo.Visible = false;

        }

        private void btnAceptarIngresoEfectivo_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwCaja.SelectedItems[0].SubItems[2].Text == "Caja Abierta")
                {
                    gpbConfigCaja.Enabled = true;

                    string agregarCaja = "INSERT INTO TesoreriaMovimiento(Sucursal, IdtipoMov, TipoMov, FechaMov, IngresoMov, Usuario, NroCaja, IdEmpresa, Observacion) VALUES ('" + sPtoVta.Trim() + "', 2, 'EFECTIVO', '" + FormateoFecha() + "',  '" + txtImporteIngreso.Text + "', '" + sUsuarioLegueado.Trim() + "', " + txtNroCajaIngresoEfectivo.Text.Trim() + ", " + IDEMPRESA + ", '" + txtDescripcionIngreso.Text + "')";
                    connGeneric.InsertarGeneric(agregarCaja);

                    lblTotalesEfectivo.Text = "0";
                    lblTotalesGastos.Text = "0";
                    lblTotalesBanco.Text = "0";

                    TotalBanco = 0;
                    TotalEfectivo = 0;
                    TotalGasto = 0;

                    muestraMovimientoTesoreria();
                    gpbIngresoEfectivo.Visible = false;
                }
                else
                    MessageBox.Show("La caja ya se encuentra Cerrada.", "Estado de Caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwCaja.SelectedItems[0].SubItems[2].Text == "Caja Abierta")
                {
                    if (connGeneric.EliminarGeneric("TesoreriaMovimiento", " IdMov = " + Convert.ToInt32(txtIdMov.Text)))
                    {
                        lblTotalesEfectivo.Text = "0";
                        lblTotalesGastos.Text = "0";
                        lblTotalesBanco.Text = "0";

                        TotalBanco = 0;
                        TotalEfectivo = 0;
                        TotalGasto = 0;

                        gpbObservacionMov.Visible = false;
                        muestraMovimientoTesoreria();

                        MessageBox.Show("Datos Eliminados");
                    }
                    else
                        MessageBox.Show("Error al Eliminar");
                }
                else
                    MessageBox.Show("La caja ya se encuentra Cerrada.", "Estado de Caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { }
        }

        private void btnCerrarEstadistica_Click(object sender, EventArgs e)
        {
            gpbEstadisticaBanco.Visible = false;
        }

        private void btReporteBanco_Click(object sender, EventArgs e)
        {
            gpbEstadisticaBanco.Visible = true;

            try
            {
                //lvwMovimientoCaja.Items.Clear();

                decimal dTarjeta = 0;
                decimal dCheque = 0;
                decimal dTransf = 0;
                decimal dRetGan = 0;
                decimal dRetIva = 0;
                decimal dRetIB = 0;
                decimal dSubTotalSaldoBco = 0;

                SqlCommand cm = new SqlCommand("SELECT TesoreriaCaja.*, TesoreriaMovimiento.IdMov, TesoreriaMovimiento.Sucursal, TesoreriaMovimiento.IdTipoMov, TesoreriaMovimiento.TipoMov, TesoreriaMovimiento.FechaMov, TesoreriaMovimiento.IngresoMov, TesoreriaMovimiento.EgresoMov, TesoreriaMovimiento.Usuario as 'UsuarioTeso', TesoreriaMovimiento.NroCaja , TesoreriaMovimiento.IdEmpresa, TesoreriaMovimiento.Observacion, TesoreriaMovimiento.NroFacturaInterno, TesoreriaMovimiento.NroReciboInterno, TesoreriaMovimiento.NroFactu, TesoreriaMovimiento.NroRecibo FROM TesoreriaCaja, TesoreriaMovimiento WHERE TesoreriaCaja.NroCaja = TesoreriaMovimiento.NroCaja AND TesoreriaMovimiento.IDEMPRESA = " + IDEMPRESA + " AND TesoreriaCaja.IDEMPRESA = " + IDEMPRESA + " AND TesoreriaMovimiento.SUCURSAL = '" + sPtoVta + "' AND TesoreriaMovimiento.NroCaja = " + lvwCaja.SelectedItems[0].SubItems[0].Text + " AND TipoMov like '%BANCO%' ORDER BY TesoreriaMovimiento.FechaMov", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["TipoMov"].ToString() == "BANCO-TRANSF")
                    {
                        dTransf = dTransf + Convert.ToDecimal(dr["IngresoMov"].ToString());
                    }

                    else if (dr["TipoMov"].ToString() == "BANCO-CHEQUE")
                    {
                        dCheque = dCheque + Convert.ToDecimal(dr["IngresoMov"].ToString());
                    }

                    else if (dr["TipoMov"].ToString() == "BANCO-TARJETA")
                    {
                        dTarjeta = dTarjeta + Convert.ToDecimal(dr["IngresoMov"].ToString());
                    }


                    ///////////////////
                    else if (dr["TipoMov"].ToString() == "BANCO-RET.GAN")
                    {
                        dRetGan = dRetGan + Convert.ToDecimal(dr["EgresoMov"].ToString());
                    }

                    else if (dr["TipoMov"].ToString() == "BANCO-RET.I.B")
                    {
                        dRetIB = dRetIB + Convert.ToDecimal(dr["EgresoMov"].ToString());
                    }

                    else if (dr["TipoMov"].ToString() == "BANCO-RET.IVA")
                    {
                        dRetIva = dRetIva + Convert.ToDecimal(dr["EgresoMov"].ToString());
                    }
                    ///////////////////

                    lblTarjeta.Text = Math.Round(dTarjeta,2).ToString();
                    lblCheque.Text = Math.Round(dCheque,2).ToString();
                    lblTransferencia.Text = Math.Round(dTransf,2).ToString();

                    lblRetGanancia.Text = Math.Round(dRetGan,2).ToString();
                    lblRetIVA.Text = Math.Round(dRetIva,2).ToString();
                    lblRetIB.Text = Math.Round(dRetIB,2).ToString();
                }

                dSubTotalSaldoBco = ((dTarjeta + dCheque + dTransf)) - ((dRetGan + dRetIva + dRetIB));
                lblSubTotalSaldoBanco.Text = Math.Round(dSubTotalSaldoBco,2).ToString();
                
                /*decimal dTotalCaj;
                decimal dSaldoAp;
                decimal dEgreso;

                dSaldoAp = Convert.ToDecimal(lvwCaja.SelectedItems[0].SubItems[4].Text);
                dEgreso = Convert.ToDecimal(lblTotalesGastos.Text);

                dTotalCaja = ((dSaldoAp) + ((TotalEfectivo + TotalBanco) - dEgreso));
                lblTotalCaja.Text = dTotalCaja.ToString();*/

                cm.Connection.Close();


                /*if (txtObservacionFactura.Text.Trim() == "Pago Contado")
                   btnCerrarFactura.Visible = false;*/
            }
            catch //(System.Exception excep)
            { }

        }

        private void btnCerrarConfigEgreso_Click(object sender, EventArgs e)
        {
            gbConfigEgreso.Visible = false;
        }

        private void optBanco_CheckedChanged(object sender, EventArgs e)
        {
            try
            {                
                cmbCaja.Enabled = false;
            }
            catch //(System.Exception excep)
            { }
        }

        private void optCaja_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cmbCaja.Enabled = true;    
                int i = 0;
                int j = 0;
                int z = 0;
                int x = 0;
                string[] sNombreCaja = new string[100000];
                                
                cmbCaja.Items.Clear();                

                SqlCommand cm = new SqlCommand("SELECT * FROM TesoreriaCaja", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                        this.cmbCaja.Items.Add(dr["NOMBRECAJA"].ToString());
                        sNombreCaja[i] = dr["NOMBRECAJA"].ToString();
                        i = i + 1;
                }

                for (j = 0; j <= i; j++)
                {
                    for (z = 1; z <= i; z++)
                    {
                        if (sNombreCaja[j] == sNombreCaja[z])
                            this.cmbCaja.Items.Remove(sNombreCaja[z]);
                    }

                    for (x = 0; x <= i; x++)
                        sNombreCaja[x] = "";                            

                    i = this.cmbCaja.Items.Count;
                    cmbCaja.Items.CopyTo(sNombreCaja, 0);
                }
            }
            catch //(System.Exception excep)
            { }
        }

        private void btnAceptarConfigEgreso_Click(object sender, EventArgs e)
        {

            string CajaOrigen;
            string CajaDestino;
            int iNroCaja;
            string sPtoVenta;

            if (cmbCaja.Text.Trim() == lvwCaja.SelectedItems[0].SubItems[1].Text)
                MessageBox.Show("La caja origen y destino son las mismas.", "Transferencia entre Cajas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                CajaOrigen = lvwCaja.SelectedItems[0].SubItems[1].Text.Trim();
                CajaDestino = cmbCaja.Text;

                iNroCaja = RecuperaUltimaCaja(CajaDestino);
                sPtoVenta = RecuperaPtoVta(iNroCaja);

                string agregarCaja = "INSERT INTO TesoreriaMovimiento(Sucursal, IdtipoMov, TipoMov, FechaMov, IngresoMov, Usuario, NroCaja, IdEmpresa, Observacion) VALUES ('" + sPtoVenta.Trim() + "', 2, 'EFECTIVO', '" + FormateoFecha() + "',  '" + txtImporteIngresoCajaDestino.Text + "', '" + sUsuarioLegueado.Trim() + "', " + iNroCaja + ", " + IDEMPRESA + ", '" + txtDescripcionPaseEntreCaja.Text + "')";
                connGeneric.InsertarGeneric(agregarCaja);

                //Agreso de la caja origen por pase a caja destinio
                string sPasaCaja = "INSERT INTO TesoreriaMovimiento(Sucursal, IdtipoMov, TipoMov, FechaMov, EgresoMov, Usuario, NroCaja, IdEmpresa, Observacion) VALUES ('" + sPtoVta.Trim() + "', 8, 'GASTO SUELTO', '" + FormateoFecha() + "',  '" + txtImporteIngresoCajaDestino.Text + "', '" + sUsuarioLegueado.Trim() + "', " + lvwCaja.SelectedItems[0].SubItems[0].Text + ", " + IDEMPRESA + ", '" + txtDescripcionPaseEntreCaja.Text + "')";
                connGeneric.InsertarGeneric(sPasaCaja);

                MessageBox.Show("Pase entre caja realizado correctamente.", "Pase entre caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int RecuperaUltimaCaja(string sCajaDestino)
        {
            int iUltimoIdCaja;
           
            connGeneric.LeeGeneric("SELECT MAX(NroCaja) as NroUltimaCajaAbierta FROM TesoreriaCaja WHERE IDEMPRESA = " + IDEMPRESA + " AND NombreCaja = '" + sCajaDestino + "' AND IdEstadoCaja=20", "TesoreriaCaja");
            iUltimoIdCaja = Convert.ToInt32(connGeneric.leerGeneric["NroUltimaCajaAbierta"].ToString());

            return iUltimoIdCaja;
        }

        private string RecuperaPtoVta(int idCaja)
        {
            string PtoVta;

            connGeneric.LeeGeneric("SELECT sucursal FROM TesoreriaCaja WHERE nrocaja="+ idCaja + "", "TesoreriaCaja");
            PtoVta = connGeneric.leerGeneric["sucursal"].ToString();

            return PtoVta.Trim();
        }

    }
}
