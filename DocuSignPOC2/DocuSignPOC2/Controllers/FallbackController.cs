using Microsoft.AspNetCore.Mvc;

namespace DocuSignPOC2.Controllers
{
    public class FallbackController : Controller
    {
        public IActionResult Index()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "index.html");
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "index.html"), "text/HTML");
        }
    }
}
