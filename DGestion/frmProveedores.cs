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
    public partial class frmProveedores : Form
    {
        public delegate void pasarProvee1(int CodProvee);
        public event pasarProvee1 pasadoProvee1;
        public delegate void pasarProvee2(string RSprovee);
        public event pasarProvee2 pasadoProvee2;
        
        public frmProveedores()
        {
            InitializeComponent();
        }
        
        ProveedorBD conn = new ProveedorBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#"); string codArt;

        EmpresaBD connEmpresa = new EmpresaBD();

        int IDEMPRESA;

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

        private void frmProveedores_Load(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            lvwProveedores.Height = 420;

            conn.ConectarBD();
            IDEMPRESA = ConsultaEmpresa(); //Lee Empresa
            MostrarDatos2();
            FormatoListView();

            cboBuscar.SelectedIndex = 0;
        }

        private void FormatoListView()
        {
            lvwProveedores.View = View.Details;
            lvwProveedores.LabelEdit = false;
            lvwProveedores.AllowColumnReorder = false;
            lvwProveedores.FullRowSelect = true;
            lvwProveedores.GridLines = true;
        }

        public void MostrarDatos2()
        {
            try
            {
                lvwProveedores.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT IdProveedor AS 'Código', RAZONSOCIAL AS 'Razón Social', Domicilio, NRODECUIT FROM Proveedor WHERE IDEMPRESA = " + IDEMPRESA + " ORDER BY RazonSocial", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwProveedores.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Código"].ToString());
                    item.SubItems.Add(dr["Razón Social"].ToString());
                    item.SubItems.Add(dr["Domicilio"].ToString());
                    item.SubItems.Add(dr["NRODECUIT"].ToString());
                                        
                    item.ImageIndex = 0;

                    lvwProveedores.Items.Add(item);
                }
                codArt = lvwProveedores.SelectedItems[0].SubItems[0].Text;
                cm.Connection.Close();
            }
            catch { }
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void gridProveedores_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
           
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            this.lvwProveedores.Height = 240;

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;

            Limpieza();
        }

        private void btcCerrar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            lvwProveedores.Height = 420;

            tsBtnNuevo.Enabled = true;
            tsBtnModificar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void txtCodTipoIva_TextChanged(object sender, EventArgs e) {
            try {
                if (this.txtCodTipoIva.Text.Trim() != "") {
                    conn.ConsultaTipoIvaProv("Select * FROM TipoIva WHERE IdTipoIva = " + txtCodTipoIva.Text + "", "TipoIva");

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

        private void txtCodRubro_TextChanged(object sender, EventArgs e) {
            try {
                if (this.txtCodRubro.Text.Trim() != "") {
                    conn.ConsultaTipoIvaProv("Select * FROM Rubro WHERE IdRubro = " + txtCodRubro.Text + "", "Rubro");

                    this.cboRubro.DataSource = conn.ds.Tables[0];
                    this.cboRubro.ValueMember = "IDRUBRO";
                    this.cboRubro.DisplayMember = "DESCRIPCION";
                }
                else
                    cboRubro.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboRubro.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void tsBtnModificar_Click(object sender, EventArgs e)  {
            gpoCliente.Visible = true;
            this.lvwProveedores.Height = 240;

            btnEliminar.Enabled = true;
            tsBtnModificar.Enabled = false;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e)  {            
            try
            {
                if (cboBuscar.SelectedIndex == 0)
                {
                    BuscarDatos1();
                }

                else if (cboBuscar.SelectedIndex == 1)
                {
                    BuscarDatos2();
                }
            }
            catch { }      
        }

        /// ///////////////////////////////////////////////////BLOQUE DE BUSQUEDA//////////////////////////////////////////////////////////////
        public void BuscarDatos1()
        {
            try
            {
                lvwProveedores.Items.Clear();
                SqlCommand cm = new SqlCommand("SELECT IdProveedor, RAZONSOCIAL, Domicilio, NRODECUIT FROM Proveedor WHERE IDEMPRESA = " + IDEMPRESA + " AND IdProveedor LIKE '" + txtConsultaProvee.Text.Trim() + "%'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwProveedores.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["IdProveedor"].ToString());
                    item.SubItems.Add(dr["RAZONSOCIAL"].ToString());
                    item.SubItems.Add(dr["Domicilio"].ToString());
                    item.SubItems.Add(dr["NRODECUIT"].ToString());

                    item.ImageIndex = 0;

                    lvwProveedores.Items.Add(item);
                }

                cm.Connection.Close();
            }
            catch { }
        }

        public void BuscarDatos2()
        {
            try
            {
                lvwProveedores.Items.Clear();

                if ((cboBuscar.SelectedIndex == 1 && txtConsultaProvee.Text == "") || txtConsultaProvee.Text == "*")
                {
                    if (txtConsultaProvee.Text == "*")
                        txtConsultaProvee.Text = "";

                    SqlCommand cm = new SqlCommand("SELECT IdProveedor, RAZONSOCIAL, Domicilio, NRODECUIT FROM Proveedor WHERE IDEMPRESA = " + IDEMPRESA + " AND RAZONSOCIAL LIKE '" + txtConsultaProvee.Text + "%' ORDER BY RAZONSOCIAL", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwProveedores.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["IdProveedor"].ToString());
                        item.SubItems.Add(dr["RAZONSOCIAL"].ToString());
                        item.SubItems.Add(dr["Domicilio"].ToString());
                        item.SubItems.Add(dr["NRODECUIT"].ToString());
                        item.ImageIndex = 0;
                        lvwProveedores.Items.Add(item);
                    }
                    cm.Connection.Close();
                }

                else if (cboBuscar.SelectedIndex == 1 && txtConsultaProvee.Text != "")
                {
                    SqlCommand cm = new SqlCommand("SELECT IdProveedor, RAZONSOCIAL, Domicilio, NRODECUIT FROM Proveedor WHERE IDEMPRESA = " + IDEMPRESA + " AND RAZONSOCIAL LIKE '" + txtConsultaProvee.Text + "%' ORDER BY RAZONSOCIAL", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwProveedores.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["IdProveedor"].ToString());
                        item.SubItems.Add(dr["RAZONSOCIAL"].ToString());
                        item.SubItems.Add(dr["Domicilio"].ToString());
                        item.SubItems.Add(dr["NRODECUIT"].ToString());
                        item.ImageIndex = 0;
                        lvwProveedores.Items.Add(item);
                    }                    
                    cm.Connection.Close();
                }
            }
            catch { }
        }
        /////////////////////////////////////////////////////FIN BLOQUE DE BUSQUEDA///////////////////////////////////////////////////////////////// 

        private void btnGuardar_Click(object sender, EventArgs e) {
            try {
                
               // lvwProveedores.Height = 420;

                tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;
                btnModificar.Enabled = false;

                // int IdRubro; int IdTipoIva;

                if (txtRazonSocial.Text.Trim() == "")
                    txtRazonSocial.Text = "-";
                if (txtDirComercial.Text.Trim() == "")
                    txtDirComercial.Text = "-";
                if (txtTelefonos.Text.Trim() == "")
                    txtTelefonos.Text = "-";

                if (txtCodTipoIva.Text.Trim() == "")
                    txtCodTipoIva.Text = "null";
                if (cboTipoIva.Text.Trim() == "")
                    cboTipoIva.Text = "-";

                if (txtNroCuit.Text.Trim() == "")
                    txtNroCuit.Text = "-";
                if (txtPersonaContact.Text.Trim() == "")
                    txtPersonaContact.Text = "-";

                if (txtCodRubro.Text.Trim() == "")
                    txtCodRubro.Text = "null";
                if (cboRubro.Text.Trim() == "")
                    cboRubro.Text = "-";

                if (txtObservaciones.Text.Trim() == "")
                    txtObservaciones.Text = "-";

                //if ((int.TryParse(txtCodRubro.Text, out IdRubro)) && (int.TryParse(txtCodTipoIva.Text, out IdTipoIva))) {
                string agregar = "INSERT INTO Proveedor (RAZONSOCIAL, DOMICILIO, TELEFONOS, CONTACTO, IDRUBRO, IDTIPOIVA, NRODECUIT, OBSERVACIONES, IDEMPRESA) VALUES ('" + txtRazonSocial.Text.Trim() + "', '" + txtDirComercial.Text.Trim() + "', '" + txtTelefonos.Text.Trim() + "', '" + txtPersonaContact.Text.Trim() + "', " + txtCodRubro.Text.Trim() + ", " + txtCodTipoIva.Text.Trim() + ", '" + txtNroCuit.Text.Trim() + "', '" + txtObservaciones.Text.Trim() + "', " + IDEMPRESA + ")";

                if (conn.InsertarProveedor(agregar)) {
                    MostrarDatos2();
                    MessageBox.Show("Datos Agregados");
                }
            }            
            catch { }     
        }

        private void btnModificar_Click(object sender, EventArgs e) {
            try {
                
                lvwProveedores.Height = 240;

                btnEliminar.Enabled = false;
                tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;

                if (txtRazonSocial.Text.Trim() == "")
                    txtRazonSocial.Text = "-";
                if (txtDirComercial.Text.Trim() == "")
                    txtDirComercial.Text = "-";
                if (txtTelefonos.Text.Trim() == "")
                    txtTelefonos.Text = "-";

                if (txtCodTipoIva.Text.Trim() == "")
                    txtCodTipoIva.Text = "null";
                if (cboTipoIva.Text.Trim() == "")
                    cboTipoIva.Text = "-";

                if (txtNroCuit.Text.Trim() == "")
                    txtNroCuit.Text = "-";
                if (txtPersonaContact.Text.Trim() == "")
                    txtPersonaContact.Text = "-";

                if (txtCodRubro.Text.Trim() == "")
                    txtCodRubro.Text = "null";
                if (cboRubro.Text.Trim() == "")
                    cboRubro.Text = "-";

                if (txtObservaciones.Text.Trim() == "")
                    txtObservaciones.Text = "-";

                string actualizar = "RAZONSOCIAL='" + txtRazonSocial.Text.Trim() + "', DOMICILIO='" + txtDirComercial.Text.Trim() + "', TELEFONOS ='" + txtTelefonos.Text.Trim() + "', CONTACTO='" + txtPersonaContact.Text.Trim() + "', IDRUBRO= " + txtCodRubro.Text.Trim() + ", IDTIPOIVA =" + txtCodTipoIva.Text.Trim() + ",  NRODECUIT='" + txtNroCuit.Text.Trim() + "', OBSERVACIONES='" + txtObservaciones.Text.Trim() + "'";

                if (conn.ActualizaProveedor("Proveedor", actualizar, " IDEMPRESA = " + IDEMPRESA + " AND IdProveedor = " + this.txtCodProvee.Text.Trim() + "")) {
                    MostrarDatos2();
                    //gpoCliente.Visible = false;
                    //lvwProveedores.Height = 420;                   
                    MessageBox.Show("Datos Actualizados");
                }
            }
            catch {
                gpoCliente.Visible = true;
                lvwProveedores.Height = 240;
                MessageBox.Show("Error: Al Actualizar datos");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e) {
            try {
                btnEliminar.Enabled = true;
                tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;
                btnModificar.Enabled = false;
                btnGuardar.Enabled = false;

                if (conn.EliminarProveedor("Proveedor", " IdProveedor = " + this.txtCodProvee.Text.Trim())) {
                    MostrarDatos2();
                    //gpoCliente.Visible = false;
                    //lvwProveedores.Height = 420;
                    tsBtnNuevo.Enabled = true;
                    tsBtnModificar.Enabled = true;
                    //btnEliminar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnGuardar.Enabled = false;
                    MessageBox.Show("Datos Eliminados");

                    Limpieza();

                }
                else
                    MessageBox.Show("Error al Eliminar");
            }
            catch { }
        }

        private void Limpieza() {
            txtCodProvee.Text = "Automático"; txtRazonSocial.Text = "";
            txtDirComercial.Text = ""; txtTelefonos.Text = "";
            txtCodTipoIva.Text = ""; cboTipoIva.Text = "";
            txtNroCuit.Text = ""; txtPersonaContact.Text = "";
            txtCodRubro.Text = ""; cboRubro.Text = "";
            txtObservaciones.Text = "";
        }

        private void btnTipoIva_Click(object sender, EventArgs e) {
            frmTipoIva formTipoIva = new frmTipoIva();
            formTipoIva.pasarTipoIvaCod1 += new frmTipoIva.pasarTipoIvaCod(CodTipoIva);  //Delegado1 Familia Articulo
            formTipoIva.pasarTipoIvaDesc1 += new frmTipoIva.pasarTipoIvaDesc(DescTipoIva); //Delegado2 Familia Articulo
            formTipoIva.ShowDialog();
        }

        //Metodos de delegado TipoIva
        public void CodTipoIva(int dato1) {
            this.txtCodTipoIva.Text = dato1.ToString();
        }

        public void DescTipoIva(string dato2) {
            this.cboTipoIva.Text = dato2.ToString();
        }
        //////////////////////////////////

        private void btnRubro_Click(object sender, EventArgs e) {
            frmRubroArticulo formRubro = new frmRubroArticulo();
            formRubro.pasado11 += new frmRubroArticulo.pasar11(CodRubro);  //Delegado1 Familia Articulo
            formRubro.pasado22 += new frmRubroArticulo.pasar22(DescripcionRubro); //Delegado2 Familia Articulo
            formRubro.ShowDialog();
        }

        //Metodos de delegado Rubro Proveedor
        public void CodRubro(int dato1) {
            this.txtCodRubro.Text = dato1.ToString();
        }

        public void DescripcionRubro(string dato2) {
            this.cboRubro.Text = dato2.ToString();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvwProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CodProveedor;
                CodProveedor = lvwProveedores.SelectedItems[0].SubItems[0].Text;

                conn.LeeProveedor("SELECT * FROM Proveedor WHERE IDEMPRESA = " + IDEMPRESA + " AND IdProveedor = " + Convert.ToInt32(lvwProveedores.SelectedItems[0].SubItems[0].Text) + "", "Proveedor");

                this.txtCodProvee.Text = conn.leerProv["IDPROVEEDOR"].ToString();
                this.txtRazonSocial.Text = conn.leerProv["RAZONSOCIAL"].ToString();
                this.txtDirComercial.Text = conn.leerProv["DOMICILIO"].ToString();
                this.txtTelefonos.Text = conn.leerProv["TELEFONOS"].ToString();
                this.txtPersonaContact.Text = conn.leerProv["CONTACTO"].ToString();

                this.txtCodRubro.Text = conn.leerProv["IDRUBRO"].ToString();
                if (this.txtCodRubro.Text.Trim() == "")
                    this.cboRubro.Text = "";

                this.txtCodTipoIva.Text = conn.leerProv["IDTIPOIVA"].ToString();
                if (this.txtCodTipoIva.Text.Trim() == "")
                    this.cboTipoIva.Text = "";

                this.txtNroCuit.Text = conn.leerProv["NRODECUIT"].ToString();
                this.txtObservaciones.Text = conn.leerProv["OBSERVACIONES"].ToString();

                conn.DesconectarBDLeeProv();
            }
            catch { }
        }

        private void lvwProveedores_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RecargaDatos();  
        }

        private void RecargaDatos()
        {
            try
            {
                pasadoProvee1(Int16.Parse(this.txtCodProvee.Text));  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                pasadoProvee2(this.txtRazonSocial.Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch { }
        }

        private void lvwProveedores_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                RecargaDatos();
            }
        }        

        //////////////////////////////////

    }
}
