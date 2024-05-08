using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Tesztfeladat.Entity.DTOs;
using Tesztfeladat.Interfaces.Service;
using Tesztfeladat.Validators;

namespace Tesztfeladat.Controller
{
    [Route("/Nyugta")]
    [ApiController]
    [Authorize]
    public class NyugtaController : ControllerBase
    {
        private INyugtaService nyugtaService;
        private ILogger<NyugtaController> logger;
        
        public NyugtaController(INyugtaService nyugtaService, ILogger<NyugtaController> logger)
        {
            this.nyugtaService = nyugtaService;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult NyugtakLekerdezes()
        {
            //logger.LogInformation("Bejelentkezes ellenorzese");
            //if (String.IsNullOrEmpty(userManager.GetUsername()))
            //{
            //    logger.LogError("Nincs bejelentkezett felhasznalo");
            //    return Unauthorized("Nincs bejelentkezett felhasznalo");
            //}

            logger.LogInformation("Nyugták lekérdezése elindult");
            var data = nyugtaService.GetAll();
            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult NyugtaLetrehozasa([FromBody] IEnumerable<Tetel> tetels)
        {
            //logger.LogInformation("Bejelentkezes ellenorzese");
            //if (String.IsNullOrEmpty(userManager.GetUsername()))
            //{
            //    logger.LogError("Nincs bejelentkezett felhasznalo");
            //    return Unauthorized("Nincs bejelentkezett felhasznalo");
            //}
            
            logger.LogInformation("Nyugta létrehozása elindult");
            var validationResult = TetelValidator.Validation(tetels);
            if (validationResult == TetelValidator.valid)
            {
                try
                {
                    if (nyugtaService.NyugtaLetrehozasa(tetels))
                    {
                        return Ok();
                    }    
                }
                catch (Exception ex)
                {
                    logger.LogError("Sikertelen nyugta létrehozás: " + ex.Message);
                    throw ex;
                }
            }
            logger.LogError("Validációs probléma: " + validationResult);
            return BadRequest(validationResult);
        }
    }
}
