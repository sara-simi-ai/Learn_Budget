using System;
using System.Collections.Generic;

namespace server.Models
{
    public class Employee
    {
        public int Id { get; set; }
        //public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public string Department { get; set; } = string.Empty;
        public int TotalCredits { get; set; }
        public int UsedCredits { get; set; }
        public int AvailableCredits => TotalCredits - UsedCredits;

        public ICollection<CourseRegistration> Registrations { get; set; }
            = new List<CourseRegistration>();
    }
}