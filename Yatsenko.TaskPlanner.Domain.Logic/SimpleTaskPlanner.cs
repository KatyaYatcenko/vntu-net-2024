using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatsenko.TaskPlanner.Domain.Models;

namespace Yatsenko.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        public WorkItem[] CreatePlan(WorkItem[] workItems)
        {
            if (workItems == null || workItems.Length == 0)
            {
                return Array.Empty<WorkItem>();
            }

            List<WorkItem> workItemList = workItems.ToList();

            workItemList.Sort((x, y) =>
            {
                int priorityComparison = y.Priority.CompareTo(x.Priority);
                if (priorityComparison != 0)
                {
                    return priorityComparison;
                }

                int dueDateComparison = x.DueDate.CompareTo(y.DueDate);
                if (dueDateComparison != 0)
                {
                    return dueDateComparison;
                }

                return string.Compare(x.Title, y.Title, StringComparison.Ordinal);
            });

            return workItemList.ToArray();
        }
    }
}
