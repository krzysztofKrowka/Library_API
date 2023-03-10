using FluentAssertions.Common;
using Library.Repositories.Models;
using Library.Repositories.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace libraryAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<IBookRepository, BookRepository>();

            services.AddDbContext<BookContext>(options =>  options.UseInMemoryDatabase("name.txt"));
        }
        public void Configure(IApplicationBuilder app)
        {
           
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }
    }
}
