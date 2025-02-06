namespace TeacherModule.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public string BatchName { get; set; }
        public string BatchTiming { get; set; }
        public string BatchType { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
