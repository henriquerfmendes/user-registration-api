using UserRegistrationBackend.Repositories.Interfaces;
using UserRegistrationBackend.Validators.Interfaces;

namespace UserRegistrationBackend.Validators;

public class UserValidator : IUserValidator
{
    private readonly IUserRepository _userRepository;

    public UserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ValidateEmail(string email)
    {
        if (await _userRepository.EmailExists(email))
            throw new Exception("Email already registered");
    }

    public async Task ValidateUser(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {id} not found");
    }
}