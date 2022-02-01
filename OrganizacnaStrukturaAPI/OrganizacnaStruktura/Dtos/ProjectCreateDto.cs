using System.ComponentModel.DataAnnotations;

namespace OrganizacnaStruktura.Dtos
{
    //trieda na parametre pri tvorbe projektu
    public class ProjectCreateDto
    {       
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int HeadOfProjectId { get; set; }

        public int DivisionId { get; set; }
        
    }
}