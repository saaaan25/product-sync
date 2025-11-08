using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using promociones.Data; // 👈 aquí está tu AppDBContext
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

namespace product_sync
{
    public class CatalogoSync
    {
        private readonly ILogger _logger;

        public CatalogoSync(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CatalogoSync>();
            Env.Load(); // carga variables desde .env
        }

        [Function("CatalogoSync")]
        public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"⏰ Función ejecutada a las: {DateTime.Now}");

            try
            {
                using var context = new AppDBContext(
                    new DbContextOptionsBuilder<AppDBContext>()
                        .UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL"))
                        .Options
                );

                // TODO: lógica de sincronización
                _logger.LogInformation("✅ Sincronización completada correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Error durante sincronización: {ex.Message}");
            }

            await Task.CompletedTask;
        }
    }
}
