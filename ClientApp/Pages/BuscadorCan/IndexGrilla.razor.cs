using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using ClientApp.Services.IService;
using ClientApp.Models;
using Newtonsoft.Json;

namespace ClientApp.Pages.BuscadorCan
{
    public partial class IndexGrilla
    {
        [Parameter]
        public BuscarRequest buscarRequest { get; set; }
        [Parameter]
        public List<List<int>> selectedValues { get; set; }
        [Inject]
        public IBusquedaService servicio { get; set; }
        [Inject]
        public IVwHomologacionRepository vwHomologacionRepository { get; set; }
        private Modal modal = default!;
        public Grid<DataHomologacionEsquema> grid;
        private List<VwHomologacion>? listaEtiquetasGrilla;
        private int totalCount = 0;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                listaEtiquetasGrilla = await vwHomologacionRepository.GetHomologacionAsync("etiquetas_grilla");
            } catch (Exception) { }
        }
        private async Task<List<DataHomologacionEsquema>> BuscarEsquemas(int PageNumber, int PageSize)
        {
            var listDataHomologacionEsquema = new List<DataHomologacionEsquema>();
            try {
                ResultDataHomologacionEsquema result = await servicio.PsBuscarPalabraAsync(JsonConvert.SerializeObject(new {
                    TextoBuscar = buscarRequest.TextoBuscar ?? "",
                    IdHomologacionFiltro = selectedValues.SelectMany(list => list).Where(c => c != 0).ToList()
                }), PageNumber, PageSize);

                if (!(result.Data is null)) {
                    listDataHomologacionEsquema = result.Data;
                }

                if (PageNumber == 1) {
                    totalCount = result.TotalCount;
                }
            } catch (Exception e) { 
                Console.WriteLine(e);
            }

            return listDataHomologacionEsquema;
        }
        private async Task<GridDataProviderResult<DataHomologacionEsquema>> ResultadoBusquedaDataProvider(GridDataProviderRequest<DataHomologacionEsquema> request)
        {
            return await Task.FromResult(new GridDataProviderResult<DataHomologacionEsquema> {
                Data = await BuscarEsquemas(request.PageNumber, request.PageSize),
                TotalCount = totalCount
            });
        }
        private async void showModal(DataHomologacionEsquema dataLake)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("dataLake", dataLake);
            await modal.ShowAsync<EsquemaModal>(title: "Información Detallada", parameters: parameters);
        }
    }
}