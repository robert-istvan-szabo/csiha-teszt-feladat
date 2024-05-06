using Tesztfeladat.Entity.DTOs;
using Tesztfeladat.Interfaces.Repository;
using Tesztfeladat.Interfaces.Service;
using Tesztfeladat.Mappers;

namespace Tesztfeladat.Services
{
    public class NyugtaService : INyugtaService
    {
        private readonly INyugtaRepository nyugtaRepository;
        private readonly ITetelRepository tetelRepository;

        public NyugtaService(INyugtaRepository nyugtaRepository, ITetelRepository tetelRepository)
        {
            this.nyugtaRepository = nyugtaRepository;
            this.tetelRepository = tetelRepository;
        }

        public IEnumerable<Nyugta> GetAll()
        {
            return NyugtaMapper.FromModelList(nyugtaRepository.GetAll());
        }

        public bool NyugtaLetrehozasa(IEnumerable<Tetel> tetelek)
        {
            var nyugta = new Nyugta();
            nyugta.letrehozas = DateTime.Now;
            nyugta.osszeg = tetelek.Sum(x => (x.ar * x.mennyiseg));
            var nyugtaszam = nyugtaRepository.Create(NyugtaMapper.FromDTO(nyugta));

            foreach(var tetel in tetelek)
            {
                tetel.nyugta = nyugtaszam;
            }

            return tetelRepository.CreateMultiple(TetelMapper.FromDTOList(tetelek)) > 0;
        }
    }
}
