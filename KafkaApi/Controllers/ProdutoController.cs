using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KafkaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
namespace KafkaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {

        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoProducer producer;

        public ProdutoController(IProdutoProducer producer, ILogger<ProdutoController> logger)
        {
            _logger = logger;
            this.producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Produto produto) {
            var mensagem = JsonConvert.SerializeObject(produto);
            var resultado = await this.producer.EnviarMensagem("produtos", mensagem);
            return Ok(produto);
        }
    }
}
