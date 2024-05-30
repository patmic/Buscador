using ClientApp.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace ClientApp.Pages.BuscadorCan
{
    public partial class EsquemaModalGrillaTab
    {
        [Parameter] public HomologacionEsquema homologacionEsquema { get; set; }
        private IEnumerable<VwHomologacion> Columnas = new List<VwHomologacion>();
        private IEnumerable<VwHomologacion> resultados = new List<VwHomologacion>();
        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() =>
            {
                Columnas = JsonConvert.DeserializeObject<List<VwHomologacion>>(homologacionEsquema.EsquemaJson).OrderBy(c => c.MostrarWebOrden).ToList();
            });
        }
    }
}