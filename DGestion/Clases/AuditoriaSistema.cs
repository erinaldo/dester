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
   public class AuditoriaSistema
    {
        CGenericBD conn = new CGenericBD();

        private string FormateoFecha() {                     
            DateTimePicker dtr = new DateTimePicker();
            dtr.Value = DateTime.Now;
            return String.Format("{0:d/M/yyyy HH:mm:ss}", dtr.Value);
        }

       //AUDITORIA DE SEGURIDAD//
        public void GAErrorLogin_0001(string DescripSuceso, string Usuario) {
            try {
                MessageBox.Show("Problema de Conexion: No se ha podido establecer conexion con la Base de Datos.", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }            
        }

        public void GAIngresoLogin_0002(string DescripSuceso, string Usuario) {
            try {              
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" +  FormateoFecha() + "', '" + Usuario + "', '4')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();

                string actualizarEstadoUsuario = "ISLOGIN='True'";
                conn.ActualizaGeneric("Personal", actualizarEstadoUsuario, " USUARIO = '" + Usuario + "'");
                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void GAIngresoLogin_0003(string DescripSuceso, string Usuario) {
            try {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '8')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void GACierreLogin_0004(string DescripSuceso, string Usuario) {
            try {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '5')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();

                string actualizarEstadoUsuario = "ISLOGIN='False'";
                conn.ActualizaGeneric("Personal", actualizarEstadoUsuario, " USUARIO = '" + Usuario + "'");
                conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



       //AUDITORIA DE SISTEMA//
       //FACTURACION
        public void SistemaProcesoAuditor_0001(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0002(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0003(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0004(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0005(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0006(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0007(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0008(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0009(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0010(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0011(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0012(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0013(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0014(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }

        public void SistemaProcesoAuditor_0015(string DescripSuceso, string Usuario)
        {
            try
            {
                string agregar = "INSERT INTO Suceso (EVENTO, FECHAHORA, USUARIO, IDESTADO) VALUES ('" + DescripSuceso + "', '" + FormateoFecha() + "', '" + Usuario + "', '16')";
                if (conn.InsertarGeneric(agregar))
                    conn.DesconectarBD();
            }
            catch { conn.DesconectarBD(); }
        }
        /////////////////////////////////////////////////////////////////////////////////////
       
       ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}