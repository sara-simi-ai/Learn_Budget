
using server.Models;

namespace server.Interfaces
{
    public interface ICourseRegistrationRepository
    {
        Task<CourseRegistration?> GetByIdAsync(int id);
        Task<CourseRegistration?> GetByEmployeeAndCourseAsync(int employeeId, int courseId);
        Task<IEnumerable<CourseRegistration>> GetEmployeesByCourseIdAsync(int courseId);
        Task<IEnumerable<CourseRegistration>> GetCoursesByEmployeeIdAsync(int employeeId);
        Task AddRegistrationAsync(CourseRegistration registration);
        Task<CourseRegistrationStatus> ChangeStatusCourseAsync(int registrationId, CourseRegistrationStatus newStatus);
        Task SaveChangesAsync();
    }
}