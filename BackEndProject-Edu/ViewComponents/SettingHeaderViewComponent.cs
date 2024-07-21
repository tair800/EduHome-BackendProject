using BackEndProject_Edu.Data;
using BackEndProject_Edu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject_Edu.ViewComponents
{
    public class SettingHeaderViewComponent : ViewComponent
    {
        private readonly EduDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;


        public SettingHeaderViewComponent(EduDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.FullName = user.FullName;
            }
            var setting = _dbContext.Settings.ToDictionary(k => k.Key, k => k.Value);
            return View(await Task.FromResult(setting));
        }
    }
}
