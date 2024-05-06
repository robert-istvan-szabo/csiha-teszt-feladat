using Tesztfeladat.Entity.Models;

namespace Tesztfeladat.Interfaces.Repository
{
    public interface ITetelRepository
    {
        public IEnumerable<Tetel> GetAll();
        public IEnumerable<Tetel> GetByNyugta(int nyugtaszam);
        public bool Create(Tetel tetel);
        public int CreateMultiple(IEnumerable<Tetel> tetelList);
    }
}
