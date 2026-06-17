using System.ComponentModel.DataAnnotations;

namespace server.DTOs
{
    public class LecturerDto
    {
        public class LecturerResponseDto
        {
            public string Id { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string CompanyName { get; set; } = string.Empty;
            public decimal Cost { get; set; }

            public IEnumerable<CourseDetailDto> Courses { get; set; } = new List<CourseDetailDto>();
        }

        public class CourseDetailDto
        {
            public int CourseId { get; set; }
            public string Location { get; set; } = string.Empty;
            public string LecturerName { get; set; } = string.Empty;
            public string? MeetingLink { get; set; }
        }

        public class LecturerCreateDto
        {
            [Required]
            public string Id { get; set; } = string.Empty;

            [Required]
            [MaxLength(100)]
            public string FullName { get; set; } = string.Empty;

            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            public string Phone { get; set; } = string.Empty;

            public string CompanyName { get; set; } = string.Empty;

            [Range(0, double.MaxValue)]
            public decimal Cost { get; set; }
        }

        public class LecturerUpdateDto
        {
            [MaxLength(100)]
            public string? FullName { get; set; }

            [EmailAddress]
            public string? Email { get; set; }

            public string? Phone { get; set; }

            public string? CompanyName { get; set; }

            public decimal? Cost { get; set; }
        }
    }
}