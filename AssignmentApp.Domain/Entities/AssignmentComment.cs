namespace AssignmentApp.Domain.Entities
{
    public class AssignmentComment
    {
        private AssignmentComment(Guid id, Guid assignmentId, string? message)
        {
            Id = id;
            AssignmentId = assignmentId;
            Message = message;
        }

        public Guid Id { get; private set; }
        public Guid AssignmentId { get; private set; }
        public string? Message { get; private set; }

        public void UpdateCommentInfo(string message)
        {
            Message = message;
        }
        public static AssignmentComment Create(Guid id, Guid assignmentId, string? message)
        {
            return new AssignmentComment(id, assignmentId, message);
        }
    }
}
