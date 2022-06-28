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
    public partial class VentasForm : Form
    {
        public VentasForm()
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
            bool errorVendedor = evaluarVendedorDni();
            bool errorCliente = evaluarClienteCuilDni();

            if (error || errorVendedor || errorCliente)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            }
            else
            {
                DialogResult resp = MessageBox.Show("Confirmar", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                var ve = new Ventas();

                if (resp == DialogResult.Yes)
                {
                    ve.id = Convert.ToInt32(txtId.Text);
                    ve.producto = txtProductoId.Text;
                    ve.precio = Convert.ToDecimal(txtPrecio.Text);
                    ve.cantidad = Convert.ToInt32(txtCantidad.Text);
                    ve.fecha = txtFecha.Text;
                    ve.vendedorDni = Convert.ToInt32(txtVendedorDni.Text);
                    ve.clienteCuilDni = Convert.ToDecimal(txtClienteCuilDni.Text);

                    var veMetodo = new VentasMetodos();
                    veMetodo.guardarVenta(ve);
                }

                cargarDB();
                resetearForm();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            bool error = evaluarErrores();
            bool errorVendedor = evaluarVendedorDni();
            bool errorCliente = evaluarClienteCuilDni();

            if (error || errorVendedor || errorCliente)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            }
            else
            {
                DialogResult resp = MessageBox.Show("Confirmar", "Modificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                var ve = new Ventas();

                if (resp == DialogResult.Yes)
                {
                    ve.id = Convert.ToInt32(txtId.Text);
                    ve.producto = txtProductoId.Text;
                    ve.precio = Convert.ToDecimal(txtPrecio.Text);
                    ve.cantidad = Convert.ToInt32(txtCantidad.Text);
                    ve.fecha = txtFecha.Text;
                    ve.vendedorDni = Convert.ToInt32(txtVendedorDni.Text);
                    ve.clienteCuilDni = Convert.ToDecimal(txtClienteCuilDni.Text);

                    var veMetodo = new VentasMetodos();
                    veMetodo.modificarVenta(ve);
                }

                cargarDB();
                resetearForm();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            bool error = evaluarIdOp();

            if (error)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            }
            else
            {
                var ds = new DataSet();
                var dt = new DataTable();
                var ve = new VentasMetodos();

                dt = ve.consultarVenta(Convert.ToInt32(txtIdOp.Text));

                if (dt.Rows.Count != 0)
                {
                    dgvVentas.DataSource = dt;
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
                    var veMetodo = new VentasMetodos();
                    bool borrar = veMetodo.borrarVenta(Convert.ToInt32(txtIdOp.Text));

                    if (borrar == false)
                    {
                        MessageBox.Show("Error al eliminar", "Verifique");
                    }
                }

                cargarDB();
                resetearForm();
            }
        }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dgvVentas.Rows[e.RowIndex].Cells[0].Value.ToString();

            var ve = new Ventas();
            var veMetodo = new VentasMetodos();

            DataRow dr;
            dr = veMetodo.CargaCampos(Convert.ToInt32(txtId.Text));

            if (dr["id"] != null)
            {
                txtId.Text = Convert.ToString(dr["id"]);
                txtProductoId.Text = Convert.ToString(dr["producto"]);
                txtPrecio.Text = Convert.ToString(Convert.ToInt32(dr["precio"]));
                txtCantidad.Text = Convert.ToString(dr["cantidad"]);
                txtFecha.Text = Convert.ToString(dr["fecha"]);
                txtVendedorDni.Text = Convert.ToString(dr["vendedorDni"]);
                txtClienteCuilDni.Text = Convert.ToString(dr["clienteCuilDni"]);
            }
        }

        private void cargarDB()
        {
            var ds = new DataSet();
            var dt = new DataTable();
            var ve = new VentasMetodos();

            dt = ve.Consultar();

            if (dt.Rows.Count != 0)
            {
                dgvVentas.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No hay registros en la seleccion");
            }
        }

        private void resetearForm()
        {
            txtId.Text = "";
            txtProductoId.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
            txtFecha.Text = "";
            txtVendedorDni.Text = "";
            txtClienteCuilDni.Text = "";

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
            else if (txtProductoId.Text == "")
            {
                MessageBox.Show("Producto incorrecto");
                txtProductoId.Text = "";
                txtProductoId.Focus();
                error = true;
            }
            else if ((txtPrecio.Text.Length > 10 && txtPrecio.Text.Length <= 0) || txtPrecio.Text == "")
            {
                MessageBox.Show("Precio incorrecto");
                txtPrecio.Text = "";
                txtPrecio.Focus();
                error = true;
            }
            else if (txtCantidad.Text == "" || txtCantidad.Text.Length <= 0)
            {
                MessageBox.Show("Cantidad incorrecta");
                txtProductoId.Text = "";
                txtProductoId.Focus();
                error = true;
            }
            else if (txtFecha.Text == "")
            {
                MessageBox.Show("Fecha incorrecta");
                txtFecha.Text = "";
                txtFecha.Focus();
                error = true;
            }
            else if (txtVendedorDni.Text.Length != 8 || txtVendedorDni.Text == "")
            {
                MessageBox.Show("Cuil o Dni del vendedor incorrecto");
                txtVendedorDni.Text = "";
                txtVendedorDni.Focus();
                error = true;
            }
            else if ((txtClienteCuilDni.Text.Length < 8 && txtClienteCuilDni.Text.Length > 11) || txtClienteCuilDni.Text == "")
            {
                MessageBox.Show("Cuil o Dni del cliente incorrecto");
                txtClienteCuilDni.Text = "";
                txtClienteCuilDni.Focus();
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
                MessageBox.Show("Id para buscar o borrar incorrecto");
                txtIdOp.Text = "";
                txtIdOp.Focus();
                error = true;
            }
            else
            {
                error = false;
            }

            return error;
        }

        private bool evaluarVendedorDni()
        {
            bool error;

            var ds = new DataSet();
            var dt = new DataTable();
            var us = new UsuariosMetodos();

            dt = us.consultarUsuario(Convert.ToInt32(txtVendedorDni.Text));

            if (dt.Rows.Count != 0)
            {
                error = false;
            }
            else
            {
                MessageBox.Show("El dni del vendedor no pertenece a ningún registro");
                txtVendedorDni.Text = "";
                txtVendedorDni.Focus();
                error = true;
            }

            return error;
        }

        private bool evaluarClienteCuilDni()
        {
            bool error;

            var ds = new DataSet();
            var dt = new DataTable();
            var cl = new ClientesMetodos();

            dt = cl.consultarCliente(Convert.ToDecimal(txtClienteCuilDni.Text));

            if (dt.Rows.Count != 0)
            {
                error = false;
            }
            else
            {
                MessageBox.Show("El cuil o dni del cliente no pertenece a ningún registro");
                txtClienteCuilDni.Text = "";
                txtClienteCuilDni.Focus();
                error = true;
            }

            return error;
        }
    }
}
