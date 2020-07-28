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
    class Personal
    {
        public class ImpuestosBD
        {
            SqlConnection conectaPerso = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");
            SqlConnection conectaLeePerso = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

            private SqlCommandBuilder cmd;
            public DataSet ds = new DataSet();
            public SqlDataAdapter da;
            public SqlCommand commando;
            public SqlDataReader leerImp;

            public void ConectarBD() {
                try {
                    conectaPerso.Open();
                }
                catch {
                    MessageBox.Show("Error al Conectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally {
                    conectaPerso.Close();
                }
            }

            public void DesconectarBD() {
                try {
                    conectaPerso.Close();
                }
                catch {
                    MessageBox.Show("Error al Desconectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally {
                    conectaPerso.Close();
                }
            }
            // /////////////////////////////////////////////////////

            //Lecturas DataReader
            public void ConectarBDLeeImp() {
                try {
                    conectaLeePerso.Open();
                }
                catch {
                    MessageBox.Show("Error al Conectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally {
                    conectaLeePerso.Close();
                }
            }

            public void DesconectarBDLeeImp() {
                try {
                    conectaLeePerso.Close();
                }
                catch {
                    MessageBox.Show("Error al Desconectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally {
                    conectaLeePerso.Close();
                }
            }
            // /////////////////////////////////////////////////////

            // ABM PERSONAL //         
            public void LeePersonal(string sql, string tabla) {
                try {
                    ds.Tables.Clear();
                    da = new SqlDataAdapter(sql, conectaPerso);
                    cmd = new SqlCommandBuilder(da);
                    da.Fill(ds, tabla);
                    da.Dispose(); cmd.Dispose();
                }
                catch {
                    MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            public void ConsultaPersonal(string sql, string tabla) {
                try {
                    ds.Tables.Clear();
                    conectaLeePerso.Open();
                    commando = new SqlCommand(sql, conectaLeePerso);
                    leerImp = commando.ExecuteReader();
                    leerImp.Read();
                }
                catch {
                    MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                finally {
                    conectaPerso.Close();
                }
            }

            public bool InsertarPersonal(string sql) {
                try {
                    conectaPerso.Open();
                    commando = new SqlCommand(sql, conectaPerso);
                    int i = commando.ExecuteNonQuery();
                    conectaPerso.Close();

                    if (i > 0)
                        return true;
                    else
                        return false;
                }
                catch {
                    return false;
                }
            }

            public bool ActualizaPersonal(string tabla, string campos, string condicion) {
                conectaPerso.Open();
                string actualizar = "update " + tabla + " set " + campos + " where " + condicion;
                commando = new SqlCommand(actualizar, conectaPerso);
                int i = commando.ExecuteNonQuery();
                conectaPerso.Close();
                if (i > 0)
                    return true;
                else
                    return false;
            }

            public bool EliminarPersonal(string tabla, string condicion) {
                try {
                    conectaPerso.Open();
                    string elimina = "delete from " + tabla + " where " + condicion;
                    commando = new SqlCommand(elimina, conectaPerso);
                    int i = commando.ExecuteNonQuery();
                    conectaPerso.Close();

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

            //GESTION AMBC PERSONAL //
            public void ConsultaTipoPersonal(string sql, string tabla) {
                try {
                    ds.Tables.Clear();
                    da = new SqlDataAdapter(sql, conectaPerso);
                    cmd = new SqlCommandBuilder(da);
                    da.Fill(ds, tabla);
                    da.Dispose(); cmd.Dispose();
                }
                catch {
                    MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
