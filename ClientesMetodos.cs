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
    internal class ClientesMetodos : Conexion
    {
        public DataTable Consultar()
        {
            string sqlStr = "SELECT * FROM Clientes";

            var da = new SqlDataAdapter(sqlStr, conectar());
            var ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            return dt;
        }

        public DataTable consultarCliente(decimal cuilDni)
        {
            string sqlStr = "SELECT * FROM Clientes WHERE cuilDni = '" + cuilDni + "'";

            var da = new SqlDataAdapter(sqlStr, conectar());
            var ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            return dt;
        }

        public Boolean guardarCliente(Clientes cliente)
        {
            try
            {
                var sel = "INSERT INTO Clientes(cuilDni, nombre,  email, telefono, calle, numeracion)" +
                    " VALUES ('" + cliente.cuilDni + "', '"+ cliente.nombre  + "', '" + cliente.email + "', '" + 
                    cliente.telefono + "', '" + cliente.calle + "', '" + cliente.numeracion + "')";

                SqlCommand com = new SqlCommand(sel, conectar());

                com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void modificarCliente(Clientes cl)
        {
            try
            {
                var sel = "UPDATE Clientes SET nombre = '" + cl.nombre + "', email = '" + cl.email + "', telefono = '" + 
                     cl.telefono + "', calle = '" + cl.calle + "', numeracion = '" + cl.numeracion + "' WHERE cuilDni = '" + 
                     cl.cuilDni + "'";

                SqlCommand com = new SqlCommand(sel, conectar());

                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);

            }

        }

        public Boolean borrarCliente(decimal cuilDni)
        {
            try
            {
                var sel = "DELETE FROM Clientes WHERE cuilDni = '" + cuilDni + "'";

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

            string sqlStr = "SELECT * FROM Clientes WHERE cuilDni = '" + cuilDni + "'";

            var da = new SqlDataAdapter(sqlStr, conectar());
            ds = new DataSet();
            da.Fill(ds);

            dr = ds.Tables[0].Rows[0];

            return dr;
        }
    }
}
