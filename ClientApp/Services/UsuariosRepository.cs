using System.Net.Http.Json;
using System.Text;
using ClientApp.Helpers;
using ClientApp.Models;
using ClientApp.Services.IService;
using Newtonsoft.Json;

namespace ClientApp.Services {
    // Crea un servicio que maneje la lógica para llamar al API REST y obtener los datos.
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly HttpClient _httpClient;
        private string url = $"{Inicializar.UrlBaseApi}api/usuarios";

        public UsuariosRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            var response = await _httpClient.GetAsync($"{url}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Usuario>>();
        }

        public async Task<Usuario> GetUsuarioAsync(int IdUsuario)
        {
            var response = await _httpClient.GetAsync($"{url}/{IdUsuario}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }
        public async Task<RespuestaRegistro> RegistrarOActualizar(Usuario registro)
        {
            var content = JsonConvert.SerializeObject(registro);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            if (registro.IdUsuario > 0)
            {
                response = await _httpClient.PutAsync($"{url}/{registro.IdUsuario}", bodyContent);
            }
            else
            {
                response = await _httpClient.PostAsync($"{url}/registro", bodyContent);
            }

            var contentTemp = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<RespuestaRegistro>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new RespuestaRegistro { registroCorrecto = true };
            }
            else
            {
                return resultado;
            }
        }
    }
}