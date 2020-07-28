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
    public partial class frmCliente : Form
    {
        public delegate void pasarClienteCod1(int CodCliente);
        public event pasarClienteCod1 pasarClienteCod;
        public delegate void pasarClienteRS(string RSCliente);
        public event pasarClienteRS pasarClientRS;

        EmpresaBD connEmpresa = new EmpresaBD();

        int contadorCODclienteNuevo;

        int IDEMPRESA;
        string sPtoVta;

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

        public frmCliente() {
            InitializeComponent();
        }

        CGenericBD conn = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private string FormateoFecha()
        {
            DateTimePicker dtr = new DateTimePicker();
            dtr.Value = txtUltimaFactu.Value;
            return String.Format("{0:d/M/yyyy HH:mm:ss}", dtr.Value);
        }

        private string FormateoFecha2(string dateSelect)
        {
            DateTimePicker dtr = new DateTimePicker();
            //dtr.Value = this.dtFechaInicial.Value;
            return String.Format("{0:d/M/yyyy HH:mm:ss}", Convert.ToDateTime(dateSelect));
        }

        private void frmCliente_Load(object sender, EventArgs e) {
            // TODO: esta línea de código carga datos en la tabla 'DGestionDTGeneral.CtaCteClienteDeuda_Mostrador' Puede moverla o quitarla según sea necesario.
            this.CtaCteClienteDeuda_MostradorTableAdapter.Fill(this.DGestionDTGeneral.CtaCteClienteDeuda_Mostrador);

            // TODO: esta línea de código carga datos en la tabla 'DGestionDTGeneral.CtaCteClienteDeuda' Puede moverla o quitarla según sea necesario.
            //this.ctaCteClienteDeudaTableAdapter.Fill(this.DGestionDTGeneral.CtaCteClienteDeuda);
            // TODO: esta línea de código carga datos en la tabla 'DGestionDTGeneral.CtaCteCliente' Puede moverla o quitarla según sea necesario.
            //this.ctaCteClienteTableAdapter.Fill(this.DGestionDTGeneral.CtaCteCliente);
            // TODO: esta línea de código carga datos en la tabla 'DGestionDTGeneral.Cliente' Puede moverla o quitarla según sea necesario.
            //this.ClienteTableAdapter.Fill(this.DGestionDTGeneral.Cliente);

            gpoCliente.Visible = false;
            lvwCliente.Height = 575;

            conn.ConectarBD();
            IDEMPRESA = ConsultaEmpresa(); //Lee Empresa
            
            FormatoListView();

            cboBuscaCliente.SelectedIndex = 0;

            MostrarDatos();

            //this.rptClienteCtaCte.RefreshReport();            
            //this.rptClienteTodaCtaCte.RefreshReport();      

        }
        
        private void FormatoListView() {
            lvwCliente.View = View.Details;
            lvwCliente.LabelEdit = false;
            lvwCliente.AllowColumnReorder = false;
            lvwCliente.FullRowSelect = true;
            lvwCliente.GridLines = true;

            lvwCtaCte.View = View.Details;
            lvwCtaCte.LabelEdit = false;
            lvwCtaCte.AllowColumnReorder = false;
            lvwCtaCte.FullRowSelect = true;
            lvwCtaCte.GridLines = true;
        }

        public void MostrarDatos() {
            try {
                this.lvwCliente.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM Cliente WHERE IDEMPRESA = " + IDEMPRESA + " AND NROCENTRO = '"+ sPtoVta +"' ORDER BY RAZONSOCIAL", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows) {
                    lvwCliente.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IDCLIENTE"].ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString());
                    item.SubItems.Add(dr["LOCALIDAD"].ToString());
                    item.SubItems.Add(dr["TELEFONOSCOMERCIALES"].ToString());
                    item.SubItems.Add(dr["NUMDECUIT"].ToString());
                    item.SubItems.Add(dr["NROCENTRO"].ToString());

                    item.ImageIndex = 0;

                    lvwCliente.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }


        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            NuevoCliente();
        }

        private void NuevoCliente()
        {
            gpoCliente.Visible = true;
            lvwCliente.Height = 275;

            txtDomicilioComercial.Text = "";
            txtRazonSocial.Text = "";
            txtLocalidad.Text = "";
            txtTelefonosCom.Text = "";
            txtDomicilioPartic.Text = "";
            txtCodTipoIva.Text = "";
            cboTipoIva.Text = "";
            txtNroCuit.Text = "";
            txtCodEspecialidad.Text = "";
            cboEspecialidad.Text = "";
            txtCodPersonal.Text = "";
            cboPersonal.Text = "";
            txtCodFormaPago.Text = "";
            cboFormaPago.Text = "";
            txtCodListaPrecio.Text = "";
            cboListaPrecio.Text = "";
            txtContacto.Text = "";
            txtUltimaFactu.Text = "";
            txtDiasFactu.Text = "";
            txtObservacion.Text = "";

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;

            cboNroSucursal.Text = frmPrincipal.PtoVenta.Trim();

            /////////////////////////////////////////// AUTONUMERICO NRO INTERNO //////////////////////////////////////////
            conn.LeeGeneric("SELECT MAX(IDCLIENTE) as CODCLIENTE FROM CLIENTE", "CLIENTE");

            if (conn.leerGeneric["CODCLIENTE"].ToString() == "")
                txtCodCliente.Text = "0";
            else
                txtCodCliente.Text = conn.leerGeneric["CODCLIENTE"].ToString();

            contadorCODclienteNuevo = (Convert.ToInt32(txtCodCliente.Text));
            contadorCODclienteNuevo = contadorCODclienteNuevo + 1;
            txtCodCliente.Text = contadorCODclienteNuevo.ToString();
        }


        private void tsBtnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            lvwCliente.Height = 275;

            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = false;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
        }

        private void btcCerrar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            lvwCliente.Height = 575;

            tsBtnNuevo.Enabled = true;
            tsBtnModificar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void btnTipoIva_Click(object sender, EventArgs e) {
            frmTipoIva formTipoIva = new frmTipoIva();

            formTipoIva.pasarTipoIvaCod1 += new frmTipoIva.pasarTipoIvaCod(CodTIVA);  //Delegado1 
            formTipoIva.pasarTipoIvaDesc1 += new frmTipoIva.pasarTipoIvaDesc(TipoIvaDesc); //Delegado2


            txtNroCuit.Focus();
            formTipoIva.ShowDialog();
        }

        public void CodTIVA(int dato1)
        {
            this.txtCodTipoIva.Text = dato1.ToString();
        }

        public void TipoIvaDesc(string dato2)
        {
            this.cboTipoIva.Text = dato2.ToString();
        }

        private void btnEspecialidad_Click(object sender, EventArgs e) {
            frmEspecialidad formEspecialidad = new frmEspecialidad();
            
            txtCodPersonal.Focus();
            formEspecialidad.ShowDialog(); 
        }

        private void btnPersonal_Click(object sender, EventArgs e) {
            frmPersonal formPersonal = new frmPersonal();

            formPersonal.pasadoPerso1 += new frmPersonal.pasarPersona1(Perso1);  //Delegado1 
            formPersonal.pasadoPerso2 += new frmPersonal.pasarPersona2(Perso2); //Delegado2

            txtCodFormaPago.Focus();
            formPersonal.ShowDialog();   
        }

        public void Perso1(int dato1)
        {
            this.txtCodPersonal.Text = dato1.ToString();
        }

        public void Perso2(string dato2)
        {
            this.cboPersonal.Text = dato2.ToString();
        }

        private void btnFormaPago_Click(object sender, EventArgs e) {
            frmFormaPago formFormaPago = new frmFormaPago();

            formFormaPago.pasarFPCod += new frmFormaPago.pasarFormaPagoCod1(CodFP);  //Delegado1 
            formFormaPago.pasarFPN += new frmFormaPago.pasarFormaPagoRS(FPRS); //Delegado2

            txtCodListaPrecio.Focus();
            formFormaPago.ShowDialog();
        }

        public void CodFP(int dato1)
        {
            this.txtCodFormaPago.Text = dato1.ToString();
        }

        public void FPRS(string dato2)
        {
            this.cboFormaPago.Text = dato2.ToString();
        }

        private void btnListaPrecio_Click(object sender, EventArgs e) {
            frmListaPrecioVenta formListaPrecio = new frmListaPrecioVenta();

            formListaPrecio.pasarListaVendeCod1 += new frmListaPrecioVenta.pasarListaVendeCod(CodVende);  //Delegado1 
            formListaPrecio.pasarListaVendeDesc1 += new frmListaPrecioVenta.pasarListaVendeDes(NombreVende); //Delegado2

            txtDomicilioPartic.Focus();
            formListaPrecio.ShowDialog();
        }

        public void CodVende(int dato1)
        {
            this.txtCodListaPrecio.Text = dato1.ToString();
        }

        public void NombreVende(string dato2)
        {
            this.cboListaPrecio.Text = dato2.ToString();
        }


        private void btnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            try
            {
                gpoCliente.Visible = true;
                lvwCliente.Height = 275;

                tsBtnModificar.Enabled = false;
                tsBtnNuevo.Enabled = true;
                btnModificar.Enabled = false;
                btnEliminar.Enabled = true;
                btnGuardar.Enabled = true;
                
                if (txtRazonSocial.Text.Trim() != "")
                {
                    string agregar = "INSERT INTO Cliente VALUES('" + txtRazonSocial.Text + "', " + txtNroCuit.Text + ", '" + txtDomicilioComercial.Text + "', '" + txtLocalidad.Text + "', '" + txtTelefonosCom.Text + "','" + txtDomicilioPartic.Text + "', '" + txtContacto.Text + "', " + txtCodTipoIva.Text + ", " + txtCodEspecialidad.Text + ", " + txtCodPersonal.Text + ", " + txtCodFormaPago.Text + ", " + txtCodListaPrecio.Text + ", " + txtDiasFactu.Text + ", '" + FormateoFecha() + "', '" + txtObservacion.Text + "', " + IDEMPRESA + ", '"+ sPtoVta.Trim() +"')";

                    if (conn.InsertarGeneric(agregar))
                    {
                        MostrarDatos();
                        MessageBox.Show("Datos Agregados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("El campo descripcion esta vacio", "Adventencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                conn.DesconectarBD();
            }                
            catch { conn.DesconectarBD();}
        }

        private void lvwCliente_SelectedIndexChanged(object sender, EventArgs e) {
            try {

                conn.LeeGeneric("SELECT * FROM Cliente WHERE IDEMPRESA = " + IDEMPRESA + " AND NROCENTRO = "+ sPtoVta +" AND IdCliente = " + Convert.ToInt32(lvwCliente.SelectedItems[0].SubItems[0].Text) + "", "Cliente");

                this.txtCodCliente.Text = conn.leerGeneric["IDCLIENTE"].ToString();
                this.txtRazonSocial.Text = conn.leerGeneric["RAZONSOCIAL"].ToString();
                this.txtDomicilioComercial.Text = conn.leerGeneric["DOMICILIOCOMERCIAL"].ToString();
                this.txtLocalidad.Text = conn.leerGeneric["LOCALIDAD"].ToString();
                this.txtTelefonosCom.Text = conn.leerGeneric["TELEFONOSCOMERCIALES"].ToString();
                this.txtDomicilioPartic.Text = conn.leerGeneric["DOMICILIOPARTICULAR"].ToString();

                this.txtDomicilioPartic.Text = conn.leerGeneric["DOMICILIOPARTICULAR"].ToString();
                this.txtCodTipoIva.Text = conn.leerGeneric["IDTIPOIVA"].ToString();
                this.txtNroCuit.Text = conn.leerGeneric["NUMDECUIT"].ToString();
                this.txtCodEspecialidad.Text = conn.leerGeneric["IDESPECIALIDAD"].ToString();
                this.txtCodPersonal.Text = conn.leerGeneric["IDPERSONAL"].ToString();
                this.txtContacto.Text = conn.leerGeneric["CONTACTO"].ToString();
                this.txtCodFormaPago.Text = conn.leerGeneric["IDFORMADEPAGO"].ToString();
                this.txtCodListaPrecio.Text = conn.leerGeneric["IDLISTAPRECIO"].ToString();

                this.txtDiasFactu.Text = conn.leerGeneric["DIASENTREFACTURACION"].ToString();
                this.txtUltimaFactu.Text = conn.leerGeneric["ULTIMAFACTURACION"].ToString();
                this.txtObservacion.Text = conn.leerGeneric["OBSERVACIONES"].ToString();

                this.cboNroSucursal.Text = conn.leerGeneric["NROCENTRO"].ToString();

                conn.DesconectarBDLeeGeneric();
            }
            catch { conn.DesconectarBDLeeGeneric(); }
        }

        private void lvwCliente_MouseDoubleClick(object sender, MouseEventArgs e) {
            RecargaDatos();

          /*  if (gpReporteCliente.Visible == false)
            {
                gpReporteCliente.Visible = true;
                ConsultaAnterior();
            }*/
        }

        private void RecargaDatos()
        {
            try
            {
                pasarClienteCod(Int16.Parse(this.txtCodCliente.Text));  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                pasarClientRS(this.txtRazonSocial.Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch { }
        }

        private void txtCodTipoIva_TextChanged(object sender, EventArgs e) {
            try {
                if (this.txtCodTipoIva.Text.Trim() != "") {
                    conn.ConsultaGeneric("Select * FROM TipoIva WHERE IdTipoIva = " + Convert.ToInt32(txtCodTipoIva.Text) + "", "TipoIva");

                    this.cboTipoIva.DataSource = conn.ds.Tables[0];
                    this.cboTipoIva.ValueMember = "IDTIPOIVA";
                    this.cboTipoIva.DisplayMember = "DESCRIPCION";
                }
                else
                    cboTipoIva.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboTipoIva.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void txtCodListaPrecio_TextChanged(object sender, EventArgs e) {
            try {
                if (this.txtCodListaPrecio.Text.Trim() != "") {
                    conn.ConsultaGeneric("Select * FROM ListaPrecios WHERE IdListaPrecio = " + Convert.ToInt32(this.txtCodListaPrecio.Text) + "", "ListaPrecios");

                    this.cboListaPrecio.DataSource = conn.ds.Tables[0];
                    this.cboListaPrecio.ValueMember = "IdListaPrecio";
                    this.cboListaPrecio.DisplayMember = "DESCRIPCION";
                }
                else
                    cboListaPrecio.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboListaPrecio.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void txtCodFormaPago_TextChanged(object sender, EventArgs e) {
            try {
                if (this.txtCodFormaPago.Text.Trim() != "") {
                    conn.ConsultaGeneric("Select * FROM FormaPago WHERE IdFormaPago = " + Convert.ToInt32(this.txtCodFormaPago.Text) + "", "FormaPago");

                    this.cboFormaPago.DataSource = conn.ds.Tables[0];
                    this.cboFormaPago.ValueMember = "IdFormaPago";
                    this.cboFormaPago.DisplayMember = "Descripcion";
                }
                else
                    cboFormaPago.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboFormaPago.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void txtCodEspecialidad_TextChanged(object sender, EventArgs e) {
            try {
                if (this.txtCodEspecialidad.Text.Trim() != "") {
                    conn.ConsultaGeneric("Select * FROM Especialidad WHERE IdEspecialidad = " + Convert.ToInt32(this.txtCodEspecialidad.Text) + "", "Especialidad");

                    this.cboEspecialidad.DataSource = conn.ds.Tables[0];
                    this.cboEspecialidad.ValueMember = "IdEspecialidad";
                    this.cboEspecialidad.DisplayMember = "Descripcion";
                }
                else
                    cboEspecialidad.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboEspecialidad.Text = "";

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
                    conn.ConsultaGeneric("Select * FROM Personal WHERE IdPersonal = " + Convert.ToInt32(this.txtCodPersonal.Text) + "", "Personal");

                    this.cboPersonal.DataSource = conn.ds.Tables[0];
                    this.cboPersonal.ValueMember = "IdPersonal";
                    this.cboPersonal.DisplayMember = "NOMBREYAPELLIDO";
                }
                else
                    cboPersonal.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboPersonal.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                gpoCliente.Visible = true;
                lvwCliente.Height = 275;

                btnEliminar.Enabled = false;
                tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;

                if (txtCodTipoIva.Text.Trim() == "")
                    txtCodTipoIva.Text = "null";
                if (txtCodEspecialidad.Text.Trim() == "")
                    txtCodEspecialidad.Text = "null";
                if (txtCodPersonal.Text.Trim() == "")
                    txtCodPersonal.Text = "null";
                if (txtCodFormaPago.Text.Trim() == "")
                    txtCodFormaPago.Text = "null";
                if (txtCodListaPrecio.Text.Trim() == "")
                    txtCodListaPrecio.Text = "null";
                if (txtDiasFactu.Text.Trim() == "")
                    txtDiasFactu.Text = "0";
                if (txtNroCuit.Text.Trim() == "")
                    txtNroCuit.Text = "0";

                string actualizar = "RAZONSOCIAL = '" + txtRazonSocial.Text + "', NUMDECUIT = " + txtNroCuit.Text + ", DOMICILIOCOMERCIAL = '" + txtDomicilioComercial.Text + "', LOCALIDAD = '" + txtLocalidad.Text + "', TELEFONOSCOMERCIALES = '" + txtTelefonosCom.Text + "', DOMICILIOPARTICULAR = '" + txtDomicilioPartic.Text + "', CONTACTO = '" + txtContacto.Text + "', IDTIPOIVA = " + txtCodTipoIva.Text + ", IDESPECIALIDAD = " + txtCodEspecialidad.Text + ", IDPERSONAL = " + txtCodPersonal.Text + ", IDFORMADEPAGO = " + txtCodFormaPago.Text + ", IDLISTAPRECIO = " + txtCodListaPrecio.Text + ", DIASENTREFACTURACION = " + Convert.ToDouble(txtDiasFactu.Text) + ", ULTIMAFACTURACION = '" + FormateoFecha() + "', OBSERVACIONES = '" + txtObservacion.Text + "', NROCENTRO='"+ cboNroSucursal.Text.Trim() + "'";
                if (conn.ActualizaGeneric("Cliente", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND NROCENTRO='"+ sPtoVta.Trim() +"' AND IdCliente = " + Convert.ToInt32(txtCodCliente.Text)))
                {
                    MostrarDatos();
                    MessageBox.Show("Datos Actualizados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Actualizar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { conn.DesconectarBDLeeGeneric(); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                btnEliminar.Enabled = true;
                tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;
                btnModificar.Enabled = false;
                btnGuardar.Enabled = false;

                if (conn.EliminarGeneric("Cliente", " IdCliente = " + txtCodCliente.Text))
                {
                    MostrarDatos();
                    MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }

        private void lvwCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                RecargaDatos();
            }
        }

        private void cboBuscaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtBuscarCliente.Focus();
            }
        }

        private void txtBuscarArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtRazonSocial.Focus();
            }
        }

        private void txtRazonSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtDomicilioComercial.Focus();
            }
        }

        private void txtDomicilioComercial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtLocalidad.Focus();
            }
        }

        private void txtLocalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtTelefonosCom.Focus();
            }
        }

        private void txtTelefonosCom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodTipoIva.Focus();
            }
        }

        private void txtCodTipoIva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboTipoIva.Focus();
            }
        }

        private void cboTipoIva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnTipoIva.Focus();
            }
        }

        private void btnTipoIva_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtNroCuit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtCodEspecialidad.Focus();
            }
        }

        private void txtCodEspecialidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboEspecialidad.Focus();
            }
        }

        private void cboEspecialidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnEspecialidad.Focus();
            }
        }

        private void btnEspecialidad_KeyPress(object sender, KeyPressEventArgs e)
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
                btnPersonal.Focus();
            }
        }

        private void btnPersonal_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCodListaPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                cboListaPrecio.Focus();
            }
        }

        private void cboListaPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnListaPrecio.Focus();
            }
        }

        private void btnListaPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtDomicilioPartic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtContacto.Focus();
            }
        }

        private void txtContacto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtUltimaFactu.Focus();
            }
        }

        private void txtUltimaFactu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtDiasFactu.Focus();
            }
        }

        private void txtDiasFactu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtObservacion.Focus();
            }
        }

        private void txtObservacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnGuardar.Focus();
            }
        }

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                NuevoCliente();
                txtRazonSocial.Focus();
            }
        }

        private void tsBtnReporte_Click(object sender, EventArgs e)
        {
            gpReporteCliente.Visible = true;
            gpFiltroClientes.Visible = true;
            //ConsultaAnterior();
        }

        private void btnVerReporte_Click(object sender, EventArgs e)
        {
            try
            {
                //   if (lvwCtaCte.Items.Count == 0)
                //       MessageBox.Show("Error: No se ha filtrado datos para mostrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //   else
                //   {


                if (chkTodaLaCtaCte.Checked == false && optCtaCte.Checked == true)
                {

                    if (sPtoVta.Trim() == "0001" && IDEMPRESA == 1)
                    { 
                        this.CtaCteClienteDeuda_MostradorTableAdapter.Fill(this.DGestionDTGeneral.CtaCteClienteDeuda_Mostrador); ///CtaCte del Cliente
                        this.rptClienteCtaCteMostrador.RefreshReport();
                    }
                    else
                    {
                        this.ctaCteClienteDeudaTableAdapter.Fill(this.DGestionDTGeneral.CtaCteClienteDeuda); ///CtaCte del Cliente
                        this.rptClienteCtaCte.RefreshReport();
                    }

                    if (btnVerReporte.Text == "     Reporte")
                    {
                        btnVerReporte.Text = "   Salir";
                        btnCerrar.Enabled = false;
                        gpFiltroClientes.Enabled = false;
                        btnVerDatos.Enabled = false;
                    }
                    else
                    {
                        btnVerReporte.Text = "     Reporte";
                        btnCerrar.Enabled = true;
                        gpFiltroClientes.Enabled = true;
                        btnVerDatos.Enabled = true;
                    }

                    if (rptClienteCtaCte.Visible == true)
                    {
                        rptClienteDeuda.Visible = false;
                        rptClienteTodaDeuda.Visible = false;
                        rptClienteCtaCte.Visible = false;
                        rptClienteCtaCteMostrador.Visible = false;
                        rptClienteTodaCtaCte.Visible = false;                        
                    }
                    else
                    {
                        rptClienteDeuda.Visible = false;
                        rptClienteTodaDeuda.Visible = false;

                        if (sPtoVta.Trim() == "0001" && IDEMPRESA == 1)
                            rptClienteCtaCteMostrador.Visible = true;
                        else
                            rptClienteCtaCte.Visible = true;

                        rptClienteTodaCtaCte.Visible = false;
                    }
                }

                else if (chkTodaLaCtaCte.Checked == true && optCtaCte.Checked == true)
                {
                    this.ctaCteClienteDeudaTableAdapter.Fill(this.DGestionDTGeneral.CtaCteClienteDeuda); //Toda la Cta Cte de los clientes
                    this.rptClienteTodaCtaCte.RefreshReport();

                    if (btnVerReporte.Text == "     Reporte")
                    {
                        btnVerReporte.Text = "   Salir";
                        btnCerrar.Enabled = false;
                        gpFiltroClientes.Enabled = false;
                        btnVerDatos.Enabled = false;
                    }
                    else
                    {
                        btnVerReporte.Text = "     Reporte";
                        btnCerrar.Enabled = true;
                        gpFiltroClientes.Enabled = true;
                        btnVerDatos.Enabled = true;
                    }

                    if (rptClienteTodaCtaCte.Visible == true)
                    {
                        rptClienteDeuda.Visible = false;
                        rptClienteTodaDeuda.Visible = false;
                        rptClienteCtaCte.Visible = false;
                        rptClienteCtaCteMostrador.Visible = false;
                        rptClienteTodaCtaCte.Visible = false;
                    }
                    else
                    {
                        rptClienteDeuda.Visible = false;
                        rptClienteTodaDeuda.Visible = false;
                        rptClienteCtaCte.Visible = false;
                        rptClienteCtaCteMostrador.Visible = false;
                        rptClienteTodaCtaCte.Visible = true;
                    }
                }

                else if (chkTodaLaDeuda.Checked == false && optDeudaCliente.Checked == true)
                {
                    this.ctaCteClienteDeudaTableAdapter.FillBy(this.DGestionDTGeneral.CtaCteClienteDeuda); //Deuda Cliente
                    this.rptClienteDeuda.RefreshReport();

                    if (btnVerReporte.Text == "     Reporte")
                    {
                        btnVerReporte.Text = "   Salir";
                        btnCerrar.Enabled = false;
                        gpFiltroClientes.Enabled = false;
                        btnVerDatos.Enabled = false;
                    }
                    else
                    {
                        btnVerReporte.Text = "     Reporte";
                        btnCerrar.Enabled = true;
                        gpFiltroClientes.Enabled = true;
                        btnVerDatos.Enabled = true;
                    }

                    if (rptClienteDeuda.Visible == true)
                    {
                        rptClienteDeuda.Visible = false;
                        rptClienteTodaDeuda.Visible = false;
                        rptClienteCtaCte.Visible = false;
                        rptClienteCtaCteMostrador.Visible = false;
                        rptClienteTodaCtaCte.Visible = false;
                    }
                    else
                    {
                        rptClienteDeuda.Visible = true;
                        rptClienteTodaDeuda.Visible = false;
                        rptClienteCtaCte.Visible = false;
                        rptClienteCtaCteMostrador.Visible = false;
                        rptClienteTodaCtaCte.Visible = false;
                    }
                }

                else if (chkTodaLaDeuda.Checked == true && optDeudaCliente.Checked == true)
                {
                    this.ctaCteClienteDeudaTableAdapter.FillBy(this.DGestionDTGeneral.CtaCteClienteDeuda); //Deuda Cliente
                    this.rptClienteTodaDeuda.RefreshReport();

                    if (btnVerReporte.Text == "     Reporte")
                    {
                        btnVerReporte.Text = "   Salir";
                        btnCerrar.Enabled = false;
                        gpFiltroClientes.Enabled = false;
                        btnVerDatos.Enabled = false;
                    }
                    else
                    {
                        btnVerReporte.Text = "     Reporte";
                        btnCerrar.Enabled = true;
                        gpFiltroClientes.Enabled = true;
                        btnVerDatos.Enabled = true;
                    }

                    if (rptClienteTodaDeuda.Visible == true)
                    {
                        rptClienteDeuda.Visible = false;
                        rptClienteTodaDeuda.Visible = false;
                        rptClienteCtaCte.Visible = false;
                        rptClienteCtaCteMostrador.Visible = false;
                        rptClienteTodaCtaCte.Visible = false;
                    }
                    else
                    {                        
                        rptClienteDeuda.Visible = false;
                        rptClienteTodaDeuda.Visible = true;
                        rptClienteCtaCte.Visible = false;
                        rptClienteCtaCteMostrador.Visible = false;
                        rptClienteTodaCtaCte.Visible = false;
                    }
                }

                //  }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btnVerDatos_Click(object sender, EventArgs e)
        {
            try
            {
                // if ((lvwCliente.SelectedItems.Count == 0 && lvwCliente.Items.Count != 0) && chkTodaLaCtaCte.Checked == false)
                if (lvwCliente.Items.Count == 0)
                    MessageBox.Show("Error: No existen clientes cargados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if ((chkTodaLaCtaCte.Checked == false) && (optCtaCte.Checked == true) && lvwCliente.SelectedItems.Count > 0)
                    {
                        btnVerReporte.Enabled = true;
                        ArmaCtaCte(Convert.ToInt32(lvwCliente.SelectedItems[0].SubItems[0].Text), dtFechaInicial.Text, 0);
                        LeeCtaCteCliente(0, -1);

                        lblSaldoFinal.Visible = false;
                        txtSaldoDeuda.Visible = false;

                        lblRazonSocial.Visible=true;
                        lblRazonSocialSeleccionada.Visible = true;
                        lblRazonSocialSeleccionada.Text = lvwCliente.SelectedItems[0].SubItems[1].Text;
                        btnCerrar.Enabled = true;
                    }
                    else if (chkTodaLaCtaCte.Checked == true && optCtaCte.Checked == true)
                    {
                        btnVerReporte.Enabled = true;
                        ArmaCtaCte(-1, dtFechaInicial.Text, 2);
                        LeeCtaCteCliente(2, -1);

                        lblSaldoFinal.Visible = false;
                        txtSaldoDeuda.Visible = false;

                        lblRazonSocial.Visible = false;
                        lblRazonSocialSeleccionada.Visible = false;
                        btnCerrar.Enabled = true;
                    }

                    else if (chkTodaLaDeuda.Checked == false && optDeudaCliente.Checked == true)
                    {
                        btnVerReporte.Enabled = true;
                        ArmaCtaCte(Convert.ToInt32(lvwCliente.SelectedItems[0].SubItems[0].Text), dtFechaInicial.Text, 1);
                        LeeCtaCteCliente(1, 0);

                        //lblSaldoFinal.Visible = true;
                        //txtSaldoDeuda.Visible = true;

                        lblRazonSocial.Visible = true;
                        lblRazonSocialSeleccionada.Visible = true;                        
                        lblRazonSocialSeleccionada.Text = lvwCliente.SelectedItems[0].SubItems[1].Text;
                        btnCerrar.Enabled = true;
                    }

                    else if (chkTodaLaDeuda.Checked == true && optDeudaCliente.Checked == true)
                    {
                        btnVerReporte.Enabled = true;
                        ArmaCtaCte(-1, dtFechaInicial.Text, 3);
                        LeeCtaCteCliente(3, 0);

                        //lblSaldoFinal.Visible = true;
                        //txtSaldoDeuda.Visible = true;

                        lblRazonSocial.Visible = false;
                        lblRazonSocialSeleccionada.Visible = false;                                               
                        lblRazonSocialSeleccionada.Text = lvwCliente.SelectedItems[0].SubItems[1].Text;
                        btnCerrar.Enabled = true;
                    }
                }   
            }
            catch { }
        }

        private void ArmaCtaCte(int idCliente, string FechaInicial, int iEstado)
        {
            try
            {                
                //iEstado = 0 => CtaCte del cliente
                //iEstado = 2 => Deuda del CLiente

                //iEstado = 1 => Toda la CtaCte del cliente                
                //iEstado = 3 => Toda la Deuda

                //idCliente = -1 => no muestra el cliente

                this.lvwCtaCte.Items.Clear();

                /////////////CTA CTE X CLIENTE///////////////////////////
                if (idCliente != -1 && iEstado == 0)
                {
                    conn.EliminarGeneric2("CtaCteCliente");
                    //lblSaldoFinal.Text = "Saldo Cta. Cte.:";

                    ///FACTURA///
                    SqlCommand cm = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, FacturasVentas.NROFACTURAINTERNO, FacturasVentas.NROFACTURA, FacturasVentas.FECHA, FacturasVentas.TOTAL, FacturasVentas.Pagado, FacturasVentas.Pendiente, EstadoSistema.Descripcion, FacturasVentas.NRONCINTERNO  FROM  Cliente, FacturasVentas, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = FacturasVentas.IDCLIENTE AND EstadoSistema.IdEstado = FacturasVentas.IDESTADO AND CLIENTE.IDCLIENTE = " + lvwCliente.SelectedItems[0].SubItems[0].Text + " AND FacturasVentas.FECHA >= '" + FechaInicial + "' AND FacturasVentas.sucursal='" + sPtoVta + "'", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["NRONCINTERNO"].ToString().Trim() == "")
                        {
                            string agregar = "INSERT INTO CtaCteCliente VALUES(" + dr["IDCLIENTE"].ToString() + ", '" + dr["RAZONSOCIAL"].ToString() + "', '" + dr["NROFACTURA"].ToString() + "', 'FA', '" + FormateoFecha2(dr["FECHA"].ToString()) + "', (Cast(replace('" + dr["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0','0', '" + dr["Descripcion"].ToString() + "', (Cast(replace('" + dr["PAGADO"].ToString() + "', ',', '.') as decimal(10,2))), (Cast(replace('" + dr["PENDIENTE"].ToString() + "', ',', '.') as decimal(10,2))),'0', " + IDEMPRESA + ", '0')";
                            conn.InsertarGeneric(agregar);
                            cm.Connection.Close();
                            conn.DesconectarBD();
                        }
                        else
                        {
                            string agregar = "INSERT INTO CtaCteCliente VALUES(" + dr["IDCLIENTE"].ToString() + ", '" + dr["RAZONSOCIAL"].ToString() + "', '" + dr["NROFACTURA"].ToString() + "', 'FA', '" + FormateoFecha2(dr["FECHA"].ToString()) + "', (Cast(replace('" + dr["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0','0', '" + dr["Descripcion"].ToString() + "', (Cast(replace('" + dr["PAGADO"].ToString() + "', ',', '.') as decimal(10,2))), (Cast(replace('" + dr["PENDIENTE"].ToString() + "', ',', '.') as decimal(10,2))),'0', " + IDEMPRESA + ", " + dr["NRONCINTERNO"].ToString() + ")";
                            conn.InsertarGeneric(agregar);
                            cm.Connection.Close();
                            conn.DesconectarBD();
                        }
                    }

                    //////////////////////////////////////////
                    ///RECIBO///
                    SqlCommand cm10 = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, Recibos.NRORECIBOINTERNO, Recibos.NRORECIBO, Recibos.FECHA, Recibos.TOTAL, Recibos.ImporteRestante, EstadoSistema.Descripcion FROM Cliente, Recibos, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = Recibos.IDCLIENTE AND EstadoSistema.IdEstado = Recibos.IDESTADO AND CLIENTE.IDCLIENTE = " + lvwCliente.SelectedItems[0].SubItems[0].Text + " AND Recibos.FECHA >= '" + FechaInicial + "' AND Recibos.sucursal='" + sPtoVta + "' ", conectaEstado);

                    SqlDataAdapter da10 = new SqlDataAdapter(cm10);
                    DataTable dt10 = new DataTable();
                    da10.Fill(dt10);

                    foreach (DataRow dr10 in dt10.Rows)
                    {
                        string agregar2 = "INSERT INTO CtaCteCliente VALUES(" + dr10["IDCLIENTE"].ToString() + ", '" + dr10["RAZONSOCIAL"].ToString() + "', '" + dr10["NRORECIBO"].ToString() + "', 'RC', '" + FormateoFecha2(dr10["FECHA"].ToString()) + "', '0', (Cast(replace('" + dr10["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0', '" + dr10["Descripcion"].ToString() + "', '0', '0', (Cast(replace('" + dr10["IMPORTERESTANTE"].ToString() + "', ',', '.') as decimal(10,2))), " + IDEMPRESA + ",'0')";
                        conn.InsertarGeneric(agregar2);
                        cm10.Connection.Close();
                        conn.DesconectarBD();
                    }
                    //////////////////////////////////////////

                    ///NOTA DE DEBITO///
                    SqlCommand cm30 = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, NotaDebito.NRONOTAINTERNO, NotaDebito.NRONOTADEBITO, NotaDebito.FECHA, NotaDebito.TOTAL, EstadoSistema.Descripcion FROM Cliente, NotaDebito, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = NotaDebito.IDCLIENTE AND NotaDebito.FECHA >= '" + FechaInicial + "' AND CLIENTE.IDCLIENTE = " + lvwCliente.SelectedItems[0].SubItems[0].Text + " AND EstadoSistema.IdEstado = NotaDebito.IDESTADO AND NotaDebito.sucursal='" + sPtoVta + "'", conectaEstado);

                    SqlDataAdapter da30 = new SqlDataAdapter(cm30);
                    DataTable dt30 = new DataTable();
                    da30.Fill(dt30);

                    foreach (DataRow dr30 in dt30.Rows)
                    {
                        string agregar3 = "INSERT INTO CtaCteCliente VALUES(" + dr30["IDCLIENTE"].ToString() + ", '" + dr30["RAZONSOCIAL"].ToString() + "', '" + dr30["NRONOTADEBITO"].ToString().Trim() + "', 'ND', '" + FormateoFecha2(dr30["FECHA"].ToString()) + "', (Cast(replace('" + dr30["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0', '0','0', '" + dr30["Descripcion"].ToString() + "', '0',-(Cast(replace('" + dr30["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))),'0' ," + IDEMPRESA + ",'0')";
                        conn.InsertarGeneric(agregar3);
                        cm30.Connection.Close();
                        conn.DesconectarBD();
                    }
                    cm.Connection.Close();
                    //////////////////////////////////////////



                    ///NOTA DE CREDITO///
                    SqlCommand cm20 = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, NotaCredito.NRONOTAINTERNO, NotaCredito.NRONOTACRED, NotaCredito.FECHA, NotaCredito.TOTAL, EstadoSistema.Descripcion FROM  Cliente, NotaCredito, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = NotaCredito.IDCLIENTE AND NotaCredito.FECHA >= '" + FechaInicial + "' AND CLIENTE.IDCLIENTE = " + lvwCliente.SelectedItems[0].SubItems[0].Text + " AND EstadoSistema.IdEstado = NotaCredito.IDESTADO AND NotaCredito.sucursal='" + sPtoVta + "'", conectaEstado);

                    SqlDataAdapter da20 = new SqlDataAdapter(cm20);
                    DataTable dt20 = new DataTable();
                    da20.Fill(dt20);

                    foreach (DataRow dr20 in dt20.Rows)
                    {
                        string agregar3 = "INSERT INTO CtaCteCliente VALUES(" + dr20["IDCLIENTE"].ToString() + ", '" + dr20["RAZONSOCIAL"].ToString() + "', '" + dr20["NRONOTACRED"].ToString().Trim() + "', 'NC', '" + FormateoFecha2(dr20["FECHA"].ToString()) + "', '0', (Cast(replace('" + dr20["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0', '" + dr20["Descripcion"].ToString() + "', '0',(Cast(replace('" + dr20["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))),'0' ," + IDEMPRESA + ",'0')";
                        conn.InsertarGeneric(agregar3);
                        cm20.Connection.Close();
                        conn.DesconectarBD();
                    }
                    cm20.Connection.Close();
                    //////////////////////////////////////////                  
                }



                /////////////DEUDA X CLIENTE/////////////////////////
                else if (idCliente != -1 && iEstado == 1)
                {
                    conn.EliminarGeneric2("CtaCteCliente");

                    ///FACTURA///
                    SqlCommand cm = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, FacturasVentas.NROFACTURAINTERNO, FacturasVentas.NROFACTURA, FacturasVentas.FECHA, FacturasVentas.TOTAL, FacturasVentas.Pagado, FacturasVentas.Pendiente, FacturasVentas.NRONCINTERNO, EstadoSistema.Descripcion, FacturasVentas.NRONCINTERNO  FROM  Cliente, FacturasVentas, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = FacturasVentas.IDCLIENTE AND EstadoSistema.IdEstado = FacturasVentas.IDESTADO AND (EstadoSistema.Descripcion = 'No Pagado' OR EstadoSistema.Descripcion = 'Pago Parcial') AND CLIENTE.IDCLIENTE = " + lvwCliente.SelectedItems[0].SubItems[0].Text + " AND FacturasVentas.FECHA >= '" + FechaInicial + "' AND  FacturasVentas.sucursal='" + sPtoVta + "'", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["NRONCINTERNO"].ToString().Trim() == "")
                        {
                            string agregar = "INSERT INTO CtaCteCliente VALUES(" + dr["IDCLIENTE"].ToString() + ", '" + dr["RAZONSOCIAL"].ToString() + "', '" + dr["NROFACTURA"].ToString() + "', 'FA', '" + FormateoFecha2(dr["FECHA"].ToString()) + "', (Cast(replace('" + dr["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0','0', '" + dr["Descripcion"].ToString() + "', (Cast(replace('" + dr["PAGADO"].ToString() + "', ',', '.') as decimal(10,2))), (Cast(replace('" + dr["PENDIENTE"].ToString() + "', ',', '.') as decimal(10,2))),'0', " + IDEMPRESA + ", '0')";
                            conn.InsertarGeneric(agregar);
                            cm.Connection.Close();
                            conn.DesconectarBD();
                        }
                        else
                        {
                            string agregar = "INSERT INTO CtaCteCliente VALUES(" + dr["IDCLIENTE"].ToString() + ", '" + dr["RAZONSOCIAL"].ToString() + "', '" + dr["NROFACTURA"].ToString() + "', 'FA', '" + FormateoFecha2(dr["FECHA"].ToString()) + "', (Cast(replace('" + dr["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0','0', '" + dr["Descripcion"].ToString() + "', (Cast(replace('" + dr["PAGADO"].ToString() + "', ',', '.') as decimal(10,2))), (Cast(replace('" + dr["PENDIENTE"].ToString() + "', ',', '.') as decimal(10,2))),'0', " + IDEMPRESA + ", " + Convert.ToInt32(dr["NRONCINTERNO"].ToString()) + ")";
                            conn.InsertarGeneric(agregar);
                            cm.Connection.Close();
                            conn.DesconectarBD();
                        }
                    }
                    //////////////////////////////////////////

                    /*///RECIBO///
                    SqlCommand cm10 = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, Recibos.NRORECIBOINTERNO, Recibos.NRORECIBO, Recibos.FECHA, Recibos.TOTAL, Recibos.ImporteRestante FROM  Cliente, Recibos WHERE Cliente.IDCLIENTE = Recibos.IDCLIENTE AND CLIENTE.IDCLIENTE = " + lvwCliente.SelectedItems[0].SubItems[0].Text + " AND Recibos.FECHA >= '" + FechaInicial + "'", conectaEstado);

                    SqlDataAdapter da10 = new SqlDataAdapter(cm10);
                    DataTable dt10 = new DataTable();
                    da10.Fill(dt10);

                    foreach (DataRow dr10 in dt10.Rows)
                    {
                        string agregar2 = "INSERT INTO CtaCteCliente VALUES(" + dr10["IDCLIENTE"].ToString() + ", '" + dr10["RAZONSOCIAL"].ToString() + "', '" + dr10["NRORECIBO"].ToString() + "', 'RC', '" + FormateoFecha2(dr10["FECHA"].ToString()) + "', '0', (Cast(replace('" + dr10["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0','-', '0', '0', (Cast(replace('" + dr10["IMPORTERESTANTE"].ToString() + "', ',', '.') as decimal(10,2))))";
                        conn.InsertarGeneric(agregar2);
                        cm10.Connection.Close();
                        conn.DesconectarBD();
                    }                    */
                    //////////////////////////////////////////

                    //////////////////////////////////////////
                    ///NOTA DE DEBITO///
                    SqlCommand cm30 = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, NotaDebito.NRONOTAINTERNO, NotaDebito.NRONOTADEBITO, NotaDebito.FECHA, NotaDebito.TOTAL, EstadoSistema.Descripcion FROM Cliente, NotaDebito, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = NotaDebito.IDCLIENTE AND NotaDebito.FECHA >= '" + FechaInicial + "' AND CLIENTE.IDCLIENTE = " + lvwCliente.SelectedItems[0].SubItems[0].Text + " AND EstadoSistema.IdEstado = NotaDebito.IDESTADO AND NotaDebito.sucursal='" + sPtoVta + "'", conectaEstado);

                    SqlDataAdapter da30 = new SqlDataAdapter(cm30);
                    DataTable dt30 = new DataTable();
                    da30.Fill(dt30);

                    foreach (DataRow dr30 in dt30.Rows)
                    {
                        string agregar3 = "INSERT INTO CtaCteCliente VALUES(" + dr30["IDCLIENTE"].ToString() + ", '" + dr30["RAZONSOCIAL"].ToString() + "', '" + dr30["NRONOTADEBITO"].ToString().Trim() + "', 'ND', '" + FormateoFecha2(dr30["FECHA"].ToString()) + "', (Cast(replace('" + dr30["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0', '0','0', '" + dr30["Descripcion"].ToString() + "', '0',-(Cast(replace('" + dr30["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))),'0' ," + IDEMPRESA + ",'0')";
                        conn.InsertarGeneric(agregar3);
                        cm30.Connection.Close();
                        conn.DesconectarBD();
                    }
                    cm.Connection.Close();
                    //////////////////////////////////////////

                    ///NOTA DE CREDITO///
                    SqlCommand cm20 = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, NotaCredito.NRONOTAINTERNO, NotaCredito.NRONOTACRED, NotaCredito.FECHA, NotaCredito.TOTAL, EstadoSistema.Descripcion FROM  Cliente, NotaCredito, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = NotaCredito.IDCLIENTE AND NotaCredito.FECHA >= '" + FechaInicial + "' AND CLIENTE.IDCLIENTE = " + lvwCliente.SelectedItems[0].SubItems[0].Text + " AND EstadoSistema.IdEstado = NotaCredito.IDESTADO AND NotaCredito.sucursal='" + sPtoVta + "'", conectaEstado);

                    SqlDataAdapter da20 = new SqlDataAdapter(cm20);
                    DataTable dt20 = new DataTable();
                    da20.Fill(dt20);

                    foreach (DataRow dr20 in dt20.Rows)
                    {
                        string agregar3 = "INSERT INTO CtaCteCliente VALUES(" + dr20["IDCLIENTE"].ToString() + ", '" + dr20["RAZONSOCIAL"].ToString() + "', '" + dr20["NRONOTACRED"].ToString().Trim() + "', 'NC', '" + FormateoFecha2(dr20["FECHA"].ToString()) + "', '0', (Cast(replace('" + dr20["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0', '" + dr20["Descripcion"].ToString() + "', '0',(Cast(replace('" + dr20["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))),'0' ," + IDEMPRESA + ",'0')";
                        conn.InsertarGeneric(agregar3);
                        cm20.Connection.Close();
                        conn.DesconectarBD();
                    }
                    cm20.Connection.Close();
                    ////////////////////////////////////////// 
                }



                /////////////TODA LA DEUDA///////////////////////////
                else if (idCliente == -1 && iEstado == 3)
                {
                    conn.EliminarGeneric2("CtaCteCliente");

                    ///FACTURA///
                    SqlCommand cm = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, FacturasVentas.NROFACTURAINTERNO, FacturasVentas.NROFACTURA, FacturasVentas.FECHA, FacturasVentas.TOTAL, FacturasVentas.Pagado, FacturasVentas.Pendiente, FacturasVentas.NRONCINTERNO, EstadoSistema.Descripcion, FacturasVentas.NRONCINTERNO FROM  Cliente, FacturasVentas, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = FacturasVentas.IDCLIENTE AND EstadoSistema.IdEstado = FacturasVentas.IDESTADO AND (EstadoSistema.Descripcion = 'No Pagado' OR EstadoSistema.Descripcion = 'Pago Parcial') AND FacturasVentas.FECHA >= '" + FechaInicial + "' AND FacturasVentas.sucursal='" + sPtoVta + "'", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["NRONCINTERNO"].ToString().Trim() == "")
                        {
                            string agregar = "INSERT INTO CtaCteCliente VALUES(" + dr["IDCLIENTE"].ToString() + ", '" + dr["RAZONSOCIAL"].ToString() + "', '" + dr["NROFACTURA"].ToString() + "', 'FA', '" + FormateoFecha2(dr["FECHA"].ToString()) + "', (Cast(replace('" + dr["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0','0', '" + dr["Descripcion"].ToString() + "', (Cast(replace('" + dr["PAGADO"].ToString() + "', ',', '.') as decimal(10,2))), (Cast(replace('" + dr["PENDIENTE"].ToString() + "', ',', '.') as decimal(10,2))),'0', " + IDEMPRESA + ", '0')";
                            conn.InsertarGeneric(agregar);
                            cm.Connection.Close();
                            conn.DesconectarBD();
                        }
                        else
                        {
                            string agregar = "INSERT INTO CtaCteCliente VALUES(" + dr["IDCLIENTE"].ToString() + ", '" + dr["RAZONSOCIAL"].ToString() + "', '" + dr["NROFACTURA"].ToString() + "', 'FA', '" + FormateoFecha2(dr["FECHA"].ToString()) + "', (Cast(replace('" + dr["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0','0', '" + dr["Descripcion"].ToString() + "', (Cast(replace('" + dr["PAGADO"].ToString() + "', ',', '.') as decimal(10,2))), (Cast(replace('" + dr["PENDIENTE"].ToString() + "', ',', '.') as decimal(10,2))),'0', " + IDEMPRESA + ", " + Convert.ToInt32(dr["NRONCINTERNO"].ToString()) + ")";
                            conn.InsertarGeneric(agregar);
                            cm.Connection.Close();
                            conn.DesconectarBD();
                        }
                    }
                    //////////////////////////////////////////

                    ///RECIBO///
                    /*SqlCommand cm10 = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, Recibos.NRORECIBOINTERNO, Recibos.NRORECIBO, Recibos.FECHA, Recibos.TOTAL, Recibos.ImporteRestante FROM  Cliente, Recibos WHERE Cliente.IDCLIENTE = Recibos.IDCLIENTE AND Recibos.FECHA >= '" + FechaInicial + "'", conectaEstado);

                    SqlDataAdapter da10 = new SqlDataAdapter(cm10);
                    DataTable dt10 = new DataTable();
                    da10.Fill(dt10);

                    foreach (DataRow dr10 in dt10.Rows)
                    {
                        string agregar2 = "INSERT INTO CtaCteCliente VALUES(" + dr10["IDCLIENTE"].ToString() + ", '" + dr10["RAZONSOCIAL"].ToString() + "', '" + dr10["NRORECIBO"].ToString() + "', 'RC', '" + FormateoFecha2(dr10["FECHA"].ToString()) + "', '0', (Cast(replace('" + dr10["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0','-', '0', '0', (Cast(replace('" + dr10["IMPORTERESTANTE"].ToString() + "', ',', '.') as decimal(10,2))))";
                        conn.InsertarGeneric(agregar2);
                        cm10.Connection.Close();
                        conn.DesconectarBD();
                    }                    */
                    //////////////////////////////////////////

                    //////////////////////////////////////////
                    ///NOTA DE DEBITO///
                    SqlCommand cm30 = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, NotaDebito.NRONOTAINTERNO, NotaDebito.NRONOTADEBITO, NotaDebito.FECHA, NotaDebito.TOTAL, EstadoSistema.Descripcion FROM Cliente, NotaDebito, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = NotaDebito.IDCLIENTE AND NotaDebito.FECHA >= '" + FechaInicial + "' AND EstadoSistema.IdEstado = NotaDebito.IDESTADO AND NotaDebito.sucursal='" + sPtoVta + "'", conectaEstado);

                    SqlDataAdapter da30 = new SqlDataAdapter(cm30);
                    DataTable dt30 = new DataTable();
                    da30.Fill(dt30);

                    foreach (DataRow dr30 in dt30.Rows)
                    {
                        string agregar3 = "INSERT INTO CtaCteCliente VALUES(" + dr30["IDCLIENTE"].ToString() + ", '" + dr30["RAZONSOCIAL"].ToString() + "', '" + dr30["NRONOTADEBITO"].ToString().Trim() + "', 'ND', '" + FormateoFecha2(dr30["FECHA"].ToString()) + "', (Cast(replace('" + dr30["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0', '0','0', '" + dr30["Descripcion"].ToString() + "', '0',-(Cast(replace('" + dr30["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))),'0' ," + IDEMPRESA + ",'0')";
                        conn.InsertarGeneric(agregar3);
                        cm30.Connection.Close();
                        conn.DesconectarBD();
                    }
                    cm.Connection.Close();
                    //////////////////////////////////////////


                    ///NOTA DE CREDITO///
                    SqlCommand cm20 = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, NotaCredito.NRONOTAINTERNO, NotaCredito.NRONOTACRED, NotaCredito.FECHA, NotaCredito.TOTAL, EstadoSistema.Descripcion FROM  Cliente, NotaCredito, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = NotaCredito.IDCLIENTE AND NotaCredito.FECHA >= '" + FechaInicial + "' AND EstadoSistema.IdEstado = NotaCredito.IDESTADO AND NotaCredito.sucursal='" + sPtoVta + "'", conectaEstado);

                    SqlDataAdapter da20 = new SqlDataAdapter(cm20);
                    DataTable dt20 = new DataTable();
                    da20.Fill(dt20);

                    foreach (DataRow dr20 in dt20.Rows)
                    {
                        string agregar3 = "INSERT INTO CtaCteCliente VALUES(" + dr20["IDCLIENTE"].ToString() + ", '" + dr20["RAZONSOCIAL"].ToString() + "', '" + dr20["NRONOTACRED"].ToString().Trim() + "', 'NC', '" + FormateoFecha2(dr20["FECHA"].ToString()) + "', '0', (Cast(replace('" + dr20["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0', '" + dr20["Descripcion"].ToString() + "', '0',(Cast(replace('" + dr20["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))),'0' ," + IDEMPRESA + ",'0')";
                        conn.InsertarGeneric(agregar3);
                        cm20.Connection.Close();
                        conn.DesconectarBD();
                    }
                    cm20.Connection.Close();
                    ////////////////////////////////////////// 
                }


                /////////////TODA LA CTACTE///////////////////////////
                else if (idCliente == -1 && iEstado == 2)
                {
                    conn.EliminarGeneric2("CtaCteCliente");

                    conn.EliminarGeneric2("CtaCteCliente");
                    //lblSaldoFinal.Text = "Saldo Cta. Cte.:";

                    ///FACTURA///
                    SqlCommand cm = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, FacturasVentas.NROFACTURAINTERNO, FacturasVentas.NROFACTURA, FacturasVentas.FECHA, FacturasVentas.TOTAL, FacturasVentas.Pagado, FacturasVentas.Pendiente, EstadoSistema.Descripcion, FacturasVentas.NRONCINTERNO FROM  Cliente, FacturasVentas, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = FacturasVentas.IDCLIENTE AND EstadoSistema.IdEstado = FacturasVentas.IDESTADO AND FacturasVentas.FECHA >= '" + FechaInicial + "' AND FacturasVentas.sucursal='" + sPtoVta + "'", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["NRONCINTERNO"].ToString().Trim() == "")
                        {
                            string agregar = "INSERT INTO CtaCteCliente VALUES(" + dr["IDCLIENTE"].ToString() + ", '" + dr["RAZONSOCIAL"].ToString() + "', '" + dr["NROFACTURA"].ToString() + "', 'FA', '" + FormateoFecha2(dr["FECHA"].ToString()) + "', (Cast(replace('" + dr["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0','0', '" + dr["Descripcion"].ToString() + "', (Cast(replace('" + dr["PAGADO"].ToString() + "', ',', '.') as decimal(10,2))), (Cast(replace('" + dr["PENDIENTE"].ToString() + "', ',', '.') as decimal(10,2))),'0', " + IDEMPRESA + ", '0')";
                            conn.InsertarGeneric(agregar);
                            cm.Connection.Close();
                            conn.DesconectarBD();
                        }
                        else
                        {
                            string agregar = "INSERT INTO CtaCteCliente VALUES(" + dr["IDCLIENTE"].ToString() + ", '" + dr["RAZONSOCIAL"].ToString() + "', '" + dr["NROFACTURA"].ToString() + "', 'FA', '" + FormateoFecha2(dr["FECHA"].ToString()) + "', (Cast(replace('" + dr["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0','0', '" + dr["Descripcion"].ToString() + "', (Cast(replace('" + dr["PAGADO"].ToString() + "', ',', '.') as decimal(10,2))), (Cast(replace('" + dr["PENDIENTE"].ToString() + "', ',', '.') as decimal(10,2))),'0', " + IDEMPRESA + ", " + Convert.ToInt32(dr["NRONCINTERNO"].ToString()) + ")";
                            conn.InsertarGeneric(agregar);
                            cm.Connection.Close();
                            conn.DesconectarBD();
                        }
                    }

                    //////////////////////////////////////////
                    
                    ///RECIBO///
                    SqlCommand cm10 = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, Recibos.NRORECIBOINTERNO, Recibos.NRORECIBO, Recibos.FECHA, Recibos.TOTAL, Recibos.ImporteRestante, EstadoSistema.Descripcion FROM  Cliente, Recibos, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = Recibos.IDCLIENTE AND EstadoSistema.IdEstado = Recibos.IDESTADO AND Recibos.FECHA >= '" + FechaInicial + "' AND Recibos.sucursal='" + sPtoVta + "'", conectaEstado);

                    SqlDataAdapter da10 = new SqlDataAdapter(cm10);
                    DataTable dt10 = new DataTable();
                    da10.Fill(dt10);

                    foreach (DataRow dr10 in dt10.Rows)
                    {
                        string agregar2 = "INSERT INTO CtaCteCliente VALUES(" + dr10["IDCLIENTE"].ToString() + ", '" + dr10["RAZONSOCIAL"].ToString() + "', '" + dr10["NRORECIBO"].ToString() + "', 'RC', '" + FormateoFecha2(dr10["FECHA"].ToString()) + "', '0', (Cast(replace('" + dr10["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0', '" + dr10["Descripcion"].ToString() + "', '0', '0', (Cast(replace('" + dr10["IMPORTERESTANTE"].ToString() + "', ',', '.') as decimal(10,2))), " + IDEMPRESA + ",'0')";
                        conn.InsertarGeneric(agregar2);
                        cm10.Connection.Close();
                        conn.DesconectarBD();
                    }
                    //////////////////////////////////////////

                    //////////////////////////////////////////
                    ///NOTA DE DEBITO///
                    SqlCommand cm30 = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, NotaDebito.NRONOTAINTERNO, NotaDebito.NRONOTADEBITO, NotaDebito.FECHA, NotaDebito.TOTAL, EstadoSistema.Descripcion FROM Cliente, NotaDebito, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = NotaDebito.IDCLIENTE AND NotaDebito.FECHA >= '" + FechaInicial + "' AND EstadoSistema.IdEstado = NotaDebito.IDESTADO AND NotaDebito.sucursal='" + sPtoVta + "'", conectaEstado);

                    SqlDataAdapter da30 = new SqlDataAdapter(cm30);
                    DataTable dt30 = new DataTable();
                    da30.Fill(dt30);

                    foreach (DataRow dr30 in dt30.Rows)
                    {
                        string agregar3 = "INSERT INTO CtaCteCliente VALUES(" + dr30["IDCLIENTE"].ToString() + ", '" + dr30["RAZONSOCIAL"].ToString() + "', '" + dr30["NRONOTADEBITO"].ToString().Trim() + "', 'ND', '" + FormateoFecha2(dr30["FECHA"].ToString()) + "', (Cast(replace('" + dr30["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0', '0','0', '" + dr30["Descripcion"].ToString() + "', '0',-(Cast(replace('" + dr30["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))),'0' ," + IDEMPRESA + ",'0')";
                        conn.InsertarGeneric(agregar3);
                        cm30.Connection.Close();
                        conn.DesconectarBD();
                    }
                    cm.Connection.Close();
                    //////////////////////////////////////////
                    ///NOTA DE CREDITO///
                    SqlCommand cm20 = new SqlCommand("SELECT Cliente.IDCLIENTE, Cliente.RAZONSOCIAL, NotaCredito.NRONOTAINTERNO, NotaCredito.NRONOTACRED, NotaCredito.FECHA, NotaCredito.TOTAL, EstadoSistema.Descripcion FROM  Cliente, NotaCredito, EstadoSistema WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.IDCLIENTE = NotaCredito.IDCLIENTE AND NotaCredito.FECHA >= '" + FechaInicial + "' AND EstadoSistema.IdEstado = NotaCredito.IDESTADO AND NotaCredito.sucursal='" + sPtoVta + "'", conectaEstado);

                    SqlDataAdapter da20 = new SqlDataAdapter(cm20);
                    DataTable dt20 = new DataTable();
                    da20.Fill(dt20);

                    foreach (DataRow dr20 in dt20.Rows)
                    {
                        string agregar3 = "INSERT INTO CtaCteCliente VALUES(" + dr20["IDCLIENTE"].ToString() + ", '" + dr20["RAZONSOCIAL"].ToString() + "', '" + dr20["NRONOTACRED"].ToString().Trim() + "', 'NC', '" + FormateoFecha2(dr20["FECHA"].ToString()) + "', '0', (Cast(replace('" + dr20["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))), '0','0', '" + dr20["Descripcion"].ToString() + "', '0',(Cast(replace('" + dr20["TOTAL"].ToString() + "', ',', '.') as decimal(10,2))),'0' ," + IDEMPRESA + ",'0')";
                        conn.InsertarGeneric(agregar3);
                        cm20.Connection.Close();
                        conn.DesconectarBD();
                    }
                    cm20.Connection.Close();
                    //////////////////////////////////////////  
                }

                ////////////CALCULA DEBE HABER////////////////
                SqlCommand cm100 = new SqlCommand("SELECT * FROM CtaCteCliente WHERE CtaCteCliente.IDEMPRESA = " + IDEMPRESA + " ORDER BY FECHA, RAZONSOCIAL asc", conectaEstado);
                SqlDataAdapter da100 = new SqlDataAdapter(cm100);
                DataTable dt100 = new DataTable();
                da100.Fill(dt100);

                double fSaldo = 0;
                //double fHaber = 0;
                double fDebe = 0;
                
                //double fDeuda = 0;
                double fTotalDeuda = 0;

                long iItemCtaCte = 0;

                foreach (DataRow dr100 in dt100.Rows)
                {
                    lvwCtaCte.SmallImageList = imageList2;
                    ListViewItem item = new ListViewItem(dr100["IDITEM"].ToString().Trim());

                    item.SubItems.Add(dr100["RAZONSOCIAL"].ToString().Trim());
                    item.SubItems.Add(dr100["FECHA"].ToString().Trim());
                    item.SubItems.Add(dr100["NROCOMPROBANTE"].ToString().Trim());
                    item.SubItems.Add(dr100["TIPOCOMPROBANTE"].ToString().Trim());

                    iItemCtaCte = Convert.ToInt32(dr100["IDITEM"].ToString().Trim());

                    if (dr100["TIPOCOMPROBANTE"].ToString().Trim() == "FA")
                    {
                        item.ImageIndex = 1; //DEBE
                        fDebe = Convert.ToDouble(dr100["DEBE"].ToString());  //Calculo de Cta Cte
                        fSaldo = fSaldo - fDebe;                 //Calculo de Cta Cte
                        
                        fTotalDeuda =  -(fDebe);                  //Calculo Deuda

                        string actualizar = "SALDO = (Cast(replace('" + Math.Round(fSaldo, 2) + "', ',', '.') as decimal(10,2))), MONTOPENDIENTE = (Cast(replace('" + Math.Round(fTotalDeuda, 2) + "', ',', '.') as decimal(10,2)))";

                        conn.ActualizaGeneric("CtaCteCliente", actualizar, " CtaCteCliente.IDEMPRESA = " + IDEMPRESA + " AND IdItem = " + iItemCtaCte);
                    }

                    if (dr100["TIPOCOMPROBANTE"].ToString().Trim() == "ND")
                    {
                        item.ImageIndex = 1; //DEBE
                        fDebe = Convert.ToDouble(dr100["DEBE"].ToString());
                        fSaldo = fSaldo - fDebe;

                        fTotalDeuda = -(fDebe);                  //Calculo Deuda

                        string actualizar = "SALDO = (Cast(replace('" + Math.Round(fSaldo, 2) + "', ',', '.') as decimal(10,2))), MONTOPENDIENTE = (Cast(replace('" + Math.Round(fTotalDeuda, 2) + "', ',', '.') as decimal(10,2)))";

                        conn.ActualizaGeneric("CtaCteCliente", actualizar, " CtaCteCliente.IDEMPRESA = " + IDEMPRESA + " AND IdItem = " + iItemCtaCte);
                    }

                    if (dr100["TIPOCOMPROBANTE"].ToString().Trim() == "RC")
                    {
                        item.ImageIndex = 0; //HABER
                        fDebe = Convert.ToDouble(dr100["HABER"].ToString());
                        fSaldo = fSaldo + fDebe;

                        fTotalDeuda = 0;//fDebe;                  //Calculo Deuda

                        string actualizar = "SALDO = (Cast(replace('" + Math.Round(fSaldo, 2) + "', ',', '.') as decimal(10,2))), MONTOPENDIENTE = (Cast(replace('" + Math.Round(fTotalDeuda, 2) + "', ',', '.') as decimal(10,2)))";
                        
                        conn.ActualizaGeneric("CtaCteCliente", actualizar, " CtaCteCliente.IDEMPRESA = " + IDEMPRESA + " AND IdItem = " + iItemCtaCte);
                    }

                    if (dr100["TIPOCOMPROBANTE"].ToString().Trim() == "NC")
                    {
                        item.ImageIndex = 0; //HABER
                        fDebe = Convert.ToDouble(dr100["HABER"].ToString());
                        fSaldo = fSaldo + fDebe;

                        fTotalDeuda = 0;//fDebe;                  //Calculo Deuda

                        string actualizar = "SALDO = (Cast(replace('" + Math.Round(fSaldo, 2) + "', ',', '.') as decimal(10,2))), MONTOPENDIENTE = (Cast(replace('" + Math.Round(fTotalDeuda, 2) + "', ',', '.') as decimal(10,2)))";

                        conn.ActualizaGeneric("CtaCteCliente", actualizar, " CtaCteCliente.IDEMPRESA = " + IDEMPRESA + " AND IdItem = " + iItemCtaCte);
                    }                    

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr100["DEBE"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr100["HABER"]), 2).ToString(), Color.Empty, Color.LightGray, null);


                    if (fSaldo >= 0)
                            item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr100["SALDO"]), 2).ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                     else
                            item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr100["SALDO"]), 2).ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
  
                        
                    if (Convert.ToDecimal(dr100["MONTOPENDIENTE"].ToString()) >= 0)
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr100["MONTOPENDIENTE"]), 2).ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    else
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(dr100["MONTOPENDIENTE"]), 2).ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));


                    //Estados del Movimiento
                    if (dr100["DESCRIPCION"].ToString().Trim() == "Pagado")
                        item.SubItems.Add(dr100["DESCRIPCION"].ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    else if (dr100["DESCRIPCION"].ToString().Trim() == "No Pagado")
                        item.SubItems.Add(dr100["DESCRIPCION"].ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));

                    else if (dr100["DESCRIPCION"].ToString().Trim() == "Vinculado")
                        item.SubItems.Add(dr100["DESCRIPCION"].ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));

                    else if (dr100["DESCRIPCION"].ToString().Trim() == "No Vinculado")
                        item.SubItems.Add(dr100["DESCRIPCION"].ToString(), Color.Snow, Color.DarkCyan, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));

                    else if (dr100["DESCRIPCION"].ToString().Trim() == "Pago Parcial")
                        item.SubItems.Add(dr100["DESCRIPCION"].ToString(), Color.Snow, Color.DarkOrange, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    /////////////////////

                    item.UseItemStyleForSubItems = false;
                    lvwCtaCte.Items.Add(item);
                }                

                if (fSaldo >= 0) {
                    txtSaldoFinal.BackColor = Color.LightGreen;
                    txtSaldoFinal.Text = "$ " + Math.Round(fSaldo, 2).ToString(); //Resultado
                    txtSaldoDeuda.Text = "$ " + Math.Round(fTotalDeuda + fSaldo, 2).ToString(); //Resultado
                }
                else {
                    txtSaldoFinal.BackColor = Color.LightPink;
                    txtSaldoFinal.Text = "$ " + Math.Round(fSaldo, 2).ToString(); //Resultado
                    txtSaldoDeuda.Text = "$ " + Math.Round(fTotalDeuda - fSaldo, 2).ToString(); //Resultado
                }                

                cm100.Connection.Close();
            }
            catch {  }
        }

        private void LeeCtaCteCliente(int iEstado, int iDeuda)
        {
            try
            {
                //iDeuda = 1 => CtaCte del Cliente
                //iDeuda = 0 => Deuda del Cliente

                //conn.LeeGeneric("SELECT COUNT(IDITEM) as 'CantRegistro' FROM CtaCteCliente", "CtaCteCliente");
                //ArmaDeudaCliente(Convert.ToInt32(conn.leerGeneric["CantRegistro"].ToString()));
                conn.DesconectarBDLeeGeneric();

                SqlCommand cmS = new SqlCommand("SELECT * FROM CtaCteCliente WHERE CtaCteCliente.IDEMPRESA = " + IDEMPRESA + " ORDER BY FECHA, RAZONSOCIAL asc", conectaEstado);

                SqlDataAdapter daS = new SqlDataAdapter(cmS);
                DataTable dtS = new DataTable();
                daS.Fill(dtS);

                this.lvwCtaCte.Items.Clear();     

                foreach (DataRow drS in dtS.Rows)
                {
                    lvwCtaCte.SmallImageList = imageList2;
                    ListViewItem item = new ListViewItem(drS["IDITEM"].ToString().Trim());

                    if (iEstado == 0) //CtaCte del Cliente
                    {
                        lvwCtaCte.Columns[1].Width = 0; //Razon Social
                        lvwCtaCte.Columns[8].Width = 80; //Monto
                        lvwCtaCte.Columns[9].Width = 80; //Estado
                        lvwCtaCte.Columns[6].Width = 80; //Haber
                        lvwCtaCte.Columns[7].Width = 80; //Saldo

                        lblSaldoDeuda.Visible = true;
                        lblSaldoFinal.Visible = true;
                        txtSaldoDeuda.Visible = true;
                        txtSaldoFinal.Visible = true;

                        lblRazonSocial.Visible = true;
                        lblRazonSocialSeleccionada.Visible = true;
                    }

                    if (iEstado == 2) //Deuda del Cliente
                    {
                        lvwCtaCte.Columns[8].Width = 80; //Monto
                        lvwCtaCte.Columns[1].Width = 200; //Razon Social
                        lvwCtaCte.Columns[9].Width = 80; //Estado

                        lvwCtaCte.Columns[6].Width = 80; //Haber
                        lvwCtaCte.Columns[7].Width = 80; //Saldo

                        lblSaldoDeuda.Visible = true;                        
                        lblSaldoFinal.Visible = true;
                        txtSaldoDeuda.Visible = true;
                        txtSaldoFinal.Visible = true;

                        lblRazonSocial.Visible = true;
                        lblRazonSocialSeleccionada.Visible = true;
                    }

                    if (iEstado == 1) //Deuda del Cliente
                    {
                        lvwCtaCte.Columns[8].Width = 80; //Monto
                        lvwCtaCte.Columns[1].Width = 0; //Razon Social
                        lvwCtaCte.Columns[9].Width = 80; //Estado

                        lvwCtaCte.Columns[6].Width = 0; //Haber
                        lvwCtaCte.Columns[7].Width = 0; //Saldo

                        lblSaldoDeuda.Visible = false;
                        lblSaldoFinal.Visible = false;
                        txtSaldoDeuda.Visible = false;
                        txtSaldoFinal.Visible = false;

                        lblRazonSocial.Visible = false;
                        lblRazonSocialSeleccionada.Visible = false;
                    }

                    if (iEstado == 3) //Deuda del Cliente
                    {
                        lvwCtaCte.Columns[8].Width = 80; //Monto
                        lvwCtaCte.Columns[1].Width = 280; //Razon Social
                        lvwCtaCte.Columns[9].Width = 80; //Estado

                        lvwCtaCte.Columns[6].Width = 0; //Haber
                        lvwCtaCte.Columns[7].Width = 0; //Saldo

                        lblSaldoDeuda.Visible = false;
                        lblSaldoFinal.Visible = false;
                        txtSaldoDeuda.Visible = false;
                        txtSaldoFinal.Visible = false;

                        lblRazonSocial.Visible = false;
                        lblRazonSocialSeleccionada.Visible = false;
                    }

                    item.SubItems.Add(drS["RAZONSOCIAL"].ToString().Trim());
                    item.SubItems.Add(drS["FECHA"].ToString().Trim());
                    item.SubItems.Add(drS["NROCOMPROBANTE"].ToString().Trim());
                    item.SubItems.Add(drS["TIPOCOMPROBANTE"].ToString().Trim());

                    if (drS["TIPOCOMPROBANTE"].ToString().Trim() == "FA")
                        item.ImageIndex = 1; //DEBE                    
                    if (drS["TIPOCOMPROBANTE"].ToString().Trim() == "ND")
                        item.ImageIndex = 1; //DEBE

                    if (drS["TIPOCOMPROBANTE"].ToString().Trim() == "RC")
                        item.ImageIndex = 0; //HABER

                    if (drS["TIPOCOMPROBANTE"].ToString().Trim() == "NC")
                        item.ImageIndex = 0; //HABER                    

                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(drS["DEBE"]), 2).ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(drS["HABER"]), 2).ToString(), Color.Empty, Color.LightGray, null);

                    if (Convert.ToDecimal(drS["SALDO"].ToString().Trim()) >= 0)
                    {
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(drS["SALDO"]), 2).ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));

                        txtSaldoFinal.BackColor = Color.LightGreen;
                        txtSaldoFinal.Text = "$ " + Math.Round(Convert.ToDecimal(drS["SALDO"]), 2).ToString(); //Resultado
                    }
                    else
                    {
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(drS["SALDO"]), 2).ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));

                        txtSaldoFinal.BackColor = Color.LightPink;
                        txtSaldoFinal.Text = "$ " + Math.Round(Convert.ToDecimal(drS["SALDO"]), 2).ToString(); //Resultado
                    }                    

                    if (Convert.ToDecimal(drS["MONTOPENDIENTE"].ToString()) >= 0)
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(drS["PENDIENTE"]), 2).ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    else
                        item.SubItems.Add("$ " + Math.Round(Convert.ToDecimal(drS["PENDIENTE"]), 2).ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));



                    //Estados del Movimiento
                    if (drS["DESCRIPCION"].ToString().Trim() == "Pagado")
                        item.SubItems.Add(drS["DESCRIPCION"].ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    else if (drS["DESCRIPCION"].ToString().Trim() == "No Pagado")
                        item.SubItems.Add(drS["DESCRIPCION"].ToString(), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));

                    else if (drS["DESCRIPCION"].ToString().Trim() == "Vinculado")
                        item.SubItems.Add(drS["DESCRIPCION"].ToString(), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));

                    else if (drS["DESCRIPCION"].ToString().Trim() == "No Vinculado")
                        item.SubItems.Add(drS["DESCRIPCION"].ToString(), Color.Snow, Color.DarkCyan, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));

                    else if (drS["DESCRIPCION"].ToString().Trim() == "Pago Parcial")
                        item.SubItems.Add(drS["DESCRIPCION"].ToString(), Color.Snow, Color.DarkOrange, new System.Drawing.Font("Microsoft Sans Serif", 7, System.Drawing.FontStyle.Bold));
                    /////////////////////


                    item.UseItemStyleForSubItems = false;
                    lvwCtaCte.Items.Add(item);

                }
                cmS.Connection.Close();
            }
            catch { }
        }
        

        private void ArmaDeudaCliente(int iCantRegistro)
        {
            try
            {
                string[] sNroComprobante = new string[iCantRegistro]; ;
                decimal[] iIdCliente = new decimal[iCantRegistro]; ;

                //decimal[,] dFacturas = new decimal[iCantRegistro+1, iCantRegistro+1];

                string[] sComprobantes = new string[iCantRegistro];
                decimal[] dMontoPendiente = new decimal[iCantRegistro];

                decimal[] dFactura = new decimal[iCantRegistro];
                decimal[] dRecibo = new decimal[iCantRegistro];

                //decimal[,] dMontoPendiente2 = new decimal[iCantRegistro, iCantRegistro];

                int i = 0;
                int j = 0;
                int k = 0;

                int x = 0;

                int iIndexRecibo = 0;
                int iIndexFactura = 0;
                int iCantFactura = 0;
                int iCantRecibo = 0;

                string Letra="";

                SqlCommand cm = new SqlCommand("SELECT * FROM CtaCteCliente WHERE CtaCteCliente.IDEMPRESA = " + IDEMPRESA + " ORDER BY FECHA asc", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (iCantRegistro > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        iIdCliente[i] = Convert.ToInt32(dr["IDCLIENTE"].ToString().Trim());
                        sNroComprobante[i] = dr["NROCOMPROBANTE"].ToString().Trim();

                        if (dr["TIPOCOMPROBANTE"].ToString().Trim() == "FA")
                        {
                            sComprobantes[i] = "F" + dr["DEBE"].ToString(); //Agrega Facturas          
                            i++;
                        }

                        if (dr["TIPOCOMPROBANTE"].ToString().Trim() == "RC")
                        {
                            sComprobantes[i] = "R" + dr["HABER"].ToString(); //Agreaga los recibos
                            i++;
                        }
                    }

                    j = 0; i = 0; k = 0;
                    int registro;

                    for (registro = 0; registro < iCantRegistro; registro++)
                    {
                        char[] QuitaSimbolo = { 'R', ' ' };
                        char[] QuitaSimbolo2 = { 'F', ' ' };

                        Letra = sComprobantes[registro].Substring(0, 1);

                        if (Letra == "F")
                        {
                            iIndexFactura = registro;
                            dFactura[x] = Decimal.Round(Convert.ToDecimal(sComprobantes[iIndexFactura].TrimStart(QuitaSimbolo2)), 2);
                            x++;
                            iCantFactura++;
                        }

                        if (Letra == "R")
                        {
                            iIndexRecibo = registro;
                            dRecibo[x] = Decimal.Round(Convert.ToDecimal(sComprobantes[iIndexRecibo].TrimStart(QuitaSimbolo)), 2);
                            x++;
                            iCantRecibo++;
                            //ACCION SOBRE RECIBO IMPUTADO

                            //////////////////////////////////////////////////
                        }
                    }

                    x = 0;
                    k = 0;

                    decimal dUltimoSaldo = 0;
                    decimal dSumaFacturas = 0;
                    decimal dSumaRecibos = 0;

                    int iRecibo = 0;
                    decimal dImporteRecibo = 0;
                    decimal iImporteAnteriorRecibo = 0;

                    bool IndicadorDeIndiceDeRecibo = false;
                    bool UltimoReciboVerificado = false;

                    int iCantFacturaXrecibo = 0;
                    int facturasXrecibo = 0;
                    int iCantRegistros = 0;

                    //CONTROLES PRELIMINARES
                    for (x = 0; x < iCantRegistro; x++)
                    {
                        dSumaFacturas = dSumaFacturas + dFactura[x];
                        dSumaRecibos = dSumaRecibos + dRecibo[x];
                    }

                    if (dSumaRecibos == dSumaFacturas) //Deuda Cancelada
                    {
                        for (x = 0; x <= k; x++)
                            dMontoPendiente[x] = 0;
                    }

                    else if (dSumaRecibos == 0)
                    {
                        for (x = 0; x < iCantFactura; x++)
                            dMontoPendiente[x] = -dFactura[x];
                    }
                    /////////////////////////////////////////////////////

                    else
                    {
                        //QUITA LOS REGISTROS DE RECIBOS EN LA MATRIZ FACTURA
                        /*decimal[] dSoloFacturas = new decimal[iCantRegistro];
                        int iRegistroOriginal = 0;
                        int iRegistroAux = 0;
                        
                        for (iRegistroOriginal = 0; iRegistroOriginal < (iCantFactura + iCantRecibo); iRegistroOriginal++)
                        {
                            if (dFactura[iRegistroOriginal] != 0)
                            {
                                dSoloFacturas[iRegistroAux] = dFactura[iRegistroOriginal];
                                iRegistroAux = iRegistroAux + 1;
                            }

                            if (dFactura[iRegistroOriginal] == 0 && (iCantFactura + iCantRecibo) > iRegistroOriginal + 1)
                            {
                                iRegistroOriginal = iRegistroOriginal + 1;
                                dSoloFacturas[iRegistroAux] = dFactura[iRegistroOriginal];
                                iRegistroAux = iRegistroAux + 1;
                            }
                        }    */
                        
                        //////////////////////////////////////////////////////

                        //VARIABLES CANTIDAD DE FACTURA HASTA EL PROXIMO RECIBO
                        int iCantFacturasDesdeReciboSeleccionado = 0;
                        /////////////////////////////////////////////////////

                        //Analiza registro si existe recibo y analizo el primer pago contra facturas//
                        for (x = 0; x < (iCantFactura + iCantRecibo); x++)
                        {
                            iCantFacturasDesdeReciboSeleccionado = 0;

                            if (iCantRecibo == 0)
                                dImporteRecibo = 0;
                            else
                            {
                                for (iRecibo = k; iRecibo < iCantRegistro; iRecibo++)
                                {
                                    if (dFactura[iRecibo] != 0)
                                        iCantFacturasDesdeReciboSeleccionado = iCantFacturasDesdeReciboSeleccionado + 1;

                                    if (dRecibo[iRecibo] != 0 && (iRecibo <= iIndexRecibo) && (UltimoReciboVerificado == false))
                                    {
                                        dImporteRecibo = dRecibo[iRecibo];
                                        IndicadorDeIndiceDeRecibo = true;

                                        //Cantidad de facturas desde el ultimo pago
                                        //iCantFacturaXrecibo = iCantFactura - iCantRecibo;
                                        iCantFacturaXrecibo = iCantFacturasDesdeReciboSeleccionado;
                                        ///////////////////////////////////////////////////////////

                                        if (iCantRecibo > 1)
                                            k = iRecibo + 1;
                                        else
                                        {
                                            k = iCantRegistro - 1;
                                            dImporteRecibo = dRecibo[iRecibo] - iImporteAnteriorRecibo;
                                            iImporteAnteriorRecibo = dRecibo[iRecibo];
                                        }

                                        if (iRecibo == iIndexRecibo)
                                            UltimoReciboVerificado = true;

                                        break;
                                    }
                                    else
                                    {
                                        IndicadorDeIndiceDeRecibo = false;
                                        iCantFacturaXrecibo = 0;
                                    }
                                }
                            }


                            if (iCantRegistros <= iCantFacturaXrecibo)
                            //if (iCantFacturaXrecibo > 0)
                            {
                                for (facturasXrecibo = 0; facturasXrecibo < iCantFacturaXrecibo; facturasXrecibo++)
                                {
                                    if ((x + iCantRegistros) == 0)
                                    {
                                        dUltimoSaldo = (dImporteRecibo - dFactura[iCantRegistros]);
                                        dImporteRecibo = 0;
                                        iCantRegistros++;
                                    }
                                    else
                                    {
                                        if (IndicadorDeIndiceDeRecibo == true && UltimoReciboVerificado == false) //(iRecibo > iIndexRecibo)
                                        {
                                            if (dFactura[iCantRegistros] == 0)
                                                iCantRegistros = iCantRegistros + 1;

                                            dMontoPendiente[iCantRegistros] = (dUltimoSaldo - dFactura[iCantRegistros]); //+ dImporteRecibo;
                                            dUltimoSaldo = dMontoPendiente[iCantRegistros];
                                        }
                                        else
                                        {
                                            if (dFactura[iCantRegistros] == 0)
                                                iCantRegistros = iCantRegistros + 1;

                                            dMontoPendiente[iCantRegistros] = (dFactura[iCantRegistros]) - dImporteRecibo;
                                            dUltimoSaldo = dMontoPendiente[iCantRegistros];
                                            dImporteRecibo = 0;
                                        }

                                        iCantRegistros++;
                                    }
                                }
                            }
                            else
                            {
                                if (((iCantRegistros) < (iCantFactura + iCantRecibo)))
                                {
                                    if (dFactura[iCantRegistros] != 0)
                                    {
                                        dMontoPendiente[iCantRegistros] = -dFactura[iCantRegistros];
                                        iCantRegistros++;
                                    }
                                    else
                                        iCantRegistros++;
                                }
                            }

                            /*else if (iCantFacturaXrecibo == 0)
                            {
                                dMontoPendiente[iCantRegistros] = (dUltimoSaldo - dFactura[iCantRegistros]); //+ dImporteRecibo;
                                dUltimoSaldo = dMontoPendiente[iCantRegistros];
                                iCantRegistros++;
                            }*/


                                /*else if (UltimoReciboVerificado == true)
                                {
                                    dMontoPendiente[iCantRegistros] = dUltimoSaldo + dImporteRecibo;
                                    dUltimoSaldo = dMontoPendiente[iCantRegistros];
                                    dImporteRecibo = 0;
                                    iCantRegistros++;
                                }*/
                                //iCantRegistros++;

                                /*if (dUltimoSaldo >= 0 && x != iCantFactura)
                                     dMontoPendiente[iCantRegistros] = 0;
                                 else
                                     dMontoPendiente[iCantRegistros] = dUltimoSaldo;*/
                        }
                    }

                    for (k = 0; k < iCantRegistro; k++)
                    {
                        string actualizar = "MONTOPENDIENTE = (Cast(replace('" + Math.Round(dMontoPendiente[k], 2) + "', ',', '.') as decimal(10,2)))";
                        conn.ActualizaGeneric("CtaCteCliente", actualizar, " CtaCteCliente.IDEMPRESA = " + IDEMPRESA + " AND NROCOMPROBANTE = '" + sNroComprobante[k] + "'");
                    }
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else
                { MessageBox.Show("No existen registros.", "Vacio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                cm.Connection.Close();                
            }
            catch { MessageBox.Show("Error Inesperado, consultar a soporte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCerrar_Click(object sender, EventArgs e) {
            gpReporteCliente.Visible = false;
            rptClienteCtaCte.Visible = false;
            rptClienteTodaCtaCte.Visible = false;
            //btnVerReporte.Enabled = false;
            this.lvwCtaCte.Items.Clear();
            txtSaldoFinal.BackColor = Color.LightGray;
            txtSaldoFinal.Text = "$ " + "0,00";
            btnVerReporte.Text = "     Reporte";
            lblRazonSocial.Visible = false;
            lblRazonSocialSeleccionada.Visible = false;
            gpFiltroClientes.Visible = false;
        }

        private void ConsultaAnterior()
        {
            try
           {
               if ((lvwCliente.SelectedItems.Count == 0 && lvwCliente.Items.Count != 0) && chkTodaLaCtaCte.Checked == false)
                   MessageBox.Show("Error: No se ha seleccionado ningún cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               else
               {
                   if ((chkTodaLaCtaCte.Checked == false) && (optCtaCte.Checked == true))
                   {
                       btnVerReporte.Enabled = true;
                       //ArmaCtaCte(Convert.ToInt32(lvwCliente.SelectedItems[0].SubItems[0].Text), dtFechaInicial.Text, 0);
                       LeeCtaCteCliente(0, -1);

                       lblRazonSocial.Visible = true;
                       lblRazonSocialSeleccionada.Visible = true;
                       lblRazonSocialSeleccionada.Text = lvwCliente.SelectedItems[0].SubItems[1].Text;
                   }
                   else if (chkTodaLaCtaCte.Checked == true && optCtaCte.Checked == true)
                   {
                       btnVerReporte.Enabled = true;
                       //ArmaCtaCte(-1, dtFechaInicial.Text, 2);
                       LeeCtaCteCliente(2, -1);
                       lblRazonSocial.Visible = false;
                       lblRazonSocialSeleccionada.Visible = false;
                   }

                   else if (chkTodaLaDeuda.Checked == false && optDeudaCliente.Checked == true)
                   {
                       btnVerReporte.Enabled = true;
                       //ArmaCtaCte(Convert.ToInt32(lvwCliente.SelectedItems[0].SubItems[0].Text), dtFechaInicial.Text, 1);
                       LeeCtaCteCliente(1, 0);

                       lblRazonSocial.Visible = true;
                       lblRazonSocialSeleccionada.Visible = true;
                       lblRazonSocialSeleccionada.Text = lvwCliente.SelectedItems[0].SubItems[1].Text;
                   }

                   else if (chkTodaLaDeuda.Checked == true && optDeudaCliente.Checked == true)
                   {
                       btnVerReporte.Enabled = true;
                       //ArmaCtaCte(-1, dtFechaInicial.Text, 3);
                       LeeCtaCteCliente(3, 0);

                       lblRazonSocial.Visible = false;
                       lblRazonSocialSeleccionada.Visible = false;
                       lblRazonSocialSeleccionada.Text = lvwCliente.SelectedItems[0].SubItems[1].Text;
                   }
               }
           }
           catch { }
        }       

        private void fillByToolStripButton_Click(object sender, EventArgs e) {
            try {
                this.ctaCteClienteTableAdapter.FillBy(this.DGestionDTGeneral.CtaCteCliente);
            }
            catch (System.Exception ex) {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void optCtaCte_CheckedChanged(object sender, EventArgs e) 
        {
            if (optCtaCte.Checked == true)
            {
                chkTodaLaCtaCte.Checked = false;
                chkTodaLaDeuda.Checked = false;
            }       
        }

        private void optDeudaCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (optDeudaCliente.Checked == true)
            {
                chkTodaLaCtaCte.Checked = false;
                chkTodaLaDeuda.Checked = false;
            }  
        }

        private void chkTodaLaCtaCte_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodaLaCtaCte.Checked == true)
            {
                chkTodaLaDeuda.Checked = false;
                optCtaCte.Checked = true;
                chkTodaLaCtaCte.Checked = true;
            }
        }

        private void chkTodaLaDeuda_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodaLaDeuda.Checked == true)
            {
                chkTodaLaCtaCte.Checked = false;
                optDeudaCliente.Checked = true;
                chkTodaLaDeuda.Checked = true;
            }
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboBuscaCliente.SelectedIndex == 0)
                {
                    BuscarDatos2(); //Razon Social
                }

                else if (cboBuscaCliente.SelectedIndex == 1)
                {
                    BuscarDatos3(); //CUIT
                }

                else if (cboBuscaCliente.SelectedIndex == 2)
                {
                    BuscarDatos1(); //CodCliente
                }
            }
            catch { }
        }

        /// ///////////////////////////////////////////////////BLOQUE DE BUSQUEDA//////////////////////////////////////////////////////////////
        public void BuscarDatos1()
        {
            try
            {
                lvwCliente.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT IdCliente, RAZONSOCIAL, LOCALIDAD, TELEFONOSCOMERCIALES, NUMDECUIT FROM Cliente WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.NROCENTRO='" + sPtoVta.Trim() +"' AND IdCliente LIKE '" + txtBuscarCliente.Text.Trim() + "%'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwCliente.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IdCliente"].ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString());
                    item.SubItems.Add(dr["LOCALIDAD"].ToString());
                    item.SubItems.Add(dr["TELEFONOSCOMERCIALES"].ToString());
                    item.SubItems.Add(dr["NUMDECUIT"].ToString());

                    item.ImageIndex = 0;

                    lvwCliente.Items.Add(item);
                }

                cm.Connection.Close();
            }
            catch { }
        }

        public void BuscarDatos2()
        {
            try
            {
                lvwCliente.Items.Clear();

                if ((cboBuscaCliente.SelectedIndex == 0 && txtBuscarCliente.Text == "") || txtBuscarCliente.Text == "*")
                {
                    if (txtBuscarCliente.Text == "*")
                        txtBuscarCliente.Text = "";

                    SqlCommand cm = new SqlCommand("SELECT IdCliente, RAZONSOCIAL, LOCALIDAD, TELEFONOSCOMERCIALES, NUMDECUIT FROM Cliente WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.NROCENTRO='" + sPtoVta.Trim() + "' AND RAZONSOCIAL LIKE '" + txtBuscarCliente.Text + "%' ORDER BY RAZONSOCIAL", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwCliente.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["IdCliente"].ToString());
                        item.SubItems.Add(dr["RAZONSOCIAL"].ToString());
                        item.SubItems.Add(dr["LOCALIDAD"].ToString());
                        item.SubItems.Add(dr["TELEFONOSCOMERCIALES"].ToString());
                        item.SubItems.Add(dr["NUMDECUIT"].ToString());
                        item.ImageIndex = 0;
                        lvwCliente.Items.Add(item);
                    }
                    cm.Connection.Close();
                }

                else if (cboBuscaCliente.SelectedIndex == 0 && txtBuscarCliente.Text != "")
                {
                    SqlCommand cm = new SqlCommand("SELECT IdCliente, RAZONSOCIAL, LOCALIDAD, TELEFONOSCOMERCIALES, NUMDECUIT FROM Cliente WHERE Cliente.IDEMPRESA = " + IDEMPRESA + " AND Cliente.NROCENTRO='" + sPtoVta.Trim() + "' AND RAZONSOCIAL LIKE '" + txtBuscarCliente.Text + "%' ORDER BY RAZONSOCIAL", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwCliente.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["IdCliente"].ToString());
                        item.SubItems.Add(dr["RAZONSOCIAL"].ToString());
                        item.SubItems.Add(dr["LOCALIDAD"].ToString());
                        item.SubItems.Add(dr["TELEFONOSCOMERCIALES"].ToString());
                        item.SubItems.Add(dr["NUMDECUIT"].ToString());
                        item.ImageIndex = 0;
                        lvwCliente.Items.Add(item);
                    }
                    cm.Connection.Close();
                }
            }
            catch { }
        }

        public void BuscarDatos3()
        {
            try
            {
                lvwCliente.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT IdCliente, RAZONSOCIAL, LOCALIDAD, TELEFONOSCOMERCIALES, NUMDECUIT FROM Cliente WHERE Cliente.IDEMPRESA = " + IDEMPRESA + "  AND Cliente.NROCENTRO='" + sPtoVta.Trim() + "' AND NUMDECUIT LIKE '" + txtBuscarCliente.Text.Trim() + "%'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwCliente.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IdCliente"].ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString());
                    item.SubItems.Add(dr["LOCALIDAD"].ToString());
                    item.SubItems.Add(dr["TELEFONOSCOMERCIALES"].ToString());
                    item.SubItems.Add(dr["NUMDECUIT"].ToString());

                    item.ImageIndex = 0;

                    lvwCliente.Items.Add(item);
                }

                cm.Connection.Close();
            }
            catch { }
        }

        /////////////////////////////////////////////////////FIN BLOQUE DE BUSQUEDA///////////////////////////////////////////////////////////////// 

    }
}
