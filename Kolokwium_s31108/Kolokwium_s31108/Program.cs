using Kolokwium_s31108.Services;

namespace Kolokwium_s31108;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddScoped<IDbServices, DbServices>();
        
        var app = builder.Build();

// Configure the HTTP request pipeline.
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}