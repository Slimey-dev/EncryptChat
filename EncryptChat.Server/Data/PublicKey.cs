namespace EncryptChat.Server.Data;

public class PublicKey
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Key { get; set; }
}