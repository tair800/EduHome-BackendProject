using BackEndProject_Edu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.Controllers
{
    public class EventController : Controller
    {
        private readonly EduDbContext _dbContext;

        public EventController(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return BadRequest();
            var item = _dbContext.Events.Include(k => k.Speakers).FirstOrDefault(k => k.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }
    }
}
