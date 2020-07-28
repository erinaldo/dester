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
    public class ProveedorBD
    {
        SqlConnection conectaProv = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");
        SqlConnection conectaLeeProv = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private SqlCommandBuilder cmd;
        public DataSet ds = new DataSet();
        public SqlDataAdapter da;
        public SqlCommand commando;
        public SqlDataReader leerProv;

        public void ConectarBD() {
            try {
                conectaProv.Open();
                //   MessageBox.Show("Conectado");
            }
            catch {
                MessageBox.Show("Error al Conectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaProv.Close();
            }
        }

        public void DesconectarBD() {
            try {
                conectaProv.Close();
                //   MessageBox.Show("Conectado");
            }
            catch {
                MessageBox.Show("Error al Desconectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaProv.Close();
            }
        }
        // /////////////////////////////////////////////////////

        //Lecturas DataReader
        public void ConectarBDLeeProv() {
            try {
                conectaLeeProv.Open();
            }
            catch {
                MessageBox.Show("Error al Conectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaLeeProv.Close();
            }
        }

        public void DesconectarBDLeeProv() {
            try {
                conectaLeeProv.Close();
            }
            catch {
                MessageBox.Show("Error al Desconectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaLeeProv.Close();
            }
        }
        // /////////////////////////////////////////////////////

        public void LeeProveedor(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                conectaLeeProv.Open();
                commando = new SqlCommand(sql, conectaLeeProv);
                leerProv = commando.ExecuteReader();
                leerProv.Read();
            }
            catch {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally {
                conectaProv.Close();
            }
        }


        // ABMC PROVEEDOR
        public void ConsultaProveedor(string sql, string tabla) {
            try {
                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conectaProv);
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, tabla);
                da.Dispose(); cmd.Dispose();
            }
            catch {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool InsertarProveedor(string sql) {
            try {
                conectaProv.Open();
                commando = new SqlCommand(sql, conectaProv);
                int i = commando.ExecuteNonQuery();
                conectaProv.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }

        public bool ActualizaProveedor(string tabla, string campos, string condicion) {
            conectaProv.Open();
            string actualizar = "update " + tabla + " set " + campos + " where " + condicion;
            commando = new SqlCommand(actualizar, conectaProv);
            int i = commando.ExecuteNonQuery();
            conectaProv.Close();
            if (i > 0)
                return true;
            else
                return false;
        }

        public bool EliminarProveedor(string tabla, string condicion) {
            try {
                conectaProv.Open();
                string elimina = "delete from " + tabla + " where " + condicion;
                commando = new SqlCommand(elimina, conectaProv);
                int i = commando.ExecuteNonQuery();
                conectaProv.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }

        // ///////////////////////////////

        //CONSULTA TIPOIVA//
        public void ConsultaTipoIvaProv(string sql, string tabla)
        {
            try
            {
                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conectaProv);
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
