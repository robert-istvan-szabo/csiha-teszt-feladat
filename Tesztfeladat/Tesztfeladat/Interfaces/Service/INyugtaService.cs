using Tesztfeladat.Entity.DTOs;

namespace Tesztfeladat.Interfaces.Service
{
    public interface INyugtaService
    {
        public IEnumerable<Nyugta> GetAll();
        public bool NyugtaLetrehozasa(IEnumerable<Tetel> tetelek);
    }
}
