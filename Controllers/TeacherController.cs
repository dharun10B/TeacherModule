using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherModule.DTOs;
using TeacherModule.Models;

namespace TeacherModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherDBContext _context;

        public TeacherController(TeacherDBContext context)
        {
            _context = context;
        }

        // GET: api/teacher
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            // Fetch all teachers along with their related courses
            var teachers = await _context.Teachers
                .Include(t => t.Courses) // Assuming a Teacher has a collection of Courses
                .ToListAsync();

            // Create a list of TeacherDTOs
            var teacherDtos = teachers.Select(t => new TeacherDto
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                PhoneNumber = t.PhoneNumber,
                Address = t.Address,
                Courses = t.Courses.Select(c => new CourseDto
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    Description = c.Description,
                    TeacherId = c.TeacherId,
                }).ToList()
            }).ToList();

            return Ok(teacherDtos);
        }

        // GET: api/teacher/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            // Fetch the teacher along with their related courses
            var teacher = await _context.Teachers
                .Include(t => t.Courses) // Assuming a Teacher has a collection of Courses
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null) return NotFound();

            // Create a TeacherDTO
            var teacherDto = new TeacherDto
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                Address = teacher.Address,
                Courses = teacher.Courses.Select(c => new CourseDto
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    Description = c.Description,
                    TeacherId =c.TeacherId,
                }).ToList()
            };

            return Ok(teacherDto);
        }

        // POST: api/teacher
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
        }

        // PUT: api/teacher/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/teacher/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}