using TrackingWebApp.Dtos;

namespace TrackingWebApp.Services
{
    public class InMemoryTrackingStore
    {
        private readonly List<TrackingEventDto> _events = new();

        public void Add(TrackingEventDto trackingEvent)
        {
            _events.Add(trackingEvent);
        }

        public IReadOnlyList<TrackingEventDto> GetAll()
        {
            return _events;
        }
    }
}
