using System.ComponentModel.DataAnnotations;

namespace IdentityFrame.Models
{
    public class SignUpModel
    {
        [Required]
        [StringLength(15)]
        public string Name { get; set; }
        [Required]
        public string bdate { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        [Compare("confirem")]
        public string password { get; set; }
        

        public string confirem { get; set; }

    }
}
