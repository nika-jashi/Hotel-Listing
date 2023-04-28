using Microsoft.IdentityModel.Tokens;
using HotelListingAPI.Configurations;
using HotelListingAPI.Contracts;
using HotelListingAPIData;
using HotelListingAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text;
using HotelListingAPI.Middleware;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.OData;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace HotelListingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
            builder.Services.AddDbContext<HotelListingDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<User>>("HotelListingApi")
                .AddEntityFrameworkStores<HotelListingDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddHealthChecks()
                .AddCheck<CustomHealtCheck>(
                "Health Check",
                failureStatus: HealthStatus.Degraded,
                tags: new[] { "Custom" }
                ).AddSqlServer(connectionString, tags: new[] { "database" })
                .AddDbContextCheck<HotelListingDbContext>();

            builder.Services.AddControllers().AddOData(options =>
            {
                options.Select().Filter().OrderBy();
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel Listing API", Version = "v1" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = JwtBearerDefaults.AuthenticationScheme
                                        },
                            Scheme = "0auth2",
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    b => b.AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod());
            });

            builder.Services.AddApiVersioning(
                options => {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = ApiVersionReader.Combine(
                            new QueryStringApiVersionReader("api-version"),
                            new HeaderApiVersionReader("x-version"),
                            new MediaTypeApiVersionReader("ver")
                        );
                });

            builder.Services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

            builder.Services.AddAutoMapper(typeof(MapperConfig));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
            builder.Services.AddScoped<IHotelsRepository, HotelsRepository>();
            builder.Services.AddScoped<IAuthManager,AuthManager>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]
                        ))};
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
                });
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.MapHealthChecks("/customhealthcheck",new HealthCheckOptions
            {
                Predicate = healtcheck => healtcheck.Tags.Contains("Custom"),
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                },
                ResponseWriter = WriteResponse
            });

            app.MapHealthChecks("/generalhealthcheck");

            app.MapHealthChecks("/databasehealthcheck", new HealthCheckOptions
            {
                Predicate = healtcheck => healtcheck.Tags.Contains("database"),
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                },
                ResponseWriter = WriteResponse
            });

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static Task WriteResponse(HttpContext context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            var options = new JsonWriterOptions { Indented = true };
            using var memoryStream = new MemoryStream();
            using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                jsonWriter.WriteStartObject();
                jsonWriter.WriteString("status",healthReport.Status.ToString());
                jsonWriter.WriteStartObject("results");
                foreach (var healthReportEntry in healthReport.Entries)
                {
                    jsonWriter.WriteStartObject(healthReportEntry.Key);
                    jsonWriter.WriteString("status",
                        healthReportEntry.Value.Status.ToString());
                    jsonWriter.WriteString("description",
                        healthReportEntry.Value.Description);
                    jsonWriter.WriteStartObject("data");

                    foreach (var item in healthReportEntry.Value.Data) 
                    {
                        jsonWriter.WritePropertyName(item.Key);
                        JsonSerializer.Serialize(jsonWriter, item.Value,
                            item.Value?.GetType() ?? typeof(object));   
                    }
                    jsonWriter.WriteEndObject();
                    jsonWriter.WriteEndObject();

                }
                jsonWriter.WriteEndObject();
                jsonWriter.WriteEndObject();

            }
            return context.Response.WriteAsync(
                Encoding.UTF8.GetString(memoryStream.ToArray())
                );
        }

        class CustomHealtCheck : IHealthCheck
        {
            public CustomHealtCheck()
            {
                
            }

            public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
            {
                var isHealthy = true;

                //Checks 
                if (isHealthy)
                {
                    return Task.FromResult(HealthCheckResult.Healthy("All System Are Up"));
                }
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "System Is Unhealthy"));

            }
        }
    }
} 