using Confluent.Kafka;

namespace Producer;

internal class Program
{
    static void Main()
    {
        var config = new ProducerConfig()
        {
            BootstrapServers = "kafka:9092"
        };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        string topic = "test-topic";

        int count = 1;

        while (count <= 1000)
        {
            var message = new Message<Null, string> { Value = $"message - {count++}" };

            producer.Produce(topic, message, deliveryHandler =>
            {
                Console.WriteLine($"Sent message:{deliveryHandler.Message.Value}");
            });

            Task.Delay(TimeSpan.FromMilliseconds(800)).Wait();
        }
    }
}