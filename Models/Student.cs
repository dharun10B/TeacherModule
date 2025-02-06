namespace TeacherModule.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        // Navigation property
        public ICollection<Course> Courses { get; set; }
    }
}
