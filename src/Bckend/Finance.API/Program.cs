
using Finance.API.Filter;
using Finance.API.Middleware;
using Finance.CrossCutting.Repository;
using Finance.CrossCutting.Services;
using Finance.CrossCutting.UseCase;
using Microsoft.OpenApi.Models;

namespace Finance.API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRepository(builder.Configuration);
            builder.Services.AddAplicationService(builder.Configuration);
            builder.Services.AddAplicationUseCase();
            builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
            builder.Services.AddScoped<AuthenticatedUser>();

            // Adiciona CORS liberando para qualquer origem
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });

            builder.Services.AddSwaggerGen(options => {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Description = @"JWT Authorization header using the Bearer scheme.
                                Entre 'Bearer' [space] and then your token in the text input below.
                                Example: 'Bearer 12345abcdef",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme="oauth2",
                            Name="Bearer",
                            In=ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Usa o CORS antes da autorização
            app.UseCors("AllowAll");

            app.UseAuthorization();
            
            app.UseMiddleware<CultureMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
