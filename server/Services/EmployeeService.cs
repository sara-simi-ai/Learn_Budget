using server.DTOs;
using server.Interfaces;
using static server.DTOs.UserDTOs;

namespace server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeResponseDto>> GetEmployeesListAsync()
        {
            var employees = await _employeeRepository.GetEmployeesListAsync();
            return employees.Select(MapToEmployeeResponseDto);
        }

        public async Task<EmployeeResponseDto?> GetEmployeeByIdAsync(string id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return employee != null ? MapToEmployeeResponseDto(employee) : null;
        }

        public async Task<EmployeeResponseDto?> UpdateEmployee(string id, EmployeeUpdateDto updateDto)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null) return null;

            if (!string.IsNullOrWhiteSpace(updateDto.FirstName)) employee.User.FirstName = updateDto.FirstName;
            if (!string.IsNullOrWhiteSpace(updateDto.LastName)) employee.User.LastName = updateDto.LastName;
            if (updateDto.Email != null) employee.User.Email = updateDto.Email;
            if (updateDto.Phone != null) employee.User.Phone = updateDto.Phone;

            if (!string.IsNullOrWhiteSpace(updateDto.Department)) employee.Department = updateDto.Department;
            
            if (updateDto.TotalCredits.HasValue) 
            {
                employee.TotalCredits = updateDto.TotalCredits.Value;
            }

            var updated = await _employeeRepository.UpdateEmployeeAsync(employee);
            
            return updated != null ? MapToEmployeeResponseDto(updated) : null;
        }

        public async Task<bool> DeleteEmployeeAsync(string id)
        {
            return await _employeeRepository.DeleteEmployeeAsync(id);
        }

        public async Task<EmployeeResponseDto?> AddCreditsAsync(int employeeId, int creditsToAdd)
        {
            if (creditsToAdd <= 0) 
                return null; 

            var updatedEmployee = await _employeeRepository.AddCreditsAsync(employeeId, creditsToAdd);
            
            if (updatedEmployee == null) return null;

            return MapToEmployeeResponseDto(updatedEmployee);          
        }
        private static EmployeeResponseDto MapToEmployeeResponseDto(Models.Employee emp)
        {
            return new EmployeeResponseDto
            {
                Id = emp.User.Id,
                FirstName = emp.User.FirstName,
                LastName = emp.User.LastName,
                Email = emp.User.Email,
                Phone = emp.User.Phone,
                Role = emp.User.Role,
                EmployeeId = emp.Id,
                Department = emp.Department,
                TotalCredits = emp.TotalCredits,
                UsedCredits = emp.UsedCredits,
                AvailableCredits = emp.AvailableCredits
            };
        }
    }
}