
using lab_12_2.Models;
using System.Linq;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (!context.Companies.Any())
        {
            context.Companies.AddRange(
                new Company {Id=1, Name = "Company1", Location = "Location1" },
                new Company { Id = 2, Name = "Company2", Location = "Location2" },
                new Company { Id = 3, Name = "Company3", Location = "Location3" },
                new Company { Id = 4, Name = "Company4", Location = "Location4" },
                new Company { Id = 5, Name = "Company5", Location = "Location5" }
     
            );
            context.SaveChanges();
        }
    }
}
