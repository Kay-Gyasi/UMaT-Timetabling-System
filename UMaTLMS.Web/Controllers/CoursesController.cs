using Microsoft.AspNetCore.Mvc;

namespace UMaTLMS.Web.Controllers
{
    public class CoursesController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
        
        public IActionResult Assign()
        {
            return View();
        }
    }
}