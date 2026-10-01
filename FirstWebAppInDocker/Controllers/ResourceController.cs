using FirstWebAppInDocker.Models.ViewModels.Resource;
using FirstWebAppInDocker.DataAccess;
using FirstWebAppInDocker.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAppInDocker.Controllers
{
    public class ResourceController : Controller
    {
        private readonly FirstWebAppInDocker.DataAccess.Repositories.IResourceRepository _repo;

        public ResourceController(FirstWebAppInDocker.DataAccess.Repositories.IResourceRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult ResourceForm()
        {
            // Provide available types to the form
            var vm = new ResourceViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult ResourceOverview(ResourceViewModel resViewModel)
        {
            // Server-side validation
            if (!ModelState.IsValid)
            {
                return View("ResourceForm", resViewModel);
            }

            // Map view model to entity
            var resource = new Resource
            {
                Name = resViewModel.Name ?? string.Empty,
                Description = resViewModel.Description,
                Type = resViewModel.SelectedTypes != null ? string.Join(',', resViewModel.SelectedTypes) : null,
                Latitude = resViewModel.Latitude,
                Longitude = resViewModel.Longitude,
                CreatedAt = DateTime.UtcNow
            };

            // Save via repository
            _repo.AddAsync(resource).GetAwaiter().GetResult();

            // Optionally you could redirect to a details page; here we'll show the overview with the posted data
            return View(resViewModel);
        }
    }
}
