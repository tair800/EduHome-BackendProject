using BackEndProject_Edu.Data;
using BackEndProject_Edu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BackEndProject_Edu.Controllers
{
    public class BasketController : Controller
    {
        private readonly EduDbContext _dbContext;

        public BasketController(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();
            var existCourse = await _dbContext.Courses.FirstOrDefaultAsync(p => p.Id == id);
            if (existCourse is null) return BadRequest();

            string basket = Request.Cookies["basket"];

            List<BasketVM> list;
            if (basket is null)
            {
                list = new();
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
            var existBasketProduct = list.FirstOrDefault(m => m.Id == id);
            if (existBasketProduct is null)
            {
                list.Add(new BasketVM()
                {
                    Id = existBasketProduct.Id,
                    BasketCount = 1
                });
            }
            else
            {
                existBasketProduct.BasketCount++;
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(list));
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> ShowBasket()
        {
            string basket = Request.Cookies["basket"];

            List<BasketVM> list;
            if (basket is null)
            {
                list = new();
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                foreach (var basketProduct in list)
                {
                    var existCourse = await _dbContext.Courses.FirstOrDefaultAsync(m => m.Id == basketProduct.Id);
                    basketProduct.Name = existCourse.Name;
                    basketProduct.ImageUrl = existCourse.ImgUrl;
                }
            }
            return View(list);
        }

        public IActionResult Delete(int? id)
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var deletedProduct = list.FirstOrDefault(m => m.Id == id);
            if (deletedProduct is not null)
            {
                list.Remove(deletedProduct);
                Response.Cookies.Append("basket", JsonConvert.SerializeObject(list));

            }
            return RedirectToAction("ShowBasket");

        }
    }
}
