
using Microsoft.AspNetCore.Identity;

namespace BookPlatform.Domain;

public class User : IdentityUser
{
    public virtual ICollection<UserFriend> UserFriends { get; set; }
}