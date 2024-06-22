using Lab13.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Lab13.Request;

namespace Lab13.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        //Crear un endpoint lista los estudiantes que busque por nombre, apellido y email ordenado de forma descendente por apellido
        public List<Student> Listv1([FromQuery] StudentRequestV1 request)
        {
            var students = _context.Students
                .Where(s => (request.FirstName == null || s.FirstName == request.FirstName) &&
                                           (request.LastName == null || s.LastName == request.LastName) &&
                                                                      (request.Email == null || s.Email == request.Email))
                .OrderByDescending(s => s.LastName)
                .ToList();
            return students;
        }
        //Crear un endpoint lista los estudiantes que busque por nombre y grado y que retorne todos los datos de los estudiantes y del grado que pertenecen ordenado de forma descendente por nombre del curso

        public class StudentRequestV2
        {
            public string FirstName { get; set; }
            public string Grade { get; set; }
        }

        public class StudentResponseV2
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public Grade Grade { get; set; }
        }
    }
}
