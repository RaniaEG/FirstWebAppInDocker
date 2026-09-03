using System.ComponentModel.DataAnnotations;

namespace FirstWebAppInDocker.Models.ViewModels.Resource
{
    public class ResourceViewModel
    {
        // Maybe change the text of the error message to Norwegian
        [Required(ErrorMessage = "Resource Name is required")]
        public string? Name { get; set; } 
        [Required(ErrorMessage = "Resource Description is required")] // Can add length for description as a validation rule
        public string? Description { get; set; } 
        [Required(ErrorMessage = "Resource Type is required")]
        public List<string> Type { get; set; } = new List<string> { "Kjøretøy", "Heisekran", "Frivillige", "Tilhenger" };

        // Check that strings are in good format or other format (decimal, float, double) for latitude and longitude
        public string? Latitude { get; set; } 
        
        public string? Longitude { get; set; } 

        // Can add more properties for availability, date-period, and status 
    }
}
