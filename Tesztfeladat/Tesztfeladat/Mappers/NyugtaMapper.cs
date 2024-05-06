using Tesztfeladat.Entity.DTOs;

namespace Tesztfeladat.Mappers
{
    public static class NyugtaMapper
    {
        public static Nyugta FromModel(Entity.Models.Nyugta nyugta)
        {
            var result = new Nyugta();
            result.azonosito = nyugta.Azonosito;
            result.osszeg = nyugta.Osszeg;
            result.letrehozas = nyugta.Letrehozas;
            return result;
        }

        public static Entity.Models.Nyugta FromDTO(Nyugta nyugta)
        {
            var result = new Entity.Models.Nyugta();
            result.Azonosito = nyugta.azonosito;
            result.Osszeg = nyugta.osszeg;
            result.Letrehozas = nyugta.letrehozas;
            return result;
        }

        public static IEnumerable<Nyugta> FromModelList(IEnumerable<Entity.Models.Nyugta> nyugtaList)
        {
            var result = new List<Nyugta>();
            foreach (var nyugta in nyugtaList)
            {
                result.Add(FromModel(nyugta));
            }
            return result;
        }
    }
}
