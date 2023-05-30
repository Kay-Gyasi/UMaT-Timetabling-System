using System.ComponentModel.DataAnnotations;

namespace UMaTLMS.Web.Models;

public class SigninRequest
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
