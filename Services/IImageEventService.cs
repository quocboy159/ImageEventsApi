using ImageEventsApi.Models;

namespace ImageEventsApi.Repositories
{
    public interface IImageEventService
    {
        void AddEvent(ImageEvent imageEvent);
        int GetLastHoursCount(int hours);
        IEnumerable<ImageEvent> GetLastImageEvents(int hour);
    }
}
