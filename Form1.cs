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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            var usu = new LoginMetodos();
            var email = txtEmail.Text;
            var password = txtPassword.Text;
            dt = usu.consultarLogin(email, password);

            if (dt.Rows.Count == 1)
            {
                MessageBox.Show("Ingreso correcto");
                var frm = new Administrador();
                this.Hide();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Usuario o password incorrectos");
            }
        }

        private void llbNuevoUsuario_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new NuevoUsuarios();
            frm.Show();
        }
    }
}
