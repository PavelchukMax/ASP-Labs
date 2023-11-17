using System;
using ConsoleApp1;
class Program
{
    static void Main()
    {
        using (var context = new AppDbContext())
        {
            context.Database.EnsureCreated();
        }

        // Додайте 3 елементи до бази даних
        AddUsers();

        // Витягніть дані з бази даних та виведіть на консоль
        DisplayUsers();
    }

    static void AddUsers()
    {
        using (var context = new AppDbContext())
        {
            var users = new[]
            {
                new User { FirstName = "Іван", LastName = "Іванов", Age = 25 },
                new User { FirstName = "Петро", LastName = "Петров", Age = 30 },
                new User { FirstName = "Марія", LastName = "Сидорова", Age = 22 }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }

    static void DisplayUsers()
    {
        using (var context = new AppDbContext())
        {
            var users = context.Users.ToList();

            Console.WriteLine("Список користувачів:");
            foreach (var user in users)
            {
                Console.WriteLine($"Ім'я: {user.FirstName}, Прізвище: {user.LastName}, Вік: {user.Age}");
            }
        }
    }
}
