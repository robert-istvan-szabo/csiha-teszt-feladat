using Tesztfeladat.Entity.DTOs;

namespace Tesztfeladat.Validators
{
    public static class TetelValidator
    {
        public static readonly string valid = "Valid";
        public static string Validation(IEnumerable<Tetel> tetels)
        {
            if (tetels == null) return "A tételek listája nem lehet null";
            if (tetels.Count() == 0) return "Legalább 1 tételnek szerepelnie kell a nyugtán";
            if (tetels.Count() > 99) return "Maximum 99 elem szerepelhet egy nyugtán";
            foreach(var tetel in tetels)
            {
                if (tetel.mennyiseg > 99) return "Egy tétel maximális mennyisége 99 lehet";
                if (tetel.mennyiseg < 1) return "Egy tétel minimális mennyisége 1 lehet";
                if (tetel.mertekegyseg.Length > 2) return "A mértékegység maximum 2 karakter lehet";
                if (tetel.mertekegyseg.Length == 0) return "Mértékenység megadása kötelező";
                if (tetel.ar < 1) return "Az ár legalább 1 kell legyen";
                if (tetel.ar > 99999) return "Az ár nem lehet több mint 99999";
            }
            return valid;
        }
    }
}
