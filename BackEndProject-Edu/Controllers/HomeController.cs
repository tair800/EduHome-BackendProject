using BackEndProject_Edu.Data;
using BackEndProject_Edu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Controllers
{
    public class HomeController : Controller
    {
        private readonly EduDbContext _context;

        public HomeController(EduDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sliders = _context.Sliders.AsNoTracking().ToList();
            var sliderTexts = _context.SliderTexts.AsNoTracking().FirstOrDefault();
            HomeVM homeVM = new()
            {
                Sliders = sliders,
                SliderTexts = sliderTexts
            };
            return View(homeVM);
        }

    }
}
