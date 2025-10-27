using Microsoft.AspNetCore.Identity;
namespace backend.models
{
    public class AuthorAccount : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
