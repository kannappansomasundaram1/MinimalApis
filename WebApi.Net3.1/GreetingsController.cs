using Microsoft.AspNetCore.Mvc;

namespace WebApi.Net3._1
{
    [ApiController]
    [Route("")]
    public class GreetingsController : ControllerBase
    {
        [HttpGet]
        public string Greetings()
        {
            return "Hello world";
        }
    }
}