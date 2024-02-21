namespace AssignmentApp.Application.Responses
{
    public record ResponseAssignmentList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ResponseAssignment> Assignments { get; set; }
    }
}
