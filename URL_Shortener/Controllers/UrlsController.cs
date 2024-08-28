using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models;

namespace URL_Shortener.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly YURL_ShortenerContext _context;
        private static readonly char[] Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        private static readonly Random Random = new Random();
        private static readonly int ShortUrlLength = 10;

       public UrlsController(YURL_ShortenerContext context)
        {
            _context = context;
        }

        // GET: api/Urls/abc35
        [HttpGet("{ShortenedUrl}")]
        public async Task<ActionResult<URLNoAccessedTimes>> GetUrl(string ShortenedUrl)
        {
            if (_context.Urls == null)
            {
                return NotFound();
            }
            var url = await _context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == ShortenedUrl);

            if (url == null)
            {
                return NotFound();
            }


            var urlnoat = new URLNoAccessedTimes
            {
                Id = url.Id,
                FullUrl = url.FullUrl,
                ShortUrl = url.ShortUrl
            };

            // Increment the AccessedTimes property in the Database 
            url.AccessedTimes = (url.AccessedTimes ?? 0) + 1;
            await _context.SaveChangesAsync();

            return urlnoat;
        }

        // GET: api/Urls/abc35
        [HttpGet("{ShortenedUrl}/stats")]
        public async Task<ActionResult<Url>> GetUrlAT(string ShortenedUrl)
        {
            if (_context.Urls == null)
            {
                return NotFound();
            }
            var url = await _context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == ShortenedUrl);

            if (url == null)
            {
                return NotFound();
            }

            return url;
        }

        // PUT: api/Urls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{ShortenedUrl}")]
        public async Task<IActionResult> PutUrl(string ShortenedUrl, UrlUpdateDto urlUpdateDto) 
        {
           
            var existingUrl = await _context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == ShortenedUrl);

            if (existingUrl == null)
            {
                return NotFound();
            }

            // Overposting Protection
            // Update only the properties specified in the urlUpdateDto
            existingUrl.FullUrl = urlUpdateDto.FullUrl;

            _context.Entry(existingUrl).State = EntityState.Modified;
            //_context.Entry(existingUrl).CurrentValues.SetValues(url);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrlExists(ShortenedUrl))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Urls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Url>> PostUrl(Url url)
        {
          if (_context.Urls == null)
          {
              return Problem("Entity set 'YURL_ShortenerContext.Urls'  is null.");
          }

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            url.ShortUrl = GenerateShortUrl(10) ;
            url.AccessedTimes = 0 ;

            _context.Urls.Add(url);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUrl), 
                new { ShortenedUrl = url.ShortUrl }, 
                url
            );
        }

        // DELETE: api/Urls/5
        [HttpDelete("{ShortenedUrl}")]
        public async Task<IActionResult> DeleteUrl(string ShortenedUrl)
        {
            var url = await _context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == ShortenedUrl);

            if (url == null)
            {
                return NotFound();
            }

            _context.Urls.Remove(url);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UrlExists(string ShortenedUrl)
        {
            return (_context.Urls?.Any(u => u.ShortUrl == ShortenedUrl)).GetValueOrDefault();
        }

        // Generate a random alphanumeric string 
        private string GenerateShortUrl(int length)
        {
            var stringBuilder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                char randomChar = Characters[Random.Next(Characters.Length)];
                stringBuilder.Append(randomChar);
            }
            return stringBuilder.ToString();
        }
    }
}
