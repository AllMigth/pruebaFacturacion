using FacturacionCliente.Models;
using FacturacionCliente.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionCliente
{
    public partial class FormAdminProductos : Form
    {
        private readonly ApiService _api;
        private List<Producto> _productos = new();

        public FormAdminProductos(ApiService api)
        {
            InitializeComponent();
            _api = api;
        }

        private async void FormAdminProductos_Load(object sender, EventArgs e)
        {
            await CargarProductos();
        }

        private async Task CargarProductos()
        {
            _productos = await _api.GetProductosAsync();
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = _productos;
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || !int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("Precio o stock inválidos.");
                return;
            }

            var nuevo = new Producto
            {
                Nombre = txtNombre.Text.Trim(),
                Precio = precio,
                Stock = stock
            };

            await _api.CrearProductoAsync(nuevo);
            await CargarProductos();
            LimpiarFormulario();
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un producto para actualizar.");
                return;
            }

            var producto = (Producto)dgvProductos.CurrentRow.DataBoundItem;

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || !int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("Precio o stock inválidos.");
                return;
            }

            producto.Nombre = txtNombre.Text.Trim();
            producto.Precio = precio;
            producto.Stock = stock;

            await _api.ActualizarProductoAsync(producto);
            await CargarProductos();
            LimpiarFormulario();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un producto para eliminar.");
                return;
            }

            var producto = (Producto)dgvProductos.CurrentRow.DataBoundItem;
            await _api.EliminarProductoAsync(producto.Id);
            await CargarProductos();
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null) return;

            var producto = (Producto)dgvProductos.CurrentRow.DataBoundItem;
            txtNombre.Text = producto.Nombre;
            txtPrecio.Text = producto.Precio.ToString();
            txtStock.Text = producto.Stock.ToString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();
            var filtrados = _productos.Where(p => p.Nombre.ToLower().Contains(filtro)).ToList();
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = filtrados;
        }

        private async void btnRefrescar_Click(object sender, EventArgs e)
        {
            await CargarProductos();
            txtBuscar.Clear();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            FormProductos productosForm = new FormProductos(_api);
            productosForm.Show();
            this.Hide();
        }
    }
}
