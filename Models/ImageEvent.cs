namespace ImageEventsApi.Models
{
    public sealed class ImageEvent
    {
        public string ImageUrl { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedDate { get; private set; }

        private ImageEvent(string imageUrl, string description)
        {
            ImageUrl = imageUrl;
            Description = description;
            CreatedDate = DateTime.Now;
        }

        public static ImageEvent CreateImageEvent(string imageUrl, string description)
        {
            imageUrl = imageUrl ?? throw new ArgumentNullException(nameof(imageUrl));
            description = description ?? throw new ArgumentNullException(nameof(description));
            var imageEvent = new ImageEvent(imageUrl, description);
            return imageEvent;
        }
    }
}
