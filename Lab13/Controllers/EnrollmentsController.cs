using Lab13.Models;
using Lab13.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Lab13.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public EnrollmentsController(SchoolContext context)
        {
            _context = context;
        }

        //Crear un endpoint lista los estudiantes que busque por nombre y grado y que retorne todos los datos de los estudiantes y del grado que pertenecen ordenado de forma descendente por nombre del curso

        [HttpGet]
        public List<Enrollment> Listv1([FromQuery] EnrollmentRequestV1 request)
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => (request.StudentName == null || e.Student.FirstName == request.StudentName) &&
                                           (request.GradeName == null || e.Student.Grade.Name == request.GradeName))
                .OrderByDescending(e => e.Course.Name)
                .ToList();
            return enrollments;
        }

        //Crear un endpoint lista de estudiantes matriculados y búsqueda sea por el nombre del curso, debe retornar los datos del estudiante, curso y matrícula ordenados por curso y luego por apellido

        [HttpGet]
        public List<Enrollment> Listv2([FromQuery] EnrollmentRequestV2 request)
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => request.CourseName == null || e.Course.Name == request.CourseName)
                .OrderBy(e => e.Course.Name)
                .ThenBy(e => e.Student.LastName)
                .ToList();
            return enrollments;
        }

        //Crear un endpoint lista de estudiantes matriculados y búsqueda sea por el grado, debe retornar los datos del estudiante, curso y matrícula ordenados por curso y luego por apellido

        public class EnrollmentRequestV3
        {
            public string GradeName { get; set; }
        }

        public class EnrollmentResponseV3
        {
            public Student Student { get; set; }
            public Course Course { get; set; }
            public Enrollment Enrollment { get; set; }
        }

        [HttpGet]
        public List<EnrollmentResponseV3> Listv3([FromQuery] EnrollmentRequestV3 request)
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => request.GradeName == null || e.Student.Grade.Name == request.GradeName)
                .OrderBy(e => e.Course.Name)
                .ThenBy(e => e.Student.LastName)
                .Select(e => new EnrollmentResponseV3
                {
                    Student = e.Student,
                    Course = e.Course,
                    Enrollment = e
                })
                .ToList();
            return enrollments;
        }
    }
}
