using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedEntityClasses.MainClasses;

namespace WebApplicationWithSharjeel.Models
{
    public class DepartmentRespository(AppDBContext appDBContext) : IDepartmentRepository
    {

        public async Task<DepartmentDto> AddDepartment(DepartmentDto departmentDto)
        {
            Department departmentEntity = ToEntity(departmentDto); // we are converting Employee to EmployeeDTO
            var result = await appDBContext.Departments.AddAsync(departmentEntity);// we are adding department into database
            appDBContext.SaveChanges();

            // Dto conversion
            DepartmentDto dtoDepartment = ToDto(result.Entity);
            return dtoDepartment;

        }

        //DeparmentDto to Entity
        private DepartmentDto ToDto(Department department)
        {
            DepartmentDto departmentdto = new DepartmentDto()
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,

            };
            return departmentdto;
        }

        // Entity to DTO
        private Department ToEntity(DepartmentDto departmentDto)
        {
            Department department = new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                IsActive = true,
                CreatedAt = DateTime.Now,

            };
            return department;
        }

        public async Task<Department> DeleteDepartment(int id)
        {
            var result = await appDBContext.Departments.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                appDBContext.Departments.Remove(result);
                await appDBContext.SaveChangesAsync();
                return result;

            }
            return null;
        }

        public List<DepartmentDto> GetAll()
        {
            var dep = appDBContext.Departments.ToList();
            
            List<DepartmentDto> departmentDtos = AllDtos(dep); // conversion of the dapartmentDto here
            return departmentDtos;
        }

        private List<DepartmentDto> AllDtos(List<Department> departments)
        {
            var depart = new List<DepartmentDto>(); // took a list to save data of departmentDto
            foreach (var department in departments)
            {
               
            DepartmentDto departmentDto = new DepartmentDto()
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description,

                };
                depart.Add(departmentDto);
            }


            return depart;
        }


        public async Task<Department> GetById(int id)
        {
            return await appDBContext.Departments.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Department> UpdateDepartment(Department department)
        {
            var result = await appDBContext.Departments.FirstOrDefaultAsync(e => e.Id == department.Id);
            if (result != null)
            {
                result.Id = department.Id;
                result.Name = department.Name;
                result.Description = department.Description;


                appDBContext.Departments.Update(result);

                await appDBContext.SaveChangesAsync();

                return result;

            }
            return null;
        }
    }
}

