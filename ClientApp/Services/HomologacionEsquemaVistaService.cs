using ClientApp.Services.IService;
using ClientApp.Models;
using SharedApp.Models.Dtos;
using Newtonsoft.Json;
using System.Text;
using ClientApp.Helpers;
using System.Net.Http.Json;

namespace ClientApp.Services {
    // Crea un servicio que maneje la lógica para llamar al API REST y obtener los datos.
    public class HomologacionEsquemaVistaService (HttpClient httpClient) : IHomologacionEsquemaVistaService
    {
        private readonly HttpClient _httpClient = httpClient;
        private string url = $"{Inicializar.UrlBaseApi}api/homologacion_esquema_vista";

        public async Task<RespuestaRegistro> Registrar(List<HomologacionEsquemaVistaDto> data)
        {
            var content = JsonConvert.SerializeObject(data);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response;

            response = await _httpClient.PostAsync($"{url}", bodyContent);

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
        public async Task<List<HomologacionEsquemaVistaDto>> GetFindByVistaEsquemaAsync(int idVista, int idHomologacionEsquema)
        {
            var response = await _httpClient.GetAsync($"{url}/por_vista_esquema/{idVista}/{idHomologacionEsquema}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<HomologacionEsquemaVistaDto>>();
        }
    }
}