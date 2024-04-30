using InventoryManagement.InventoryService.Models;

namespace InventoryManagement.InventoryService.Services.SchemaManagement.SampleData
{
    internal static class SkuSampleData
    {
        internal static Sku WormBlanket = new Sku
        {
            Id = Guid.NewGuid(),
            Code = "IPWB20240711A",
            Description = "Innovative Products Worm Blanket (Model A)"
        };
    }
}
