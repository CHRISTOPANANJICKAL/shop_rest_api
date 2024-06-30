using Microsoft.AspNetCore.Identity;
namespace WebApplication1.Models.UserModel;

public class UserModel : IdentityUser
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
}