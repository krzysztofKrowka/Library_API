using FluentAssertions.Common;
using Library.Repositories.Models;
using Library.Repositories.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;
using Library.Services.Services;

namespace Library.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();

            
            services.AddTransient<ILibraryContext, LibraryContext>();
            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app)
        {
           
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }
    }
}
