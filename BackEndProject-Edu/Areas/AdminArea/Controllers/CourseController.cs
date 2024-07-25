using BackEndProject_Edu.Areas.AdminArea.ViewModels.CourseVMs;
using BackEndProject_Edu.Data;
using BackEndProject_Edu.Models;
using BackEndProject_Edu.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

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

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await GetCategories();
            ViewBag.Tags = await GetTags();

            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM request)
        {
            ViewBag.Categories = await GetCategories();
            ViewBag.Tags = await GetTags();


            if (!ModelState.IsValid) return View(request);

            bool isExist = await _dbContext.Courses.AnyAsync(m => m.Name == request.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", $"{request.Name} Allready exists");
                return View(request);
            }
            var file = request.ImgUrl;
            Course newCourse = new()
            {
                Name = request.Name,
                Desc = request.Desc,
                ImgUrl = await SaveFilesAsync(file),
                About = request.About,
                Certification = request.Certification,
                Apply = request.Apply,
                CourseTags = new List<CourseTag>(),
                CourseFeatures = new List<CourseFeature>(),
                CategoryId = request.CategoryId,

            };
            Features newFeature = new()
            {
                StartDate = DateTime.Now,
                Hours = request.Hours,
                Duration = request.Duration,
                Skill = request.Skill,
                StuNum = request.StuNum,
                Assesment = request.Assesment,
                Price = request.Price,
                Language = request.Language,
            };
            newCourse.CourseTags.AddRange(request.TagIds
                .Select(tagIds => new CourseTag()
                {
                    CourseId = newCourse.Id,
                    TagId = tagIds
                }));

            newCourse.CourseFeatures.AddRange(request.FeaturesIds
                .Select(featureIds => new CourseFeature()
                {
                    CourseId = newCourse.Id,
                    FeaturesId = featureIds
                }));

            await _dbContext.Courses.AddAsync(newCourse);
            await _dbContext.Features.AddAsync(newFeature);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var course = await _dbContext.Courses
      .Include(c => c.CourseTags).ThenInclude(ct => ct.Tag)
      .Include(c => c.CourseFeatures).ThenInclude(cf => cf.Features)
      .Include(c => c.Category)
      .FirstOrDefaultAsync(c => c.Id == id);

            CourseDetailVM vm = new()
            {
                Id = course.Id,
                Name = course.Name,
                Desc = course.Desc,
                ImgUrl = course.ImgUrl,
                About = course.About,
                Apply = course.Apply,
                Certification = course.Certification,
                Date = course.Date,
                CourseFeatures = course.CourseFeatures.Select(cf => cf.Features).ToList(),
                CategoryName = course.Category?.Name,
                CourseTags = course.CourseTags.Select(ct => ct.Tag).ToList()
            };
            return View(vm);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();
            Course dbCourse = await _dbContext.Courses
              .Include(c => c.CourseTags).ThenInclude(ct => ct.Tag)
              .Include(c => c.CourseFeatures).ThenInclude(cf => cf.Features)
              .Include(c => c.Category)
              .FirstOrDefaultAsync(c => c.Id == id);

            CourseUpdateVM model = new()
            {
                Name = dbCourse.Name,
                Desc = dbCourse.Desc,
                ImgUrl = dbCourse.ImgUrl,
                About = dbCourse.About,
                Apply = dbCourse.Apply,
                Certification = dbCourse.Certification,
                Date = dbCourse.Date,
                Features = dbCourse.CourseFeatures,
                Tags = dbCourse.CourseTags,
                CategoryId = dbCourse.Category.Id,

            };

            ViewBag.Categories = await GetCategories();
            ViewBag.Tags = await GetTags();

            return View(model);
        }









        public async Task<SelectList> GetCategories()
        {
            IEnumerable<Category> categories = await _dbContext.Categories.ToListAsync();
            return new SelectList(categories, "Id", "Name");
        }

        public async Task<SelectList> GetTags()
        {
            IEnumerable<Tag> tags = await _dbContext.Tags.ToListAsync();
            return new SelectList(tags, "Id", "Name");
        }





        public async Task<string> SaveFilesAsync(IFormFile file)
        {
            var filePath = Path.Combine("wwwroot/img/course/", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"{file.FileName}";
        }




























        //public async Task<IActionResult> Index(int page = 1)
        //{
        //    var query = _dbContext.Courses
        //        .Include(m => m.Category)
        //        .AsNoTracking()
        //        .Select(m => new CourseListVM()
        //        {
        //            Id = m.Id,
        //            Name = m.Name,
        //            ImgUrl = m.ImgUrl,
        //            CategoryName = m.Category.Name,
        //            Date = m.Date
        //        });

        //    return View(await PaginationVM<CourseListVM>.CreateVM(query, page, 2));
        //}
        //public async Task<IActionResult> Detail(int? id)
        //{
        //    if (id is null) return BadRequest();
        //    var query = await _dbContext.Courses
        //        .Include(m => m.courseFeatures)
        //        .Include(m => m.CourseTags)
        //        .Include(m => m.Category)
        //        .AsNoTracking()
        //        .Select(m => new CourseDetailVM()
        //        {
        //            Id = m.Id,
        //            ImgUrl = m.ImgUrl,
        //            Name = m.Name,
        //            Desc = m.Desc,
        //            About = m.About,
        //            Apply = m.Apply,
        //            Certification = m.Certification,
        //            Date = m.Date,
        //            CourseFeatures = m.courseFeatures,
        //            CategoryName = m.Category.Name,
        //            CourseTags = m.CourseTags
        //        }).FirstOrDefaultAsync(m => m.Id == id);
        //    if (query is null) return NotFound();
        //    return View(query);

        //}
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id is null) return BadRequest();
        //    var course = await _dbContext.Courses.AsNoTracking().FirstOrDefaultAsync(k => k.Id == id);
        //    if (course is null) return NotFound();
        //    _dbContext.Remove(course);
        //    await _dbContext.SaveChangesAsync();
        //    return RedirectToAction("Index");


        //}
    }
}
