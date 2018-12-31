using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ncframework.Models
{
    public class Lookup
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]

        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]

        public string Code { get; set; }

        [ForeignKey("Parent")]
        [StringLength(36)]
        public string ParentId { get; set; }
        public Lookup Parent { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]

        public string Group { get; set; }
    }
}
