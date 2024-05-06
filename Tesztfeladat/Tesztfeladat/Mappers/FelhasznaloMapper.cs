using Tesztfeladat.Entity.Models;

namespace Tesztfeladat.Mappers
{
    public static class FelhasznaloMapper
    {
        public static Felhasznalo FromDTO(Entity.DTOs.Felhasznalo felhasznalo)
        {
            var result = new Felhasznalo();
            result.UserName = felhasznalo.UserName;
            result.Jelszo = felhasznalo.jelszo;
            result.Email = felhasznalo.Email;
            return result;
        }
    }
}
