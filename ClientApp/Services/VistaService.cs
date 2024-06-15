using System.Net.Http.Json;
using ClientApp.Helpers;
using ClientApp.Services.IService;
using ClientApp.Models;
using SharedApp.Models.Dtos;

namespace ClientApp.Services {
    // Crea un servicio que maneje la lógica para llamar al API REST y obtener los datos.
    public class VistaService (HttpClient httpClient) : IVistaService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<VistaDto>> GetFindBySystemAsync(int idHomologacionSistema)
        {
            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/vistas/por_sistema/{idHomologacionSistema}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<VistaDto>>();
        }
        public async Task<List<PropiedadesTablaDto>> GetPropertiesAsync(string vistaNombre)
        {
            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/vistas/propiedades/{vistaNombre}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<PropiedadesTablaDto>>();
        }
   }
}