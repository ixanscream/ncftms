using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ncframework.Models
{
    public class User
    {
        [Key]
        [StringLength(36)]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [Required]
        public string UserId { get; set; }
        
        [StringLength(36, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]

        [DataType(DataType.Password)]
        [Required]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }


        [ForeignKey("Employee")]
        [StringLength(36)]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Boolean IsAdmin { get; set; }
    }
}
