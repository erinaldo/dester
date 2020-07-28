using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace DGestion.Clases
{
    public class EstadosBD
    {
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");        

        //private SqlCommandBuilder cmd;
        public DataSet ds = new DataSet();
        public SqlDataAdapter da;
        public SqlCommand commando;
        public SqlDataReader leerProv;

        public void ConectarBD() {
            try {
                conectaEstado.Open();
                //   MessageBox.Show("Conectado");
            }
            catch {
                MessageBox.Show("Error al Conectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaEstado.Close();
            }
        }

        public void DesconectarBD() {
            try {
                conectaEstado.Close();
                //   MessageBox.Show("Conectado");
            }
            catch {
                MessageBox.Show("Error al Desconectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaEstado.Close();
            }
        }
        // /////////////////////////////////////////////////////



        public bool InsertarImagen(string nombreEstado, PictureBox imagen)
        {
            try {
                commando.Connection = conectaEstado;
                commando.CommandText = "INSERT INTO Estado (NombreEstado, ImagenEstado) VALUES (@Nombre, @Imagen)";

                commando.Parameters.Add("@Nombre", SqlDbType.NVarChar);                
                commando.Parameters.Add("@Imagen", SqlDbType.Image);

                commando.Parameters["Nombre"].Value = nombreEstado;      
          
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                imagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                commando.Parameters["@Imagen"].Value = ms.GetBuffer();

                conectaEstado.Open();
                int i = commando.ExecuteNonQuery();
                conectaEstado.Close();

                if (i > 0) {
                    return true;
                }
                else
                    return false;
            }
            catch { return false; }
        }

        public void VerImagen(PictureBox imagen, ComboBox combo) {

            commando = new SqlCommand("SELECT ImagenEstado FROM Estado WHERE Id = '" + combo.SelectedValue.ToString() + "'", conectaEstado);
            da = new SqlDataAdapter(commando);
            ds = new DataSet("Estado");
            da.Fill(ds, "Estado");

            byte[] Datos = new byte[0];
            DataRow DR = ds.Tables["Estado"].Rows[0];

            Datos = (byte[])DR["ImagenEstado"];

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            imagen.Image = System.Drawing.Bitmap.FromStream(ms);
        }


    }
}
