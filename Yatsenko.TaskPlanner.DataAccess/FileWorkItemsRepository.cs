using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Yatsenko.TaskPlanner.DataAccess.Abstractions;
using Yatsenko.TaskPlanner.Domain.Models;

namespace Yatsenko.TaskPlanner.DataAccess
{
    public class FileWorkItemsRepository : IWorkItemsRepository
    {
        private const string FileName = "work-items.json";
        private readonly Dictionary<Guid, WorkItem> _workItems;

        public FileWorkItemsRepository()
        {
            _workItems = new Dictionary<Guid, WorkItem>();

            if (File.Exists(FileName))
            {
                string json = File.ReadAllText(FileName);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    var workItems = JsonConvert.DeserializeObject<WorkItem[]>(json);
                    foreach (var workItem in workItems)
                    {
                        _workItems[workItem.Id] = workItem;
                    }
                }
            }
            else
            {
                File.WriteAllText(FileName, "[]");
            }
        }

        public Guid Add(WorkItem workItem)
        {
            var newWorkItem = workItem.Clone();
            newWorkItem.Id = Guid.NewGuid();
            _workItems[newWorkItem.Id] = newWorkItem;
            return newWorkItem.Id;
        }

        public WorkItem Get(Guid id) => _workItems.TryGetValue(id, out var workItem) ? workItem : null;

        public WorkItem[] GetAll() => new List<WorkItem>(_workItems.Values).ToArray();

        public bool Update(WorkItem workItem)
        {
            if (_workItems.ContainsKey(workItem.Id))
            {
                _workItems[workItem.Id] = workItem;
                return true;
            }
            return false;
        }

        public bool Remove(Guid id) => _workItems.Remove(id);

        public void SaveChanges()
        {
            try
            {
                var workItemsArray = _workItems.Values.ToArray();
                var json = JsonConvert.SerializeObject(workItemsArray, Formatting.Indented);
                File.WriteAllText(FileName, json);
                Console.WriteLine("Changes saved to file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
            }
        }
    }
}
