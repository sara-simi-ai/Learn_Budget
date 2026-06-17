using System;

namespace server.Models
{
    public class CourseRegistration
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public CourseRegistrationStatus Status { get; set; } = CourseRegistrationStatus.Active;
    }
}