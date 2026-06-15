using System.Collections.Generic;

namespace server.Models
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        
        public decimal Cost { get; set; } 

        public ICollection<CourseDetail> CourseDetails { get; set; } = new List<CourseDetail>();
    }
}