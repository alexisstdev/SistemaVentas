using System;
using System.Windows.Forms;

namespace Sistema_de_Ventas
{
    public partial class frmProductos : Form
    {
        private Producto miProducto = new Producto();

        public frmProductos()
        {
            InitializeComponent();
        }

        private void ActualizarDataGrid()
        {
            dtgProductos.Rows.Clear();
            foreach (Producto miProducto in miProducto.misProductos)
            {
                dtgProductos.Rows.Add(miProducto.IDProducto, miProducto.NombreProducto, miProducto.StockProducto, miProducto.PrecioCompra, miProducto.PrecioVenta, miProducto.Proveedor);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            miProducto.misProductos.RemoveAt(dtgProductos.CurrentCell.RowIndex);
            miProducto.SerializarLista();
            ActualizarDataGrid();
            LimpiarDatos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            foreach (Control control in datosContainer.Controls)
            {
                if (control.Text.Trim() == "" || control.Text == "0.00")
                {
                    MessageBox.Show("Todos los campos son obligatorios");
                    return;
                }
            }

            if (miProducto.misProductos.FindIndex(x => x.IDProducto == txtID.Text) != -1)
            {
                var messageBox = MessageBox.Show($"Ya existe un producto con el ID: {txtID.Text} ¿Desea sobreescribirlo?", "",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

                if (messageBox == DialogResult.No) return;
                else miProducto.misProductos.RemoveAt(miProducto.misProductos.FindIndex(x => x.IDProducto == txtID.Text));
            }
            var producto = new Producto
            {
                IDProducto = txtID.Text,
                NombreProducto = txtNombre.Text,
                Proveedor = txtProveedor.Text,
                StockProducto = (int)nudStock.Value,
                PrecioCompra = (double)nudCompra.Value,
                PrecioVenta = (double)nudVenta.Value
            };

            miProducto.misProductos.Add(producto);
            miProducto.SerializarLista();

            ActualizarDataGrid();

            LimpiarDatos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarDatos();
        }

        private void dtgProductos_SelectionChanged(object sender, EventArgs e)
        {
            lblIndex.Text = $"{dtgProductos.CurrentCell.RowIndex}";

            miProducto.DeserializarLista();

            if (dtgProductos.CurrentCell.RowIndex >= 0 && dtgProductos.CurrentCell.RowIndex < miProducto.misProductos.Count)
            {
                txtID.Text = miProducto.misProductos[dtgProductos.CurrentCell.RowIndex].IDProducto;
                txtNombre.Text = miProducto.misProductos[dtgProductos.CurrentCell.RowIndex].NombreProducto;
                nudStock.Value = miProducto.misProductos[dtgProductos.CurrentCell.RowIndex].StockProducto;
                nudCompra.Value = (decimal)miProducto.misProductos[dtgProductos.CurrentCell.RowIndex].PrecioCompra;
                nudVenta.Value = (decimal)miProducto.misProductos[dtgProductos.CurrentCell.RowIndex].PrecioVenta;
                txtProveedor.Text = miProducto.misProductos[dtgProductos.CurrentCell.RowIndex].Proveedor;
            }
        }

        private void LimpiarDatos()
        {
            foreach (Control control in datosContainer.Controls)
            {
                NumericUpDown nud = control as NumericUpDown;
                if (control is TextBox) control.Text = "";
                if (control is NumericUpDown) nud.Value = 0;
            }
        }

        private void frmProductos_Load_1(object sender, EventArgs e)
        {
            miProducto.DeserializarLista();
            ActualizarDataGrid();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            int indexBusqueda = -1;
            if (txtBusqueda.Text == "")
            {
                MessageBox.Show("Campo de búsqueda vacío");
                return;
            }
            switch (cbxBuscar.Text)
            {
                case "Nombre":
                    indexBusqueda = miProducto.misProductos.FindIndex(x => x.NombreProducto.Contains(txtBusqueda.Text));
                    break;

                case "ID":
                    indexBusqueda = miProducto.misProductos.FindIndex(x => x.IDProducto.Contains(txtBusqueda.Text));
                    break;

                case "Stock":
                    indexBusqueda = miProducto.misProductos.FindIndex(x => x.StockProducto == int.Parse(txtBusqueda.Text));
                    break;

                default:
                    MessageBox.Show("Seleccione una opción de búsqueda");
                    break;
            }
            if (indexBusqueda == -1)
            {
                MessageBox.Show($"No se encontró un producto con este {cbxBuscar.Text}");
                return;
            }
            dtgProductos.Rows[indexBusqueda].Selected = true;
        }
    }
}