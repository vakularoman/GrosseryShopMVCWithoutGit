namespace AquaPlayground.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    [Route("[controller]")]
    public class ErrorController : Controller
    {
        [Route("401")]
        public ActionResult UnauthorizedError()
        {
            return View();
        }

        [Route("403")]
        public ActionResult ForbiddenError()
        {
            return View();
        }

        [HttpGet("404")]
        public ActionResult PageNotFoundError()
        {
            return View();
        }

        [Route("500")]
        public ActionResult ServerError(Exception ex)
        {
            if (ex is not null)
            {
                Log.Error(ex.Message);
            }

            return View();
        }
    }
}
