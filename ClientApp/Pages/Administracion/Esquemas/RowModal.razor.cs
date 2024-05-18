using ClientApp.Models;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Pages.Administracion.Esquemas
{
    public partial class RowModal
    {
        [Parameter] public List<HomologacionEsquemaColumnas> columnas { get; set; }
    }
}