using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DGestion.Clases;
using System.Data.SqlClient;


namespace DGestion
{
    public partial class frmSoporteTecnico : Form
    {
        public frmSoporteTecnico()
        {
            InitializeComponent();
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        CGenericBD conn = new CGenericBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private void btnGuardar_Click(object sender, EventArgs e) {
            try {
                if (txtDescripcion.Text.Trim() != "" || cboPrioridad.Text != "") {
                    string agregar = "INSERT INTO SoporteTecnico(FECHASOLICITUD, DESCRIPCION, PRIORIDAD, USUARIO, ESTADOACTUAL) VALUES('" + dtpFechaFactu.Value + "', '" + txtDescripcion.Text + "', '" + cboPrioridad.Text + "','" + frmLogIn.usuarioLogeado + "', '" + cboEstadoActual.Text + "')";
                    if (conn.InsertarGeneric(agregar)) {
                        MostrarDatos();
                        MessageBox.Show("Informe Enviado a Soporte Técnico");
                    }
                    else
                        MessageBox.Show("Error al Agregar, no se ha agregado una descripcion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("El campo descripcion o prioridad esta vacio", "Adventencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { }
        }

        private void FormatoListView()
        {
            lvwSoporteTecnico.View = View.Details;
            lvwSoporteTecnico.LabelEdit = false;
            lvwSoporteTecnico.AllowColumnReorder = false;
            lvwSoporteTecnico.FullRowSelect = true;
            lvwSoporteTecnico.GridLines = true;
        }

        private void frmSoporteTecnico_Load(object sender, EventArgs e)
        {
            FormatoListView();
            conn.ConectarBD();
            MostrarDatos();

            dtpFechaFactu.Value = DateTime.Now;

            cboPrioridad.SelectedIndex = 0;
            cboBuscaTiquetSoporte.SelectedIndex = 0;
            cboEstadoActual.SelectedIndex = 0;

            //Nivel de Permiso
            if (frmPrincipal.GrupoUsuario == "Sistema")
            {
                cboEstadoActual.Enabled = true;
                txtObservaciones.ReadOnly = false;
                txtObservaciones.Enabled = true;
                btnGuardar.Enabled = false;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                tsBtnNuevo.Enabled = false;
                
                dtpFechaFactu.Enabled = false;
                cboPrioridad.Enabled = false;
                txtDescripcion.ReadOnly = true;                
            }
            else
            {
                cboEstadoActual.Enabled = false;
                txtObservaciones.ReadOnly = true;
                txtObservaciones.Enabled = false;
                btnGuardar.Enabled = true;
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                tsBtnNuevo.Enabled = true;
                                
                dtpFechaFactu.Enabled = true;
                cboPrioridad.Enabled = true;
                txtDescripcion.ReadOnly = false;                
            }
            ///////////////////////////////////////////////
        }

        public void MostrarDatos()
        {
            try
            {
                lvwSoporteTecnico.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM SoporteTecnico ORDER BY NROTIQUET desc", conectaEstado);

                cm.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwSoporteTecnico.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["NROTIQUET"].ToString());
                    item.SubItems.Add(dr["FECHASOLICITUD"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());
                    item.SubItems.Add(dr["PRIORIDAD"].ToString());
                    item.SubItems.Add(dr["USUARIO"].ToString());
                    item.SubItems.Add(dr["ESTADOACTUAL"].ToString());
                    item.SubItems.Add(dr["OBSERVACIONES"].ToString());

                    if (item.SubItems[5].Text == "Pendiente")
                    {
                        if (item.SubItems[3].Text == "Prioridad Baja")
                            item.ImageIndex = 0;
                        else if (item.SubItems[3].Text == "Prioridad Media")
                            item.ImageIndex = 1;
                        else if (item.SubItems[3].Text == "Prioridad Alta")
                            item.ImageIndex = 2;
                        else
                            item.ImageIndex = 0;
                    }
                    else if (item.SubItems[5].Text == "Atendiendo")
                    {
                        item.ImageIndex = 3;
                    }

                    else if (item.SubItems[5].Text == "Solucionado")
                    {
                        item.ImageIndex = 4;
                    }

                    lvwSoporteTecnico.Items.Add(item);
                }
                cm.Connection.Close();

            }
            catch { }
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            dtpFechaFactu.Value = DateTime.Now;
            cboPrioridad.SelectedIndex = 0;
            cboEstadoActual.SelectedIndex = 0;
            txtDescripcion.Text = "";
            txtObservaciones.Text = "";

            gpoCliente.Visible = true;
            this.lvwSoporteTecnico.Height = 408;
        }

        private void lvwSoporteTecnico_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int nroTiquet;

                nroTiquet = Convert.ToInt32(lvwSoporteTecnico.SelectedItems[0].SubItems[0].Text);
                conn.LeeGeneric("SELECT * FROM SoporteTecnico WHERE NROTIQUET = " + nroTiquet + "", "Articulo");

                this.dtpFechaFactu.Text = conn.leerGeneric["FECHASOLICITUD"].ToString();
                this.cboPrioridad.Text = conn.leerGeneric["PRIORIDAD"].ToString();
                this.txtDescripcion.Text = conn.leerGeneric["DESCRIPCION"].ToString();
                this.cboEstadoActual.Text = conn.leerGeneric["ESTADOACTUAL"].ToString();
                this.txtObservaciones.Text = conn.leerGeneric["OBSERVACIONES"].ToString();

                conn.DesconectarBDLeeGeneric();
                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwSoporteTecnico.SelectedItems.Count == 0)
                    MessageBox.Show("Error: No se ha seleccionado ningun ítem de soporte", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    string actualizar = "ESTADOACTUAL='" + this.cboEstadoActual.Text.Trim() + "', OBSERVACIONES='" + this.txtObservaciones.Text.Trim() + "'";
                    if (conn.ActualizaGeneric("SoporteTecnico", actualizar, " NROTIQUET = " + Convert.ToInt32(lvwSoporteTecnico.SelectedItems[0].SubItems[0].Text) + ""))
                    {
                        MostrarDatos();
                        MessageBox.Show("Información Actualizada");
                    }
                }
            }
            catch { conn.DesconectarBD(); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.EliminarGeneric("SoporteTecnico", " NROTIQUET = " + Convert.ToInt32(lvwSoporteTecnico.SelectedItems[0].SubItems[0].Text)))
                {
                    MostrarDatos();
                    MessageBox.Show("Datos Eliminados");
                }
                else
                    MessageBox.Show("Error al Eliminar");
            }
            catch { conn.DesconectarBD(); }
        }

        private void btcCerrar_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = false;
            this.lvwSoporteTecnico.Height = 630;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MostrarDatos();
        }
    }

}