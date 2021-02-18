using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models;
using server.Models.Database;

namespace server.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly DefaultContext context;

        public HomeController(ILogger<HomeController> logger, DefaultContext context) {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index() {
            Hello hello = new Hello {
                Header = "My Message",
                Message = "Hello World"
            };

            if (this.context.Hellos.Count()  == 0) {
                this.context.Add(hello);
                this.context.SaveChanges();
            }

            /* the below two lines do the same thing */
            var firstHello = (from x in this.context.Hellos
                             select x).First();
            //firstHello = this.context.Hellos.First();

            this.ViewData.Add("FirstHello", firstHello);
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
