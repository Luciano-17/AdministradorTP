using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPractico
{
    internal class Compras
    {
        public int id { get; set; }
        public string producto { get; set; }
        public int cantidad { get; set; }
        public int precio { get; set; }
        public string fecha { get; set; }
        public decimal proveedorCuilDni { get; set; }
    }
}
