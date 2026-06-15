using server.Models;
 
namespace server.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<IEnumerable<Course>> GetAvailableCoursesAsync();
        Task<Course?> GetCourseByIdAsync(int id);
        Task<Course?> GetCourseWithEmployeesAsync(int id);
        Task<IEnumerable<Course>> SearchCoursesByNameAsync(string name);
        Task<IEnumerable<Course>> SearchCoursesByLecturerNameAsync(string lecturerName);
        Task<Course> CreateCourseAsync(Course course);
        Task<Course?> UpdateCourseAsync(Course course);
        Task<bool> DeleteCourseAsync(int id);
    }
}