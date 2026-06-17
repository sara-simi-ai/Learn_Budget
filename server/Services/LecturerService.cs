using server.Interfaces;
using server.Models;
using static server.DTOs.LecturerDto;

namespace server.Services
{
    public class LecturerService : ILecturerService
    {
        private readonly ILecturerRepository _lecturerRepository;

        public LecturerService(ILecturerRepository lecturerRepository)
        {
            _lecturerRepository = lecturerRepository;
        }

        public async Task<LecturerResponseDto?> GetLecturerById(string id)
        {
            var lecturer = await _lecturerRepository.GetLecturerById(id);

            return lecturer == null ? null : MapToResponseDto(lecturer);
        }

        public async Task<IEnumerable<LecturerResponseDto>> GetLecturersList()
        {
            var lecturers = await _lecturerRepository.GetLecturersList();

            return lecturers.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<LecturerResponseDto>> SearchLecturersByName(string name)
        {
            var lecturers = await _lecturerRepository.SearchLecturersByName(name);

            return lecturers.Select(MapToResponseDto);
        }

        public async Task<LecturerResponseDto?> CreateLecturer(LecturerCreateDto lecturer)
        {
            var existLecturer = await _lecturerRepository.GetLecturerById(lecturer.Id);

            if (existLecturer != null)
                return null;

            var newLecturer = new Lecturer
            {
                Id = lecturer.Id,
                FullName = lecturer.FullName,
                Email = lecturer.Email,
                Phone = lecturer.Phone,
                CompanyName = lecturer.CompanyName,
                Cost = lecturer.Cost
            };

            var created = await _lecturerRepository.CreateLecturer(newLecturer);

            return MapToResponseDto(created!);
        }

        public async Task<LecturerResponseDto?> UpdateLecturer(string id, LecturerUpdateDto lecturer)
        {
            var existing = await _lecturerRepository.GetLecturerById(id);

            if (existing == null)
                return null;

            if (lecturer.FullName != null)
                existing.FullName = lecturer.FullName;

            if (lecturer.Email != null)
                existing.Email = lecturer.Email;

            if (lecturer.Phone != null)
                existing.Phone = lecturer.Phone;

            if (lecturer.CompanyName != null)
                existing.CompanyName = lecturer.CompanyName;

            if (lecturer.Cost.HasValue)
                existing.Cost = lecturer.Cost.Value;

            var updated = await _lecturerRepository.UpdateLecturer(existing);

            return updated == null ? null : MapToResponseDto(updated);
        }

        public async Task<bool> DeleteLecturer(string id)
        {
            return await _lecturerRepository.DeleteLecturer(id);
        }

        private static LecturerResponseDto MapToResponseDto(Lecturer lecturer)
        {
            return new LecturerResponseDto
            {
                Id = lecturer.Id,
                FullName = lecturer.FullName,
                Email = lecturer.Email,
                Phone = lecturer.Phone,
                CompanyName = lecturer.CompanyName,
                Cost = lecturer.Cost,

                Courses = lecturer.CourseDetails.Select(cd => new CourseDetailDto
                {
                    CourseId = cd.CourseId,
                    Location = cd.Location,
                    LecturerName = cd.LecturerName,
                    MeetingLink = cd.MeetingLink
                }).ToList()
            };
        }
    }
}