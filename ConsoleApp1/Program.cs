using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var topics = new[] { "poc-net-jobs"};

            var config = new Dictionary<string, object>
            {
                { "group.id", "simple-csharp-consumer" },
                { "bootstrap.servers", "kafka-dev.ce.sdxcorp.net"/*"10.32.82.2:9092"*/ }
            };

            using (var consumer = new Consumer(config))//<Null, string>(config, null, new StringDeserializer(Encoding.UTF8)))
            {
                consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(topics.First(), 0, 0) });

                while (true)
                {
                    if (consumer.Consume(out Message  msg))
                    {
                        Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
                    }
                }
            }
        }
    }
}