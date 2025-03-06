using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OptiLogic_API.Model;
using Microsoft.AspNetCore.Cors;
using OptiLogic_API.Service;
using OptiLogic_API.Utility;

namespace OptiLogic_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")));

            builder.Services.AddScoped<CarService>();
            builder.Services.AddScoped<PredictionService>();
            builder.Services.AddScoped<CarFilter>();
            builder.Services.AddScoped<ParkingSpotService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            app.UseCors("AllowAllOrigins");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();


            

        }
    }
}
