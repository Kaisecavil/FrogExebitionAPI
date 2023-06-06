
using FrogExebitionAPI.Database;
using Microsoft.EntityFrameworkCore;
using FrogExebitionAPI.Controllers;
using Microsoft.AspNetCore.Identity;
using FrogExebitionAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FrogExebitionAPI.UoW;
using FrogExebitionAPI.Swashbuckle;
using FrogExebitionAPI.Interfaces;

namespace FrogExebitionAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            

            builder.Services.AddDbContext<ApplicationContext>(
                o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IFrogService, FrogService>();
            builder.Services.AddScoped<IExebitionService, ExebitionService>();
            builder.Services.AddScoped<IFrogOnExebitionService,FrogOnExebitionService>();
            builder.Services.AddScoped<IVoteService, VoteService>();



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                    ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
                };
            }
            );

            builder.Services.AddTransient<IAuthService, AuthService>();



            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddTransient<Seed>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => {
                // ...
                c.SchemaFilter<SchemaFilter>();
            });

            

            var app = builder.Build();

            if (args.Length == 1 && args[0].ToLower() == "seeddata")
                SeedData(app);

            void SeedData(IHost app)
            {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

                using (var scope = scopedFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetService<Seed>();
                    service.SeedApplicationContext();
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}