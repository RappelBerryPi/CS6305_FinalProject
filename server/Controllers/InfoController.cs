using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models.Database;

namespace server.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : Controller {
        [HttpGet("Get/{id}")]
        [Authorize]
        public Hello Get(string id) {
            var returnObj = new Hello();
            returnObj.Id = 1;
            returnObj.Header = "Hello";
            returnObj.Message = id;
            return returnObj;
        }


    }
}