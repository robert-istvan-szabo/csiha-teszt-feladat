using Microsoft.AspNetCore.Mvc;
using Tesztfeladat.Interfaces.Repository;

namespace Tesztfeladat.Controller
{
    [Route("/Tetel")]
    [ApiController]
    //[Authorize]
    public class TetelController : ControllerBase
    {
        private ITetelRepository tetelRepository;
        private ILogger<TetelController> logger;

        public TetelController(ITetelRepository tetelRepository, ILogger<TetelController> logger)
        {
            this.tetelRepository = tetelRepository;
            this.logger = logger;
        }

        [HttpGet("{nyugtaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult NyugtaTetelei([FromRoute]int nyugtaId)
        {
            logger.LogInformation("Nyugta tételeinek lekérdezése");
            try
            {
                var data = tetelRepository.GetByNyugta(nyugtaId);
                if (data == null || data.Count() == 0)
                {
                    logger.LogWarning("Megadott nyugtához tartozó tétel nem található");
                    return NotFound();
                }
                return Ok(data);
            } catch (Exception ex)
            {
                logger.LogError("Nyugta tételeinek lekérdezése sikertelen volt: " + ex.Message);
                throw ex;
            }
        }
    }
}
