using AssignmentApp.Domain.Entities;

namespace AssignmentApp.Application.Responses
{
    public record ResponseAssignment
    {
        public Guid Id { get; set; }
        public Guid AssignmentListId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public List<AssignmentComment>? Comments { get; set; }
    }
}
