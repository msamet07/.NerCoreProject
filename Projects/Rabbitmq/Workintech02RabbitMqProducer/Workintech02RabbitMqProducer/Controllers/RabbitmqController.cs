using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Workintech02RabbitMqProducer.Producer;

namespace Workintech02RabbitMqProducer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RabbitmqController : ControllerBase
    {
        private readonly IProducer producer;

        public RabbitmqController(IProducer _producer)
        {
            producer = _producer;
        }

        [HttpPost]
        public IActionResult Post(string message)
        {
            producer.SendMessage(message);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var sampleMovie = new Models.Movie
            {
                Title = "The Shawshank Redemption",
                Director = "Frank Darabont",
                Year = 1994,
                Rating = 9.3,
                IMDBUrl = "https://www.imdb.com/title/tt0111161/",
                ImageUrl = "https://www.imdb.com/title/tt0111161/mediaviewer/rm2487852032/"
            };

            string message = JsonSerializer.Serialize(sampleMovie);
            producer.SendMessage(message);
            return Ok("Mesaj Kuyruğa bırakıldı");
        }

        [HttpGet]
        public IActionResult SendQueueMessage(int messageCount)
        {
            for (int i = 0; i < messageCount; i++)
            {
                producer.SendMessage($"Message {i}");
            }
            
            return Ok($"{messageCount} Mesaj Kuyruğa bırakıldı");
        }

    }
}
