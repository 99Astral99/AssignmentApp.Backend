namespace AssignmentApp.Application.Responses
{
    public record ResponseAssignmentComment
    {
        public Guid Id { get; set; }
        public Guid AssignmentId { get; set; }
        public string? Comment { get; set; }
    }
}
