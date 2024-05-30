using BlazorBootstrap;
using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Pages.Administracion.Grupo
{
    public partial class Formulario
    {
        private Button saveButton = default!;
        private VwHomologacion homologacion = new VwHomologacion();
        [Inject]
        public IHomologacionRepository homologacionRepository { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Parameter]
        public int? Id { get; set; }
        [Inject]
        public Services.ToastService toastService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (Id > 0) {
                homologacion = await homologacionRepository.GetHomologacionAsync(Id.Value);
            } else {
                homologacion.InfoExtraJson = "{}";
            }
        }
        private async Task GuardarHomologacion()
        {
            saveButton.ShowLoading("Guardando...");

            var result = await homologacionRepository.RegistrarOActualizar(homologacion);
            if (result.registroCorrecto)
            {
                toastService.CreateToastMessage(ToastType.Success, "Registrado exitosamente");
                navigationManager.NavigateTo("/grupos");
            }
            else
            {
                toastService.CreateToastMessage(ToastType.Danger, "Debe llenar todos los campos");
            }

            saveButton.HideLoading();
        }
    }
}
