using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Paqueteria.API.Configurations;
using Paqueteria.API.Middlewares;
using Paqueteria.Core.Settings;
using Paqueteria.Infrastructure;
using Paqueteria.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
if (jwtSettings == null || string.IsNullOrWhiteSpace(jwtSettings.Key))
{
    throw new InvalidOperationException(
        $"Error Crítico de Configuración: La clave para la sección '{JwtSettings.SectionName}' no se encuentra definida en el entorno.");
}
var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers()
    .ConfigureCustomValidationErrorResponse();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); 
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; // Cambiar a true en producción con SSL
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero // Elimina el margen de 5 min por defecto
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseExceptionHandler(); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al aplicar las migraciones de EF Core en el arranque.");
    }
}

app.Run();