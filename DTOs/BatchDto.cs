namespace TeacherModule.DTOs
{
    public class BatchDto
    {
        public int Id { get; set; }
        public string BatchName { get; set; }
        public string BatchTiming { get; set; }
        public string BatchType { get; set; }
        public int CourseId { get; set; }
    }
}
