namespace server.Models
{
   public class CourseDetail
{
    public int CourseId { get; set; } 
    public string Location { get; set; } = string.Empty;
    public string LecturerName { get; set; } = string.Empty;
    public string? MeetingLink { get; set; }
    public DateTime StartDate { get; set; }
     public string? LecturerId { get; set; } 
    public Lecturer? Lecturer { get; set; }
    public Course Course { get; set; } = null!;

}
}
