using AssignmentApp.Domain.Enums;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApp.Domain.Entities
{
    public class Assignment
    {
        private Assignment(Guid id, string name, string? description, Guid assignmentListId)
        {
            Id = id;
            Name = name;
            Description = description;
            DateCreated = DateTime.UtcNow;
            AssignmentListId = assignmentListId;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public IReadOnlyList<AssignmentComment> Comments => _comments.ToList();
        private readonly List<AssignmentComment> _comments = new();
        public DateTime DateCreated { get; private set; }
        public Status CurrentStatus { get; private set; } = Status.Pending;

        public Guid AssignmentListId { get; private set; }

        public static Result<Assignment> Create(Guid id, string name, string? description, Guid assignmentListId)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Fail("Название не может быть пустым");

            return Result.Ok(new Assignment(id, name, description, assignmentListId));
        }

        public void AddComment(AssignmentComment comment)
        {
            _comments.Add(comment);
        }

        public void UpdateInfo(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            Name = name;
            Description = description ?? Description;
        }

        public void UpdateStatus(Status status)
        {
            if (status == Status.Pending || status == Status.In_work || status == Status.Success)
                CurrentStatus = status;

            return;
        }
    }
}
