using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using testing_net.Models;

namespace testing_net.Models.Views
{
    public class UserManagementViewModel
    {
        [MinLength(4)]
        public string Name { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string NewRole { get; set; }
        public List<SelectListItem> RolesListItem { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }

        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [MinLength(6)]
        [MaxLength(40)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [MinLength(6)]
        [MaxLength(40)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The password doesn't match the confirmation password")]
        public string ConfirmNewPassword { get; set; }
    }
}
