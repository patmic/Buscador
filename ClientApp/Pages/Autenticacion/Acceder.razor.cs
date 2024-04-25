using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;
using System.Web;
using Microsoft.JSInterop;
using ClientApp.Helpers;

namespace ClientApp.Pages.Autenticacion
{
    public partial class Acceder
    {
        private UsuarioAutenticacion usuarioAutenticacion = new UsuarioAutenticacion();
        public bool EstaProcesando { get; set; } = false;
        public bool MostrarErroresAutenticacion { get; set; }

        public string UrlRetorno { get; set; }

        public string Errores { get; set; }

        [Inject]
        public IServiceAutenticacion servicioAutenticacion { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        private async Task AccesoUsuario()
        {
            EstaProcesando = true;
            var result = await servicioAutenticacion.Acceder(usuarioAutenticacion);
            if (result.IsSuccess)
            {
                EstaProcesando = false;
                var urlAbsoluta = new Uri(navigationManager.Uri);
                var parametrosQuery = HttpUtility.ParseQueryString(urlAbsoluta.Query);
                UrlRetorno = parametrosQuery["returnUrl"];
                if (string.IsNullOrEmpty(UrlRetorno))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/" + UrlRetorno);
                }
               await JsRuntime.ToastrSuccess("¡Credenciales Correctas!");
            }
            else
            {
                EstaProcesando = false;
                navigationManager.NavigateTo("/acceder");
                await JsRuntime.ToastrError(Errores);
            }
        }
    }
}
