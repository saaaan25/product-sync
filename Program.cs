using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DotNetEnv;
using promociones.Services;

Env.Load(); // cargar .env si usas variables locales

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        // Registrar HttpClient para ProductoSyncService
        services.AddHttpClient();

        // Registrar el servicio de sincronización
        services.AddScoped<ProductoSyncService>();
    })
    .Build();

host.Run();
