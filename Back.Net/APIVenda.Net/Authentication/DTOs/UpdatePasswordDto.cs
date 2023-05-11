namespace APIVenda.Net.Authentication.DTOs;

public class UpdatePasswordDto
{
    public string Email { get; set; }
    public string AtualPassword { get; set; }
    public string NovaPassword { get; set; }
}

