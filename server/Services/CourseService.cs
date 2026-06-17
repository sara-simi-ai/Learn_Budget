
using server.DTOs;
using server.Interfaces;
using server.Models;

namespace server.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseResponseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return courses.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<CourseResponseDto>> GetAvailableCoursesAsync()
        {
            var courses = await _courseRepository.GetAvailableCoursesAsync();
            return courses.Select(MapToResponseDto);
        }

        public async Task<CourseResponseDto?> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            return course != null ? MapToResponseDto(course) : null;
        }

        public async Task<CourseResponseDto?> CreateCourseAsync(CreateCourseDto dto)
        {
            var courseEntity = new Course
            {
                Name = dto.Name,
                Description = dto.Description,
                CreditCost = dto.CreditCost,
                MaxPlaces = dto.MaxPlaces,
                RegistrationDeadline = dto.RegistrationDeadline,
                CourseDetail = dto.CourseDetail != null ? new CourseDetail
                {
                    Location = dto.CourseDetail.Location,
                    LecturerId = dto.CourseDetail.LecturerId,
                    MeetingLink = dto.CourseDetail.MeetingLink,
                    StartDate = dto.CourseDetail.StartDate,
                } : null
            };

            var createdCourse = await _courseRepository.CreateCourseAsync(courseEntity);
            return MapToResponseDto(createdCourse);
        }
        public async Task<CourseResponseDto?> UpdateCourseAsync(int id, UpdateCourseDto updCourse)
        {
            var existingCourse = await _courseRepository.GetCourseByIdAsync(id);

            if (existingCourse == null)
                return null;

            existingCourse.Name = updCourse.Name;
            existingCourse.Description = updCourse.Description;
            existingCourse.CreditCost = updCourse.CreditCost;
            existingCourse.MaxPlaces = updCourse.MaxPlaces;
            existingCourse.RegistrationDeadline = updCourse.RegistrationDeadline;

            if (updCourse.CourseDetail != null)
            {
                if (existingCourse.CourseDetail == null)
                {
                    existingCourse.CourseDetail = new CourseDetail
                    {
                        CourseId = existingCourse.Id
                    };
                }

                existingCourse.CourseDetail.Location = updCourse.CourseDetail.Location;
                existingCourse.CourseDetail.LecturerId = updCourse.CourseDetail.LecturerId;
                existingCourse.CourseDetail.MeetingLink = updCourse.CourseDetail.MeetingLink;
                existingCourse.CourseDetail.StartDate = updCourse.CourseDetail.StartDate;
            }

            var updatedCourse = await _courseRepository.UpdateCourseAsync(existingCourse);

            return updatedCourse == null ? null : MapToResponseDto(updatedCourse);
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            return await _courseRepository.DeleteCourseAsync(id);
        }
        
        private CourseResponseDto MapToResponseDto(Course course)
        {
            if (course == null) return null!;
            string fullLecturerName = string.Empty;

            if (course.CourseDetail?.Lecturer != null)
            {
                fullLecturerName = $"{course.CourseDetail.Lecturer.FullName}".Trim();
            }

            return new CourseResponseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                CreditCost = course.CreditCost,
                MaxPlaces = course.MaxPlaces,
                PlacesLeft = course.PlacesLeft,
                RegistrationDeadline = course.RegistrationDeadline,
                
                CourseDetail = course.CourseDetail != null ? new CourseDetailResponseDto
                {
                    Location = course.CourseDetail.Location,
                    LecturerName = fullLecturerName,
                    MeetingLink = course.CourseDetail.MeetingLink,
                    StartDate = course.CourseDetail.StartDate,
                } : null
            };
        }
    }
}