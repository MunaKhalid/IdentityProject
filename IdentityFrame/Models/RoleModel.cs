using System.ComponentModel.DataAnnotations;

namespace IdentityFrame.Models
{
    public class RoleModel
    {
        [Required]
        public string Name { get; set; }
    }
}
