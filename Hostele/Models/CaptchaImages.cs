using System.ComponentModel.DataAnnotations;

namespace Hostele.Models;

public class CaptchaImages
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string Answer { get; set; }
    public string Path { get; set; }
}