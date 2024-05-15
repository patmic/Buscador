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

        public UsuariosRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/usuarios");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Usuario>>();
        }

        public async Task<Usuario> GetUsuarioAsync(int IdUsuario)
        {
            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/usuarios/{IdUsuario}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }
        public async Task<RespuestaRegistro> RegistrarUsuario(Usuario usuarioParaRegistro)
        {
            var content = JsonConvert.SerializeObject(usuarioParaRegistro);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{Inicializar.UrlBaseApi}api/usuarios/registro", bodyContent);
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