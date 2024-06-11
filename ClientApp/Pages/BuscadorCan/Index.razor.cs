using BlazorBootstrap;
using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace ClientApp.Pages.BuscadorCan
{
    public partial class Index
    {
        private Modal modal = default!;
        private Grid<DataHomologacionEsquema> grid;
        private BuscarRequest buscarRequest = new BuscarRequest();
        [Inject]
        public IBusquedaRepository servicio { get; set; }
        [Inject]
        public IVwHomologacionRepository vwHomologacionRepository { get; set; }
        private List<VwHomologacion>? listaEtiquetasFiltros;
        private List<VwHomologacion>? listaEtiquetasGrilla;
        private List<List<VwHomologacion>>? listadeOpciones = new List<List<VwHomologacion>>();
        private List<List<int>> selectedValues = new List<List<int>>();
        private List<DataHomologacionEsquema>? listDataHomologacionEsquema;
        private int totalCount = 1000;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                listaEtiquetasFiltros = await vwHomologacionRepository.GetHomologacionAsync("etiquetas_filtro");

                foreach(var opciones in listaEtiquetasFiltros)
                {
                    listadeOpciones.Add(await vwHomologacionRepository.GetHomologacionDetalleAsync("filtro_detalles", opciones.IdHomologacion));
                }

                listaEtiquetasGrilla = await vwHomologacionRepository.GetHomologacionAsync("etiquetas_grilla");
            } catch (Exception) { }
        }
        private void CambiarSeleccion(int selectedValue, int index)
        {
            while (selectedValues.Count <= index)
            {
                selectedValues.Add(new List<int>());
            }

            if (selectedValues[index].Contains(selectedValue))
            {
                selectedValues[index].Remove(selectedValue);
            }
            else
            {
                selectedValues[index].Add(selectedValue);
            }
        }
        private async Task BuscarPalabraRequest()
        {
            await grid.ResetPageNumber();
            // await grid.RefreshDataAsync();
        }
        private async Task<GridDataProviderResult<DataHomologacionEsquema>> ResultadoBusquedaDataProvider(GridDataProviderRequest<DataHomologacionEsquema> request)
        {
            try {
                listDataHomologacionEsquema = await servicio.PsBuscarPalabraAsync(JsonConvert.SerializeObject(new {
                    TextoBuscar = buscarRequest.TextoBuscar ?? "",
                    IdHomologacionFiltro = selectedValues.SelectMany(list => list).Where(c => c != 0).ToList()
                }), request.PageNumber, request.PageSize);
            } catch (Exception e) { 
                Console.WriteLine(e);
            }

            if (listDataHomologacionEsquema is null) {
                listDataHomologacionEsquema = new List<DataHomologacionEsquema>();
            }

            return await Task.FromResult(new GridDataProviderResult<DataHomologacionEsquema> { Data = listDataHomologacionEsquema, TotalCount = listDataHomologacionEsquema.Count() == 0 ? 0 : totalCount });
        }
        private async void showModal(DataHomologacionEsquema dataLake)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("dataLake", dataLake);
            await modal.ShowAsync<EsquemaModal>(title: "Información Detallada", parameters: parameters);
        }
    }
}