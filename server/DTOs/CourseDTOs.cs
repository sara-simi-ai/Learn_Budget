
using System.ComponentModel.DataAnnotations;

namespace server.DTOs
{
    public class CourseResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CreditCost { get; set; }
        public int MaxPlaces { get; set; }
        public int PlacesLeft { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        
        public CourseDetailResponseDto? CourseDetail { get; set; }
    }

    public class CreateCourseDto
    {
        [Required, MinLength(1, ErrorMessage = "MinLength is 1")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500, ErrorMessage = "MaxLength is 500")]
        public string Description { get; set; } = string.Empty;
        [Required, Range(1, int.MaxValue, ErrorMessage = "CreditCost must be greater than 0")]
        public int CreditCost { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "MaxPlaces must be greater than 0")]
        public int MaxPlaces { get; set; }
        [Required]
        public DateTime RegistrationDeadline { get; set; }
        
        public CreateCourseDetailDto? CourseDetail { get; set; }
    }

    public class UpdateCourseDto
    {
        [MinLength(1, ErrorMessage = "MinLength is 1")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500, ErrorMessage = "MaxLength is 500")]
        public string Description { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "CreditCost must be greater than 0")]
        public int CreditCost { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "MaxPlaces must be greater than 0")]
        public int MaxPlaces { get; set; }
        [Required]
        public DateTime RegistrationDeadline { get; set; }
        
        public UpdateCourseDetailDto? CourseDetail { get; set; }
    }
}

