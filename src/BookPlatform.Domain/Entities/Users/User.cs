namespace BookPlatform.Domain;

public class User : AuditEntity
{
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public virtual ICollection<UserFriend> UserFriends { get; set; }


    public static User Create(string id, string username, byte[] passwordHash, byte[] passwordSalt)
    {
        return new User
        {
            Id = id,
            Username = username,
            PasswordSalt = passwordSalt,
            PasswordHash = passwordHash
        };
    }

    public static User Create(string username, byte[] passwordHash, byte[] passwordSalt)
    {
        return new User
        {
            Username = username,
            PasswordSalt = passwordSalt,
            PasswordHash = passwordHash
        };
    }
}