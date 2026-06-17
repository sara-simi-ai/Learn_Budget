using static server.DTOs.LecturerDto;

namespace server.Interfaces
{
    public interface ILecturerService
    {
        Task<LecturerResponseDto?> GetLecturerById(string id);

        Task<IEnumerable<LecturerResponseDto>> GetLecturersList();

        Task<IEnumerable<LecturerResponseDto>> SearchLecturersByName(string name);

        Task<LecturerResponseDto?> CreateLecturer(LecturerCreateDto lecturer);

        Task<LecturerResponseDto?> UpdateLecturer(string id, LecturerUpdateDto lecturer);

        Task<bool> DeleteLecturer(string id);
    }
}