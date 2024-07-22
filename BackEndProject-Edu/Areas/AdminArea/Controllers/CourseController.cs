using BackEndProject_Edu.Areas.AdminArea.ViewModels.CourseVMs;
using BackEndProject_Edu.Data;
using BackEndProject_Edu.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CourseController : Controller
    {
        private readonly EduDbContext _dbContext;
        private readonly IEmailService _emailService;

        public CourseController(EduDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            var query = await _dbContext.Courses
                .Include(m => m.Category)
                .AsNoTracking()
                .Select(m => new CourseListVM()
                {
                    Id = m.Id,
                    Name = m.Name,
                    ImgUrl = m.ImgUrl,
                    CategoryName = m.Category.Name,
                    Date = m.Date
                }).ToListAsync();

            return View(query);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var query = await _dbContext.Courses
                .Include(m => m.courseFeatures)
                .Include(m => m.CourseTags)
                .Include(m => m.Category)
                .AsNoTracking()
                .Select(m => new CourseDetailVM()
                {
                    Id = m.Id,
                    ImgUrl = m.ImgUrl,
                    Name = m.Name,
                    Desc = m.Desc,
                    About = m.About,
                    Apply = m.Apply,
                    Certification = m.Certification,
                    Date = m.Date,
                    CourseFeatures = m.courseFeatures,
                    CategoryName = m.Category.Name,
                    CourseTags = m.CourseTags
                }).FirstOrDefaultAsync(m => m.Id == id);
            if (query is null) return NotFound();
            return View(query);

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var course = await _dbContext.Courses.AsNoTracking().FirstOrDefaultAsync(k => k.Id == id);
            if (course is null) return NotFound();
            _dbContext.Remove(course);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");


        }
    }
}
