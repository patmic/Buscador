using WebApp.Repositories.IRepositories;
using WebApp.Service;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApp.Service.IService;
using SharedApp.Models;
using SharedApp.Models.Dtos;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class UsuarioRepository(
        SqlServerDbContext dbContext,
        IJwtService jwtService,
        IEmailService emailService,
        IMd5Service md5Service,
        IPasswordService passwordService,
        ILogger<UsuarioRepository> logger
    ) : IUsuarioRepository
    {
        private readonly SqlServerDbContext _bd = dbContext;
        private readonly IJwtService _jwtService = jwtService;
        private readonly IEmailService _emailService = emailService;
        private readonly IMd5Service _md5Service = md5Service;
        private readonly IPasswordService _passwordService = passwordService;
        private readonly ILogger<UsuarioRepository> _logger = logger;

        public Usuario find(int usuarioId)
        {
            return _bd.Usuario.AsNoTracking().FirstOrDefault(c => c.IdUsuario == usuarioId);
        }

        public ICollection<Usuario> findAll()
        {
            return _bd.Usuario.AsNoTracking().OrderBy(c => c.IdUsuario).ToList();
        }

        public bool isUniqueUser(string email)
        {
            return _bd.Usuario.AsNoTracking().FirstOrDefault(u => u.Email == email) == null;
        }

        public bool create(Usuario usuario)
        {
            usuario.Clave = _md5Service.GenerateMd5(usuario.Clave);
            usuario.IdUserCreacion = _jwtService.GetUserIdFromToken(_jwtService.GetTokenFromHeader());
            usuario.IdUserModifica = usuario.IdUserCreacion;

            _bd.Usuario.Add(usuario);
            return _bd.SaveChanges() >= 0 ? true : false;
        }
        public bool update(Usuario usuario)
        {
            var usuarioExistente = _bd.Usuario.AsNoTracking().FirstOrDefault(u => u.IdUsuario == usuario.IdUsuario);

            usuarioExistente.FechaModifica = DateTime.Now;
            usuarioExistente.IdUserModifica = _jwtService.GetUserIdFromToken(_jwtService.GetTokenFromHeader());

            PropertyInfo[] propiedades = typeof(Usuario).GetProperties();

            foreach (PropertyInfo propiedad in propiedades)
            {
                object valorModificado = propiedad.GetValue(usuario);
                object valorExistente = propiedad.GetValue(usuarioExistente);

                if (valorModificado != null && !object.Equals(valorModificado, valorExistente))
                {
                    propiedad.SetValue(usuarioExistente, valorModificado);
                }
            }

            if (!string.IsNullOrWhiteSpace(usuario.Clave))
            {
                usuarioExistente.Clave = _md5Service.GenerateMd5(usuario.Clave);;
            }

            _bd.Usuario.Update(usuarioExistente);
            return _bd.SaveChanges() >= 0 ? true : false;
        }
        public async Task<UsuarioAutenticacionRespuestaDto> login(UsuarioAutenticacionDto usuarioAutenticacionDto)
        {
            try {
                var passwordEncriptado = _md5Service.GenerateMd5(usuarioAutenticacionDto.Clave);
                var usuario = _bd.Usuario.AsNoTracking().FirstOrDefault(u => 
                    u.Email.ToLower() == usuarioAutenticacionDto.Email.ToLower() &&
                    u.Clave == passwordEncriptado
                );

                if (usuario == null)
                {
                    return new UsuarioAutenticacionRespuestaDto();
                }

                return new UsuarioAutenticacionRespuestaDto {
                    Token = _jwtService.GenerateJwtToken(usuario.IdUsuario),
                    Usuario = new UsuarioDto
                    {
                        IdUsuario = usuario.IdUsuario,
                        Email = usuario.Email,
                        Nombre = usuario.Nombre,
                        Apellido = usuario.Apellido,
                        Telefono = usuario.Telefono,
                        Rol = usuario.Rol,
                    }
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error en {nameof(login)}");
                return new UsuarioAutenticacionRespuestaDto();
                // return StatusCode(500, new {
                //     ErrorMessages = new List<string> { "Error en el servidor" }
                // });
            }
        }
        public async Task<bool> Recuperar(UsuarioRecuperacionDto usuarioRecuperacionDto)
        {
            var usuario = _bd.Usuario.AsNoTracking().FirstOrDefault(u => u.Email.ToLower() == usuarioRecuperacionDto.Email.ToLower());

            if (usuario == null)
            {
                return false;
            }

            string clave = _passwordService.GenerateTemporaryPassword(8);
            usuario.Clave = _md5Service.GenerateMd5(clave);

            await _emailService.EnviarCorreoAsync(usuario.Email, "Envío de Clave Temporal", $"Su nueva clave temporal es: {clave}");

            _bd.Usuario.Update(usuario);

            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}