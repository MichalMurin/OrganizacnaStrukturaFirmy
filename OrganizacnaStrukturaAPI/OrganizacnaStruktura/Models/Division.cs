using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OrganizacnaStruktura.Data;

namespace OrganizacnaStruktura.Models
{
    //model divízie
    public class Division
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        public Employee HeadOfDivision { get; set; }

        public int HeadOfDivisionId { get; set; }

        public List<Project> Projects { get; set; }

        public int? CompanyId { get; set; }

        
    }
}