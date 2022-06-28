using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPractico
{
    internal class Usuarios : Conexion
    {
        public int dni { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string clave { get; set; }
        public decimal telefono { get; set; }
        public int nivel { get; set; }
    }
}
