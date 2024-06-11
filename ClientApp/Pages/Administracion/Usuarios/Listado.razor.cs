using BlazorBootstrap;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;
using SharedApp.Models;

namespace ClientApp.Pages.Administracion.Usuarios
{
    public partial class Listado
    {
        private List<UsuarioDto>? listaUsuarios;
        [Inject]
        IUsuariosRepository usuariosRepository { get; set; }
        private async Task<GridDataProviderResult<UsuarioDto>> UsuariosDataProvider(GridDataProviderRequest<UsuarioDto> request)
        {
            if (listaUsuarios is null)
                listaUsuarios = await usuariosRepository.GetUsuariosAsync();

            return await Task.FromResult(request.ApplyTo(listaUsuarios));
        }
    }
}