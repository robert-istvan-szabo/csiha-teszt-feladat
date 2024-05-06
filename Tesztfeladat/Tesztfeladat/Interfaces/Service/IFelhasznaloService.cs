using Tesztfeladat.Entity.DTOs;

namespace Tesztfeladat.Interfaces.Service
{
    public interface IFelhasznaloService
    {
        public string Bejelentkezes(string felhasznalonev, string jelszo);
        public string Regisztracio(Felhasznalo felhasznalo);
    }
}
