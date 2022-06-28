using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TrabajoPractico
{
    internal class ProveedoresMetodos : Conexion
    {
        public DataTable Consultar()
        {
            string sqlStr = "SELECT * FROM Proveedores";

            var da = new SqlDataAdapter(sqlStr, conectar());
            var ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            return dt;
        }

        public DataTable consultarProveedor(decimal cuilDni)
        {
            string sqlStr = "SELECT * FROM Proveedores WHERE cuilDni = '" + cuilDni + "'";

            var da = new SqlDataAdapter(sqlStr, conectar());
            var ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            return dt;
        }

        public Boolean guardarProveedor(Proveedores proveedor)
        {
            try
            {
                var sel = "INSERT INTO Proveedores(cuilDni, nombre, email, ciudad, telefono, calle, numeracion)" +
                    " VALUES ('" + proveedor.cuilDni + "', '" + proveedor.nombre + "', '" + proveedor.email + "', '" +
                    proveedor.ciudad + "', '" + proveedor.telefono + "', '" + proveedor.calle + "', '" + proveedor.numeracion + "')";

                SqlCommand com = new SqlCommand(sel, conectar());

                com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void modificarProveedor(Proveedores pr)
        {
            try
            {
                var sel = "UPDATE Proveedores SET nombre = '" + pr.nombre + "', email = '" + pr.email + "', ciudad = '" +
                    pr.ciudad + "', telefono = '" + pr.telefono + "', calle = '" + pr.calle + "', numeracion = '" + pr.numeracion +
                    "' WHERE cuilDni = '" + pr.cuilDni + "'";

                SqlCommand com = new SqlCommand(sel, conectar());

                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);

            }
        }

        public Boolean borrarProveedor(decimal cuilDni)
        {
            try
            {
                var sel = "DELETE FROM Proveedores WHERE cuilDni = '" + cuilDni + "'";

                SqlCommand com = new SqlCommand(sel, conectar());

                var i = com.ExecuteNonQuery();
                if (i == 0) return false;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                return false;
            }
        }

        public DataRow CargaCampos(decimal cuilDni)
        {
            DataTable dt = new DataTable();
            var ds = new DataSet();
            DataRow dr;

            string sqlStr = "SELECT * FROM Proveedores WHERE cuilDni = '" + cuilDni + "'";

            var da = new SqlDataAdapter(sqlStr, conectar());
            ds = new DataSet();
            da.Fill(ds);

            dr = ds.Tables[0].Rows[0];

            return dr;
        }
    }
}
