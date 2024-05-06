using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Tesztfeladat.Entity.DTOs;
using Tesztfeladat.Interfaces.Service;

namespace Tesztfeladat.Controller
{
    [Route("/Felhasznalo")]
    [ApiController]
    public class FelhasznaloController : ControllerBase
    {
        private IFelhasznaloService felhasznaloService;
        private ILogger<FelhasznaloController> logger;
        private UserManager<Felhasznalo> userManager;

        public FelhasznaloController(IFelhasznaloService felhasznaloService, ILogger<FelhasznaloController> logger, UserManager<Felhasznalo> userManager)
        {
            this.felhasznaloService = felhasznaloService;
            this.logger = logger;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("bejelentkezes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Bejelentkezes([FromBody] Felhasznalo felhasznalo)
        {
            logger.LogInformation($"Bejelentkezési kérés elindult, felhasználó {felhasznalo.UserName}");
            try
            {
                var nev = felhasznaloService.Bejelentkezes(felhasznalo.UserName, felhasznalo.jelszo);
                if (nev == felhasznalo.UserName)
                {
                    var user = userManager.Create(felhasznalo, felhasznalo.jelszo);
                    return Ok(felhasznalo);
                }
                else
                {
                    logger.LogWarning("Hibás felhasználónév vagy jelszó");
                    return NotFound();
                }
            } catch (Exception ex) 
            {
                logger.LogError("Bejelentkezési hiba: " + ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        [Route("regisztracio")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Regisztraco([FromBody] Felhasznalo felhasznalo)
        {
            logger.LogInformation($"Regisztrációs kérés elindult, felhasznalo {felhasznalo.UserName}");
            try
            {
                felhasznaloService.Regisztracio(felhasznalo);
                return Ok();
            } catch (Exception ex)
            {
                logger.LogError("Hiba lépett fel a felhasználó regisztrációja közben. " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
