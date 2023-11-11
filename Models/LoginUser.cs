#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TheWall.Models;
public class LoginUser
{
    [Required]
    public string EmailLogin { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string PasswordLogin { get; set; }
}
