using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Interfaces;
using server.Models;

namespace server.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LearnBudgetContext _context;

        public CourseRepository(LearnBudgetContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        // public async Task<IEnumerable<Course>> GetCoursesWithEmployeesAsync()
        // {
        //     return await _context.Courses
        //                  .Include(c => c.Employees)
        //                  .ToListAsync();
        // }

        public async Task<IEnumerable<Course>> GetAvailableCoursesAsync()
        {
           var now = DateTime.Now;

           return await _context.Courses
                        .Where(c => c.RegistrationDeadline > now && c.PlacesLeft > 0)
                        .ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _context.Courses
                        .Include(c => c.CourseDetail)
                        .ThenInclude(cd => cd!.Lecturer)
                        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course?> GetCourseWithEmployeesAsync(int id)
        {
            return await _context.Courses
                        .Include(c => c.Employees)
                        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Course>> SearchCoursesByNameAsync(string name)
        {
            return await _context.Courses
                .Where(c => c.Name.Contains(name))
                //.Include(c => c.CourseDetail)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> SearchCoursesByLecturerNameAsync(string lecturerName)
        {
                return await _context.Courses
                .Where(c => c.CourseDetail != null && 
                            c.CourseDetail.Lecturer != null && 
                            c.CourseDetail.Lecturer.FullName.Contains(lecturerName))
                .Include(c => c.CourseDetail)
                    .ThenInclude(cd => cd!.Lecturer)
                .ToListAsync();
        }
        
        public async Task<Course> CreateCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<Course?> UpdateCourseAsync(Course course)
        {
            var existCourse = await _context.Courses
                                        .Include(c => c.CourseDetail)
                                        .FirstOrDefaultAsync(c => c.Id == course.Id);

            if (existCourse == null)
                return null;

            _context.Entry(existCourse).CurrentValues.SetValues(course);

            if (course.CourseDetail != null)
            {
                if (existCourse.CourseDetail == null)
                {
                    existCourse.CourseDetail = new CourseDetail { CourseId = existCourse.Id };
                }
                _context.Entry(existCourse.CourseDetail).CurrentValues.SetValues(course.CourseDetail);
            }

            if (course.CourseDetail != null)
            {
                if (existCourse.CourseDetail == null)
                {
                    existCourse.CourseDetail = new CourseDetail { CourseId = existCourse.Id };
                }
                
                _context.Entry(existCourse.CourseDetail).CurrentValues.SetValues(course.CourseDetail);
            }

            await _context.SaveChangesAsync();
            return existCourse;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            int rowsDeleted = await _context.Courses
                                            .Where(c => c.Id == id)
                                            .ExecuteDeleteAsync();

            return rowsDeleted > 0;
        }
        
    }
}
