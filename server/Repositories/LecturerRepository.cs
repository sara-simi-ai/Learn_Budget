using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Interfaces;
using server.Models;

namespace server.Repositories
{
    public class LecturerRepository : ILecturerRepository
    {
        private readonly LearnBudgetContext _context;

        public LecturerRepository(LearnBudgetContext context)
        {
            _context = context;
        }

        public async Task<Lecturer?> GetLecturerById(string id)
        {
            return await _context.Lecturers
                .Include(l => l.CourseDetails)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Lecturer>> GetLecturersList()
        {
            return await _context.Lecturers
                .Include(l => l.CourseDetails)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lecturer>> SearchLecturersByName(string name)
        {
            return await _context.Lecturers
                .Where(l => l.FullName.Contains(name))
                .Include(l => l.CourseDetails)
                .ToListAsync();
        }

        public async Task<Lecturer?> CreateLecturer(Lecturer lecturer)
        {
            _context.Lecturers.Add(lecturer);
            await _context.SaveChangesAsync();
            return lecturer;
        }

        public async Task<Lecturer?> UpdateLecturer(Lecturer lecturer)
        {
            var existing = await _context.Lecturers.FindAsync(lecturer.Id);

            if (existing == null)
                return null;

            existing.FullName = lecturer.FullName;
            existing.Email = lecturer.Email;
            existing.Phone = lecturer.Phone;
            existing.CompanyName = lecturer.CompanyName;
            existing.Cost = lecturer.Cost;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteLecturer(string id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);

            if (lecturer == null)
                return false;

            _context.Lecturers.Remove(lecturer);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}