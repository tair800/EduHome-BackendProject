using BackEndProject_Edu.Models;

namespace BackEndProject_Edu.ViewModels
{
    public class CourseVM
    {
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Desc { get; set; }
        public string About { get; set; }
        public string Apply { get; set; }
        public string Certificion { get; set; }

        public IEnumerable<Blog> Blogs { get; set; }
    }
}
