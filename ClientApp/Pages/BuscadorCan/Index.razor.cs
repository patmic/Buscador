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
        private Grid<DataLakeOrganizacion> grid;
        private BuscarRequest buscarRequest = new BuscarRequest();
        [Inject]
        public IBusquedaRepository servicio { get; set; }
        [Inject]
        public IVwHomologacionRepository vwHomologacionRepository { get; set; }
        private List<VwHomologacion>? listaEtiquetasFiltros;
        private List<VwHomologacion>? listaEtiquetasGrilla;
        private List<List<VwHomologacion>>? listadeOpciones = new List<List<VwHomologacion>>();
        private List<int> selectedValues = new List<int>();
        private List<DataLakeOrganizacion>? listDataLakeOrganizacion;
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
        private void CambiarSeleccion(ChangeEventArgs e, int index)
        {
            while (selectedValues.Count <= index)
            {
                selectedValues.Add(0);
            }

            if (int.TryParse(e.Value.ToString(), out int selectedValue))
            {
                selectedValues[index] = selectedValue;
            } else {
                selectedValues[index] = 0;
            }
        }
        private async Task BuscarPalabraRequest()
        {
            try {
                listDataLakeOrganizacion = await servicio.BuscarPalabraAsync(JsonConvert.SerializeObject(new {
                    TextoBuscar = buscarRequest.TextoBuscar ?? "",
                    IdHomologacionFiltro = selectedValues.Where(c => c != 0).ToList()
                }));
            } catch (Exception e) { 
                Console.WriteLine(e);
            }
            await grid.RefreshDataAsync();
        }
        private async Task<GridDataProviderResult<DataLakeOrganizacion>> ResultadoBusquedaDataProvider(GridDataProviderRequest<DataLakeOrganizacion> request)
        {
            if (listDataLakeOrganizacion is null) {
                listDataLakeOrganizacion = new List<DataLakeOrganizacion>();
            }

            return await Task.FromResult(request.ApplyTo(listDataLakeOrganizacion));
        }
        private async void showModal(DataLakeOrganizacion dataLake)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("dataLake", dataLake);
            await modal.ShowAsync<EsquemaModal>(title: "Información Detallada", parameters: parameters);
        }
    }
}