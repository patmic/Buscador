using Microsoft.AspNetCore.Mvc;
using WebApp.Repositories;

namespace WebApp;
    // [Route("[controller]")]
[Route("api/peru")]
[ApiController]

public class PeruController : Controller
{
    private readonly ILogger<PeruController> _logger;
    private readonly IPeruRepository _peruRepository;
    public PeruController(ILogger<PeruController> logger, IPeruRepository ecuadorRepository)
    {
        _logger = logger;
        _peruRepository= ecuadorRepository;
    }

   [HttpGet]
    public async Task<IActionResult> GetOrganizacion()
    {
        try
        {
            var data = await _peruRepository.GetOrganizacionAsync();
            return Ok(data);
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrganizacionById(int id)
    {
         try
        {
            var Organizacion = await _peruRepository.GetOrganizacionByIdAsync(id);
            if(Organizacion == null)
                return NotFound (new { statusCode = 404, message = "Organizacion no existe"});
            return Ok(Organizacion);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new {
                    statusCode = 500,
                    message = e.Message
            });
        }
    }

}