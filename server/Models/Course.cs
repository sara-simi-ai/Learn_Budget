namespace server.Models
{
   public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CreditCost { get; set; }
    public int MaxPlaces { get; set; }
    public int PlacesLeft { get; set; }
    public DateTime RegistrationDeadline { get; set; }

    public CourseDetail? CourseDetail { get; set; }
    public ICollection<CourseRegistration> Registrations { get; set; } 
        = new List<CourseRegistration>();
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
}
