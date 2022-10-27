using Microsoft.AspNetCore.Identity;

namespace IdentityFrame.Models
{
    public class ApplicaionUser :IdentityUser
    {
        public string Name { get; set; }
        public string Bdate { get; set; }
        

    }
}
