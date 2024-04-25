using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.Repositories.IRepositories;
using WebApp.Models.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Route("api/ecuador")]
    [ApiController]

    public class EcuadorController : Controller
    {
        private readonly ILogger<EcuadorController> _logger;
        private readonly IEcuadorRepository _ecuadorRepository;
        private readonly IMapper _mapper;
        public EcuadorController(ILogger<EcuadorController> logger, IEcuadorRepository ecuadorRepository, IMapper mapper)
        {
            _logger = logger;
            _ecuadorRepository= ecuadorRepository;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmpresas()
        {
            try
            {
                var listaEmpresas = _ecuadorRepository.GetEmpresas();

                var listaEmpresasDto = new List<EmpresaDto>();

                foreach (var lista in listaEmpresas)
                {
                    listaEmpresasDto.Add(_mapper.Map<EmpresaDto>(lista));
                }
                return Ok(listaEmpresasDto);
                // var empresa = await _ecuadorRepository.GetEmpresasAsync();
                // return Ok(empresa);
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
        [HttpGet("{empresaId:int}", Name = "GetEmpresa")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmpresa(int empresaId)
        {
            try
            {
                var itemEmpresa = _ecuadorRepository.GetEmpresa(empresaId);

                if (itemEmpresa == null)
                {
                    return NotFound();
                }

                var itemEmpresaDto = _mapper.Map<EmpresaDto>(itemEmpresa);
                return Ok(itemEmpresaDto);
                // var empresa = await _ecuadorRepository.GetEmpresasByIdAsync(id);
                // if(empresa == null)
                //     return NotFound (new { statusCode = 404, message = "empresa no existe"});
                // return Ok(empresa);
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