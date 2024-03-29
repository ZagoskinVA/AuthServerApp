﻿namespace AuthServerApp.EventBus.Abstract;

public interface IEvent
{
    public string ExchangeName { get; }
    public string RoutingKeyName { get; }
    public string QueueName { get; }
}