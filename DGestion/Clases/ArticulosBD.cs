using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace DGestion
{
    public class ArticulosBD {
                
        SqlConnection conecta = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");
        SqlConnection conectaLee = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private SqlCommandBuilder cmd;
        public DataSet ds = new DataSet();
        public SqlDataAdapter da;
        public SqlCommand commando;
        public SqlDataReader leer;
        public SqlDataReader leerListaArticuloVenta;

        public void ConectarBD() {
            try {
                conecta.Open();
                //   MessageBox.Show("Conectado");
            }
            catch {
                MessageBox.Show("Error al Conectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conecta.Close();
            }
        }

        public void DesconectarBD() {
            try {
                conecta.Close();
                //   MessageBox.Show("Conectado");
            }
            catch {
                MessageBox.Show("Error al Desconectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conecta.Close();
            }
        }
        // /////////////////////////////////////////////////////

        //Lecturas DataReader
        public void ConectarBDLee() {
            try {
                conectaLee.Open();               
            }
            catch {
                MessageBox.Show("Error al Conectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaLee.Close();
            }
        }

        public void DesconectarBDLee()
        {
            try {
                 conectaLee.Close();               
            }
            catch {
                MessageBox.Show("Error al Desconectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaLee.Close();
            }
        }
        // /////////////////////////////////////////////////////
        

        // GESTION ABMC ARTICULO
        public void consultaArticulo(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conecta);
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, tabla);
                da.Dispose(); cmd.Dispose();
            }
            catch {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LeeArticulo(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                conectaLee.Open();
                commando = new SqlCommand(sql, conectaLee);
                leer = commando.ExecuteReader();
                leer.Read();                
            }
            catch {
                //MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally {
                conecta.Close();
            }
        }

        public bool ActualizaArticulo(string tabla, string campos, string condicion)
        {
            if (conecta.State != ConnectionState.Open)
                conecta.Open();
            //conecta.Open();

            string actualizar = "update " + tabla + " set " + campos + " where " + condicion;
            commando = new SqlCommand(actualizar, conecta);
            int i = commando.ExecuteNonQuery();
            conecta.Close();
            if (i > 0)
                return true;
            else
                return false;
        }

        public bool InsertarArticulo(string sql) {
            try {
                conecta.Open();
                commando = new SqlCommand(sql, conecta);
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }

        public bool EliminarArticulo(string tabla, string condicion) {
            try {
                conecta.Open();
                string elimina = "delete from " + tabla + " where " + condicion;
                commando = new SqlCommand(elimina, conecta);
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }

        //GESTION AMBC FAMILIA ARTICULO//
        public void consultaFamiliaArticulo(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conecta);
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, tabla);
                da.Dispose();  cmd.Dispose();
            }
            catch {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }   
        
        public bool InsertarFamilia(string sql) {            
            try {
                conecta.Open();
                commando = new SqlCommand(sql, conecta);                  
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {               
                return false;
            }            
        }

        public bool EliminarFamilia(string tabla, string condicion) {
            try {
                conecta.Open();
                string elimina = "delete from " + tabla + " where " + condicion;
                commando = new SqlCommand(elimina, conecta);
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch  {
                return false;
            }    
        }
        
        public bool ActualizarFamilia(string tabla, string campos, string condicion) {
            conecta.Open();
            string actualizar = "update " + tabla + " set " + campos + " where " + condicion;
            commando = new SqlCommand(actualizar, conecta);
            int i = commando.ExecuteNonQuery();
            conecta.Close();
            if (i > 0)
                return true;
            else
                return false;
        }
        // /////////////////////////////////////////////////////

        //GESTION AMBC RUBRO//
        public void consultaRubroArticulo(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conecta);
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, tabla);
                da.Dispose(); cmd.Dispose();
            }
            catch {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool ActualizarRubro(string tabla, string campos, string condicion) {
            conecta.Open();
            string actualizar = "update " + tabla + " set " + campos + " where " + condicion;
            commando = new SqlCommand(actualizar, conecta);
            int i = commando.ExecuteNonQuery();
            conecta.Close();
            if (i > 0)
                return true;
            else
                return false;
        }

        public bool EliminarRubro(string tabla, string condicion) {
            try {
                conecta.Open();
                string elimina = "delete from " + tabla + " where " + condicion;
                commando = new SqlCommand(elimina, conecta);
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }

        public bool InsertarRubro(string sql) {
            try {
                conecta.Open();
                commando = new SqlCommand(sql, conecta);
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }
        // /////////////////////////////////////////////////////

        //GESTION AMBC MARCA//
        public void consultaMarcaArticulo(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conecta);
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, tabla);
                da.Dispose(); cmd.Dispose();
            }
            catch {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool ActualizarMarca(string tabla, string campos, string condicion) {
            conecta.Open();
            string actualizar = "update " + tabla + " set " + campos + " where " + condicion;
            commando = new SqlCommand(actualizar, conecta);
            int i = commando.ExecuteNonQuery();
            conecta.Close();
            if (i > 0)
                return true;
            else
                return false;
        }

        public bool EliminarMarca(string tabla, string condicion) {
            try {
                conecta.Open();
                string elimina = "delete from " + tabla + " where " + condicion;
                commando = new SqlCommand(elimina, conecta);
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }

        public bool InsertarMarca(string sql) {
            try {
                conecta.Open();
                commando = new SqlCommand(sql, conecta);
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }
        // /////////////////////////////////////////////////////

        //GESTION AMBC TIPOARTICULO//
        public void consultaTipoArticulo(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conecta);
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, tabla);
                da.Dispose(); cmd.Dispose();
            }
            catch {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool ActualizarTipoArticulo(string tabla, string campos, string condicion) {
            conecta.Open();
            string actualizar = "update " + tabla + " set " + campos + " where " + condicion;
            commando = new SqlCommand(actualizar, conecta);
            int i = commando.ExecuteNonQuery();
            conecta.Close();
            if (i > 0)
                return true;
            else
                return false;
        }

        public bool EliminarTipoArticulo(string tabla, string condicion) {
            try {
                conecta.Open();
                string elimina = "delete from " + tabla + " where " + condicion;
                commando = new SqlCommand(elimina, conecta);
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }

        public bool InsertarTipoArticulo(string sql) {
            try {
                conecta.Open();
                commando = new SqlCommand(sql, conecta);
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }
        // /////////////////////////////////////////////////////

        //GESTION AMBC TIPOPRODUCTO//
        public void consultaTipoProducto(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conecta);
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, tabla);
                da.Dispose(); cmd.Dispose();
            }
            catch {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool ActualizarTipoProducto(string tabla, string campos, string condicion) {
            conecta.Open();
            string actualizar = "update " + tabla + " set " + campos + " where " + condicion;
            commando = new SqlCommand(actualizar, conecta);
            int i = commando.ExecuteNonQuery();
            conecta.Close();
            if (i > 0)
                return true;
            else
                return false;
        }

        public bool EliminarTipoProducto(string tabla, string condicion) {
            try {
                conecta.Open();
                string elimina = "delete from " + tabla + " where " + condicion;
                commando = new SqlCommand(elimina, conecta);
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }

        public bool InsertarTipoProducto(string sql) {
            try {
                conecta.Open();
                commando = new SqlCommand(sql, conecta);
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }
        // /////////////////////////////////////////////////////

        //GESTION AMBC LISTA PRECIO VENTA ARTICULO//

        public void LeeListaPrecioVenta(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                conectaLee.Open();
                commando = new SqlCommand(sql, conectaLee);
                leerListaArticuloVenta = commando.ExecuteReader();
                leerListaArticuloVenta.Read();
            }
            catch {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally {
                conecta.Close();
            }
        }

        public void ConsultaListaPrecioVenta(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conecta);
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, tabla);
                da.Dispose(); cmd.Dispose();
            }
            catch {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ConsultaListaPrecioVenta_SIN_ERROR(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conecta);
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, tabla);
                da.Dispose(); cmd.Dispose();
            }
            catch {}
        }

        public bool ActualizarListaPrecioVenta(string tabla, string campos, string condicion) {
            conecta.Open();
            string actualizar = "update " + tabla + " set " + campos + " where " + condicion;
            commando = new SqlCommand(actualizar, conecta);
            int i = commando.ExecuteNonQuery();
            conecta.Close();
            if (i > 0)
                return true;
            else
                return false;
        }

        public bool EliminarListaPrecioVenta(string tabla, string condicion) {
            try {
                conecta.Open();
                string elimina = "delete from " + tabla + " where " + condicion;
                commando = new SqlCommand(elimina, conecta);
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }

        public bool InsertarListaPrecioVenta(string sql) {
            try {
                conecta.Open();
                commando = new SqlCommand(sql, conecta);
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }
        // /////////////////////////////////////////////////////
        
        //GESTION IMAGENES QR ARTICULOS//

        public bool InsertarImagenQR(PictureBox imagen, string CodArticulo) {
           
            try {
                commando.Connection = conecta;
                commando.CommandText = "UPDATE Articulo SET Imagen = @Imagen WHERE Codigo = '" + CodArticulo + "'";

                commando.Parameters.Add("@Imagen", SqlDbType.Image);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                imagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                commando.Parameters["@Imagen"].Value = ms.GetBuffer();


                if (conecta.State != ConnectionState.Open)
                    conecta.Open();
                
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0) {
                    return true;
                }
                else
                    return false;
            }
            catch { return false; }
        }

        public bool VerImagenQR(PictureBox imagen, string CodArticulo, int iTipoCod) {

            try
            {
                string p = Application.StartupPath;
                SqlCommand comando = new SqlCommand("SELECT Imagen FROM Articulo WHERE Codigo = '" + CodArticulo + "'", conecta);

                SqlDataAdapter dp = new SqlDataAdapter(comando);
                DataSet ds = new DataSet("Imagen");
                dp.Fill(ds, "Imagen");

                byte[] Datos = new byte[0];
                DataRow DR = ds.Tables["Imagen"].Rows[0];

                if (DR["Imagen"].ToString() != "")
                {
                    Datos = (byte[])DR["Imagen"];
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(Datos);
                    imagen.Image = System.Drawing.Bitmap.FromStream(ms);
                    return true;
                }
                else
                {
                    if (iTipoCod == 1)
                        imagen.ImageLocation = p + "//QRError.jpg";
                    else if (iTipoCod == 2)
                        imagen.ImageLocation = p + "//BarrCodError.jpg";
                    else if (iTipoCod == 0)
                        imagen.ImageLocation = p + "//CodError.jpg";

                    return false;
                }
            }
            catch { return false; }
        }

        public bool ActualizarImagenQR(string CodArticulo) {
            
            try {
                commando.Connection = conecta;
                commando.CommandText = "UPDATE Articulo SET Imagen = @Imagen WHERE Codigo = '" + CodArticulo + "'";

                commando.Parameters.Add("@Imagen", SqlDbType.Image);
                commando.Parameters["@Imagen"].Value = DBNull.Value;

                conecta.Open();
                int i = commando.ExecuteNonQuery();
                conecta.Close();

                if (i > 0) {
                    return true;
                }
                else
                    return false;
            }
            catch { return false; }
        }

        ///////////////////////////////////////////////////////
        
    }
}