using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Pages.Administracion
{
    public partial class Formulario
    {
        private Usuario Usuario = new Usuario();
        public bool EstaProcesando { get; set; } = false;
        public bool MostrarErroresRegistro {  get; set; }

        public IEnumerable<string> Errores { get; set; }

        [Inject]
        public IUsuariosRepository usuariosRepository { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Parameter]
        public int? Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Id > 0) {
                Usuario = await usuariosRepository.GetUsuarioAsync(Id.Value);
            }
        }
        private async Task RegistrarUsuario()
        {
            MostrarErroresRegistro = false;
            EstaProcesando = true;
            var result = await usuariosRepository.RegistrarUsuario(Usuario);
            if (result.registroCorrecto)
            {
                EstaProcesando = false;
                navigationManager.NavigateTo("/usuarios");
            }
            else
            {
                EstaProcesando = false;
                Errores = result.Errores;
                MostrarErroresRegistro = true;
            }
        }
    }
}
