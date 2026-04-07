using Microsoft.AspNetCore.Mvc;

namespace TrackingWebApp.Controllers
{
    public class SiteController : Controller
    {
        public IActionResult Index() => View();      // /
        public IActionResult Services() => View();   // /Site/Services
        public IActionResult Cases() => View();      // /Site/Cases
        public IActionResult About() => View();      // /Site/About
        public IActionResult Contact() => View();    // /Site/Contact
    }
}
