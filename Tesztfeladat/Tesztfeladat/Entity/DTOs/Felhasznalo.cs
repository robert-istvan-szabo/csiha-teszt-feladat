using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace Tesztfeladat.Entity.DTOs
{
    public class Felhasznalo : IdentityUser, IUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string jelszo { get; set; }

        public string Id => UserName;
    }
}
