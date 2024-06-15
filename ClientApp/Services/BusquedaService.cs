using System.Net.Http.Json;
using ClientApp.Helpers;
using ClientApp.Models;
using ClientApp.Services.IService;

namespace ClientApp.Services
{
    public class BusquedaService(HttpClient httpClient) : IBusquedaService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<ResultDataHomologacionEsquema> PsBuscarPalabraAsync(string paramJSON, int PageNumber, int RowsPerPage)
        {
            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/buscador/buscarPalabra?paramJSON={paramJSON}&PageNumber={PageNumber}&RowsPerPage={RowsPerPage}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ResultDataHomologacionEsquema>();
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