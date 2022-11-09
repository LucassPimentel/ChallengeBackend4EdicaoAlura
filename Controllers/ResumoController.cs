using ChallengeBackend4EdicaoAlura.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeBackend4EdicaoAlura.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResumoController : ControllerBase
    {

        private readonly IResumoRepository _resumoRepository;
        public ResumoController(IResumoRepository resumoRepository)
        {
            _resumoRepository = resumoRepository;
        }

        [HttpGet("{ano}/{mes}")]
        public IActionResult GetResumoPorData(int ano, int mes)
        {
            var resumo = _resumoRepository.GerarResumo(ano, mes);

            return Ok(resumo);
        }

    }
}
