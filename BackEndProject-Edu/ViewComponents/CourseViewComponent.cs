﻿using BackEndProject_Edu.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject_Edu.ViewComponents
{
    public class CourseViewComponent : ViewComponent
    {
        private readonly EduDbContext _dbContext;

        public CourseViewComponent(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var courses = _dbContext.Courses.Take(3).ToList();
            return View(await Task.FromResult(courses));
        }
    }
}
