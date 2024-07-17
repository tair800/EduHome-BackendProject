using BackEndProject_Edu.Data;
using BackEndProject_Edu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Controllers
{
    public class CourseController : Controller
    {
        private readonly EduDbContext _context;

        public CourseController(EduDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.CourseCounts = _context.Courses.Count();

            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return BadRequest();
            var course = _context.Courses.AsNoTracking().FirstOrDefault(b => b.Id == id);
            var blogs = _context.Blogs.AsNoTracking().ToList();

            CourseVM courseVM = new()
            {
                Name = course.Name,
                ImgUrl = course.ImgUrl,
                Desc = course.Desc,
                About = course.About,
                Apply = course.Apply,
                Blogs = blogs,
            };
            return View(courseVM);
        }

    }
}
