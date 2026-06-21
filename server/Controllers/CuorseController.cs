
using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Interfaces;

namespace server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ICourseService courseService, ILogger<CourseController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        [HttpGet("coursesList")]
        public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetAllCourses()
        {
            _logger.LogInformation("Fetching all courses");
            try
            {
                var courses = await _courseService.GetAllCoursesAsync();
                _logger.LogInformation("Successfully retrieved all courses");
                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all courses");
                return StatusCode(500, new { message = "An error occurred while fetching courses" });
            }
        }

        [HttpGet("availableCourses")]
        public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetAvailableCourses()
        {
            _logger.LogInformation("Fetching available courses");
            try
            {
                var courses = await _courseService.GetAvailableCoursesAsync();
                _logger.LogInformation("Successfully retrieved available courses");
                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching available courses");
                return StatusCode(500, new { message = "An error occurred while fetching available courses" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseResponseDto>> GetCourseById(int id)
        {
            _logger.LogInformation("Fetching course - CourseId: {CourseId}", id);
            try
            {
                var course = await _courseService.GetCourseByIdAsync(id);
                if(course == null)
                {
                    _logger.LogWarning("Course not found - CourseId: {CourseId}", id);
                    return NotFound(new { message = $"Course with ID {id} does not exist!" });
                }
            _logger.LogInformation("Creating new course - CourseName: {CourseName}", newCourse.Name);
            try
            {
                var createdCourse = await _courseService.CreateCourseAsync(newCourse);
                if (createdCourse == null)
                {
                    _logger.LogWarning("Failed to create course - CourseName: {CourseName}", newCourse.Name);
                    return BadRequest(new { message = $"Error creating course!" });
                }
                _logger.LogInformation("Course created successfully - CourseId: {CourseId}, CourseName: {CourseName}", createdCourse.Id, createdCourse.Name);
                return Ok(createdCourse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating course - CourseName: {CourseName}", newCourse.Name);

        [HttpPost]
        public async Task<ActionResult<CourseResponseDto>> CreateCourse([FromBody] CreateCourseDto newCourse)
        {
            try
            {
                var createdCourse = await _courseService.CreateCourseAsync(newCourse);
                if (createdCourse == null)
                    return BadRequest(new { message = $"Error creating course!" });
                return Ok(createdCourse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            _logger.LogInformation("Updating course - CourseId: {CourseId}, CourseName: {CourseName}", id, updCourse.Name);
            try
            {
                var updatedCourse = await _courseService.UpdateCourseAsync(id, updCourse);
                if (updatedCourse == null)
                {
                    _logger.LogWarning("Cannot update course - Course not found - CourseId: {CourseId}", id);
                    return NotFound(new { message = $"Course with ID {id} does not exist!" });
                }
                _logger.LogInformation("Course updated successfully - CourseId: {CourseId}", id);
                return Ok(updatedCourse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating course - CourseId: {CourseId}", id);   var updatedCourse = await _courseService.UpdateCourseAsync(id, updCourse);
                if (updatedCourse == null)
                    return NotFound(new { message = $"Course with ID {id} does not exist!" });
                return Ok(updatedCourse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }_logger.LogInformation("Deleting course - CourseId: {CourseId}", id);
            try
            {
                var isDeleted = await _courseService.DeleteCourseAsync(id);
                if(!isDeleted)
                {
                    _logger.LogWarning("Cannot delete course - Course not found - CourseId: {CourseId}", id);
                    return NotFound(new { message = $"Course with ID {id} does not exist!" });
                }
                _logger.LogInformation("Course deleted successfully - CourseId: {CourseId}", id);
                return Ok("Deleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting course - CourseId: {CourseId}", id);
                return StatusCode(500, new { message = "An error occurred while deleting course" });
            }
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var isDeleted = await _courseService.DeleteCourseAsync(id);
            if(!isDeleted)
                return NotFound(new { message = $"Course with ID {id} does not exist!" });
            return Ok("Deleted!");
        }
    }


}

