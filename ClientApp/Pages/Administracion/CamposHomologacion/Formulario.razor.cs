using BlazorBootstrap;
using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Pages.Administracion.CamposHomologacion
{
    public partial class Formulario
    {
        private Button saveButton = default!;
        private VwHomologacion homologacion = new VwHomologacion();
        private VwHomologacion homologacionGrupo = new VwHomologacion();
        [Inject]
        public IHomologacionRepository homologacionRepository { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Parameter]
        public int? Id { get; set; }
        [Parameter]
        public int? IdPadre { get; set; }
        [Inject]
        public Services.ToastService toastService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            homologacionGrupo = await homologacionRepository.GetHomologacionAsync((int) IdPadre);
            if (Id > 0) {
                homologacion = await homologacionRepository.GetHomologacionAsync(Id.Value);
            } else {
                homologacion.IdHomologacionGrupo = IdPadre;
                homologacion.InfoExtraJson = "{}";
                homologacion.MascaraDato = "TEXTO";
            }
        }
        private async Task GuardarHomologacion()
        {
            saveButton.ShowLoading("Guardando...");

            var result = await homologacionRepository.RegistrarOActualizar(homologacion);
            if (result.registroCorrecto)
            {
                toastService.CreateToastMessage(ToastType.Success, "Registrado exitosamente");
                navigationManager.NavigateTo("/campos-homologacion");
            }
            else
            {
                toastService.CreateToastMessage(ToastType.Danger, "Debe llenar todos los campos");
            }

            saveButton.HideLoading();
        }
        private void OnAutoCompleteChanged(string mascaraDato) {
            homologacion.MascaraDato = mascaraDato;
        }
    }
}