using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using testing_net.Models;
using testing_net.Models.Views;
using testing_net.Repositories;
using testing_net.Repositories.Database;

namespace testing_net.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class UserManagementController : Controller
    {
        private readonly UserRepository _userRepository;

         public UserManagementController(DbContextOptions<DataBaseContext> options,
                                        UserManager<ApplicationUser> userManager,
                                        RoleManager<IdentityRole> roleManager,
                                        IHtmlLocalizer<UserManagementController> localizer)
        {
            this._userRepository = new UserRepository(options, userManager, roleManager);
        }

        [HttpGet]
        public IActionResult Users()
        {
            return View(new UserManagementViewModel{Users = _userRepository.GetAllUsersWithRoles()});
        }

    }

}
