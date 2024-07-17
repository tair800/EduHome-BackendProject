using BackEndProject_Edu.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject_Edu.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILayoutService _layoutService;

        public AboutController(ILayoutService IlayoutService)
        {
            _layoutService = IlayoutService;
        }

        public IActionResult Index()
        {
            return View(_layoutService.GetSettings());
        }

    }
}
