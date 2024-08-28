using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace URL_Shortener.Models
{
    public partial class Url
    {
        public int Id { get; set; }

        //[Required]
        public string FullUrl { get; set; } = null!;

        //[JsonIgnore]
        public string ShortUrl { get; set; } 

        public int? AccessedTimes { get; set; }
    }
}
