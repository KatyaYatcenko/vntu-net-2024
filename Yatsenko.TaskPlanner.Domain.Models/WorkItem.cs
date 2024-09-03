using System.Globalization;
using System;

namespace Yatsenko.TaskPlanner.Domain.Models
{
    public class WorkItem
    {
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Enums.Priority Priority { get; set; }
        public Enums.Complexity Complexity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public Guid Id { get; set; }


        public override string ToString()
        {
            string formattedDate = DueDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            string priorityString = Priority.ToString().ToLower();

            return $"{Title}: due {formattedDate}, {priorityString} priority";
        }

        public WorkItem Clone()
        {
            return new WorkItem
            {
                Id = Guid.NewGuid(),
                CreationDate = this.CreationDate,
                DueDate = this.DueDate,
                Priority = this.Priority,
                Complexity = this.Complexity,
                Title = this.Title,
                Description = this.Description,
                IsCompleted = this.IsCompleted
            };
        }

    }
}
