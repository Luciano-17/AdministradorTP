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
    public partial class UsuariosForm : Form
    {
        public UsuariosForm()
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

            if (error)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            } else
            {
                DialogResult resp = MessageBox.Show("Confirmar", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                var us = new Usuarios();

                if (resp == DialogResult.Yes)
                {
                    us.dni = Convert.ToInt32(txtDni.Text);
                    us.nombre = txtNombre.Text;
                    us.apellido = txtApellido.Text;
                    us.email = txtEmail.Text;
                    us.clave = txtClave.Text;
                    us.telefono = Convert.ToDecimal(txtTelefono.Text);
                    us.nivel = Convert.ToInt32(txtNivel.Text);

                    var usMetodo = new UsuariosMetodos();
                    usMetodo.guardarUsuario(us);
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

                var us = new Usuarios();

                if (resp == DialogResult.Yes)
                {
                    us.dni = Convert.ToInt32(txtDni.Text);
                    us.nombre = txtNombre.Text;
                    us.apellido = txtApellido.Text;
                    us.email = txtEmail.Text;
                    us.clave = txtClave.Text;
                    us.telefono = Convert.ToDecimal(txtTelefono.Text);
                    us.nivel = Convert.ToInt32(txtNivel.Text);

                    var usMetodo = new UsuariosMetodos();
                    usMetodo.modificarUsuario(us);
                }

                cargarDB();
                resetearForm();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            bool error = evaluarDniOp();

            if (error)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            } else
            {
                var ds = new DataSet();
                var dt = new DataTable();
                var us = new UsuariosMetodos();

                dt = us.consultarUsuario(Convert.ToInt32(txtDniOp.Text));

                if (dt.Rows.Count != 0)
                {
                    dgvUsuarios.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No hay registros en la seleccion");
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            bool error = evaluarDniOp();

            if (error)
            {
                MessageBox.Show("Por favor, vuelva a ingresar");
            } else
            {
                DialogResult resp = MessageBox.Show("Confirmar la Eliminación", "Eliminar", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    var usMetodo = new UsuariosMetodos();
                    bool borrar = usMetodo.borrarUsuario(Convert.ToInt32(txtDniOp.Text));

                    if (borrar == false)
                    {
                        MessageBox.Show("Error al eliminar", "Verifique");
                    }
                }

                cargarDB();
                resetearForm();
            }
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDni.Text = dgvUsuarios.Rows[e.RowIndex].Cells[0].Value.ToString();

            var us = new Usuarios();
            var usMetodo = new UsuariosMetodos();

            DataRow dr;
            dr = usMetodo.CargaCampos(Convert.ToInt32(txtDni.Text));

            if (dr["dni"] != null)
            {
                txtDni.Text = Convert.ToString(dr["dni"]);
                txtNombre.Text = Convert.ToString(dr["nombre"]);
                txtApellido.Text = Convert.ToString(dr["apellido"]);
                txtEmail.Text = Convert.ToString(dr["email"]);
                txtClave.Text = Convert.ToString(dr["clave"]);
                txtTelefono.Text = Convert.ToString(dr["telefono"]);
                txtNivel.Text = Convert.ToString(dr["nivel"]);
            }
        }

        private void cargarDB()
        {
            var ds = new DataSet();
            var dt = new DataTable();
            var us = new UsuariosMetodos();

            dt = us.Consultar();

            if (dt.Rows.Count != 0)
            {
                dgvUsuarios.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No hay registros en la seleccion");
            }
        }

        private void resetearForm()
        {
            txtDni.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtClave.Text = "";
            txtTelefono.Text = "";
            txtNivel.Text = "";

            txtDni.Focus();
        }

        private bool evaluarErrores()
        {
            bool error;

            if (txtDni.Text.Length != 8 || txtDni.Text == "")
            {
                MessageBox.Show("Dni incorrecto");
                txtDni.Text = "";
                txtDni.Focus();
                error = true;
            }
            else if (txtNombre.Text == "")
            {
                MessageBox.Show("Nombre incorrecto");
                txtNombre.Text = "";
                txtNombre.Focus();
                error = true;
            }
            else if (txtApellido.Text == "")
            {
                MessageBox.Show("Apellido incorrecto");
                txtApellido.Text = "";
                txtApellido.Focus();
                error = true;
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Email incorrecto");
                txtEmail.Text = "";
                txtEmail.Focus();
                error = true;
            }
            else if (txtClave.Text == "")
            {
                MessageBox.Show("Clave incorrecta");
                txtClave.Text = "";
                txtClave.Focus();
                error = true;
            }
            else if (txtTelefono.Text.Length != 10 || txtTelefono.Text == "")
            {
                MessageBox.Show("Teléfono incorrecto");
                txtTelefono.Text = "";
                txtTelefono.Focus();
                error = true;
            }
            else if (txtNivel.Text == "")
            {
                MessageBox.Show("Nivel incorrecto");
                txtNivel.Text = "";
                txtNivel.Focus();
                error = true;
            }
            else
            {
                error = false;
            }

            return error;
        }

        private bool evaluarDniOp()
        {
            bool error;

            if (txtDniOp.Text == "" || txtDniOp.Text.Length != 8)
            {
                MessageBox.Show("Dni para buscar o borrar incorrecto");
                txtDniOp.Text = "";
                txtDniOp.Focus();
                error = true;
            }
            else
            {
                error = false;
            }

            return error;
        }
    }
}
