using MassTransit;
using Shared.Contracts;

namespace PaymentService.Api.Consumers;

public class ProcessPaymentConsumer(ILogger<ProcessPaymentConsumer> logger, IPublishEndpoint publishEndpoint)
    : IConsumer<ProcessPayment>
{
    public async Task Consume(ConsumeContext<ProcessPayment> context)
    {
        logger.LogInformation("Processing payment for order: {OrderId}", context.Message.OrderId);

        // Simulate payment processing
        await Task.Delay(1000);

        // 90% success rate
        if (Random.Shared.Next(100) < 90)
        {
            await publishEndpoint.Publish(new PaymentProcessed
            {
                OrderId = context.Message.OrderId,
                PaymentIntentId = $"pi_{Guid.NewGuid():N}"
            });
        }
        else
        {
            await publishEndpoint.Publish(new OrderFailed
            {
                OrderId = context.Message.OrderId,
                Reason = "Payment failed"
            });
        }
    }
}
