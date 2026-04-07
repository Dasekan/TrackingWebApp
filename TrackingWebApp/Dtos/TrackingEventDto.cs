namespace TrackingWebApp.Dtos
{
    public class TrackingEventDto
    {
        public string EventType { get; set; } = "";   // "pageview" eller "click"
        public string Path { get; set; } = "";        // fx "/Pages/Index"
        public string? ElementId { get; set; }        // fx "testButton"
        public string? SessionId { get; set; }        // kommer i dag 3
        public DateTime TimestampUtc { get; set; }    // kommer fra client eller server
    }
}
