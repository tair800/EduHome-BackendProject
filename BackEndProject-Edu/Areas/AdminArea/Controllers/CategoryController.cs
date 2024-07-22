using Microsoft.AspNetCore.Mvc;

namespace BackEndProject_Edu.Areas.AdminArea.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
