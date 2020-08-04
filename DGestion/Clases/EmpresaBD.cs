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
    public class EmpresaBD
    {
        SqlConnection conectaEmpresa = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2018;Persist Security Info=True;User ID=sa;Password=1am45d50G#");
        SqlConnection conectaLeeEmpresa = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2018;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private SqlCommandBuilder cmd;
        public DataSet ds = new DataSet();
        public SqlDataAdapter da;
        public SqlCommand commando;
        public SqlDataReader leerEmpresa;

        //Lecturas DataReader
        public void ConectarBDLeeEmpresa()
        {
            try
            {
                conectaLeeEmpresa.Open();
            }
            catch
            {
                MessageBox.Show("Error al Conectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conectaLeeEmpresa.Close();
            }
        }

        public void DesconectarBDLeeEmpresa()
        {
            try
            {
                conectaLeeEmpresa.Close();
            }
            catch
            {
                MessageBox.Show("Error al Desconectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conectaLeeEmpresa.Close();
            }
        }
        // /////////////////////////////////////////////////////


        public void LeeEmpresa(string sql, string tabla)
        {
            try
            {
                ds.Tables.Clear();
                conectaLeeEmpresa.Open();
                commando = new SqlCommand(sql, conectaLeeEmpresa);
                leerEmpresa = commando.ExecuteReader();
                leerEmpresa.Read();
            }
            catch
            {
                //MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                conectaEmpresa.Close();
            }
        }


        // ABMC PROVEEDOR
        public void ConsultaEmpresa(string sql, string tabla)
        {
            try
            {
                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conectaEmpresa);
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, tabla);
                da.Dispose(); cmd.Dispose();
            }
            catch
            {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool InsertarEmpresa(string sql)
        {
            try
            {
                conectaEmpresa.Open();
                commando = new SqlCommand(sql, conectaEmpresa);
                int i = commando.ExecuteNonQuery();
                conectaEmpresa.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool ActualizaEmpresa(string tabla, string campos, string condicion)
        {
            try
            {

                conectaEmpresa.Open();
                string actualizar = "update " + tabla + " set " + campos + " where " + condicion;
                commando = new SqlCommand(actualizar, conectaEmpresa);
                int i = commando.ExecuteNonQuery();
                conectaEmpresa.Close();
                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarEmpresa(string tabla, string condicion)
        {
            try
            {
                conectaEmpresa.Open();
                string elimina = "delete from " + tabla + " where " + condicion;
                commando = new SqlCommand(elimina, conectaEmpresa);
                int i = commando.ExecuteNonQuery();
                conectaEmpresa.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public void ObtenerEmpresaActiva(string sql, string tabla)
        {
            try
            {
                ds.Tables.Clear();
                conectaLeeEmpresa.Open();
                commando = new SqlCommand(sql, conectaLeeEmpresa);
                leerEmpresa = commando.ExecuteReader();
                leerEmpresa.Read();
            }
            catch
            {
                //MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                conectaEmpresa.Close();
            }

        }



    }
}
