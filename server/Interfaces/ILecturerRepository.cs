using server.Models;

namespace server.Interfaces
{
    public interface ILecturerRepository
    {
        Task<Lecturer?> GetLecturerById(string id);
        Task<IEnumerable<Lecturer>> GetLecturersList();
        Task<IEnumerable<Lecturer>> SearchLecturersByName(string name);

        Task<Lecturer?> CreateLecturer(Lecturer lecturer);
        Task<Lecturer?> UpdateLecturer(Lecturer lecturer);
        Task<bool> DeleteLecturer(string id);
    }
}