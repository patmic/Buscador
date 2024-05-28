using BlazorBootstrap;
using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Pages.Administracion.Usuarios
{
    public partial class Listado
    {
        private List<Usuario>? listaUsuarios;
        [Inject]
        IUsuariosRepository usuariosRepository { get; set; }
        private async Task<GridDataProviderResult<Usuario>> UsuariosDataProvider(GridDataProviderRequest<Usuario> request)
        {
            if (listaUsuarios is null)
                listaUsuarios = await usuariosRepository.GetUsuariosAsync();

            return await Task.FromResult(request.ApplyTo(listaUsuarios));
        }
    }
}