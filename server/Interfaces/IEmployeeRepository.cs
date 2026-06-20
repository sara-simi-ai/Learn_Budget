using server.Models;

namespace server.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<IEnumerable<Employee>> GetEmployeesListAsync();
        Task<Employee?> GetEmployeeByIdAsync(int userId);
        Task<Employee?> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int userId);
        Task<Employee?> AddCreditsAsync(int employeeId, int creditsToAdd);
    }
} 