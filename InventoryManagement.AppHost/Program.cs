var builder = DistributedApplication.CreateBuilder(args);

var postgresServer = builder.AddPostgres("impgsql").PublishAsAzurePostgresFlexibleServer().WithPgAdmin();
var inventoryDatabase = postgresServer.AddDatabase("inventory");

builder.AddProject<Projects.InventoryManagement_InventoryService>("inventoryservice")
       .WithReference(inventoryDatabase);

builder.Build().Run();
