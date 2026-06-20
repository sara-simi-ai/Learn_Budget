using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Interfaces;
using server.Models;

namespace server.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly LearnBudgetContext _context;

        public EmployeeRepository(LearnBudgetContext context)
        {
            _context = context;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesListAsync()
        {
            return await _context.Employees
                .Include(e => e.User)
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int userId)
        {
            return await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == userId);
        }

        public async Task<Employee?> UpdateEmployeeAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteEmployeeAsync(int userId)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == userId);
                
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            
            var user = await _context.Users.FindAsync(userId);
            if (user != null) _context.Users.Remove(user);

            await _context.SaveChangesAsync();
            return true;
        }

    public async Task<Employee?> AddCreditsAsync(int employeeId, int creditsToAdd)
    {
        var employee = await _context.Employees
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employee == null) return null;

        employee.TotalCredits += creditsToAdd;

        await _context.SaveChangesAsync();
        return employee;
    }
    }
}