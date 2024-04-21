namespace SharedEntityClasses.MainClasses
{
    public class EmployeeDto
    {
        public int Id { get; set; }
       
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Gender { get; set; }
        public string Address { get; set; }

        public int DepartmentId { get; set; }

    }
}
