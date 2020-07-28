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

using System.Globalization;
using Microsoft.Reporting.WinForms;

using DGestion.Reportes;

using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;

namespace DGestion
{
    public partial class frmArticulo : Form
    {
        public delegate void pasarArticulo1(string CodPArt);
        public event pasarArticulo1 pasadoArt1;
        public delegate void pasarArticulo2(string RSArt);
        public event pasarArticulo2 pasadoArt2;
        public delegate void pasarArticulo3(string Costo);
        public event pasarArticulo3 pasadoArt3;

        int iIdArticulo;

        int PagAv = 30, PagRe = 1;

        public frmArticulo()
        {
            InitializeComponent();
        }

        ArticulosBD conn = new ArticulosBD();
        ProveedorBD conn1 = new ProveedorBD();
        DataGridViewImageColumn img = new DataGridViewImageColumn();
        string codArt;
        int idEstadoEliminado;

        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private void FormatoListView()
        {
            lvwArticulo.View = View.Details;
            lvwArticulo.LabelEdit = false;
            lvwArticulo.AllowColumnReorder = false;
            lvwArticulo.FullRowSelect = true;
            lvwArticulo.GridLines = true;
        }

        public void MostrarDatos2(string codigoArticulo, int iPaginaAv, int iPaginaRe) {
            try {

                if (codigoArticulo == "0")
                {
                    lvwArticulo.Items.Clear();

                    tsTextPag.Text = "AR " + iPaginaAv + "-" + iPaginaRe;

                    SqlCommand cm = new SqlCommand("SELECT IdArticulo AS 'ID', Codigo AS 'Código', Descripcion AS 'Descripción Artículo', CANT_ACTUAL, Imagen AS 'QR', IdEstado as Estado FROM Articulo WHERE IDEstado = 1 AND IDARTICULO BETWEEN " + iPaginaAv + " AND " + iPaginaRe + "  ORDER BY Descripcion", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwArticulo.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["Id"].ToString());
                        item.SubItems.Add(dr["Código"].ToString());
                        item.SubItems.Add(dr["Descripción Artículo"].ToString());
                        item.SubItems.Add(dr["QR"].ToString());
                        item.SubItems.Add(dr["Estado"].ToString());
                        item.SubItems.Add(dr["CANT_ACTUAL"].ToString());

                        if (item.SubItems[3].Text != "System.Byte[]")
                            if (item.SubItems[5].Text.Trim() == "0")
                                item.ImageIndex = 3;
                            else
                                item.ImageIndex = 0;
                        else
                            item.ImageIndex = 2;

                        lvwArticulo.Items.Add(item);
                    }
                    codArt = lvwArticulo.SelectedItems[0].SubItems[0].Text;
                    cm.Connection.Close();
                }

                else
                {
                    lvwArticulo.Items.Clear();

                    SqlCommand cm = new SqlCommand("SELECT IdArticulo AS 'ID', Codigo AS 'Código', Descripcion AS 'Descripción Artículo', CANT_ACTUAL, Imagen AS 'QR', IdEstado as Estado FROM Articulo WHERE IDEstado = 1 AND Codigo = '" + codigoArticulo + "' ORDER BY Descripcion", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwArticulo.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["Id"].ToString());
                        item.SubItems.Add(dr["Código"].ToString());
                        item.SubItems.Add(dr["Descripción Artículo"].ToString());
                        item.SubItems.Add(dr["QR"].ToString());
                        item.SubItems.Add(dr["Estado"].ToString());
                        item.SubItems.Add(dr["CANT_ACTUAL"].ToString());

                        if (item.SubItems[3].Text != "System.Byte[]")
                            if (item.SubItems[5].Text.Trim() == "0")
                                item.ImageIndex = 3;
                            else
                                item.ImageIndex = 0;
                        else
                            item.ImageIndex = 2;

                        lvwArticulo.Items.Add(item);
                    }
                    codArt = lvwArticulo.SelectedItems[0].SubItems[0].Text;
                    cm.Connection.Close();
                }

            }
            catch { }
        }

        private void frmArticulo_Load(object sender, EventArgs e)
        {

            // TODO: esta línea de código carga datos en la tabla 'datArticulos.Articulo' Puede moverla o quitarla según sea necesario.
            gpoCliente.Visible = false;
            //gridArticulo.Height = 480;
            lvwArticulo.Height = 570;

            conn.ConectarBD();
            // MostrarDatos();

            FormatoListView();
            MostrarDatos2("0",0,30);

            cboBuscaArticulo.SelectedIndex = 0;

            //OrdenaGrilla();

            if (conn.VerImagenQR(this.QRpicture, codArt,0))  {
                btnQRGenerar.Enabled = false;
                btnQRImprimir.Enabled = true;
                btnVerQR.Enabled = true;
                btnQuitarQR.Enabled = true;
                bntVerQRgenerado.Enabled = true;
            }
            else {
                btnQRGenerar.Enabled = true;
                btnQRImprimir.Enabled = false;
                btnVerQR.Enabled = false;
                btnQuitarQR.Enabled = false;
                bntVerQRgenerado.Enabled = false;
            }

            this.BarCodpicture.Width = 324;
            this.BarCodpicture.Height = 113;
        }

        private void btcCerrar_Click(object sender, EventArgs e)
        {
            gpoCliente.Visible = false;
            //gridArticulo.Height = 480;
            lvwArticulo.Height = 570;

            tsBtnNuevo.Enabled = true;
            tsBtnModificar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = true;
        }

        private void btnRubro_Click(object sender, EventArgs e)
        {
            frmRubroArticulo formRubroArt = new frmRubroArticulo();
            formRubroArt.pasado11 += new frmRubroArticulo.pasar11(CodRubroArt);  //Delegado11 Rubro Articulo
            formRubroArt.pasado22 += new frmRubroArticulo.pasar22(DescripcionRubro); //Delegado2 Rubro Articulo
            formRubroArt.ShowDialog();
        }

        private void btnMarca_Click(object sender, EventArgs e)
        {
            frmMarcaArticulo formMarcaArt = new frmMarcaArticulo();
            formMarcaArt.pasado111 += new frmMarcaArticulo.pasar111(CodMarcaArt);  //Delegado11 Marca Articulo
            formMarcaArt.pasado222 += new frmMarcaArticulo.pasar222(DescripcionMarca); //Delegado2 Marca Articulo
            formMarcaArt.ShowDialog();
        }

        private void btnTipoArticulo_Click(object sender, EventArgs e)
        {
            frmTipoIBArticulo formTipoArt = new frmTipoIBArticulo();
            formTipoArt.pasado1111 += new frmTipoIBArticulo.pasar1111(CodTipoArt);  //Delegado11 Tipo Articulo
            formTipoArt.pasado2222 += new frmTipoIBArticulo.pasar2222(DescripcionTipoArt); //Delegado2 Tipo Articulo
            formTipoArt.ShowDialog();
        }

        private void btnTipoProducto_Click(object sender, EventArgs e)
        {
            frmTipoIVAProducto formTipoProd = new frmTipoIVAProducto();
            formTipoProd.pasado11111 += new frmTipoIVAProducto.pasar11111(CodTipoProd);  //Delegado11 Tipo Producto
            formTipoProd.pasado22222 += new frmTipoIVAProducto.pasar22222(DescripcionTipoProd); //Delegado2 Tipo Producto
            formTipoProd.ShowDialog();
        }

        private void btnUltimoProveedor_Click(object sender, EventArgs e)
        {
            frmProveedores formProveed = new frmProveedores();
            formProveed.pasadoProvee1 += new frmProveedores.pasarProvee1(CodProvee);  //Delegado11 Rubro Articulo
            formProveed.pasadoProvee2 += new frmProveedores.pasarProvee2(RSProvee); //Delegado2 Rubro Articulo
            formProveed.ShowDialog();
        }

        private void btnFamilia_Click(object sender, EventArgs e)
        {
            frmFamiliaArticulo formFamiliaArt = new frmFamiliaArticulo();
            formFamiliaArt.pasado1 += new frmFamiliaArticulo.pasar1(CodFamiliaArt);  //Delegado1 Familia Articulo
            formFamiliaArt.pasado2 += new frmFamiliaArticulo.pasar2(DescripcionFamilia); //Delegado2 Familia Articulo
            formFamiliaArt.ShowDialog();
        }

        private void btnImpuestoArt_Click(object sender, EventArgs e)
        {
            frmImpuestos formImp = new frmImpuestos();
            formImp.pasarImpuestoCod1 += new frmImpuestos.pasarImpuestoCod(CodImpAlic);  //Delegado11 Rubro Articulo
            formImp.pasarImpuestoAlicuota1 += new frmImpuestos.pasarImpuestoAlic(DescAlicuota); //Delegado2 Rubro Articulo
            formImp.pasarImpuestoDesc1 += new frmImpuestos.pasarImpuestoDesc(DescImp);
            formImp.ShowDialog();
        }

        //Metodos de delegado Tipo Impuesto
        public void CodImpAlic(int dato1)
        {
            this.txtCodAlicuota.Text = dato1.ToString();
        }

        public void DescAlicuota(string dato2)
        {
            this.cboAlicuota.Text = dato2.ToString();
        }
        public void DescImp(string dato3)
        {
        }
        //

        //Metodos de delegado Proveedor
        public void CodProvee(int dato1)
        {
            this.txtCodProveedor.Text = dato1.ToString();
        }

        public void RSProvee(string dato2)
        {
            this.cboRazonSocialProveedor.Text = dato2.ToString();
        }

        //Metodos de delegado Familia Articulo
        public void CodFamiliaArt(int dato1)
        {
            txtCodFamiliaArt.Text = dato1.ToString();
        }

        public void DescripcionFamilia(string dato2)
        {
            this.cmbDescripcionFamiliaArt.Text = dato2.ToString();
        }
        //

        //Metodos de delegado Rubro Articulo
        public void CodRubroArt(int dato1)
        {
            this.txtCodRubroArt.Text = dato1.ToString();
        }

        public void DescripcionRubro(string dato2)
        {
            this.cboRubroArt.Text = dato2.ToString();
        }
        //

        //Metodos de delegado Marca Articulo
        public void CodMarcaArt(int dato1)
        {
            this.txtCodMarcaArt.Text = dato1.ToString();
        }

        public void DescripcionMarca(string dato2)
        {
            this.cboMarcaArt.Text = dato2.ToString();
        }
        //

        //Metodos de delegado Tipo Articulo
        public void CodTipoArt(int dato1)
        {
            this.txtCodTipoArt.Text = dato1.ToString();
        }

        public void DescripcionTipoArt(string dato2)
        {
            this.cboTipoArt.Text = dato2.ToString();
        }
        //

        //Metodos de delegado Tipo Producto
        public void CodTipoProd(int dato1)
        {
            this.txtCodTipoProd.Text = dato1.ToString();
        }

        public void DescripcionTipoProd(string dato2)
        {
            this.cboTipoProd.Text = dato2.ToString();
        }
        //


        public void Limpieza()
        {

            int contadorCodigoNuevo;

            gpoCliente.Visible = true;
            //gridArticulo.Height = 235;
            lvwArticulo.Height = 320;

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;

            ////////////////////////// AUTONUMERICO //////////////////////////////////////////
            conn.LeeArticulo("SELECT MAX(IDARTICULO)  as Codigo FROM Articulo", "Articulo");
            txtCodigoArticulo.Text = conn.leer["Codigo"].ToString();
            contadorCodigoNuevo = (Convert.ToInt32(txtCodigoArticulo.Text));
            contadorCodigoNuevo = contadorCodigoNuevo + 1;
            txtCodigoArticulo.Text = contadorCodigoNuevo.ToString();
            conn.DesconectarBDLee();
            ////////////////////////////////////////////////////////////////////////////////

            if (this.txtCodFabrica.Text.Trim() != "")
                txtCodFabrica.Text = "";
            if (this.txtSituacion.Text.Trim() != "")
                txtSituacion.Text = "";
            if (txtDescripcionArticulo.Text.Trim() != "")
                txtDescripcionArticulo.Text = "";
            if (txtCodFamiliaArt.Text.Trim() != "")
                txtCodFamiliaArt.Text = "";
            if (txtCodRubroArt.Text.Trim() != "")
                txtCodRubroArt.Text = "";
            if (txtCodMarcaArt.Text.Trim() != "")
                txtCodMarcaArt.Text = "";
            if (txtCodTipoArt.Text.Trim() != "")
                txtCodTipoArt.Text = "";
            if (txtCodTipoProd.Text.Trim() != "")
                txtCodTipoProd.Text = "";
            if (txtCodProveedor.Text.Trim() != "")
                txtCodProveedor.Text = "";

            if (this.txtCodAlicuota.Text.Trim() != "")
                txtCodAlicuota.Text = "";

            txtCosto.Text = "$ 0,000";
            txtCostoEnLista.Text = "$ 0,000";

            if (txtCostoListaProc.Text.Trim() != "")
                txtCostoListaProc.Text = "0";
            if (txtUnidadMedida.Text.Trim() != "")
                txtUnidadMedida.Text = "";
            if (txtUnidadVenta.Text.Trim() != "")
                txtUnidadVenta.Text = "0";
            if (txtCantActual.Text.Trim() != "")
                txtCantActual.Text = "0";
            if (txtCantPtoPedido.Text.Trim() != "")
                txtCantPtoPedido.Text = "0";
            if (txtCantXReposicion.Text.Trim() != "")
                txtCantXReposicion.Text = "0";
            if (this.txtBuscarArticulo.Text.Trim() != "")
                cboBuscaArticulo.SelectedIndex = 0;
            if (this.cboMarcaArt.Text.Trim() != "")
                cboMarcaArt.Text = "";
            if (this.cboRazonSocialProveedor.Text.Trim() != "")
                cboRazonSocialProveedor.Text = "";
            if (this.cboRubroArt.Text.Trim() != "")
                cboRubroArt.Text = "";
            if (this.cboTipoArt.Text.Trim() != "")
                cboTipoArt.Text = "";
            if (this.cboTipoProd.Text.Trim() != "")
                cboTipoProd.Text = "";
            if (this.cboAlicuota.Text.Trim() != "")
                cboAlicuota.Text = "";
            if (this.cmbDescripcionFamiliaArt.Text.Trim() != "")
                cmbDescripcionFamiliaArt.Text = "";
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            int contadorCodigoNuevo;

            gpoCliente.Visible = true;
            //gridArticulo.Height = 235;
            lvwArticulo.Height = 320;

            btnEliminar.Enabled = false;
            tsBtnModificar.Enabled = true;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;

            ////////////////////////// AUTONUMERICO //////////////////////////////////////////
            conn.LeeArticulo("SELECT MAX(IDARTICULO)  as Codigo FROM Articulo", "Articulo");
            txtCodigoArticulo.Text = conn.leer["Codigo"].ToString();
            contadorCodigoNuevo = (Convert.ToInt32(txtCodigoArticulo.Text));
            contadorCodigoNuevo = contadorCodigoNuevo + 1;
            txtCodigoArticulo.Text = contadorCodigoNuevo.ToString();
            conn.DesconectarBDLee();
            ////////////////////////////////////////////////////////////////////////////////

            if (this.txtCodFabrica.Text.Trim() != "")
                txtCodFabrica.Text = "";
            if (this.txtSituacion.Text.Trim() != "")
                txtSituacion.Text = "";
            if (txtDescripcionArticulo.Text.Trim() != "")
                txtDescripcionArticulo.Text = "";
            if (txtCodFamiliaArt.Text.Trim() != "")
                txtCodFamiliaArt.Text = "";
            if (txtCodRubroArt.Text.Trim() != "")
                txtCodRubroArt.Text = "";
            if (txtCodMarcaArt.Text.Trim() != "")
                txtCodMarcaArt.Text = "";
            if (txtCodTipoArt.Text.Trim() != "")
                txtCodTipoArt.Text = "";
            if (txtCodTipoProd.Text.Trim() != "")
                txtCodTipoProd.Text = "";
            if (txtCodProveedor.Text.Trim() != "")
                txtCodProveedor.Text = "";

            if (this.txtCodAlicuota.Text.Trim() != "")
                txtCodAlicuota.Text = "";

            txtCosto.Text = "$ 0,000";
            txtCostoEnLista.Text = "$ 0,000";

            if (txtCostoListaProc.Text.Trim() != "")
                txtCostoListaProc.Text = "0";
            if (txtUnidadMedida.Text.Trim() != "")
                txtUnidadMedida.Text = "";
            if (txtUnidadVenta.Text.Trim() != "")
                txtUnidadVenta.Text = "0";
            if (txtCantActual.Text.Trim() != "")
                txtCantActual.Text = "0";
            if (txtCantPtoPedido.Text.Trim() != "")
                txtCantPtoPedido.Text = "0";
            if (txtCantXReposicion.Text.Trim() != "")
                txtCantXReposicion.Text = "0";
            if (this.txtBuscarArticulo.Text.Trim() != "")
                cboBuscaArticulo.SelectedIndex = 0;
            if (this.cboMarcaArt.Text.Trim() != "")
                cboMarcaArt.Text = "";
            if (this.cboRazonSocialProveedor.Text.Trim() != "")
                cboRazonSocialProveedor.Text = "";
            if (this.cboRubroArt.Text.Trim() != "")
                cboRubroArt.Text = "";
            if (this.cboTipoArt.Text.Trim() != "")
                cboTipoArt.Text = "";
            if (this.cboTipoProd.Text.Trim() != "")
                cboTipoProd.Text = "";
            if (this.cboAlicuota.Text.Trim() != "")
                cboAlicuota.Text = "";
            if (this.cmbDescripcionFamiliaArt.Text.Trim() != "")
                cmbDescripcionFamiliaArt.Text = "";
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsBtnModificar_Click(object sender, EventArgs e)
        {

            if (lvwArticulo.SelectedItems.Count == 0)
                MessageBox.Show("Error: No se ha seleccionado ningun Artículo para modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                gpoCliente.Visible = true;
                //gridArticulo.Height = 235;
                lvwArticulo.Height = 320;

                btnEliminar.Enabled = true;
                tsBtnModificar.Enabled = false;
                tsBtnNuevo.Enabled = true;
                btnModificar.Enabled = true;
                btnGuardar.Enabled = false;
            }
        }

        private void txtCodFamiliaArt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodFamiliaArt.Text.Trim() != "")
                {
                    conn.consultaFamiliaArticulo("Select IDFamilia As 'Código', Descripcion As 'Familia Artículo' FROM Familia WHERE IDFamilia = " + txtCodFamiliaArt.Text + "", "Familia");
                    this.cmbDescripcionFamiliaArt.DataSource = conn.ds.Tables[0];
                    this.cmbDescripcionFamiliaArt.ValueMember = "Código";
                    this.cmbDescripcionFamiliaArt.DisplayMember = "Familia Artículo";
                }
                else
                    cmbDescripcionFamiliaArt.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cmbDescripcionFamiliaArt.Text = "";

                conn.DesconectarBD();

            }
            catch { conn.DesconectarBD(); }
        }

        private void txtCodRubroArt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodRubroArt.Text.Trim() != "")
                {
                    conn.consultaRubroArticulo("Select IDRubro As 'Código', Descripcion As 'Rubro Artículo' FROM Rubro WHERE IDRubro = " + this.txtCodRubroArt.Text + "", "Rubro");

                    this.cboRubroArt.DataSource = conn.ds.Tables[0];
                    this.cboRubroArt.ValueMember = "Código";
                    this.cboRubroArt.DisplayMember = "Rubro Artículo";
                }
                else
                    cboRubroArt.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboRubroArt.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void txtCodMarcaArt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodMarcaArt.Text.Trim() != "")
                {
                    conn.consultaMarcaArticulo("Select IDMarca As 'Código', Descripcion As 'Marca Artículo' FROM Marca WHERE IDMarca = " + this.txtCodMarcaArt.Text + "", "Marca");

                    this.cboMarcaArt.DataSource = conn.ds.Tables[0];
                    this.cboMarcaArt.ValueMember = "Código";
                    this.cboMarcaArt.DisplayMember = "Marca Artículo";
                }
                else
                    cboMarcaArt.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboMarcaArt.Text = "";

                conn.DesconectarBD();

            }
            catch { conn.DesconectarBD(); }
        }

        private void txtCodTipoArt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodTipoArt.Text.Trim() != "")
                {
                    conn.consultaTipoArticulo("Select IDTipoArticulo As 'Código', Descripcion As 'Tipo Artículo' FROM TipoArticulo WHERE IDTipoArticulo = " + this.txtCodTipoArt.Text + "", "TipoArticulo");

                    this.cboTipoArt.DataSource = conn.ds.Tables[0];
                    this.cboTipoArt.ValueMember = "Código";
                    this.cboTipoArt.DisplayMember = "Tipo Artículo";
                }
                else
                    cboTipoArt.Text = "";


                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboTipoArt.Text = "";

                conn.DesconectarBD();

            }
            catch { conn.DesconectarBD(); }
        }

        private void txtCodTipoProd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodTipoProd.Text.Trim() != "")
                {
                    conn.consultaTipoProducto("Select IDTipoProducto As 'Código', Descripcion As 'Tipo Producto' FROM TipoProducto WHERE IDTipoProducto = " + this.txtCodTipoProd.Text + "", "TipoProducto");

                    this.cboTipoProd.DataSource = conn.ds.Tables[0];
                    this.cboTipoProd.ValueMember = "Código";
                    this.cboTipoProd.DisplayMember = "Tipo Producto";
                }
                else
                    cboTipoProd.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboTipoProd.Text = "";

                conn.DesconectarBD();

            }
            catch { conn.DesconectarBD(); }
        }

        private void txtProveedor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodProveedor.Text.Trim() != "")
                {
                    conn1.ConsultaProveedor("Select IDProveedor As 'Código', RazonSocial AS 'Razón Social' FROM Proveedor WHERE IDProveedor = " + this.txtCodProveedor.Text + "", "Proveedor");

                    this.cboRazonSocialProveedor.DataSource = conn1.ds.Tables[0];
                    this.cboRazonSocialProveedor.ValueMember = "Código";
                    this.cboRazonSocialProveedor.DisplayMember = "Razón Social";
                }
                else
                    cboRazonSocialProveedor.Text = "";

                if (conn1.ds.Tables[0].Rows.Count < 1)
                    cboRazonSocialProveedor.Text = "";

                conn1.DesconectarBD();
            }
            catch { conn1.DesconectarBD(); }
        }

        private void txtCodImpuesto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodAlicuota.Text.Trim() != "")
                {
                    conn.consultaTipoProducto("Select IDImpuesto, Alicuota FROM Impuesto WHERE IDImpuesto = " + this.txtCodAlicuota.Text + "", "Impuesto");

                    this.cboAlicuota.DataSource = conn.ds.Tables[0];
                    this.cboAlicuota.ValueMember = "IDImpuesto";
                    this.cboAlicuota.DisplayMember = "Alicuota";
                }
                else
                    this.cboAlicuota.Text = "";

                if (conn.ds.Tables[0].Rows.Count < 1)
                    cboAlicuota.Text = "";

                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        private void tsBtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboBuscaArticulo.SelectedIndex == 0)
                {
                    BuscarDatos2();
                }

                else if (cboBuscaArticulo.SelectedIndex == 1)
                {
                    BuscarDatos1();
                }
            }
            catch { }
        }

        //////////////////////////////////////////////////////BLOQUE DE BUSQUEDA//////////////////////////////////////////////////////////////
        public void BuscarDatos1()
        {
            try
            {
                lvwArticulo.Items.Clear();

                SqlCommand cm = new SqlCommand("SELECT IdArticulo AS 'ID', Codigo AS 'Código', Descripcion AS 'Descripción Artículo', CANT_ACTUAL, Imagen AS 'QR', IDEstado AS 'Estado' FROM Articulo WHERE Codigo LIKE '" + txtBuscarArticulo.Text.Trim() + "%' AND IDESTADO = 1 ORDER BY Descripcion", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    lvwArticulo.SmallImageList = imageList1;
                    ListViewItem item = new ListViewItem(dr["Id"].ToString());
                    item.SubItems.Add(dr["Código"].ToString());
                    item.SubItems.Add(dr["Descripción Artículo"].ToString());
                    item.SubItems.Add(dr["QR"].ToString());
                    item.SubItems.Add(dr["Estado"].ToString());
                    item.SubItems.Add(dr["CANT_ACTUAL"].ToString());

                    if (item.SubItems[3].Text != "System.Byte[]")
                        if (item.SubItems[4].Text == "1")
                            if (item.SubItems[5].Text.Trim() == "0")
                                item.ImageIndex = 3;
                            else
                                item.ImageIndex = 0;
                        else
                            item.ImageIndex = 1;
                    else
                        item.ImageIndex = 2;

                    lvwArticulo.Items.Add(item);
                }
                if (lvwArticulo.SelectedIndices.Count != 0)
                    codArt = lvwArticulo.SelectedItems[0].SubItems[0].Text;

                cm.Connection.Close();
            }
            catch { }
        }

        public void BuscarDatos2()
        {
            try
            {
                lvwArticulo.Items.Clear();

                if ((cboBuscaArticulo.SelectedIndex == 1 && txtBuscarArticulo.Text == "") || txtBuscarArticulo.Text == "*")
                {
                    if (txtBuscarArticulo.Text == "*")
                        txtBuscarArticulo.Text = "";

                    SqlCommand cm = new SqlCommand("SELECT IdArticulo AS 'ID', Codigo AS 'Código', Descripcion AS 'Descripción Artículo', CANT_ACTUAL, Imagen AS 'QR', IDEstado AS 'Estado' FROM Articulo WHERE Descripcion LIKE '" + txtBuscarArticulo.Text + "%' AND IDESTADO=1 ORDER BY Descripcion", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwArticulo.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["Id"].ToString());
                        item.SubItems.Add(dr["Código"].ToString());
                        item.SubItems.Add(dr["Descripción Artículo"].ToString());
                        item.SubItems.Add(dr["QR"].ToString());
                        item.SubItems.Add(dr["Estado"].ToString());
                        item.SubItems.Add(dr["CANT_ACTUAL"].ToString());

                        if (item.SubItems[3].Text != "System.Byte[]")
                            if (item.SubItems[4].Text == "1")
                                if (item.SubItems[5].Text.Trim() == "0")
                                    item.ImageIndex = 3;
                                else
                                    item.ImageIndex = 0;
                            else
                                item.ImageIndex = 1;
                        else
                            item.ImageIndex = 2;

                        lvwArticulo.Items.Add(item);
                    }
                    if (lvwArticulo.SelectedIndices.Count != 0)
                        codArt = lvwArticulo.SelectedItems[0].SubItems[0].Text;
                    cm.Connection.Close();

                }

                else if (cboBuscaArticulo.SelectedIndex == 0 && txtBuscarArticulo.Text != "")
                {
                    SqlCommand cm = new SqlCommand("SELECT IdArticulo AS 'ID', Codigo AS 'Código', Descripcion AS 'Descripción Artículo', Imagen AS 'QR', IDEstado AS 'Estado' FROM Articulo WHERE Descripcion LIKE '" + txtBuscarArticulo.Text + "%' AND IDESTADO = 1 ORDER BY Descripcion", conectaEstado);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        lvwArticulo.SmallImageList = imageList1;
                        ListViewItem item = new ListViewItem(dr["Id"].ToString());
                        item.SubItems.Add(dr["Código"].ToString());
                        item.SubItems.Add(dr["Descripción Artículo"].ToString());
                        item.SubItems.Add(dr["QR"].ToString());
                        item.SubItems.Add(dr["Estado"].ToString());

                        if (item.SubItems[3].Text != "System.Byte[]")
                            if (item.SubItems[4].Text == "1")
                                item.ImageIndex = 0;
                            else
                                item.ImageIndex = 1;
                        else
                            item.ImageIndex = 2;

                        lvwArticulo.Items.Add(item);
                    }
                    codArt = lvwArticulo.SelectedItems[0].SubItems[0].Text;
                    cm.Connection.Close();
                }

            }
            catch { }
        }
        /// ///////////////////////////////////////////////////FIN BLOQUE DE BUSQUEDA//////////////////////////////////////////////////////////////

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //gpoCliente.Visible = false;
            //lvwArticulo.Height = 570;

            tsBtnModificar.Enabled = true;
            tsBtnNuevo.Enabled = true;
            btnModificar.Enabled = false;

            //////Validador de codigo de articulo//////

            ////////////////////////// AUTONUMERICO //////////////////////////////////////////
            /*conn.LeeArticulo("SELECT CodigoArticulo FROM Articulo WHERE CodigoArticulo='"+ txtCodigoArticulo + "'", "Articulo");
            txtCodigoArticulo.Text = conn.leer["Codigo"].ToString();
            contadorCodigoNuevo = (Convert.ToInt32(txtCodigoArticulo.Text));
            contadorCodigoNuevo = contadorCodigoNuevo + 1;
            txtCodigoArticulo.Text = contadorCodigoNuevo.ToString();
            conn.DesconectarBDLee();*/

            ////////////////////////////////////////////////////////////////////////////////

            float CostoArticulo;
            float CostoListaProc;
            float CostoEnLista;            

            if (txtDescripcionArticulo.Text.Trim() == "")
                txtDescripcionArticulo.Text = "";
            if (txtCodFamiliaArt.Text.Trim() == "")
                txtCodFamiliaArt.Text = "null";
            if (txtCodRubroArt.Text.Trim() == "")
                txtCodRubroArt.Text = "null";
            if (txtCodMarcaArt.Text.Trim() == "")
                txtCodMarcaArt.Text = "null";
            if (txtCodTipoArt.Text.Trim() == "")
                txtCodTipoArt.Text = "null";
            if (txtCodTipoProd.Text.Trim() == "")
                txtCodTipoProd.Text = "null";
            if (txtCodProveedor.Text.Trim() == "")
                txtCodProveedor.Text = "null";
            if (txtCosto.Text.Trim() == "")
                txtCosto.Text = "";
            if (txtCostoListaProc.Text.Trim() == "")
                txtCostoListaProc.Text = "-";
            if (txtCostoEnLista.Text.Trim() == "")
                txtCostoEnLista.Text = "-";
            if (txtUnidadMedida.Text.Trim() == "")
                txtUnidadMedida.Text = "-";
            if (txtUnidadVenta.Text.Trim() == "")
                txtUnidadVenta.Text = "null";
            if (txtCantActual.Text.Trim() == "")
                txtCantActual.Text = "null";
            if (txtCantPtoPedido.Text.Trim() == "")
                txtCantPtoPedido.Text = "null";
            if (txtCantXReposicion.Text.Trim() == "")
                txtCantXReposicion.Text = "null";

            if (txtCodAlicuota.Text.Trim() == "")
                txtCodAlicuota.Text = "null";

            //Quita Simbolos para guardar los datos numéricos
            char[] QuitaSimbolo = { '$', ' ' };
            this.txtCosto.Text = this.txtCosto.Text.TrimStart(QuitaSimbolo);
            //this.txtCostoListaProc.Text = this.txtCostoListaProc.Text.TrimStart(QuitaSimbolo);
            this.txtCostoEnLista.Text = this.txtCostoEnLista.Text.TrimStart(QuitaSimbolo);

            if ((float.TryParse(txtCosto.Text, out CostoArticulo)) && (float.TryParse(txtCostoListaProc.Text, out CostoListaProc)) && (float.TryParse(txtCostoEnLista.Text, out CostoEnLista)))
            {
                string agregar = "INSERT INTO Articulo(CODIGO, DESCRIPCION, IDFAMILIA, IDRUBRO, IDMARCA, IDTIPOARTICULO, CODIGOFABRICA, IDTIPOPRODUCTO, SITUACION, COSTO, PROCCOSTOENLISTA, COSTOENLISTA, IDPROVEEDOR, UNIDADDEVENTA, UNIDADDEMEDIDA, CANT_ACTUAL, CANT_DE_PUNTO_DE_PEDIDO, CANTENCADAREPOSICION, IDIMPUESTO, IDESTADO) VALUES ('" + txtCodigoArticulo.Text.Trim() + "', '" + txtDescripcionArticulo.Text.Trim() + "', " + txtCodFamiliaArt.Text.Trim() + ", " + txtCodRubroArt.Text.Trim() + ", " + txtCodMarcaArt.Text.Trim() + ", " + txtCodTipoArt.Text.Trim() + ", '" + txtCodFabrica.Text.Trim() + "', " + txtCodTipoProd.Text.Trim() + ", '" + txtSituacion.Text.Trim() + "', '" + txtCosto.Text.Trim() + "', '" + txtCostoListaProc.Text.Trim() + "', '" + txtCostoEnLista.Text.Trim() + "', " + txtCodProveedor.Text.Trim() + ", " + txtUnidadVenta.Text.Trim() + ", '" + txtUnidadMedida.Text.Trim() + "', " + txtCantActual.Text.Trim() + ", " + txtCantPtoPedido.Text.Trim() + ", " + txtCantXReposicion.Text.Trim() + ", " + txtCodAlicuota.Text.Trim() + ", '1')";

                if (conn.InsertarArticulo(agregar))
                {
                    MostrarDatos2("0",0,30);
                    this.txtCosto.Text = "$ " + this.txtCosto.Text.Trim();
                    this.txtCostoListaProc.Text = this.txtCostoListaProc.Text.Trim();
                    this.txtCostoEnLista.Text = "$ " + this.txtCostoEnLista.Text.Trim();
                    MessageBox.Show("Datos Agregados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Error: La información ingresada no tiene el formato correcto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            Limpieza();
        }

        private void ValidaCodigoArticulo()
        {
           
        }

        private void lvwArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string p = Application.StartupPath;
                string CodArticulo;

                CodArticulo = lvwArticulo.SelectedItems[0].SubItems[1].Text.Trim();
                codArt = CodArticulo;
                iIdArticulo = Convert.ToInt32(lvwArticulo.SelectedItems[0].SubItems[0].Text.Trim());
                idEstadoEliminado = Convert.ToInt32(lvwArticulo.SelectedItems[0].SubItems[4].Text);

                int iTipoCodigo;
                iTipoCodigo = LeeTipoCodigoArticulo(CodArticulo);

                if (iTipoCodigo == 1)
                {
                    btnBarGenerar.Enabled = false;
                    btnQRGenerar.Enabled = true;
                    //Visualiza QR
                    if (conn.VerImagenQR(this.QRpicture, codArt, iTipoCodigo))
                    {
                        btnQRGenerar.Enabled = false;
                        btnQRImprimir.Enabled = true;
                        btnVerQR.Enabled = true;
                        btnQuitarQR.Enabled = true;
                        bntVerQRgenerado.Enabled = true;
                    }
                    else
                    {
                        btnQRGenerar.Enabled = true;
                        btnQRImprimir.Enabled = false;
                        btnVerQR.Enabled = false;
                        btnQuitarQR.Enabled = false;
                        bntVerQRgenerado.Enabled = false;
                    }
                    
                    BarCodpicture.ImageLocation =  p + "//BarrCodError.jpg";
                }

                else if (iTipoCodigo == 2)
                {
                    btnQRGenerar.Enabled = false;
                    btnBarGenerar.Enabled = true;
                    //Visualiza Codigo Barra
                    if (conn.VerImagenQR(this.BarCodpicture, codArt, iTipoCodigo))
                    {
                        btnBarGenerar.Enabled = false;
                        btnBarImprimir.Enabled = true;
                        btnQuitarQR.Enabled = true;
                    }
                    else
                    {
                        btnBarGenerar.Enabled = true;
                        btnBarImprimir.Enabled = false;
                        btnBarQuitar.Enabled = false;
                    }
                    QRpicture.ImageLocation = p + "//QRError.jpg";
                }

                else if (iTipoCodigo == 0)
                {
                    conn.VerImagenQR(this.QRpicture, codArt, iTipoCodigo);
                    conn.VerImagenQR(this.BarCodpicture, codArt, iTipoCodigo);
                    btnBarGenerar.Enabled = true;
                    btnQRGenerar.Enabled = true;
                }
                ////////////////////////////////////////////

                conn.LeeArticulo("SELECT * FROM Articulo WHERE idArticulo = '" + iIdArticulo + "'", "Articulo");

                this.txtCodigoArticulo.Text = conn.leer["Codigo"].ToString();
                this.txtDescripcionArticulo.Text = conn.leer["Descripcion"].ToString();

                this.txtCodFamiliaArt.Text = conn.leer["IdFamilia"].ToString();
                if (this.txtCodFamiliaArt.Text.Trim() == "")
                    this.cmbDescripcionFamiliaArt.Text = "";

                this.txtCodRubroArt.Text = conn.leer["IdRubro"].ToString();
                if (this.txtCodRubroArt.Text.Trim() == "")
                    this.cboRubroArt.Text = "";

                this.txtCodMarcaArt.Text = conn.leer["IdMarca"].ToString();
                if (this.txtCodMarcaArt.Text.Trim() == "")
                    this.cboMarcaArt.Text = "";

                this.txtCodTipoArt.Text = conn.leer["IdTipoArticulo"].ToString();
                if (this.txtCodTipoArt.Text.Trim() == "")
                    this.cboTipoArt.Text = "";

                this.txtCodTipoProd.Text = conn.leer["IdTipoProducto"].ToString();
                if (this.txtCodTipoProd.Text.Trim() == "")
                    this.cboTipoProd.Text = "";

                this.txtCodProveedor.Text = conn.leer["IdProveedor"].ToString();
                if (this.txtCodProveedor.Text.Trim() == "")
                    this.cboRazonSocialProveedor.Text = "";

                this.txtCodAlicuota.Text = conn.leer["IDIMPUESTO"].ToString();
                if (this.txtCodAlicuota.Text.Trim() == "")
                    this.txtCodAlicuota.Text = "";

                this.txtUnidadVenta.Text = conn.leer["UnidadDeVenta"].ToString();
                this.txtUnidadMedida.Text = conn.leer["UnidadDeMedida"].ToString();
                this.txtCodFabrica.Text = conn.leer["CodigoFabrica"].ToString();
                this.txtSituacion.Text = conn.leer["Situacion"].ToString();
                this.txtCosto.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leer["COSTO"]),3).ToString();
                this.txtCostoListaProc.Text = conn.leer["PROCCOSTOENLISTA"].ToString();
                this.txtCantActual.Text = conn.leer["CANT_ACTUAL"].ToString();
                this.txtCostoEnLista.Text = "$ " + Math.Round(Convert.ToDecimal(conn.leer["COSTOENLISTA"]),3).ToString();
                this.txtCantXReposicion.Text = conn.leer["CANTENCADAREPOSICION"].ToString();
                this.txtCantPtoPedido.Text = conn.leer["CANT_DE_PUNTO_DE_PEDIDO"].ToString();

                conn.DesconectarBDLee();
                conn.DesconectarBD();

                this.BarCodpicture.Width = 215;
                this.BarCodpicture.Height = 80;
            }
            catch { conn.DesconectarBD(); }
        }

        private int LeeTipoCodigoArticulo(string codart)
        {
            int iTipoCodigo=0;

            SqlCommand cm = new SqlCommand("SELECT TipoCodigo FROM Articulo WHERE Codigo = '"+ codart + "'", conectaEstado);

            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
                iTipoCodigo = Convert.ToInt32(dr["TipoCodigo"].ToString());

            cm.Connection.Close();

            return iTipoCodigo;
        }

        private void btnModificar_Click(object sender, EventArgs e) {
            try {
                // gpoCliente.Visible = false;
                //   gridArticulo.Height = 235;

                if (idEstadoEliminado == 3)
                    MessageBox.Show("Error no se puede actualizar un artículo eliminado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    btnEliminar.Enabled = false;
                    tsBtnModificar.Enabled = true;
                    tsBtnNuevo.Enabled = true;

                    float CostoArticulo;
                    float CostoListaProc;
                    float CostoEnLista;

                    if (txtDescripcionArticulo.Text.Trim() == "")
                        txtDescripcionArticulo.Text = "";
                    if (txtCodFamiliaArt.Text.Trim() == "")
                        txtCodFamiliaArt.Text = "null";
                    if (txtCodRubroArt.Text.Trim() == "")
                        txtCodRubroArt.Text = "null";
                    if (txtCodMarcaArt.Text.Trim() == "")
                        txtCodMarcaArt.Text = "null";
                    if (txtCodTipoArt.Text.Trim() == "")
                        txtCodTipoArt.Text = "null";
                    if (txtCodTipoProd.Text.Trim() == "")
                        txtCodTipoProd.Text = "null";
                    if (txtCodProveedor.Text.Trim() == "")
                        txtCodProveedor.Text = "null";
                    if (txtCosto.Text.Trim() == "")
                        txtCosto.Text = "";
                    if (txtCostoListaProc.Text.Trim() == "")
                        txtCostoListaProc.Text = "";
                    if (txtCostoEnLista.Text.Trim() == "")
                        txtCostoEnLista.Text = "";
                    if (txtUnidadMedida.Text.Trim() == "")
                        txtUnidadMedida.Text = "";
                    if (txtUnidadVenta.Text.Trim() == "")
                        txtUnidadVenta.Text = "null";
                    if (txtCantActual.Text.Trim() == "")
                        txtCantActual.Text = "null";
                    if (txtCantPtoPedido.Text.Trim() == "")
                        txtCantPtoPedido.Text = "null";
                    if (txtCantXReposicion.Text.Trim() == "")
                        txtCantXReposicion.Text = "null";
                    if (txtCodAlicuota.Text.Trim() == "")
                        txtCodAlicuota.Text = "null";

                    //Quita Simbolos para guardar los datos numéricos
                    char[] QuitaSimbolo = { '$', ' ' };
                    this.txtCosto.Text = this.txtCosto.Text.TrimStart(QuitaSimbolo);
                    //this.txtCostoListaProc.Text = this.txtCostoListaProc.Text.TrimStart(QuitaSimbolo);
                    this.txtCostoEnLista.Text = this.txtCostoEnLista.Text.TrimStart(QuitaSimbolo);

                    if ((float.TryParse(txtCosto.Text, out CostoArticulo)) && (float.TryParse(txtCostoListaProc.Text, out CostoListaProc)) && (float.TryParse(txtCostoEnLista.Text, out CostoEnLista)))
                    {
                        string actualizar = "CODIGO='" + txtCodigoArticulo.Text.Trim() + "', DESCRIPCION='" + txtDescripcionArticulo.Text.Trim() + "', IDFAMILIA =" + txtCodFamiliaArt.Text.Trim() + ", IDRUBRO=" + txtCodRubroArt.Text.Trim() + ", IDMARCA= " + txtCodMarcaArt.Text.Trim() + ", IDTIPOARTICULO =" + txtCodTipoArt.Text.Trim() + ", CODIGOFABRICA='" + txtCodFabrica.Text.Trim() + "', IDTIPOPRODUCTO= " + txtCodTipoProd.Text.Trim() + ", SITUACION= '" + txtSituacion.Text.Trim() + "', COSTO = (Cast(replace('" + CostoArticulo + "', ',', '.') as decimal(10,3))), PROCCOSTOENLISTA='" + CostoListaProc + "', COSTOENLISTA = (Cast(replace('" + CostoEnLista + "', ',', '.') as decimal(10,3))), IDPROVEEDOR=" + txtCodProveedor.Text.Trim() + ", UNIDADDEVENTA=" + txtUnidadVenta.Text.Trim() + ",  UNIDADDEMEDIDA='" + txtUnidadMedida.Text.Trim() + "', CANT_ACTUAL=" + txtCantActual.Text.Trim() + ", CANT_DE_PUNTO_DE_PEDIDO=" + txtCantPtoPedido.Text.Trim() + ", CANTENCADAREPOSICION=" + txtCantXReposicion.Text.Trim() + ", IDIMPUESTO=" + txtCodAlicuota.Text.Trim() + ", IDESTADO = " + 1 + "";

                        if (conn.ActualizaArticulo("Articulo", actualizar, " IdArticulo = " + iIdArticulo + ""))
                        {
                            MostrarDatos2(txtCodigoArticulo.Text.Trim(),0,30);
                            //gpoCliente.Visible = false;
                            //gridArticulo.Height = 480;
                            this.txtCosto.Text = "$ " + this.txtCosto.Text.Trim();
                            this.txtCostoListaProc.Text = this.txtCostoListaProc.Text.Trim();
                            this.txtCostoEnLista.Text = "$ " + this.txtCostoEnLista.Text.Trim();
                            MessageBox.Show("Datos Actualizados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            //gpoCliente.Visible = true;
                            //gridArticulo.Height = 235;
                            MessageBox.Show("Error: Al Actualizar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        //gpoCliente.Visible = true;
                        //gridArticulo.Height = 235;
                        MessageBox.Show("Error: La información ingresada no tiene el formato correcto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                //gpoCliente.Visible = true;
                //gridArticulo.Height = 235;
                conn.DesconectarBD();
                MessageBox.Show("Error: Al Actualizar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (idEstadoEliminado == 3)
                MessageBox.Show("Error el artículo ya está eliminado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                btnEliminar.Enabled = true;
                tsBtnModificar.Enabled = true;
                tsBtnNuevo.Enabled = true;
                btnModificar.Enabled = false;
                btnGuardar.Enabled = false;

                string actualizar = "IDEstado=3";

                //if (conn.EliminarArticulo("Articulo", " Idarticulo = " + lvwArticulo.SelectedItems[0].SubItems[0].Text))

                if (conn.ActualizaArticulo("Articulo", actualizar, " Idarticulo = " + lvwArticulo.SelectedItems[0].SubItems[0].Text + ""))
                {
                    MostrarDatos2(txtCodigoArticulo.Text.Trim(),0,30);
                    //gpoCliente.Visible = false;
                    //lvwArticulo.Height = 570;
                    tsBtnNuevo.Enabled = true;
                    tsBtnModificar.Enabled = true;
                    //btnEliminar.Enabled = false;
                    btnModificar.Enabled = true;
                    btnGuardar.Enabled = true;

                    if (this.txtCodigoArticulo.Text.Trim() != "")
                        txtCodigoArticulo.Text = "";

                    if (this.txtCodFabrica.Text.Trim() != "")
                        txtCodFabrica.Text = "";
                    if (this.txtSituacion.Text.Trim() != "")
                        txtSituacion.Text = "";
                    if (txtDescripcionArticulo.Text.Trim() != "")
                        txtDescripcionArticulo.Text = "";
                    if (txtCodFamiliaArt.Text.Trim() != "")
                        txtCodFamiliaArt.Text = "";
                    if (txtCodRubroArt.Text.Trim() != "")
                        txtCodRubroArt.Text = "";
                    if (txtCodMarcaArt.Text.Trim() != "")
                        txtCodMarcaArt.Text = "";
                    if (txtCodTipoArt.Text.Trim() != "")
                        txtCodTipoArt.Text = "";
                    if (txtCodTipoProd.Text.Trim() != "")
                        txtCodTipoProd.Text = "";
                    if (txtCodProveedor.Text.Trim() != "")
                        txtCodProveedor.Text = "";

                    if (this.txtCodAlicuota.Text.Trim() != "")
                        txtCodAlicuota.Text = "";

                    txtCosto.Text = "$ 0,000";
                    txtCostoEnLista.Text = "$ 0,000";

                    if (txtCostoListaProc.Text.Trim() != "")
                        txtCostoListaProc.Text = "0";
                    if (txtUnidadMedida.Text.Trim() != "")
                        txtUnidadMedida.Text = "";
                    if (txtUnidadVenta.Text.Trim() != "")
                        txtUnidadVenta.Text = "0";
                    if (txtCantActual.Text.Trim() != "")
                        txtCantActual.Text = "0";
                    if (txtCantPtoPedido.Text.Trim() != "")
                        txtCantPtoPedido.Text = "0";
                    if (txtCantXReposicion.Text.Trim() != "")
                        txtCantXReposicion.Text = "0";
                    if (this.txtBuscarArticulo.Text.Trim() != "")
                        cboBuscaArticulo.SelectedIndex = 0;
                    if (this.cboMarcaArt.Text.Trim() != "")
                        cboMarcaArt.Text = "";
                    if (this.cboRazonSocialProveedor.Text.Trim() != "")
                        cboRazonSocialProveedor.Text = "";
                    if (this.cboRubroArt.Text.Trim() != "")
                        cboRubroArt.Text = "";
                    if (this.cboTipoArt.Text.Trim() != "")
                        cboTipoArt.Text = "";
                    if (this.cboTipoProd.Text.Trim() != "")
                        cboTipoProd.Text = "";
                    if (this.cboAlicuota.Text.Trim() != "")
                        cboAlicuota.Text = "";
                    if (this.cmbDescripcionFamiliaArt.Text.Trim() != "")
                        cmbDescripcionFamiliaArt.Text = "";

                    MessageBox.Show("Datos Eliminados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al Eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == ".")
            {
                e.Handled = true;
                this.txtCosto.Text += ",";
                SendKeys.Send("{END}");
            }
        }

        private void txtCostoEnLista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == ".")
            {
                e.Handled = true;
                this.txtCostoEnLista.Text += ",";
                SendKeys.Send("{END}");
            }
        }

        private void txtCostoListaProc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == ".")
            {
                e.Handled = true;
                this.txtCostoListaProc.Text += ",";
                SendKeys.Send("{END}");
            }
        }

        //Cuando toma el foco en la caja de texto de valores desaparece simbolo $, cuando pierde el foco aparece
        private void txtCosto_Enter(object sender, EventArgs e)
        {
            char[] QuitaSimbolo = { '$', ' ' };
            this.txtCosto.Text = this.txtCosto.Text.TrimStart(QuitaSimbolo);
        }

        private void txtCosto_Leave(object sender, EventArgs e)
        {
            this.txtCosto.Text = "$ " + this.txtCosto.Text.Trim();
            this.txtCosto.Text = this.txtCosto.Text.Trim();

            decimal resultado;
            string Costo, Porcentaje;


            if (txtCostoListaProc.Text.Trim() == "0" || txtCostoListaProc.Text.Trim() == "0,000")
                txtCostoEnLista.Text = txtCosto.Text.Trim();
            else
            {
                char[] QuitaSimbolo = { '$', ' ' };
                Costo = this.txtCosto.Text.TrimStart(QuitaSimbolo);
                char[] QuitaSimbolo2 = { '$', ' ' };
                Porcentaje = this.txtCostoListaProc.Text.TrimStart(QuitaSimbolo2);

                resultado = ((Convert.ToDecimal(Costo) * Convert.ToDecimal(Porcentaje) / 100) + (Convert.ToDecimal(Costo)));
                this.txtCostoEnLista.Text = resultado.ToString();
            }


        }

        private void txtCostoListaProc_Enter(object sender, EventArgs e)
        {
            //char[] QuitaSimbolo = { '$', ' ' };
            //this.txtCostoListaProc.Text = this.txtCostoListaProc.Text.TrimStart(QuitaSimbolo);
            this.txtCostoListaProc.Text = this.txtCostoListaProc.Text.Trim();
        }

        private void txtCostoListaProc_Leave(object sender, EventArgs e)
        {
            //this.txtCostoListaProc.Text = this.txtCostoListaProc.Text.Trim();      

            decimal resultado;
            string Costo, Porcentaje;

            char[] QuitaSimbolo = { '$', ' ' };
            Costo = this.txtCosto.Text.TrimStart(QuitaSimbolo);
            char[] QuitaSimbolo2 = { '$', ' ' };
            Porcentaje = this.txtCostoListaProc.Text.TrimStart(QuitaSimbolo2);


            resultado = ((Convert.ToDecimal(Costo) * Convert.ToDecimal(Porcentaje) / 100) + (Convert.ToDecimal(Costo)));
            this.txtCostoEnLista.Text = resultado.ToString();
        }

        private void txtCostoEnLista_Enter(object sender, EventArgs e)
        {
            char[] QuitaSimbolo = { '$', ' ' };
            this.txtCostoEnLista.Text = this.txtCostoEnLista.Text.TrimStart(QuitaSimbolo);
        }

        private void txtCostoEnLista_Leave(object sender, EventArgs e)
        {
            this.txtCostoEnLista.Text = "$ " + this.txtCostoEnLista.Text.Trim();
            this.txtCostoEnLista.Text = this.txtCostoEnLista.Text.Trim();
        }

        private void txtCodigoArticulo_Leave(object sender, EventArgs e)
        {
            this.txtCodigoArticulo.Text = this.txtCodigoArticulo.Text.Trim();
        }

        private void txtDescripcionArticulo_Leave(object sender, EventArgs e)
        {
            this.txtDescripcionArticulo.Text = this.txtDescripcionArticulo.Text.Trim();
        }

        private void txtCodFabrica_Leave(object sender, EventArgs e)
        {
            this.txtCodFabrica.Text = this.txtCodFabrica.Text.Trim();
        }

        private void txtUnidadMedida_Leave(object sender, EventArgs e)
        {
            this.txtUnidadMedida.Text = this.txtUnidadMedida.Text.Trim();
        }

        private void txtSituacion_Leave(object sender, EventArgs e)
        {
            this.txtSituacion.Text = this.txtSituacion.Text.Trim();
        }

        private void txtUnidadVenta_Leave(object sender, EventArgs e)
        {
            this.txtUnidadVenta.Text = this.txtUnidadVenta.Text.Trim();
        }

        private void txtCodFamiliaArt_Leave(object sender, EventArgs e)
        {
            this.txtCodFamiliaArt.Text = this.txtCodFamiliaArt.Text.Trim();
        }

        private void txtCodRubroArt_Leave(object sender, EventArgs e)
        {
            this.txtCodRubroArt.Text = this.txtCodRubroArt.Text.Trim();
        }

        private void txtCodMarcaArt_Leave(object sender, EventArgs e)
        {
            this.txtCodMarcaArt.Text = this.txtCodMarcaArt.Text.Trim();
        }

        private void txtCodTipoArt_Leave(object sender, EventArgs e)
        {
            this.txtCodTipoArt.Text = this.txtCodTipoArt.Text.Trim();

            if (cboTipoArt.Text.Trim() == "Artículo")
                txtSituacion.Text = "A";
            else if (cboTipoArt.Text.Trim() == "Servicio")
                txtSituacion.Text = "S";
            else if (cboTipoArt.Text.Trim() == "Fabricación")
                txtSituacion.Text = "F";
            else
                txtSituacion.Text = "-";
        }

        private void txtCodTipoProd_Leave(object sender, EventArgs e)
        {
            this.txtCodTipoProd.Text = this.txtCodTipoProd.Text.Trim();
        }

        private void tsBtnReporte_Click(object sender, EventArgs e)
        {
            DGestion.Reportes.frmRPTarticulo formRubroArt = new frmRPTarticulo();
            formRubroArt.ShowDialog();
        }

        private void tsBtnComponente_Click(object sender, EventArgs e)
        {
        }

        private void btnQRCerrar_Click(object sender, EventArgs e)
        {
            gpBarcode.Visible = false;
            gpQRcode.Visible = false;
            gpoCliente.Enabled = true;
        }

        private void tsArtGestionBarCode_Click(object sender, EventArgs e)
        {
            try
            {
                string CodArticulo;
                CodArticulo = lvwArticulo.SelectedItems[0].SubItems[1].Text.Trim();

                int iTipoCodigo;
                iTipoCodigo = LeeTipoCodigoArticulo(CodArticulo);

                if (iTipoCodigo == 1)
                {
                    btnBarGenerar.Enabled = false;
                    btnQRGenerar.Enabled = true;
                    //Visualiza QR
                    if (conn.VerImagenQR(this.QRpicture, codArt, iTipoCodigo))
                    {
                        btnQRGenerar.Enabled = false;
                        btnQRImprimir.Enabled = true;
                        btnVerQR.Enabled = true;
                        btnQuitarQR.Enabled = true;
                        bntVerQRgenerado.Enabled = true;
                    }
                    else
                    {
                        btnQRGenerar.Enabled = true;
                        btnQRImprimir.Enabled = false;
                        btnVerQR.Enabled = false;
                        btnQuitarQR.Enabled = false;
                        bntVerQRgenerado.Enabled = false;
                    }
                }
                else if (iTipoCodigo == 2)
                {
                    btnQRGenerar.Enabled = false;
                    btnBarGenerar.Enabled = true;
                    //Visualiza Codigo Barra
                    if (conn.VerImagenQR(this.BarCodpicture, codArt, iTipoCodigo))
                    {
                        btnBarGenerar.Enabled = false;
                        btnBarImprimir.Enabled = true;
                        btnQuitarQR.Enabled = true;
                    }
                    else
                    {
                        btnBarGenerar.Enabled = true;
                        btnBarImprimir.Enabled = false;
                        btnBarQuitar.Enabled = false;
                    }
                }

                else if (iTipoCodigo == 0)
                {
                    conn.VerImagenQR(this.QRpicture, codArt, iTipoCodigo);
                    conn.VerImagenQR(this.BarCodpicture, codArt, iTipoCodigo);
                    btnBarGenerar.Enabled = true;
                    btnQRGenerar.Enabled = true;
                }
                ////////////////////////////////////////////

                gpQRcode.Visible = true;
                gpBarcode.Visible = true;                                
                gpoCliente.Enabled = false;

            }
            catch { MessageBox.Show("Error no se ha seleccionado ningún artículo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnQRGenerar_Click(object sender, EventArgs e)
        {
            if (lvwArticulo.SelectedItems[0].SubItems[4].Text == "3")
                MessageBox.Show("Error el artículo esta eliminado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string InfoArt = this.lvwArticulo.SelectedItems[0].SubItems[1].Text.Trim() + "\n" + this.lvwArticulo.SelectedItems[0].SubItems[2].Text.Trim() + "\n" + this.lvwArticulo.SelectedItems[0].SubItems[5].Text.Trim();
                QRCodeEncoder encode = new QRCodeEncoder();
                Bitmap qrcode = encode.Encode(InfoArt);
                QRpicture.Image = qrcode as Image;
                btnQRImprimir.Enabled = true;

                codArt = lvwArticulo.SelectedItems[0].SubItems[1].Text.Trim();

                if (conn.InsertarImagenQR(this.QRpicture, codArt))
                {
                    MostrarDatos2(lvwArticulo.SelectedItems[0].SubItems[1].Text.Trim(),0,30);
                    MessageBox.Show("Código QR Agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MostrarDatos2(txtCodigoArticulo.Text.Trim(),0,30);
                    MessageBox.Show("Error al asociar Código QR al artículo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                string actualizar = "TIPOCODIGO='1'";
                conn.ActualizaArticulo("Articulo", actualizar, " Codigo = '" + codArt + "'");
            }
        }

        private void btnVerQR_Click(object sender, EventArgs e)
        {
            QRCodeDecoder decoder = new QRCodeDecoder();
            MessageBox.Show(decoder.decode(new QRCodeBitmapImage(this.QRpicture.Image as Bitmap)));
        }

        private void bntVerQRgenerado_Click(object sender, EventArgs e)
        {
            QRCodeDecoder decoder = new QRCodeDecoder();
            MessageBox.Show(decoder.decode(new QRCodeBitmapImage(this.QRpicture.Image as Bitmap)));
        }

        private void btnQuitarQR_Click(object sender, EventArgs e)
        {
            try
            {
                string sCodArt;
                sCodArt = lvwArticulo.SelectedItems[0].SubItems[1].Text.Trim();

                if (lvwArticulo.SelectedItems[0].SubItems[4].Text == "3")
                    MessageBox.Show("Error el artículo esta eliminado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    string p = Application.StartupPath;
                    if (conn.ActualizarImagenQR(codArt))
                    {
                        MostrarDatos2(txtCodigoArticulo.Text.Trim(),0,30);
                        this.QRpicture.ImageLocation = p + "//QRError.jpg";

                        MessageBox.Show("Código QR Eliminado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MostrarDatos2(txtCodigoArticulo.Text.Trim(),0,30);
                        MessageBox.Show("Error al quitar Código QR al artículo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string actualizar = "TIPOCODIGO='0'";
                conn.ActualizaArticulo("Articulo", actualizar, " codigo = '" + sCodArt + "'");
            }
            catch { }
        }

        private void btnQRImprimir_Click(object sender, EventArgs e)
        {

            if (lvwArticulo.SelectedItems[0].SubItems[4].Text == "3")
                MessageBox.Show("Error el artículo esta eliminado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                System.Drawing.Printing.PrintDocument myPrintDocument1 = new System.Drawing.Printing.PrintDocument();
                PrintDialog myPrinDialog1 = new PrintDialog();
                myPrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(myPrintDocument2_PrintPage);
                myPrinDialog1.Document = myPrintDocument1;

                if (myPrinDialog1.ShowDialog() == DialogResult.OK)
                {
                    myPrintDocument1.Print();
                }
            }
        }

        private void myPrintDocument2_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap myBitmap1 = new Bitmap(this.QRpicture.Width, QRpicture.Height);
            QRpicture.DrawToBitmap(myBitmap1, new Rectangle(0, 0, QRpicture.Width, QRpicture.Height));
            
            //e.Graphics.DrawImage(myBitmap1, 0, 0);

            //COL1
            e.Graphics.DrawImage(QRpicture.Image, 30, 0);            
            e.Graphics.DrawImage(QRpicture.Image, 30, 200);            
            e.Graphics.DrawImage(QRpicture.Image, 30, 400);            
            e.Graphics.DrawImage(QRpicture.Image, 30, 600);            
            e.Graphics.DrawImage(QRpicture.Image, 30, 800);                        

            //COL2
            e.Graphics.DrawImage(QRpicture.Image, 320, 0);            
            e.Graphics.DrawImage(QRpicture.Image, 320, 200);            
            e.Graphics.DrawImage(QRpicture.Image, 320, 400);            
            e.Graphics.DrawImage(QRpicture.Image, 320, 600);            
            e.Graphics.DrawImage(QRpicture.Image, 320, 800);                        

            //COL3
            e.Graphics.DrawImage(QRpicture.Image, 600, 0);            
            e.Graphics.DrawImage(QRpicture.Image, 600, 200);            
            e.Graphics.DrawImage(QRpicture.Image, 600, 400);            
            e.Graphics.DrawImage(QRpicture.Image, 600, 600);            
            e.Graphics.DrawImage(QRpicture.Image, 600, 800);                        

            myBitmap1.Dispose();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodigoArticulo_Enter(object sender, EventArgs e)
        {
            this.txtCodigoArticulo.Text = this.txtCodigoArticulo.Text.Trim();
        }

        private void cboTipoArt_Leave(object sender, EventArgs e)
        {
            if (cboTipoArt.Text.Trim() == "Artículo")
                txtSituacion.Text = "A";
            else if (cboTipoArt.Text.Trim() == "Servicio")
                txtSituacion.Text = "S";
            else if (cboTipoArt.Text.Trim() == "Fabricación")
                txtSituacion.Text = "F";
            else
                txtSituacion.Text = "-";
        }

        private void cboTipoArt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoArt.Text.Trim() == "Artículo")
                txtSituacion.Text = "A";
            else if (cboTipoArt.Text.Trim() == "Servicio")
                txtSituacion.Text = "S";
            else if (cboTipoArt.Text.Trim() == "Fabricación")
                txtSituacion.Text = "F";
            else
                txtSituacion.Text = "-";
        }

        private void lvwArticulo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RecargaDatos();
        }

        private void RecargaDatos()
        {
            try
            {
                pasadoArt1(this.txtCodigoArticulo.Text.Trim());  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                pasadoArt2(this.txtDescripcionArticulo.Text.Trim());  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms
                char[] QuitaSimbolo1 = { '$', ' ' };
                pasadoArt3(this.txtCostoEnLista.Text.TrimStart(QuitaSimbolo1));  //Si doble clicj en cella ejecuta delegado para pasar datos entre forms

                this.Close();
            }
            catch { this.Close(); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // if (this.txtCodigoArticulo.Text.Trim() != "" || this.txtDescripcionArticulo.Text.Trim() != "")
            //     MostrarDatos2(txtCodigoArticulo.Text.Trim());
            // else
            MostrarDatos2("0",0,30);
        }

        private void lvwArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                RecargaDatos();
            }
        }

        private void btnBarGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                string barcode = lvwArticulo.SelectedItems[0].SubItems[1].Text.Trim();
                Bitmap bitmap = new Bitmap(barcode.Length * 23, 75);

                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    Font oFont = new System.Drawing.Font("IDAHC39M Code 39 Barcode", 12);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush black = new SolidBrush(Color.Black);
                    SolidBrush White = new SolidBrush(Color.White);
                    graphics.FillRectangle(White, 0, 0, bitmap.Width, bitmap.Height);
                    graphics.DrawString("*" + barcode + "*", oFont, black, point);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Jpeg);
                    BarCodpicture.Image = bitmap;
                    BarCodpicture.Height = bitmap.Height;
                    BarCodpicture.Width = bitmap.Width;
                }

                if (conn.InsertarImagenQR(this.BarCodpicture, barcode.Trim()))
                {
                    MostrarDatos2(lvwArticulo.SelectedItems[0].SubItems[1].Text.Trim(),0,30);
                    MessageBox.Show("Código Barra Agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MostrarDatos2(txtCodigoArticulo.Text.Trim(),0,30);
                    MessageBox.Show("Error al asociar Código Barra al artículo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                string actualizar = "TIPOCODIGO='2'";
                conn.ActualizaArticulo("Articulo", actualizar, " Codigo = '" + barcode + "'");

            }
            catch { }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Timer timer1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArticulo));
            this.gpoCliente = new System.Windows.Forms.GroupBox();
            this.cboAlicuota = new System.Windows.Forms.ComboBox();
            this.txtCodAlicuota = new System.Windows.Forms.TextBox();
            this.btnImpuestoArt = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.bntVerQRgenerado = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.txtCodProveedor = new System.Windows.Forms.TextBox();
            this.txtCodFabrica = new System.Windows.Forms.TextBox();
            this.txtCodTipoProd = new System.Windows.Forms.TextBox();
            this.txtCodTipoArt = new System.Windows.Forms.TextBox();
            this.txtCodMarcaArt = new System.Windows.Forms.TextBox();
            this.txtCodRubroArt = new System.Windows.Forms.TextBox();
            this.txtCodFamiliaArt = new System.Windows.Forms.TextBox();
            this.txtCantXReposicion = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtCantPtoPedido = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtCantActual = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnUltimoProveedor = new System.Windows.Forms.Button();
            this.cboRazonSocialProveedor = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtCostoEnLista = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCostoListaProc = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCosto = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnTipoProducto = new System.Windows.Forms.Button();
            this.cboTipoProd = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnTipoArticulo = new System.Windows.Forms.Button();
            this.cboTipoArt = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnMarca = new System.Windows.Forms.Button();
            this.cboMarcaArt = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRubro = new System.Windows.Forms.Button();
            this.cboRubroArt = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFamilia = new System.Windows.Forms.Button();
            this.btcCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtSituacion = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUnidadMedida = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbDescripcionFamiliaArt = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtUnidadVenta = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCodigoArticulo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescripcionArticulo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboBuscaArticulo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBuscarArticulo = new System.Windows.Forms.TextBox();
            this.tlsBarArticulo = new System.Windows.Forms.ToolStrip();
            this.tsBtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsBtnModificar = new System.Windows.Forms.ToolStripButton();
            this.tsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnRePag = new System.Windows.Forms.ToolStripButton();
            this.tsTextPag = new System.Windows.Forms.ToolStripTextBox();
            this.tsBtnAvPag = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnComponente = new System.Windows.Forms.ToolStripButton();
            this.tsArtGestionBarCode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnReporte = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gpQRcode = new System.Windows.Forms.GroupBox();
            this.btnQuitarQR = new System.Windows.Forms.Button();
            this.btnVerQR = new System.Windows.Forms.Button();
            this.btnQRGenerar = new System.Windows.Forms.Button();
            this.btnQRCerrar = new System.Windows.Forms.Button();
            this.btnQRImprimir = new System.Windows.Forms.Button();
            this.QRpicture = new System.Windows.Forms.PictureBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lvwArticulo = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Codigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DescripciónArtículo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Imagen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Estado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cantidad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gpBarcode = new System.Windows.Forms.GroupBox();
            this.btnBarQuitar = new System.Windows.Forms.Button();
            this.btnBarGenerar = new System.Windows.Forms.Button();
            this.btnBarCerrar = new System.Windows.Forms.Button();
            this.btnBarImprimir = new System.Windows.Forms.Button();
            this.BarCodpicture = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            timer1 = new System.Windows.Forms.Timer(this.components);
            this.gpoCliente.SuspendLayout();
            this.tlsBarArticulo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gpQRcode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QRpicture)).BeginInit();
            this.gpBarcode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BarCodpicture)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            timer1.Interval = 120000;
            timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // gpoCliente
            // 
            this.gpoCliente.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpoCliente.Controls.Add(this.cboAlicuota);
            this.gpoCliente.Controls.Add(this.txtCodAlicuota);
            this.gpoCliente.Controls.Add(this.btnImpuestoArt);
            this.gpoCliente.Controls.Add(this.label20);
            this.gpoCliente.Controls.Add(this.bntVerQRgenerado);
            this.gpoCliente.Controls.Add(this.btnEliminar);
            this.gpoCliente.Controls.Add(this.btnModificar);
            this.gpoCliente.Controls.Add(this.txtCodProveedor);
            this.gpoCliente.Controls.Add(this.txtCodFabrica);
            this.gpoCliente.Controls.Add(this.txtCodTipoProd);
            this.gpoCliente.Controls.Add(this.txtCodTipoArt);
            this.gpoCliente.Controls.Add(this.txtCodMarcaArt);
            this.gpoCliente.Controls.Add(this.txtCodRubroArt);
            this.gpoCliente.Controls.Add(this.txtCodFamiliaArt);
            this.gpoCliente.Controls.Add(this.txtCantXReposicion);
            this.gpoCliente.Controls.Add(this.label19);
            this.gpoCliente.Controls.Add(this.txtCantPtoPedido);
            this.gpoCliente.Controls.Add(this.label18);
            this.gpoCliente.Controls.Add(this.txtCantActual);
            this.gpoCliente.Controls.Add(this.label17);
            this.gpoCliente.Controls.Add(this.btnUltimoProveedor);
            this.gpoCliente.Controls.Add(this.cboRazonSocialProveedor);
            this.gpoCliente.Controls.Add(this.label16);
            this.gpoCliente.Controls.Add(this.txtCostoEnLista);
            this.gpoCliente.Controls.Add(this.label15);
            this.gpoCliente.Controls.Add(this.txtCostoListaProc);
            this.gpoCliente.Controls.Add(this.label13);
            this.gpoCliente.Controls.Add(this.txtCosto);
            this.gpoCliente.Controls.Add(this.label11);
            this.gpoCliente.Controls.Add(this.btnTipoProducto);
            this.gpoCliente.Controls.Add(this.cboTipoProd);
            this.gpoCliente.Controls.Add(this.label12);
            this.gpoCliente.Controls.Add(this.btnTipoArticulo);
            this.gpoCliente.Controls.Add(this.cboTipoArt);
            this.gpoCliente.Controls.Add(this.label10);
            this.gpoCliente.Controls.Add(this.btnMarca);
            this.gpoCliente.Controls.Add(this.cboMarcaArt);
            this.gpoCliente.Controls.Add(this.label6);
            this.gpoCliente.Controls.Add(this.btnRubro);
            this.gpoCliente.Controls.Add(this.cboRubroArt);
            this.gpoCliente.Controls.Add(this.label4);
            this.gpoCliente.Controls.Add(this.btnFamilia);
            this.gpoCliente.Controls.Add(this.btcCerrar);
            this.gpoCliente.Controls.Add(this.btnGuardar);
            this.gpoCliente.Controls.Add(this.txtSituacion);
            this.gpoCliente.Controls.Add(this.label14);
            this.gpoCliente.Controls.Add(this.label9);
            this.gpoCliente.Controls.Add(this.txtUnidadMedida);
            this.gpoCliente.Controls.Add(this.label8);
            this.gpoCliente.Controls.Add(this.cmbDescripcionFamiliaArt);
            this.gpoCliente.Controls.Add(this.label7);
            this.gpoCliente.Controls.Add(this.txtUnidadVenta);
            this.gpoCliente.Controls.Add(this.label5);
            this.gpoCliente.Controls.Add(this.txtCodigoArticulo);
            this.gpoCliente.Controls.Add(this.label3);
            this.gpoCliente.Controls.Add(this.txtDescripcionArticulo);
            this.gpoCliente.Controls.Add(this.label2);
            this.gpoCliente.Location = new System.Drawing.Point(12, 380);
            this.gpoCliente.Name = "gpoCliente";
            this.gpoCliente.Size = new System.Drawing.Size(831, 265);
            this.gpoCliente.TabIndex = 30;
            this.gpoCliente.TabStop = false;
            // 
            // cboAlicuota
            // 
            this.cboAlicuota.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboAlicuota.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboAlicuota.FormattingEnabled = true;
            this.cboAlicuota.Location = new System.Drawing.Point(184, 203);
            this.cboAlicuota.Name = "cboAlicuota";
            this.cboAlicuota.Size = new System.Drawing.Size(50, 21);
            this.cboAlicuota.TabIndex = 65;
            // 
            // txtCodAlicuota
            // 
            this.txtCodAlicuota.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodAlicuota.Location = new System.Drawing.Point(128, 204);
            this.txtCodAlicuota.Name = "txtCodAlicuota";
            this.txtCodAlicuota.Size = new System.Drawing.Size(50, 20);
            this.txtCodAlicuota.TabIndex = 22;
            this.txtCodAlicuota.TextChanged += new System.EventHandler(this.txtCodImpuesto_TextChanged);
            // 
            // btnImpuestoArt
            // 
            this.btnImpuestoArt.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnImpuestoArt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImpuestoArt.Image = ((System.Drawing.Image)(resources.GetObject("btnImpuestoArt.Image")));
            this.btnImpuestoArt.Location = new System.Drawing.Point(240, 201);
            this.btnImpuestoArt.Name = "btnImpuestoArt";
            this.btnImpuestoArt.Size = new System.Drawing.Size(30, 25);
            this.btnImpuestoArt.TabIndex = 24;
            this.btnImpuestoArt.UseVisualStyleBackColor = false;
            this.btnImpuestoArt.Click += new System.EventHandler(this.btnImpuestoArt_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(33, 206);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(89, 13);
            this.label20.TabIndex = 63;
            this.label20.Text = "Código Impuesto:";
            // 
            // bntVerQRgenerado
            // 
            this.bntVerQRgenerado.Cursor = System.Windows.Forms.Cursors.Default;
            this.bntVerQRgenerado.Image = ((System.Drawing.Image)(resources.GetObject("bntVerQRgenerado.Image")));
            this.bntVerQRgenerado.Location = new System.Drawing.Point(240, 15);
            this.bntVerQRgenerado.Name = "bntVerQRgenerado";
            this.bntVerQRgenerado.Size = new System.Drawing.Size(30, 25);
            this.bntVerQRgenerado.TabIndex = 5;
            this.bntVerQRgenerado.UseVisualStyleBackColor = true;
            this.bntVerQRgenerado.Click += new System.EventHandler(this.bntVerQRgenerado_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEliminar.Enabled = false;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(632, 234);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(85, 25);
            this.btnEliminar.TabIndex = 40;
            this.btnEliminar.Text = "   Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.Location = new System.Drawing.Point(541, 234);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(85, 25);
            this.btnModificar.TabIndex = 39;
            this.btnModificar.Text = "   Actualizar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // txtCodProveedor
            // 
            this.txtCodProveedor.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodProveedor.Location = new System.Drawing.Point(543, 124);
            this.txtCodProveedor.Name = "txtCodProveedor";
            this.txtCodProveedor.Size = new System.Drawing.Size(50, 20);
            this.txtCodProveedor.TabIndex = 31;
            this.txtCodProveedor.TextChanged += new System.EventHandler(this.txtProveedor_TextChanged);
            // 
            // txtCodFabrica
            // 
            this.txtCodFabrica.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodFabrica.Location = new System.Drawing.Point(543, 45);
            this.txtCodFabrica.Name = "txtCodFabrica";
            this.txtCodFabrica.Size = new System.Drawing.Size(265, 20);
            this.txtCodFabrica.TabIndex = 27;
            this.txtCodFabrica.Leave += new System.EventHandler(this.txtCodFabrica_Leave);
            // 
            // txtCodTipoProd
            // 
            this.txtCodTipoProd.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodTipoProd.Location = new System.Drawing.Point(128, 177);
            this.txtCodTipoProd.Name = "txtCodTipoProd";
            this.txtCodTipoProd.Size = new System.Drawing.Size(50, 20);
            this.txtCodTipoProd.TabIndex = 19;
            this.txtCodTipoProd.TextChanged += new System.EventHandler(this.txtCodTipoProd_TextChanged);
            this.txtCodTipoProd.Leave += new System.EventHandler(this.txtCodTipoProd_Leave);
            // 
            // txtCodTipoArt
            // 
            this.txtCodTipoArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodTipoArt.Location = new System.Drawing.Point(128, 150);
            this.txtCodTipoArt.Name = "txtCodTipoArt";
            this.txtCodTipoArt.Size = new System.Drawing.Size(50, 20);
            this.txtCodTipoArt.TabIndex = 16;
            this.txtCodTipoArt.TextChanged += new System.EventHandler(this.txtCodTipoArt_TextChanged);
            this.txtCodTipoArt.Leave += new System.EventHandler(this.txtCodTipoArt_Leave);
            // 
            // txtCodMarcaArt
            // 
            this.txtCodMarcaArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodMarcaArt.Location = new System.Drawing.Point(128, 123);
            this.txtCodMarcaArt.Name = "txtCodMarcaArt";
            this.txtCodMarcaArt.Size = new System.Drawing.Size(50, 20);
            this.txtCodMarcaArt.TabIndex = 13;
            this.txtCodMarcaArt.TextChanged += new System.EventHandler(this.txtCodMarcaArt_TextChanged);
            this.txtCodMarcaArt.Leave += new System.EventHandler(this.txtCodMarcaArt_Leave);
            // 
            // txtCodRubroArt
            // 
            this.txtCodRubroArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodRubroArt.Location = new System.Drawing.Point(128, 97);
            this.txtCodRubroArt.Name = "txtCodRubroArt";
            this.txtCodRubroArt.Size = new System.Drawing.Size(50, 20);
            this.txtCodRubroArt.TabIndex = 10;
            this.txtCodRubroArt.TextChanged += new System.EventHandler(this.txtCodRubroArt_TextChanged);
            this.txtCodRubroArt.Leave += new System.EventHandler(this.txtCodRubroArt_Leave);
            // 
            // txtCodFamiliaArt
            // 
            this.txtCodFamiliaArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodFamiliaArt.Location = new System.Drawing.Point(128, 71);
            this.txtCodFamiliaArt.Name = "txtCodFamiliaArt";
            this.txtCodFamiliaArt.Size = new System.Drawing.Size(50, 20);
            this.txtCodFamiliaArt.TabIndex = 7;
            this.txtCodFamiliaArt.TextChanged += new System.EventHandler(this.txtCodFamiliaArt_TextChanged);
            this.txtCodFamiliaArt.Leave += new System.EventHandler(this.txtCodFamiliaArt_Leave);
            // 
            // txtCantXReposicion
            // 
            this.txtCantXReposicion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtCantXReposicion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantXReposicion.Location = new System.Drawing.Point(543, 176);
            this.txtCantXReposicion.Name = "txtCantXReposicion";
            this.txtCantXReposicion.Size = new System.Drawing.Size(50, 20);
            this.txtCantXReposicion.TabIndex = 36;
            this.txtCantXReposicion.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(438, 180);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(99, 13);
            this.label19.TabIndex = 58;
            this.label19.Text = "Cant. x Reposición:";
            // 
            // txtCantPtoPedido
            // 
            this.txtCantPtoPedido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtCantPtoPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantPtoPedido.Location = new System.Drawing.Point(758, 176);
            this.txtCantPtoPedido.Name = "txtCantPtoPedido";
            this.txtCantPtoPedido.Size = new System.Drawing.Size(50, 20);
            this.txtCantPtoPedido.TabIndex = 37;
            this.txtCantPtoPedido.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(662, 179);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(90, 13);
            this.label18.TabIndex = 56;
            this.label18.Text = "Cantidad Mínima:";
            // 
            // txtCantActual
            // 
            this.txtCantActual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtCantActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantActual.Location = new System.Drawing.Point(543, 150);
            this.txtCantActual.Name = "txtCantActual";
            this.txtCantActual.Size = new System.Drawing.Size(50, 20);
            this.txtCantActual.TabIndex = 34;
            this.txtCantActual.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(469, 152);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(68, 13);
            this.label17.TabIndex = 54;
            this.label17.Text = "Cant. Actual:";
            // 
            // btnUltimoProveedor
            // 
            this.btnUltimoProveedor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnUltimoProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUltimoProveedor.Image = ((System.Drawing.Image)(resources.GetObject("btnUltimoProveedor.Image")));
            this.btnUltimoProveedor.Location = new System.Drawing.Point(778, 119);
            this.btnUltimoProveedor.Name = "btnUltimoProveedor";
            this.btnUltimoProveedor.Size = new System.Drawing.Size(30, 25);
            this.btnUltimoProveedor.TabIndex = 33;
            this.btnUltimoProveedor.UseVisualStyleBackColor = false;
            this.btnUltimoProveedor.Click += new System.EventHandler(this.btnUltimoProveedor_Click);
            // 
            // cboRazonSocialProveedor
            // 
            this.cboRazonSocialProveedor.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboRazonSocialProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboRazonSocialProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRazonSocialProveedor.FormattingEnabled = true;
            this.cboRazonSocialProveedor.Location = new System.Drawing.Point(599, 123);
            this.cboRazonSocialProveedor.Name = "cboRazonSocialProveedor";
            this.cboRazonSocialProveedor.Size = new System.Drawing.Size(173, 21);
            this.cboRazonSocialProveedor.TabIndex = 32;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(446, 126);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(91, 13);
            this.label16.TabIndex = 52;
            this.label16.Text = "Último Proveedor:";
            // 
            // txtCostoEnLista
            // 
            this.txtCostoEnLista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtCostoEnLista.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCostoEnLista.Location = new System.Drawing.Point(758, 150);
            this.txtCostoEnLista.Name = "txtCostoEnLista";
            this.txtCostoEnLista.ReadOnly = true;
            this.txtCostoEnLista.Size = new System.Drawing.Size(50, 20);
            this.txtCostoEnLista.TabIndex = 35;
            this.txtCostoEnLista.Text = "$ 0,000";
            this.txtCostoEnLista.Enter += new System.EventHandler(this.txtCostoEnLista_Enter);
            this.txtCostoEnLista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCostoEnLista_KeyPress);
            this.txtCostoEnLista.Leave += new System.EventHandler(this.txtCostoEnLista_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(675, 153);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 13);
            this.label15.TabIndex = 49;
            this.label15.Text = "Costo en Lista:";
            // 
            // txtCostoListaProc
            // 
            this.txtCostoListaProc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtCostoListaProc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCostoListaProc.Location = new System.Drawing.Point(758, 96);
            this.txtCostoListaProc.Name = "txtCostoListaProc";
            this.txtCostoListaProc.Size = new System.Drawing.Size(50, 20);
            this.txtCostoListaProc.TabIndex = 30;
            this.txtCostoListaProc.Text = "0";
            this.txtCostoListaProc.Enter += new System.EventHandler(this.txtCostoListaProc_Enter);
            this.txtCostoListaProc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCostoListaProc_KeyPress);
            this.txtCostoListaProc.Leave += new System.EventHandler(this.txtCostoListaProc_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(663, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 13);
            this.label13.TabIndex = 47;
            this.label13.Text = "% Costo en Lista:";
            // 
            // txtCosto
            // 
            this.txtCosto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtCosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCosto.Location = new System.Drawing.Point(543, 98);
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.Size = new System.Drawing.Size(50, 20);
            this.txtCosto.TabIndex = 29;
            this.txtCosto.Text = "$ 0,000";
            this.txtCosto.Enter += new System.EventHandler(this.txtCosto_Enter);
            this.txtCosto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCosto_KeyPress);
            this.txtCosto.Leave += new System.EventHandler(this.txtCosto_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(500, 100);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 45;
            this.label11.Text = "Costo:";
            // 
            // btnTipoProducto
            // 
            this.btnTipoProducto.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTipoProducto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTipoProducto.Image = ((System.Drawing.Image)(resources.GetObject("btnTipoProducto.Image")));
            this.btnTipoProducto.Location = new System.Drawing.Point(364, 174);
            this.btnTipoProducto.Name = "btnTipoProducto";
            this.btnTipoProducto.Size = new System.Drawing.Size(30, 25);
            this.btnTipoProducto.TabIndex = 21;
            this.btnTipoProducto.UseVisualStyleBackColor = false;
            this.btnTipoProducto.Click += new System.EventHandler(this.btnTipoProducto_Click);
            // 
            // cboTipoProd
            // 
            this.cboTipoProd.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboTipoProd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboTipoProd.FormattingEnabled = true;
            this.cboTipoProd.Location = new System.Drawing.Point(184, 176);
            this.cboTipoProd.Name = "cboTipoProd";
            this.cboTipoProd.Size = new System.Drawing.Size(174, 21);
            this.cboTipoProd.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(46, 183);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 43;
            this.label12.Text = "Cód. Tipo IVA:";
            // 
            // btnTipoArticulo
            // 
            this.btnTipoArticulo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTipoArticulo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTipoArticulo.Image = ((System.Drawing.Image)(resources.GetObject("btnTipoArticulo.Image")));
            this.btnTipoArticulo.Location = new System.Drawing.Point(364, 147);
            this.btnTipoArticulo.Name = "btnTipoArticulo";
            this.btnTipoArticulo.Size = new System.Drawing.Size(30, 25);
            this.btnTipoArticulo.TabIndex = 18;
            this.btnTipoArticulo.UseVisualStyleBackColor = false;
            this.btnTipoArticulo.Click += new System.EventHandler(this.btnTipoArticulo_Click);
            // 
            // cboTipoArt
            // 
            this.cboTipoArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboTipoArt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboTipoArt.FormattingEnabled = true;
            this.cboTipoArt.Location = new System.Drawing.Point(184, 149);
            this.cboTipoArt.Name = "cboTipoArt";
            this.cboTipoArt.Size = new System.Drawing.Size(174, 21);
            this.cboTipoArt.TabIndex = 17;
            this.cboTipoArt.SelectedIndexChanged += new System.EventHandler(this.cboTipoArt_SelectedIndexChanged);
            this.cboTipoArt.Leave += new System.EventHandler(this.cboTipoArt_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(47, 157);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 40;
            this.label10.Text = "Cód. Tipo I.B.:";
            // 
            // btnMarca
            // 
            this.btnMarca.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnMarca.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMarca.Image = ((System.Drawing.Image)(resources.GetObject("btnMarca.Image")));
            this.btnMarca.Location = new System.Drawing.Point(364, 120);
            this.btnMarca.Name = "btnMarca";
            this.btnMarca.Size = new System.Drawing.Size(30, 25);
            this.btnMarca.TabIndex = 15;
            this.btnMarca.UseVisualStyleBackColor = false;
            this.btnMarca.Click += new System.EventHandler(this.btnMarca_Click);
            // 
            // cboMarcaArt
            // 
            this.cboMarcaArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboMarcaArt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboMarcaArt.FormattingEnabled = true;
            this.cboMarcaArt.Location = new System.Drawing.Point(184, 122);
            this.cboMarcaArt.Name = "cboMarcaArt";
            this.cboMarcaArt.Size = new System.Drawing.Size(174, 21);
            this.cboMarcaArt.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Código Marca:";
            // 
            // btnRubro
            // 
            this.btnRubro.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRubro.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRubro.Image = ((System.Drawing.Image)(resources.GetObject("btnRubro.Image")));
            this.btnRubro.Location = new System.Drawing.Point(364, 95);
            this.btnRubro.Name = "btnRubro";
            this.btnRubro.Size = new System.Drawing.Size(30, 25);
            this.btnRubro.TabIndex = 12;
            this.btnRubro.UseVisualStyleBackColor = false;
            this.btnRubro.Click += new System.EventHandler(this.btnRubro_Click);
            // 
            // cboRubroArt
            // 
            this.cboRubroArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboRubroArt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboRubroArt.FormattingEnabled = true;
            this.cboRubroArt.Location = new System.Drawing.Point(184, 96);
            this.cboRubroArt.Name = "cboRubroArt";
            this.cboRubroArt.Size = new System.Drawing.Size(174, 21);
            this.cboRubroArt.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Código Rubro:";
            // 
            // btnFamilia
            // 
            this.btnFamilia.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFamilia.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnFamilia.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFamilia.Image = ((System.Drawing.Image)(resources.GetObject("btnFamilia.Image")));
            this.btnFamilia.Location = new System.Drawing.Point(364, 68);
            this.btnFamilia.Name = "btnFamilia";
            this.btnFamilia.Size = new System.Drawing.Size(30, 25);
            this.btnFamilia.TabIndex = 9;
            this.btnFamilia.UseVisualStyleBackColor = false;
            this.btnFamilia.Click += new System.EventHandler(this.btnFamilia_Click);
            // 
            // btcCerrar
            // 
            this.btcCerrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btcCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btcCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btcCerrar.Image")));
            this.btcCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btcCerrar.Location = new System.Drawing.Point(723, 234);
            this.btcCerrar.Name = "btcCerrar";
            this.btcCerrar.Size = new System.Drawing.Size(85, 25);
            this.btcCerrar.TabIndex = 41;
            this.btcCerrar.Text = "   Cerrar";
            this.btcCerrar.UseVisualStyleBackColor = false;
            this.btcCerrar.Click += new System.EventHandler(this.btcCerrar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(450, 234);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 25);
            this.btnGuardar.TabIndex = 38;
            this.btnGuardar.Text = "   Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtSituacion
            // 
            this.txtSituacion.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtSituacion.Location = new System.Drawing.Point(543, 72);
            this.txtSituacion.Name = "txtSituacion";
            this.txtSituacion.ReadOnly = true;
            this.txtSituacion.Size = new System.Drawing.Size(50, 20);
            this.txtSituacion.TabIndex = 28;
            this.txtSituacion.Leave += new System.EventHandler(this.txtSituacion_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(483, 74);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Situación:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(456, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Código Fábrica:";
            // 
            // txtUnidadMedida
            // 
            this.txtUnidadMedida.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtUnidadMedida.Location = new System.Drawing.Point(543, 19);
            this.txtUnidadMedida.Name = "txtUnidadMedida";
            this.txtUnidadMedida.Size = new System.Drawing.Size(50, 20);
            this.txtUnidadMedida.TabIndex = 26;
            this.txtUnidadMedida.Text = "-";
            this.txtUnidadMedida.Leave += new System.EventHandler(this.txtUnidadMedida_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(455, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Unidad Medida:";
            // 
            // cmbDescripcionFamiliaArt
            // 
            this.cmbDescripcionFamiliaArt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cmbDescripcionFamiliaArt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbDescripcionFamiliaArt.FormattingEnabled = true;
            this.cmbDescripcionFamiliaArt.Location = new System.Drawing.Point(184, 70);
            this.cmbDescripcionFamiliaArt.Name = "cmbDescripcionFamiliaArt";
            this.cmbDescripcionFamiliaArt.Size = new System.Drawing.Size(174, 21);
            this.cmbDescripcionFamiliaArt.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Código Flia Art.:";
            // 
            // txtUnidadVenta
            // 
            this.txtUnidadVenta.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtUnidadVenta.Location = new System.Drawing.Point(344, 203);
            this.txtUnidadVenta.Name = "txtUnidadVenta";
            this.txtUnidadVenta.Size = new System.Drawing.Size(50, 20);
            this.txtUnidadVenta.TabIndex = 25;
            this.txtUnidadVenta.Text = "0";
            this.txtUnidadVenta.Leave += new System.EventHandler(this.txtUnidadVenta_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(272, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Unid. Venta:";
            // 
            // txtCodigoArticulo
            // 
            this.txtCodigoArticulo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCodigoArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoArticulo.Location = new System.Drawing.Point(128, 18);
            this.txtCodigoArticulo.Name = "txtCodigoArticulo";
            this.txtCodigoArticulo.Size = new System.Drawing.Size(106, 20);
            this.txtCodigoArticulo.TabIndex = 4;
            this.txtCodigoArticulo.TextChanged += new System.EventHandler(this.txtCodigoArticulo_TextChanged);
            this.txtCodigoArticulo.Enter += new System.EventHandler(this.txtCodigoArticulo_Enter);
            this.txtCodigoArticulo.Leave += new System.EventHandler(this.txtCodigoArticulo_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Código:";
            // 
            // txtDescripcionArticulo
            // 
            this.txtDescripcionArticulo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtDescripcionArticulo.Location = new System.Drawing.Point(128, 44);
            this.txtDescripcionArticulo.Name = "txtDescripcionArticulo";
            this.txtDescripcionArticulo.Size = new System.Drawing.Size(266, 20);
            this.txtDescripcionArticulo.TabIndex = 6;
            this.txtDescripcionArticulo.Leave += new System.EventHandler(this.txtDescripcionArticulo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Descripción:";
            // 
            // cboBuscaArticulo
            // 
            this.cboBuscaArticulo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cboBuscaArticulo.FormattingEnabled = true;
            this.cboBuscaArticulo.Items.AddRange(new object[] {
            "Descripción",
            "Cód. Artículo"});
            this.cboBuscaArticulo.Location = new System.Drawing.Point(78, 19);
            this.cboBuscaArticulo.Name = "cboBuscaArticulo";
            this.cboBuscaArticulo.Size = new System.Drawing.Size(120, 21);
            this.cboBuscaArticulo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Buscar por:";
            // 
            // txtBuscarArticulo
            // 
            this.txtBuscarArticulo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtBuscarArticulo.Location = new System.Drawing.Point(204, 19);
            this.txtBuscarArticulo.Name = "txtBuscarArticulo";
            this.txtBuscarArticulo.Size = new System.Drawing.Size(150, 20);
            this.txtBuscarArticulo.TabIndex = 2;
            this.txtBuscarArticulo.TextChanged += new System.EventHandler(this.txtBuscarArticulo_TextChanged);
            // 
            // tlsBarArticulo
            // 
            this.tlsBarArticulo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tlsBarArticulo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsBarArticulo.Dock = System.Windows.Forms.DockStyle.None;
            this.tlsBarArticulo.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsBarArticulo.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tlsBarArticulo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tlsBarArticulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnNuevo,
            this.tsBtnModificar,
            this.tsBtnBuscar,
            this.toolStripSeparator5,
            this.tsBtnRePag,
            this.tsTextPag,
            this.tsBtnAvPag,
            this.toolStripSeparator2,
            this.tsBtnComponente,
            this.tsArtGestionBarCode,
            this.toolStripSeparator4,
            this.tsBtnReporte,
            this.toolStripSeparator1,
            this.tsBtnSalir,
            this.toolStripSeparator3});
            this.tlsBarArticulo.Location = new System.Drawing.Point(459, 16);
            this.tlsBarArticulo.Name = "tlsBarArticulo";
            this.tlsBarArticulo.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tlsBarArticulo.Size = new System.Drawing.Size(367, 31);
            this.tlsBarArticulo.Stretch = true;
            this.tlsBarArticulo.TabIndex = 3;
            this.tlsBarArticulo.Text = "tlsBarArticulo";
            // 
            // tsBtnNuevo
            // 
            this.tsBtnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnNuevo.Image")));
            this.tsBtnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnNuevo.Name = "tsBtnNuevo";
            this.tsBtnNuevo.Size = new System.Drawing.Size(28, 28);
            this.tsBtnNuevo.Text = "Nuevo Artículo";
            this.tsBtnNuevo.Click += new System.EventHandler(this.tsBtnNuevo_Click);
            // 
            // tsBtnModificar
            // 
            this.tsBtnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnModificar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnModificar.Image")));
            this.tsBtnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnModificar.Name = "tsBtnModificar";
            this.tsBtnModificar.Size = new System.Drawing.Size(28, 28);
            this.tsBtnModificar.Text = "Modificar Item";
            this.tsBtnModificar.Click += new System.EventHandler(this.tsBtnModificar_Click);
            // 
            // tsBtnBuscar
            // 
            this.tsBtnBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnBuscar.Image")));
            this.tsBtnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnBuscar.Name = "tsBtnBuscar";
            this.tsBtnBuscar.Size = new System.Drawing.Size(28, 28);
            this.tsBtnBuscar.Text = "Buscar Item";
            this.tsBtnBuscar.Click += new System.EventHandler(this.tsBtnBuscar_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // tsBtnRePag
            // 
            this.tsBtnRePag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnRePag.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnRePag.Image")));
            this.tsBtnRePag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRePag.Name = "tsBtnRePag";
            this.tsBtnRePag.Size = new System.Drawing.Size(28, 28);
            this.tsBtnRePag.Text = "Retroceder Página";
            this.tsBtnRePag.Click += new System.EventHandler(this.tsBtnRePag_Click);
            // 
            // tsTextPag
            // 
            this.tsTextPag.Name = "tsTextPag";
            this.tsTextPag.Size = new System.Drawing.Size(80, 31);
            // 
            // tsBtnAvPag
            // 
            this.tsBtnAvPag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnAvPag.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnAvPag.Image")));
            this.tsBtnAvPag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAvPag.Name = "tsBtnAvPag";
            this.tsBtnAvPag.Size = new System.Drawing.Size(28, 28);
            this.tsBtnAvPag.Text = "Avanzar Página";
            this.tsBtnAvPag.Click += new System.EventHandler(this.tsBtnAvPag_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsBtnComponente
            // 
            this.tsBtnComponente.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnComponente.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnComponente.Image")));
            this.tsBtnComponente.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnComponente.Name = "tsBtnComponente";
            this.tsBtnComponente.Size = new System.Drawing.Size(28, 28);
            this.tsBtnComponente.Text = "Componentes para Fabricación";
            this.tsBtnComponente.Click += new System.EventHandler(this.tsBtnComponente_Click);
            // 
            // tsArtGestionBarCode
            // 
            this.tsArtGestionBarCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsArtGestionBarCode.Image = ((System.Drawing.Image)(resources.GetObject("tsArtGestionBarCode.Image")));
            this.tsArtGestionBarCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsArtGestionBarCode.Name = "tsArtGestionBarCode";
            this.tsArtGestionBarCode.Size = new System.Drawing.Size(28, 28);
            this.tsArtGestionBarCode.Text = "Gestion de Códigos de Barra";
            this.tsArtGestionBarCode.Click += new System.EventHandler(this.tsArtGestionBarCode_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // tsBtnReporte
            // 
            this.tsBtnReporte.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnReporte.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnReporte.Image")));
            this.tsBtnReporte.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnReporte.Name = "tsBtnReporte";
            this.tsBtnReporte.Size = new System.Drawing.Size(28, 28);
            this.tsBtnReporte.Text = "Visualizar Reportes";
            this.tsBtnReporte.Click += new System.EventHandler(this.tsBtnReporte_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsBtnSalir
            // 
            this.tsBtnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSalir.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSalir.Image")));
            this.tsBtnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSalir.Name = "tsBtnSalir";
            this.tsBtnSalir.Size = new System.Drawing.Size(28, 28);
            this.tsBtnSalir.Text = "Salir del Módulo Artículos";
            this.tsBtnSalir.Click += new System.EventHandler(this.tsBtnSalir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.tlsBarArticulo);
            this.groupBox1.Controls.Add(this.txtBuscarArticulo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboBuscaArticulo);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(831, 50);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            // 
            // gpQRcode
            // 
            this.gpQRcode.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpQRcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.gpQRcode.Controls.Add(this.btnQuitarQR);
            this.gpQRcode.Controls.Add(this.btnVerQR);
            this.gpQRcode.Controls.Add(this.btnQRGenerar);
            this.gpQRcode.Controls.Add(this.btnQRCerrar);
            this.gpQRcode.Controls.Add(this.btnQRImprimir);
            this.gpQRcode.Controls.Add(this.QRpicture);
            this.gpQRcode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gpQRcode.Location = new System.Drawing.Point(601, 59);
            this.gpQRcode.Name = "gpQRcode";
            this.gpQRcode.Size = new System.Drawing.Size(240, 278);
            this.gpQRcode.TabIndex = 32;
            this.gpQRcode.TabStop = false;
            this.gpQRcode.Text = "Gestión de Códigos QR";
            this.gpQRcode.Visible = false;
            // 
            // btnQuitarQR
            // 
            this.btnQuitarQR.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnQuitarQR.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnQuitarQR.Image = ((System.Drawing.Image)(resources.GetObject("btnQuitarQR.Image")));
            this.btnQuitarQR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuitarQR.Location = new System.Drawing.Point(144, 216);
            this.btnQuitarQR.Name = "btnQuitarQR";
            this.btnQuitarQR.Size = new System.Drawing.Size(67, 25);
            this.btnQuitarQR.TabIndex = 43;
            this.btnQuitarQR.Text = "    Quitar";
            this.btnQuitarQR.UseVisualStyleBackColor = false;
            this.btnQuitarQR.Click += new System.EventHandler(this.btnQuitarQR_Click);
            // 
            // btnVerQR
            // 
            this.btnVerQR.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnVerQR.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVerQR.Image = ((System.Drawing.Image)(resources.GetObject("btnVerQR.Image")));
            this.btnVerQR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVerQR.Location = new System.Drawing.Point(31, 216);
            this.btnVerQR.Name = "btnVerQR";
            this.btnVerQR.Size = new System.Drawing.Size(67, 25);
            this.btnVerQR.TabIndex = 42;
            this.btnVerQR.Text = "   Ver";
            this.btnVerQR.UseVisualStyleBackColor = false;
            this.btnVerQR.Click += new System.EventHandler(this.btnVerQR_Click);
            // 
            // btnQRGenerar
            // 
            this.btnQRGenerar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnQRGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnQRGenerar.Image = ((System.Drawing.Image)(resources.GetObject("btnQRGenerar.Image")));
            this.btnQRGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQRGenerar.Location = new System.Drawing.Point(8, 247);
            this.btnQRGenerar.Name = "btnQRGenerar";
            this.btnQRGenerar.Size = new System.Drawing.Size(70, 25);
            this.btnQRGenerar.TabIndex = 44;
            this.btnQRGenerar.Text = "     Generar";
            this.btnQRGenerar.UseVisualStyleBackColor = false;
            this.btnQRGenerar.Click += new System.EventHandler(this.btnQRGenerar_Click);
            // 
            // btnQRCerrar
            // 
            this.btnQRCerrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnQRCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnQRCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnQRCerrar.Image")));
            this.btnQRCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQRCerrar.Location = new System.Drawing.Point(160, 248);
            this.btnQRCerrar.Name = "btnQRCerrar";
            this.btnQRCerrar.Size = new System.Drawing.Size(70, 25);
            this.btnQRCerrar.TabIndex = 46;
            this.btnQRCerrar.Text = "   Cerrar";
            this.btnQRCerrar.UseVisualStyleBackColor = false;
            this.btnQRCerrar.Click += new System.EventHandler(this.btnQRCerrar_Click);
            // 
            // btnQRImprimir
            // 
            this.btnQRImprimir.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnQRImprimir.Enabled = false;
            this.btnQRImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnQRImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnQRImprimir.Image")));
            this.btnQRImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQRImprimir.Location = new System.Drawing.Point(84, 247);
            this.btnQRImprimir.Name = "btnQRImprimir";
            this.btnQRImprimir.Size = new System.Drawing.Size(70, 25);
            this.btnQRImprimir.TabIndex = 45;
            this.btnQRImprimir.Text = "      Imprimir";
            this.btnQRImprimir.UseVisualStyleBackColor = false;
            this.btnQRImprimir.Click += new System.EventHandler(this.btnQRImprimir_Click);
            // 
            // QRpicture
            // 
            this.QRpicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.QRpicture.ErrorImage = ((System.Drawing.Image)(resources.GetObject("QRpicture.ErrorImage")));
            this.QRpicture.Image = ((System.Drawing.Image)(resources.GetObject("QRpicture.Image")));
            this.QRpicture.Location = new System.Drawing.Point(31, 29);
            this.QRpicture.Name = "QRpicture";
            this.QRpicture.Size = new System.Drawing.Size(180, 181);
            this.QRpicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.QRpicture.TabIndex = 0;
            this.QRpicture.TabStop = false;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(12, 284);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(10, 10);
            this.btnSalir.TabIndex = 84;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lvwArticulo
            // 
            this.lvwArticulo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lvwArticulo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.Codigo,
            this.DescripciónArtículo,
            this.Imagen,
            this.Estado,
            this.Cantidad});
            this.lvwArticulo.FullRowSelect = true;
            this.lvwArticulo.GridLines = true;
            this.lvwArticulo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwArticulo.HideSelection = false;
            this.lvwArticulo.LargeImageList = this.imageList1;
            this.lvwArticulo.Location = new System.Drawing.Point(12, 59);
            this.lvwArticulo.MultiSelect = false;
            this.lvwArticulo.Name = "lvwArticulo";
            this.lvwArticulo.Size = new System.Drawing.Size(831, 315);
            this.lvwArticulo.SmallImageList = this.imageList1;
            this.lvwArticulo.TabIndex = 3;
            this.lvwArticulo.UseCompatibleStateImageBehavior = false;
            this.lvwArticulo.View = System.Windows.Forms.View.Details;
            this.lvwArticulo.SelectedIndexChanged += new System.EventHandler(this.lvwArticulo_SelectedIndexChanged);
            this.lvwArticulo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvwArticulo_KeyPress);
            this.lvwArticulo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwArticulo_MouseDoubleClick);
            // 
            // Id
            // 
            this.Id.Text = "-";
            this.Id.Width = 28;
            // 
            // Codigo
            // 
            this.Codigo.Text = "Código";
            this.Codigo.Width = 100;
            // 
            // DescripciónArtículo
            // 
            this.DescripciónArtículo.Text = "Descripción Artículo";
            this.DescripciónArtículo.Width = 450;
            // 
            // Imagen
            // 
            this.Imagen.Text = "QR";
            this.Imagen.Width = 0;
            // 
            // Estado
            // 
            this.Estado.Text = "Estado";
            this.Estado.Width = 0;
            // 
            // Cantidad
            // 
            this.Cantidad.Text = "Cantidad";
            this.Cantidad.Width = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Generics.ico");
            this.imageList1.Images.SetKeyName(1, "cancel.ico");
            this.imageList1.Images.SetKeyName(2, "barcode.ico");
            this.imageList1.Images.SetKeyName(3, "System Box Empty.ico");
            this.imageList1.Images.SetKeyName(4, "ok.ico");
            // 
            // gpBarcode
            // 
            this.gpBarcode.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gpBarcode.Controls.Add(this.btnBarQuitar);
            this.gpBarcode.Controls.Add(this.btnBarGenerar);
            this.gpBarcode.Controls.Add(this.btnBarCerrar);
            this.gpBarcode.Controls.Add(this.btnBarImprimir);
            this.gpBarcode.Controls.Add(this.BarCodpicture);
            this.gpBarcode.Controls.Add(this.button2);
            this.gpBarcode.Location = new System.Drawing.Point(287, 59);
            this.gpBarcode.Name = "gpBarcode";
            this.gpBarcode.Size = new System.Drawing.Size(308, 148);
            this.gpBarcode.TabIndex = 85;
            this.gpBarcode.TabStop = false;
            this.gpBarcode.Text = "Gestión de Codigos de Barra";
            this.gpBarcode.Visible = false;
            // 
            // btnBarQuitar
            // 
            this.btnBarQuitar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBarQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBarQuitar.Image = ((System.Drawing.Image)(resources.GetObject("btnBarQuitar.Image")));
            this.btnBarQuitar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBarQuitar.Location = new System.Drawing.Point(158, 115);
            this.btnBarQuitar.Name = "btnBarQuitar";
            this.btnBarQuitar.Size = new System.Drawing.Size(67, 25);
            this.btnBarQuitar.TabIndex = 48;
            this.btnBarQuitar.Text = "    Quitar";
            this.btnBarQuitar.UseVisualStyleBackColor = false;
            this.btnBarQuitar.Click += new System.EventHandler(this.btnBarQuitar_Click);
            // 
            // btnBarGenerar
            // 
            this.btnBarGenerar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBarGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBarGenerar.Image = ((System.Drawing.Image)(resources.GetObject("btnBarGenerar.Image")));
            this.btnBarGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBarGenerar.Location = new System.Drawing.Point(6, 115);
            this.btnBarGenerar.Name = "btnBarGenerar";
            this.btnBarGenerar.Size = new System.Drawing.Size(70, 25);
            this.btnBarGenerar.TabIndex = 49;
            this.btnBarGenerar.Text = "     Generar";
            this.btnBarGenerar.UseVisualStyleBackColor = false;
            this.btnBarGenerar.Click += new System.EventHandler(this.btnBarGenerar_Click);
            // 
            // btnBarCerrar
            // 
            this.btnBarCerrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBarCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBarCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnBarCerrar.Image")));
            this.btnBarCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBarCerrar.Location = new System.Drawing.Point(231, 115);
            this.btnBarCerrar.Name = "btnBarCerrar";
            this.btnBarCerrar.Size = new System.Drawing.Size(70, 25);
            this.btnBarCerrar.TabIndex = 51;
            this.btnBarCerrar.Text = "   Cerrar";
            this.btnBarCerrar.UseVisualStyleBackColor = false;
            this.btnBarCerrar.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnBarImprimir
            // 
            this.btnBarImprimir.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBarImprimir.Enabled = false;
            this.btnBarImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBarImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnBarImprimir.Image")));
            this.btnBarImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBarImprimir.Location = new System.Drawing.Point(82, 115);
            this.btnBarImprimir.Name = "btnBarImprimir";
            this.btnBarImprimir.Size = new System.Drawing.Size(70, 25);
            this.btnBarImprimir.TabIndex = 50;
            this.btnBarImprimir.Text = "      Imprimir";
            this.btnBarImprimir.UseVisualStyleBackColor = false;
            this.btnBarImprimir.Click += new System.EventHandler(this.btnBarImprimir_Click);
            // 
            // BarCodpicture
            // 
            this.BarCodpicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BarCodpicture.ErrorImage = ((System.Drawing.Image)(resources.GetObject("BarCodpicture.ErrorImage")));
            this.BarCodpicture.Image = ((System.Drawing.Image)(resources.GetObject("BarCodpicture.Image")));
            this.BarCodpicture.Location = new System.Drawing.Point(47, 29);
            this.BarCodpicture.Name = "BarCodpicture";
            this.BarCodpicture.Size = new System.Drawing.Size(215, 80);
            this.BarCodpicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.BarCodpicture.TabIndex = 2;
            this.BarCodpicture.TabStop = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(6, 115);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 25);
            this.button2.TabIndex = 47;
            this.button2.Text = "   Ver";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            // 
            // frmArticulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(855, 657);
            this.Controls.Add(this.gpBarcode);
            this.Controls.Add(this.gpQRcode);
            this.Controls.Add(this.lvwArticulo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpoCliente);
            this.Controls.Add(this.btnSalir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmArticulo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Artículos";
            this.Load += new System.EventHandler(this.frmArticulo_Load);
            this.gpoCliente.ResumeLayout(false);
            this.gpoCliente.PerformLayout();
            this.tlsBarArticulo.ResumeLayout(false);
            this.tlsBarArticulo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpQRcode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.QRpicture)).EndInit();
            this.gpBarcode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BarCodpicture)).EndInit();
            this.ResumeLayout(false);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            gpBarcode.Visible = false;
            gpQRcode.Visible = false;
            gpoCliente.Enabled = true;
        }

        private void btnBarQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                string sCodArt;
                sCodArt = lvwArticulo.SelectedItems[0].SubItems[1].Text.Trim();

                if (lvwArticulo.SelectedItems[0].SubItems[4].Text == "3")
                    MessageBox.Show("Error el artículo esta eliminado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    string p = Application.StartupPath;
                    if (conn.ActualizarImagenQR(codArt))
                    {
                        MostrarDatos2(sCodArt,0,30);
                        this.QRpicture.ImageLocation = p + "//QRError.jpg";

                        MessageBox.Show("Código Barra Eliminado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MostrarDatos2(txtCodigoArticulo.Text.Trim(),0,30);
                        MessageBox.Show("Error al quitar el Código Barra al artículo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                string actualizar = "TIPOCODIGO='0'";
                conn.ActualizaArticulo("Articulo", actualizar, " Codigo = '" + sCodArt + "'");
            }
            catch { }
        }

        private void btnBarImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwArticulo.SelectedItems[0].SubItems[4].Text == "3")
                    MessageBox.Show("Error el artículo esta eliminado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    System.Drawing.Printing.PrintDocument myPrintDocument1 = new System.Drawing.Printing.PrintDocument();
                    PrintDialog myPrinDialog1 = new PrintDialog();
                    myPrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(myPrintDocument0_PrintPage);
                    myPrinDialog1.Document = myPrintDocument1;

                    if (myPrinDialog1.ShowDialog() == DialogResult.OK)
                        myPrintDocument1.Print();
                    /*              
                    PrintDocument tmpDoc = new PrintDocument();
                    tmpDoc.PrintPage += new PrintPageEventHandler(Tmpdoc_Print);
                    PrintPreviewDialog tmpPpd = new PrintPreviewDialog();
                    tmpPpd.Document = tmpDoc;
                    tmpPpd.ShowDialog();
                    */
                }
            }
            catch { }
        }

        private void myPrintDocument0_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap myBitmap1 = new Bitmap(BarCodpicture.Width, BarCodpicture.Height);            
            BarCodpicture.DrawToBitmap(myBitmap1, new Rectangle(0, 0, BarCodpicture.Width, BarCodpicture.Height));
            
            //e.Graphics.DrawImage(myBitmap1, 0, 0);

            //COL1
            e.Graphics.DrawImage(BarCodpicture.Image, 30, 0);
            e.Graphics.DrawImage(BarCodpicture.Image, 30, 100);
            e.Graphics.DrawImage(BarCodpicture.Image, 30, 200);
            e.Graphics.DrawImage(BarCodpicture.Image, 30, 300);
            e.Graphics.DrawImage(BarCodpicture.Image, 30, 400);
            e.Graphics.DrawImage(BarCodpicture.Image, 30, 500);
            e.Graphics.DrawImage(BarCodpicture.Image, 30, 600);
            e.Graphics.DrawImage(BarCodpicture.Image, 30, 700);
            e.Graphics.DrawImage(BarCodpicture.Image, 30, 800);
            e.Graphics.DrawImage(BarCodpicture.Image, 30, 900);
            e.Graphics.DrawImage(BarCodpicture.Image, 30, 1000);

            //COL2
            e.Graphics.DrawImage(BarCodpicture.Image, 180, 0);
            e.Graphics.DrawImage(BarCodpicture.Image, 180, 100);
            e.Graphics.DrawImage(BarCodpicture.Image, 180, 200);
            e.Graphics.DrawImage(BarCodpicture.Image, 180, 300);
            e.Graphics.DrawImage(BarCodpicture.Image, 180, 400);
            e.Graphics.DrawImage(BarCodpicture.Image, 180, 500);
            e.Graphics.DrawImage(BarCodpicture.Image, 180, 600);
            e.Graphics.DrawImage(BarCodpicture.Image, 180, 700);
            e.Graphics.DrawImage(BarCodpicture.Image, 180, 800);
            e.Graphics.DrawImage(BarCodpicture.Image, 180, 900);
            e.Graphics.DrawImage(BarCodpicture.Image, 180, 1000);

            //COL3
            e.Graphics.DrawImage(BarCodpicture.Image, 320, 0);
            e.Graphics.DrawImage(BarCodpicture.Image, 320, 100);
            e.Graphics.DrawImage(BarCodpicture.Image, 320, 200);
            e.Graphics.DrawImage(BarCodpicture.Image, 320, 300);
            e.Graphics.DrawImage(BarCodpicture.Image, 320, 400);
            e.Graphics.DrawImage(BarCodpicture.Image, 320, 500);
            e.Graphics.DrawImage(BarCodpicture.Image, 320, 600);
            e.Graphics.DrawImage(BarCodpicture.Image, 320, 700);
            e.Graphics.DrawImage(BarCodpicture.Image, 320, 800);
            e.Graphics.DrawImage(BarCodpicture.Image, 320, 900);
            e.Graphics.DrawImage(BarCodpicture.Image, 320, 1000);

            //COL4
            e.Graphics.DrawImage(BarCodpicture.Image, 460, 0);
            e.Graphics.DrawImage(BarCodpicture.Image, 460, 100);
            e.Graphics.DrawImage(BarCodpicture.Image, 460, 200);
            e.Graphics.DrawImage(BarCodpicture.Image, 460, 300);
            e.Graphics.DrawImage(BarCodpicture.Image, 460, 400);
            e.Graphics.DrawImage(BarCodpicture.Image, 460, 500);
            e.Graphics.DrawImage(BarCodpicture.Image, 460, 600);
            e.Graphics.DrawImage(BarCodpicture.Image, 460, 700);
            e.Graphics.DrawImage(BarCodpicture.Image, 460, 800);
            e.Graphics.DrawImage(BarCodpicture.Image, 460, 900);
            e.Graphics.DrawImage(BarCodpicture.Image, 460, 1000);

            //COL5
            e.Graphics.DrawImage(BarCodpicture.Image, 600, 0);
            e.Graphics.DrawImage(BarCodpicture.Image, 600, 100);
            e.Graphics.DrawImage(BarCodpicture.Image, 600, 200);
            e.Graphics.DrawImage(BarCodpicture.Image, 600, 300);
            e.Graphics.DrawImage(BarCodpicture.Image, 600, 400);
            e.Graphics.DrawImage(BarCodpicture.Image, 600, 500);
            e.Graphics.DrawImage(BarCodpicture.Image, 600, 600);
            e.Graphics.DrawImage(BarCodpicture.Image, 600, 700);
            e.Graphics.DrawImage(BarCodpicture.Image, 600, 800);
            e.Graphics.DrawImage(BarCodpicture.Image, 600, 900);
            e.Graphics.DrawImage(BarCodpicture.Image, 600, 1000);

            myBitmap1.Dispose();
        }

        private void txtCodigoArticulo_TextChanged(object sender, EventArgs e)
        {

        }

        private void tsBtnRePag_Click(object sender, EventArgs e)
        {
            if (PagRe != 0 && PagRe != 1)
            {
                int iPosterior;

                iPosterior = PagRe;
                PagRe = PagRe - 30;

                PagAv = iPosterior;

                MostrarDatos2("0", PagRe, PagAv);
            }
            else
            {
                PagRe = 0;
                PagAv = 30;
                MostrarDatos2("0", PagRe, PagAv);
            }
        }

        private void txtBuscarArticulo_TextChanged(object sender, EventArgs e)
        {

        }

        private void tsBtnAvPag_Click(object sender, EventArgs e)
        {
            int iAnterior;

            iAnterior = PagAv;
            PagAv = PagAv + 30;

            PagRe = iAnterior;
            MostrarDatos2("0", PagRe, PagAv);
        }

        /* private void Tmpdoc_Print(Object sender, PrintPageEventArgs e)
         {
             //COL1
             e.Graphics.DrawImage(BarCodpicture.Image, 30, 0);
             e.Graphics.DrawImage(BarCodpicture.Image, 30, 100);
             e.Graphics.DrawImage(BarCodpicture.Image, 30, 200);
             e.Graphics.DrawImage(BarCodpicture.Image, 30, 300);
             e.Graphics.DrawImage(BarCodpicture.Image, 30, 400);
             e.Graphics.DrawImage(BarCodpicture.Image, 30, 500);
             e.Graphics.DrawImage(BarCodpicture.Image, 30, 600);
             e.Graphics.DrawImage(BarCodpicture.Image, 30, 700);
             e.Graphics.DrawImage(BarCodpicture.Image, 30, 800);
             e.Graphics.DrawImage(BarCodpicture.Image, 30, 900);
             e.Graphics.DrawImage(BarCodpicture.Image, 30, 1000);

             //COL2
             e.Graphics.DrawImage(BarCodpicture.Image, 320, 0);
             e.Graphics.DrawImage(BarCodpicture.Image, 320, 100);
             e.Graphics.DrawImage(BarCodpicture.Image, 320, 200);
             e.Graphics.DrawImage(BarCodpicture.Image, 320, 300);
             e.Graphics.DrawImage(BarCodpicture.Image, 320, 400);
             e.Graphics.DrawImage(BarCodpicture.Image, 320, 500);
             e.Graphics.DrawImage(BarCodpicture.Image, 320, 600);
             e.Graphics.DrawImage(BarCodpicture.Image, 320, 700);
             e.Graphics.DrawImage(BarCodpicture.Image, 320, 800);
             e.Graphics.DrawImage(BarCodpicture.Image, 320, 900);
             e.Graphics.DrawImage(BarCodpicture.Image, 320, 1000);

             //COL3
             e.Graphics.DrawImage(BarCodpicture.Image, 600, 0);
             e.Graphics.DrawImage(BarCodpicture.Image, 600, 100);
             e.Graphics.DrawImage(BarCodpicture.Image, 600, 200);
             e.Graphics.DrawImage(BarCodpicture.Image, 600, 300);
             e.Graphics.DrawImage(BarCodpicture.Image, 600, 400);
             e.Graphics.DrawImage(BarCodpicture.Image, 600, 500);
             e.Graphics.DrawImage(BarCodpicture.Image, 600, 600);
             e.Graphics.DrawImage(BarCodpicture.Image, 600, 700);
             e.Graphics.DrawImage(BarCodpicture.Image, 600, 800);
             e.Graphics.DrawImage(BarCodpicture.Image, 600, 900);
             e.Graphics.DrawImage(BarCodpicture.Image, 600, 1000);
         }*/

    }
}