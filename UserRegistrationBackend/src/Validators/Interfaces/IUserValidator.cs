using UserRegistrationBackend.Models;

namespace UserRegistrationBackend.Validators.Interfaces;

public interface IUserValidator
{
    Task ValidateEmail(string email);
    Task ValidateUser(int id);
}