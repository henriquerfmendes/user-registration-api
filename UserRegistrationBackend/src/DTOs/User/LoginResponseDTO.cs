namespace UserRegistrationBackend.DTOs.User;

public class LoginResponseDTO
{
    public required string Token { get; set; }
    public required UserResponseDTO User { get; set; }
}