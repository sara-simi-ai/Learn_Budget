using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("employeesList")]
        public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> GetEmployeesList()
        {
            var employees = await _employeeService.GetEmployeesListAsync();
            return Ok(employees);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<EmployeeResponseDto>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound(new { message = $"Employee with ID {id} does not exist!" });
            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeResponseDto>> UpdateEmployee(int id, [FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            try
            {
                var updatedEmployee = await _employeeService.UpdateEmployee(id, employeeUpdateDto);
                if (updatedEmployee == null)
                    return NotFound(new { message = $"Employee with ID {id} does not exist!" });
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeService.DeleteEmployeeAsync(id);
            if (!employee)
                return NotFound(new { message = $"Employee with ID {id} does not exist!" });
            return Ok("Deleted!");
        }

        [HttpPost]
        [Route("{employeeId}/addCredits")]
        public async Task<ActionResult<EmployeeResponseDto>> AddCredits(int employeeId, [FromBody] int request)
        {
            try
            {
                var updatedEmployee = await _employeeService.AddCreditsAsync(employeeId, request);
                if (updatedEmployee == null)
                    return BadRequest(new { message = $"Invalid credits amount or Employee with ID {employeeId} does not exist!" });
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

}
