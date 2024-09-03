using System;
using System.Collections.Generic;
using System.Globalization;
using Yatsenko.TaskPlanner.Domain.Logic;
using Yatsenko.TaskPlanner.Domain.Models.Enums;
using Yatsenko.TaskPlanner.Domain.Models;
using Yatsenko.TaskPlanner.DataAccess;

internal static class Program
{
    private static FileWorkItemsRepository _repository;

    public static void Main(string[] args)
    {
        _repository = new FileWorkItemsRepository();

        while (true)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("[A]dd work item");
            Console.WriteLine("[B]uild a plan");
            Console.WriteLine("[M]ark work item as completed");
            Console.WriteLine("[R]emove a work item");
            Console.WriteLine("[Q]uit the app");

            string choice = Console.ReadLine().ToUpper();

            switch (choice)
            {
                case "A":
                    AddWorkItem();
                    break;
                case "B":
                    BuildPlan();
                    break;
                case "M":
                    MarkWorkItemAsCompleted();
                    break;
                case "R":
                    RemoveWorkItem();
                    break;
                case "Q":
                    _repository.SaveChanges();
                    Console.WriteLine("Exiting the application...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static void AddWorkItem()
    {
        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Creation Date (dd.MM.yyyy): ");
        DateTime creationDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

        Console.Write("Due Date (dd.MM.yyyy): ");
        DateTime dueDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

        Console.Write("Priority (None, Low, Medium, High, Urgent): ");
        Priority priority = Enum.Parse<Priority>(Console.ReadLine(), true);

        Console.Write("Complexity (None, Low, Medium, High, Critical): ");
        Complexity complexity = Enum.Parse<Complexity>(Console.ReadLine(), true);

        Console.Write("Is Completed (true/false): ");
        bool isCompleted = bool.Parse(Console.ReadLine());

        var workItem = new WorkItem
        {
            Title = title,
            Description = description,
            CreationDate = creationDate,
            DueDate = dueDate,
            Priority = priority,
            Complexity = complexity,
            IsCompleted = isCompleted
        };

        Guid itemId = _repository.Add(workItem);
        Console.WriteLine($"Work item added with ID: {itemId}");
    }

    private static void BuildPlan()
    {
        var planner = new SimpleTaskPlanner();
        WorkItem[] workItems = _repository.GetAll();
        WorkItem[] sortedWorkItems = planner.CreatePlan(workItems);

        Console.WriteLine("\nSorted Work Items:");
        foreach (var item in sortedWorkItems)
        {
            Console.WriteLine(item);
        }
    }

    private static void MarkWorkItemAsCompleted()
    {
        Console.Write("Enter the ID of the work item to mark as completed: ");
        Guid id = Guid.Parse(Console.ReadLine());

        var workItem = _repository.Get(id);
        if (workItem != null)
        {
            workItem.IsCompleted = true;
            _repository.Update(workItem);
            Console.WriteLine("Work item marked as completed.");
        }
        else
        {
            Console.WriteLine("Work item not found.");
        }
    }

    private static void RemoveWorkItem()
    {
        Console.Write("Enter the ID of the work item to remove: ");
        Guid id = Guid.Parse(Console.ReadLine());

        bool removed = _repository.Remove(id);
        if (removed)
        {
            Console.WriteLine("Work item removed.");
        }
        else
        {
            Console.WriteLine("Work item not found.");
        }
    }
}
