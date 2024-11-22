using ImageEventsApi.Models;

namespace ImageEventsApi.Repositories
{
    public sealed class ImageEventService : IImageEventService
    {
        private readonly IList<ImageEvent> _imageEvents = [];

        public void AddEvent(ImageEvent imageEvent)
        {
            _imageEvents.Add(imageEvent);
        }

        public int GetLastHoursCount(int hours)
        {
            return _imageEvents.Count(x => x.CreatedDate >= DateTime.Now.AddHours(-hours));
        }

        public IEnumerable<ImageEvent> GetLastImageEvents(int hour)
        {
            return _imageEvents
                .Where(x => x.CreatedDate >= DateTime.Now.AddHours(-hour))
                .OrderByDescending(x => x.CreatedDate);
        }
    }
}
