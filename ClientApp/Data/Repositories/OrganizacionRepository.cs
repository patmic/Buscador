using System.Net.Http.Json;

// Crea un servicio que maneje la lógica para llamar al API REST y obtener los datos.
public class OrganizacionRepository : IOrganizacionRepository
{
    private readonly HttpClient _httpClient;

    public OrganizacionRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Organizacion>> GetOrganizacionesAsync()
    {
        var response = await _httpClient.GetAsync("api/peru");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Organizacion>>();
    }
}