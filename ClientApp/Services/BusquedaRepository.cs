using System.Net.Http.Json;
using ClientApp.Helpers;
using ClientApp.Models;
using ClientApp.Services.IService;

namespace ClientApp.Services {
    public class BusquedaRepository : IBusquedaRepository
    {
        private readonly HttpClient _httpClient;
        public BusquedaRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<DataHomologacionEsquema>> PsBuscarPalabraAsync(string value, int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/buscador/buscarPalabra?value={value}&pageNumber={pageNumber}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<DataHomologacionEsquema>>();
        }
        public async Task<List<HomologacionEsquema>> FnHomologacionEsquemaTodoAsync()
        {
            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/buscador/homologacionEsquemaTodo");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<HomologacionEsquema>>();
        }
        public async Task<HomologacionEsquema> FnHomologacionEsquemaAsync(int idHomologacionEsquema)
        {
            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/buscador/homologacionEsquema/{idHomologacionEsquema}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<HomologacionEsquema>();
        }
        public async Task<List<DataHomologacionEsquema>> FnHomologacionEsquemaDatoAsync(int idHomologacionEsquema, int idDataLakeOrganizacion)
        {
            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/buscador/homologacionEsquemaDato/{idHomologacionEsquema}/{idDataLakeOrganizacion}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<DataHomologacionEsquema>>();
        }
    }
}