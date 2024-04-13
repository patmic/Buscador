using Microsoft.AspNetCore.Mvc;
using WebApp.Repositories;

namespace WebApp.Controllers;

[Route("api/vwBusqueda")]
[ApiController]
public class EcuadorSAEvwBusquedaController: Controller
{
    private readonly ILogger<EcuadorSAEvwBusquedaController> _logger;
    private readonly IEcuadorSAEvwBusquedaRepository _EcuadorSAERepository;
    public EcuadorSAEvwBusquedaController(  ILogger<EcuadorSAEvwBusquedaController> logger, 
                                            IEcuadorSAEvwBusquedaRepository _IEcuadorSAEvwBusquedaRepository)
    {
        _logger = logger;
        _EcuadorSAERepository = _IEcuadorSAEvwBusquedaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetALlAsync_SAEvwBusqueda()
    {
        try
        {
            var vwBusqueda = await _EcuadorSAERepository.GetALlAsync_SAEvwBusqueda();
            return Ok(vwBusqueda);
        }
        catch (Exception e) { return MsgError(e); }
        // catch (Exception e)
        // {
        //     _logger.LogError(e.Message);
        //     return StatusCode(StatusCodes.Status500InternalServerError, new
        //     {
        //         statusCode = 500,
        //         message = e.Message
        //     });
        // }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync_SAEvwBusqueda(int id)
    {
         try
        {
            var vwBusqueda = await _EcuadorSAERepository.GetByIdAsync_SAEvwBusqueda(id);
            if(vwBusqueda == null)
                return NotFound (new { statusCode = 404, message = "vwBusqueda no encontrado..."});
            return Ok(vwBusqueda);
        }
        catch (Exception e) { return MsgError(e); }
    }

    private IActionResult MsgError(Exception e)
    {
        _logger.LogError(e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError, new
        {
            statusCode = 500,
            message = e.Message
        });
    }
}