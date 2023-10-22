namespace AuthServerApp.EventBus.Abstract;

public interface IRabbitMqProducer<T> where T: IEvent
{
    public void Publish<U>(U @event);
}