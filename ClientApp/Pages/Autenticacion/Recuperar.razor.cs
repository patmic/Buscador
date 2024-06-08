using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;
using BlazorBootstrap;

namespace ClientApp.Pages.Autenticacion
{
    public partial class Recuperar
    {
        private Button saveButton = default!;
        private UsuarioRecuperacion usuarioRecuperacion = new UsuarioRecuperacion();
        public string alertMessage { get; set; }
        [Inject]
        public IServiceAutenticacion servicioAutenticacion { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        private async Task RecuperarClave()
        {
            saveButton.ShowLoading("Verificando...");
            var result = await servicioAutenticacion.Recuperar(usuarioRecuperacion);

            if (result.IsSuccess)
            {
                navigationManager.NavigateTo("/acceder");
            }
            else
            {
                alertMessage = result.ErrorMessages;
            }

            saveButton.HideLoading();
            await Task.CompletedTask;
        }
    }
}