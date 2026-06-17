using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Interfaces;
using server.Models;

namespace server.Repositories
{
    public class CourseRegistrationRepository : ICourseRegistrationRepository
    {
        private readonly LearnBudgetContext _context;

        public CourseRegistrationRepository(LearnBudgetContext context)
        {
            _context = context;
        }

        public async Task<CourseRegistration?> GetByIdAsync(int id)
        {
            return await _context.CourseRegistrations
                                 .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<CourseRegistration?> GetByEmployeeAndCourseAsync(int employeeId, int courseId)
        {
            return await _context.CourseRegistrations
                                 .FirstOrDefaultAsync(r => r.EmployeeId == employeeId && r.CourseId == courseId);
        }

        public async Task<IEnumerable<CourseRegistration>> GetEmployeesByCourseIdAsync(int courseId)
        {
            return await _context.CourseRegistrations
                                 .Include(r => r.Employee)
                                 .Where(r => r.CourseId == courseId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<CourseRegistration>> GetCoursesByEmployeeIdAsync(int employeeId)
        {
            return await _context.CourseRegistrations
                                .Include(r => r.Course)
                                .Where(r => r.EmployeeId == employeeId)
                                .ToListAsync();
        }

        public async Task AddRegistrationAsync(CourseRegistration registration)
        {
            await _context.CourseRegistrations.AddAsync(registration);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseRegistrationStatus> ChangeStatusCourseAsync(int registrationId, CourseRegistrationStatus newStatus)
        {
            var registration = await _context.CourseRegistrations.FirstOrDefaultAsync(r => r.Id == registrationId);
            if (registration != null)
            {
                registration.Status = newStatus;
                await _context.SaveChangesAsync();
            }
            return registration?.Status ?? CourseRegistrationStatus.Canceled;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}