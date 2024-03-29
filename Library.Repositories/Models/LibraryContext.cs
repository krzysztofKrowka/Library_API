﻿using Library.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System.Reflection.Metadata;

namespace Library.Repositories.Models
{
    public partial class LibraryContext : DbContext, ILibraryContext 
    { 
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) {  }
        
        public DbSet<Book> Books { get; set; }
        
        public DbSet<Author> Authors { get; set; }
        
        public DbSet<Librarian> Librarians { get; set; }
        
        public DbSet<User> Users { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=library;Trusted_Connection=True;TrustServerCertificate=True;",b => b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
            
            base.OnConfiguring(optionsBuilder);
  
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorID);
        }
    }
}
