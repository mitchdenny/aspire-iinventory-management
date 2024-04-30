
using InventoryManagement.InventoryService.Models;
using InventoryManagement.InventoryService.Services.SchemaManagement.SampleData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InventoryManagement.InventoryService.Services.SchemaManagement
{
    public class SchemaManagementJob(ILogger<SchemaManagementJob> logger, IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Starting schema management job.");

            using var periodic = new PeriodicTimer(TimeSpan.FromSeconds(10));

            do
            {
                try
                {
                    using var scope = serviceProvider.CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<InventoryContext>();
                    await context.Database.EnsureCreatedAsync(stoppingToken);
                    await PopulateSampleData(context, stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to migrate database.");
                }
            } while (await periodic.WaitForNextTickAsync(stoppingToken));

            logger.LogInformation("Finishing schema management job.");
        }

        private static async Task PopulateSampleData(InventoryContext context, CancellationToken cancellationToken)
        {
            if (await context.Skus.SingleOrDefaultAsync(sku => sku.Id == SkuSampleData.WormBlanket.Id) is not { })
            {
                await context.Skus.AddAsync(SkuSampleData.WormBlanket, cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
