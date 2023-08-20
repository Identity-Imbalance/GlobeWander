using GlobeWander.Data;
using GlobeWander.Models;
using GlobeWander.Models.Interfaces;
using GlobeWander.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

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
            builder.Services.AddControllers()
       .AddJsonOptions(options =>
       {
           options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
       });

            string? stringConnection = builder.Configuration.GetConnectionString("DefaultConnection");



            // Add the db context class  to the sql server so this creating database 
            builder.Services.AddDbContext<GlobeWanderDbContext>(
                options => options.UseSqlServer(stringConnection
                ));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
               
            }).AddEntityFrameworkStores<GlobeWanderDbContext>();

            builder.Services.AddTransient<IUser, IdentityUserService>();
            builder.Services.AddTransient<ITourSpot, TourSpotService>();
            builder.Services.AddTransient<ITrip, TripService>();
            builder.Services.AddTransient<IHotel, HotelService>();
            builder.Services.AddTransient<IHotelRoom, HotelRoomService>();
            builder.Services.AddTransient<IRoom, RoomService>();
            builder.Services.AddTransient<IBookingRoom, BookingRoomService>();
            builder.Services.AddTransient<IBookingTrip, BookingTripService>();
            builder.Services.AddTransient<IRate, RateService>();
            builder.Services.AddScoped<JWTTokenService>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {

                options.TokenValidationParameters = JWTTokenService.GetValidationPerameters(builder.Configuration);
            });


            builder.Services.AddAuthorization(options => { 
                options.AddPolicy("create", policy => policy.RequireClaim("permissions", "create"));
                options.AddPolicy("update", policy => policy.RequireClaim("permissions", "update"));
                options.AddPolicy("delete", policy => policy.RequireClaim("permissions", "delete")); 
                options.AddPolicy("read", policy => policy.RequireClaim("permissions", "read")); });


            builder.Services.AddAuthorization();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Globe Wander API",
                    Version = "v1",
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "add the JWT TOKEN"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
     {{
         new OpenApiSecurityScheme {
         Reference=
         new OpenApiReference{
             Type=ReferenceType.SecurityScheme,
             Id= "Bearer"
     }
     },
     new string[]{ } }
     });
            });


            var app = builder.Build();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
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