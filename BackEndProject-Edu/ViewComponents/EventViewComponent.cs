﻿using BackEndProject_Edu.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject_Edu.ViewComponents
{
    public class EventViewComponent : ViewComponent
    {
        private readonly EduDbContext _dbContext;

        public EventViewComponent(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var events = _dbContext.Events.ToList();
            return View(await Task.FromResult(events));
        }

    }
}
