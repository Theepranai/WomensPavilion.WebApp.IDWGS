using Microsoft.AspNetCore.Identity;
using PixelPlusMedia.API.Middleware;
using PixelPlusMedia.Application;
using PixelPlusMedia.Identity;
using PixelPlusMedia.Identity.Models;
using Serilog;
using Microsoft.OpenApi.Models;
using PixelPlusMedia.API.Utility;
using PixelPlusMedia.Persistence;
using PixelPlusMedia.Application.Contracts;
using PixelPlusMedia.API.Services;
using Microsoft.AspNetCore.Antiforgery;
using System.Text.Json.Serialization;
using Amazon.S3;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// var builder = WebApplication.CreateBuilder(args);

var builder = WebApplication.CreateBuilder(args);

// Get configuration
ConfigurationManager config = builder.Configuration;

IWebHostEnvironment environment = builder.Environment;

builder.Host.UseSerilog();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
}).AddNewtonsoftJson();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(config);
builder.Services.AddIdentityServices(config);
builder.Services.AddAntiforgery(opts => opts.HeaderName = "X-XSRF-TOKEN");
builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\n Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "IDWGS API",
    });
    c.OperationFilter<FileResultContentTypeOperationFilter>();
});

builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
    setupAction.ReportApiVersions = true;
});

/*builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});*/


// Autorizarion Policy Mapper
builder.Services.AddAuthorization(options => PoliciesMapper.MapPoliciesOptions(options));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
        await PixelPlusMedia.Identity.Seed.CreateFirstUser.SeedAsync(userManager, roleManager);
        Log.Information("Application Starting");
    }
    catch (Exception ex)
    {
        Log.Warning(ex, "An error occured while starting the application");
    }
}

app.UseRouting();
app.UseAuthentication();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseSwagger();
app.UseSwaggerUI();

app.UseCustomExceptionHandle();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/csrf/token", (IAntiforgery forgeryService, HttpContext context) =>
{
    var tokens = forgeryService.GetAndStoreTokens(context);
    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken!,
            new CookieOptions { HttpOnly = false});

    return Results.Ok();
});

app.UseAntiXssMiddleware();

app.UseHostFiltering();
// Disabled Map Controller
// app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapFallbackToController("Index", "Fallback");
app.Run();