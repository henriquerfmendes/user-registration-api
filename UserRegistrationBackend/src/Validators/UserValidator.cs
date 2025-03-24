using UserRegistrationBackend.Repositories.Interfaces;
using UserRegistrationBackend.Validators.Interfaces;
using UserRegistrationBackend.DTOs.User;

namespace UserRegistrationBackend.Validators;

public class UserValidator : IUserValidator
{
    private readonly IUserRepository _userRepository;

    public UserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ValidateCreateUser(CreateUserDTO userDTO)
    {
        if (string.IsNullOrWhiteSpace(userDTO.Name))
            throw new Exception("Name is required");

        if (string.IsNullOrWhiteSpace(userDTO.Password))
            throw new Exception("Password is required");

        if (string.IsNullOrWhiteSpace(userDTO.PhoneNumber))
            throw new Exception("Phone number is required");

        if (userDTO.Password.Length < 8)
            throw new Exception("Password must be at least 8 characters long");

        await ValidateEmail(userDTO.Email);
    }

    public async Task ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("Email is required");

        if (await _userRepository.EmailExists(email))
            throw new Exception("Email already registered");
    }

    public async Task ValidateUser(int id)
    {
        if (id <= 0)
            throw new Exception("User ID must be greater than 0");

        var user = await _userRepository.GetById(id);

        if (user == null)
            throw new KeyNotFoundException($"User with ID {id} not found");
    }
}