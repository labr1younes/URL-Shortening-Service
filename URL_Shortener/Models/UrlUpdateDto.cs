using System.ComponentModel.DataAnnotations;

public class UrlUpdateDto
{
    // This class is a DTO ( Data Transfer Objects )
    // We use it only when updating
    // 
    [Required]
    public string FullUrl { get; set; }
}
