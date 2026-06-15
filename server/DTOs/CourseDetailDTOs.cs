namespace server.DTOs
{
    public class CourseDetailResponseDto
    {
        public int CourseId { get; set; }
        public string Location { get; set; } = string.Empty;
        public string LecturerName { get; set; } = string.Empty;
        public string? MeetingLink { get; set; }
    }

    public class CreateCourseDetailDto
    {
        public string Location { get; set; } = string.Empty;
        public int? LecturerId { get; set; }
        public string? MeetingLink { get; set; }
    }

    public class UpdateCourseDetailDto
    {
        public string Location { get; set; } = string.Empty;
        public int? LecturerId { get; set; }
        public string? MeetingLink { get; set; }
    }

}
