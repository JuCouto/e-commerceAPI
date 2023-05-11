namespace APIVenda.Net.Authentication.DTOs;

public class RecoverPasswordDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Codigo { get; set; }
}
