using SharedEntityClasses.MainClasses;

namespace WebApplicationWithSharjeel.Models
{
    public interface IEmployeeRepository
    {
        List<EmployeeDto> GetAll(); // get employee all 
        Task<Employee> GetById(int id); // get all employee by id 
        Task<EmployeeDto> AddEmployee(EmployeeDto employeeDto); // for create
         Task<Employee> UpdateEmployee(Employee employee); // Update Employee

        Task<Employee> DeleteEmployee(int id); // Delete employee


    }
}
