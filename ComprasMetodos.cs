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
    internal class ComprasMetodos : Conexion
    {
        public DataTable Consultar()
        {
            string sqlStr = "SELECT * FROM Compras";

            var da = new SqlDataAdapter(sqlStr, conectar());
            var ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            return dt;
        }

        public DataTable consultarCompra(decimal cuilDni)
        {
            string sqlStr = "SELECT * FROM Compras WHERE proveedorCuilDni = '" + cuilDni + "'";

            var da = new SqlDataAdapter(sqlStr, conectar());
            var ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            return dt;
        }

        public Boolean guardarCompra(Compras compra)
        {
            try
            {
                var sel = "INSERT INTO Compras(id, producto, cantidad, precio, fecha, proveedorCuilDni)" +
                    " VALUES ('" + compra.id + "', '" + compra.producto + "', '" + compra.cantidad + "', '" + compra.precio +
                    "', '" + compra.fecha + "', '" + compra.proveedorCuilDni + "')";

                SqlCommand com = new SqlCommand(sel, conectar());

                com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void modificarCompra(Compras co)
        {
            try
            {
                var sel = "UPDATE Compras SET producto = '" + co.producto + "', cantidad = '" + co.cantidad + "', precio = '" +
                    co.precio + "', fecha = '" + co.fecha + "', proveedorCuilDni = '" + co.proveedorCuilDni + "' WHERE id = '" +
                    co.id + "'";

                SqlCommand com = new SqlCommand(sel, conectar());

                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);

            }

        }

        public Boolean borrarCompra(int id)
        {
            try
            {
                var sel = "DELETE FROM Compras WHERE id = '" + id + "'";

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

        public DataRow CargaCampos(int id)
        {
            DataTable dt = new DataTable();
            var ds = new DataSet();
            DataRow dr;

            string sqlStr = "SELECT * FROM Compras WHERE id = '" + id + "'";

            var da = new SqlDataAdapter(sqlStr, conectar());
            ds = new DataSet();
            da.Fill(ds);

            dr = ds.Tables[0].Rows[0];

            return dr;
        }
    }
}
