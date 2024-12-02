
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly UrlContext _context;

        public HomeController(UrlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string originalUrl)
        {
            if (string.IsNullOrWhiteSpace(originalUrl))
            {
                ModelState.AddModelError("", "URL cannot be empty.");
                return View("Index");
            }

            var shortCode = GenerateShortCode();
            var shortenedUrl = new ShortenedUrl
            {
                ShortCode = shortCode,
                OriginalUrl = originalUrl
            };

            _context.ShortenedUrls.Add(shortenedUrl);
            _context.SaveChanges();

           // ViewData["ShortenedUrl"] = $"www.focusmr.de/{shortCode}";
            ViewData["ShortenedUrl"] = $"https://localhost:7298/{shortCode}";
            return View("Index");
        }

        [HttpGet("/{shortCode}")]
        public IActionResult RedirectToOriginal(string shortCode)
        {
            if (string.IsNullOrEmpty(shortCode))
            {
                return BadRequest("Invalid short code.");
            }

            // Fetch the original URL from the database
            var url = _context.ShortenedUrls.FirstOrDefault(u => u.ShortCode == shortCode);

            if (url == null)
            {
                return NotFound("Shortened URL not found.");
            }

            // Redirect to the original URL
            return Redirect(url.OriginalUrl);
        }


       // Temporary Code to see the Database Entries.
        [HttpGet("/debug/urls")]
        public IActionResult DebugUrls()
        {
            var urls = _context.ShortenedUrls.ToList();
            foreach (var url in urls)
            {
                Console.WriteLine($"ShortCode: {url.ShortCode}, OriginalUrl: {url.OriginalUrl}");
            }

            return Ok(urls);
        }


        private string GenerateShortCode()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[3]; // 5-character string
            rng.GetBytes(bytes);
            // Convert to Base64 and clean it up for URL use
            string base64String = Convert.ToBase64String(bytes)
                .Replace("+", "a")
                .Replace("/", "b")
                .Replace("=", ""); // Remove padding if any
            return base64String.Length >= 5 ? base64String.Substring(0, 5) : base64String.PadRight(5, 'x');
        }


    }
}
