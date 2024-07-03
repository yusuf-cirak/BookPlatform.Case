
namespace BookPlatform.Domain;

public class UserFriend : BaseEntity
{
    public string UserId { get; set; }
    public string FriendUserId { get; set; }
    
    public virtual User User { get; set; }
    
    public virtual User FriendUser { get; set; }
    
    public UserFriend(string userId, string friendUserId)
    {
        UserId = userId;
        FriendUserId = friendUserId;
    }
    
    public static UserFriend Create(string userId, string friendUserId)
    {
        return new UserFriend(userId, friendUserId);
    }
    
    public override string ToString()
    {
        return $"{UserId} - {FriendUserId}";
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        var userFriend = (UserFriend)obj;
        return UserId == userFriend.UserId && FriendUserId == userFriend.FriendUserId;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, FriendUserId);
    }
    
}