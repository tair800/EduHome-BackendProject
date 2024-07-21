﻿using BackEndProject_Edu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProject_Edu.ViewComponents
{
    public class TeacherViewComponent : ViewComponent
    {
        private readonly EduDbContext _dbContext;

        public TeacherViewComponent(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var teachers = _dbContext.Teachers.AsNoTracking().ToList();
            return View(await Task.FromResult(teachers));
        }
    }
}
