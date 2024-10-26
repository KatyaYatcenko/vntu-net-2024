using System;
using System.Collections.Generic;
using System.Linq;
using Yatsenko.TaskPlanner.DataAccess.Abstractions;
using Yatsenko.TaskPlanner.Domain.Models;

namespace Yatsenko.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        private readonly IWorkItemsRepository _workItemsRepository;

        public SimpleTaskPlanner(IWorkItemsRepository workItemsRepository)
        {
            _workItemsRepository = workItemsRepository ?? throw new ArgumentNullException(nameof(workItemsRepository));
        }

        public WorkItem[] CreatePlan()
        {
            WorkItem[] workItems = _workItemsRepository.GetAll();

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
