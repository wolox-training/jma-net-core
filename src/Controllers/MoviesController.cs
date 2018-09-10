using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace testing_net.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}