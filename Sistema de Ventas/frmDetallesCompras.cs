using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Ventas
{
    public partial class frmDetallesCompras : Form
    {
        private Compra miCompra = new Compra();
        private List<Compra> misCompras = new List<Compra>();

        public frmDetallesCompras()
        {
            InitializeComponent();
        }

        private void frmDetallesCompras_Load(object sender, EventArgs e)
        {
            misCompras = miCompra.DeserializarListaCompra();
            foreach (Compra compra in misCompras)
            {
                cbxID.Items.Add(compra.IDCompra);
            }
        }

        private void cbxID_SelectedIndexChanged(object sender, EventArgs e)
        {
            misCompras = miCompra.DeserializarListaCompra();

            /* lblDatos.Text = $"ID de la compra: {miCompra.misCompras[cbxID.SelectedIndex].IDCompra}\n" +
                              $"Proveedor: {miCompra.misCompras[cbxID.SelectedIndex].Proveedor}\n" +
                              $"Fecha: {miCompra.misCompras[cbxID.SelectedIndex].FechaCompra}\n";*/
        }

        private void ActualizarDataGrid()
        {
            /*dtgCompras.Rows.Clear();
            foreach (Compra miCompra in miCompra.DeserializarListaCompra())
            {
                dtgCompras.Rows.Add(miCompra.NombreProducto, miCompra.PrecioCompra, miCompra.StockProducto, miCompra.PrecioCompra * miCompra.StockProducto);
            }
            lblMontoTotal =*/
        }
    }
}