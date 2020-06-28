using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Kafka.Client.Extensions;
using Confluent.Kafka;
namespace KafkaWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        IKafkaClient<string, string> kafkaClient;


        public Worker(IKafkaClient<string, string> kafkaClient, ILogger<Worker> logger)
        {
            this.kafkaClient = kafkaClient;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var resultado = kafkaClient.Consume("produtos");

                if(resultado != null) {
                    _logger.LogInformation($"Consumed message {resultado.Value} at {resultado.TopicPartitionOffset}");
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
