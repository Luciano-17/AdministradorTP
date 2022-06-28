using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPractico
{
    internal class Ventas
    {
        public int id { get; set; }
        public string producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public string fecha { get; set; }
        public int vendedorDni { get; set; }
        public decimal clienteCuilDni { get; set; }
    }
}
