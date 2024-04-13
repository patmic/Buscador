using Microsoft.AspNetCore.Mvc;
using WebApp.Repositories;

namespace WebApp;
    // [Route("[controller]")]
[Route("api/ecuador")]
[ApiController]

public class EcuadorController : Controller
{
    private readonly ILogger<EcuadorController> _logger;
    private readonly IEcuadorRepository _ecuadorRepository;
    public EcuadorController(ILogger<EcuadorController> logger, IEcuadorRepository ecuadorRepository)
    {
        _logger = logger;
        _ecuadorRepository= ecuadorRepository;
    }

   [HttpGet]
    public async Task<IActionResult> GetEmpresas()
    {
        try
        {
            var empresa = await _ecuadorRepository.GetEmpresasAsync();
            return Ok(empresa);
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
    public async Task<IActionResult> GetEmpresaById(int id)
    {
         try
        {
            var empresa = await _ecuadorRepository.GetEmpresasByIdAsync(id);
            if(empresa == null)
                return NotFound (new { statusCode = 404, message = "empresa no existe"});
            return Ok(empresa);
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