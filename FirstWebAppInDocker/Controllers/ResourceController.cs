using FirstWebAppInDocker.Models.ViewModels.Resource;   
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAppInDocker.Controllers
{
    public class ResourceController : Controller
    {
        [HttpGet]
        public IActionResult ResourceForm()
        {
            return View(new ResourceViewModel());
        }

        [HttpPost]
        public IActionResult ResourceOverview(ResourceViewModel resViewModel)
        {
            // Server-side validation
            if (!ModelState.IsValid)
            {
                return View("ResourceForm", resViewModel);
            }

            return View(resViewModel);
        }
    }
}
