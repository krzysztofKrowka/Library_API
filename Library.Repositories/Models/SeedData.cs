using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Library.Repositories;
using System;
using System.Linq;
using Library.Repositories.Models;

namespace Library.Repositories;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new LibraryContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<LibraryContext>>()))
        {
            // Look for any books.
            if (context.Books.Any())
            {
                return;   // DB has been seeded
            }
            context.Books.AddRange(
               
            );
            context.SaveChanges();
        }
    }
}