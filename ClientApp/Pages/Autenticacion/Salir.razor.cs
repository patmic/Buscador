using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Pages.Autenticacion
{
    public partial class Salir
    {
        [Inject]
        public IServiceAutenticacion servicioAutenticacion { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await servicioAutenticacion.Salir();
            navigationManager.NavigateTo("/");
        }
    }
}
