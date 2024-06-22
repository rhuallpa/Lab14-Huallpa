namespace Lab13.Request
{
    public class StudentRequestV1
    {
        //Crear un endpoint lista los estudiantes que busque por nombre, apellido y email ordenado de forma descendente por apellido
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
