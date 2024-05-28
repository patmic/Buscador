using BlazorBootstrap;
using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace ClientApp.Pages.Administracion.Esquemas
{
    public partial class Listado
    {
        private Modal modal = default!;
        private Grid<HomologacionEsquema> grid;
        public event Action DataLoaded;
        private IEnumerable<HomologacionEsquema>? listaHomologacionEsquemas;
        [Inject]
        private IHomologacionEsquemaRepository homologacionEsquemaRepository { get; set; }
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        public IVwHomologacionRepository vwHomologacionRepository { get; set; }
        private List<VwHomologacion>? listaVwHomologacion;

        protected override async Task OnInitializedAsync()
        {
            listaVwHomologacion = await vwHomologacionRepository.GetHomologacionAsync("dimension");

            DataLoaded += async () => {
                if (!(listaHomologacionEsquemas is null)) {
                    await Task.Delay(2000);
                    await JSRuntime.InvokeVoidAsync("initSortable", DotNetObjectReference.Create(this));
                }
            };
        }
        private async Task<GridDataProviderResult<HomologacionEsquema>> EsquemasDataProvider(GridDataProviderRequest<HomologacionEsquema> request)
        {
            if (listaHomologacionEsquemas is null)
                listaHomologacionEsquemas = await homologacionEsquemaRepository.GetHomologacionEsquemasAsync();

            DataLoaded?.Invoke();

            return await Task.FromResult(request.ApplyTo(listaHomologacionEsquemas));
        }
        [JSInvokable]
        public async Task OnDragEnd(string[] sortedIds)
        {
            for (int i = 0; i < sortedIds.Length; i += 1)
            {
                HomologacionEsquema homo = listaHomologacionEsquemas.FirstOrDefault(h => h.IdHomologacionEsquema == int.Parse(sortedIds[i]));
                if (homo != null && homo.MostrarWebOrden != i + 1)
                {
                    homo.MostrarWebOrden = i + 1;
                    await homologacionEsquemaRepository.RegistrarOActualizar(homo);
                }
            }
            await Task.CompletedTask;
        }
        private async Task OnDeleteClick(int IdHomologacionEsquema)
        {
            var respuesta = await homologacionEsquemaRepository.EliminarHomologacionEsquema(IdHomologacionEsquema);
            if (respuesta.registroCorrecto) {
                listaHomologacionEsquemas = listaHomologacionEsquemas.Where(c => c.IdHomologacionEsquema != IdHomologacionEsquema);
                await grid.RefreshDataAsync();
            }
        }
        private async void showModal(int IdHomologacionEsquema)
        {
            var homo = listaHomologacionEsquemas.FirstOrDefault(c => c.IdHomologacionEsquema == IdHomologacionEsquema);
            var columnas = JsonConvert.DeserializeObject<List<VwHomologacion>>(homo.EsquemaJson);

            var parameters = new Dictionary<string, object>();
            parameters.Add("columnas", columnas);
            parameters.Add("listaVwHomologacion", listaVwHomologacion);
            await modal.ShowAsync<RowModal>(title: $"{homo.MostrarWeb}", parameters: parameters);
        }
    }
}