using LibraryCore.Domain;
using LibraryInfrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfrastructure.Commands
{
    public class PutBookCommand : Command
    {
        public static async Task<IActionResult> PutBook(string title, BookDTO bookDTO,BookContext _context)
        {
            if (title != bookDTO.Title)
            {
                return null;
            }
            var book = await _context.Books.Where(b => b.Title == title).SingleAsync();
            book.Description = bookDTO.Description;
            book.Author = bookDTO.Author;
            book.Category = bookDTO.Category;
            book.PublicationDate = bookDTO.PublicationDate;
            book.Cost = bookDTO.Cost;
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Books.Where(b => b.Title == title).SingleAsync() == null)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return null;
        }
    }
}
