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

        public override string ToString()
        {
            string formattedDate = DueDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            string priorityString = Priority.ToString().ToLower();

            return $"{Title}: due {formattedDate}, {priorityString} priority";
        }
    }
}
