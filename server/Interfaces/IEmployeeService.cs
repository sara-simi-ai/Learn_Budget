using server.DTOs;
 
namespace server.Interfaces
{
    public interface IEmployeeService
    {
         Task<IEnumerable<EmployeeResponseDto>> GetAllEmployeesAsync();
        Task<EmployeeResponseDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeResponseDto> CreateEmployeeAsync(CreateEmployeeDto dto);
        Task<EmployeeResponseDto?> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<EmployeeResponseDto?> AddCreditsAsync(int employeeId, int creditsToAdd);
    }
}
 