using WebApp.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using SharedApp.Models;

namespace WebApp.Controllers
{
    [Route("api/buscador")]
    [ApiController]
    public class BuscadorController(ILogger<BuscadorController> logger, IBuscadorRepository vhRepo) : ControllerBase
    {
        private readonly IBuscadorRepository _vhRepo = vhRepo;
        // protected RespuestasAPI? _respuestaApi;
        private readonly ILogger<BuscadorController> _logger = logger;

        [HttpGet("buscarPalabra")]
        public IActionResult PsBuscarPalabra(string paramJSON, int PageNumber, int RowsPerPage)
        {
            try
            {
                return Ok(_vhRepo.PsBuscarPalabra(paramJSON, PageNumber, RowsPerPage));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error en {nameof(PsBuscarPalabra)}");
                return StatusCode(500, "Error en el servidor");
            }
        }
        [HttpGet("homologacionEsquemaTodo")]
        public IActionResult FnHomologacionEsquemaTodo()
        {
            try
            {
                return Ok(_vhRepo.FnHomologacionEsquemaTodo());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error en {nameof(FnHomologacionEsquemaTodo)}");
                return StatusCode(500, "Error en el servidor");
            }
        }
        [HttpGet("homologacionEsquema/{idHomologacionEsquema}")]
        public IActionResult FnHomologacionEsquema(int idHomologacionEsquema)
        {
            try
            {
                return Ok(_vhRepo.FnHomologacionEsquema(idHomologacionEsquema));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error en {nameof(FnHomologacionEsquema)}");
                return StatusCode(500, "Error en el servidor");
            }
        }
        [HttpGet("homologacionEsquemaDato/{idHomologacionEsquema}/{idDataLakeOrganizacion}")]
        public IActionResult FnHomologacionEsquemaDato(int idHomologacionEsquema, int idDataLakeOrganizacion)
        {
            try
            {
                return Ok(_vhRepo.FnHomologacionEsquemaDato(idHomologacionEsquema, idDataLakeOrganizacion));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error en {nameof(FnHomologacionEsquemaDato)}");
                return StatusCode(500, "Error en el servidor");
            }
        }
    }
}