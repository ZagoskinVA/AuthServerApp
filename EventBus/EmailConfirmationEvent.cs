using AuthServerApp.EventBus.Abstract;

namespace AuthServerApp.EventBus;

public class EmailConfirmationEvent: IEvent
{
    public string ExchangeName { get; } = "EmailConfirmationExchange";
    public string RoutingKeyName { get; } = "EmailConfirmationCommand";
    public string QueueName { get; } = "EmailConfirmationQueue";
}