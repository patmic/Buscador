using BlazorBootstrap;
using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace ClientApp.Pages.Administracion.Esquemas
{
    public partial class Formulario
    {
        private string homologacionName;
        private Button saveButton = default!;
        public event Action DataLoaded;
        private HomologacionEsquema homologacionEsquema = new HomologacionEsquema();
        [Inject]
        public IHomologacionEsquemaRepository homologacionEsquemaRepository { get; set; }
        [Inject]
        public IHomologacionRepository iHomologacionRepository { get; set; }
        [Inject]
        public IVwHomologacionRepository vwHomologacionRepository { get; set; }
        private List<VwHomologacion>? listaVwHomologacion;

        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Parameter]
        public int? Id { get; set; }
        [Inject]
        public Services.ToastService toastService { get; set; }
        private IEnumerable<VwHomologacion> lista = new List<VwHomologacion>();
        protected override async Task OnInitializedAsync()
        {
            listaVwHomologacion = await vwHomologacionRepository.GetHomologacionAsync("dimension");

            if (Id > 0) {
                homologacionEsquema = await homologacionEsquemaRepository.GetHomologacionEsquemaAsync(Id.Value);

                lista = JsonConvert.DeserializeObject<List<VwHomologacion>>(homologacionEsquema.EsquemaJson);
            } else {
                homologacionEsquema.EsquemaJson = "{}";
            }

            DataLoaded += async () => {
                if (!(lista is null)) {
                    await Task.Delay(2000);
                    await JSRuntime.InvokeVoidAsync("initSortable", DotNetObjectReference.Create(this));
                }
            };

            DataLoaded?.Invoke();
        }
        private async Task GuardarHomologacionEsquema()
        {
            saveButton.ShowLoading("Guardando...");

            homologacionEsquema.EsquemaJson = JsonConvert.SerializeObject(lista);

            var result = await homologacionEsquemaRepository.RegistrarOActualizar(homologacionEsquema);
            if (result.registroCorrecto)
            {
                // guardar las nuevas posiciones
                foreach (var n in lista) {
                  await iHomologacionRepository.RegistrarOActualizar(new VwHomologacion() {
                    IdHomologacion = n.IdHomologacion,
                    MostrarWebOrden = n.MostrarWebOrden
                  });
                }

                toastService.CreateToastMessage(ToastType.Success, "Registrado exitosamente");
                navigationManager.NavigateTo("/esquemas");
            }
            else
            {
                toastService.CreateToastMessage(ToastType.Danger, "Debe llenar todos los campos");
            }

            saveButton.HideLoading();
        }
        private void EliminarElemento(int elemento)
        {
            lista = lista.Where(c => c.IdHomologacion != elemento).ToList();
        }
        [JSInvokable]
        public async Task OnDragEnd(string[] sortedIds)
        {
            var tempList = new List<VwHomologacion>();
            for (int i = 0; i < sortedIds.Length; i += 1)
            {
                VwHomologacion homo = lista.FirstOrDefault(h => h.IdHomologacion == Int32.Parse(sortedIds[i]));
                homo.MostrarWebOrden = i + 1;
                tempList.Add(homo);
            }
            lista = tempList;
            await Task.CompletedTask;
        }
        private async Task<AutoCompleteDataProviderResult<VwHomologacion>> VwHomologacionDataProvider(AutoCompleteDataProviderRequest<VwHomologacion> request)
        {
            if (listaVwHomologacion is null)
                listaVwHomologacion = await vwHomologacionRepository.GetHomologacionAsync("dimension");

            return await Task.FromResult(request.ApplyTo(listaVwHomologacion.OrderBy(vmH => vmH.MostrarWebOrden)));
        }
        private void OnAutoCompleteChanged(VwHomologacion _vwHomologacionSelected)
        {
            _vwHomologacionSelected.MostrarWebOrden = lista.Count();
            lista = lista.Append(_vwHomologacionSelected).ToList();
        }
    }
}
