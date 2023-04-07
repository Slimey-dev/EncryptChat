namespace EncryptChat.Client.Model;

public class Notification
{
    public Guid RoomId { get; set; }
    public string? OwnerName { get; set; }
    public string? EncryptedSymmetricKey { get; set; }
}