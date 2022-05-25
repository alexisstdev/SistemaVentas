using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_de_Ventas
{
    public partial class Inicio : Form
    {
        private static Form formularioActivo = null;
        private static IconMenuItem MenuActivo = null;
        private static Usuario usuarioActual;

        public Inicio(Usuario miUsuario)
        {
            usuarioActual = miUsuario;
            InitializeComponent();
        }

        private void AbrirForms(IconMenuItem menu, Form formulario)
        {
            if (MenuActivo != null) MenuActivo.BackColor = Color.FromArgb(239, 247, 249);

            menu.BackColor = Color.FromArgb(155, 196, 203);
            MenuActivo = menu;

            if (formularioActivo != null) formularioActivo.Close();
            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            container.Controls.Add(formulario);
            formulario.Show();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            lblUser.Text = $"Usuario: {usuarioActual.NombreUsuario} ({usuarioActual.Rol})";

            if (usuarioActual.Rol == "Empleado")
            {
                menuCompras.Visible = false;
            }
        }

        private void menuClientes_Click(object sender, EventArgs e)
        {
            AbrirForms((IconMenuItem)sender, new frmClientes());
        }

        private void menuProductos_Click(object sender, EventArgs e)
        {
            AbrirForms((IconMenuItem)sender, new frmProductos());
        }

        private void menuUsuarios_Click_1(object sender, EventArgs e)
        {
            AbrirForms((IconMenuItem)sender, new frmUsuarios());
        }

        private void menuVentas_Click(object sender, EventArgs e)
        {
            AbrirForms((IconMenuItem)sender, new frmVentas());
        }

        private void menuAñadirCompra_Click(object sender, EventArgs e)
        {
            AbrirForms((IconMenuItem)sender, new frmCompras());
        }

        private void menuVerCompras_Click(object sender, EventArgs e)
        {
            AbrirForms((IconMenuItem)sender, new frmDetallesCompras());
        }
    }
}