using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repositories;

namespace WebApp;
[Route("api/persona")]
[ApiController]
public class PersonaController : ControllerBase
{
    private readonly ILogger<PersonaController> _logger;
    private readonly IPersonaRepository _personaRepository;
    public PersonaController(ILogger<PersonaController> logger, IPersonaRepository personaRepository)
    {
        _logger = logger;
        _personaRepository = personaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetPersonas()
    {
        try
        {
            var persona = await _personaRepository.GetPersonasAsync();
            return Ok(persona);
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

    [HttpPost]
    public async Task<IActionResult> CreatePersona(Persona persona)
    {
        try
        {
            var createPersona = await _personaRepository.AddPersonaAsync(persona);
            return CreatedAtAction(nameof(CreatePersona), createPersona);
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

    [HttpPut]
    public async Task<IActionResult> UpdatePersona(Persona personaToUpdate)
    {
        try
        {
            var persona = await _personaRepository.GetPersonasByIdAsync(personaToUpdate.id);
            if(persona == null)
                return NotFound (new { statusCode = 404, message = "Persona no existe"});
            
            await _personaRepository.UpdatePersonaAsync(personaToUpdate);
            return NoContent();
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
    public async Task<IActionResult> GetPersonaById(int id)
    {
         try
        {
            var persona = await _personaRepository.GetPersonasByIdAsync(id);
            if(persona == null)
                return NotFound (new { statusCode = 404, message = "Persona no existe"});
            return Ok(persona);
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersonaById(int id)
    {
         try
        {
            var persona = await _personaRepository.GetPersonasByIdAsync(id);
            if(persona == null)
                return NotFound (new { statusCode = 404, message = "Persona no existe"});
            await _personaRepository.DeletePersonaAsync(id);
            return NoContent();
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
