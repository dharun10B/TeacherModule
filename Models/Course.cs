namespace TeacherModule.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }

        // Foreign Key
        public int TeacherId { get; set; }

        // Navigation properties
        public Teacher Teacher { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
