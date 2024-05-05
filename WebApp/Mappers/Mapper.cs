using WebApp.Models;
using WebApp.Models.Dtos;
using AutoMapper;

namespace WebApp.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioActualizarDto, Usuario>();
            CreateMap<Models.Endpoint, EndpointDto>();
            CreateMap<UsuarioEndpointPermiso, UsuarioEndpointPermisoDto>();
            CreateMap<Empresa, EmpresaDto>();
            CreateMap<Organizacion, OrganizacionDto>();
            CreateMap<VwPais, CatalogosDto>();
            CreateMap<VwRazonSocial, CatalogosDto>();
            CreateMap<VwOrgAcredita, CatalogosDto>();
            CreateMap<VwEstado, CatalogosDto>();
            CreateMap<VwEsqAcredita, CatalogosDto>();
            CreateMap<VwAlcance, CatalogosDto>();
            CreateMap<Armonizacion, ArmonizacionDto>();
        }
    }
}
