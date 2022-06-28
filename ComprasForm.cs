using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabajoPractico
{
    public partial class ComprasForm : Form
    {
        public ComprasForm()
        {
            InitializeComponent();
            cargarDB();
            resetearForm();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bool error = evaluarErrores();
            bool errorProveedor = evaluarProveedorCuilDni();

            if (error || errorProveedor)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            }
            else
            {
                DialogResult resp = MessageBox.Show("Confirmar", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                var co = new Compras();

                if (resp == DialogResult.Yes)
                {
                    co.id = Convert.ToInt32(txtId.Text);
                    co.producto = txtProducto.Text;
                    co.precio = Convert.ToInt32(txtPrecio.Text);
                    co.cantidad = Convert.ToInt32(txtCantidad.Text);
                    co.fecha = txtFecha.Text;
                    co.proveedorCuilDni = Convert.ToDecimal(txtProveedorCuilDni.Text);

                    var coMetodo = new ComprasMetodos();
                    coMetodo.guardarCompra(co);
                }

                cargarDB();
                resetearForm();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            bool error = evaluarErrores();
            bool errorProveedor = evaluarProveedorCuilDni();

            if (error || errorProveedor)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            }
            else
            {
                DialogResult resp = MessageBox.Show("Confirmar", "Modificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                var co = new Compras();

                if (resp == DialogResult.Yes)
                {
                    co.id = Convert.ToInt32(txtId.Text);
                    co.producto = txtProducto.Text;
                    co.precio = Convert.ToInt32(txtPrecio.Text);
                    co.cantidad = Convert.ToInt32(txtCantidad.Text);
                    co.fecha = txtFecha.Text;
                    co.proveedorCuilDni = Convert.ToDecimal(txtProveedorCuilDni.Text);

                    var coMetodo = new ComprasMetodos();
                    coMetodo.modificarCompra(co);
                }

                cargarDB();
                resetearForm();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            bool error = evaluarProveedorOp();

            if (error)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            }
            else
            {
                var ds = new DataSet();
                var dt = new DataTable();
                var co = new ComprasMetodos();

                dt = co.consultarCompra(Convert.ToDecimal(txtProveedorOp.Text));

                if (dt.Rows.Count != 0)
                {
                    dgvCompras.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No hay registros en la seleccion");
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            bool error = evaluarIdOp();

            if (error)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            }
            else
            {
                DialogResult resp = MessageBox.Show("Confirmar la Eliminación", "Eliminar", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    var coMetodo = new ComprasMetodos();
                    bool borrar = coMetodo.borrarCompra(Convert.ToInt32(txtIdOp.Text));

                    if (borrar == false)
                    {
                        MessageBox.Show("Error al eliminar", "Verifique");
                    }
                }

                cargarDB();
                resetearForm();
            }
        }

        private void dgvCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dgvCompras.Rows[e.RowIndex].Cells[0].Value.ToString();

            var co = new Compras();
            var coMetodo = new ComprasMetodos();

            DataRow dr;
            dr = coMetodo.CargaCampos(Convert.ToInt32(txtId.Text));

            if (dr["id"] != null)
            {
                txtId.Text = Convert.ToString(dr["id"]);
                txtProducto.Text = Convert.ToString(dr["producto"]);
                txtPrecio.Text = Convert.ToString(Convert.ToInt32(dr["precio"]));
                txtCantidad.Text = Convert.ToString(dr["cantidad"]);
                txtFecha.Text = Convert.ToString(dr["fecha"]);
                txtProveedorCuilDni.Text = Convert.ToString(dr["proveedorCuilDni"]);
            }
        }

        private void cargarDB()
        {
            var ds = new DataSet();
            var dt = new DataTable();
            var co = new ComprasMetodos();

            dt = co.Consultar();

            if (dt.Rows.Count != 0)
            {
                dgvCompras.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No hay registros en la seleccion");
            }
        }

        private void resetearForm()
        {
            txtId.Text = "";
            txtProducto.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
            txtFecha.Text = "";
            txtProveedorCuilDni.Text = "";

            txtId.Focus();
        }

        private bool evaluarErrores()
        {
            bool error;

            if (txtId.Text == "")
            {
                MessageBox.Show("Id incorrecto");
                txtId.Text = "";
                txtId.Focus();
                error = true;
            }
            else if (txtProducto.Text == "")
            {
                MessageBox.Show("Producto incorrecto");
                txtProducto.Text = "";
                txtProducto.Focus();
                error = true;
            }
            else if (txtCantidad.Text == "" || txtCantidad.Text.Length <= 0)
            {
                MessageBox.Show("Cantidad incorrecta");
                txtProducto.Text = "";
                txtProducto.Focus();
                error = true;
            }
            else if (txtPrecio.Text.Length > 10 || txtPrecio.Text.Length <= 0 || txtPrecio.Text == "")
            {
                MessageBox.Show("Precio incorrecto");
                txtPrecio.Text = "";
                txtPrecio.Focus();
                error = true;
            }
            else if (txtFecha.Text == "")
            {
                MessageBox.Show("Fecha incorrecto");
                txtFecha.Text = "";
                txtFecha.Focus();
                error = true;
            }
            else if ((txtProveedorCuilDni.Text.Length < 8 && txtProveedorCuilDni.Text.Length > 11) || txtProveedorCuilDni.Text == "")
            {
                MessageBox.Show("Cuil del proveedor incorrecto");
                txtProveedorCuilDni.Text = "";
                txtProveedorCuilDni.Focus();
                error = true;
            }
            else
            {
                error = false;
            }

            return error;
        }

        private bool evaluarProveedorOp()
        {
            bool error;

            if ((txtProveedorOp.Text.Length < 8 && txtProveedorOp.Text.Length > 11) || txtProveedorOp.Text == "")
            {
                MessageBox.Show("Cuil o dni del proveedor para buscar incorrecto");
                txtProveedorOp.Text = "";
                txtProveedorOp.Focus();
                error = true;
            }
            else
            {
                error = false;
            }

            return error;
        }

        private bool evaluarIdOp()
        {
            bool error;

            if (txtIdOp.Text == "")
            {
                MessageBox.Show("Id de la compra para borrar incorrecto");
                txtProveedorOp.Text = "";
                txtProveedorOp.Focus();
                error = true;
            }
            else
            {
                error = false;
            }

            return error;
        }

        private bool evaluarProveedorCuilDni()
        {
            bool error;

            var ds = new DataSet();
            var dt = new DataTable();
            var pr = new ProveedoresMetodos();

            dt = pr.consultarProveedor(Convert.ToDecimal(txtProveedorCuilDni.Text));

            if (dt.Rows.Count != 0)
            {
                error = false;
            }
            else
            {
                MessageBox.Show("El cuil o dni del proveedor no pertenece a ningún registro");
                txtProveedorCuilDni.Text = "";
                txtProveedorCuilDni.Focus();
                error = true;
            }

            return error;
        }
    }
}
