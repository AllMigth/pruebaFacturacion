using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FacturacionCliente.Models;

namespace FacturacionCliente.Services
{
    public class ApiService
    {
        private readonly HttpClient _http = new();
        public string Token { get; set; } = "";

        public ApiService(string baseUrl)
        {
            _http.BaseAddress = new Uri(baseUrl);
        }

        public async Task<string> LoginAsync(string usuario, string password)
        {
            var body = new { usuario, password };
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var response = await _http.PostAsync("/api/auth/login", content);
            var json = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(json);
            Token = $"Bearer {result.token}";
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.token.ToString());
            return Token;
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            var response = await _http.GetAsync("/api/productos");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Producto>>(json) ?? new List<Producto>();
        }


        public async Task<string> AgregarAlCarritoAsync(int productoId, int cantidad)
        {
            var body = new { productoId, cantidad };
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var response = await _http.PostAsync("/api/carrito/agregar", content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ConfirmarCompraAsync()
        {
            var response = await _http.PostAsync("/api/carrito/confirmar", null);
            return await response.Content.ReadAsStringAsync();
        }
        public async Task CrearProductoAsync(Producto producto)
        {
            var json = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
            await _http.PostAsync("/api/productos", json);
        }

        public async Task ActualizarProductoAsync(Producto producto)
        {
            var json = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
            await _http.PutAsync($"/api/productos/{producto.Id}", json);
        }

        public async Task EliminarProductoAsync(int id)
        {
            await _http.DeleteAsync($"/api/productos/{id}");
        }

    }
}
