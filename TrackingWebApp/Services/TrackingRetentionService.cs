using TrackingWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace TrackingWebApp.Services
{
    public class TrackingRetentionService
    {
        private readonly AppDbContext _db;

        public TrackingRetentionService(AppDbContext db)
        {
            _db = db;
        }

        // Slet events ældre end X dage
        public async Task<int> DeleteOlderThanDaysAsync(int days)
        {
            var cutoff = DateTime.UtcNow.AddDays(-days);

            var deleted = await _db.TrackingEvents
                .Where(e => e.TimestampUtc < cutoff)
                .ExecuteDeleteAsync(); // EF Core 7/8: sletter direkte i DB

            return deleted;
        }

        // Slet ALT
        public async Task<int> DeleteAllAsync()
        {
            var deleted = await _db.TrackingEvents.ExecuteDeleteAsync();
            return deleted;
        }
    }
}
