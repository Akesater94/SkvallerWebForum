using Microsoft.EntityFrameworkCore;
using SkvallerAPI.Data;
using SkvallerAPI.Repositories;
using SkvallerAPI.Services;
using SnackisApi.Services;


namespace SkvallerAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //VÄlj azureConnectionString för produktion och databasen som ligger på Azure.
        var azureConnectionString = builder.Configuration.GetConnectionString("AzureConnection");
        //Välj defaultConnectionString för utveckling och lokal databas
        var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApiDbContext>(options =>

            options.UseSqlServer(builder.Configuration.GetConnectionString(azureConnectionString)) // <- connection strängen ska in här. Glöm inte att göra samma sak i webappens program.cs

            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
