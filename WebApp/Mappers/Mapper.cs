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
            CreateMap<DataLakeOrganizacion, BuscadorDto>();
            CreateMap<BuscadorOrganizacion, BuscadorDto>();
            CreateMap<IVwHomologacion, CatalogosDto>();
            CreateMap<VwPais, CatalogosDto>();
            CreateMap<VwGrilla, CatalogosDto>();
            CreateMap<VwFiltro, CatalogosDto>();
            CreateMap<HomologacionEsquema, HomologacionEsquemaDto>();
            CreateMap<HomologacionEsquemaDto, HomologacionEsquema>();
        }
    }
}
