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
            CreateMap<UsuarioDto, Usuario>();
            
            CreateMap<Models.Endpoint, EndpointDto>();
            
            CreateMap<UsuarioEndpointPermiso, UsuarioEndpointPermisoDto>();
            
            CreateMap<DataLakeOrganizacion, BuscadorDto>();
            
            CreateMap<BuscadorOrganizacion, BuscadorDto>();
            
            CreateMap<IVwHomologacion, CatalogosDto>();
            
            CreateMap<VwDimension, CatalogosDto>();
            CreateMap<VwGrilla, CatalogosDto>();
            CreateMap<VwFiltro, CatalogosDto>();
            
            CreateMap<HomologacionEsquema, HomologacionEsquemaDto>();
            CreateMap<HomologacionEsquemaDto, HomologacionEsquema>();

            CreateMap<Homologacion, CatalogosDto>();
            CreateMap<Homologacion, HomologacionDto>();
            CreateMap<HomologacionDto, Homologacion>();
        }
    }
}
