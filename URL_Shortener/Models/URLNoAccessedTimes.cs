namespace URL_Shortener.Models
{
    public class URLNoAccessedTimes
    {
        // This class is a DTO ( Data Transfer Objects )
        public int Id { get; set; }
        public string FullUrl { get; set; } = null!;
        public string ShortUrl { get; set; } = null!;
    }
}
