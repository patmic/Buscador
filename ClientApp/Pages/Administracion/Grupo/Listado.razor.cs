using BlazorBootstrap;
using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ClientApp.Pages.Administracion.Grupo
{
    public partial class Listado
    {
        private Grid<VwHomologacion> grid;
        private List<VwHomologacion>? listaHomologacions = new List<VwHomologacion>();
        [Inject]
        private IVwHomologacionRepository vwHomologacionRepository { get; set; }
        [Inject]
        private IHomologacionRepository homologacionRepository { get; set; }
        public event Action DataLoaded;
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        protected override async Task OnInitializedAsync()
        {
            DataLoaded += async () => {
                if (!(listaHomologacions is null)) {
                    await Task.Delay(2000);
                    await JSRuntime.InvokeVoidAsync("initSortable", DotNetObjectReference.Create(this));
                }
            };
        }
        private async Task<GridDataProviderResult<VwHomologacion>> HomologacionDataProvider(GridDataProviderRequest<VwHomologacion> request)
        {
            listaHomologacions = await vwHomologacionRepository.GetHomologacionAsync("grupo");

            DataLoaded?.Invoke();

            return await Task.FromResult(request.ApplyTo(listaHomologacions));
        }
        private async Task OnDeleteClick(int IdHomologacion)
        {
            var respuesta = await homologacionRepository.EliminarHomologacion(IdHomologacion);
            if (respuesta.registroCorrecto) {
                await grid.RefreshDataAsync();
            }
        }
        [JSInvokable]
        public async Task OnDragEnd(string[] sortedIds)
        {
            for (int i = 0; i < sortedIds.Length; i += 1)
            {
                VwHomologacion homo = listaHomologacions.FirstOrDefault(h => h.IdHomologacion == int.Parse(sortedIds[i]));
                if (homo != null && homo.MostrarWebOrden != i + 1)
                {
                    homo.MostrarWebOrden = i + 1;
                    await homologacionRepository.RegistrarOActualizar(homo);
                }
            }
            await Task.CompletedTask;
        }
    }
}