using Microsoft.EntityFrameworkCore;
using SharedEntityClasses.MainClasses;

namespace WebApplicationWithSharjeel.Models
{
    // this class for Implementation of the IEmployeeRespository Service 
    public class EmployeeRepository(AppDBContext appDBContext) : IEmployeeRepository
    {


        public async Task<EmployeeDto> AddEmployee(EmployeeDto employeeDto)
        {
            Employee employeeEntity = ToEntity(employeeDto);

            var result = await appDBContext.Employees.AddAsync(employeeEntity);
            appDBContext.SaveChangesAsync();

            EmployeeDto entityEmployee = ToDto(result.Entity);
            return entityEmployee;
        }


        //EmployeeDTO to Employee
        private EmployeeDto ToDto(Employee employee)
        {
            EmployeeDto employeedto = new EmployeeDto()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Address = employee.Address,
                Email = employee.Email,
                Gender = employee.Gender,

                DepartmentId = employee.DepartmentId,

                
                
            };
            return employeedto;
        }

        //Employee to EmployeeDTO
        private Employee ToEntity(EmployeeDto employeeDto)
        {
            Employee employee = new Employee()
            {
                Id = employeeDto.Id,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Address = employeeDto.Address,
                Email = employeeDto.Email,
                Gender = employeeDto.Gender,

                DepartmentId = employeeDto.DepartmentId,
                IsActive = true,
                CreatedAt = DateTime.Now

            };
            return employee;
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            var result = await appDBContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                appDBContext.Employees.Remove(result);
                await appDBContext.SaveChangesAsync();
                return result;

            }
            return null;

        }

        public async Task<Employee> GetById(int id)
        {
            return await appDBContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await appDBContext.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);

            if (result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.Email = employee.Email;
                result.Gender = employee.Gender;


                // after saving update the employee 
                appDBContext.Employees.Update(result);
                //appDbContext.Employees.Add(result);

                await appDBContext.SaveChangesAsync();

                return result;

            }
            return null;
        }

        // craeta a variable which pass date like 10 date
        // convert  this variable into dateTime format into a variable
        //put the variable in this Datetime.Now place. 
        public List<EmployeeDto> GetAll()
        {
            var date = new DateTime(2024, 4, 10); 
            var emp = appDBContext.Employees.Where(e => e.CreatedAt < date).ToList();  // where perform, it will get the data only active employee

            List<EmployeeDto> entityEmployee = ToDtos(emp);
            return entityEmployee;
        }
        public List<EmployeeDto> ToDtos(List<Employee> employees)
        {
            var result = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                //if (employee.IsActive == false)
                //    continue; // condition must false then it will persue further.     

                EmployeeDto employeedto = new EmployeeDto()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Address = employee.Address,
                    Email = employee.Email,
                    Gender = employee.Gender,
                    
                    DepartmentId = employee.DepartmentId,
                };
                result.Add(employeedto);
            }
            return result;
        }

     }
}
