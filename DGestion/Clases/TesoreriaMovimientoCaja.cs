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

    public class TesoreriaMovimientoCaja
    {

        CGenericBD conn = new CGenericBD();
        EmpresaBD connEmpresa = new EmpresaBD();
        SqlConnection conectaEstado = new SqlConnection("Data Source=desterargentina.com.ar;Initial Catalog=DesterGestion2017;Persist Security Info=True;User ID=sa;Password=1am45d50G#");

        string sPtoVta;
        int iIdEmpresa;

        private string FormateoFecha()
        {
            DateTimePicker dtr = new DateTimePicker();
            dtr.Value = DateTime.Now;
            return String.Format("{0:d/M/yyyy HH:mm:ss}", dtr.Value);
        }

        private int ConsultaEmpresa()
        {
            try
            {
                int IdEmpresa;
                connEmpresa.ObtenerEmpresaActiva("SELECT * FROM Empresa WHERE RazonSocial = '" + frmPrincipal.Empresa + "'", "Empresa");
                IdEmpresa = Convert.ToInt32(connEmpresa.leerEmpresa["IdEmpresa"].ToString());

                connEmpresa.DesconectarBDLeeEmpresa();
                sPtoVta = frmPrincipal.PtoVenta.Trim();
                iIdEmpresa = IdEmpresa;

                return IdEmpresa;
            }
            catch { return 0; }
        }


        public void IngresoCaja(string sSucursal, int iIdTipoMov, string sTipoMovim, decimal dImporteIngreso, decimal dImporteEgreso, string sUsuario, int iIdcaja, int nroFactuInterno, int nroReciboInterno, string nroFactu, string nroRecibo)
        {
            try
            {
                int idempresa;

                idempresa = ConsultaEmpresa();

                string agregar = "INSERT INTO TesoreriaMovimiento (Sucursal, IdTipoMov, TipoMov, FechaMov, IngresoMov, EgresoMov, Usuario, NroCaja, IdEmpresa, NroFacturaInterno, NroReciboInterno, NroFactu, NroRecibo) VALUES ('" + sSucursal + "', "+ iIdTipoMov + ", '"+ sTipoMovim + "', '" + FormateoFecha() + "', (Cast(replace('" + dImporteIngreso + "', ',', '.') as decimal(10,3))), (Cast(replace('" + dImporteEgreso + "', ',', '.') as decimal(10,3))), '" + sUsuario + "',  " + iIdcaja + ", "+ idempresa + ", " + nroFactuInterno + ", " + nroReciboInterno +", '"+ nroFactu +"', '"+ nroRecibo + "')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();             
            }
            catch { conn.DesconectarBD(); }
        }


        public int ValidaCreacionCaja(string NombreCaja)
        {
            try
            {
                ConsultaEmpresa();

                int iEstadoCaja=0;
                
                SqlCommand cm = new SqlCommand("SELECT * FROM TesoreriaCaja WHERE NombreCaja = '" + NombreCaja + "' AND TesoreriaCaja.IdEmpresa = '" + iIdEmpresa + "' AND TesoreriaCaja.Sucursal='" + sPtoVta + "'", conectaEstado);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    iEstadoCaja = Convert.ToInt32(dr["IdEstadoCaja"].ToString().Trim());
                    if (iEstadoCaja == 20)
                        return iEstadoCaja;                    
                }
                return iEstadoCaja;                
            }
            catch { return 0; }
        }

    }
}
