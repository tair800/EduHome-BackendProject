namespace BackEndProject_Edu.Models
{
    public class CourseFeatures : BaseEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int FeatureId { get; set; }
        public Features Features { get; set; }
    }
}
