using BlazorBootstrap;
using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Pages.Administracion.Usuarios
{
    public partial class Formulario
    {
        private Button saveButton = default!;
        private Usuario Usuario = new Usuario();
        [Inject]
        public IUsuariosRepository usuariosRepository { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Parameter]
        public int? Id { get; set; }
        [Inject]
        public Services.ToastService toastService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Id > 0) {
                Usuario = await usuariosRepository.GetUsuarioAsync(Id.Value);
                Usuario.Clave = null;
            } else {
                Usuario.Estado = "A";
            }
        }
        private async Task RegistrarUsuario()
        {
            saveButton.ShowLoading("Guardando...");

            var result = await usuariosRepository.RegistrarOActualizar(Usuario);
            if (result.registroCorrecto)
            {
                toastService.CreateToastMessage(ToastType.Success, "Registrado exitosamente");
                navigationManager.NavigateTo("/usuarios");
            }
            else
            {
                toastService.CreateToastMessage(ToastType.Danger, "Error al registrar en el servidor");
            }

            saveButton.HideLoading();
        }
    }
}
