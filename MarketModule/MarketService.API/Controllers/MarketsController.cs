using MarketService.API.Dtos;
using MarketService.Data.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;

namespace MarketService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketsController : ControllerBase
    {

        private readonly MarketRepository _marketRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public MarketsController(MarketRepository marketRepository, IPublishEndpoint publishEndpoint)
        {
            _marketRepository = marketRepository;
            _publishEndpoint = publishEndpoint;
        }

        /*Markete bir item eklendiğinde eklenen itemin 
         envanterden düşmesini istiyoruz. Bununiçin itemin market ile
        itemin de envanterle iletişim kurması gerekiyor. HttpClient ile senkron değil,
        Rabbit MQ ya da Kafka.. vs. gibi bir message broker ile asenkron istek atabiliriz.
        Distributed transaction araçları da => MassTransit || CAP  
        
        Markete bir item eklenmesi işlemi aslında Marketin create olması yani 
        Market Created işlemi oluyor. Yani markete item koyduğumuzda, market oluşuyor ve
        envanter'den bir item düşüyor (Count'undan satışa koyulan ürün sayısı kadar düşüyor)*/

        [HttpGet]
        public IActionResult Get()
        {
            var result = _marketRepository.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMarketDto dto)
        {
            var result = _marketRepository.Add(new Data.Entities.Market
            {
                InventoryId = dto.InventoryId,
                ItemId = dto.ItemId,
                Price = dto.price,
                PlayerId = dto.PlayerId,
            });

            await _publishEndpoint.Publish<MarketCreated>(new
            {
                dto.ItemId,
                dto.InventoryId,
                Count = 1
            });
            return Created("", result);
        }
    }
}
