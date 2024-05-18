using BlazorBootstrap;
using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Pages.BuscadorCan
{
    public partial class Index
    {
        private Grid<DataLakeOrganizacion> grid;
        private BuscarRequest buscarRequest = new BuscarRequest();
        [Inject]
        public IBusquedaRepository servicio { get; set; }
        [Inject]
        public IVwHomologacionRepository vwHomologacionRepository { get; set; }
        private List<VwHomologacion>? listaEtiquetasFiltros;
        private List<VwHomologacion>? listaEtiquetasGrilla;
        private List<List<VwHomologacion>>? listadeOpciones = new List<List<VwHomologacion>>();
        private Dictionary<int, int> valoresSeleccionados = new Dictionary<int, int>();
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
        private async Task CambiarSeleccion(ChangeEventArgs args, List<VwHomologacion> opciones, int index)
        {
            if (args.Value != null && opciones != null && opciones.Any() && index >= 0 && index < listadeOpciones.Count)
            {
                int idSeleccionado = Convert.ToInt32(args.Value);

                if (valoresSeleccionados.ContainsKey(index))
                {
                    valoresSeleccionados[index] = idSeleccionado;
                }
                else
                {
                    valoresSeleccionados.Add(index, idSeleccionado);
                }

                await BuscarPalabraRequest();
            }
        }
        private string GetSelectedValue(int index)
        {
            if (valoresSeleccionados.ContainsKey(index))
            {
                return valoresSeleccionados[index].ToString();
            }
            return "";
        }
        private async Task BuscarPalabraRequest()
        {
            try {
                if (!string.IsNullOrEmpty(buscarRequest.TextoBuscar)) {
                    listDataLakeOrganizacion = await servicio.BuscarPalabraAsync(buscarRequest.TextoBuscar);
                }
            } catch (Exception e) { 
                Console.WriteLine(e);
            }
            await grid.RefreshDataAsync();
        }
        private async Task<GridDataProviderResult<DataLakeOrganizacion>> ResultadoBusquedaDataProvider(GridDataProviderRequest<DataLakeOrganizacion> request)
        {
            if (listDataLakeOrganizacion is null && !string.IsNullOrEmpty(buscarRequest.TextoBuscar)) {
                 try {
                    listDataLakeOrganizacion = await servicio.BuscarPalabraAsync(buscarRequest.TextoBuscar);
                 } catch (Exception) { }
            }
            if (listDataLakeOrganizacion is null) {
                listDataLakeOrganizacion = new List<DataLakeOrganizacion>();
            }

            return await Task.FromResult(request.ApplyTo(listDataLakeOrganizacion));
        }
    }
}