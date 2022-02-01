using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OrganizacnaStruktura.Data;

namespace OrganizacnaStruktura.Models
{
    //Model projektu
    public class Project
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        public Employee HeadOfProject { get; set; }

        public int HeadOfProjectId { get; set; }

       public List<Department> Departments { get; set; }

        public int? DivisionId { get; set; }
        
    }
}