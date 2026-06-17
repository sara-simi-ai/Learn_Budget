using server.DTOs;
using server.Interfaces;
using server.Models;

namespace server.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ICourseRegistrationRepository _registrationRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public RegistrationService(
            ICourseRegistrationRepository registrationRepository,
            ICourseRepository courseRepository,
            IEmployeeRepository employeeRepository)
        {
            _registrationRepository = registrationRepository;
            _courseRepository = courseRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<CourseRegistrationResponseDto?> GetRegistrationByIdAsync(int id)
        {
            var registration = await _registrationRepository.GetByIdAsync(id);
            if (registration == null)
                return null;

            var employee = await _employeeRepository.GetEmployeeByIdAsync(registration.EmployeeId);
            var course = await _courseRepository.GetCourseByIdAsync(registration.CourseId);

            if (employee == null || course == null)
                return null;

            return MapToResponseDto(registration, employee, course);
        }

        public async Task<IEnumerable<CourseRegistrationResponseDto>> GetEmployeeRegistrationsAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
                return Enumerable.Empty<CourseRegistrationResponseDto>();

            var registrations = await _registrationRepository.GetCoursesByEmployeeIdAsync(employeeId);
            
            var result = new List<CourseRegistrationResponseDto>();
            foreach (var registration in registrations)
            {
                var course = await _courseRepository.GetCourseByIdAsync(registration.CourseId);
                if (course != null)
                {
                    result.Add(MapToResponseDto(registration, employee, course));
                }
            }

            return result;
        }

        public async Task<IEnumerable<CourseReportEntryDto>> GetCourseRegistrationsAsync(int courseId)
        {
            var registrations = await _registrationRepository.GetEmployeesByCourseIdAsync(courseId);
            return registrations.Select(r => new CourseReportEntryDto
            {
                EmployeeId = r.EmployeeId,
                EmployeeName = $"{r.Employee?.User?.FirstName} {r.Employee?.User?.LastName}".Trim(),
                RegistrationDate = r.RegistrationDate
            });
        }

        public async Task<CourseRegistrationResponseDto?> RegisterToCourseAsync(EmployeeRegistrationDto dto)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(dto.EmployeeId);
            if (employee == null)
                return null;

            var course = await _courseRepository.GetCourseByIdAsync(dto.CourseId);
            if (course == null)
                return null;

            // Check if already registered
            var existingRegistration = await _registrationRepository.GetByEmployeeAndCourseAsync(dto.EmployeeId, dto.CourseId);
            if (existingRegistration != null)
                return null;

            var courseRegistration = new CourseRegistration
            {
                EmployeeId = dto.EmployeeId,
                CourseId = dto.CourseId,
                RegistrationDate = DateTime.Now,
                Status = CourseRegistrationStatus.Active
            };

            await _registrationRepository.AddRegistrationAsync(courseRegistration);

            return MapToResponseDto(courseRegistration, employee, course);
        }

        public async Task<bool> ChangeRegistrationStatusAsync(int registrationId, int newStatus)
        {
            if (!Enum.IsDefined(typeof(CourseRegistrationStatus), newStatus))
                return false;

            var status = (CourseRegistrationStatus)newStatus;
            var result = await _registrationRepository.ChangeStatusCourseAsync(registrationId, status);
            
            return result != CourseRegistrationStatus.Canceled || status == CourseRegistrationStatus.Canceled;
        }

        public async Task<bool> CancelRegistrationAsync(int registrationId)
        {
            var registration = await _registrationRepository.GetByIdAsync(registrationId);
            
            if (registration == null)
            {
                throw new KeyNotFoundException($"לא נמצאה הרשמה עם מזהה {registrationId}");
            }

            if (registration.Status == CourseRegistrationStatus.Canceled)
            {
                throw new InvalidOperationException("הרשמה זו כבר בוטלה בעבר.");
            }

            var employee = await _employeeRepository.GetEmployeeByIdAsync(registration.EmployeeId);
            var course = await _courseRepository.GetCourseByIdAsync(registration.CourseId);

            if (employee == null || course == null)
            {
                throw new Exception("נתוני העובד או הקורס המשויכים להרשמה זו אינם קיימים במערכת.");
            }

            if (course.CourseDetail != null && course.CourseDetail.StartDate <= DateTime.UtcNow)
            {
                throw new InvalidOperationException("לא ניתן לבטל רישום לקורס שכבר התחיל.");
            }

            registration.Status = CourseRegistrationStatus.Canceled;

            employee.TotalCredits += course.CreditCost;

            course.PlacesLeft++;

            await _registrationRepository.SaveChangesAsync();

            return true;
        }

        private CourseRegistrationResponseDto MapToResponseDto(
            CourseRegistration registration,
            Employee employee,
            Course course)
        {
            return new CourseRegistrationResponseDto
            {
                RegistrationId = registration.Id,
                EmployeeId = registration.EmployeeId,
                EmployeeName = $"{employee.User?.FirstName} {employee.User?.LastName}".Trim(),
                CourseId = registration.CourseId,
                CourseName = course.Name,
                RegistrationDate = registration.RegistrationDate,
                AvailableCredits = employee.AvailableCredits,
                Status = (int)registration.Status
            };
        }
    }
}
