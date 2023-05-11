using System.ComponentModel.DataAnnotations;

namespace APIVenda.Net.Authentication.Entities;

public class RefreshToken
{

    [Key]
    public Guid Id { get; set; }

    public string Email { get; set; }
    public string TokenRefresh { get; set; }

    public RefreshToken(Guid id, string email, string tokenRefresh)
    {
        Id = id;
        Email = email;
        TokenRefresh = tokenRefresh;
    }

    public RefreshToken(string email, string tokenRefresh)
    {
        Email = email;
        TokenRefresh = tokenRefresh;
    }
}
