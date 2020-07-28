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
using System.Configuration;
using System.Reflection;
using System.Threading;

namespace DGestion.Clases
{
    public class Permisos
    {
        SqlConnection conectaGeneric = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");
        SqlConnection conectaLeeGeneric = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private SqlCommandBuilder cmd;
        public DataSet ds = new DataSet();
        public SqlDataAdapter da;
        public SqlCommand commando;
        public SqlDataReader leerGeneric;

        public void ConectarBD()
        {
            try
            {
                conectaGeneric.Open();
                //MessageBox.Show("Conectado");
            }
            catch
            {
                MessageBox.Show("Error al Conectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conectaGeneric.Close();
            }
        }

        public void DesconectarBD()
        {
            try
            {
                conectaGeneric.Close();
                //MessageBox.Show("Conectado");
            }
            catch
            {
                MessageBox.Show("Error al Desconectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conectaGeneric.Close();
            }
        }
        // /////////////////////////////////////////////////////




        // Consulta Permiso
        public void ConsultaGenericPermiso(string sql, string tabla)
        {
            try
            {
                conectaGeneric.Close();
                if (conectaGeneric.State != ConnectionState.Open)
                    conectaGeneric.Open();

                ds.Tables.Clear();
                da = new SqlDataAdapter(sql, conectaGeneric);
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, tabla);
                da.Dispose(); cmd.Dispose();
            }
            catch
            {
                MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LeeGeneric(string sql, string tabla)
        {
            try
            {
                conectaLeeGeneric.Close();
                ds.Tables.Clear();

                if (conectaLeeGeneric.State != ConnectionState.Open)
                    conectaLeeGeneric.Open();

                commando = new SqlCommand(sql, conectaLeeGeneric);
                leerGeneric = commando.ExecuteReader();
                leerGeneric.Read();
            }
            catch
            {
                //MessageBox.Show("Error en la Consulta, veríficala e inténtalo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                conectaGeneric.Close();
            }
        }




    }
}
