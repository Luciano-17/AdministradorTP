using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TrabajoPractico
{
    internal class LoginMetodos : Conexion
    {
        public DataTable consultarLogin(string email, string password)
        {
            string sqlStr = "SELECT email, clave FROM Usuarios WHERE email = '" + email + "' AND clave = '" + 
                password + "'";

            var da = new SqlDataAdapter(sqlStr, conectar());
            var ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            return dt;
        }

        public Boolean guardarUsuario(Login usuario)
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
    }
}
