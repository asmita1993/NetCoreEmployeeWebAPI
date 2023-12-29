using System;
using EmployeeWebAPI.Auth;
using EmployeeWebAPI.Models;
using EmployeeWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebAPI.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]


    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employee;
        private readonly IDepartmentRepository _department;
        public EmployeeController(IEmployeeRepository employee, IDepartmentRepository department)
        {
            _employee = employee ??
                throw new ArgumentNullException(nameof(employee));
            _department = department ??
                throw new ArgumentNullException(nameof(department));
        }
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employee.GetEmployees());
        }
        [HttpGet]
        [Route("GetEmployeeByID/{Id}")]
        public async Task<IActionResult> GetEmpByID(int Id)
        {
            return Ok(await _employee.GetEmployeeByID(Id));
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post(EmployeeData emp)
        {
            try
            {
                var existingEmployee = await _employee.GetEmployeeByID(emp.EmployeeID);

                if (existingEmployee != null)
                {
                    // Employee already exists, throw custom exception
                    throw new AlredyExisted($"Employee with ID {emp.EmployeeID} already exists");
                }

                // Continue with the insertion logic if the employee doesn't exist
                var result = await _employee.InsertEmployee(emp);

                return Ok("Added Successfully");
            }
            catch (AlredyExisted ex)
            {
                // Catch and handle the custom exception
                return Conflict(ex.Message); // HTTP 409 Conflict status code
            }
            catch (Exception ex)
            {
                // Log the exception details if needed
                // e.g., _logger.LogError("Error inserting employee: {ErrorMessage}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }

        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> Put(EmployeeData emp)
        {
            await _employee.UpdateEmployee(emp);
            return Ok("Updated Successfully");
        }
        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        //[HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var result = _employee.DeleteEmployee(id);
            return new JsonResult("Deleted Successfully");
        }

        [HttpGet]
        [Route("GetDepartment")]
        public async Task<IActionResult> GetAllDepartmentNames()
        {
            return Ok(await _department.GetDepartments());
        }
    }
}

