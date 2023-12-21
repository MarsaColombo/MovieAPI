using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MoviesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services);

            var app = builder.Build();

            Configure(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Add Cors to access every data outside
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Add services to the container.
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Register repositories using dependency injection
            const string keyConnectionString =
                @"Host=localhost;Port=5436;Username=postgres;Password=PirateBiaou@123;Database=movie_db;Pooling=true; Include Error Detail=true";
            services.AddSingleton(new MovieRepository(keyConnectionString));
            services.AddSingleton(new ActorRepository(keyConnectionString));

            // Register controllers
            services.AddControllers();
        }

        private static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors();

            // Use routing and controllers
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
