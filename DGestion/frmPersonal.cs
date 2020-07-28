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
    public partial class frmPersonal : Form
    {
        public delegate void pasarPersona1(int CodPPerso);
        public event pasarPersona1 pasadoPerso1;
        public delegate void pasarPersona2(string RSPerso);
        public event pasarPersona2 pasadoPerso2;

        public frmPersonal() {
            InitializeComponent();
        }

        CGenericBD conn = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private string FormateoFechaNacimiento()
        {
            DateTimePicker dtr = new DateTimePicker();
            dtr.Value = dtpFechaNacimiento.Value;
            return String.Format("{0:d/M/yyyy HH:mm:ss}", dtr.Value);
        }

        private string FormateoFechaNacimientoConyuge()
        {
            DateTimePicker dtr = new DateTimePicker();
            dtr.Value = dtpFechaNacConyuge.Value;
            return String.Format("{0:d/M/yyyy HH:mm:ss}", dtr.Value);
        }

        private string FormateoFechaNacimientoCartillaSanitaria()
        {
            DateTimePicker dtr = new DateTimePicker();
            dtr.Value = dtpFechaVencimientoCartilla.Value;
            return String.Format("{0:d/M/yyyy HH:mm:ss}", dtr.Value);
        }        

        private void frmPersonal_Load(object sender, EventArgs e) {           
            try
            {
                gpoCliente.Visible = false;
                lvwPersonal.Height = 500;
                conn.ConectarBD(); //LlenarComboBuscar();
                cboBuscaPersonal.SelectedIndex = 0; cboBuscaPersonal.SelectedIndex = 0;

                dtpFechaNacimiento.Value = DateTime.Today;
                dtpFechaNacConyuge.Value = DateTime.Today;
                dtpFechaVencimientoCartilla.Value = DateTime.Today;
                
                FormatoListView();
                MostrarDatos();
            }
            catch { }
        }

        private void FormatoListView()
        {
            lvwPersonal.View = View.Details;
            lvwPersonal.LabelEdit = false;
            lvwPersonal.AllowColumnReorder = false;
            lvwPersonal.FullRowSelect = true;
            lvwPersonal.GridLines = true;
        }

        public void MostrarDatos() {
            try {

                lvwPersonal.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT IDPERSONAL as 'Código Persona', Legajo, DNI,  NOMBREYAPELLIDO as 'Nombre y Apellido', DOMICILIO as 'Domicilio', TELFIJO as 'Teléfono Fijo', FECHANACIMIENTO as 'Fecha Nacimiento', IdEstado FROM Personal", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows) {
                    lvwPersonal.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Código Persona"].ToString());
                    item.SubItems.Add(dr["Legajo"].ToString());
                    item.SubItems.Add(dr["DNI"].ToString());
                    item.SubItems.Add(dr["Nombre y Apellido"].ToString());
                    item.SubItems.Add(dr["Domicilio"].ToString());
                    item.SubItems.Add(dr["Teléfono Fijo"].ToString());
                    item.SubItems.Add(dr["Fecha Nacimiento"].ToString());
                                        
                    if (dr["IdEstado"].ToString() == "10")
                    {
                        item.SubItems.Add("Activo");
                        item.ImageIndex = 0;
                    }
                    else
                    {
                        item.SubItems.Add("Inactivo");
                        item.ImageIndex = 2;
                    }

                    lvwPersonal.Items.Add(item);
                }
                cm.Connection.Close();               
            }
            catch { }
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            lvwPersonal.Height = 255;

            btnBajaPersonal.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btcCerrar_Click(object sender, EventArgs e)  {
            gpoCliente.Visible = false;
            lvwPersonal.Height = 500;

            tsBtnNuevo.Enabled = true;
            btnBajaPersonal.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private void btnEstadoCivil_Click(object sender, EventArgs e) {
            frmEstadoCivil formEstadoCiv = new frmEstadoCivil();
            formEstadoCiv.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnTPerso_Click(object sender, EventArgs e) {
            frmTipoPersonal formTipoPersonal = new frmTipoPersonal();
            formTipoPersonal.ShowDialog();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                gpoCliente.Visible = true;
                lvwPersonal.Height = 255;

                tsBtnNuevo.Enabled = true;
                btnBajaPersonal.Enabled = true;
                btnGuardar.Enabled = true;

                if (txtCodEstadoCivil.Text.Trim() == "")
                    txtCodEstadoCivil.Text = "null";
                if (txtCodTipoPersonal.Text.Trim() == "")
                    txtCodTipoPersonal.Text = "null";
                if (txtNumCartillaSanitaria.Text.Trim() == "")
                    txtNumCartillaSanitaria.Text = "0";
   

                if (txtNombreApellido.Text.Trim() != "" && txtDNI.Text.Trim() != "")
                {
                    string agregar = "INSERT INTO Personal VALUES('" + txtDNI.Text + "', '" + txtLegajo.Text + "', '" + txtNombreApellido.Text + "', '" + txtDomicilio.Text + "', '" + txtTelFijo.Text + "','" + txtTelCelular.Text + "', '" + FormateoFechaNacimiento() + "', " + txtCodEstadoCivil.Text + ", " + txtNumCartillaSanitaria.Text + ", '" + FormateoFechaNacimientoCartillaSanitaria() + "', '" + txtNombreConyuge.Text + "', '" + FormateoFechaNacimientoConyuge() + "', " + txtCodTipoPersonal.Text + ", '" + txtObservaciones.Text + "','','','false')";

                    if (conn.InsertarGeneric(agregar))
                    {
                        MostrarDatos();
                        MessageBox.Show("Datos Agregados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("El campo Descripcion o DNI están vacio", "Adventencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                gpoCliente.Visible = true;
                lvwPersonal.Height = 255;

                btnBajaPersonal.Enabled = false;
                tsBtnNuevo.Enabled = true;

                if (txtCodEstadoCivil.Text.Trim() == "")
                    txtCodEstadoCivil.Text = "null";
                if (txtCodTipoPersonal.Text.Trim() == "")
                    txtCodTipoPersonal.Text = "null";
                if (txtNumCartillaSanitaria.Text.Trim() == "")
                    txtNumCartillaSanitaria.Text = "0";

                string actualizar = "DNI = '" + txtDNI.Text + "', LEGAJO = '" + txtLegajo.Text + "', NOMBREYAPELLIDO = '" + txtNombreApellido.Text + "', DOMICILIO = '" + txtDomicilio.Text + "', TELFIJO = '" + txtTelFijo.Text + "', TELCELULAR = '" + txtTelCelular.Text + "', FECHANACIMIENTO = '" + FormateoFechaNacimiento() + "', IDESTADOCIVIL = " + txtCodEstadoCivil.Text + ", NRODECARTILLASANITARIA = " + txtNumCartillaSanitaria.Text + ", VENCIMIENTO = '" + FormateoFechaNacimientoCartillaSanitaria() + "', NOMBRECONYUGE = '" + txtNombreConyuge.Text + "', FECHANACCONYUGE = '" + FormateoFechaNacimientoConyuge() + "', IDTIPOPERSONAL = " + txtCodTipoPersonal.Text + ", OBSERVACIONES = '" + txtObservaciones.Text + "'";

                if (conn.ActualizaGeneric("Personal", actualizar, " IdPersonal = " + Convert.ToInt32(lvwPersonal.SelectedItems[0].SubItems[0].Text)))
                {
                    MostrarDatos();
                    MessageBox.Show("Datos Actualizados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Actualizar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { conn.DesconectarBDLeeGeneric(); }
        }

        private void tsBtnModificar_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = true;
            lvwPersonal.Height = 255;

            btnBajaPersonal.Enabled = true;            
            tsBtnModificar.Enabled = false;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
        }

        private void lvwPersonal_SelectedIndexChanged(object sender, EventArgs e) {            
            try
            {
                if (lvwPersonal.Items.Count != 0) {
                Limpieza();
                conn.LeeGeneric("SELECT * FROM Personal WHERE IdPersonal = " + Convert.ToInt32(lvwPersonal.SelectedItems[0].SubItems[0].Text) + "", "Personal");

                this.txtLegajo.Text = conn.leerGeneric["Legajo"].ToString();
                this.txtDNI.Text = conn.leerGeneric["DNI"].ToString();
                this.txtNombreApellido.Text = conn.leerGeneric["NOMBREYAPELLIDO"].ToString();
                this.txtDomicilio.Text = conn.leerGeneric["DOMICILIO"].ToString();
                this.txtTelCelular.Text = conn.leerGeneric["TELFIJO"].ToString();
                this.txtTelFijo.Text = conn.leerGeneric["TELCELULAR"].ToString();

                if (conn.leerGeneric["IDESTADO"].ToString() == "11")  {
                    btnModificar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnBajaPersonal.Enabled = false;
                    tsBtnModificar.Enabled = false;
                    tsBtnNuevo.Enabled = false;
                }
                else  {
                    btnModificar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnBajaPersonal.Enabled = true;
                    tsBtnModificar.Enabled = true;
                    tsBtnNuevo.Enabled = true;
                }

                this.dtpFechaNacimiento.Value =  Convert.ToDateTime(conn.leerGeneric["FECHANACIMIENTO"].ToString());
                this.txtCodEstadoCivil.Text = conn.leerGeneric["IDESTADOCIVIL"].ToString();
                this.txtNumCartillaSanitaria.Text = conn.leerGeneric["NRODECARTILLASANITARIA"].ToString();
                this.dtpFechaVencimientoCartilla.Value = Convert.ToDateTime(conn.leerGeneric["VENCIMIENTO"].ToString());
                this.txtNombreConyuge.Text = conn.leerGeneric["NOMBRECONYUGE"].ToString();
                this.dtpFechaNacConyuge.Value =  Convert.ToDateTime(conn.leerGeneric["FECHANACCONYUGE"].ToString());
                this.txtCodTipoPersonal.Text = conn.leerGeneric["IDTIPOPERSONAL"].ToString();
                this.txtObservaciones.Text = conn.leerGeneric["OBSERVACIONES"].ToString();                

                conn.DesconectarBDLeeGeneric();
                }
                else { this.txtLegajo.Text = "-"; this.txtDomicilio.Text = cboBuscaPersonal.Text.Trim(); this.txtTelCelular.Text = "-"; }
            }
            catch { conn.DesconectarBDLeeGeneric(); }
        }

        private void lvwPersonal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RecargaDatos();
        }

        private void RecargaDatos()
        {
            try
            {
                pasadoPerso1(Convert.ToInt32(lvwPersonal.SelectedItems[0].SubItems[0].Text));  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                pasadoPerso2(this.txtNombreApellido.Text);  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                this.Close();
            }
            catch { }
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void txtCodEstadoCivil_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodEstadoCivil.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("Select * FROM EstadoCivil WHERE IdEstadoCivil = " + Convert.ToInt32(this.txtCodEstadoCivil.Text) + "", "EstadoCivil");

                    this.cboEstadoCivil.DataSource = conn.ds.Tables[0];
                    this.cboEstadoCivil.ValueMember = "IdEstadoCivil";
                    this.cboEstadoCivil.DisplayMember = "Descripcion";
                }
                else
                    cboEstadoCivil.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboEstadoCivil.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void Limpieza()
        {
            this.txtLegajo.Text = "";
            this.txtDNI.Text = "";
            this.txtNombreApellido.Text = "";
            this.txtDomicilio.Text = "";
            this.txtTelCelular.Text = "";
            this.txtTelFijo.Text = "";

            this.dtpFechaNacimiento.Value = DateTime.Today;
            this.txtCodEstadoCivil.Text = "";
            this.txtNumCartillaSanitaria.Text = "";
            this.dtpFechaVencimientoCartilla.Value = DateTime.Today;
            this.txtNombreConyuge.Text = "";
            this.dtpFechaNacConyuge.Value = DateTime.Today;
            this.txtCodTipoPersonal.Text = "";
            this.txtObservaciones.Text = "";        
        }

        private void txtCodTipoPersonal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodTipoPersonal.Text.Trim() != "")
                {
                    conn.ConsultaGeneric("Select * FROM TipoPersonal WHERE IdTipoPersonal = " + txtCodTipoPersonal.Text + "", "TipoPersonal");

                    this.cboPersonal.DataSource = conn.ds.Tables[0];
                    this.cboPersonal.ValueMember = "IdTipoPersonal";
                    this.cboPersonal.DisplayMember = "Descripcion";
                }
                else
                    cboPersonal.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboPersonal.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void btnBajaPersonal_Click(object sender, EventArgs e)
        {
            try
            {
                string actualizar = "IdEstado = '11'" ;


                if (conn.ActualizaGeneric("Personal", actualizar, "IdPersonal = " + Convert.ToInt32(lvwPersonal.SelectedItems[0].SubItems[0].Text) + ""))
                {
                    MostrarDatos();
                    MessageBox.Show("Situación persona actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Actualizar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
  

                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();
            }
            catch { conn.DesconectarBDLeeGeneric(); conn.DesconectarBD(); }


        }

        private void lvwPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                RecargaDatos();
            }
        }

        private void cboBuscaPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtBuscarArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtNombreApellido_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtDomicilio_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtTelFijo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtTelCelular_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dtpFechaNacimiento_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCodEstadoCivil_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cboEstadoCivil_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnEstadoCivil_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtNumCartillaSanitaria_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dtpFechaVencimientoCartilla_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtNombreConyuge_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dtpFechaNacConyuge_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCodTipoPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cboPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnTPerso_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtObservaciones_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnGuardar_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
