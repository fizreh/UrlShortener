﻿using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public class UrlContext : DbContext
    {
        public UrlContext(DbContextOptions<UrlContext> options) : base(options) { }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; } = default!;

    }
}
