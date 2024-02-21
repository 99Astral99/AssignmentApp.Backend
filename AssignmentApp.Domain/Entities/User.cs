using FluentResults;

namespace AssignmentApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        public IReadOnlyList<AssignmentList> AssignmentLists => _assignmentLists.ToList();
        private readonly List<AssignmentList> _assignmentLists = new();

        private User(Guid id, string name, string email, string passwordHash)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }

        public static Result<User> Create(Guid id, string name, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Fail("Имя не может быть пустым");
            if (string.IsNullOrWhiteSpace(email))
                return Result.Fail("Email не может быть пустым");

            return Result.Ok(new User(id, name, email, passwordHash));
        }
    }
}
