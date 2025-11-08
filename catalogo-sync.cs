using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using promociones.Services;

namespace product_sync
{
    public class CatalogoSync
    {
        private readonly ILogger _logger;
        private readonly ProductoSyncService _syncService;

        public CatalogoSync(ILoggerFactory loggerFactory, ProductoSyncService syncService)
        {
            _logger = loggerFactory.CreateLogger<CatalogoSync>();
            _syncService = syncService;
        }

        [Function("CatalogoSync")]
        public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"⏰ Función ejecutada a las: {DateTime.UtcNow}");

            try
            {
                await _syncService.SyncListadoAsync();
                _logger.LogInformation("✅ Sincronización completada correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Error durante sincronización: {ex.Message}");
            }
        }
    }
}