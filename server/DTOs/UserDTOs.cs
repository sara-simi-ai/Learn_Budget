using System.ComponentModel.DataAnnotations;

namespace server.DTOs
{
    public class UserDTOs
    {
        public class UserResponseDto
        {
            public string Id { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public UserRole Role { get; set; }
        }

        public class UserCreateDto
        {
            [Required, RegularExpression(@"^\d{9}$", ErrorMessage = "Id must have 9 numbers!")]
            public string Id { get; set; } = string.Empty;
            [Required,MaxLength(50)]
            public string FirstName { get; set; } = string.Empty;
            [Required,MaxLength(50)]
            public string LastName { get; set; } = string.Empty;
            [Required,EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required,Phone]
            public string Phone { get; set; } = string.Empty;
            [Required]
            [MinLength(8, ErrorMessage = "Password must have at least 8 chars!")]
            public string Password { get; set; } = string.Empty;
        }
        public class UserUpdateDto
        {
            [MaxLength(50)]
            public string FirstName { get; set; } = string.Empty;
            [MaxLength(50)]
            public string LastName { get; set; } = string.Empty;
            [EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required, Phone]
            public string Phone { get; set; } = string.Empty;
        }
        
        public class UserWithSumTicketsDto
        {
            public string Id { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public int NumOfTickets { get; set; }
        }

    }
}
