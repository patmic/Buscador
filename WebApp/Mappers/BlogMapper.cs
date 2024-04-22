using WebApp.Models;
using WebApp.Models.Dtos;
using AutoMapper;

namespace WebApp.Mappers
{
    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<Empresa, EmpresaDto>();
            CreateMap<Organizacion, OrganizacionDto>();
        }
    }
}
