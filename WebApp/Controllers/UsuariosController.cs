using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApp.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usRepo;
        protected RespuestasAPI _respuestaApi;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(ILogger<UsuariosController> logger, IUsuarioRepository usRepo, IMapper mapper)
        {
            _usRepo = usRepo;
            _mapper = mapper;
            _logger = logger;
            this._respuestaApi = new();
        }

        // [Authorize]
        [HttpPost("registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registro([FromBody] UsuarioRegistroDto usuarioRegistroDto)
        {
            try
            {
                bool validarEmailUnico = _usRepo.IsUniqueUser(usuarioRegistroDto.Email);
                if (!validarEmailUnico)
                {
                    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                    _respuestaApi.IsSuccess = false;
                    _respuestaApi.ErrorMessages.Add("El nombre de usuario ya existe");
                    return BadRequest(_respuestaApi);
                }

                var usuario = await _usRepo.Registro(usuarioRegistroDto);
                if (usuario == null)
                {
                    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                    _respuestaApi.IsSuccess = false;
                    _respuestaApi.ErrorMessages.Add("Error en el registro");
                    return BadRequest(_respuestaApi);
                }

                _respuestaApi.StatusCode = HttpStatusCode.OK;
                _respuestaApi.IsSuccess = true;
                _respuestaApi.Result = _mapper.Map<UsuarioDto>(usuario);
                return Ok(_respuestaApi);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new {
                            statusCode = 500,
                            message = e.Message
                    });
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {          
            try
            {
                var respuestaLogin = await _usRepo.Login(usuarioLoginDto);
                if (respuestaLogin.Usuario == null || string.IsNullOrEmpty(respuestaLogin.Token))
                {
                    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                    _respuestaApi.IsSuccess = false;
                    _respuestaApi.ErrorMessages.Add("El nombre de usuario o password son incorrectos");
                    return BadRequest(_respuestaApi);
                }

                _respuestaApi.StatusCode = HttpStatusCode.OK;
                _respuestaApi.IsSuccess = true;
                _respuestaApi.Result = respuestaLogin;
                return Ok(_respuestaApi);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new {
                            statusCode = 500,
                            message = e.Message
                    });
            }
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsuarios()
        {
            try
            {
                var listaUsuarios = _usRepo.GetUsuarios();

                var listaUsuariosDto = new List<UsuarioDto>();

                foreach (var lista in listaUsuarios)
                {
                    listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(lista));
                }
                return Ok(listaUsuariosDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new {
                            statusCode = 500,
                            message = e.Message
                    });
            }
        }

        [Authorize]
        [HttpGet("{usuarioId:int}", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsuario(int usuarioId)
        {
            try
            {
                var itemUsuario = _usRepo.GetUsuario(usuarioId);

                if (itemUsuario == null)
                {
                    return NotFound();
                }

                var itemUsuarioDto = _mapper.Map<UsuarioDto>(itemUsuario);
                return Ok(itemUsuarioDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new {
                            statusCode = 500,
                            message = e.Message
                    });
            }
        }

        [Authorize]
        [HttpPatch("{usuarioId:int}", Name = "ActualizarPatchUsuario")]
        [ProducesResponseType(201, Type = typeof(UsuarioActualizarDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarPatchUsuario(int usuarioId, [FromBody] UsuarioActualizarDto usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (usuarioDto == null || usuarioId != usuarioDto.IdUsuario)
            {
                return BadRequest(ModelState);
            }

            var usr = _mapper.Map<Usuario>(usuarioDto);

            if (!_usRepo.ActualizarUsuario(usr))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro{usr.Email}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
