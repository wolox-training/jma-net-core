using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class AccountController : Controller
{
    [AllowAnonymous]
    public ActionResult Login()
    {
    }

    [AllowAnonymous]
    public ActionResult Register()
    {
    }

    public ActionResult Logout()
    {
    }
}
