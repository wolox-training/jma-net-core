using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testing_net.Models;
using testing_net.Repositories.Database;

namespace testing_net.Repositories
{
    public class UserRepository
    {
        private readonly DbContextOptions<DataBaseContext> _options;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(DbContextOptions<DataBaseContext> options, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._options = options;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public DataBaseContext Context
        {
            get { return new DataBaseContext(this._options); }
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public List<ApplicationUser> GetAllUsersWithRoles()
        {
            using (var context = Context)
            {
                return (from user in context.Users
                        select new ApplicationUser
                        {
                            UserName = user.UserName,
                            Roles = (from role in context.Roles
                                    join userRole in context.UserRoles on role.Id equals userRole.RoleId
                                    where userRole.UserId == user.Id
                                    select role).ToList(),
                        }).Distinct().ToList();
            }
        }

        public List<IdentityRole> GetAllRoles() => _roleManager.Roles.ToList();
    }
}

