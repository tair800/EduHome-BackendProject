using BackEndProject_Edu.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject_Edu.ViewComponents
{
    public class SettingHeaderViewComponent : ViewComponent
    {
        private readonly EduDbContext _dbContext;

        public SettingHeaderViewComponent(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setting = _dbContext.Settings.ToDictionary(k => k.Key, k => k.Value);
            return View(await Task.FromResult(setting));
        }
    }
}
