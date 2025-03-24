using UserRegistrationBackend.Models;
using UserRegistrationBackend.DTOs.User;

namespace UserRegistrationBackend.Validators.Interfaces;

public interface IUserValidator
{
    Task ValidateEmail(string email);
    Task ValidateUser(int id);
    Task ValidateCreateUser(CreateUserDTO userDTO);
}