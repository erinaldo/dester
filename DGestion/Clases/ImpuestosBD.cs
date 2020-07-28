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
    public class ImpuestosBD
    {
        SqlConnection conectaImp = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");
        SqlConnection conectaLeeImp = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private SqlCommandBuilder cmd;
        public DataSet ds = new DataSet();
        public SqlDataAdapter da;
        public SqlCommand commando;
        public SqlDataReader leerImp;

        public void ConectarBD() {
            try {
                conectaImp.Open();
                //   MessageBox.Show("Conectado");
            }
            catch {
                MessageBox.Show("Error al Conectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaImp.Close();
            }
        }

        public void DesconectarBD() {
            try {
                conectaImp.Close();
                //   MessageBox.Show("Conectado");
            }
            catch {
                MessageBox.Show("Error al Desconectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaImp.Close();
            }
        }
        // /////////////////////////////////////////////////////

        //Lecturas DataReader
        public void ConectarBDLeeImp() {
            try {
                conectaLeeImp.Open();
            }
            catch {
                MessageBox.Show("Error al Conectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaLeeImp.Close();
            }
        }

        public void DesconectarBDLeeImp() {
            try {
                conectaLeeImp.Close();
            }
            catch {
                MessageBox.Show("Error al Desconectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaLeeImp.Close();
            }
        }
        // /////////////////////////////////////////////////////

        // ABM IMPUESTOS //         
        public void LeeImpuesto(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conectaImp);
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, tabla);
                da.Dispose(); cmd.Dispose();
            }
            catch {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ConsultaImpuesto(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                conectaLeeImp.Open();
                commando = new SqlCommand(sql, conectaLeeImp);
                leerImp = commando.ExecuteReader();
                leerImp.Read();
            }
            catch {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally {
                conectaImp.Close();
            }
        }
        
        public bool InsertarImpuesto(string sql) {
            try {
                conectaImp.Open();
                commando = new SqlCommand(sql, conectaImp);
                int i = commando.ExecuteNonQuery();
                conectaImp.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }

        public bool ActualizaImpuesto(string tabla, string campos, string condicion) {
            conectaImp.Open();
            string actualizar = "update " + tabla + " set " + campos + " where " + condicion;
            commando = new SqlCommand(actualizar, conectaImp);
            int i = commando.ExecuteNonQuery();
            conectaImp.Close();
            if (i > 0)
                return true;
            else
                return false;
        }

        public bool EliminarImpuesto(string tabla, string condicion) {
            try {
                conectaImp.Open();
                string elimina = "delete from " + tabla + " where " + condicion;
                commando = new SqlCommand(elimina, conectaImp);
                int i = commando.ExecuteNonQuery();
                conectaImp.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////

        //GESTION AMBC IMPUESTOS //
        public void ConsultaTipoImpuestos(string sql, string tabla)
        {
            try
            {
                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conectaImp);
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, tabla);
                da.Dispose(); cmd.Dispose();
            }
            catch
            {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }   
                  
    }
}
