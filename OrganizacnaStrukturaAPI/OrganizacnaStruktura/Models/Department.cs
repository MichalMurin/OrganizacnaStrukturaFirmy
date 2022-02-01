using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrganizacnaStruktura.Models
{
    //Model oddelenia
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public Employee HeadOfDepartment { get; set; }

        public int HeadOfDepartmentId { get; set; }
        
        public int? ProjectId { get; set; }
    }
}