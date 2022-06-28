using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TrabajoPractico
{
    internal class Conexion
    {
        private SqlConnection sCon = new SqlConnection();
        private string con = "Data Source=DESKTOP-600BLS6;Initial Catalog=TP2;Integrated Security=True";

        public SqlConnection conectar()
        {
            try
            {
                sCon = new SqlConnection(con);

                if (sCon.State.Equals(ConnectionState.Open))
                {
                    sCon.Close();
                }
                else
                {
                    sCon.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return sCon;
        }
    }
}
