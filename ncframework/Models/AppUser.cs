using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ncframework.Models
{
    public class AppUser
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }
        public string Email { get; set; }
        public string IsAdmin { get; set; }
        public string MenuJson { get; set; }
    }

    public class AppCurrentUser : ClaimsPrincipal
    {
        public AppCurrentUser(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        public string Id
        {
            get
            {
                return this.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }

        public string Name
        {
            get
            {
                return this.FindFirst(ClaimTypes.Name).Value;
            }
        }
        

        public string Email
        {
            get
            {
                return this.FindFirst(ClaimTypes.Email).Value;
            }
        }

        public string Role
        {
            get
            {
                return this.FindFirst(ClaimTypes.Role).Value;
            }
        }
    }
}
