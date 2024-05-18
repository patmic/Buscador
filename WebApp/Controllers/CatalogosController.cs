using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/catalogos")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

        [HttpGet("etiquetas_grilla")]
        public IActionResult ObtenerEtiquetaGrilla()
        {
            return GetCatalogData(_vhRepo.ObtenerEtiquetaGrilla, "Error obteniendo datos de Etiqueta Grilla");
        }

        [HttpGet("etiquetas_filtro")]
        public IActionResult ObtenerEtiquetaFiltros()
        {
            return GetCatalogData(_vhRepo.ObtenerEtiquetaFiltros, "Error obteniendo datos de Etiqueta Grilla");
        }
        [HttpGet("dimension")]
        public IActionResult ObtenerDimension()
        {
            return GetCatalogData(_vhRepo.ObtenerDimension, "Error obteniendo datos de Dimensión");
        }

        [HttpGet("filtro_detalles/{IdHomologacion:int}", Name = "ObtenerFiltroDetalles")]
        public IActionResult ObtenerFiltroDetalles(int IdHomologacion)
        {
            try
            {
                var data = _vhRepo.ObtenerFiltroDetalles(IdHomologacion);

                var catalogDtoList = data.Select(item => _mapper.Map<CatalogosDto>(item)).ToList();

                return Ok(catalogDtoList);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new {
                        statusCode = 500,
                        message = "Error obteniendo datos de Filtros Detalle"
                    });
            }
        }

        private IActionResult GetCatalogData(Func<IEnumerable<IVwHomologacion>> getData, string errorMessage)
        {
            try
            {
                var data = getData();

                var catalogDtoList = data.Select(item => _mapper.Map<CatalogosDto>(item)).ToList();

                return Ok(catalogDtoList);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, errorMessage);
            }
        }
    }
}