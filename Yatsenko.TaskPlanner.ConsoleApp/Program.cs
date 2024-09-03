using System;
using System.Globalization;
using Yatsenko.TaskPlanner.Domain.Logic;
using Yatsenko.TaskPlanner.Domain.Models.Enums;
using Yatsenko.TaskPlanner.Domain.Models;

internal static class Program
{
    public static void Main(string[] args)
    {
        // Створюємо список для зберігання введених WorkItem
        var workItems = new List<WorkItem>();

        Console.WriteLine("Enter work items (type 'exit' to finish):");

        while (true)
        {
            // Введення назви задачі
            Console.Write("Title: ");
            string title = Console.ReadLine();
            if (title.ToLower() == "exit") break;

            // Введення опису задачі
            Console.Write("Description: ");
            string description = Console.ReadLine();

            // Введення дати створення
            Console.Write("Creation Date (dd.MM.yyyy): ");
            DateTime creationDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

            // Введення дедлайну
            Console.Write("Due Date (dd.MM.yyyy): ");
            DateTime dueDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

            // Введення пріоритету
            Console.Write("Priority (None, Low, Medium, High, Urgent): ");
            Priority priority = Enum.Parse<Priority>(Console.ReadLine(), true);

            // Введення складності
            Console.Write("Complexity (None, Low, Medium, High, Critical): ");
            Complexity complexity = Enum.Parse<Complexity>(Console.ReadLine(), true);

            // Введення статусу виконання
            Console.Write("Is Completed (true/false): ");
            bool isCompleted = bool.Parse(Console.ReadLine());

            // Створення нового WorkItem
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

            workItems.Add(workItem);

            Console.WriteLine("Work item added! Type 'exit' to stop or continue adding work items.\n");
        }

        // Сортування задач за допомогою SimpleTaskPlanner
        var planner = new SimpleTaskPlanner();
        WorkItem[] sortedWorkItems = planner.CreatePlan(workItems.ToArray());

        // Виведення відсортованих задач на екран
        Console.WriteLine("\nSorted Work Items:");
        foreach (var item in sortedWorkItems)
        {
            Console.WriteLine(item);
        }
    }
}
