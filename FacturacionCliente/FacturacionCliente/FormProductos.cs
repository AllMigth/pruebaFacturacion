using FacturacionCliente.Models;
using FacturacionCliente.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FacturacionCliente
{
    public partial class FormProductos : Form
    {
        private readonly ApiService _api;
        private readonly List<CarritoDetalle> _carrito = new();
        private readonly string _usuario;
        public FormProductos(ApiService api)
        {
            InitializeComponent();
            _api = api;
            _usuario = _usuario;
        }

        private async void FormProductos_Load(object sender, EventArgs e)
        {
            var productos = await _api.GetProductosAsync();
            dgvProductos.DataSource = productos;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null) return;

            var producto = (Producto)dgvProductos.CurrentRow.DataBoundItem;
            int cantidad = (int)nudCantidad.Value;

            if (cantidad <= 0 || cantidad > producto.Stock)
            {
                MessageBox.Show("Cantidad inválida.");
                return;
            }

            _carrito.Add(new CarritoDetalle
            {
                ProductoId = producto.Id,
                NombreProducto = producto.Nombre,
                Cantidad = cantidad,
                PrecioUnitario = producto.Precio
            });

            lstCarrito.Items.Add($"{producto.Nombre} x{cantidad} = {producto.Precio * cantidad:C}");
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (_carrito.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.");
                return;
            }

            foreach (var item in _carrito)
            {
                await _api.AgregarAlCarritoAsync(item.ProductoId, item.Cantidad);
            }

            var respuesta = await _api.ConfirmarCompraAsync();

            // Calcula total
            decimal total = _carrito.Sum(c => c.Cantidad * c.PrecioUnitario);
            string detalle = string.Join("\n", _carrito.Select(c =>
                $"- {c.NombreProducto} x{c.Cantidad} = {c.Cantidad * c.PrecioUnitario:C}"));

            int diasEntrega = new Random().Next(1, 5);

            MessageBox.Show(
                $"Gracias por tu compra, {_usuario}!\n\n" +
                $"🧾 Detalle de la compra:\n{detalle}\n\n" +
                $"💰 Total: {total:C}\n\n" +
                $"📦 Tu compra llegará en {diasEntrega} días.",
                "Compra confirmada", MessageBoxButtons.OK, MessageBoxIcon.Information
            );

            _carrito.Clear();
            lstCarrito.Items.Clear();
        }


        private void btnIrAdmin_Click(object sender, EventArgs e)
        {
            FormAdminProductos adminForm = new FormAdminProductos(_api);
            adminForm.Show();
            this.Hide();
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (lstCarrito.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un producto en el carrito.");
                return;
            }

            // Obtiene el nombre del producto del texto seleccionado
            var textoSeleccionado = lstCarrito.SelectedItem.ToString();
            var nombre = textoSeleccionado.Split('x')[0].Trim(); // Ej: "LAPTOP x1 = 300"

            // Elimina del ListBox
            lstCarrito.Items.RemoveAt(lstCarrito.SelectedIndex);

            // Elimina del objeto _carrito
            var item = _carrito.FirstOrDefault(c => c.NombreProducto == nombre);
            if (item != null)
            {
                _carrito.Remove(item);
            }
        }


        private void btnModificarStock_Click(object sender, EventArgs e)
        {
            if (lstCarrito.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un producto del carrito.");
                return;
            }

            var textoSeleccionado = lstCarrito.SelectedItem.ToString();
            var nombre = textoSeleccionado.Split('x')[0].Trim(); // Ej: "LAPTOP x1 = $300"

            var item = _carrito.FirstOrDefault(c => c.NombreProducto == nombre);
            if (item == null)
            {
                MessageBox.Show("Producto no encontrado en el carrito.");
                return;
            }

            int nuevaCantidad = (int)nudCantidad.Value;
            if (nuevaCantidad <= 0)
            {
                MessageBox.Show("Cantidad inválida.");
                return;
            }

            item.Cantidad = nuevaCantidad;

            // Actualiza visualmente el ListBox
            int index = lstCarrito.SelectedIndex;
            lstCarrito.Items[index] = $"{item.NombreProducto} x{item.Cantidad} = {item.PrecioUnitario * item.Cantidad:C}";
        }

    }
}
