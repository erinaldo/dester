using DGestion.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime;
using System.Runtime.InteropServices;
using System.Reflection;

namespace DGestion
{
    public partial class frmPrincipal : Form
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReserverdValue);

        public static string GrupoUsuario;
        public static string Usuario;
        public static string PtoVenta;
        public static string Empresa;

        CGenericBD conn = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G##");

        string sVersion;

        public frmPrincipal()
        {
            InitializeComponent();
        }        

        private void mnuCerrar_Click(object sender, EventArgs e) {
            this.Hide();
            frmLogIn sesion = new frmLogIn();
            sesion.ShowDialog();
        }

        private void mnuSalir_Click(object sender, EventArgs e) {
            Application.Exit();    
        }

        private void mnuVentaCliente_Click(object sender, EventArgs e)  {
            frmCliente childFrom = new frmCliente();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void tipoDeIVAToolStripMenuItem_Click(object sender, EventArgs e) {
            frmTipoIva childFrom = new frmTipoIva();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void VerificaUsuarioEnLinea()
        {
            try
            {
                tsUsuarioEnLinea.DropDownItems.Clear();
                SqlCommand cm = new SqlCommand("SELECT * FROM Personal WHERE ISLOGIN = 'True'", conectaEstado);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    //if (dr["ISLOGIN"].ToString() == "True" && dr["USUARIO"].ToString() == tsUsuarioEnLinea.DropDownItems.Find())
                    tsUsuarioEnLinea.DropDownItems.Add(dr["NOMBREYAPELLIDO"].ToString());
                }
                conn.DesconectarBDLeeGeneric();
            }
            catch { conn.DesconectarBDLeeGeneric(); }
        }

        private void Principal_Load(object sender, EventArgs e) {
            try {
                    lblStatusUser.Text = frmLogIn.usuarioLogeado;
                    conn.LeeGeneric("SELECT * FROM Personal WHERE usuario='" + lblStatusUser.Text + "'", "Personal");

                    if (conn.leerGeneric["IDTIPOPERSONAL"].ToString() == "1")
                        lblTipoUsuairo.Text = "Director";
                    else if (conn.leerGeneric["IDTIPOPERSONAL"].ToString() == "2")
                        lblTipoUsuairo.Text = "Sistema";
                    else if (conn.leerGeneric["IDTIPOPERSONAL"].ToString() == "3")
                        lblTipoUsuairo.Text = "Administrador";
                    else if (conn.leerGeneric["IDTIPOPERSONAL"].ToString() == "4")
                        lblTipoUsuairo.Text = "Operario";
                    else if (conn.leerGeneric["IDTIPOPERSONAL"].ToString() == "5")
                        lblTipoUsuairo.Text = "Vendedor";
                    else
                        lblTipoUsuairo.Text = "Sin Grupo";

                    conn.DesconectarBDLeeGeneric();

                    timer1.Enabled = true; timer1.Interval = 25;
                    GrupoUsuario = StatusBar.Items[9].Text;
                    Usuario = StatusBar.Items[5].Text;

                if (Usuario == "dir" || Usuario == "DIR"  || Usuario == "mdg" ||  Usuario == "MDG")
                {
                    toolBar.Items[29].Enabled = true;
                    tsPermisoDelSistema.Enabled = true;
                }
                else
                {
                    toolBar.Items[29].Enabled = false;
                    tsPermisoDelSistema.Enabled = false;
                }

                VerificaUsuarioEnLinea();
                
                // Detecta nueva versiones //
                if (VerificaVersionSistema() == true)
                    MessageBox.Show("Existe una nueva versión del sistema " + sVersion + ", version actual " + StatusBar.Items[19].Text.Trim() + "", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Empresas();

            }
            catch { conn.DesconectarBDLeeGeneric(); }
        }

        public void Empresas()
        {
            frmEmpresa NombreEmpresa = new frmEmpresa();
            NombreEmpresa.NEmpresas += new frmEmpresa.pasarEmpresas(NombreEmpresas);
            NombreEmpresa.PtoVenta += new frmEmpresa.pasarPtoVtaEmpresa(ptovta);
            NombreEmpresa.ShowDialog();
        }

        public void NombreEmpresas(string Nombre)
        {
            StatusBar.Items[15].Text = "Empresa: " + Nombre;
            Empresa = Nombre;            
        }

        public void ptovta(string PtoVtaNombre)
        {
            StatusBar.Items[17].Text = " Pto. Vta.: "+ PtoVtaNombre;
            PtoVenta = PtoVtaNombre;
        }

        private bool VerificaVersionSistema() {
            try
            {
                conn.LeeGeneric("SELECT VERSIONSISTEMA FROM Parametros WHERE IDPARAMETROS = 1", "Parametros");
                if (conn.leerGeneric["VERSIONSISTEMA"].ToString().Trim() == StatusBar.Items[19].Text.Trim())
                    return false;
                else {
                    sVersion = conn.leerGeneric["VERSIONSISTEMA"].ToString().Trim();
                    return true;
                }
            }
            catch { return false; }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            StatusBar.Items[27].Text = DateTime.Now.ToShortDateString(); //+ " - " + DateTime.Now.ToLongTimeString();
            EstadoRed();            
        }

        public void EstadoRed() {
            try {
                ///Detector de Red
                int Desc;
                if (InternetGetConnectedState(out Desc, 0).ToString() == "True")
                    StatusBar.Items[12].Image = imageList1.Images[1];
                else
                    StatusBar.Items[12].Image = imageList1.Images[0];
            }
            catch { } 
        }

        private void mnuAdminEspecialidad_Click(object sender, EventArgs e) {
            frmEspecialidad childFrom = new frmEspecialidad();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuCompraProveedores_Click(object sender, EventArgs e) {
            frmProveedores childFrom = new frmProveedores();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void manuAdminTipoPersona_Click(object sender, EventArgs e)  {
            frmTipoPersonal childFrom = new frmTipoPersonal();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuAdminEstadoCivil_Click(object sender, EventArgs e) {
            frmEstadoCivil childFrom = new frmEstadoCivil();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void impuestosToolStripMenuItem_Click(object sender, EventArgs e)  {
            frmImpuestos childFrom = new frmImpuestos();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuStockFamilia_Click(object sender, EventArgs e) {
            frmFamiliaArticulo childFrom = new frmFamiliaArticulo();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuStockRubro_Click(object sender, EventArgs e)  {
            frmRubroArticulo childFrom = new frmRubroArticulo();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuStockMarca_Click(object sender, EventArgs e)  {
            frmMarcaArticulo childFrom = new frmMarcaArticulo();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuStockTipoArticulo_Click(object sender, EventArgs e) {
            frmTipoIBArticulo childFrom = new frmTipoIBArticulo();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuStockTipoProducto_Click(object sender, EventArgs e) {
            frmTipoIVAProducto childFrom = new frmTipoIVAProducto();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuGestionArticulos_Click(object sender, EventArgs e) {
            frmArticulo childFrom = new frmArticulo();
            childFrom.MdiParent = this;
            childFrom.Show();

        }

        private void mnuAyudaVersion_Click(object sender, EventArgs e) {
            frmVersion childFrom = new frmVersion();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e) {
            Conexion conn = new Conexion();
            conn.DesconectarBD();
            Application.Exit();
        }

        private void tsGestionArticulos_Click(object sender, EventArgs e) {
            frmArticulo childFrom = new frmArticulo();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void tsBtnVersion_Click(object sender, EventArgs e) {
            frmVersion childFrom = new frmVersion();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void tsBtnCerrarSesion_Click(object sender, EventArgs e) {

            //Auditoria
            AuditoriaSistema AS1 = new AuditoriaSistema();
            AS1.GACierreLogin_0004("Cerró Sesión", StatusBar.Items[5].Text);
            ///////////////////////////////////////////////////////
            
            this.Hide();
            frmLogIn sesion = new frmLogIn();
            sesion.ShowDialog();           
        }

        private void tsBtnProveedor_Click(object sender, EventArgs e) {
            frmProveedores childFrom = new frmProveedores();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void tsBtnListaCambio_Click(object sender, EventArgs e) {
            frmChangeLog childFrom = new frmChangeLog();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuListaDePreciosDeVenta_Click(object sender, EventArgs e) {
            frmListaPrecioVenta childFrom = new frmListaPrecioVenta();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuStockArticuloPrecoVenta_Click(object sender, EventArgs e) {
            frmArtPrecioVenta childFrom = new frmArtPrecioVenta();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuAyudaChangeLog_Click(object sender, EventArgs e) {
            frmChangeLog childFrom = new frmChangeLog();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void tsBtnGestionDelPersonal_Click(object sender, EventArgs e) {
            frmPersonal childFrom = new frmPersonal();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void tsBtnClientes_Click(object sender, EventArgs e) {
            frmCliente childFrom = new frmCliente();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuAdminConfiguracion_Click(object sender, EventArgs e) {
            frmAdminConfig childFrom = new frmAdminConfig();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuEstadoExistenciaArticulo_Click(object sender, EventArgs e) {
            frmArticuloEstadoStock childFrom = new frmArticuloEstadoStock();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuCompraFactu_Click(object sender, EventArgs e) {
            frmProveedCompra childFrom = new frmProveedCompra();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void tsBtnProveedCompras_Click(object sender, EventArgs e) {
            frmProveedCompra childFrom = new frmProveedCompra();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuAdminPersona_Click(object sender, EventArgs e)
        {
            frmPersonal childPersonal = new frmPersonal();
            childPersonal.MdiParent = this;
            childPersonal.Show();
        }

        private void formaDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFormaPago childFormaPago = new frmFormaPago();
            childFormaPago.MdiParent = this;
            childFormaPago.Show();
        }

        private void mnuAdminVisorSuceso_Click(object sender, EventArgs e)
        {
            frmVisorSuceso childVisor = new frmVisorSuceso();
            childVisor.MdiParent = this;
            childVisor.Show();
        }

        private void mnuAdminGrupoUsuario_Click(object sender, EventArgs e)
        {
            frmUsuarioGrupo childUsuarioGrupo = new frmUsuarioGrupo();
            childUsuarioGrupo.MdiParent = this;
            childUsuarioGrupo.Show();
        }

        private void tsBtnCerrar_Click(object sender, EventArgs e) {
            try {
                //Auditoria
                AuditoriaSistema AS1 = new AuditoriaSistema();
                AS1.GACierreLogin_0004("Cerró Sesión", StatusBar.Items[5].Text);
                ///////////////////////////////////////////////////////
                Application.Exit();
            }
            catch { }
        }

        private void mnuVentaFacturacion_Click(object sender, EventArgs e)
        {
            frmFacturacion childFacturacion = new frmFacturacion();
            childFacturacion.MdiParent = this;
            childFacturacion.Show();
        }

        private void mnuAdminVendedor_Click(object sender, EventArgs e)
        {
            frmVendedor childVendedor = new frmVendedor();
            childVendedor.MdiParent = this;
            childVendedor.Show();
        }

        private void mnuVentaRemito_Click(object sender, EventArgs e)
        {
            frmRemito childRemito = new frmRemito();
            childRemito.MdiParent = this;
            childRemito.Show();
        }

        private void mnuVentaTipoDePago_Click(object sender, EventArgs e)
        {
            frmTipoPago childTipoPago = new frmTipoPago();
            childTipoPago.MdiParent = this;
            childTipoPago.Show();
        }

        private void mnuVentaSubtipoDePago_Click(object sender, EventArgs e)
        {
            frmSubTipoPago childSubTipoPago = new frmSubTipoPago();
            childSubTipoPago.MdiParent = this;
            childSubTipoPago.Show();
        }

        private void mnuCompraListaDePreciosProveed_Click(object sender, EventArgs e)
        {
            frmListaPrecioCompra childListaDeCompras = new frmListaPrecioCompra();
            childListaDeCompras.MdiParent = this;
            childListaDeCompras.Show();
        }

        private void pedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNotaPedido childPedidoNota = new frmNotaPedido();
            childPedidoNota.MdiParent = this;
            childPedidoNota.Show();
        }

        private void soporteTécnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSoporteTecnico childSoporteTecnico = new frmSoporteTecnico();
            childSoporteTecnico.MdiParent = this;
            childSoporteTecnico.Show();
        }

        private void tsSoporteTecnico_Click(object sender, EventArgs e)
        {
            frmSoporteTecnico childSoporteTecnico = new frmSoporteTecnico();
            childSoporteTecnico.MdiParent = this;
            childSoporteTecnico.Show();
        }

        private void tsNotaPedido_Click(object sender, EventArgs e)
        {
            frmNotaPedido childNotaPedido = new frmNotaPedido();
            childNotaPedido.MdiParent = this;
            childNotaPedido.Show();
        }

        private void tsRemito_Click(object sender, EventArgs e)
        {
            frmRemito chieldRemito = new frmRemito();
            chieldRemito.MdiParent = this;
            chieldRemito.Show();
        }

        private void tsFactura_Click(object sender, EventArgs e)
        {
            frmFacturacion chieldFacturacion = new frmFacturacion();
            chieldFacturacion.MdiParent = this;
            chieldFacturacion.Show();
        }

        private void tsRecibos_Click(object sender, EventArgs e)
        {
            frmRecibo chieldRecibo = new frmRecibo();
            chieldRecibo.MdiParent = this;
            chieldRecibo.Show();
        }

        private void tsNotaCredito_Click(object sender, EventArgs e)
        {
            frmNotaDeCredito childFrom = new frmNotaDeCredito();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            VerificaUsuarioEnLinea();
        }

        private void tsGrupos_Click(object sender, EventArgs e)
        {
            frmUsuarioGrupo chieldGrupo = new frmUsuarioGrupo();
            chieldGrupo.MdiParent = this;
            chieldGrupo.Show();
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //Auditoria
                AuditoriaSistema AS1 = new AuditoriaSistema();
                AS1.GACierreLogin_0004("Cerró Sesión", StatusBar.Items[5].Text);
                ///////////////////////////////////////////////////////
                Application.Exit();
            }
            catch { }
        }

        private void toolStripStatusLabel12_Click(object sender, EventArgs e)
        {
            /*frmSaldo chieldSaldo = new frmSaldo();
            chieldSaldo.MdiParent = this;
            chieldSaldo.Show();*/
        }

        private void mnuVentaNotaDeCredito_Click(object sender, EventArgs e)
        {
            frmNotaDeCredito chielNotaDeCredito = new frmNotaDeCredito();
            chielNotaDeCredito.MdiParent = this;
            chielNotaDeCredito.Show();
        }

        private void mnuVentaNotaDeDebito_Click(object sender, EventArgs e)
        {
            frmNotaDeDebito chieldNotaDebito = new frmNotaDeDebito();
            chieldNotaDebito.MdiParent = this;
            chieldNotaDebito.Show();
        }

        private void tsNotaDebito_Click(object sender, EventArgs e)
        {
            frmNotaDeDebito childFrom = new frmNotaDeDebito();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuVentaRecibos_Click(object sender, EventArgs e)
        {
            frmRecibo chieldRecibo = new frmRecibo();
            chieldRecibo.MdiParent = this;
            chieldRecibo.Show();
        }

        private void mnuAdminMonitorRede_Click(object sender, EventArgs e)
        {
            frmMonitorRed chieldMonitor = new frmMonitorRed();
            chieldMonitor.MdiParent = this;
            chieldMonitor.Show();
        }

        private void tsGestionEmpresa_Click(object sender, EventArgs e)
        {
            frmEmpresa chieldEmpresa = new frmEmpresa();
            chieldEmpresa.MdiParent = this;
            chieldEmpresa.Show();
        }

        private void tsListaPrecioVenta_Click(object sender, EventArgs e)
        {
            frmArtPrecioVenta childFrom = new frmArtPrecioVenta();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void mnuHabilitarCentro_Click(object sender, EventArgs e)
        {
            frmMultiCentro childFrom = new frmMultiCentro();
            childFrom.MdiParent = this;
            childFrom.Show();

        }

        private void tsEmpresa_Click(object sender, EventArgs e)
        {            
            Empresas();
        }

        private void tsPermisoDelSistema_Click(object sender, EventArgs e)
        {
            frmPermisos childFrom = new frmPermisos();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void tsBtnPermisos_Click(object sender, EventArgs e)
        {
            frmPermisos childFrom = new frmPermisos();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void tsBtnTesoreria_Click(object sender, EventArgs e)
        {
            frmTesoreria childFrom = new frmTesoreria();
            childFrom.MdiParent = this;
            childFrom.Show();
        }

        private void tsEstadisticas_Click(object sender, EventArgs e)
        {

        }
    }
}
