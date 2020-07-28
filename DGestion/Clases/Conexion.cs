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
    public class Conexion
    {
        SqlConnection conecta = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");
                
        public DataSet ds = new DataSet();
        public SqlDataAdapter da;
        public SqlCommand comando;

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

        public bool ConsultaConParametrosLogIn(string table, string condicion)  {
            conecta.Open(); 
            string consultarConParametros = "SELECT * FROM  " + table + " WHERE " + condicion;
            comando = new SqlCommand(consultarConParametros, conecta);            
            SqlDataReader dr = comando.ExecuteReader();

            if (dr.HasRows) {
                conecta.Close();
                
                return true;
            }
            else {
                conecta.Close();
                return false;
            }
        }

    }
}
