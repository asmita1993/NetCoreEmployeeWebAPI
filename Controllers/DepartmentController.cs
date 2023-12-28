using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeWebAPI.Auth;
using EmployeeWebAPI.Models;
using EmployeeWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeWebAPI.Controllers
{
   // [Authorize(Roles =UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _department;
        public DepartmentController(IDepartmentRepository department)
        {
            _department = department ??
                throw new ArgumentNullException(nameof(department));
        }
        [HttpGet]
        [Route("GetDepartment")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _department.GetDepartments());
        }
        [HttpGet]
        [Route("GetDepartmentByID/{Id}")]
        public async Task<IActionResult> GetDeptById(int Id)

        {
            try
            {
                var department = await _department.GetDepartmentById(Id);

                if(department==null)
                {
                    throw new NotFoundException($"Department with ID {Id} not found");
                }
                return Ok(department);

            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> Post(Department dep)
        {
            try
            {
                var result = await _department.InsertDepartment(dep);

                if (result.DepartmentId == 0)
                {
                    // Log the exception details if needed
                    // e.g., _logger.LogError("Error inserting department: {ErrorMessage}", "Something Went Wrong");

                    return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
                }

                return Ok("Added Successfully");
            }
            catch (Exception ex)
            {
                // Log the exception details if needed
                // e.g., _logger.LogError("Error inserting department: {ErrorMessage}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> Put(Department dep)
        {
            try
            {
                await _department.UpdateDepartement(dep);
                return Ok("Updated Successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency conflict (e.g., return a conflict status code)
                return Conflict("Concurrency conflict. The record has been modified by another user.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return BadRequest($"Error updating department: {ex.Message}");
            }
        }
        [HttpDelete]
        //[HttpDelete("{id}")]
        [Route("DeleteDepartment/{id}")]
        public JsonResult Delete(int id)
        {
            _department.DeleteDepartment(id);
            return new JsonResult("Deleted Successfully");
        }
    }
}

