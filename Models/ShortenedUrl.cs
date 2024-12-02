using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class ShortenedUrl
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OriginalUrl { get; set; }

        [Required]
        public string ShortCode { get; set; }

    }
}
