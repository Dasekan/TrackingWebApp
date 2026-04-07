namespace TrackingWebApp.ViewModels
{
    public class AdminStatsViewModel
    {
        public List<PageCount> TopPages { get; set; } = new();
        public List<ElementCount> TopClicks { get; set; } = new();
        public List<SessionCount> TopSessions { get; set; } = new();
        public int Days { get; set; }
        public List<DailySeriesPoint> DailySeries { get; set; } = new();
    }

    public class PageCount
    {
        public string Path { get; set; } = "";
        public int Count { get; set; }
    }

    public class ElementCount
    {
        public string ElementId { get; set; } = "";
        public int Count { get; set; }
    }

    public class SessionCount
    {
        public string SessionId { get; set; } = "";
        public int Count { get; set; }

    }

    public class DailySeriesPoint
    {
        public DateTime Date { get; set; }
        public string EventType { get; set; } = "";
        public int Count { get; set; }
    }
}
