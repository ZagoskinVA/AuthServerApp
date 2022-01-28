using Plain.RabbitMQ;

namespace AuthServerApp.Services
{
    public class SendMessageService
    {
        private readonly IPublisher _publisher;

        public SendMessageService(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void SendMessage(string message, string key) 
        {
            _publisher.Publish(message, key, null);
        }
    }
}
