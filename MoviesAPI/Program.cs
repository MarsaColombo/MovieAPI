using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MoviesAPI
{
    /// <summary>
    /// Classe principale représentant le point d'entrée de l'application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        /// <param name="args">Arguments de la ligne de commande.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services);

            var app = builder.Build();

            Configure(app);

            app.Run();
        }

        /// <summary>
        /// Configurer les services nécessaires pour l'application.
        /// </summary>
        /// <param name="services">Collection de services à configurer.</param>
        private static void ConfigureServices(IServiceCollection services)
        {
            // Ajouter CORS pour accéder à toutes les données à l'extérieur.
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Ajouter la documentation Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Enregistrer les référentiels avec l'injection de dépendances
            const string keyConnectionString =
                @"Host=localhost;Port=5436;Username=postgres;Password=PirateBiaou@123;Database=movie_db;Pooling=true; Include Error Detail=true";
            services.AddSingleton(new MovieRepository(keyConnectionString));
            services.AddSingleton(new ActorRepository(keyConnectionString));

            // Enregistrer les contrôleurs
            services.AddControllers();
        }

        /// <summary>
        /// Configurer l'application web.
        /// </summary>
        /// <param name="app">L'application web à configurer.</param>
        private static void Configure(WebApplication app)
        {
            // Configurer le pipeline de requêtes HTTP.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors();

            // Utiliser le routage et les contrôleurs
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
