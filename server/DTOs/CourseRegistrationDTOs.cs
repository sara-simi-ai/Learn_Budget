
namespace server.DTOs
{
  
    public class CourseRegistrationResponseDto
        {
            public int RegistrationId { get; set; }
            public int EmployeeId { get; set; }
            public string EmployeeName { get; set; } = string.Empty;
            public int CourseId { get; set; }
            public string CourseTitle { get; set; } = string.Empty;
            public DateTime RegistrationDate { get; set; }
            public int CreditsRemaining { get; set; }
        }
    
        public class CourseReportEntryDto
        {
            public int EmployeeId { get; set; }
            public string EmployeeName { get; set; } = string.Empty;
            public DateTime RegistrationDate { get; set; }
        }
}