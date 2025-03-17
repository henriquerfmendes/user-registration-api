using UserRegistrationBackend.DTOs.User;
using UserRegistrationBackend.Services.Interfaces;
using UserRegistrationBackend.Repositories.Interfaces;

namespace UserRegistrationBackend.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;
    private readonly ITokenService _tokenService;
    public AuthService(
        IUserRepository userRepository,
        IPasswordHashService passwordHashService,
        ITokenService tokenService
    )
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDTO> Login(LoginDTO loginDTO)
    {
        var user = await _userRepository.GetByEmail(loginDTO.Email);
        bool isValidPassword = _passwordHashService.VerifyPassword(loginDTO.Password, user.Password);

        if (user == null || !isValidPassword)
            throw new UnauthorizedAccessException("Invalid email or password");

        var token = _tokenService.GenerateToken(user);

        return new LoginResponseDTO
        {
            Token = token,
            User = new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            }
        };
    }
}