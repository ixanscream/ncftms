using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ncframework.Models
{
    public class Employee
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "NIK")]
        public int Code { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [StringLength(255)]
        public string Photo { get; set; }


        [StringLength(50), MinLength(8)]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        

        [ForeignKey("Parent")]
        [Display(Name = "Report To")]
        [StringLength(36)]
        public string ParentId { get; set; }
        public Employee Parent { get; set; }

        [ForeignKey("Group")]
        [Display(Name = "Position")]
        [StringLength(36)]
        public string GroupId { get; set; }
        public Lookup Group { get; set; }

        [Display(Name = "Role Group")]
        [StringLength(100)]
        public string GroupMenu { get; set; }
    }
}
