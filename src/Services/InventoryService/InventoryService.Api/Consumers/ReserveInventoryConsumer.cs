using MassTransit;
using Shared.Contracts;

namespace InventoryService.Api.Consumers;

public class ReserveInventoryConsumer(ILogger<ReserveInventoryConsumer> logger, IPublishEndpoint publishEndpoint)
    : IConsumer<ReserveInventory>
{
    public async Task Consume(ConsumeContext<ReserveInventory> context)
    {
        logger.LogInformation("Reserving inventory for order: {OrderId}", context.Message.OrderId);

        // Simulate inventory check
        await Task.Delay(1000);

        // 95% success rate
        if (Random.Shared.Next(100) < 95)
        {
            await publishEndpoint.Publish(new InventoryReserved
            {
                OrderId = context.Message.OrderId
            });
        }
        else
        {
            await publishEndpoint.Publish(new OrderFailed
            {
                OrderId = context.Message.OrderId,
                Reason = "Inventory not available"
            });
        }
    }
}
