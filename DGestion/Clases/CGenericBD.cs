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
    public class CGenericBD
    {
        ///SqlConnection conectaGeneric = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");
        //SqlConnection conectaLeeGeneric = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        SqlConnection conectaGeneric = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");
        SqlConnection conectaLeeGeneric = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        private SqlCommandBuilder cmd;
        public DataSet ds = new DataSet();
        public SqlDataAdapter da;
        public SqlCommand commando;
        public SqlDataReader leerGeneric;

        public void ConectarBD() {
            try {
                conectaGeneric.Open();
                //MessageBox.Show("Conectado");
            }
            catch {
                MessageBox.Show("Error al Conectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaGeneric.Close();
            }
        }

        public void DesconectarBD() {
            try {
                conectaGeneric.Close();
                //MessageBox.Show("Conectado");
            }
            catch {
                MessageBox.Show("Error al Desconectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaGeneric.Close();
            }
        }
        // /////////////////////////////////////////////////////

        //Lecturas DataReader
        public void ConectarBDLeeGeneric()
        {
            try {
                conectaLeeGeneric.Open();
            }
            catch {
                MessageBox.Show("Error al Conectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaLeeGeneric.Close();
            }
        }

        public void DesconectarBDLeeGeneric()
        {
            try {
                conectaLeeGeneric.Close();
            }
            catch {
                MessageBox.Show("Error al Desconectar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conectaLeeGeneric.Close();
            }
        }
        // /////////////////////////////////////////////////////

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

        // ABMC PROVEEDOR
        public void ConsultaGeneric(string sql, string tabla) {
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

        public bool InsertarGeneric(string sql)
        {
            try {
                conectaGeneric.Close();
                if (conectaGeneric.State != ConnectionState.Open)
                    conectaGeneric.Open();

                commando = new SqlCommand(sql, conectaGeneric);
                int i = commando.ExecuteNonQuery();
                conectaGeneric.Close();

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch {
                return false;
            }
        }

        public bool ActualizaGeneric(string tabla, string campos, string condicion)
        {
            try
            {
                conectaGeneric.Close();
                if (conectaGeneric.State != ConnectionState.Open)
                    conectaGeneric.Open();

                string actualizar = "update " + tabla + " set " + campos + " where " + condicion;
                commando = new SqlCommand(actualizar, conectaGeneric);
                int i = commando.ExecuteNonQuery();
                conectaGeneric.Close();
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

        public bool EliminarGeneric(string tabla, string condicion)
        {
            try
            {
                conectaGeneric.Close();
                if (conectaGeneric.State != ConnectionState.Open)
                    conectaGeneric.Open();

                string elimina = "delete from " + tabla + " where " + condicion;
                commando = new SqlCommand(elimina, conectaGeneric);
                int i = commando.ExecuteNonQuery();
                conectaGeneric.Close();

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

        public bool EliminarGeneric2(string tabla)
        {
            try
            {
                conectaGeneric.Close();
                if (conectaGeneric.State != ConnectionState.Open)
                    conectaGeneric.Open();

                string elimina = "delete from " + tabla;
                commando = new SqlCommand(elimina, conectaGeneric);
                int i = commando.ExecuteNonQuery();
                conectaGeneric.Close();

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

        // ///////////////////////////////

        //CONSULTA TIPOIVA//
    /*    public void ConsultaTipoIvaProv(string sql, string tabla)
        {
            try
            {
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
        } */

    }
}
