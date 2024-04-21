using SharedEntityClasses.MainClasses;

namespace WebApplicationWithSharjeel.Models
{
    public interface IDepartmentRepository
    {
        List<DepartmentDto> GetAll(); // get employee all 
        Task<Department> GetById(int id); // get all employee by id 
        Task<DepartmentDto> AddDepartment(DepartmentDto departmentDto); // for create

        Task<Department> UpdateDepartment(Department department); // Update Employee

        Task<Department> DeleteDepartment(int id); // Delete employee
    }
}
