using Blazored.LocalStorage;
using ClientApp.Helpers;
using ClientApp.Models;
using ClientApp.Services.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;


namespace ClientApp.Services
{
    public class ServiceAutenticacion : IServiceAutenticacion
    {
        private readonly HttpClient _cliente;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _estadoProveedorAutenticacion;

        public ServiceAutenticacion(HttpClient cliente, 
            ILocalStorageService localStorage,
            AuthenticationStateProvider estadoProveedorAutenticacion)
        {
            _cliente = cliente;
            _localStorage = localStorage;
            _estadoProveedorAutenticacion = estadoProveedorAutenticacion;
        }
        public async Task<RespuestaAutenticacion> Acceder(UsuarioAutenticacion usuarioDesdeAutenticacion)
        {
            var content = JsonConvert.SerializeObject(usuarioDesdeAutenticacion);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _cliente.PostAsync($"{Inicializar.UrlBaseApi}api/usuarios/login", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var resultado = (JObject)JsonConvert.DeserializeObject(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                var Token = resultado["token"].Value<string>();
                var Usuario = resultado["usuario"]["email"].Value<string>();
                var Rol = resultado["usuario"]["rol"].Value<string>();

                await _localStorage.SetItemAsync(Inicializar.Token_Local, Token);
                await _localStorage.SetItemAsync(Inicializar.Datos_Usuario_Local, Usuario);
                await _localStorage.SetItemAsync(Inicializar.Datos_Usuario_Rol_Local, Rol);
                ((AuthStateProvider)_estadoProveedorAutenticacion).NotificarUsuarioLogueado(Token);
                _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);
                return new RespuestaAutenticacion { IsSuccess = true };
            }
            else
            {
                if (resultado["errorMessages"] is JArray errorMessagesArray)
                {
                    var errorMessagesList = errorMessagesArray.ToObject<string[]>();
                    var errorMessages = string.Join(", ", errorMessagesList);

                    return new RespuestaAutenticacion { IsSuccess = false, ErrorMessages = errorMessages };
                }
                else
                {
                    return new RespuestaAutenticacion { IsSuccess = false, ErrorMessages = "An unexpected error occurred." };
                }
            }
        }
        public async Task<RespuestaAutenticacion> Recuperar(UsuarioRecuperacion usuarioRecuperacion) {
            var content = JsonConvert.SerializeObject(usuarioRecuperacion);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _cliente.PostAsync($"{Inicializar.UrlBaseApi}api/usuarios/recuperar", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var resultado = (JObject)JsonConvert.DeserializeObject(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new RespuestaAutenticacion { IsSuccess = true };
            }
            else
            {
                if (resultado["errorMessages"] is JArray errorMessagesArray)
                {
                    var errorMessagesList = errorMessagesArray.ToObject<string[]>();
                    var errorMessages = string.Join(", ", errorMessagesList);

                    return new RespuestaAutenticacion { IsSuccess = false, ErrorMessages = errorMessages };
                }
                else
                {
                    return new RespuestaAutenticacion { IsSuccess = false, ErrorMessages = "An unexpected error occurred." };
                }
            }
        }

        public async Task Salir()
        {
            await _localStorage.RemoveItemAsync(Inicializar.Token_Local);
            await _localStorage.RemoveItemAsync(Inicializar.Datos_Usuario_Local);
            ((AuthStateProvider)_estadoProveedorAutenticacion).NotificarUsuarioSalir();
            _cliente.DefaultRequestHeaders.Authorization = null;
        }   
    }
}
