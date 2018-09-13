using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

namespace testing_net.Controllers 
{
    public class MoviesController : Controller 
    {
        public IActionResult Index () 
        {
            return View ();
        }
    }
}
