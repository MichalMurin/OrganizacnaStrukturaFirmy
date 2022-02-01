using System.ComponentModel.DataAnnotations;

namespace OrganizacnaStruktura.Dtos
{
    //trieda na parametre pri tvorbe zamestnanca
    public class EmployeeCreateDto
    {               
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