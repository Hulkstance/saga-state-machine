using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Api.Sagas;
using Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsConstants.AllowAll, policyBuilder =>
    {
        policyBuilder
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<OrderSagaDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgresdb"));
});
builder.EnrichNpgsqlDbContext<OrderSagaDbContext>();

builder.Services.AddMigration<OrderSagaDbContext, OrderSagaDbContextSeed>();

builder.Services.AddMassTransit(x =>
{
    x.AddSagaStateMachine<OrderStateMachine, OrderState>()
        .EntityFrameworkRepository(r =>
        {
            r.ConcurrencyMode = ConcurrencyMode.Pessimistic; // or use Optimistic, which requires RowVersion
            r.AddDbContext<DbContext, OrderSagaDbContext>();
            r.UsePostgres();
        });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("messaging"));
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/v1/openapi.json");
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1/openapi.json", "Example API");
        options.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();
app.UseCors(CorsConstants.AllowAll);

app.MapControllers();

app.MapDefaultEndpoints();

app.Run();
