using server.DTOs;

namespace server.Interfaces
{
    public interface IRegistrationService
    {
        Task<CourseRegistrationResponseDto?> GetRegistrationByIdAsync(int id);
        Task<IEnumerable<CourseRegistrationResponseDto>> GetEmployeeRegistrationsAsync(int employeeId);
        Task<IEnumerable<CourseReportEntryDto>> GetCourseRegistrationsAsync(int courseId);
        Task<CourseRegistrationResponseDto?> RegisterToCourseAsync(EmployeeRegistrationDto dto);
        Task<bool> ChangeRegistrationStatusAsync(int registrationId, int newStatus);
        Task<bool> CancelRegistrationAsync(int registrationId);
    }
}