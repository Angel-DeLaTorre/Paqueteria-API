using System.Globalization;
using Microsoft.AspNetCore.Components.Authorization;
using Paqueteria.Cliente.Core.Auth;
using Paqueteria.Cliente.Core.Handlers;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Cliente.Core.Services;
using Paqueteria.Desktop.Services;
using Paqueteria.UI;

namespace Paqueteria.Desktop;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        // 1. Configurar cultura idéntica a la Web (español México)
        var cultura = new CultureInfo("es-MX");
        CultureInfo.DefaultThreadCurrentCulture = cultura;
        CultureInfo.DefaultThreadCurrentUICulture = cultura;

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

        // Registro de almacenamiento seguro nativo para Desktop
        builder.Services.AddSingleton<ITokenStorage, DesktopTokenStorage>();

        // Handler HTTP para interceptar JWT
        builder.Services.AddTransient<AuthorizationHandler>();

        // 2. Cliente HTTP (Corregido con el puerto real 5005 de tu Web)
        builder.Services.AddHttpClient("PaqueteriaApi",
                client => { client.BaseAddress = new Uri("http://localhost:5005/api/"); })
            .AddHttpMessageHandler<AuthorizationHandler>();

        builder.Services.AddScoped(sp =>
            sp.GetRequiredService<IHttpClientFactory>().CreateClient("PaqueteriaApi"));

        // Autenticación de Blazor
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

        // 3. Servicios de Dominio duplicados para que funcionen las páginas en Desktop
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IChoferServicio, ChoferServicio>();
        builder.Services.AddScoped<IClienteServicio, ClienteServicio>();
        builder.Services.AddScoped<IArticuloServicio, ArticuloServicio>();
        builder.Services.AddScoped<IRutaServicio, RutaServicio>();
        builder.Services.AddScoped<ISucursalServicio, SucursalServicio>();
        builder.Services.AddScoped<ISeguroServicio, SeguroServicio>();
        builder.Services.AddScoped<IGuiaServicio, GuiaServicio>();
        builder.Services.AddScoped<IAsignacionServicio, AsignacionServicio>();
        builder.Services.AddScoped<IMunicipioServicio, MunicipioServicio>();
        builder.Services.AddScoped<IEstadoServicio, EstadoServicio>();
        builder.Services.AddScoped<IRolServicio, RolServicio>();
        builder.Services.AddScoped<IPermisoServicio, PermisoServicio>();
        builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();

        // 4. Inicializador de tus componentes compartidos de UI
        builder.Services.AddCustomUiComponents();

        return builder.Build();
    }
}
