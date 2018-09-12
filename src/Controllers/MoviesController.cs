using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using testing_net.Repositories.Interfaces;

namespace testing_net.Controllers {
    
    public class MoviesController : Controller {

        private readonly IUnitOfWork _unitOfWork;

        public MoviesController(IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork {
            get { return this._unitOfWork; }
        }
        
        public IActionResult Index() {
            return View();
        }
    }
}
