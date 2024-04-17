// Utilizaremos el patrón Repository para esto.
public interface IOrganizacionRepository
{
    Task<List<Organizacion>> GetOrganizacionesAsync();
}