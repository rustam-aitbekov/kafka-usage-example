using Confluent.Kafka;

namespace Consumer;

internal class Program
{
    static void Main()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "kafka:9092",
            GroupId = "test-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("test-topic");

        while (true)
        {
            var message = consumer.Consume();
            Console.WriteLine($"Received message:{message.Message.Value}");
        }
    }
}