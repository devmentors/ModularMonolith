using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.Modules.Speakers.Api.Controllers
{
    [Route(BasePath)]
    internal class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get() => "Speakers module";
    }
}