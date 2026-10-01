using System.ComponentModel.DataAnnotations;

namespace FirstWebAppInDocker.Models.Entities
{
    public class Resource
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(4000)]
        public string? Description { get; set; }

        [MaxLength(200)]
        public string? Type { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
