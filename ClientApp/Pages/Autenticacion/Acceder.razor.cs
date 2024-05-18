using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;
using System.Web;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components.Forms;

namespace ClientApp.Pages.Autenticacion
{
    public partial class Acceder
    {
        private Button saveButton = default!;
        private EditContext editContext;
        private UsuarioAutenticacion usuarioAutenticacion = new UsuarioAutenticacion();

        public string UrlRetorno { get; set; }

        public string alertMessage { get; set; }

        [Inject]
        public IServiceAutenticacion servicioAutenticacion { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }
        protected override void OnInitialized()
        {
            editContext = new EditContext(usuarioAutenticacion);
        }

        private async Task AccesoUsuario()
        {
            if (editContext.Validate())
            {
                saveButton.ShowLoading("Verificando...");
                var result = await servicioAutenticacion.Acceder(usuarioAutenticacion);
                if (result.IsSuccess)
                {
                    var urlAbsoluta = new Uri(navigationManager.Uri);
                    var parametrosQuery = HttpUtility.ParseQueryString(urlAbsoluta.Query);
                    UrlRetorno = parametrosQuery["returnUrl"];
                    if (string.IsNullOrEmpty(UrlRetorno))
                    {
                        navigationManager.NavigateTo("/administracion");
                    }
                    else
                    {
                        navigationManager.NavigateTo("/" + UrlRetorno);
                    }
                }
                else
                {
                    alertMessage = result.ErrorMessages;
                }
                saveButton.HideLoading();
            }
            await Task.CompletedTask;
        }
    }
}
