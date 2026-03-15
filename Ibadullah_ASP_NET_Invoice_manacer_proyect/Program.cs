
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddDatabase(builder.Configuration)
    .AddApplicationServices()
    .AddFluentValidationConfig()
    .AddJwtAuthentication(builder.Configuration)
    .AddSwaggerDocumentation();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
     app.UseSwaggerDocumentation();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.MapControllers();

app.Run();