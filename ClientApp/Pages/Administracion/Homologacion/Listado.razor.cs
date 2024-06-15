using BlazorBootstrap;
using ClientApp.Models;
using SharedApp.Models.Dtos;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace ClientApp.Pages.Administracion.Homologacion
{
    public partial class Listado
    {
        [Inject]
        public IVwHomologacionRepository vwHomologacionRepository { get; set; }
        [Inject]
        private IHomologacionRepository? homologacionRepository { get; set; }
        [Inject]
        private IHomologacionEsquemaRepository homologacionEsquemaRepository { get; set; }
        [Inject]
        private IBusquedaService servicio { get; set; }
        [Inject]
        private IVistaService vistaService { get; set; }
        private Grid<VwHomologacion> grid;
        private List<VwHomologacion>? listaOrganizaciones = new List<VwHomologacion>();
        private List<HomologacionEsquema>? listaHomologacionEsquemas = new List<HomologacionEsquema>();
        private List<VwHomologacion>? listaHomologacions = new List<VwHomologacion>();
        private List<VistaDto>? listaVistas = new List<VistaDto>();
        private List<string> propiedadesVista = new List<string>();
        private HomologacionEsquema esquemaSelected;
        private VwHomologacion organizacionSelected;
        private VistaDto vistaSelected;
        protected override async Task OnInitializedAsync()
        {
            listaOrganizaciones = await vwHomologacionRepository.GetHomologacionDetalleAsync("filtro_detalles", 3);
            listaHomologacionEsquemas = await homologacionEsquemaRepository.GetHomologacionEsquemasAsync();
        }
        private async Task CambiarSeleccionOrganizacion(VwHomologacion _organizacionSelected)
        {
            organizacionSelected = _organizacionSelected;
            vistaSelected = null;
            listaVistas = await vistaService.GetFindBySystemAsync(organizacionSelected.IdHomologacion);

            await Task.CompletedTask;
        }
        private async void CambiarSeleccionEsquema(HomologacionEsquema _esquemaSelected)
        {
            esquemaSelected = _esquemaSelected;
            await grid.RefreshDataAsync();
        }
        private async Task CambiarSeleccionVista(VistaDto _vistaSelected)
        {
            vistaSelected = _vistaSelected;
            propiedadesVista = await vistaService.GetPropertiesAsync(vistaSelected.VistaNombre);

            await Task.CompletedTask;
        }
        private async Task<GridDataProviderResult<VwHomologacion>> HomologacionDataProvider(GridDataProviderRequest<VwHomologacion> request)
        {
            var Columnas = new List<VwHomologacion>();

            if (esquemaSelected != null) {
                var homologacionEsquema = await servicio.FnHomologacionEsquemaAsync(esquemaSelected.IdHomologacionEsquema);
                Columnas = JsonConvert.DeserializeObject<List<VwHomologacion>>(homologacionEsquema.EsquemaJson).OrderBy(c => c.MostrarWebOrden).ToList();
            }

            return await Task.FromResult(request.ApplyTo(Columnas));
        }
    }
}