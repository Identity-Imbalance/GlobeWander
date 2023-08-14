using GlobeWander.Data;
using Microsoft.EntityFrameworkCore;

namespace GlobeWander
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            // This bring for us the String Connection
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
     );

            string? stringConnection = builder.Configuration.GetConnectionString("DefaultConnection");



            // Add the db context class  to the sql server so this creating database 
            builder.Services.AddDbContext<GlobeWanderDbContext>(
                options => options.UseSqlServer(stringConnection
                ));

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Globe Wander API",
                    Version = "v1",
                });
            });


            var app = builder.Build();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Globe Wander API");
                options.RoutePrefix = "";
            });

            app.MapControllers();


            app.Run();
        }
    }
}