using System.Net.Http.Json;
using ClientApp.Helpers;
using ClientApp.Services.IService;

namespace ClientApp.Services {
    public class BusquedaRepository : IBusquedaRepository
    {
        private readonly HttpClient _httpClient;

        public BusquedaRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultadoBusqueda>> BuscarPalabraAsync(string value, params int[] fields)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/buscador/buscar_palabras?value={value}&field1=47");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ResultadoBusqueda>>();
        }
    }
}