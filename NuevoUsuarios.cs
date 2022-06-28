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
    public partial class NuevoUsuarios : Form
    {
        public NuevoUsuarios()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            evaluarErrores();

            DialogResult resp = MessageBox.Show("Confirmar usuario", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            var usuario = new Login();

            if (resp == DialogResult.Yes)
            {
                usuario.dni = Convert.ToInt32(txtDniNuevo.Text);
                usuario.nombre = txtNombreNuevo.Text;
                usuario.apellido = txtApellidoNuevo.Text;
                usuario.email = txtEmailNuevo.Text;
                usuario.clave = txtClaveNuevo.Text;
                usuario.telefono = Convert.ToDecimal(txtTelefonoNuevo.Text);
                usuario.nivel = Convert.ToInt32(txtNivelNuevo.Text);

                var usuarioMetodo = new LoginMetodos();
                Boolean guardar = usuarioMetodo.guardarUsuario(usuario);

                if (guardar)
                {
                    MessageBox.Show("Se guardó correctamente", "Guardar");
                }
                else
                {
                    MessageBox.Show("Error al guardar", "Verifique");
                }
            }
            
            this.Hide();
        }

        private void NuevoUsuarios_Load(object sender, EventArgs e)
        {
            txtDniNuevo.Focus();
        }

        private void evaluarErrores()
        {
            while(txtDniNuevo.Text.Length != 8 || txtDniNuevo.Text == "")
            {
                MessageBox.Show("Dni incorrecto");
                txtDniNuevo.Text = "";
                txtDniNuevo.Focus();
            }
            while(txtNombreNuevo.Text == "")
            {
                MessageBox.Show("Nombre incorrecto");
                txtNombreNuevo.Focus();
            }
            while (txtApellidoNuevo.Text == "")
            {
                MessageBox.Show("Apellido incorrecto");
                txtApellidoNuevo.Focus();
            }
            while (txtEmailNuevo.Text == "")
            {
                MessageBox.Show("Email incorrecto");
                txtEmailNuevo.Focus();
            }
            while (txtClaveNuevo.Text == "")
            {
                MessageBox.Show("Clave incorrecta");
                txtClaveNuevo.Focus();
            }
            while (txtTelefonoNuevo.Text.Length != 10 || txtTelefonoNuevo.Text == "")
            {
                MessageBox.Show("Teléfono incorrecto");
                txtApellidoNuevo.Focus();
            }
            while (txtNivelNuevo.Text.Length != 1 || txtTelefonoNuevo.Text == "")
            {
                MessageBox.Show("Nivel incorrecto");
                txtNivelNuevo.Focus();
            }
        }
    }
}
