using TECHTALKFORUM.Data;
using Microsoft.EntityFrameworkCore;
using TECHTALKFORUM.Data.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TECHTALKFORUM.Services;

namespace TECHTALKFORUM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ======== Add Services =========
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<IChannelService, ChannelService>();

            builder.Services.AddScoped<IMessageService, MessageService>();



            // 🟩 Swagger + JWT Authorize-knapp
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TECHTALKFORUM",
                    Version = "v1"
                });

                // 🔐 JWT-token support
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Skriv 'Bearer [din token]'"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // 🟩 Database
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 🟩 JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = builder.Configuration["Jwt:Key"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
                    };
                });

            var app = builder.Build();

            // ======== Seed data =========
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (!context.Users.Any())
                {
                    var users = SeedData.GenerateUsers(20);
                    context.Users.AddRange(users);
                }
                if (!context.Channels.Any())
                {
                    var channels = SeedData.GenerateChannels(10); // valfritt antal
                    context.Channels.AddRange(channels);
                }
                if (!context.Messages.Any())
                {
                    var messages = SeedData.GenerateMessages(20, context.Users.ToList(), context.Channels.ToList());
                    context.Messages.AddRange(messages);
                }
                context.SaveChanges();

            }

            // ======== Middleware pipeline =========
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

