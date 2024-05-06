using Microsoft.AspNetCore.Identity;

namespace Tesztfeladat.Entity.Models
{
    public class Felhasznalo : IdentityUser
    {
        public string Felhasznalonev { get; set; }
        public string Email { get; set; }
        public string Jelszo { get; set; }

    }
}
