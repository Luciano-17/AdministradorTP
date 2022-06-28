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
    public partial class ClientesForm : Form
    {
        public ClientesForm()
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

            if(error == true)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            } else
            {
                DialogResult resp = MessageBox.Show("Confirmar", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                var cl = new Clientes();

                if (resp == DialogResult.Yes)
                {
                    cl.cuilDni = Convert.ToDecimal(txtCuilDni.Text);
                    cl.nombre = txtNombre.Text;
                    cl.email = txtEmail.Text;
                    cl.telefono = Convert.ToDecimal(txtTelefono.Text);
                    cl.calle = txtCalle.Text;
                    cl.numeracion = Convert.ToInt32(txtNumeracion.Text);

                    var clMetodo = new ClientesMetodos();
                    clMetodo.guardarCliente(cl);
                }

                cargarDB();
                resetearForm();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            bool error = evaluarErrores();

            if(error)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            } else
            {
                DialogResult resp = MessageBox.Show("Confirmar", "Modificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                var cl = new Clientes();

                if (resp == DialogResult.Yes)
                {
                    cl.cuilDni = Convert.ToDecimal(txtCuilDni.Text);
                    cl.nombre = txtNombre.Text;
                    cl.email = txtEmail.Text;
                    cl.telefono = Convert.ToDecimal(txtTelefono.Text);
                    cl.calle = txtCalle.Text;
                    cl.numeracion = Convert.ToInt32(txtNumeracion.Text);

                    var clMetodo = new ClientesMetodos();
                    clMetodo.modificarCliente(cl);
                }

                cargarDB();
                resetearForm();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            bool error = evaluarCuilDniOp();

            if (error)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            } else
            {
                var ds = new DataSet();
                var dt = new DataTable();
                var cl = new ClientesMetodos();

                dt = cl.consultarCliente(Convert.ToDecimal(txtCuilDniOp.Text));

                if (dt.Rows.Count != 0)
                {
                    dgvClientes.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No hay registros en la seleccion");
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            bool error = evaluarCuilDniOp();

            if (error)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            } else
            {
                DialogResult resp = MessageBox.Show("Confirmar la Eliminación", "Eliminar", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    var clMetodo = new ClientesMetodos();
                    bool borrar = clMetodo.borrarCliente(Convert.ToDecimal(txtCuilDniOp.Text));

                    if (borrar == false)
                    {
                        MessageBox.Show("Error al eliminar", "Verifique");
                    }
                }

                cargarDB();
                resetearForm();
            }
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCuilDni.Text = dgvClientes.Rows[e.RowIndex].Cells[0].Value.ToString();

            var cl = new Clientes();
            var clMetodo = new ClientesMetodos();

            DataRow dr;
            dr = clMetodo.CargaCampos(Convert.ToDecimal(txtCuilDni.Text));

            if (dr["cuilDni"] != null)
            {
                txtCuilDni.Text = Convert.ToString(dr["cuilDni"]);
                txtNombre.Text = Convert.ToString(dr["nombre"]);
                txtCalle.Text = Convert.ToString(dr["calle"]);
                txtNumeracion.Text = Convert.ToString(dr["numeracion"]);
                txtTelefono.Text = Convert.ToString(dr["telefono"]);
                txtEmail.Text = Convert.ToString(dr["email"]);
            }
        }

        private void cargarDB()
        {
            var ds = new DataSet();
            var dt = new DataTable();
            var cl = new ClientesMetodos();

            dt = cl.Consultar();

            if (dt.Rows.Count != 0)
            {
                dgvClientes.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No hay registros en la seleccion");
            }
        }

        private void resetearForm()
        {
            txtCuilDni.Text = "";
            txtNombre.Text = "";
            txtCalle.Text = "";
            txtNumeracion.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";

            txtCuilDni.Focus();
        }

        private bool evaluarErrores()
        {
            bool error;

            if ((txtCuilDni.Text.Length < 8 && txtCuilDni.Text.Length > 11) || txtCuilDni.Text == "")
            {
                MessageBox.Show("Cuil o Dni incorrecto");
                txtCuilDni.Text = "";
                txtCuilDni.Focus();
                error = true;
            } else if (txtNombre.Text == "")
            {
                MessageBox.Show("Nombre incorrecto");
                txtNombre.Text = "";
                txtNombre.Focus();
                error = true;
            } else if (txtEmail.Text == "")
            {
                MessageBox.Show("Email incorrecta");
                txtEmail.Text = "";
                txtEmail.Focus();
                error = true;
            } else if (txtTelefono.Text.Length != 10 || txtTelefono.Text == "")
            {
                MessageBox.Show("Teléfono incorrecto");
                txtTelefono.Text = "";
                txtTelefono.Focus();
                error = true;
            } else if (txtCalle.Text == "")
            {
                MessageBox.Show("Calle incorrecto");
                txtCalle.Text = "";
                txtCalle.Focus();
                error = true;
            } else if (txtNumeracion.Text == "")
            {
                MessageBox.Show("Numeración incorrecto");
                txtNumeracion.Text = "";
                txtNumeracion.Focus();
                error = true;
            } else
            {
                error = false;
            }

            return error;
        }

        private bool evaluarCuilDniOp()
        {
            bool error;

            if ((txtCuilDniOp.Text.Length < 8 && txtCuilDniOp.Text.Length > 11) || txtCuilDniOp.Text == "")
            {
                MessageBox.Show("Cuil o Dni para buscar o borrar incorrecto");
                txtCuilDniOp.Text = "";
                txtCuilDniOp.Focus();
                error = true;
            } else
            {
                error = false;
            }

            return error;
        }
    }
}
