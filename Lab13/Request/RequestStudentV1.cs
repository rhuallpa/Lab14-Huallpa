namespace Lab13.Request
{
    public class RequestStudentV1
    {
        public int GradeID { get; set; }   // ID del grado al que pertenece el estudiante
        public string FirstName { get; set; } // Nombre del estudiante
        public string LastName { get; set; }  // Apellido del estudiante
        public string Phone { get; set; }     // Número de teléfono del estudiante
        public string Email { get; set; }     // Correo electrónico del estudiante
    
}
}