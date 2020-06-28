using Kafka.Client.Extensions;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace KafkaApi
{
    public class ProdutoProducer : IProdutoProducer
    {
        IKafkaClient<string, string> kafkaClient;

        ILogger<ProdutoProducer> logger;

        public ProdutoProducer(IKafkaClient<string, string> kafkaClient, ILogger<ProdutoProducer> logger)
        {
            this.kafkaClient = kafkaClient;
            this.logger = logger;
        }

        public async Task<string> EnviarMensagem(string topico, string mensagem) {
            var msg = new Message<string, string>{
                Key = Guid.NewGuid().ToString(),
                Value = mensagem
            };
            var resultado = await kafkaClient.ProduceAsync(topico, msg);

            if(resultado != null) {
                logger.LogInformation($"Delivered {resultado.Value} to {resultado.TopicPartitionOffset}");
                return $"Delivered {resultado.Value} to {resultado.TopicPartitionOffset}";
            }
            else {
                return "Erro";
            }

        }
        
    }
}