using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SharedApp.Models;

namespace WebApp.Controllers
{
    [Route("api/endpoint")]
    [ApiController]
    public class EndpointController : ControllerBase
    {
        private readonly IEndpointRepository _usRepo;
        protected RespuestasAPI _respuestaApi;
        private readonly IMapper _mapper;
        private readonly ILogger<EndpointController> _logger;

        public EndpointController(ILogger<EndpointController> logger, IEndpointRepository usRepo, IMapper mapper)
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
        public async Task<IActionResult> Registro([FromBody] EndpointRegistroDto endpointRegistroDto)
        {
            try
            {
                bool validarUnicos = _usRepo.IsUniqueUser(endpointRegistroDto.Nombre, endpointRegistroDto.Url);
                if (!validarUnicos)
                {
                    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                    _respuestaApi.IsSuccess = false;
                    _respuestaApi.ErrorMessages.Add("El nombre de endpoint y url ya existe");
                    return BadRequest(_respuestaApi);
                }

                var endpoint = await _usRepo.Registro(endpointRegistroDto);
                if (endpoint == null)
                {
                    _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                    _respuestaApi.IsSuccess = false;
                    _respuestaApi.ErrorMessages.Add("Error en el registro");
                    return BadRequest(_respuestaApi);
                }

                _respuestaApi.StatusCode = HttpStatusCode.OK;
                _respuestaApi.IsSuccess = true;
                _respuestaApi.Result = _mapper.Map<EndpointDto>(endpoint);
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

        //Métodos o endpoints de la API por si el estudiante quiere usarlos,
        //No se usaran en el consume usando blazor web assembly
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetEndpoints()
        {
            try
            {
                var listaEndpoint = _usRepo.GetEndpoints();

                var listaEndpointDto = new List<EndpointDto>();

                foreach (var lista in listaEndpoint)
                {
                    listaEndpointDto.Add(_mapper.Map<EndpointDto>(lista));
                }
                return Ok(listaEndpointDto);
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
        [HttpGet("{endpointId:int}", Name = "GetEndpoint")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEndpoint(int endpointId)
        {
            try
            {
                var itemEndpoint = _usRepo.GetEndpoint(endpointId);

                if (itemEndpoint == null)
                {
                    return NotFound();
                }

                var itemEndpointDto = _mapper.Map<EndpointDto>(itemEndpoint);
                return Ok(itemEndpointDto);
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
