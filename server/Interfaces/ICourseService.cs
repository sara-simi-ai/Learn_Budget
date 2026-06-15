using server.DTOs;

namespace server.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseResponseDto>> GetAllCoursesAsync();
        Task<IEnumerable<CourseResponseDto>> GetAvailableCoursesAsync();
        Task<CourseResponseDto?> GetCourseByIdAsync(int id);
        Task<CourseResponseDto?> CreateCourseAsync(CreateCourseDto dto);
        Task<CourseResponseDto?> UpdateCourseAsync(int id, UpdateCourseDto dto);
        Task<bool> DeleteCourseAsync(int id);
    }
}