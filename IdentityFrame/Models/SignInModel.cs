using System.ComponentModel.DataAnnotations;

namespace IdentityFrame.Models
{
    public class SignInModel
    {
        [Required]
        public string Username { set; get; }
        [Required]

        public string password { get; set; }

        public bool Remember { get; set; }
    }
}
