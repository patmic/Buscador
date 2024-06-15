using BlazorBootstrap;
using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace ClientApp.Pages.BuscadorCan
{
    public partial class EsquemaModalGrillaTab
    {
        [Parameter]
        public int IdHomologacionEsquema { get; set; }
        [Parameter]
        public int IdDataLakeOrganizacion { get; set; }
        [Inject]
        private IBusquedaService servicio { get; set; }
        private HomologacionEsquema homologacionEsquema;
        private IEnumerable<VwHomologacion> Columnas;
        private IEnumerable<DataHomologacionEsquema> resultados;
        protected override async Task OnInitializedAsync()
        {
            homologacionEsquema = await servicio.FnHomologacionEsquemaAsync(IdHomologacionEsquema);
            Columnas = JsonConvert.DeserializeObject<List<VwHomologacion>>(homologacionEsquema.EsquemaJson).OrderBy(c => c.MostrarWebOrden).ToList();
        }
        private async Task<GridDataProviderResult<DataHomologacionEsquema>> HomologacionEsquemasDataProvider(GridDataProviderRequest<DataHomologacionEsquema> request)
        {
            if (resultados is null)
                resultados = await servicio.FnHomologacionEsquemaDatoAsync(IdHomologacionEsquema, IdDataLakeOrganizacion);

            return await Task.FromResult(request.ApplyTo(resultados));
        }
    }
}