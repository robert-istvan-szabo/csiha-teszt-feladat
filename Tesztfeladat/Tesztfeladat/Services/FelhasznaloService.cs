using System.Security.Cryptography;
using System.Text;
using Tesztfeladat.Entity.DTOs;
using Tesztfeladat.Interfaces.Repository;
using Tesztfeladat.Interfaces.Service;
using Tesztfeladat.Mappers;

namespace Tesztfeladat.Services
{
    public class FelhasznaloService : IFelhasznaloService
    {
        IFelhasznaloRepository felhasznaloRepository;

        public string JelszoHashelese(string jelszo)
        {
            using (var sha256 = new SHA256Managed())
            {
                byte[] salt = Encoding.UTF8.GetBytes("SoAJelszohoz");

                byte[] passwordBytes = Encoding.UTF8.GetBytes(jelszo);
                byte[] salted = new byte[passwordBytes.Length + salt.Length];

                Buffer.BlockCopy(passwordBytes, 0, salted, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, salted, passwordBytes.Length, salt.Length);

                byte[] hashedBytes = sha256.ComputeHash(salted);

                return Encoding.UTF8.GetString(hashedBytes);
            }
        }

        public FelhasznaloService(IFelhasznaloRepository felhasznaloRepository)
        {
            this.felhasznaloRepository = felhasznaloRepository;
        }

        public string Bejelentkezes(string felhasznalonev, string jelszo)
        {
            if (felhasznaloRepository.Bejelentkezes(felhasznalonev, jelszo))
            {
                return felhasznalonev;
            }
            return "";
        }

        public string Regisztracio(Felhasznalo felhasznalo)
        {
            if (felhasznaloRepository.Kereses(felhasznalo.UserName))
            {
                throw new Exception("A felhasznalo mar letezik");
            }

            if (felhasznaloRepository.Create(FelhasznaloMapper.FromDTO(felhasznalo)))
                return felhasznalo.UserName;
            return "";
        }
    }
}
