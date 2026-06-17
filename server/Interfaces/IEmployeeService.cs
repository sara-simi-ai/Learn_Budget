using server.DTOs;
 
namespace server.Interfaces
{
    public interface IEmployeeService
    {
         Task<IEnumerable<EmployeeResponseDto>> GetEmployeesListAsync();
        Task<EmployeeResponseDto?> GetEmployeeByIdAsync(string id);
        Task<EmployeeResponseDto?> UpdateEmployee(string id, EmployeeUpdateDto updateDto);
        Task<bool> DeleteEmployeeAsync(string id);
        Task<EmployeeResponseDto?> AddCreditsAsync(int employeeId, int creditsToAdd);
    }
}
 