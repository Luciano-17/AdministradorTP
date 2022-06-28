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
    internal class UsuariosMetodos : Conexion
    {
        public DataTable Consultar()
        {
            string sqlStr = "SELECT * FROM Usuarios";

            var da = new SqlDataAdapter(sqlStr, conectar());
            var ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            return dt;
        }

        public DataTable consultarUsuario(int dni)
        {
            string sqlStr = "SELECT * FROM Usuarios WHERE dni = '" + dni + "'";

            var da = new SqlDataAdapter(sqlStr, conectar());
            var ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            return dt;
        }

        public Boolean guardarUsuario(Usuarios usuario)
        {
            try
            {
                var sel = "INSERT INTO Usuarios(dni, nombre, apellido, email, clave, telefono, nivel)" +
                    " VALUES ('" + usuario.dni + "', '" + usuario.nombre + "', '" + usuario.apellido + "', '" + usuario.email +
                    "', '" + usuario.clave + "', '" + usuario.telefono + "', '" + usuario.nivel + "')";

                SqlCommand com = new SqlCommand(sel, conectar());

                com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void modificarUsuario(Usuarios us)
        {
            try
            {
                var sel = "UPDATE Usuarios SET nombre = '" + us.nombre + "', apellido = '" + us.apellido + "', email = '" +
                    us.email + "', clave = '" + us.clave + "', telefono = '" + us.telefono + "', nivel = '" + us.nivel +
                    "' WHERE dni = '" + us.dni + "'";

                SqlCommand com = new SqlCommand(sel, conectar());

                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);

            }

        }

        public Boolean borrarUsuario(int dni)
        {
            try
            {
                var sel = "DELETE FROM Usuarios WHERE dni = '" + dni + "'";

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

        public DataRow CargaCampos(int dni)
        {
            DataTable dt = new DataTable();
            var ds = new DataSet();
            DataRow dr;

            string sqlStr = "SELECT * FROM Usuarios WHERE dni = '" + dni + "'";

            var da = new SqlDataAdapter(sqlStr, conectar());
            ds = new DataSet();
            da.Fill(ds);

            dr = ds.Tables[0].Rows[0];

            return dr;
        }
    }
}
