using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackingWebApp.Data;
using TrackingWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TrackingWebApp.Services;

namespace TrackingWebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly TrackingRetentionService _retention;

       

        public AdminController(AppDbContext db, IConfiguration configuration, TrackingRetentionService retention)
        {
            _db = db;
            _configuration = configuration;
            _retention = retention;
        }

        // /Admin/Events
        public async Task<IActionResult> Events()
        {
            var events = await _db.TrackingEvents
                .OrderByDescending(e => e.TimestampUtc)
                .Take(500)
                .ToListAsync();

            return View(events);
        }

        public async Task<IActionResult> Stats(int days = 15)
        {
            var allowedDays = new[] { 1, 7, 15, 30 };
            if (!allowedDays.Contains(days))
                days = 15;

            // 2) Retention (slet alt ældre end 15 dage i DB)
            await _retention.DeleteOlderThanDaysAsync(15);

            // 3) Date-range filter (hvad vi viser i dashboardet)
            var cutoff = DateTime.UtcNow.AddDays(-days);

            // Top pages (pageviews)
            var topPages = await _db.TrackingEvents
                .Where(e => e.TimestampUtc >= cutoff && e.EventType == "pageview" && !e.Path.StartsWith("/Admin"))
                .GroupBy(e => e.Path)
                .Select(g => new PageCount
                {
                    Path = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToListAsync();

            var pageNameMap = new Dictionary<string, string>
            {
                { "/", "Forside" },
                { "/Site/Services", "Services" },
                { "/Site/Cases", "Cases" },
                { "/Site/About", "Om os" },
                { "/Site/Contact", "Kontakt" }
            };

            foreach (var page in topPages)
            {
                if (pageNameMap.TryGetValue(page.Path, out var friendlyName))
                {
                    page.Path = friendlyName;
                }
            }


            // Top clicks (click events grouped by elementId)
            var topClicks = await _db.TrackingEvents
                .Where(e => e.TimestampUtc >= cutoff && e.EventType == "click" && e.ElementId != null)
                .GroupBy(e => e.ElementId!)
                .Select(g => new ElementCount
                {
                    ElementId = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToListAsync();
            var clickNameMap = new Dictionary<string, string>
            {
                { "cta_services", "Se services (forside)" },
                { "cta_cases", "Se cases (forside)" },
                { "cta_contact", "Kontakt (forside)" },

                { "service_webapps", "Webapplikationer" },
                { "service_integration", "API & integration" },
                { "service_tracking", "Tracking & dashboards" },

                { "case_portal", "Case: Kundeportal" },
                { "case_erp", "Case: ERP integration" },

                { "about_cta_contact", "Kontakt (Om os)" },
                { "contact_submit", "Kontakt: Send besked" },

                
            };

            foreach (var click in topClicks)
            {
                if (clickNameMap.ContainsKey(click.ElementId))
                {
                    click.ElementId = clickNameMap[click.ElementId];
                }
            }

            // Top sessions (events per session)
            var topSessions = await _db.TrackingEvents
                .Where(e => e.TimestampUtc >= cutoff && e.SessionId != null)
                .GroupBy(e => e.SessionId!)
                .Select(g => new SessionCount
                {
                    SessionId = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            var daily = await _db.TrackingEvents
            .Where(e => e.TimestampUtc >= cutoff && (e.EventType == "pageview" || e.EventType == "click"))
            .GroupBy(e => new { Date = e.TimestampUtc.Date, e.EventType })
            .Select(g => new
            {
                 g.Key.Date,
                 g.Key.EventType,
                 Count = g.Count()
            })
                .OrderBy(x => x.Date)
                .ToListAsync();

            var vm = new AdminStatsViewModel
            {
                Days = days,
                TopPages = topPages,
                TopClicks = topClicks,
                TopSessions = topSessions,
                DailySeries = daily.Select(x => new DailySeriesPoint
                {
                    Date = x.Date,
                    EventType = x.EventType,
                    Count = x.Count
                }).ToList()
            };

            return View(vm);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var configUser = _configuration["AdminAuth:Username"];
            var configPass = _configuration["AdminAuth:Password"];

            if (username == configUser && password == configPass)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin")
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Stats");
            }

            ViewBag.Error = "Forkert brugernavn eller password.";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearAllTrackingData()
        {
            await _retention.DeleteAllAsync();
            TempData["Message"] = "✅ Alt tracking-data er blevet slettet.";
            return RedirectToAction("Stats");
        }
    }
}
