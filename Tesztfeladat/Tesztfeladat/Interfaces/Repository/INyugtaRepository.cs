using Tesztfeladat.Entity.Models;

namespace Tesztfeladat.Interfaces.Repository
{
    public interface INyugtaRepository
    {
        public IEnumerable<Nyugta> GetAll();

        public int Create(Nyugta nyugta);
    }
}
