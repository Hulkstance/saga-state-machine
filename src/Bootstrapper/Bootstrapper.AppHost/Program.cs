var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin(pgAdmin => pgAdmin.WithHostPort(5050));
var postgresDb = postgres.AddDatabase("postgresdb");

var rabbitMq = builder.AddRabbitMQ("messaging")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithManagementPlugin()
    .WithImage("masstransit/rabbitmq");

builder.AddProject<Projects.InventoryService_Api>("inventory-service-api")
    .WithExternalHttpEndpoints()
    .WithReference(rabbitMq).WaitFor(rabbitMq);

builder.AddProject<Projects.OrderService_Api>("order-service-api")
    .WithExternalHttpEndpoints()
    .WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithReference(postgresDb);

builder.AddProject<Projects.PaymentService_Api>("payment-service-api")
    .WithExternalHttpEndpoints()
    .WithReference(rabbitMq).WaitFor(rabbitMq);

builder.Build().Run();
