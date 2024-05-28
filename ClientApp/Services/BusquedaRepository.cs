using System.Net.Http.Json;
using ClientApp.Helpers;
using ClientApp.Models;
using ClientApp.Services.IService;
using Newtonsoft.Json;

namespace ClientApp.Services {
    public class BusquedaRepository : IBusquedaRepository
    {
        private readonly HttpClient _httpClient;
        public BusquedaRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<DataLakeOrganizacion>> BuscarPalabraAsync(string value, params int[] fields)
        {
            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/buscador/buscar_palabras?value={value}&field1=41");
            response.EnsureSuccessStatusCode();

            var tempList = await response.Content.ReadFromJsonAsync<List<ResultDataLakeOrganizacion>>();
            return tempList.Select(c => new DataLakeOrganizacion()
                {
                    IdDataLakeOrganizacion = c.IdDataLakeOrganizacion,
                    DataJson = JsonConvert.DeserializeObject<List<Columna>>(c.DataJson)
                })
                .ToList();
        }
        public async Task<List<HomologacionEsquema>> ObtenerEsquemasRelacionados(int Id)
        {
            var response = await _httpClient.GetAsync($"{Inicializar.UrlBaseApi}api/buscador/esquemas_relacionados/{Id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<HomologacionEsquema>>();
        }
    }
}