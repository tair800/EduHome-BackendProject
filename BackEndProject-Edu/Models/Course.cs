namespace BackEndProject_Edu.Models
{
    public class Course : BaseEntity
    {
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public string About { get; set; }
        public string Apply { get; set; }
        public string Certification { get; set; }
        public string Features { get; set; }
        //public ICollection<CourseBlog> CoursesBlogs { get; set; }
        //public int EventId { get; set; }
        //public Event Event { get; set; }

    }
}
