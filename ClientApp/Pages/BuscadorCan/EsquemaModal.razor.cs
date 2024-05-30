using BlazorBootstrap;
using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Pages.BuscadorCan
{
    public partial class EsquemaModal
    {
        Tabs tabs = default!;
        [Parameter] public DataLakeOrganizacion dataLake { get; set; }
        [Inject]
        private IBusquedaRepository servicio { get; set; }
        private List<HomologacionEsquema>? listaEsquemas;
        protected override async Task OnInitializedAsync()
        {
            listaEsquemas = await servicio.ObtenerEsquemasRelacionados(dataLake.IdDataLakeOrganizacion);
        }
    }
}