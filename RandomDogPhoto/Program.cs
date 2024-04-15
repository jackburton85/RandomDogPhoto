
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SPPTest.Domain.Models;
using SPPTest.EFDataAccess;
using SPPTest.Repository;
using SPPTest.Services;
using SPPTest.Services.DogApIServices;
using SPPTest.Services.Models;
using SPPTest.WebAPI.Controllers;

namespace SPPTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //add the DbContext
            builder.Services.AddDbContext<DogDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("WebApiDatabase")));

            builder.Services.AddHttpClient<HttpClient>();
            builder.Services.AddScoped<IRepository<DogPhoto>, DogPhotoRepository>();
            builder.Services.AddScoped<DogApiService, DogApiService>();
            builder.Services.AddScoped<SPPDogService, SPPDogService>();
            builder.Services.AddScoped<CacheService<DogPhoto, DogPhoto>, CacheService<DogPhoto, DogPhoto>>();
            builder.Services.AddScoped<CacheService<DogPhoto, Cache<DogPhoto>>>();
            //builder.Services.AddScoped<DogApiClient, DogApiClient>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
