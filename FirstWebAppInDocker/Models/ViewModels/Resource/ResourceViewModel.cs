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
        // Available types for the select list
        public List<string> AvailableTypes { get; set; } = new List<string> { "Kjøretøy", "Heisekran", "Frivillige", "Tilhenger" };

        // Selected types posted by the form (multiple selection)
        public List<string> SelectedTypes { get; set; } = new List<string>();

        // Use numeric coordinates for database storage
        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        // Can add more properties for availability, date-period, and status 
    }
}
