using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;

namespace WebApp.Controllers
{
    [Route("api/peru")]
    [ApiController]

    public class PeruController : Controller
    {
        private readonly ILogger<PeruController> _logger;
        private readonly IPeruRepository _peruRepository;
        private readonly IMapper _mapper;
        public PeruController(ILogger<PeruController> logger, IPeruRepository ecuadorRepository, IMapper mapper)
        {
            _logger = logger;
            _peruRepository= ecuadorRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrganizaciones()
        {
            try
            {
                var listaOrganizaciones = _peruRepository.GetOrganizaciones();

                var listaOrganizacionesDto = new List<OrganizacionDto>();

                foreach (var lista in listaOrganizaciones)
                {
                    listaOrganizacionesDto.Add(_mapper.Map<OrganizacionDto>(lista));
                }
                return Ok(listaOrganizacionesDto);
                // var data = await _peruRepository.GetOrganizacionAsync();
                // return Ok(data);
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
        [HttpGet("{organizacionId:int}", Name = "GetOrganizacion")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrganizacion(int organizacionId)
        {
            try
            {
                var itemOrganizacion = _peruRepository.GetOrganizacion(organizacionId);

                if (itemOrganizacion == null)
                {
                    return NotFound();
                }

                var itemOrganizacionDto = _mapper.Map<OrganizacionDto>(itemOrganizacion);
                return Ok(itemOrganizacionDto);
                // var Organizacion = await _peruRepository.GetOrganizacionByIdAsync(id);
                // if(Organizacion == null)
                //     return NotFound (new { statusCode = 404, message = "Organizacion no existe"});
                // return Ok(Organizacion);
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
}