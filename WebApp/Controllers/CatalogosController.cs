using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/catalogos")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private readonly IVwHomologacionRepository _vhRepo;
        protected RespuestasAPI _respuestaApi;
        private readonly IMapper _mapper;
        private readonly ILogger<CatalogosController> _logger;

        public CatalogosController(ILogger<CatalogosController> logger, IVwHomologacionRepository vhRepo, IMapper mapper)
        {
            _vhRepo = vhRepo;
            _mapper = mapper;
            _logger = logger;
        }

        // [Authorize]
        [HttpGet("pais")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObtenerPais()
        {
            try
            {
                var listaPais = _vhRepo.ObtenerPais();

                var listaCatalogoDto = new List<CatalogosDto>();

                foreach (var lista in listaPais)
                {
                    listaCatalogoDto.Add(_mapper.Map<CatalogosDto>(lista));
                }
                return Ok(listaCatalogoDto);
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

        [HttpGet("razon_social")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObtenerRazonSocial()
        {
            try
            {
                var listado = _vhRepo.ObtenerRazonSocial();

                var listaCatalogoDto = new List<CatalogosDto>();

                foreach (var lista in listado)
                {
                    listaCatalogoDto.Add(_mapper.Map<CatalogosDto>(lista));
                }
                return Ok(listaCatalogoDto);
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

        [HttpGet("alcance")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObtenerAlcance()
        {
            try
            {
                var listado = _vhRepo.ObtenerAlcance();

                var listaCatalogoDto = new List<CatalogosDto>();

                foreach (var lista in listado)
                {
                    listaCatalogoDto.Add(_mapper.Map<CatalogosDto>(lista));
                }
                return Ok(listaCatalogoDto);
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

        [HttpGet("esq_acredita")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult  ObtenerEsqAcredita()
        {
            try
            {
                var listado = _vhRepo.ObtenerEsqAcredita();

                var listaCatalogoDto = new List<CatalogosDto>();

                foreach (var lista in listado)
                {
                    listaCatalogoDto.Add(_mapper.Map<CatalogosDto>(lista));
                }
                return Ok(listaCatalogoDto);
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

        [HttpGet("estado")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult  ObtenerEstado()
        {
            try
            {
                var listado = _vhRepo.ObtenerEstado();

                var listaCatalogoDto = new List<CatalogosDto>();

                foreach (var lista in listado)
                {
                    listaCatalogoDto.Add(_mapper.Map<CatalogosDto>(lista));
                }
                return Ok(listaCatalogoDto);
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

        [HttpGet("org_acredita")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult  ObtenerOrgAcredita()
        {
            try
            {
                var listado = _vhRepo.ObtenerOrgAcredita();

                var listaCatalogoDto = new List<CatalogosDto>();

                foreach (var lista in listado)
                {
                    listaCatalogoDto.Add(_mapper.Map<CatalogosDto>(lista));
                }
                return Ok(listaCatalogoDto);
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