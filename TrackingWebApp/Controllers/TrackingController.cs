using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackingWebApp.Data;
using TrackingWebApp.Entities;
using TrackingWebApp.Dtos;


namespace TrackingWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TrackingController(AppDbContext db)
        {
            _db = db;
        }
        // 1) Hurtig test for at se at controller virker
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { message = "Tracking API virker!" });
        }

        // 2) Endpoint der modtager tracking events (vi gemmer dem først i uge 3 dag 4)
        [HttpPost("event")]
        public async Task<IActionResult> ReceiveEvent([FromBody] TrackingEventDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.EventType) || string.IsNullOrWhiteSpace(dto.Path))
            {
                return BadRequest(new { message = "Ugyldigt tracking-event" });
            }

            // Sæt timestamp på serveren (mest stabilt)
            var entity = new TrackingEvent
            {
                EventType = dto.EventType,
                Path = dto.Path,
                ElementId = dto.ElementId,
                SessionId = dto.SessionId,
                TimestampUtc = DateTime.UtcNow
            };

            _db.TrackingEvents.Add(entity);
            await _db.SaveChangesAsync();

            var total = await _db.TrackingEvents.CountAsync();

            return Ok(new
            {
                message = "Event gemt i database",
                totalEvents = total
            });
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _db.TrackingEvents
                .OrderByDescending(e => e.TimestampUtc)
                .Take(200)
                .ToListAsync();

            return Ok(events);
        }
    }
}
