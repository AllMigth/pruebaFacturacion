using FacturacionCliente.Services;
using System;
using System.Windows.Forms;

namespace FacturacionCliente
{
    public partial class FormLogin : Form
    {
        private readonly ApiService _api;

        public FormLogin()
        {
            InitializeComponent();
            _api = new ApiService("https://localhost:7275"); // Cambia el puerto si tu backend usa otro
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            // Opcional: puedes dejarlo vacío o precargar datos
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor completa los campos.", "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await _api.LoginAsync(usuario, password);
                MessageBox.Show("Inicio de sesión exitoso", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FormProductos productosForm = new FormProductos(_api);
                productosForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
