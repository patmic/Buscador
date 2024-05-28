using System.Net.Http.Json;
using System.Text;
using ClientApp.Helpers;
using ClientApp.Models;
using ClientApp.Services.IService;
using Newtonsoft.Json;

namespace ClientApp.Services {
    public class HomologacionRepository : IHomologacionRepository
    {
        private readonly HttpClient _httpClient;
        private string url = $"{Inicializar.UrlBaseApi}api/homologacion";

        public HomologacionRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RespuestaRegistro> EliminarHomologacion(int idHomologacion)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{url}/{idHomologacion}");
            if (response.IsSuccessStatusCode)
            {
                return new RespuestaRegistro { registroCorrecto = true };
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RespuestaRegistro>(contentTemp);
            }
        }

        public async Task<VwHomologacion> GetHomologacionAsync(int idHomologacion)
        {
            var response = await _httpClient.GetAsync($"{url}/{idHomologacion}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<VwHomologacion>();
        }

        public async Task<List<VwHomologacion>> GetHomologacionsAsync(int value)
        {
            var response = await _httpClient.GetAsync($"{url}/findByParent/{value}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<VwHomologacion>>();
        }

        public async Task<RespuestaRegistro> RegistrarOActualizar(VwHomologacion registro)
        {
            var content = JsonConvert.SerializeObject(registro);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            if (registro.IdHomologacion > 0)
            {
                response = await _httpClient.PutAsync($"{url}/{registro.IdHomologacion}", bodyContent);
            }
            else
            {
                response = await _httpClient.PostAsync(url, bodyContent);
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