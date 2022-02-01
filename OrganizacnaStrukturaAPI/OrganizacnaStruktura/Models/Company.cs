using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OrganizacnaStruktura.Data;

namespace OrganizacnaStruktura.Models
{
    //Model firmy
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        public Employee Ceo { get; set; }

        public int CeoId { get; set; }
        
        public List<Division> Divisions { get; set; }

        
    }
}