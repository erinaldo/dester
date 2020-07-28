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
using System.Threading;

namespace DGestion
{
    public partial class frmArtPrecioVenta : Form
    {
        public frmArtPrecioVenta() {
            InitializeComponent();
        }

        ArticulosBD conn = new ArticulosBD();        
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");
        EmpresaBD connEmpresa = new EmpresaBD();
        int NumArt, CantLista;
        string codArt;

        string costoLista;
        decimal resultado;
        decimal resultado2;
        decimal resultado3;
        decimal resultado4;
        string Costo, Porcentaje;

        int idArticulo;

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

        private void FormatoListView()
        {
            try
            {
                this.lvwListaPrecioVenta.View = View.Details;
                this.lvwListaPrecioVenta.LabelEdit = false;
                this.lvwListaPrecioVenta.AllowColumnReorder = false;
                this.lvwListaPrecioVenta.FullRowSelect = true;
                this.lvwListaPrecioVenta.GridLines = true;
                               

                //lvwListaPrecioVenta.View = View.Details;
                //lvwListaPrecioVenta.LabelEdit = true;
                //lvwListaPrecioVenta.AllowColumnReorder = true;
                //lvwListaPrecioVenta.FullRowSelect = true;
                //lvwListaPrecioVenta.GridLines = true;
            }
            catch { }
        }

        private void conPermi()
        {
            try
            {
                string sUsuarioLegueado;
                sUsuarioLegueado = frmPrincipal.Usuario;

                SqlCommand cm = new SqlCommand("SELECT Personal.USUARIO, Personal.NOMBREYAPELLIDO, PermisoModulo.Idmodulo, PermisoControl.IdControl, PermisoControl.Control, EstadoSistema.Descripcion, PermisoUsuario.IdPersonal FROM Personal, PermisoModulo, PermisoControl, PermisoUsuario, EstadoSistema WHERE PermisoUsuario.IdPersonal = Personal.IdPersonal AND PermisoUsuario.IdModulo = PermisoModulo.IdModulo AND PermisoUsuario.IdControl = PermisoControl.IdControl AND PermisoUsuario.IdEstadoSistema = EstadoSistema.IdEstado AND PermisoUsuario.IdModulo = 7 AND Personal.USUARIO = '" + sUsuarioLegueado + "' ORDER BY PermisoUsuario.IdControl", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                string prueba;

                //prueba = dt.Rows[4]["Control"].ToString().Trim();

                //1
                if (dt.Rows[0]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[0]["Control"].ToString().Trim() == "Incluir Articulo en Lista")
                    tsBtnVincular.Enabled = true;
                else
                    tsBtnVincular.Enabled = false;

                //2
                if (dt.Rows[1]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[1]["Control"].ToString().Trim() == "Ver Costos de Precio")
                {
                    lvwListaPrecioVenta.Columns[2].Width = 105;
                    label4.Visible = true;
                    txtDescripListaPrecio.Visible = true;
                }
                else
                {
                    lvwListaPrecioVenta.Columns[2].Width = 0;
                    label4.Visible = false;
                    txtDescripListaPrecio.Visible = false;
                }

                //3
                if (dt.Rows[2]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[2]["Control"].ToString().Trim() == "Actualizar Precios")
                    btnModificar.Enabled = true;
                else
                    btnModificar.Enabled = false;

                //4
                if (dt.Rows[3]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[3]["Control"].ToString().Trim() == "Eliminar Articulo en Lista")
                    btnEliminar.Enabled = true;
                else
                    btnEliminar.Enabled = false;

                //5
                if (dt.Rows[4]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[4]["Control"].ToString().Trim() == "Ver Precios C/Iva")
                {
                    lvwListaPrecioVenta.Columns[3].Width = 105;
                    label4.Visible = true;
                    txtDescripListaPrecio.Visible = true;
                }
                else
                {
                    lvwListaPrecioVenta.Columns[3].Width = 0;
                    label4.Visible = false;
                    txtDescripListaPrecio.Visible = false;
                }

                //6
                if (dt.Rows[5]["Descripcion"].ToString().Trim() == "Inactivo" && dt.Rows[5]["Control"].ToString().Trim() == "Ver Precios S/IVA")
                {
                    lvwListaPrecioVenta.Columns[4].Width = 105;
                    label4.Visible = true;
                    txtDescripListaPrecio.Visible = true;
                }
                else
                {
                    lvwListaPrecioVenta.Columns[4].Width = 0;
                    label4.Visible = false;
                    txtDescripListaPrecio.Visible = false;
                }



                cm.Connection.Close();

            }
            catch { }
        }

        private string FormateoFecha()
        {
            DateTimePicker dtr = new DateTimePicker();
            dtr.Value = DateTime.Now;
            return String.Format("{0:d/M/yyyy HH:mm:ss}", dtr.Value);
        }

        private void frmArtPrecioVenta_Load(object sender, EventArgs e)
        {
            try
            {
                conPermi();

                gpoCliente.Visible = false;
                lvwListaPrecioVenta.Height = 450;
                gbListaPrecio.Height = 510;
                conn.ConectarBD(); LlenarComboBuscar();
                cboBuscar.SelectedIndex = 0; cboBuscarArt.SelectedIndex = 0;

                IDEMPRESA = ConsultaEmpresa(); //Lee Empresa

                FormatoListView();

                MostrarDatos();
            }
            catch { }
        }

       public void LlenarComboBuscar() {
            conn.ConsultaListaPrecioVenta("SELECT IdListaPrecio, Descripcion, Criterio FROM ListaPrecios ORDER BY Criterio", "ListaPrecios");
            this.cboBuscar.DataSource = conn.ds.Tables[0];
            this.cboBuscar.ValueMember = "IdListaPrecio";
            this.cboBuscar.DisplayMember = "Descripcion";
            conn.DesconectarBD();            
        }

        public void MostrarDatos()
        {
            try
            {
                lvwListaPrecioVenta.Items.Clear();                               

                SqlCommand cm = new SqlCommand("SELECT Articulo.IDARTICULO, Articulo.IDIMPUESTO, Impuesto.ALICUOTA, ListaPrecios.IDLISTAPRECIO AS 'Código Lista', ListaPrecios.DESCRIPCION AS 'Nombre Lista', ListaPrecios.PORCENTAJE AS 'Porcentaje', ListaPrecios.PORCFLETE AS 'Flete', Articulo.CODIGO AS 'Código Artículo', Articulo.DESCRIPCION AS 'Descripción Artículo', ArticuloPrecioVenta.Precio AS 'Precio Venta', ArticuloPrecioVenta.Ultimactualizacion, Articulo.CostoEnLista as 'CostoEnLista', Articulo.COSTO As 'Costo Articulo', CASE WHEN(Cast(replace(Articulo.CostoEnLista, ',', '.') as decimal(10, 2))) > (Cast(replace(ArticuloPrecioVenta.Precio, ',', '.') as decimal(10, 2))) OR(Cast(replace(ArticuloPrecioVenta.Precio, ',', '.') as decimal(10, 2))) = 0 THEN 'Precio NACT' ELSE 'Precio ACT' END AS 'Situacion Lista'FROM ArticuloPrecioVenta, ListaPrecios, Articulo, Impuesto WHERE(ListaPrecios.IDLISTAPRECIO = ArticuloPrecioVenta.IDLISTAPRECIO AND Articulo.IDARTICULO = ArticuloPrecioVenta.IDARTICULO) AND ListaPrecios.IDLISTAPRECIO = " + cboBuscar.SelectedValue + " AND Impuesto.IDIMPUESTO = articulo.IDIMPUESTO ORDER BY Articulo.Descripcion", conectaEstado);


                decimal fPrecioVentaFinal;
                decimal fAlicuota;


                //cm.Connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwListaPrecioVenta.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Código Artículo"].ToString());
                    item.SubItems.Add(dr["Descripción Artículo"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["CostoEnLista"]), 3).ToString(), Color.Empty, Color.LightGray, null);                    

                    item.SubItems.Add(String.Format("{0:0,000}", Math.Round(Convert.ToDecimal(dr["Precio Venta"]), 3).ToString()));

                    /*if (cboBuscar.SelectedValue.ToString().Trim() != "1002")
                    {*/
                        ///Muestra y calcula precio final iva incl.
                        fPrecioVentaFinal = Convert.ToDecimal(dr["Precio Venta"].ToString());
                        fAlicuota = Convert.ToDecimal(dr["Alicuota"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(fPrecioVentaFinal + (fPrecioVentaFinal * fAlicuota) / 100), 3).ToString(), Color.Empty, Color.LightGray, null);
                    /*}
                    else
                    {
                        ///Muestra y calcula precio final iva incl. Redondeo
                        fPrecioVentaFinal = Convert.ToDecimal(dr["Precio Venta"].ToString());
                        fAlicuota = Convert.ToDecimal(dr["Alicuota"].ToString());
                        item.SubItems.Add(String.Format("{0:0,00}", Math.Round(Convert.ToDecimal(fPrecioVentaFinal + (fPrecioVentaFinal * fAlicuota) / 100), 1).ToString(), Color.Empty, Color.LightGray, null));
                    }  */                      
                    ///////////////////////////////////////////


                    item.SubItems.Add(dr["Situacion Lista"].ToString());
                    item.SubItems.Add(dr["Código Lista"].ToString());
                    item.SubItems.Add(dr["Nombre Lista"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Porcentaje"]), 2).ToString());
                    item.SubItems.Add(dr["IDARTICULO"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Flete"]), 2).ToString());

                    item.ImageIndex = 0;

                    if (item.SubItems[5].Text == "Precio NACT")
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;


                    DateTime fechaArticulo;
                    fechaArticulo = Convert.ToDateTime(dr["Ultimactualizacion"].ToString());

                    if (fechaArticulo.AddDays(15) >= DateTime.Today)                        
                        item.SubItems.Add(String.Format("{0:dd/MM/yyyy}",  Convert.ToDateTime(dr["Ultimactualizacion"].ToString())),  Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold));
                    else if (fechaArticulo.AddDays(30) >= DateTime.Today)
                        item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["Ultimactualizacion"].ToString())), Color.Snow, Color.DarkOrange, new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold));
                    else
                        item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["Ultimactualizacion"].ToString())), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold));

                    
                    item.UseItemStyleForSubItems = false;                    
                    lvwListaPrecioVenta.Items.Add(item);
                }
                cm.Connection.Close();

                if (lvwListaPrecioVenta.Items.Count != 0)
                {
                    this.txtCodLista.Text = lvwListaPrecioVenta.Items[0].SubItems[6].Text;
                    this.txtNombreLista.Text = lvwListaPrecioVenta.Items[0].SubItems[7].Text;
                    this.txtPorcentaje.Text = lvwListaPrecioVenta.Items[0].SubItems[8].Text;
                    this.txtFlete.Text = lvwListaPrecioVenta.Items[0].SubItems[10].Text;
                }
                else { this.txtCodLista.Text = "-"; this.txtNombreLista.Text = cboBuscar.Text.Trim(); this.txtPorcentaje.Text = "-"; this.txtFlete.Text = "-"; }
            }
            catch { }
        }

        private void tsBtnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void cboBuscar_TextChanged(object sender, EventArgs e) {
            MostrarDatos();
        }       

        private void tsBtnListaPrecio_Click(object sender, EventArgs e) {
            frmListaPrecioVenta formListaPrecio = new frmListaPrecioVenta();
            formListaPrecio.ShowDialog();
        }

        private void tsBtnRecargarListas_Click(object sender, EventArgs e) {
            int valorLista;
            valorLista = (int)cboBuscar.SelectedValue;

            if (VerificaNuevosArticulos(valorLista)) {
                DialogResult result = MessageBox.Show("¿Agregar los nuevos artículos a la lista de precio?", "Agregar Artículos", MessageBoxButtons.YesNo,MessageBoxIcon.Information);

                if (result == DialogResult.Yes) {
                    MessageBox.Show("Ok");
                }
                else if (result == DialogResult.No) {
                }
            }
            else                
                MessageBox.Show("No se han vinculado artículos a la lista seleccionada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tsBtnVincular_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("¿Desea actualizar los ultimos articulos modificados/ingresados?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                switch (result)
                {
                    case DialogResult.Yes: //Inserta articulos que se han dado de alta pero que no existen en la lista de precios de vanta
                        {
                            int iIDarticulo;
                            string sCodigoArtic;

                            SqlCommand cmLeeListaArt = new SqlCommand("SELECT * FROM Articulo WHERE Articulo.IdEstado=1 ORDER BY idarticulo", conectaEstado);

                            SqlDataAdapter daLeeArtic = new SqlDataAdapter(cmLeeListaArt);
                            DataTable dtLeeArt = new DataTable();
                            daLeeArtic.Fill(dtLeeArt);

                            foreach (DataRow drLeeArtic in dtLeeArt.Rows)
                            {
                                iIDarticulo = Convert.ToInt32(drLeeArtic["idArticulo"].ToString().Trim());
                                sCodigoArtic = drLeeArtic["Codigo"].ToString().Trim();

                                SqlCommand cmLeeLista = new SqlCommand("SELECT * FROM ArticuloPrecioVenta WHERE Idarticulo = " + iIDarticulo + "  ORDER BY IdListaPrecio", conectaEstado);
                                SqlDataAdapter daLeeLista = new SqlDataAdapter(cmLeeLista);
                                DataTable dtLeeLista = new DataTable();
                                daLeeLista.Fill(dtLeeLista);

                                if (dtLeeLista.Rows.Count == 0)
                                {
                                    int IdListaPrecioVta;
                                    SqlCommand cmLeeListaVenta = new SqlCommand("SELECT * FROM ListaPrecios ORDER BY IdListaPrecio", conectaEstado);

                                    SqlDataAdapter daLeeListaVenta = new SqlDataAdapter(cmLeeListaVenta);
                                    DataTable dtLeeListaVenta = new DataTable();
                                    daLeeListaVenta.Fill(dtLeeListaVenta);

                                    foreach (DataRow drLeeListaVenta in dtLeeListaVenta.Rows)
                                    {
                                        IdListaPrecioVta = Convert.ToInt32(drLeeListaVenta["IdListaPrecio"].ToString().Trim());

                                        string agregar = "INSERT INTO ArticuloPrecioVenta (IDLISTAPRECIO, IDARTICULO, PRECIO) SELECT ListaPrecios.IDLISTAPRECIO, Articulo.IDARTICULO, replace((Cast(replace(Articulo.COSTOENLISTA, ',', '.') as decimal(10, 3)) * ((Cast(replace(ListaPrecios.PORCENTAJE, ',', '.') as decimal(10, 3)) / 100) + 1) * ((ListaPrecios.PORCFLETE / 100) + 1)), '.', ',') AS PRECIO FROM ListaPrecios, Articulo  WHERE ListaPrecios.IDLISTAPRECIO = " + IdListaPrecioVta + " AND Articulo.idestado = 1 AND Articulo.IdArticulo = " + iIDarticulo + " ORDER BY Articulo.Idarticulo;";   //QUERY MODIFICADO 1.1 VINCULA ART A TODAS LAS LISTA
                                        conn.InsertarListaPrecioVenta(agregar);

                                        // string actualizar = "ArticuloPrecioVenta.PRECIO = Cast(replace(Articulo.COSTOENLISTA, ',','.') as decimal(10,3)) * ((Cast(replace(ListaPrecios.PORCENTAJE, ',','.') as decimal(10,3))/100)+1) FROM Articulo, ListaPrecios, ArticuloPrecioVenta";
                                        // if (conn.ActualizarListaPrecioVenta("ArticuloPrecioVenta", actualizar, "ListaPrecios.IDLISTAPRECIO = ArticuloPrecioVenta.IDLISTAPRECIO AND Articulo.IDARTICULO = ArticuloPrecioVenta.IDARTICULO AND ListaPrecios.IDLISTAPRECIO= " + IdListaPrecioVta + "  AND Articulo.idestado = 1 AND Articulo.IdArticulo = " + iIDarticulo + " AND Articulo.Codigo = '" + sCodigoArtic + "'"))
                                        //     MostrarDatos();
                                        // else
                                        //     MessageBox.Show("Error al Vincular a la lista seleccionada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        conn.DesconectarBD();
                                    }
                                    MessageBox.Show("Nuevo Artículo Codigo: " + sCodigoArtic + " insertado en la lista de precios de venta.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    MostrarDatos();
                                }

                            }
                            break;
                        }

                    case DialogResult.No: //rearma la lista de precio con todos los articulos existentena la fecha y recalcula los precios
                        {
                            DialogResult result2 = MessageBox.Show("¿Desea actualizar todas las listas y artículos?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            switch (result2)
                            {
                                case DialogResult.Yes: //Inserta articulos que se han dado de alta pero que no existen en la lista de precios de vanta
                                    {
                                        int IdListaPrecio;

                                        ///Lee las listas existentes y comienza a actualizar y calcular una por una///
                                        SqlCommand cmLeeLista = new SqlCommand("SELECT * FROM ListaPrecios ORDER BY IdListaPrecio", conectaEstado);

                                        SqlDataAdapter daLeeLista = new SqlDataAdapter(cmLeeLista);
                                        DataTable dtLeeLista = new DataTable();
                                        daLeeLista.Fill(dtLeeLista);

                                        foreach (DataRow drLeeLista in dtLeeLista.Rows)
                                        {
                                            IdListaPrecio = Convert.ToInt32(drLeeLista["IdListaPrecio"].ToString().Trim());

                                            if (txtCodLista.Text.Trim() != "-")
                                            {
                                                conn.EliminarArticulo("ArticuloPrecioVenta", " IdListaPrecio = " + IdListaPrecio);
                                                VerificaNuevosArticulos(IdListaPrecio);
                                                lvwListaPrecioVenta.Items.Clear();
                                            }

                                            //string agregar = "INSERT INTO ArticuloPrecioVenta (IDLISTAPRECIO, IDARTICULO, PRECIO) SELECT ListaPrecios.IDLISTAPRECIO, Articulo.IDARTICULO, replace((Cast(replace(Articulo.COSTOENLISTA, ',','.') as decimal(10,2)) * ((Cast(replace(ListaPrecios.PORCENTAJE, ',','.') as decimal(10,2))/100)+1) *  ((ListaPrecios.PORCFLETE/100)+1)), '.', ',') AS PRECIO FROM ListaPrecios, Articulo WHERE ListaPrecios.IDLISTAPRECIO = " + cboBuscar.SelectedValue + " AND Articulo.idestado=1 ORDER BY Articulo.Idarticulo;"; //ORIGINAL 1.0 VINCULA DE A UN ART A LA LISTA
                                            string agregar = "INSERT INTO ArticuloPrecioVenta (IDLISTAPRECIO, IDARTICULO, PRECIO) SELECT ListaPrecios.IDLISTAPRECIO, Articulo.IDARTICULO, replace((Cast(replace(Articulo.COSTOENLISTA, ',','.') as decimal(10,3)) * ((Cast(replace(ListaPrecios.PORCENTAJE, ',','.') as decimal(10,3))/100)+1) *  ((ListaPrecios.PORCFLETE/100)+1)), '.', ',') AS PRECIO FROM ListaPrecios, Articulo WHERE ListaPrecios.IDLISTAPRECIO = " + IdListaPrecio + " AND Articulo.idestado=1 ORDER BY Articulo.Idarticulo;";   //QUERY MODIFICADO 1.1 VINCULA ART A TODAS LAS LISTA
                                            string actualizar = "ArticuloPrecioVenta.PRECIO = Cast(replace(Articulo.COSTOENLISTA, ',','.') as decimal(10,3)) * ((Cast(replace(ListaPrecios.PORCENTAJE, ',','.') as decimal(10,3))/100)+1) FROM Articulo, ListaPrecios, ArticuloPrecioVenta";

                                            conn.DesconectarBD();
                                            if (this.lvwListaPrecioVenta.Items.Count == 0)
                                            {
                                                //Generador de lista de precio
                                                if (conn.InsertarListaPrecioVenta(agregar))
                                                {
                                                    MostrarDatos();
                                                    MessageBox.Show("Artículos Vinculados a la lista N° " + IdListaPrecio + "", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                                else
                                                    MessageBox.Show("Error al Vincular a la lista seleccionada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            else
                                                if (conn.ActualizarListaPrecioVenta("ArticuloPrecioVenta", actualizar, "ListaPrecios.IDLISTAPRECIO = ArticuloPrecioVenta.IDLISTAPRECIO AND Articulo.IDARTICULO = ArticuloPrecioVenta.IDARTICULO AND ListaPrecios.IDLISTAPRECIO= " + IdListaPrecio + ""))
                                            {
                                                MostrarDatos();
                                                MessageBox.Show("Lista Actualizada N° " + IdListaPrecio + "");
                                            }
                                            else
                                                MessageBox.Show("Error al actualizar la Lista");
                                        }
                                        cmLeeLista.Connection.Close();

                                        break;
                                    }
                                case DialogResult.No: //rearma la lista de precio con todos los articulos existentena la fecha y recalcula los precios
                                    {

                                        break;
                                    }
                            }

                            break;
                        }
                }
            }

            catch { MessageBox.Show("Error al actualizar la Lista"); }
        }

        private bool VerificaNuevosArticulos(int NLista) {   
            conn.LeeArticulo("SELECT COUNT(*) AS CantidadArticulo FROM Articulo", "Articulo");
            NumArt =  Convert.ToInt32(conn.leer["CantidadArticulo"]);
            conn.DesconectarBDLee();

            conn.LeeArticulo("SELECT COUNT(*) AS CantidadArticulo FROM ArticuloPrecioVenta WHERE IDLISTAPRECIO=" + NLista + "", "ArticuloPrecioVenta");
            CantLista = Convert.ToInt32(conn.leer["CantidadArticulo"]);
            conn.DesconectarBDLee();

            if (CantLista == 0)
                return false;
            if (NumArt == CantLista)
                return false;
            else
                return true;        
        }

        private void tsBtnModificar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = true;
            lvwListaPrecioVenta.Height = 340;
            gbListaPrecio.Height = 400;            
        }

        private void btcCerrar_Click(object sender, EventArgs e) {
            gpoCliente.Visible = false;
            lvwListaPrecioVenta.Height = 450;
            gbListaPrecio.Height = 510;            
        }

        private void cboBuscar_Enter(object sender, EventArgs e) {
            LlenarComboBuscar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboBuscarArt.SelectedIndex == 0)
                {
                    BuscarDatos2();
                }

                else if (cboBuscarArt.SelectedIndex == 1)
                {
                    BuscarDatos1();
                }
            }
            catch { }
        }

        /// ///////////////////////////////////////////////////BLOQUE DE BUSQUEDA//////////////////////////////////////////////////////////////
        public void BuscarDatos1()
        {
            try
            {
                lvwListaPrecioVenta.Items.Clear();

                decimal fPrecioVentaFinal;
                decimal fAlicuota;

                SqlCommand cm = new SqlCommand("SELECT Articulo.IDARTICULO, Articulo.IDIMPUESTO, Impuesto.ALICUOTA, ListaPrecios.IDLISTAPRECIO AS 'Código Lista', ListaPrecios.DESCRIPCION AS 'Nombre Lista', ListaPrecios.PORCENTAJE AS 'Porcentaje', ListaPrecios.PORCFLETE AS 'Flete', Articulo.CODIGO AS 'Código Artículo', Articulo.DESCRIPCION AS 'Descripción Artículo', ArticuloPrecioVenta.Precio AS 'Precio Venta', ArticuloPrecioVenta.ultimactualizacion, Articulo.CostoEnLista as 'CostoEnLista', Articulo.COSTO As 'Costo Articulo', CASE WHEN (Cast(replace(Articulo.COSTO, ',', '.') as decimal(10,2))) > (Cast(replace(ArticuloPrecioVenta.Precio, ',', '.') as decimal(10,2))) OR (Cast(replace(ArticuloPrecioVenta.Precio, ',', '.') as decimal(10,2))) = 0 THEN 'Precio NACT' ELSE 'Precio ACT' END AS 'Situacion Lista' FROM ArticuloPrecioVenta,ListaPrecios,Articulo,Impuesto WHERE (ListaPrecios.IDLISTAPRECIO=ArticuloPrecioVenta.IDLISTAPRECIO AND Articulo.IDARTICULO=ArticuloPrecioVenta.IDARTICULO) AND ListaPrecios.IDLISTAPRECIO=" + cboBuscar.SelectedValue + " AND Articulo.CODIGO LIKE '" + txtConsulta.Text.Trim() + "%' AND Articulo.IdEstado=1 AND Impuesto.IDIMPUESTO = articulo.IDIMPUESTO ORDER BY Articulo.Descripcion", conectaEstado);
                
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwListaPrecioVenta.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Código Artículo"].ToString());
                    item.SubItems.Add(dr["Descripción Artículo"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["CostoEnLista"]), 3).ToString());

                    item.SubItems.Add(String.Format("{0:0,000}", Math.Round(Convert.ToDecimal(dr["Precio Venta"]), 3).ToString()));

                    /*if (cboBuscar.SelectedValue.ToString().Trim() != "1002")
                    {*/
                        ///Muestra y calcula precio final iva incl.
                        fPrecioVentaFinal = Convert.ToDecimal(dr["Precio Venta"].ToString());
                        fAlicuota = Convert.ToDecimal(dr["Alicuota"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(fPrecioVentaFinal + (fPrecioVentaFinal * fAlicuota) / 100), 3).ToString(), Color.Empty, Color.LightGray, null);
                    /*}
                    else
                    {
                        ///Muestra y calcula precio final iva incl. Redondeo
                        fPrecioVentaFinal = Convert.ToDecimal(dr["Precio Venta"].ToString());
                        fAlicuota = Convert.ToDecimal(dr["Alicuota"].ToString());
                        item.SubItems.Add(String.Format("{0:0,00}", Math.Round(Convert.ToDecimal(fPrecioVentaFinal + (fPrecioVentaFinal * fAlicuota) / 100), 1).ToString(), Color.Empty, Color.LightGray, null));
                    //}*/
                    ///////////////////////////////////////////

                    item.SubItems.Add(dr["Situacion Lista"].ToString());
                    item.SubItems.Add(dr["Código Lista"].ToString());
                    item.SubItems.Add(dr["Nombre Lista"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Porcentaje"]), 2).ToString());
                    item.SubItems.Add(dr["IDARTICULO"].ToString());
                    item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Flete"]), 2).ToString());

                    item.ImageIndex = 0;

                    if (item.SubItems[5].Text == "Precio NACT")
                        item.ImageIndex = 1;
                    else
                        item.ImageIndex = 0;

                    DateTime fechaArticulo;
                    fechaArticulo = Convert.ToDateTime(dr["Ultimactualizacion"].ToString());


                    if (fechaArticulo.AddDays(15) >= DateTime.Today)
                        item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["Ultimactualizacion"].ToString())), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold));
                    else if (fechaArticulo.AddDays(30) >= DateTime.Today)
                        item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["Ultimactualizacion"].ToString())), Color.Snow, Color.DarkOrange, new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold));
                    else
                        item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["Ultimactualizacion"].ToString())), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold));


                    item.UseItemStyleForSubItems = false;

                    lvwListaPrecioVenta.Items.Add(item);
                }
                if (lvwListaPrecioVenta.SelectedIndices.Count != 0)
                    codArt = lvwListaPrecioVenta.SelectedItems[0].SubItems[0].Text;

                cm.Connection.Close();
            }
            catch { }
        }

        public void BuscarDatos2()
        {
            try
            {
                lvwListaPrecioVenta.Items.Clear();

                decimal fPrecioVentaFinal;
                decimal fAlicuota;

                if ((cboBuscarArt.SelectedIndex == 1 && txtConsulta.Text == "") || txtConsulta.Text == "*")
                {
                    if (txtConsulta.Text == "*")
                        txtConsulta.Text = "";

                    SqlCommand cm = new SqlCommand("SELECT Articulo.IDARTICULO, Articulo.IDIMPUESTO, Impuesto.ALICUOTA, ListaPrecios.IDLISTAPRECIO AS 'Código Lista', ListaPrecios.DESCRIPCION AS 'Nombre Lista', ListaPrecios.PORCENTAJE AS 'Porcentaje', ListaPrecios.PORCFLETE AS 'Flete', Articulo.CODIGO AS 'Código Artículo', Articulo.DESCRIPCION AS 'Descripción Artículo', ArticuloPrecioVenta.Precio AS 'Precio Venta', ArticuloPrecioVenta.ultimactualizacion, Articulo.CostoEnLista as 'CostoEnLista', Articulo.COSTO As 'Costo Articulo', CASE WHEN (Cast(replace(Articulo.COSTO, ',', '.') as decimal(10,2))) > (Cast(replace(ArticuloPrecioVenta.Precio, ',', '.') as decimal(10,2))) OR (Cast(replace(ArticuloPrecioVenta.Precio, ',', '.') as decimal(10,2))) = 0 THEN 'Precio NACT' ELSE 'Precio ACT' END AS 'Situacion Lista' FROM ArticuloPrecioVenta,ListaPrecios,Articulo,Impuesto  WHERE (ListaPrecios.IDLISTAPRECIO=ArticuloPrecioVenta.IDLISTAPRECIO AND Articulo.IDARTICULO=ArticuloPrecioVenta.IDARTICULO) AND ListaPrecios.IDLISTAPRECIO=" + cboBuscar.SelectedValue + " AND Articulo.Codigo LIKE '" + txtConsulta.Text.Trim() + "%' AND Articulo.IdEstado=1 AND Impuesto.IDIMPUESTO = articulo.IDIMPUESTO ORDER BY Articulo.Descripcion", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {                        
                        ListViewItem item = new ListViewItem(dr["Código Artículo"].ToString());
                        item.SubItems.Add(dr["Descripción Artículo"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["CostoEnLista"]), 3).ToString());

                        item.SubItems.Add(String.Format("{0:0,000}", Math.Round(Convert.ToDecimal(dr["Precio Venta"]), 3).ToString()));

                        /*if (cboBuscar.SelectedValue.ToString().Trim() != "1002")
                        {*/
                            ///Muestra y calcula precio final iva incl.
                            fPrecioVentaFinal = Convert.ToDecimal(dr["Precio Venta"].ToString());
                            fAlicuota = Convert.ToDecimal(dr["Alicuota"].ToString());
                            item.SubItems.Add(Math.Round(Convert.ToDecimal(fPrecioVentaFinal + (fPrecioVentaFinal * fAlicuota) / 100), 3).ToString(), Color.Empty, Color.LightGray, null);
                        /*}
                        else
                        {
                            ///Muestra y calcula precio final iva incl. Redondeo
                            fPrecioVentaFinal = Convert.ToDecimal(dr["Precio Venta"].ToString());
                            fAlicuota = Convert.ToDecimal(dr["Alicuota"].ToString());
                            item.SubItems.Add(String.Format("{0:0,00}", Math.Round(Convert.ToDecimal(fPrecioVentaFinal + (fPrecioVentaFinal * fAlicuota) / 100), 1).ToString(), Color.Empty, Color.LightGray, null));
                        }*/
                        ///////////////////////////////////////////

                        item.SubItems.Add(dr["Situacion Lista"].ToString());
                        item.SubItems.Add(dr["Código Lista"].ToString());
                        item.SubItems.Add(dr["Nombre Lista"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Porcentaje"]), 2).ToString());
                        item.SubItems.Add(dr["IDARTICULO"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Flete"]), 2).ToString());

                        item.ImageIndex = 0;

                        if (item.SubItems[5].Text == "Precio NACT")
                            item.ImageIndex = 1;
                        else
                            item.ImageIndex = 0;

                        DateTime fechaArticulo;
                        fechaArticulo = Convert.ToDateTime(dr["Ultimactualizacion"].ToString());


                        if (fechaArticulo.AddDays(15) >= DateTime.Today)
                            item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["Ultimactualizacion"].ToString())), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold));
                        else if (fechaArticulo.AddDays(30) >= DateTime.Today)
                            item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["Ultimactualizacion"].ToString())), Color.Snow, Color.DarkOrange, new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold));
                        else
                            item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["Ultimactualizacion"].ToString())), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold));


                        item.UseItemStyleForSubItems = false;

                        lvwListaPrecioVenta.Items.Add(item);

                    }
                    if (lvwListaPrecioVenta.SelectedIndices.Count != 0)
                        codArt = lvwListaPrecioVenta.SelectedItems[0].SubItems[0].Text;
                    cm.Connection.Close();

                }

                else if (cboBuscarArt.SelectedIndex == 0 && txtConsulta.Text != "")
                {
                    SqlCommand cm = new SqlCommand("SELECT Articulo.IDARTICULO,  Articulo.IDIMPUESTO, Impuesto.ALICUOTA, ListaPrecios.IDLISTAPRECIO AS 'Código Lista', ListaPrecios.DESCRIPCION AS 'Nombre Lista', ListaPrecios.PORCENTAJE AS 'Porcentaje', ListaPrecios.PORCFLETE AS 'Flete', Articulo.CODIGO AS 'Código Artículo', Articulo.DESCRIPCION AS 'Descripción Artículo', ArticuloPrecioVenta.Precio AS 'Precio Venta', ArticuloPrecioVenta.ultimactualizacion, Articulo.CostoEnLista as 'CostoEnLista', Articulo.COSTO As 'Costo Articulo', CASE WHEN (Cast(replace(Articulo.COSTO, ',', '.') as decimal(10,2))) > (Cast(replace(ArticuloPrecioVenta.Precio, ',', '.') as decimal(10,2))) OR (Cast(replace(ArticuloPrecioVenta.Precio, ',', '.') as decimal(10,2))) = 0 THEN 'Precio NACT' ELSE 'Precio ACT' END AS 'Situacion Lista' FROM ArticuloPrecioVenta,ListaPrecios,Articulo,Impuesto WHERE (ListaPrecios.IDLISTAPRECIO=ArticuloPrecioVenta.IDLISTAPRECIO AND Articulo.IDARTICULO=ArticuloPrecioVenta.IDARTICULO) AND ListaPrecios.IDLISTAPRECIO=" + cboBuscar.SelectedValue + " AND Articulo.Descripcion LIKE '" + txtConsulta.Text.Trim() + "%' AND Articulo.IdEstado=1 AND Impuesto.IDIMPUESTO = articulo.IDIMPUESTO ORDER BY Articulo.Descripcion", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {                        
                        ListViewItem item = new ListViewItem(dr["Código Artículo"].ToString());
                        item.SubItems.Add(dr["Descripción Artículo"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["CostoEnLista"]), 3).ToString());

                        item.SubItems.Add(String.Format("{0:0,000}", Math.Round(Convert.ToDecimal(dr["Precio Venta"]), 3).ToString()));

                        /*if (cboBuscar.SelectedValue.ToString().Trim() != "1002")
                        {*/
                            ///Muestra y calcula precio final iva incl.
                            fPrecioVentaFinal = Convert.ToDecimal(dr["Precio Venta"].ToString());
                            fAlicuota = Convert.ToDecimal(dr["Alicuota"].ToString());
                            item.SubItems.Add(Math.Round(Convert.ToDecimal(fPrecioVentaFinal + (fPrecioVentaFinal * fAlicuota) / 100), 3).ToString(), Color.Empty, Color.LightGray, null);
                        /*}
                        else
                        {
                            ///Muestra y calcula precio final iva incl. Redondeo
                            fPrecioVentaFinal = Convert.ToDecimal(dr["Precio Venta"].ToString());
                            fAlicuota = Convert.ToDecimal(dr["Alicuota"].ToString());
                            item.SubItems.Add(String.Format("{0:0,00}", Math.Round(Convert.ToDecimal(fPrecioVentaFinal + (fPrecioVentaFinal * fAlicuota) / 100), 1).ToString(), Color.Empty, Color.LightGray, null));
                        }*/
                        ///////////////////////////////////////////

                        item.SubItems.Add(dr["Situacion Lista"].ToString());
                        item.SubItems.Add(dr["Código Lista"].ToString());
                        item.SubItems.Add(dr["Nombre Lista"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Porcentaje"]), 2).ToString());
                        item.SubItems.Add(dr["IDARTICULO"].ToString());
                        item.SubItems.Add(Math.Round(Convert.ToDecimal(dr["Flete"]), 2).ToString());

                        item.ImageIndex = 0;

                        if (item.SubItems[5].Text == "Precio NACT")
                            item.ImageIndex = 1;
                        else
                            item.ImageIndex = 0;

                        DateTime fechaArticulo;
                        fechaArticulo = Convert.ToDateTime(dr["Ultimactualizacion"].ToString());


                        if (fechaArticulo.AddDays(15) >= DateTime.Today)
                            item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["Ultimactualizacion"].ToString())), Color.Snow, Color.DarkGreen, new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold));
                        else if (fechaArticulo.AddDays(30) >= DateTime.Today)
                            item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["Ultimactualizacion"].ToString())), Color.Snow, Color.DarkOrange, new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold));
                        else
                            item.SubItems.Add(String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["Ultimactualizacion"].ToString())), Color.Snow, Color.DarkRed, new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold));


                        item.UseItemStyleForSubItems = false;

                        lvwListaPrecioVenta.Items.Add(item);
                    }
                    codArt = lvwListaPrecioVenta.SelectedItems[0].SubItems[0].Text;
                    cm.Connection.Close();
                }

            }
            catch { }
        }

        private void lvwListaPrecioVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //timer1.Enabled = false;

                conn.DesconectarBD();
                conn.DesconectarBDLee();

                //nuevoRemito = false;

                //idNroRemitoInterno = Convert.ToInt32(lvwListaPrecioVenta.SelectedItems[0].SubItems[0].Text);
                //indiceLvwNotaPedido = lvwListaPrecioVenta.SelectedItems[0].Index;

                conn.LeeArticulo("SELECT * FROM  Articulo WHERE Codigo = '" + lvwListaPrecioVenta.SelectedItems[0].SubItems[0].Text.Trim() + "'", "Articulo");
                codArt = lvwListaPrecioVenta.SelectedItems[0].SubItems[0].Text.Trim();
                idArticulo = Convert.ToInt32(conn.leer["IdArticulo"].ToString());

                //this.txtNroNotaPedido.Text = conn.leerGeneric["IDNOTAPEDIDO"].ToString();

                this.txtCodigo.Text = conn.leer["Codigo"].ToString();
                this.txtDescripcionArticulo.Text = conn.leer["Descripcion"].ToString();

                //if (cboBuscar.SelectedValue.ToString().Trim() != "1002")
                    this.txtDescripListaPrecio.Text = String.Format("{0:0.000}", Decimal.Round(Convert.ToDecimal(conn.leer["Costo"].ToString()), 3));                
                //else
                //    this.txtDescripListaPrecio.Text = String.Format("{0:0.00}", Decimal.Round(Convert.ToDecimal(conn.leer["Costo"].ToString()), 1));

                conn.DesconectarBD();
                conn.DesconectarBDLee();
            }
            catch { conn.DesconectarBD(); }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {    
            try
            {
                //Actualiza Costo Articulos//
                //Actualiza Lista de Precio//
                //Actualiza Remito//
                //Actualiza Nota Pedido//
                //timer1.Enabled = false;

                int idDetalleNotaPedido;
                int idNotaPedido;
                DateTime fechaNota;
                int IdArticuloDetalleNP;

                //Nota Pedido   
                double BasicoNotapedido = 0;
                double Impuesto1NotaPedido = 0;
                double Impuesto2NotaPedido = 0;
                double ImporteTotalNotaPedido = 0;
                //Detalle Nota Pedido
                double PrecioUnitArtDetalleNP = 0;
                double BasicoArtDetalleNP = 0;
                int CantArtEntrega = 0;
                double Imp1ArtDetalleNP = 0;
                double Imp2ArtDetalleNP = 0;
                double ImporteTotalArtDetalleNP = 0;

                double Porcentaje_Lista = 0;
                double Flete_Lista = 0;
                int IdListaPrecio;
                
                ///Lee las listas existentes y comienza a actualizar y calcular una por una///
                SqlCommand cmLeeLista = new SqlCommand("SELECT * FROM ListaPrecios ORDER BY IdListaPrecio", conectaEstado);                

                SqlDataAdapter daLeeLista = new SqlDataAdapter(cmLeeLista);
                DataTable dtLeeLista = new DataTable();
                daLeeLista.Fill(dtLeeLista);

                foreach (DataRow drLeeLista in dtLeeLista.Rows)
                {
                    IdListaPrecio = Convert.ToInt32(drLeeLista["IdListaPrecio"].ToString().Trim());
                    Porcentaje_Lista = Math.Round(Convert.ToDouble(drLeeLista["Porcentaje"].ToString().Trim()), 3);
                    Flete_Lista = Math.Round(Convert.ToDouble(drLeeLista["PorcFlete"].ToString().Trim()),3);

                    conn.LeeArticulo("SELECT * FROM  Articulo WHERE IdArticulo = " + idArticulo + " AND Codigo = '" + codArt + "'", "Articulo");

                    Costo = String.Format("{0:0.000}", Decimal.Round(Convert.ToDecimal(txtDescripListaPrecio.Text.Trim()), 3));
                    Porcentaje = String.Format("{0:0.000}", Decimal.Round(Convert.ToDecimal(conn.leer["PROCCOSTOENLISTA"].ToString()), 2));
                    resultado = ((Convert.ToDecimal(Costo) * Convert.ToDecimal(Porcentaje) / 100) + (Convert.ToDecimal(Costo)));
                    resultado2 = ((Convert.ToDecimal(resultado) * Convert.ToDecimal(Porcentaje_Lista) / 100) + (Convert.ToDecimal(resultado)));
                    resultado3 = ((Convert.ToDecimal(resultado2) * Convert.ToDecimal(Flete_Lista) / 100) + (Convert.ToDecimal(resultado2)));

                    //Articulo
                    string actualizar = "Costo = (Cast(replace('" + txtDescripListaPrecio.Text.Trim() + "', ',', '.') as decimal(10, 3))), CostoEnLista = (Cast(replace('" + resultado + "', ',', '.') as decimal(10, 3))) ";
                    if (conn.ActualizaArticulo("Articulo", actualizar, " Codigo = '" + codArt + "'"))
                    {
                        //MostrarDatos();                    
                        //MessageBox.Show("El costo del articulo ha sido actualizado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //Lista de Precio
                    string actualizar2 = "Precio ='" + (String.Format("{0:0.000}",resultado3)) + "', Ultimactualizacion='" + String.Format("{0:yyyy-MM-dd}", DateTime.Today) + "' ";
                    if (conn.ActualizaArticulo("ArticuloPrecioVenta", actualizar2, " IdArticulo = " + idArticulo + " AND IdListaPrecio = " + IdListaPrecio + ""))
                    {
                        MostrarDatos();
                        //MessageBox.Show("El precio de lista ha sido actualizado en todas las listas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    /*  
                      else
                          MessageBox.Show("No se ha podido actualizar los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     */ 
                }

                cmLeeLista.Connection.Close();
                MessageBox.Show("El costo del articulo ha sido actualizado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("El precio de lista ha sido actualizado en todas las listas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);


                //////////////////////////////////////////////////// FIN ACTUALIZACION LISTAS DE PRECIO ////////////////////////////////////////////////////
                ///CAMBIO DE NOTA DE PEDIDO - DETALLE NOTA PEDIDO///                
                SqlCommand cm = new SqlCommand("SELECT NotaPedido.IDNOTAPEDIDO, NotaPedido.FECHANOTA, NotaPedido.BASICO, NotaPedido.DESCUENTO, NotaPedido.IMPUESTO1, NotaPedido.IMPUESTO2, NotaPedido.TOTAL FROM Notapedido, DetalleNotaPedido WHERE Notapedido.Idnotapedido = DetalleNotaPedido.IdNotaPedido AND NotaPedido.IDEMPRESA = " + IDEMPRESA + " AND DetalleNotaPedido.IdArticulo = " + idArticulo + " ORDER BY Notapedido.IDNOTAPEDIDO", conectaEstado);
                
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    idNotaPedido = Convert.ToInt32(dr["IdNotapedido"].ToString().Trim());
                    fechaNota = Convert.ToDateTime(dr["FechaNota"].ToString().Trim()); 

                    if (fechaNota.AddDays(30) >= DateTime.Today)
                    {
                        //SqlCommand cm1 = new SqlCommand("SELECT DetalleNotaPedido.IDDETALLENOTAPEDIDO,  DetalleNotaPedido.IDNOTAPEDIDO, DetalleNotaPedido.IDARTICULO, DetalleNotaPedido.CANTIDADPEDIDA, DetalleNotaPedido.PRECUNITARIO, DetalleNotaPedido.SUBTOTAL, DetalleNotaPedido.DESCUENTO, DetalleNotaPedido.IMPUESTO1, DetalleNotaPedido.IMPUESTO2, DetalleNotaPedido.IMPORTE, DetalleNotaPedido.REMITIDO FROM DetalleNotaPedido WHERE IDNOTAPEDIDO = " + idNotaPedido + " AND IdArticulo = " + idArticulo + "", conectaEstado);
                        SqlCommand cm1 = new SqlCommand("SELECT ListaPrecios.IDLISTAPRECIO, NotaPedido.IDCLIENTE, DetalleNotaPedido.IDDETALLENOTAPEDIDO,  DetalleNotaPedido.IDNOTAPEDIDO, DetalleNotaPedido.IDARTICULO, DetalleNotaPedido.CANTIDADPEDIDA, DetalleNotaPedido.PRECUNITARIO, DetalleNotaPedido.SUBTOTAL, DetalleNotaPedido.DESCUENTO, DetalleNotaPedido.IMPUESTO1, DetalleNotaPedido.IMPUESTO2, DetalleNotaPedido.IMPORTE, DetalleNotaPedido.REMITIDO FROM DetalleNotaPedido, NotaPedido, Cliente, ListaPrecios WHERE DetalleNotaPedido.IDNOTAPEDIDO = " + idNotaPedido + " AND NotaPedido.IDEMPRESA = "+ IDEMPRESA + " AND DetalleNotaPedido.IDARTICULO = " + idArticulo + " AND DetalleNotaPedido.REMITIDO = 'NO' AND Cliente.IDLISTAPRECIO = ListaPrecios.IDLISTAPRECIO AND NotaPedido.IDCLIENTE = Cliente.IDCLIENTE", conectaEstado);

                        SqlDataAdapter da1 = new SqlDataAdapter(cm1);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);                        

                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            resultado3 = CalculaPrecioSegunListaDeCliente(Convert.ToInt32(dr1["IDLISTAPRECIO"].ToString()), Convert.ToInt32(dr1["IDARTICULO"].ToString()));

                            idDetalleNotaPedido = Convert.ToInt32(dr1["IdDetalleNotaPedido"].ToString().Trim());
                            IdArticuloDetalleNP = Convert.ToInt32(dr1["IdArticulo"].ToString().Trim());

                            CantArtEntrega = Convert.ToInt32(dr1["CantidadPedida"].ToString().Trim());
                            PrecioUnitArtDetalleNP = Math.Round(Convert.ToDouble(resultado3),3);
                            BasicoArtDetalleNP = BasicoArtDetalleNP = Math.Round((PrecioUnitArtDetalleNP) * (CantArtEntrega), 3);


                            Imp1ArtDetalleNP = Math.Round(Convert.ToDouble(dr1["Impuesto1"].ToString().Trim()),3);
                            Imp2ArtDetalleNP = Math.Round(Convert.ToDouble(dr1["Impuesto2"].ToString().Trim()),3);

                            if (Imp2ArtDetalleNP != 0)
                            {
                                Imp2ArtDetalleNP = Math.Round((BasicoArtDetalleNP * 21 / 100), 3);
                                ImporteTotalArtDetalleNP = Math.Round((Convert.ToDouble(BasicoArtDetalleNP + Imp2ArtDetalleNP)), 3);                                
                            }
                            else
                            {
                                Imp1ArtDetalleNP = Math.Round((BasicoArtDetalleNP * 10.5 / 100), 3);
                                ImporteTotalArtDetalleNP = Math.Round((Convert.ToDouble(BasicoArtDetalleNP + Imp1ArtDetalleNP)), 3);
                            }

                            //Actualiza Detalle Nota Pedido
                            string actualizar3 = "PrecUnitario = (Cast(replace('" + PrecioUnitArtDetalleNP + "', ',', '.') as decimal(10, 3))), SubTotal = (Cast(replace('" + BasicoArtDetalleNP + "', ',', '.') as decimal(10, 3))), Impuesto1 = (Cast(replace('" + Imp1ArtDetalleNP + "', ',', '.') as decimal(10, 3))), Impuesto2 = (Cast(replace('" + Imp2ArtDetalleNP + "', ',', '.') as decimal(10, 3))), Importe = (Cast(replace('" + ImporteTotalArtDetalleNP + "', ',', '.') as decimal(10, 3))) ";
                            if (conn.ActualizaArticulo("DetalleNotaPedido", actualizar3, "IdDetalleNotaPedido=" + idDetalleNotaPedido + " AND IdArticulo =" + IdArticuloDetalleNP + ""))
                            { }
                            //MessageBox.Show("Detalle de Nota de Pedido Actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                            else
                                MessageBox.Show("Error al actualizar el Detalle de Nota de Pedido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            BasicoNotapedido = Math.Round((BasicoNotapedido +BasicoArtDetalleNP),3);
                            Impuesto1NotaPedido = Math.Round((Impuesto1NotaPedido +Imp1ArtDetalleNP), 3);
                            Impuesto2NotaPedido = Math.Round((Impuesto2NotaPedido +Imp2ArtDetalleNP), 3);
                            ImporteTotalNotaPedido = Math.Round((ImporteTotalNotaPedido +ImporteTotalArtDetalleNP), 3);

                        }
                        cm1.Connection.Close();

                        //Actualiza Nota Pedido
                        string actualizar4 = "Basico = (Cast(replace('" + BasicoNotapedido + "', ',', '.') as decimal(10, 3))), Impuesto1 = (Cast(replace('" + Impuesto1NotaPedido + "', ',', '.') as decimal(10, 3))), Impuesto2 = (Cast(replace('" + Impuesto2NotaPedido + "', ',', '.') as decimal(10, 3))), Total = (Cast(replace('" + ImporteTotalNotaPedido + "', ',', '.') as decimal(10, 3))) ";
                        if (conn.ActualizaArticulo("NotaPedido", actualizar4, " IDNotaPedido = " + idNotaPedido + ""))
                        { }
                        //MessageBox.Show("Nota de Pedido Actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Error al actualizar la Nota de Pedido", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        BasicoNotapedido = 0; Impuesto1NotaPedido = 0; Impuesto2NotaPedido = 0; ImporteTotalNotaPedido = 0;
                    }

                }                
                cm.Connection.Close();
                MessageBox.Show("Notas de Pedido Actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////////////////////////////////////////////////////////////////////////FIN ACTUALIZACION NP/////////////////////////////////////////////////////////////////////

                //Ids
                int idDetalleRemito;
                int idRemito;
                DateTime fechaRemito;
                int IdArticuloDetalleRemito;

                //Remito
                double BasicoRemito = 0;
                double Impuesto1Remito = 0;
                double Impuesto2Remito = 0;
                double ImporteTotalRemito = 0;
                //Detalle Remito
                double PrecioUnitArtDetalleRemito = 0;
                double BasicoArtDetalleRemito = 0;
                int CantArtEntregaRemito = 0;
                double Imp1ArtDetalleRemito = 0;
                double Imp2ArtDetalleRemito = 0;
                double ImporteTotalArtDetalleRemito = 0;

                ///REMITO - DETALLE REMITO///                
                //SqlCommand cm5 = new SqlCommand("SELECT Remito.NROREMITOINTERNO, Remito.FECHA, Remito.BASICO, Remito.DESCUENTO, Remito.IMPUESTO1, Remito.IMPUESTO2, Remito.TOTAL FROM Remito, DetalleRemito WHERE Remito.NROREMITOINTERNO = DetalleRemito.NROREMITOINTERNO AND Remito.IDEMPRESA = " + IDEMPRESA + " AND  DetalleRemito.IDARTICULO = " + idArticulo + " AND Remito.EstadoRemito = 'NO FACTURADO' ORDER BY Remito.NROREMITOINTERNO", conectaEstado);
                SqlCommand cm5 = new SqlCommand("SELECT Remito.NROREMITOINTERNO, Remito.FECHA, Remito.BASICO, Remito.DESCUENTO, Remito.IMPUESTO1, Remito.IMPUESTO2, Remito.TOTAL, Remito.IDCLIENTE, ListaPrecios.IDLISTAPRECIO FROM Remito, DetalleRemito,Cliente, ListaPrecios WHERE Remito.NROREMITOINTERNO = DetalleRemito.NROREMITOINTERNO AND Remito.IDEMPRESA = " + IDEMPRESA + " AND DetalleRemito.IDARTICULO = " + idArticulo + " AND Remito.EstadoRemito = 'NO FACTURADO' AND Remito.IDCLIENTE = Cliente.IDCLIENTE AND Cliente.IDLISTAPRECIO = ListaPrecios.IDLISTAPRECIO ORDER BY Remito.NROREMITOINTERNO", conectaEstado);                                
                
                SqlDataAdapter da5 = new SqlDataAdapter(cm5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);                

                foreach (DataRow dr5 in dt5.Rows)
                {
                    idRemito = Convert.ToInt32(dr5["NRORemitoInterno"].ToString().Trim());
                    fechaRemito = Convert.ToDateTime(dr5["Fecha"].ToString().Trim());                    

                    //if (fechaRemito.AddDays(30) >= DateTime.Today)
                    //{
                        SqlCommand cm6 = new SqlCommand("SELECT DetalleRemito.IDDETALLEREMITO, DetalleRemito.NROREMITOINTERNO, DetalleRemito.IDARTICULO, DetalleRemito.CANTIDAD, DetalleRemito.PRECUNITARIO, DetalleRemito.SUBTOTAL, DetalleRemito.DESCUENTO, DetalleRemito.IMPUESTO1, DetalleRemito.IMPUESTO2, DetalleRemito.IMPORTE FROM DetalleRemito WHERE DetalleRemito.NROREMITOINTERNO =  " + idRemito + " AND IdArticulo = " + idArticulo + "", conectaEstado);

                        SqlDataAdapter da6 = new SqlDataAdapter(cm6);
                        DataTable dt6 = new DataTable();
                        da6.Fill(dt6);                        

                        foreach (DataRow dr6 in dt6.Rows)
                        {
                            resultado3 = CalculaPrecioSegunListaDeCliente(Convert.ToInt32(dr5["IDLISTAPRECIO"].ToString()), Convert.ToInt32(dr6["IDARTICULO"].ToString()));

                            idDetalleRemito = Convert.ToInt32(dr6["IdDetalleRemito"].ToString().Trim());
                            IdArticuloDetalleRemito = Convert.ToInt32(dr6["IdArticulo"].ToString().Trim());

                            CantArtEntregaRemito = Convert.ToInt32(dr6["Cantidad"].ToString().Trim());
                            
                            PrecioUnitArtDetalleRemito = Math.Round(Convert.ToDouble(resultado3),3);
                            BasicoArtDetalleRemito = Math.Round((PrecioUnitArtDetalleRemito) * (CantArtEntregaRemito), 3);                            

                            Imp1ArtDetalleRemito = Math.Round(Convert.ToDouble(dr6["Impuesto1"].ToString().Trim()), 3);
                            Imp2ArtDetalleRemito = Math.Round(Convert.ToDouble(dr6["Impuesto2"].ToString().Trim()), 3);

                            if (Imp2ArtDetalleRemito != 0)
                            {
                                Imp2ArtDetalleRemito = Math.Round((BasicoArtDetalleRemito * 21 / 100), 3);
                                ImporteTotalArtDetalleRemito = Math.Round((Convert.ToDouble(BasicoArtDetalleRemito + Imp2ArtDetalleRemito)), 3);
                            }
                            else
                            {                                
                                Imp1ArtDetalleRemito = Math.Round((BasicoArtDetalleRemito * 10.5 / 100), 3);
                                ImporteTotalArtDetalleRemito = Math.Round((Convert.ToDouble(BasicoArtDetalleRemito + Imp1ArtDetalleRemito)), 3);
                            }

                            //Actualiza Detalle Remito
                            string actualizar3 = "PrecUnitario = (Cast(replace('" + PrecioUnitArtDetalleRemito + "', ',', '.') as decimal(10, 3))), SubTotal = (Cast(replace('" + BasicoArtDetalleRemito + "', ',', '.') as decimal(10, 3))), Impuesto1 = (Cast(replace('" + Imp1ArtDetalleRemito + "', ',', '.') as decimal(10, 3))), Impuesto2 = (Cast(replace('" + Imp2ArtDetalleRemito + "', ',', '.') as decimal(10, 3))), Importe = (Cast(replace('" + ImporteTotalArtDetalleRemito + "', ',', '.') as decimal(10, 3))) ";
                            if (conn.ActualizaArticulo("DetalleRemito", actualizar3, "IdDetalleRemito = " + idDetalleRemito + " AND IdArticulo =" + IdArticuloDetalleRemito + ""))
                            { }
                            //MessageBox.Show("Detalle de Nota de Pedido Actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                            else
                                MessageBox.Show("Error al actualizar el Detalle de remito", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            BasicoRemito = Math.Round((BasicoRemito + BasicoArtDetalleNP), 3);
                            Impuesto1Remito = Math.Round((Impuesto1Remito + Imp1ArtDetalleRemito), 3);
                            Impuesto2Remito = Math.Round((Impuesto2Remito + Imp2ArtDetalleRemito), 3);
                            ImporteTotalRemito = Math.Round((ImporteTotalRemito + ImporteTotalArtDetalleNP), 3);
                        }
                        cm6.Connection.Close();

                        //Actualiza Remito
                        string actualizar4 = "Basico = (Cast(replace('" + BasicoRemito + "', ',', '.') as decimal(10, 3))), Impuesto1 = (Cast(replace('" + Impuesto1Remito + "', ',', '.') as decimal(10, 3))), Impuesto2 = (Cast(replace('" + Impuesto2Remito + "', ',', '.') as decimal(10, 3))), Total = (Cast(replace('" + ImporteTotalRemito + "', ',', '.') as decimal(10, 3))) ";
                        if (conn.ActualizaArticulo("Remito", actualizar4, " NRORemitoInterno = " + idRemito + ""))
                        { }
                        //MessageBox.Show("Nota de Pedido Actualizada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Error al actualizar el remito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        BasicoRemito = 0; Impuesto1Remito = 0; Impuesto2Remito = 0; ImporteTotalRemito = 0;
                   // }
                    
                }
                cm5.Connection.Close();
                MessageBox.Show("Remitos Actualizado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                /////////////////////////////////////////////////////////////////////FIN ACTUALIZACION REMITO///////////////////////////////////////////////////////////////////                

                //////////////////////////////////////////////////////////FIN PROCESO DE ACTUALIZACION DE PRECIOS///////////////////////////////////////////////////////////////
            }
            catch { MessageBox.Show("Error: No se ha podido actualizar la información", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            //////////////////////////////////////////////////////////
        }

        private decimal CalculaPrecioSegunListaDeCliente(int iIdListaPrecioCliente, int iIdArticulos)
        {
            ///Lee las listas existentes y comienza a actualizar y calcular una por una///
            SqlCommand cmLeeLista = new SqlCommand("SELECT ArticuloPrecioVenta.*, Articulo.CODIGO, Articulo.DESCRIPCION, ListaPrecios.* FROM Articulo, ArticuloPrecioVenta, ListaPrecios WHERE Articulo.IDARTICULO = ArticuloPrecioVenta.IDARTICULO AND ArticuloPrecioVenta.IDLISTAPRECIO = " + iIdListaPrecioCliente + " AND Articulo.IDARTICULO = "+ iIdArticulos + " AND ArticuloPrecioVenta.IDLISTAPRECIO = ListaPrecios.IDLISTAPRECIO ORDER BY ArticuloPrecioVenta.IDARTICULO", conectaEstado);

            SqlDataAdapter daLeeLista = new SqlDataAdapter(cmLeeLista);
            DataTable dtLeeLista = new DataTable();
            daLeeLista.Fill(dtLeeLista);
                        
            foreach (DataRow drLeeLista in dtLeeLista.Rows)
                resultado3 = Math.Round(Convert.ToDecimal(drLeeLista["precio"].ToString().Trim()), 3);
            
            return resultado3;
        }

        private void ActualizaSaldo(int idNotaPedidoInterno)
        {
            try
            {
                
            }
            catch { }
        }

        private void txtDescripListaPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == ".")
            {
                e.Handled = true;
                this.txtDescripListaPrecio.Text += ",";
                SendKeys.Send("{END}");
            }
        }

        private void txtDescripListaPrecio_Leave(object sender, EventArgs e)
        {
            this.txtDescripListaPrecio.Text = "" + this.txtDescripListaPrecio.Text.Trim();
            this.txtDescripListaPrecio.Text = this.txtDescripListaPrecio.Text.Trim();
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void tsBtnVincularSinFechaActualizacion_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("¿Desea actualizar todos los precios y porcentajes de la lista Público?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                switch (result)
                {
                    case DialogResult.Yes: //Inserta articulos que se han dado de alta pero que no existen en la lista de precios de vanta
                        {
                            int IdListaPrecio;

                            ///Lee las listas existentes y comienza a actualizar y calcular una por una///
                            SqlCommand cmLeeLista = new SqlCommand("SELECT * FROM ListaPrecios WHERE IDLISTAPRECIO=1002 ORDER BY IdListaPrecio", conectaEstado);

                            SqlDataAdapter daLeeLista = new SqlDataAdapter(cmLeeLista);
                            DataTable dtLeeLista = new DataTable();
                            daLeeLista.Fill(dtLeeLista);

                            foreach (DataRow drLeeLista in dtLeeLista.Rows)
                            {
                                IdListaPrecio = 1002;//Convert.ToInt32(drLeeLista["IdListaPrecio"].ToString().Trim());

                                if (txtCodLista.Text.Trim() != "-")
                                {
                                    conn.EliminarArticulo("ArticuloPrecioVenta", " IdListaPrecio = " + IdListaPrecio);
                                    VerificaNuevosArticulos(IdListaPrecio);
                                    lvwListaPrecioVenta.Items.Clear();
                                }

                                //string agregar = "INSERT INTO ArticuloPrecioVenta (IDLISTAPRECIO, IDARTICULO, PRECIO) SELECT ListaPrecios.IDLISTAPRECIO, Articulo.IDARTICULO, replace((Cast(replace(Articulo.COSTOENLISTA, ',','.') as decimal(10,3)) * ((Cast(replace(ListaPrecios.PORCENTAJE, ',','.') as decimal(10,3))/100)+1) *  ((ListaPrecios.PORCFLETE/100)+1)), '.', ',') AS PRECIO FROM ListaPrecios, Articulo WHERE ListaPrecios.IDLISTAPRECIO = " + IdListaPrecio + " AND Articulo.idestado=1 ORDER BY Articulo.Idarticulo;";   //QUERY MODIFICADO 1.1 VINCULA ART A TODAS LAS LISTA
                                //string agregar = "INSERT INTO ArticuloPrecioVenta(IDLISTAPRECIO, IDARTICULO, PRECIO) SELECT ListaPrecios.IDLISTAPRECIO, Articulo.IDARTICULO, replace((round(Cast(replace(Articulo.COSTOENLISTA, ',', '.') as decimal(10, 3)), 3) * ((round(Cast(replace(ListaPrecios.PORCENTAJE, ',', '.') as decimal(10, 3)),1)/ 100)+1) * round(((ListaPrecios.PORCFLETE / 100) + 1), 1)), '.', ',') AS PRECIO FROM ListaPrecios, Articulo WHERE ListaPrecios.IDLISTAPRECIO = " + IdListaPrecio + " AND Articulo.idestado = 1 ORDER BY Articulo.Idarticulo;";
                                string agregar = "INSERT INTO ArticuloPrecioVenta(IDLISTAPRECIO, IDARTICULO, PRECIO) SELECT ListaPrecios.IDLISTAPRECIO, Articulo.IDARTICULO, replace((Cast(replace(Articulo.COSTOENLISTA, ',', '.') as decimal(10, 3)) * ((Cast(replace(ListaPrecios.PORCENTAJE, ',', '.') as decimal(10, 3))/ 100) + 1) * ((ListaPrecios.PORCFLETE / 100) + 1)), '.', ',') AS PRECIO FROM ListaPrecios, Articulo WHERE ListaPrecios.IDLISTAPRECIO = " + IdListaPrecio + " AND Articulo.idestado = 1 ORDER BY Articulo.Idarticulo;";
                                string actualizar = "ArticuloPrecioVenta.PRECIO = Cast(replace(Articulo.COSTOENLISTA, ',','.') as decimal(10,3)) * ((Cast(replace(ListaPrecios.PORCENTAJE, ',','.') as decimal(10,3))/100)+1)) FROM Articulo, ListaPrecios, ArticuloPrecioVenta";
                                //string actualizar = "ArticuloPrecioVenta.PRECIO = ROUND(Cast(replace(Articulo.COSTOENLISTA, ',','.') as decimal(10,3)) * ((Cast(replace(ListaPrecios.PORCENTAJE, ',','.') as decimal(10,3))/100)+1),1) FROM Articulo, ListaPrecios, ArticuloPrecioVenta";

                                conn.DesconectarBD();
                                if (this.lvwListaPrecioVenta.Items.Count == 0)
                                {
                                    //Generador de lista de precio
                                    if (conn.InsertarListaPrecioVenta(agregar))
                                    {
                                        MostrarDatos();
                                        MessageBox.Show("Artículos Vinculados a la lista N° " + IdListaPrecio + " / Público", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                        MessageBox.Show("Error al Vincular: No existe articulos para actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                    if (conn.ActualizarListaPrecioVenta("ArticuloPrecioVenta", actualizar, "ListaPrecios.IDLISTAPRECIO = ArticuloPrecioVenta.IDLISTAPRECIO AND Articulo.IDARTICULO = ArticuloPrecioVenta.IDARTICULO AND ListaPrecios.IDLISTAPRECIO= " + IdListaPrecio + ""))
                                {
                                    MostrarDatos();
                                    MessageBox.Show("Lista Actualizada N° " + IdListaPrecio + " / Público");
                                }
                                else
                                    MessageBox.Show("Error al actualizar la Lista");
                            }
                            cmLeeLista.Connection.Close();

                            break;
                        }
                    case DialogResult.No: //Inserta articulos que se han dado de alta pero que no existen en la lista de precios de vanta
                        {
                            break;
                        }
                }
            }
            catch { MessageBox.Show("Error al actualizar la Lista"); }
        }

        private void txtConsulta_TextChanged(object sender, EventArgs e)
        {
            string sCodArt;
            char[] QuitaSimbolo = { '$', ' ' };
            char[] QuitaSimbolo2 = { '*', ' ' };

            sCodArt = txtConsulta.Text.TrimStart(QuitaSimbolo2);
            sCodArt = sCodArt.TrimEnd(QuitaSimbolo2);
            this.txtConsulta.Text = sCodArt;
        }

        private void tsBtnBuscaLista_Click(object sender, EventArgs e)
        {
            DGestion.Reportes.frmRPTListaPrecio frmRptConsultaListaPrecio = new DGestion.Reportes.frmRPTListaPrecio();
            frmRptConsultaListaPrecio.ShowDialog();
        }

        private void cboBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        /// ///////////////////////////////////////////////////FIN BLOQUE DE BUSQUEDA//////////////////////////////////////////////////////////////
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (conn.EliminarArticulo("ArticuloPrecioVenta", " IdArticulo = " + lvwListaPrecioVenta.SelectedItems[0].SubItems[8].Text))
            {                
                gpoCliente.Visible = false;                
                tsBtnNuevo.Enabled = true;
                lvwListaPrecioVenta.Height = 450;
                gbListaPrecio.Height = 510;
                //btnGuardar.Enabled = true;
                
                MostrarDatos();
                MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error al Eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
