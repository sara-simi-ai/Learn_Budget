
namespace server.DTOs
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string Department { get; set; } = string.Empty;
        public int TotalCredits { get; set; }
        public int UsedCredits { get; set; }
        public int AvailableCredits { get; set; }
    }

    public class EmployeeDetailsResponseDto
    {
        public int Id { get; set; }
        public string Department { get; set; } = string.Empty;
        public int TotalCredits { get; set; }
        public int UsedCredits { get; set; }
        public int AvailableCredits { get; set; }

        public ICollection<CourseRegistrationResponseDto> Registrations { get; set; } 
            = new List<CourseRegistrationResponseDto>();
    }

    public class CreateEmployeeDto
    {
        public string Department { get; set; } = string.Empty;
        public int TotalCredits { get; set; }
    }

    public class UpdateEmployeeDto
    {
        public string Department { get; set; } = string.Empty;
        public int TotalCredits { get; set; }
    }

    public class EmployeeRegistrationDto
    {
        public int EmployeeId { get; set; }
        public int CourseId { get; set; }
    }
}