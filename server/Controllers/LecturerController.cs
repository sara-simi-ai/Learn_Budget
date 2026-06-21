using Microsoft.AspNetCore.Mvc;
using server.Interfaces;
using static server.DTOs.LecturerDto;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerService _lecturerService;
        private readonly ILogger<LecturerController> _logger;

        public LecturerController(ILecturerService lecturerService, ILogger<LecturerController> logger)
        {
            _lecturerService = lecturerService;
            _logger = logger;
        }

        [HttpGet]
        [Route("lecturersList")]
        public async Task<ActionResult<IEnumerable<LecturerResponseDto>>> GetLecturersList()
        {
            _logger.LogInformation("Fetching all lecturers list");
            try
            {
                var lecturers = await _lecturerService.GetLecturersList();
                _logger.LogInformation("Successfully retrieved lecturers list");
                return Ok(lecturers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching lecturers list");
                return StatusCode(500, new { message = "An error occurred while fetching lecturers" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LecturerResponseDto>> GetLecturerById(string id)
        {
            _logger.LogInformation("Fetching lecturer - LecturerId: {LecturerId}", id);
            try
            {
                var lecturer = await _lecturerService.GetLecturerById(id);

                if (lecturer == null)
                {
                    _logger.LogWarning("Lecturer not found - LecturerId: {LecturerId}", id);
                    return NotFound(new { message = $"Lecturer with ID {id} does not exist!" });
                }

            _logger.LogInformation("Searching lecturers by name - SearchTerm: {SearchName}", name);
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    _logger.LogWarning("Search validation failed - SearchTerm is null or empty");
                    return BadRequest(new { message = "The search function must receive a name!" });
                }

                var result = await _lecturerService.SearchLecturersByName(name);
                _logger.LogInformation("Successfully searched lecturers - SearchTerm: {SearchName}", name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching lecturers - SearchTerm: {SearchName}", name);
                return StatusCode(500, new { message = "An error occurred while searching lecturers" });
            }
            {
                _logger.LogError(ex, "Error fetching lecturer - LecturerId: {LecturerId}", id);
                return StatusCode(500, new { message = "An error occurred while fetching lecturer" });
            }
        }_logger.LogInformation("Creating new lecturer - LecturerId: {LecturerId}, Name: {LecturerName}", newLecturer.Id, newLecturer.FullName);
            try
            {
                var lecturer = await _lecturerService.CreateLecturer(newLecturer);

                if (lecturer == null)
                {
                    _logger.LogWarning("Cannot create lecturer - Lecturer already exists - LecturerId: {LecturerId}", newLecturer.Id);
                    return BadRequest(
                        new { message = $"Lecturer with ID {newLecturer.Id} already exists!" });
                }

                _logger.LogInformation("Lecturer created successfully - LecturerId: {LecturerId}", lecturer.Id);
                return Ok(lecturer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating lecturer - LecturerId: {LecturerId}", newLecturer.Id);
                return StatusCode(500, new { message = "An error occurred while creating lecturer" });
            }t(new { message = "The search function must receive a name!" });

            return Ok(await _lecturerService.SearchLecturersByName(name));
        }

        [HttpPost]
        public async Task<ActionResult<LecturerResponseDto>> CreateLecturer([FromBody] LecturerCreateDto newLecturer)
        {_logger.LogInformation("Updating lecturer - LecturerId: {LecturerId}", id);
            try
            {
                var updated = await _lecturerService.UpdateLecturer(id, lecturer);

                if (updated == null)
                {
                    _logger.LogWarning("Cannot update lecturer - Lecturer not found - LecturerId: {LecturerId}", id);
                    return NotFound(new { message = $"Lecturer with ID {id} does not exist!" });
                }

                _logger.LogInformation("Lecturer updated successfully - LecturerId: {LecturerId}", id);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating lecturer - LecturerId: {LecturerId}", id);
                return StatusCode(500, new { message = "An error occurred while updating lecturer" });
            }ge = $"Lecturer with ID {newLecturer.Id} already exists!" });

            return Ok(lecturer);
        }

        [HttpPut("{id}")]
        publ_logger.LogInformation("Deleting lecturer - LecturerId: {LecturerId}", id);
            try
            {
                var deleted = await _lecturerService.DeleteLecturer(id);

                if (!deleted)
                {
                    _logger.LogWarning("Cannot delete lecturer - Lecturer not found - LecturerId: {LecturerId}", id);
                    return NotFound(new { message = $"Lecturer with ID {id} does not exist!" });
                }

                _logger.LogInformation("Lecturer deleted successfully - LecturerId: {LecturerId}", id);
                return Ok("Deleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting lecturer - LecturerId: {LecturerId}", id);
                return StatusCode(500, new { message = "An error occurred while deleting lecturer" });
            }w { message = $"Lecturer with ID {id} does not exist!" });

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLecturer(string id)
        {
            var deleted = await _lecturerService.DeleteLecturer(id);

            if (!deleted)
                return NotFound(new { message = $"Lecturer with ID {id} does not exist!" });

            return Ok("Deleted!");
        }
    }
}