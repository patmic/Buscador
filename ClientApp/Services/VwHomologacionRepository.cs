using System.Net.Http.Json;
using ClientApp.Helpers;
using ClientApp.Services.IService;
using ClientApp.Models;

namespace ClientApp.Services {
    // Crea un servicio que maneje la lógica para llamar al API REST y obtener los datos.
    public class VwHomologacionRepository (HttpClient httpClient) : IVwHomologacionRepository
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<VwHomologacion>> GetHomologacionAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/catalogos/{endpoint}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<VwHomologacion>>();
        }

        public async Task<List<VwHomologacion>> GetHomologacionDetalleAsync(string endpoint, int IdHomologacion)
        {
            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/catalogos/{endpoint}/{IdHomologacion}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<VwHomologacion>>();
        }
   }
}