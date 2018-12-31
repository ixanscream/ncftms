using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ncframework.Models
{
    public class Menu
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]

        public string Name { get; set; }
        [Required]
        public int Index { get; set; }

        public string Controller { get; set; }
        [Required]
        [StringLength(50)]
        public string Action { get; set; }
        [StringLength(50)]
        public string Icon { get; set; }

        [ForeignKey("Parent")]
        [StringLength(36)]
        public string ParentId { get; set; }
        public Menu Parent { get; set; }
    }
}
