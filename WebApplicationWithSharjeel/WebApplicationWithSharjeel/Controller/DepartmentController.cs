using Microsoft.AspNetCore.Mvc;
using SharedEntityClasses.MainClasses;
using WebApplicationWithSharjeel.Models;

namespace WebApplicationWithSharjeel.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository deparmentRespository;

        public DepartmentController(IDepartmentRepository deparmentRespository)
        {
            this.deparmentRespository = deparmentRespository;
        }


        // Get all Department 
        [HttpGet]
        public ActionResult GetDepartments()
        {
            try
            {
                return Ok(deparmentRespository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the Database");
            }
        }

        // Getting Data by ID of the department
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            try
            {
                var result = await deparmentRespository.GetById(id);

                if (result == null)
                {
                    return NotFound();
                }
                // miss
                return result;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving datanfrom the Database");
            }

        }

        // Creating data for department

        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> CreateDepartment(DepartmentDto departmentDto)
        {
            try
            {
                if (departmentDto == null)
                {
                    return BadRequest();
                }
                else
                {
                    var dep = await deparmentRespository.GetById(departmentDto.Id);
                    if (dep != null)
                    {
                        ModelState.AddModelError("DeparmentId", "Department Id is alreday in exists");
                        return BadRequest(ModelState);

                    }
                    var createdDepartment = await deparmentRespository.AddDepartment(departmentDto);
                    return createdDepartment;
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database");
            }
        }

        // Update the record of the department
        [HttpPut]
        public async Task<ActionResult<Department>> UpdateDepartment(Department department)
        {
            try
            {
                var departmentToUpdate = await deparmentRespository.GetById(department.Id);

                if (departmentToUpdate == null)
                {
                    return NotFound($"Department with id={department.Id} not found");
                }

                return await deparmentRespository.UpdateDepartment(department);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                      "Error updating data in the department");
            }
        }


        // Delete Employee
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Department>> DeleateDepartment(int id)
        {
            try
            {
                var departmentToDelete = await deparmentRespository.GetById(id);
                if (departmentToDelete == null)
                {
                    return NotFound($"Employee with Id={id} not found");
                }
                return await deparmentRespository.DeleteDepartment(id);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                       "Error deleting in the database of the department");
            }
        }


    }
}
