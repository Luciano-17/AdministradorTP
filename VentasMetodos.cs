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
    internal class VentasMetodos : Conexion
    {
        public DataTable Consultar()
        {
            string sqlStr = "SELECT * FROM Ventas";

            var da = new SqlDataAdapter(sqlStr, conectar());
            var ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            return dt;
        }

        public DataTable consultarVenta(int id)
        {
            string sqlStr = "SELECT * FROM Ventas WHERE id = '" + id + "'";

            var da = new SqlDataAdapter(sqlStr, conectar());
            var ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            return dt;
        }

        public Boolean guardarVenta(Ventas venta)
        {
            try
            {
                var sel = "INSERT INTO Ventas(id, producto, cantidad, precio, fecha, vendedorDni, clienteCuilDni)" +
                    " VALUES ('" + venta.id + "', '" + venta.producto + "', '" + venta.cantidad + "', '" + venta.precio +
                    "', '" + venta.fecha + "', '" + venta.vendedorDni + "', '" + venta.clienteCuilDni + "')";

                SqlCommand com = new SqlCommand(sel, conectar());

                com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void modificarVenta(Ventas ve)
        {
            try
            {
                var sel = "UPDATE Ventas SET producto = '" + ve.producto + "', cantidad = '" + ve.cantidad + "', precio = '" +
                    ve.precio + "', fecha = '" + ve.fecha + "', vendedorDni = '" + ve.vendedorDni + "', clienteCuilDni = '" +
                    ve.clienteCuilDni + "' WHERE id = '" + ve.id + "'";

                SqlCommand com = new SqlCommand(sel, conectar());

                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);

            }

        }

        public Boolean borrarVenta(int id)
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

            string sqlStr = "SELECT * FROM Ventas WHERE id = '" + id + "'";

            var da = new SqlDataAdapter(sqlStr, conectar());
            ds = new DataSet();
            da.Fill(ds);

            dr = ds.Tables[0].Rows[0];

            return dr;
        }
    }
}
