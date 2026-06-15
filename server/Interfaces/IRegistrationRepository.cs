using server.Models;
 
namespace server.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<CourseRegistration> RegisterToCourseAsync(CourseRegistration courseRegistration);
        Task<bool> CancelRegistrationAsync(int registrationId, int employeeId);
        Task<IEnumerable<CourseRegistration>> GetCourseReportAsync(int courseId);
    }
}
 