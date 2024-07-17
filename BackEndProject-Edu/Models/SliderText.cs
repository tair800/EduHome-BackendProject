﻿using System.ComponentModel.DataAnnotations;

namespace BackEndProject_Edu.Models
{
    public class SliderText : BaseEntity
    {
        [Required, StringLength(100)]
        public string Title { get; set; }
        [StringLength(200)]
        public string Desc { get; set; }
        public string ImgUrl { get; set; }
    }
}
