using InventoryManagement.InventoryService.Models;
using InventoryManagement.InventoryService.Services.SchemaManagement;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<InventoryContext>("inventory");
builder.Services.AddHostedService<SchemaManagementJob>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/skus", async (InventoryContext context) =>
{
    var skus = await context.Skus.ToListAsync();
    return Results.Ok(skus);
}).Produces<List<Sku>>();

app.Run();
