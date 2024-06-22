using Lab13.Models;
using Lab13.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCustomController : ControllerBase
    {
        private readonly SchoolContext _context;

        public CourseCustomController(SchoolContext context)
        {
            _context = context;
        }

        // POST: api/CourseCustom/InsertCourse
        [HttpPost("InsertCourse")]
        public async Task<ActionResult<Course>> InsertCourse(RequestCourseV1 requestCourseV1)
        {
            Course course = new Course
            {
                Name = requestCourseV1.Name,
                Credit = requestCourseV1.Credit,
                // Otras propiedades del curso según sea necesario
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("InsertCourse", new { id = course.CourseID }, course);
        }

        // DELETE: api/CourseCustom/DeleteCourse
        [HttpDelete("DeleteCourse")]
        public async Task<IActionResult> DeleteCourse(RequestCourseV2 requestCourseV2)
        {
            var id = requestCourseV2.CourseID;

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        // POST: api/CourseCustom/SearchStudents
        [HttpPost("SearchStudents")]
        public ActionResult<IEnumerable<Student>> SearchStudents(StudentRequestV1 request)
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(request.FirstName))
            {
                query = query.Where(s => s.FirstName.Contains(request.FirstName));
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                query = query.Where(s => s.LastName.Contains(request.LastName));
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(s => s.Email.Contains(request.Email));
            }

            // Ordenar de forma descendente por apellido
            query = query.OrderByDescending(s => s.LastName);

            var students = query.ToList();

            return students;
        }
    }
}
