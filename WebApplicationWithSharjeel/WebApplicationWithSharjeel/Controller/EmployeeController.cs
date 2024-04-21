using Microsoft.AspNetCore.Mvc;
using SharedEntityClasses.MainClasses;
using WebApplicationWithSharjeel.Models;

namespace WebApplicationWithSharjeel.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }


        // Employee record Fatching from database
        [HttpGet]
        public ActionResult GetEmployees()
        {
            try
            {
                return Ok(employeeRepository.GetAll());

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database for Employee");

            }

        }

        //Employee record Fatching from database by Id

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await employeeRepository.GetById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in fatching data from the database");
            }

        }


        //  Create New employee

        [HttpPost]

        public async Task<ActionResult<EmployeeDto>> CreateEmployee(EmployeeDto employeeDto)
        {
            try
            {
                if (employeeDto == null)
                {
                    return BadRequest();
                }
                var emp = await employeeRepository.GetById(employeeDto.Id);
                if (emp != null)
                {
                    ModelState.AddModelError("id", "Employee Id is already exists");
                    return BadRequest(ModelState);
                }

                var createdEmployee = await employeeRepository.AddEmployee(employeeDto);

                return createdEmployee;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error to fatching data from the database");

            }
        }
        
        // for update employee
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            try
            {
                var employeeToUpdate = await employeeRepository.GetById(employee.Id);

                if (employeeToUpdate == null)
                {
                    return NotFound($"Employee with id={employee.Id} not found");
                }

                return await employeeRepository.UpdateEmployee(employee);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                      "Error updating in the database");
            }
        }

        // Delete Employee
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleateEmployee(int id)
        {
            try
            {
                var employeeToDelete = await employeeRepository.GetById(id);
                if (employeeToDelete == null)
                {
                    return NotFound($"Employee with Id={id} not found");
                }
                return await employeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                       "Error deleting in the database");
            }
        }


    }
}
