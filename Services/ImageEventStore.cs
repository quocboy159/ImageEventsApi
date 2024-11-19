using ImageEventsApi.Models;
using System.Collections.Concurrent;

namespace ImageEventsApi.Repositories
{
    public static class ImageEventStore
    {
        private static ConcurrentQueue<ImageEvent> _imageEvents = [];

        public static void Add(ImageEvent imageEvent)
        {
            _imageEvents.Enqueue(imageEvent);
        }

        public static int GetLastHoursCount(int hours)
        {
            return _imageEvents.Count(x => x.CreatedDate >= DateTime.Now.AddHours(-hours));
        }

        public static IEnumerable<ImageEvent> GetLastImageEvents(int hour)
        {
            return _imageEvents.Where(x => x.CreatedDate >= DateTime.Now.AddHours(-hour));
        }
    }
}
