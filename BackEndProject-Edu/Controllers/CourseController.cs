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

            var course = _context.Courses.Include(c => c.Category).Include(m => m.CourseTags).ThenInclude(m => m.Tag).Include(m => m.courseFeatures).ThenInclude(m => m.Features).AsNoTracking().FirstOrDefault(b => b.Id == id);
            var blogs = _context.Blogs.AsNoTracking().ToList();
            var categories = _context.Categories.Include(m => m.Course).AsNoTracking().ToList();

            CourseVM courseVM = new()
            {
                Name = course.Name,
                ImgUrl = course.ImgUrl,
                Desc = course.Desc,
                About = course.About,
                Apply = course.Apply,
                Categories = categories,
                Certification = course.Certification,
                Blogs = blogs,
                CourseTags = course.CourseTags,
                CourseFeatures = course.courseFeatures,


            };
            return View(courseVM);
        }
        public async Task<IActionResult> CourseSearch(string text)
        {
            var data = await _context.Courses.Where(k => k.Name.ToLower().Contains(text.ToLower())).Take(5).ToListAsync();
            return PartialView("_CourseSearchPartialView", data);
        }

    }
}
