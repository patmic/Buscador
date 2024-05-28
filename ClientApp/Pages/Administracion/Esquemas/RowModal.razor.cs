using ClientApp.Models;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Pages.Administracion.Esquemas
{
    public partial class RowModal
    {
        [Parameter] public List<VwHomologacion> columnas { get; set; }
        [Parameter] public List<VwHomologacion> listaVwHomologacion { get; set;}
    }
}