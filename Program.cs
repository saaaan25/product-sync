using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DotNetEnv;
using promociones.Data; // referencia a tu proyecto promociones-service, si defines algún DbContext
using Microsoft.EntityFrameworkCore;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults() // esto inicializa el Azure Functions Worker (no ASP.NET Core)
    .ConfigureServices((context, services) =>
    {
        // Cargar variables del archivo .env
        Env.Load();

        var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
            ?? throw new InvalidOperationException("DATABASE_URL no está configurada.");

        // Registrar el contexto de base de datos de tu proyecto promociones-service
        services.AddDbContext<AppDBContext>(options =>
            options.UseNpgsql(connectionString));
    })
    .Build();

host.Run();
