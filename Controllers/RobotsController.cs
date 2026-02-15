using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MamNonApp.Controllers
{
    /// <summary>
    /// Robots Controller - SEO: robots.txt
    /// </summary>
    public class RobotsController : Controller
    {
        /// <summary>
        /// Generate robots.txt cho SEO
        /// </summary>
        [Route("robots.txt")]
        [Produces("text/plain")]
        public IActionResult Index()
        {
            var sb = new StringBuilder();
            sb.AppendLine("User-agent: *");
            sb.AppendLine("Disallow: /admin/");
            sb.AppendLine("Disallow: /api/");
            sb.AppendLine("Allow: /");
            sb.AppendLine("");
            sb.AppendLine("Sitemap: https://mamnonlamay.edu.vn/sitemap.xml");

            return Content(sb.ToString(), "text/plain");
        }
    }
}

