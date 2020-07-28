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

namespace DGestion
{
    public partial class frmVisorSuceso : Form
    {
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");  

        public frmVisorSuceso() {
            InitializeComponent();
        }

        private void FormatoListView() {
            lvwSuceso.View = View.Details;
            lvwSuceso.LabelEdit = false;
            lvwSuceso.AllowColumnReorder = false;
            lvwSuceso.FullRowSelect = true;
            lvwSuceso.GridLines = true;            
        }


        private void btnCerrar_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void tvwVisor_AfterSelect(object sender, TreeViewEventArgs e) {

            if (this.tvwVisor.Nodes[0].Nodes[0].IsSelected) {
                //Nodo Sistema
                //MostrarDatosAplicacion();
            }

            else if (this.tvwVisor.Nodes[0].Nodes[1].IsSelected)
            {

                MostrarDatosSistema();
                //Nodo Aplicación
            }

            else if (this.tvwVisor.Nodes[0].Nodes[2].IsSelected)
            {

                MostrarDatosSeguridad();
                //Nodo Seguridad
            }
        }

        private void frmVisorSuceso_Load(object sender, EventArgs e) {
            tvwVisor.ExpandAll();
            tvwVisor.SelectedNode = tvwVisor.Nodes[0].Nodes[0];
            cboBuscaSuceso.SelectedIndex = 0;

            lvwSuceso.View = View.Details;
            lvwSuceso.LabelEdit = false;
            lvwSuceso.AllowColumnReorder = false;
            lvwSuceso.FullRowSelect = true;
            lvwSuceso.GridLines = true;
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        public void MostrarDatosAplicacion()
        {
        }

        public void MostrarDatosSistema()
        {
            try
            {
                lvwSuceso.Items.Clear();
                FormatoListView();

                SqlCommand cm = new SqlCommand("SELECT * FROM Suceso, EstadoSistema WHERE EstadoSistema.IdEstado = Suceso.IDESTADO AND (EstadoSistema.IDESTADO=16) ORDER BY Suceso.IdSuceso Desc", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwSuceso.SmallImageList = imageList2;
                    ListViewItem item = new ListViewItem(dr["IDSUCESO"].ToString());
                    item.SubItems.Add(dr["EVENTO"].ToString());
                    item.SubItems.Add(dr["IDESTADO"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());
                    item.SubItems.Add(dr["FECHAHORA"].ToString());
                    item.SubItems.Add(dr["USUARIO"].ToString());
                    
                  //  if (item.SubItems[2].Text == "6")
                        item.ImageIndex = 3;
                    //else
                     //   item.ImageIndex = 0;

                    lvwSuceso.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

        public void MostrarDatosSeguridad() {
            try  {
                lvwSuceso.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT * FROM Suceso, EstadoSistema WHERE EstadoSistema.IdEstado = Suceso.IDESTADO AND (EstadoSistema.IDESTADO=4 OR EstadoSistema.IDESTADO=5 OR EstadoSistema.IDESTADO=8 OR EstadoSistema.IDESTADO=9) ORDER BY Suceso.IdSuceso Desc", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows) {
                    lvwSuceso.SmallImageList = imageList2;
                    ListViewItem item = new ListViewItem(dr["IDSUCESO"].ToString());
                    item.SubItems.Add(dr["EVENTO"].ToString());                    
                    item.SubItems.Add(dr["IDESTADO"].ToString());
                    item.SubItems.Add(dr["DESCRIPCION"].ToString());
                    item.SubItems.Add(dr["FECHAHORA"].ToString());
                    item.SubItems.Add(dr["USUARIO"].ToString());

                    if (item.SubItems[2].Text == "8" || item.SubItems[2].Text == "9")
                        item.ImageIndex = 3;
                    else
                        item.ImageIndex = 0;

                    lvwSuceso.Items.Add(item);
                }
                cm.Connection.Close();
            }
            catch { }
        }

    }
}