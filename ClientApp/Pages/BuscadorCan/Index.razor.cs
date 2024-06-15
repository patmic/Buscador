using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Pages.BuscadorCan
{
    public partial class Index
    {
        private IndexGrilla childComponentRef;
        [Inject]
        public IVwHomologacionRepository vwHomologacionRepository { get; set; }
        private List<VwHomologacion>? listaEtiquetasFiltros;
        private List<List<VwHomologacion>>? listadeOpciones = new List<List<VwHomologacion>>();
        private List<List<int>> selectedValues = new List<List<int>>();
        private BuscarRequest buscarRequest = new BuscarRequest();
        protected override async Task OnInitializedAsync()
        {
            try
            {
                listaEtiquetasFiltros = await vwHomologacionRepository.GetHomologacionAsync("etiquetas_filtro");

                foreach(var opciones in listaEtiquetasFiltros)
                {
                    listadeOpciones.Add(await vwHomologacionRepository.GetHomologacionDetalleAsync("filtro_detalles", opciones.IdHomologacion));
                }
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
            await childComponentRef.grid.ResetPageNumber();
        }
    }
}