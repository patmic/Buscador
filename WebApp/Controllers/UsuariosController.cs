using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class UsuariosController(ILogger<UsuariosController> logger, IUsuarioRepository vhRepo, IMapper mapper) : ControllerBase
    {
        private readonly IUsuarioRepository _vhRepo = vhRepo;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<UsuariosController> _logger = logger;
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {          
            try
            {
                var usuarioLogin = await _vhRepo.login(usuarioLoginDto);
                if (usuarioLogin == null)
                {
                    return BadRequest(new {
                        ErrorMessages = new List<string> { "El nombre de usuario o password son incorrectos" }
                    });
                }

                return Ok(usuarioLogin);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error en {nameof(Login)}");
                return StatusCode(500, "Error en el servidor");
            }
        }
        [Authorize]
        [HttpPost("registro")]
        public IActionResult Registro([FromBody] UsuarioDto dto)
        {
            try
            {
                bool validarEmailUnico = _vhRepo.isUniqueUser(dto.Email);
                if (!validarEmailUnico)
                {
                    return BadRequest(new {
                        ErrorMessages = new List<string> { "El nombre de usuario ya existe" }
                    });
                }

                _vhRepo.create(_mapper.Map<Usuario>(dto));

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error en {nameof(Registro)}");
                return StatusCode(500, "Error en el servidor");
            }
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            try
            {
                var records = _vhRepo.findAll();
                var dtos = records.Select(item => _mapper.Map<UsuarioDto>(item)).ToList();
                return Ok(dtos);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error en {nameof(GetUsuarios)}");
                return StatusCode(500, "Error en el servidor");
            }
        }
        [Authorize]
        [HttpGet("{usuarioId:int}", Name = "GetUsuario")]
        public IActionResult GetUsuario(int usuarioId)
        {
            try
            {
                var itemUsuario = _vhRepo.find(usuarioId);

                if (itemUsuario == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<UsuarioDto>(itemUsuario));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error en {nameof(GetUsuario)}");
                return StatusCode(500, "Error en el servidor");
            }
        }
        [Authorize]
        [HttpPut("{Id:int}", Name = "ActualizarUsuario")]
        public IActionResult ActualizarUsuario(int Id, [FromBody] UsuarioDto dto)
        {
            try
            {
                dto.IdUsuario = Id;
                var usuario = _mapper.Map<Usuario>(dto);
                _vhRepo.update(usuario);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error en {nameof(ActualizarUsuario)}");
                return StatusCode(500, "Error en el servidor");
            }
        }

        [Authorize]
        [HttpDelete("{Id:int}", Name = "DesactivarUsuario")]
        public IActionResult DesactivarUsuario(int Id)
        {
            try {
                var usuario = _vhRepo.find(Id);

                if (usuario == null) {
                    return NotFound();
                }

                usuario.Estado = "X";
                _vhRepo.update(usuario);

                return Ok("Eliminado correctamente");
            }
            catch (Exception e) {
                _logger.LogError(e, $"Error en {nameof(DesactivarUsuario)}");
                return StatusCode(500, "Error en el servidor");
            }
        }
        [HttpPost("recuperar")]
        public async Task<IActionResult> Recuperar([FromBody] UsuarioDto usuarioDto)
        {          
            try
            {
                var usuarioRecuperar = await _vhRepo.Recuperar(usuarioDto);
                if (usuarioRecuperar == null)
                {
                    return BadRequest(new {
                        ErrorMessages = new List<string> { "El nombre de usuario incorrecto" }
                    });
                }

                return Ok(usuarioRecuperar);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error en {nameof(Recuperar)}");
                return StatusCode(500, "Error en el servidor");
            }
        }
    }
}
