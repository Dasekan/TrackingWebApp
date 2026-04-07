using Microsoft.EntityFrameworkCore;
using TrackingWebApp.Entities;

namespace TrackingWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TrackingEvent> TrackingEvents => Set<TrackingEvent>();
    }
}
