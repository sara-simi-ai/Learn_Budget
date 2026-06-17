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

        public LecturerController(ILecturerService lecturerService)
        {
            _lecturerService = lecturerService;
        }

        [HttpGet]
        [Route("lecturersList")]
        public async Task<ActionResult<IEnumerable<LecturerResponseDto>>> GetLecturersList()
        {
            return Ok(await _lecturerService.GetLecturersList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LecturerResponseDto>> GetLecturerById(string id)
        {
            var lecturer = await _lecturerService.GetLecturerById(id);

            if (lecturer == null)
                return NotFound(new { message = $"Lecturer with ID {id} does not exist!" });

            return Ok(lecturer);
        }

        [HttpGet("SearchLecturersByName")]
        public async Task<ActionResult<IEnumerable<LecturerResponseDto>>> SearchLecturersByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest(new { message = "The search function must receive a name!" });

            return Ok(await _lecturerService.SearchLecturersByName(name));
        }

        [HttpPost]
        public async Task<ActionResult<LecturerResponseDto>> CreateLecturer([FromBody] LecturerCreateDto newLecturer)
        {
            var lecturer = await _lecturerService.CreateLecturer(newLecturer);

            if (lecturer == null)
                return BadRequest(
                    new { message = $"Lecturer with ID {newLecturer.Id} already exists!" });

            return Ok(lecturer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LecturerResponseDto>> UpdateLecturer(string id, [FromBody] LecturerUpdateDto lecturer)
        {
            var updated = await _lecturerService.UpdateLecturer(id, lecturer);

            if (updated == null)
                return NotFound(new { message = $"Lecturer with ID {id} does not exist!" });

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