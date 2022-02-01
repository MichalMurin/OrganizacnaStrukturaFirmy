using System.ComponentModel.DataAnnotations;

namespace OrganizacnaStruktura.Dtos
{
    //trieda na parametre pri tvorbe divízie
    public class DivisionCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int HeadOfDivisionId { get; set; }

        public int CompanyId { get; set; }
    }
}