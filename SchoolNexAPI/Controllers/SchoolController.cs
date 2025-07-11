using Microsoft.AspNetCore.Mvc;

namespace SchoolNexAPI.Controllers
{
    public class SchoolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
