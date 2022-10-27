using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityFrame.data
{
    [Table("Employeees")]
    public class Employee
    {
       
        public int id { get; set; }
        public string Name { get; set; }

        
      

    }
}
