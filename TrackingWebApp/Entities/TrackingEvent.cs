namespace TrackingWebApp.Entities
{
    public class TrackingEvent
    {
        public int Id { get; set; }              // Primary key
        public string EventType { get; set; } = "";
        public string Path { get; set; } = "";
        public string? ElementId { get; set; }
        public string? SessionId { get; set; }
        public DateTime TimestampUtc { get; set; }
    }
}
