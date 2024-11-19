using System.ComponentModel.DataAnnotations;

namespace ImageEventsApi.Models
{
    public class CreateImageEventRequest
    {
        [Required]
        [Url]
        public required string ImageUrl { get; set; }

        [Required]
        public required string Description { get; set; }
    }
}
