using FluentValidation;
using FluentValidation.AspNetCore;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Data;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Services;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Services.Interfaces;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Controllers + FluentValidation
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateInvoiceDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// 🔹 DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnectionString")));

// 🔹 Services (Dependency Injection)
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUserService, UserService>();

// 🔹 JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],

        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// 🔹 Authorization
builder.Services.AddAuthorization();

// 🔹 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Invoice Manager API",
        Version = "v1",
        Description = "Invoice Manager API with JWT authentication",
        Contact = new OpenApiContact
        {
            Name = "Ibadulla Huseynzade",
            Email = "h.ibad.3008@gmail.com"
        }
    });

    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);

    // Swagger JWT Authorization support
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// 🔹 Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice Manager API v1");
        c.RoutePrefix = string.Empty;
    });
}

// 🔹 JWT Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// 🔹 Map controllers
app.MapControllers();

app.Run();