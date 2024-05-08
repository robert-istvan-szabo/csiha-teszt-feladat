using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tesztfeladat.Interfaces;
using Tesztfeladat.Interfaces.Repository;

namespace Tesztfeladat.Controller
{
    [Route("/Tetel")]
    [ApiController]
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public IActionResult NyugtaTetelei([FromRoute]int nyugtaId)
        {
            //logger.LogInformation("Bejelentkezes ellenorzese");
            //if (String.IsNullOrEmpty(userManager.GetUsername()))
            //{
            //    logger.LogError("Nincs bejelentkezett felhasznalo");
            //    return Unauthorized("Nincs bejelentkezett felhasznalo");
            //}

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
