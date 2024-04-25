using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;
using WebApp.Service;
using DataAccess.Service.IService;

namespace WebApp.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbContextSqlServer _bd;
        private string claveSecreta;

        public UsuarioRepository(IDatabaseService databaseService)
        {
            string connectionString = databaseService.GetConnectionString("ConexionSql");
            _bd = new DbContextSqlServer(connectionString);
            claveSecreta = databaseService.GetValueString("ApiSettings:Secreta");
        }

        public Usuario GetUsuario(int usuarioId)
        {
            return _bd.Usuario.FirstOrDefault(c => c.IdUsuario == usuarioId);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _bd.Usuario.OrderBy(c => c.IdUsuario).ToList();
        }

        public bool IsUniqueUser(string email)
        {
            var usuariobd = _bd.Usuario.FirstOrDefault(u => u.Email == email);
            if (usuariobd == null)
            {
                return true;
            }

            return false;
        }

        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var passwordEncriptado = obtenermd5(usuarioLoginDto.Clave);

            var usuario = _bd.Usuario.FirstOrDefault(
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
                Email = usuarioRegistroDto.Email,
                Clave = usuarioRegistroDto.Clave,
                Rol = usuarioRegistroDto.Rol,
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
    }
}
