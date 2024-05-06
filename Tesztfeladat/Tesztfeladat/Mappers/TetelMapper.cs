using Tesztfeladat.Entity.Models;

namespace Tesztfeladat.Mappers
{
    public static class TetelMapper
    {
        public static Tetel FromDTO(Entity.DTOs.Tetel tetel)
        {
            var result = new Tetel();
            result.Nev = tetel.nev;
            result.Sorszam = tetel.sorszam;
            result.Ar = tetel.ar;
            result.Nyugta = tetel.nyugta;
            result.Azonosito = tetel.azonosito;
            result.Mennyiseg = tetel.mennyiseg;
            result.Mertekegyseg = tetel.mertekegyseg;
            return result;
        }

        public static IEnumerable<Tetel> FromDTOList(IEnumerable<Entity.DTOs.Tetel> tetelek)
        {
            List<Tetel> result = new List<Tetel>();
            foreach (var tetel in tetelek)
            {
                result.Add(FromDTO(tetel));
            }
            return result;
        }
    }
}
