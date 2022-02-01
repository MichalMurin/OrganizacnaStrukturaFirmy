using System.ComponentModel.DataAnnotations;

namespace OrganizacnaStruktura.Dtos
{
    //trieda na parametre pri úprave oddelenia
    public class DepartmentUpdateDto
    {   
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int HeadOfDepartmentId { get; set; }

        public int ProjectId { get; set; }
        
    }
}