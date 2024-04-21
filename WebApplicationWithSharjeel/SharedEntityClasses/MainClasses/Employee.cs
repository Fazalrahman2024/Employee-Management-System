using System.ComponentModel.DataAnnotations.Schema;

namespace SharedEntityClasses.MainClasses
{
    public class Employee
    {
        public int Id { get; set; }
        public bool IsActive {get; set; } // dto use
        public DateTime CreatedAt { get; set; } = DateTime.Now;// dto use
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Gender { get; set; }
        public string Address { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId {  get; set; } // foreign key for database

        public Department? Department { get; set; }


    }
}
