using System.ComponentModel.DataAnnotations;

namespace OrganizacnaStruktura.Dtos
{
    //trieda na parametre pri tvorbe oddelenia
    public class DepartmentCreateDto
    {               
        

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int HeadOfDepartmentId { get; set; }

        public int ProjectId { get; set; }
        
    }
}