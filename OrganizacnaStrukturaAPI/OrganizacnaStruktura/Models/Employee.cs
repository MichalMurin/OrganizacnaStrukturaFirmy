using System.ComponentModel.DataAnnotations;

namespace OrganizacnaStruktura.Models
{
    //Model zamestnanca
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        
        [MaxLength(10)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        public string Phone { get; set; }
        
        public string email { get; set; }
        
    }
}