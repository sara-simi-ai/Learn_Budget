using server.DTOs;
 
namespace server.Interfaces
{
    public interface IRegistrationService
    {
        Task<CourseRegistrationResponseDto> RegisterToCourseAsync(EmployeeRegistrationDto dto);
        Task<bool> CancelRegistrationAsync(int registrationId, int employeeId);
        Task<IEnumerable<CourseReportEntryDto>> GetCourseReportAsync(int courseId);
    }
}
 