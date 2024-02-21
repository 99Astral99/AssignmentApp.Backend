using FluentResults;

namespace AssignmentApp.Domain.Entities
{
    public class AssignmentList
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        public Guid UserId { get; set; }
        public IReadOnlyList<Assignment> Assignments => _assignments.ToList();
        private readonly List<Assignment> _assignments = new();

        private AssignmentList(Guid id, string name, string? description, Guid userId)
        {
            Id = id;
            Name = name;
            Description = description;
            UserId = userId;
        }

        public static Result<AssignmentList> Create(Guid id, string name, string? description, Guid userId)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Fail("Название не может быть пустым");

            return Result.Ok(new AssignmentList(id, name, description, userId));
        }

        public void UpdateAssignmentListInfo(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            Name = name;
            Description = description;
        }

        public void AddAssignment(Assignment assignment)
        {
            _assignments.Add(assignment);
        }


        public void AddAssignments(List<Assignment> assignments)
        {
            _assignments.AddRange(assignments);
        }
        public void UpdateAssignment(Guid AssignmentId, string name, string? description)
        {
            var assignment = _assignments.Find(x => x.Id == AssignmentId);

            if (assignment is null)
                return;

            assignment.UpdateInfo(name, description);
        }

        public void RemoveAssignment(Guid AssignmentId)
        {
            var assignment = _assignments.FirstOrDefault(a => a.Id == AssignmentId);

            if (assignment is null)
                return;

            _assignments.Remove(assignment);
        }
    }
}
