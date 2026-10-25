using Skvaller.Components;
using Skvaller.Components.Account;
using Skvaller.Data;
using Skvaller.Repositories;
using Skvaller.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Skvaller;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddHttpClient<IPostApiService, PostApiService>(client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["SkvallerApi:BaseUrl"]!);
        });

        builder.Services.AddScoped<IMessageRepository, MessageRepository>();
        builder.Services.AddScoped<IReportRepository, ReportRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();

        builder.Services.AddScoped<IMessageService, MessageService>();
        builder.Services.AddScoped<IReportService, ReportService>();
        builder.Services.AddScoped<IProfileService, ProfileService>();
        builder.Services.AddScoped<ICommentService, CommentService>();
        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddServerSideBlazor()
            .AddCircuitOptions(options =>
            {
                options.DetailedErrors = true;
            });

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        //VÄlj azureConnectionString för produktion och databasen som ligger på Azure.
        var azureConnectionString = builder.Configuration.GetConnectionString("AzureConnection");
        //Välj defaultConnectionString för utveckling och lokal databas.
        var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(azureConnectionString)); //<- Skriv in connection strängen här. Glöm inte att göra samma sak i Api:ets program.cs
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            await DbSeeder.SeedAdminAsync(scope.ServiceProvider);
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStaticFiles();

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        // Add additional endpoints required by the Identity /Account Razor components.
        app.MapAdditionalIdentityEndpoints();

        app.Run();
    }
}
