using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DGestion
{
    public partial class frmArticuloEstadoStock : Form
    {
        public frmArticuloEstadoStock()
        {
            InitializeComponent();
        }

        ArticulosBD conn = new ArticulosBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");       

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void frmArticuloStockInsuf_Load(object sender, EventArgs e) {
            conn.ConectarBD();
            FormatoListView();
            MostrarDatos();
            cboBuscaArticulo.SelectedIndex = 0;
            lblEstado.Visible = false; pbxEstado.Visible = false;
        }        

        public void MostrarDatos() {
            
            lvwEstadosExistencia.Items.Clear();

            SqlCommand cm = new SqlCommand("SELECT Articulo.IDArticulo, Articulo.CODIGO, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL AS 'CANTIDAD', Articulo.CANT_DE_PUNTO_DE_PEDIDO AS 'Cant Minima', Articulo.CANTENCADAREPOSICION as 'Cant por Reposicion', CASE WHEN Articulo.CANT_ACTUAL = 0 AND Articulo.CANT_DE_PUNTO_DE_PEDIDO >= 0 THEN 'Sin Existencia' WHEN Articulo.CANT_ACTUAL < Articulo.CANT_DE_PUNTO_DE_PEDIDO THEN 'Las Existencias son Insuficientes' WHEN Articulo.CANT_ACTUAL > Articulo.CANT_DE_PUNTO_DE_PEDIDO THEN 'Las Existencias son Suficientes' WHEN Articulo.CANT_ACTUAL = Articulo.CANT_DE_PUNTO_DE_PEDIDO THEN 'Las Existencias actuales han alcanzado el limite mínimo' ELSE 'Sin Stock' END AS 'SITUACION' FROM Articulo", conectaEstado);

            cm.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows) {
                lvwEstadosExistencia.SmallImageList = imageList1;
                ListViewItem item = new ListViewItem(dr["IDArticulo"].ToString());
                item.SubItems.Add(dr["CODIGO"].ToString());                
                item.SubItems.Add(dr["DESCRIPCION"].ToString());
                item.SubItems.Add(dr["CANTIDAD"].ToString());
                item.SubItems.Add(dr["Cant Minima"].ToString());
                item.SubItems.Add(dr["Cant por Reposicion"].ToString());
                item.SubItems.Add(dr["SITUACION"].ToString());

                if (item.SubItems[6].Text == "Las Existencias son Suficientes")
                    item.ImageIndex = 0;
                else if (item.SubItems[6].Text == "Las Existencias son Insuficientes")
                    item.ImageIndex = 2;
                else if (item.SubItems[6].Text == "Sin Existencia")
                    item.ImageIndex = 1;
                else if (item.SubItems[6].Text == "Las Existencias actuales han alcanzado el limite mínimo")
                    item.ImageIndex = 3;
                else if (item.SubItems[6].Text == "Ultima Unidad")
                    item.ImageIndex = 3;
                else
                    item.ImageIndex = 1;

                lvwEstadosExistencia.Items.Add(item);                
            }
            cm.Connection.Close();
        }

        private void FormatoListView() {            
            //listView1.Bounds = new Rectangle(new Point(10, 10), new Size(300, 200));            
            lvwEstadosExistencia.View = View.Details;            
            lvwEstadosExistencia.LabelEdit = true;            
            lvwEstadosExistencia.AllowColumnReorder = true;            
            //listView1.CheckBoxes = true;            
            //listView1.StateImageList
            lvwEstadosExistencia.FullRowSelect = true;            
            lvwEstadosExistencia.GridLines = true;                
        }

        private void tsBtnActualizaStock_Click(object sender, EventArgs e) {
            MostrarDatos();
        }

        private void lvwEstadosExistencia_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                lblEstado.Visible = true; pbxEstado.Visible = true;
                string p = Application.StartupPath;
                  lblEstado.Text = lvwEstadosExistencia.SelectedItems[0].SubItems[6].Text;

                if  (lblEstado.Text == "Las Existencias son Suficientes")
                     this.pbxEstado.ImageLocation = p + "//ok.ico";  
                else if  (lblEstado.Text == "Las Existencias son Insuficientes")
                     this.pbxEstado.ImageLocation = p + "//warning.ico";
                else if (lblEstado.Text == "Las Existencias actuales han alcanzado el limite mínimo")
                    this.pbxEstado.ImageLocation = p + "//Action_flag.ico";
                else if (lblEstado.Text == "Ultima Unidad")
                    this.pbxEstado.ImageLocation = p + "//warning.ico";
                else if (lblEstado.Text == "Sin Existencia")
                    this.pbxEstado.ImageLocation = p + "/cancel.ico";                
            }
            catch { }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (txtBuscarArticulo.Text.Trim() == "")
            {
                timer1.Interval=30000;
                MostrarDatos();
            }
            else {
                if (cboBuscaArticulo.SelectedIndex == 0) {
                    timer1.Interval = 5000;
                    BuscarDatos1();
                }

                else if (cboBuscaArticulo.SelectedIndex == 1) {
                    timer1.Interval = 5000;
                    BuscarDatos2();
                }
            }
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboBuscaArticulo.SelectedIndex == 0)
                {
                    BuscarDatos1();
                }

                else if (cboBuscaArticulo.SelectedIndex == 1)
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
                lvwEstadosExistencia.Items.Clear();

                 SqlCommand cm = new SqlCommand("SELECT Articulo.IDArticulo, Articulo.CODIGO, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL AS 'CANTIDAD', Articulo.CANT_DE_PUNTO_DE_PEDIDO AS 'Cant Minima', Articulo.CANTENCADAREPOSICION as 'Cant por Reposicion', CASE WHEN Articulo.CANT_ACTUAL = 0 AND Articulo.CANT_DE_PUNTO_DE_PEDIDO >= 0 THEN 'Sin Existencia' WHEN Articulo.CANT_ACTUAL < Articulo.CANT_DE_PUNTO_DE_PEDIDO THEN 'Las Existencias son Insuficientes' WHEN Articulo.CANT_ACTUAL > Articulo.CANT_DE_PUNTO_DE_PEDIDO THEN 'Las Existencias son Suficientes' WHEN Articulo.CANT_ACTUAL = Articulo.CANT_DE_PUNTO_DE_PEDIDO THEN 'Las Existencias actuales han alcanzado el limite mínimo' ELSE 'Sin Stock' END AS 'SITUACION' FROM Articulo WHERE  Articulo.CODIGO LIKE '" + txtBuscarArticulo.Text.Trim() + "%'", conectaEstado);

            cm.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows) {
                lvwEstadosExistencia.SmallImageList = imageList1;
                ListViewItem item = new ListViewItem(dr["IDArticulo"].ToString());
                item.SubItems.Add(dr["CODIGO"].ToString());                
                item.SubItems.Add(dr["DESCRIPCION"].ToString());
                item.SubItems.Add(dr["CANTIDAD"].ToString());
                item.SubItems.Add(dr["Cant Minima"].ToString());
                item.SubItems.Add(dr["Cant por Reposicion"].ToString());
                item.SubItems.Add(dr["SITUACION"].ToString());

                if (item.SubItems[6].Text == "Las Existencias son Suficientes")
                    item.ImageIndex = 0;
                else if (item.SubItems[6].Text == "Las Existencias son Insuficientes")
                    item.ImageIndex = 2;
                else if (item.SubItems[6].Text == "Sin Existencia")
                    item.ImageIndex = 1;
                else if (item.SubItems[6].Text == "Las Existencias actuales han alcanzado el limite mínimo")
                    item.ImageIndex = 3;
                else if (item.SubItems[6].Text == "Ultima Unidad")
                    item.ImageIndex = 3;
                else
                    item.ImageIndex = 1;
                lvwEstadosExistencia.Items.Add(item);
            }              
                cm.Connection.Close();
            }
            catch { }
        }

        public void BuscarDatos2()
        {
            try
            {
                lvwEstadosExistencia.Items.Clear();

                if ((cboBuscaArticulo.SelectedIndex == 1 && txtBuscarArticulo.Text == "") || txtBuscarArticulo.Text == "*")
                {
                    if (txtBuscarArticulo.Text == "*")
                        txtBuscarArticulo.Text = "";
                    
                 SqlCommand cm = new SqlCommand("SELECT Articulo.IDArticulo, Articulo.CODIGO, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL AS 'CANTIDAD', Articulo.CANT_DE_PUNTO_DE_PEDIDO AS 'Cant Minima', Articulo.CANTENCADAREPOSICION as 'Cant por Reposicion', CASE WHEN Articulo.CANT_ACTUAL = 0 AND Articulo.CANT_DE_PUNTO_DE_PEDIDO >= 0 THEN 'Sin Existencia' WHEN Articulo.CANT_ACTUAL < Articulo.CANT_DE_PUNTO_DE_PEDIDO THEN 'Las Existencias son Insuficientes' WHEN Articulo.CANT_ACTUAL > Articulo.CANT_DE_PUNTO_DE_PEDIDO THEN 'Las Existencias son Suficientes' WHEN Articulo.CANT_ACTUAL = Articulo.CANT_DE_PUNTO_DE_PEDIDO THEN 'Las Existencias actuales han alcanzado el limite mínimo' ELSE 'Sin Stock' END AS 'SITUACION' FROM Articulo WHERE  Articulo.Descripcion LIKE '" + txtBuscarArticulo.Text.Trim() + "%'", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                   foreach (DataRow dr in dt.Rows) {
                lvwEstadosExistencia.SmallImageList = imageList1;
                ListViewItem item = new ListViewItem(dr["IDArticulo"].ToString());
                item.SubItems.Add(dr["CODIGO"].ToString());                
                item.SubItems.Add(dr["DESCRIPCION"].ToString());
                item.SubItems.Add(dr["CANTIDAD"].ToString());
                item.SubItems.Add(dr["Cant Minima"].ToString());
                item.SubItems.Add(dr["Cant por Reposicion"].ToString());
                item.SubItems.Add(dr["SITUACION"].ToString());

                if (item.SubItems[6].Text == "Las Existencias son Suficientes")
                    item.ImageIndex = 0;
                else if (item.SubItems[6].Text == "Las Existencias son Insuficientes")
                    item.ImageIndex = 2;
                else if (item.SubItems[6].Text == "Sin Existencia")
                    item.ImageIndex = 1;
                else if (item.SubItems[6].Text == "Las Existencias actuales han alcanzado el limite mínimo")
                    item.ImageIndex = 3;
                else if (item.SubItems[6].Text == "Ultima Unidad")
                    item.ImageIndex = 3;
                else
                    item.ImageIndex = 1;

                    lvwEstadosExistencia.Items.Add(item);
                    cm.Connection.Close();
                    }
                }

                else if (cboBuscaArticulo.SelectedIndex == 1 && txtBuscarArticulo.Text != "")
                {
                   SqlCommand cm = new SqlCommand("SELECT Articulo.IDArticulo, Articulo.CODIGO, Articulo.DESCRIPCION, Articulo.CANT_ACTUAL AS 'CANTIDAD', Articulo.CANT_DE_PUNTO_DE_PEDIDO AS 'Cant Minima', Articulo.CANTENCADAREPOSICION as 'Cant por Reposicion', CASE WHEN Articulo.CANT_ACTUAL = 0 AND Articulo.CANT_DE_PUNTO_DE_PEDIDO >= 0 THEN 'Sin Existencia' WHEN Articulo.CANT_ACTUAL < Articulo.CANT_DE_PUNTO_DE_PEDIDO THEN 'Las Existencias son Insuficientes' WHEN Articulo.CANT_ACTUAL > Articulo.CANT_DE_PUNTO_DE_PEDIDO THEN 'Las Existencias son Suficientes' WHEN Articulo.CANT_ACTUAL = Articulo.CANT_DE_PUNTO_DE_PEDIDO THEN 'Las Existencias actuales han alcanzado el limite mínimo' ELSE 'Sin Stock' END AS 'SITUACION' FROM Articulo WHERE  Articulo.Descripcion LIKE '" + txtBuscarArticulo.Text.Trim() + "%'", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwEstadosExistencia.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["IDArticulo"].ToString());
                        item.SubItems.Add(dr["CODIGO"].ToString());
                        item.SubItems.Add(dr["DESCRIPCION"].ToString());
                        item.SubItems.Add(dr["CANTIDAD"].ToString());
                        item.SubItems.Add(dr["Cant Minima"].ToString());
                        item.SubItems.Add(dr["Cant por Reposicion"].ToString());
                        item.SubItems.Add(dr["SITUACION"].ToString());

                        if (item.SubItems[6].Text == "Las Existencias son Suficientes")
                            item.ImageIndex = 0;
                        else if (item.SubItems[6].Text == "Las Existencias son Insuficientes")
                            item.ImageIndex = 2;
                        else if (item.SubItems[6].Text == "Sin Existencia")
                            item.ImageIndex = 1;
                        else if (item.SubItems[6].Text == "Las Existencias actuales han alcanzado el limite mínimo")
                            item.ImageIndex = 3;
                        else if (item.SubItems[6].Text == "Ultima Unidad")
                            item.ImageIndex = 3;
                        else
                            item.ImageIndex = 1;

                        lvwEstadosExistencia.Items.Add(item);
                        cm.Connection.Close();
                    }            
                }
            }
             catch { }
        }    
        /// ///////////////////////////////////////////////////FIN BLOQUE DE BUSQUEDA//////////////////////////////////////////////////////////////

    }
}