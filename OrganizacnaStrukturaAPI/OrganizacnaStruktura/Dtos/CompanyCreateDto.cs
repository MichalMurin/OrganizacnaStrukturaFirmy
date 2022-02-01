using System.ComponentModel.DataAnnotations;

namespace OrganizacnaStruktura.Dtos
{
    //trieda na parametre pri tvorbe firmy
    public class CompanyCreateDto
    {                      

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int CeoId { get; set; }

    }
}