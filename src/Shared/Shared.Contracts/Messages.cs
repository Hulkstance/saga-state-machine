namespace Shared.Contracts;

public record OrderSubmitted
{
    public required Guid OrderId { get; init; }
    public required decimal Total { get; init; }
    public required string Email { get; init; }
}

public record ProcessPayment
{
    public required Guid OrderId { get; init; }
    public required decimal Amount { get; init; }
}

public record PaymentProcessed
{
    public required Guid OrderId { get; init; }
    public required string PaymentIntentId { get; init; }
}

public record ReserveInventory
{
    public required Guid OrderId { get; init; }
}

public record InventoryReserved
{
    public required Guid OrderId { get; init; }
}

public record RefundPayment
{
    public required Guid OrderId { get; init; }
    public required decimal Amount { get; init; }
}

public record OrderConfirmed
{
    public required Guid OrderId { get; init; }
}

public record OrderFailed
{
    public required Guid OrderId { get; init; }
    public required string Reason { get; init; }
}
