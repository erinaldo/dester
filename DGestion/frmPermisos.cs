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
    public partial class frmPermisos : Form
    {

        CGenericBD connGeneric = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        public frmPermisos()
        {
            InitializeComponent();
        }

        private void btcCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPermisos_Load(object sender, EventArgs e)
        {            
            MostrarDatos();
        }

        private void MostrarDatos()
        {
            try
            {
                cboModuloControl.Items.Clear();
                cboProcedimientoControl.Items.Clear();

                connGeneric.ConsultaGeneric("SELECT * FROM PermisoModulo", "PermisoModulo");
                this.cboModuloControl.DataSource = connGeneric.ds.Tables[0];
                this.cboModuloControl.ValueMember = "IdModulo";
                this.cboModuloControl.DisplayMember = "Modulo";

                connGeneric.DesconectarBD();

                connGeneric.ConsultaGeneric("SELECT PermisoControl.IdControl, PermisoControl.Control FROM PermisoModulo, PermisoControl WHERE PermisoModulo.IdModulo=PermisoControl.IdModulo AND PermisoModulo.IdModulo = "+ cboModuloControl.SelectedValue + " ", "PermisoControl");
                this.cboProcedimientoControl.DataSource = connGeneric.ds.Tables[0];
                this.cboProcedimientoControl.ValueMember = "IdControl";
                this.cboProcedimientoControl.DisplayMember = "Control";                

                connGeneric.DesconectarBD();

                connGeneric.ConsultaGeneric("SELECT * FROM Personal", "Personal");
                this.cboUsuarioPermiso.DataSource = connGeneric.ds.Tables[0];
                this.cboUsuarioPermiso.ValueMember = "IdPersonal";
                this.cboUsuarioPermiso.DisplayMember = "Usuario";

                connGeneric.DesconectarBD();

                //connGeneric.ConsultaGeneric("SELECT PermisoControl.IdControl, PermisoControl.Control FROM PermisoModulo, PermisoControl WHERE PermisoModulo.IdModulo=PermisoControl.IdModulo AND PermisoModulo.IdModulo = " + cboModuloControl.SelectedValue + " ", "PermisoControl");
                //txtEstadoPermiso.Text = connGeneric.leerGeneric["Estado"].ToString();


            }
            catch { connGeneric.DesconectarBD(); }
        }

        private void cboModuloControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboModuloControl.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    connGeneric.ConsultaGeneric("SELECT PermisoControl.IdControl, PermisoControl.Control FROM PermisoModulo, PermisoControl WHERE PermisoModulo.IdModulo=PermisoControl.IdModulo AND PermisoModulo.IdModulo = " + cboModuloControl.SelectedValue + " ", "PermisoControl");
                    this.cboProcedimientoControl.DataSource = connGeneric.ds.Tables[0];
                    this.cboProcedimientoControl.ValueMember = "IdControl";
                    this.cboProcedimientoControl.DisplayMember = "Control";

                    connGeneric.DesconectarBD();
                }
            }
            catch { connGeneric.DesconectarBD(); }
        }

        private void cboUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
        /*    try
            {
                if (cboUsuarioPermiso.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    connGeneric.ConsultaGeneric("SELECT  Personal.USUARIO, Personal.NOMBREYAPELLIDO, PermisoModulo.Idmodulo, PermisoControl.IdControl, PermisoControl.Control, EstadoSistema.Descripcion, PermisoUsuario.IdPersonal FROM Personal, PermisoModulo, PermisoControl, PermisoUsuario, EstadoSistema WHERE EstadoSistema.IdEstado = PermisoUsuario.IdEstadoSistema AND PermisoControl.IdModulo = PermisoModulo.IdModulo AND PermisoUsuario.IdPersonal = Personal.IdPersonal AND PermisoUsuario.IdControl = 1 AND PermisoUsuario.IdModulo = "+ cboModuloControl.SelectedValue + " AND PermisoUsuario.idpersonal = "+ cboProcedimientoControl.SelectedValue + "", "PermisoUsuario");
                    this.cboUsuarioPermiso.DataSource = connGeneric.ds.Tables[0];
                    this.cboUsuarioPermiso.ValueMember = "IdPersonal";
                    this.cboUsuarioPermiso.DisplayMember = "USUARIO";

                    connGeneric.DesconectarBD();

                    //PermisosUsuario();
                }
            }
            catch { connGeneric.DesconectarBD(); }
            */
        }


        private void PermisosUsuario()
        {
            try
            {
                int iIdControl;

                iIdControl = Convert.ToInt32(lvwDetalleControl.SelectedItems[0].SubItems[3].Text.Trim());

                lvwDetalleControl.Items.Clear();

                lvwDetalleControl.Columns[0].Width = 75;
                lvwDetalleControl.Columns[1].Width = 140;

                SqlCommand cm = new SqlCommand("SELECT Personal.USUARIO, Personal.NOMBREYAPELLIDO, PermisoModulo.Idmodulo, PermisoControl.IdControl, PermisoControl.Control, EstadoSistema.Descripcion, PermisoUsuario.IdPersonal FROM Personal, PermisoModulo, PermisoControl, PermisoUsuario, EstadoSistema WHERE PermisoUsuario.IdPersonal = Personal.IdPersonal AND PermisoUsuario.IdModulo = PermisoModulo.IdModulo AND PermisoUsuario.IdControl = PermisoControl.IdControl AND PermisoUsuario.IdEstadoSistema = EstadoSistema.IdEstado AND PermisoUsuario.IdModulo = "+ cboModuloControl.SelectedValue + " AND PermisoUsuario.IdControl = " + iIdControl + " AND PermisoUsuario.IDPERSONAL = "+ cboUsuarioPermiso.SelectedValue + "", conectaEstado);
                
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwDetalleControl.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Usuario"].ToString());
                    item.SubItems.Add(dr["NOMBREYAPELLIDO"].ToString());
                    item.SubItems.Add(dr["Idmodulo"].ToString());
                    item.SubItems.Add(dr["IdControl"].ToString());
                    item.SubItems.Add(dr["Control"].ToString());
                    item.SubItems.Add(dr["Descripcion"].ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["IdPersonal"].ToString());

                    if (dr["Descripcion"].ToString().Trim() == "Activo" )
                        item.ImageIndex = 0;
                    else
                        item.ImageIndex = 1;

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleControl.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch //(System.Exception excep)
            {
                //Auditoria
              //  AuditoriaSistema AS3 = new AuditoriaSistema();
              //  AS3.SistemaProcesoAuditor_0003("Proc. MostrarDatos() ", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////            
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            PermisosUsuario();
        }

        private void btnPermisoGlobalUsusario_Click(object sender, EventArgs e)
        {
            try
            {
                lvwDetalleControl.Items.Clear();

                lvwDetalleControl.Columns[0].Width = 28;
                lvwDetalleControl.Columns[1].Width = 0;

                SqlCommand cm = new SqlCommand("SELECT Personal.USUARIO, Personal.NOMBREYAPELLIDO, PermisoModulo.Idmodulo, PermisoControl.IdControl, PermisoControl.Control, EstadoSistema.Descripcion, PermisoUsuario.IdPersonal FROM Personal, PermisoModulo, PermisoControl, PermisoUsuario, EstadoSistema WHERE PermisoUsuario.IdPersonal = Personal.IdPersonal AND PermisoUsuario.IdModulo = PermisoModulo.IdModulo AND PermisoUsuario.IdControl = PermisoControl.IdControl AND PermisoUsuario.IdEstadoSistema = EstadoSistema.IdEstado AND PermisoUsuario.IDPERSONAL = " + cboUsuarioPermiso.SelectedValue + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwDetalleControl.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Usuario"].ToString());
                    item.SubItems.Add(dr["NOMBREYAPELLIDO"].ToString());
                    item.SubItems.Add(dr["Idmodulo"].ToString());
                    item.SubItems.Add(dr["IdControl"].ToString());
                    item.SubItems.Add(dr["Control"].ToString());
                    item.SubItems.Add(dr["Descripcion"].ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["IdPersonal"].ToString());

                    if (dr["Descripcion"].ToString().Trim() == "Activo")
                        item.ImageIndex = 0;
                    else
                        item.ImageIndex = 1;

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleControl.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch //(System.Exception excep)
            {
                //Auditoria
                //  AuditoriaSistema AS3 = new AuditoriaSistema();
                //  AS3.SistemaProcesoAuditor_0003("Proc. MostrarDatos() ", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////            
            }

        }

        private void btnActivarPermiso_Click(object sender, EventArgs e)
        {
            try
            {
                string actualizar = "IdEstadoSistema=1";

                if (connGeneric.ActualizaGeneric("PermisoUsuario", actualizar, " IdModulo = " + Convert.ToInt32(lvwDetalleControl.SelectedItems[0].SubItems[2].Text) + " AND IdControl = " + Convert.ToInt32(lvwDetalleControl.SelectedItems[0].SubItems[3].Text) + " AND IdPersonal = " + Convert.ToInt32(lvwDetalleControl.SelectedItems[0].SubItems[6].Text) + ""))
                {
                    PermisosUsuario();
                    MessageBox.Show("Información del permiso actualizado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se ha podido actualizar los datos de permiso del usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connGeneric.DesconectarBD();
                }
            }
            catch //(System.Exception excep)
            {
                MessageBox.Show("Error: No se ha podido actualizar la información de permiso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connGeneric.DesconectarBD();

                //Auditoria
                // AuditoriaSistema AS15 = new AuditoriaSistema();
                // AS15.SistemaProcesoAuditor_0015("Evento btnModificar_Click()", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////   
            }
        }

        private void btnDesactivarPermiso_Click(object sender, EventArgs e)
        {
            try
            {
                string actualizar = "IdEstadoSistema=2";

                if (connGeneric.ActualizaGeneric("PermisoUsuario", actualizar, " IdModulo = " + Convert.ToInt32(lvwDetalleControl.SelectedItems[0].SubItems[2].Text) + " AND IdControl = " + Convert.ToInt32(lvwDetalleControl.SelectedItems[0].SubItems[3].Text) + " AND IdPersonal = " + Convert.ToInt32(lvwDetalleControl.SelectedItems[0].SubItems[6].Text) + ""))
                {
                    PermisosUsuario();
                    MessageBox.Show("Información del permiso actualizado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se ha podido actualizar los datos de permiso del usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connGeneric.DesconectarBD();
                }               
            }
            catch //(System.Exception excep)
            {
                MessageBox.Show("Error: No se ha podido actualizar la información de permiso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connGeneric.DesconectarBD();

                //Auditoria
                // AuditoriaSistema AS15 = new AuditoriaSistema();
                // AS15.SistemaProcesoAuditor_0015("Evento btnModificar_Click()", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////   
            }
        }

        private void PermisosUsuarioCambios()
        {

        }

        private void btnPermisoPorModulo_Click(object sender, EventArgs e)
        {
            try
            {
                lvwDetalleControl.Items.Clear();

                lvwDetalleControl.Columns[0].Width = 28;
                lvwDetalleControl.Columns[1].Width = 0;

                SqlCommand cm = new SqlCommand("SELECT Personal.USUARIO, Personal.NOMBREYAPELLIDO, PermisoModulo.Idmodulo, PermisoControl.IdControl, PermisoControl.Control, EstadoSistema.Descripcion, PermisoUsuario.IdPersonal FROM Personal, PermisoModulo, PermisoControl, PermisoUsuario, EstadoSistema WHERE PermisoUsuario.IdPersonal = Personal.IdPersonal AND PermisoUsuario.IdModulo = PermisoModulo.IdModulo AND PermisoUsuario.IdControl = PermisoControl.IdControl AND PermisoUsuario.IdEstadoSistema = EstadoSistema.IdEstado AND PermisoUsuario.IdModulo = " + cboModuloControl.SelectedValue + " AND PermisoUsuario.IDPERSONAL = " + cboUsuarioPermiso.SelectedValue + "", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwDetalleControl.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Usuario"].ToString());
                    item.SubItems.Add(dr["NOMBREYAPELLIDO"].ToString());
                    item.SubItems.Add(dr["Idmodulo"].ToString());
                    item.SubItems.Add(dr["IdControl"].ToString());
                    item.SubItems.Add(dr["Control"].ToString());
                    item.SubItems.Add(dr["Descripcion"].ToString(), Color.Empty, Color.LightGray, null);
                    item.SubItems.Add(dr["IdPersonal"].ToString());

                    if (dr["Descripcion"].ToString().Trim() == "Activo")
                        item.ImageIndex = 0;
                    else
                        item.ImageIndex = 1;

                    item.UseItemStyleForSubItems = false;
                    lvwDetalleControl.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch //(System.Exception excep)
            {
                //Auditoria
                //  AuditoriaSistema AS3 = new AuditoriaSistema();
                //  AS3.SistemaProcesoAuditor_0003("Proc. MostrarDatos() ", frmPrincipal.Usuario);
                //////////////////////////////////////////////////////            
            }
        }
    }

}
