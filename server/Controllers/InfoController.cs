using Microsoft.AspNetCore.Mvc;

namespace server.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : Controller {
        [HttpGet("Get/{id}")]
        public string Get(string id) {
            return $"Hello {id}";
        }


    }
}