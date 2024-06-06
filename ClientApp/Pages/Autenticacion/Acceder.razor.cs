﻿using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components;
using System.Web;
using BlazorBootstrap;

namespace ClientApp.Pages.Autenticacion
{
    public partial class Acceder
    {
        private Button saveButton = default!;
        private UsuarioAutenticacion usuarioAutenticacion = new UsuarioAutenticacion();

        public string UrlRetorno { get; set; }

        public string alertMessage { get; set; }

        [Inject]
        public IServiceAutenticacion servicioAutenticacion { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task AccesoUsuario()
        {
            saveButton.ShowLoading("Verificando...");
            var result = await servicioAutenticacion.Acceder(usuarioAutenticacion);
            if (result.IsSuccess)
            {
                var urlAbsoluta = new Uri(navigationManager.Uri);
                var parametrosQuery = HttpUtility.ParseQueryString(urlAbsoluta.Query);
                UrlRetorno = parametrosQuery["returnUrl"];
                if (string.IsNullOrEmpty(UrlRetorno))
                {
                    navigationManager.NavigateTo("/administracion");
                }
                else
                {
                    navigationManager.NavigateTo("/" + UrlRetorno);
                }
            }
            else
            {
                alertMessage = result.ErrorMessages;
            }
            saveButton.HideLoading();

            await Task.CompletedTask;
        }
    }
}
