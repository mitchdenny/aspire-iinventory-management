using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.InventoryService.Models;

public class InventoryContext(DbContextOptions<InventoryContext> options) : DbContext(options)
{
    public DbSet<Sku> Skus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
