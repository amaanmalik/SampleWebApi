using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Domains;
using Sample.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> ping()
        {
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Save(EmployeeDto employeeDto)
        {
            try
            {
                // Validate and sanitize inputs
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Employee employee = new Employee
                {
                    Id = employeeDto.Id,
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    Age = employeeDto.Age,
                    Gender = employeeDto.Gender
                };

                // Call service to save employee
                _employeeService.Save(employee);

                // Return success response
                return Ok(new { Message = "Employee saved successfully." });
            }
            catch (Exception ex)
            {
                // Handle and log any exceptions
                return StatusCode(500, new { Error = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll(string firstNameFilter, string lastNameFilter, string genderFilter)
        {
            try
            {
                // Call service to get employees with filters
                var employees = _employeeService.GetAll(firstNameFilter, lastNameFilter, genderFilter);

                // Return employees
                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Handle and log any exceptions
                return StatusCode(500, new { Error = $"An error occurred: {ex.Message}" });
            }
        }
    }    
}
