using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApp.Controllers
{
    [Route("api/permiso")]
    [ApiController]
    public class UsuarioEndpointPermisoController : ControllerBase
    {
        private readonly IUsuarioEndpointPermisoRepository _usRepo;
        protected RespuestasAPI _respuestaApi;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuarioEndpointPermisoController> _logger;

        public UsuarioEndpointPermisoController(ILogger<UsuarioEndpointPermisoController> logger, IUsuarioEndpointPermisoRepository usRepo, IMapper mapper)
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
        public async Task<IActionResult> Registro([FromBody] UsuarioEndpointPermisoRegistroDto UsuarioEndpointPermisoRegistroDto)
        {
            try
            {
                var usuarioEndpointPermisoRegistro = await _usRepo.Registro(UsuarioEndpointPermisoRegistroDto);
                if (usuarioEndpointPermisoRegistro == null)
                {
                    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                    _respuestaApi.IsSuccess = false;
                    _respuestaApi.ErrorMessages.Add("Error en el registro");
                    return BadRequest(_respuestaApi);
                }

                _respuestaApi.StatusCode = HttpStatusCode.OK;
                _respuestaApi.IsSuccess = true;
                _respuestaApi.Result = _mapper.Map<UsuarioEndpointPermisoDto>(usuarioEndpointPermisoRegistro);
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

        // [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsuarioEndpointPermisos()
        {
            try
            {
                var listUsuarioEndpointPermiso = _usRepo.GetUsuarioEndpointPermisos();

                var listUsuarioEndpointPermisoDto = new List<UsuarioEndpointPermisoDto>();

                foreach (var lista in listUsuarioEndpointPermiso)
                {
                    listUsuarioEndpointPermisoDto.Add(_mapper.Map<UsuarioEndpointPermisoDto>(lista));
                }
                return Ok(listUsuarioEndpointPermisoDto);
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

        // [Authorize]
        [HttpGet("{usuarioEndpointPermisoId:int}", Name = "GetUsuarioEndpointPermiso")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsuarioEndpointPermiso(int usuarioEndpointPermisoId)
        {
            try
            {
                var itemUsuarioEndpointPermiso = _usRepo.GetUsuarioEndpointPermiso(usuarioEndpointPermisoId);

                if (itemUsuarioEndpointPermiso == null)
                {
                    return NotFound();
                }

                var itemUsuarioEndpointPermisoDto = _mapper.Map<UsuarioEndpointPermisoDto>(itemUsuarioEndpointPermiso);
                return Ok(itemUsuarioEndpointPermisoDto);
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
    }
}
