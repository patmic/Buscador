using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using System.Text;
using XSystem.Security.Cryptography;
using WebApp.Service;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApp.Service.IService;

namespace WebApp.Repositories
{
    public class UsuarioRepository(SqlServerDbContext dbContext, IJwtService jwtService) : IUsuarioRepository
    {
        private readonly SqlServerDbContext _bd = dbContext;
        private readonly IJwtService _jwtService = jwtService;

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
            var usuariobd = _bd.Usuario.AsNoTracking().FirstOrDefault(u => u.Email == email);
            if (usuariobd == null)
            {
                return true;
            }

            return false;
        }

        public bool create(Usuario usuario)
        {
            var passwordEncriptado = obtenermd5(usuario.Clave);
            usuario.Clave = passwordEncriptado;

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
                usuarioExistente.Clave = obtenermd5(usuario.Clave);;
            }

            _bd.Usuario.Update(usuarioExistente);
            return _bd.SaveChanges() >= 0 ? true : false;
        }
        public async Task<object> login(UsuarioLoginDto usuarioLoginDto)
        {
            var passwordEncriptado = obtenermd5(usuarioLoginDto.Clave);

            var usuario = _bd.Usuario.AsNoTracking().FirstOrDefault(
                u => u.Email.ToLower() == usuarioLoginDto.Email.ToLower()
                && u.Clave == passwordEncriptado
                );

            if (usuario == null)
            {
                return null;
            }

            return new {
                Token = _jwtService.GenerateJwtToken(usuario.IdUsuario),
                Usuario = new
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
        private string obtenermd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }

        public async Task<object> Recuperar(UsuarioDto usuarioDto)
        {
            var usuario = _bd.Usuario.AsNoTracking().FirstOrDefault(u => u.Email.ToLower() == usuarioDto.Email.ToLower());

            if (usuario == null)
            {
                return null;
            }

            return new { success = true };
        }
    }
}
