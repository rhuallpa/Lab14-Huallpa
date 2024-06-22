using Lab13.Models;

namespace Lab13.Response
{
    public class EnrollmentResponseV2
    {
        public Student Student { get; set; }
        public Course Course { get; set; }
        public Enrollment Enrollment { get; set; }
    }
}
