namespace OrderService.Api.Sagas;

public class OrderSagaDbContextSeed : IDbSeeder<OrderSagaDbContext>
{
    public Task SeedAsync(OrderSagaDbContext context)
    {
        // No seed
        return Task.CompletedTask;
    }
}
