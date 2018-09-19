using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace testing_net.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<IdentityRole> Roles { get; set; }
    }
}
