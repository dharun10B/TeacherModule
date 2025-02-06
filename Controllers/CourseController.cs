using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherModule.DTOs;
using TeacherModule.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly TeacherDBContext _context;

        public CourseController(TeacherDBContext context)
        {
            _context = context;
        }

        // 1. Get All Courses Taught by a Teacher
        [HttpGet("{teacherId}/courses")]
        public async Task<IActionResult> GetAllCoursesByTeacher(int teacherId)
        {
            var courses = await _context.Courses
                .Where(c => c.TeacherId == teacherId) // Assuming your Course model has a TeacherId
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    Description = c.Description,
                    TeacherId = c.TeacherId // Include if needed
                })
                .ToListAsync();

            return Ok(courses);
        }

        // 2. Get Course by ID for a Specific Teacher
        [HttpGet("{teacherId}/courses/{courseId}")]
        public async Task<IActionResult> GetCourseByIdForTeacher(int teacherId, int courseId)
        {
            var course = await _context.Courses
                .Where(c => c.Id == courseId && c.TeacherId == teacherId)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    Description = c.Description,
                    TeacherId = c.TeacherId // Include if needed
                })
                .FirstOrDefaultAsync();

            if (course == null) return NotFound();
            return Ok(course);
        }

        // 3. Create a New Course for a Teacher
        [HttpPost("{teacherId}/courses")]
        public async Task<IActionResult> CreateCourse(int teacherId, [FromBody] CourseDto courseDto)
        {
            if (courseDto == null) return BadRequest();

            var course = new Course
            {
                CourseName = courseDto.CourseName,
                Description = courseDto.Description,
                TeacherId = teacherId // Assigning teacherId to the new course
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourseByIdForTeacher), new { teacherId, courseId = course.Id }, courseDto);
        }

        // 4. Update an Existing Course for a Teacher
        [HttpPut("{teacherId}/courses/{courseId}")]
        public async Task<IActionResult> UpdateCourse(int teacherId, int courseId, [FromBody] CourseDto courseDto)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId && c.TeacherId == teacherId);
            if (course == null) return NotFound();

            course.CourseName = courseDto.CourseName;
            course.Description = courseDto.Description;

            await _context.SaveChangesAsync();
            return Ok(courseDto);
        }

        // 5. Delete a Course for a Teacher
        [HttpDelete("{teacherId}/courses/{courseId}")]
        public async Task<IActionResult> DeleteCourse(int teacherId, int courseId)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId && c.TeacherId == teacherId);
            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}