using Tesztfeladat.Entity.Models;

namespace Tesztfeladat.Interfaces.Repository
{
    public interface IFelhasznaloRepository
    {
        public bool Bejelentkezes(string felhasznalonev, string jelszo);
        public bool Create(Felhasznalo felhasznalo);
        public bool Kereses(string felhasznalonev);
    }
}
