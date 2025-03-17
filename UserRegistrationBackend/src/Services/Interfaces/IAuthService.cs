using UserRegistrationBackend.DTOs.User;

namespace UserRegistrationBackend.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDTO> Login(LoginDTO loginDTO);
}