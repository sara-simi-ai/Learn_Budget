using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class User
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string Phone { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
         public Employee? Employee { get; set; }
    }
}
