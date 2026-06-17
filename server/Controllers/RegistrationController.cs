using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseRegistrationResponseDto>> GetRegistrationById(int id)
        {
            try
            {
                var registration = await _registrationService.GetRegistrationByIdAsync(id);
                if (registration == null)
                    return NotFound(new { message = $"Registration with ID {id} not found." });

                return Ok(registration);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<CourseRegistrationResponseDto>>> GetEmployeeRegistrations(int employeeId)
        {
            try
            {
                var registrations = await _registrationService.GetEmployeeRegistrationsAsync(employeeId);
                return Ok(registrations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("course/{courseId}/registrations")]
        public async Task<ActionResult<IEnumerable<CourseReportEntryDto>>> GetCourseRegistrations(int courseId)
        {
            try
            {
                var registrations = await _registrationService.GetCourseRegistrationsAsync(courseId);
                return Ok(registrations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<CourseRegistrationResponseDto>> RegisterToCourse([FromBody] EmployeeRegistrationDto registrationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _registrationService.RegisterToCourseAsync(registrationDto);
                if (result == null)
                    return BadRequest(new { message = "Error registering to course. Employee or course not found, or already registered." });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{registrationId}/status")]
        public async Task<ActionResult> ChangeRegistrationStatus(int registrationId, [FromQuery] int status)
        {
            try
            {
                var result = await _registrationService.ChangeRegistrationStatusAsync(registrationId, status);
                if (!result)
                    return BadRequest(new { message = "Invalid status or registration not found." });

                return Ok(new { message = "Registration status updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{registrationId}/cancel")]
        public async Task<ActionResult> CancelRegistration(int registrationId, [FromQuery] int employeeId)
        {
            try
            {
                var result = await _registrationService.CancelRegistrationAsync(registrationId, employeeId);
                if (!result)
                    return NotFound(new { message = $"Registration with ID {registrationId} not found for employee {employeeId}." });

                return Ok(new { message = "Registration cancelled successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
