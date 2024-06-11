using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;
using BlazorBootstrap;
using SharedApp.Models.Dtos;

namespace ClientApp.Pages.Autenticacion
{
    public partial class Recuperar
    {
        private Button saveButton = default!;
        private UsuarioRecuperacionDto usuarioRecuperacion = new UsuarioRecuperacionDto();
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
                alertMessage = string.Join(";", result.ErrorMessages);
            }

            saveButton.HideLoading();
            await Task.CompletedTask;
        }
    }
}