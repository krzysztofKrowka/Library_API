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
        using (var context = new BookContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<BookContext>>()))
        {
            // Look for any books.
            if (context.Books.Any())
            {
                return;   // DB has been seeded
            }
            context.Books.AddRange(
                new Book
                {
                    Id=0,
                    Title = "When Harry Met Sally",
                    Author= "John Wick",
                    Description= "ABCDEFGHIJKLMNOPRSTUWXYZABCDEFGHIJKLMNOPRSTUWXYZ",
                    Cost= 123,
                    PublicationDate=1234,
                    Category="Fantasy",
                    Quantity=0
                }
            );
            context.SaveChanges();
        }
    }
}