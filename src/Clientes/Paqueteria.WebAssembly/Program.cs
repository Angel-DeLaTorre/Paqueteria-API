using System.Globalization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Paqueteria.Cliente.Core.Auth;
using Paqueteria.Cliente.Core.Handlers;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Cliente.Core.Services;
using Paqueteria.UI;
using Paqueteria.WebAssembly.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configurar cultura en español (México)
var cultura = new CultureInfo("es-MX");
CultureInfo.DefaultThreadCurrentCulture = cultura;
CultureInfo.DefaultThreadCurrentUICulture = cultura;

// Componente raíz mapeado a la carpeta Components de Paqueteria.UI
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 2. Interceptor HTTP para JWT
builder.Services.AddScoped<ITokenStorage, WebAssemblyTokenStorage>();

// 2. Interceptor HTTP para JWT
builder.Services.AddTransient<AuthorizationHandler>();

// 3. Cliente HTTP
builder.Services.AddHttpClient("PaqueteriaApi",
        client => { client.BaseAddress = new Uri("http://localhost:5005/api/"); })
    .AddHttpMessageHandler<AuthorizationHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("PaqueteriaApi"));

// 4. Autenticación de Blazor
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// 5. Servicios de Dominio
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

builder.Services.AddCustomUiComponents();

await builder.Build().RunAsync();