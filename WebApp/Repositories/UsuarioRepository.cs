using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;
using WebApp.Service;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace WebApp.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SqlServerDbContext _bd;
        private readonly IConfiguration _configuration;

        public UsuarioRepository(SqlServerDbContext dbContext, IConfiguration configuration)
        {
            _bd = dbContext;
            _configuration = configuration;
        }

        public Usuario GetUsuario(int usuarioId)
        {
            return _bd.Usuario.AsNoTracking().FirstOrDefault(c => c.IdUsuario == usuarioId);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _bd.Usuario.AsNoTracking().OrderBy(c => c.IdUsuario).ToList();
        }

        public bool IsUniqueUser(string email)
        {
            var usuariobd = _bd.Usuario.AsNoTracking().FirstOrDefault(u => u.Email == email);
            if (usuariobd == null)
            {
                return true;
            }

            return false;
        }

        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var passwordEncriptado = obtenermd5(usuarioLoginDto.Clave);

            var usuario = _bd.Usuario.AsNoTracking().FirstOrDefault(
                u => u.Email.ToLower() == usuarioLoginDto.Email.ToLower()
                && u.Clave == passwordEncriptado
                );

            if (usuario == null)
            {
                return new UsuarioLoginRespuestaDto()
                {
                    Token = "",
                    Usuario = null
                };
            }
   
            var claveSecreta = _configuration.GetValue<string>("ApiSettings:Secreta");
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Email.ToString()),
                }),
                Expires= DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadorToken.CreateToken(tokenDescriptor);

            UsuarioLoginRespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = new Usuario
                {
                    IdUsuario = usuario.IdUsuario,
                    Email = usuario.Email,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Telefono = usuario.Telefono,
                    Rol = usuario.Rol
                }
            };

            return usuarioLoginRespuestaDto;
        }

        public async Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            
            var passwordEncriptado = obtenermd5(usuarioRegistroDto.Clave);

            Usuario usuario = new Usuario()
            {
                Nombre = usuarioRegistroDto.Nombre,
                Apellido = usuarioRegistroDto.Apellido,
                Email = usuarioRegistroDto.Email,
                Clave = usuarioRegistroDto.Clave,
                Telefono = usuarioRegistroDto.Telefono,
                Rol = usuarioRegistroDto.Rol,
                FechaCrea = DateTime.Now,
                FechaModifica = DateTime.Now
            };

            _bd.Usuario.Add(usuario);
            usuario.Clave = passwordEncriptado;
            await _bd.SaveChangesAsync();
            return usuario;
           
        }

        public static string obtenermd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            var usuarioExistente = _bd.Usuario.AsNoTracking().FirstOrDefault(u => u.IdUsuario == usuario.IdUsuario);

            usuario.FechaModifica = DateTime.Now;

            PropertyInfo[] propiedades = typeof(Usuario).GetProperties();

            foreach (PropertyInfo propiedad in propiedades)
            {
                // Obtenemos el valor de la propiedad en el objeto modificado
                object valorModificado = propiedad.GetValue(usuario);

                // Obtenemos el valor de la propiedad en el registro existente
                object valorExistente = propiedad.GetValue(usuarioExistente);

                // Verificamos si los valores son diferentes y actualizamos si es necesario
                if (valorModificado != null && !object.Equals(valorModificado, valorExistente))
                {
                    propiedad.SetValue(usuarioExistente, valorModificado);
                }
            }

            _bd.Usuario.Update(usuarioExistente);

            if (usuario.Clave != null && usuario.Clave != "") {
                usuarioExistente.Clave = obtenermd5(usuario.Clave);;
            }

            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
