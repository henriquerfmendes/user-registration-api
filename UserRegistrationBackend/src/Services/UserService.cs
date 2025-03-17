using UserRegistrationBackend.Data;
using UserRegistrationBackend.Models;
using UserRegistrationBackend.DTOs.User;
using Microsoft.EntityFrameworkCore;
using UserRegistrationBackend.Repositories.Interfaces;
using UserRegistrationBackend.Validators.Interfaces;
using UserRegistrationBackend.Services.Interfaces;

namespace UserRegistrationBackend.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserValidator _userValidator;
    private readonly IPasswordHashService _passwordHashService;
    public UserService(IUserRepository userRepository, IUserValidator userValidator, IPasswordHashService passwordHashService)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _passwordHashService = passwordHashService;
    }

    private UserResponseDTO UserResponse(User user)
    {
        return new UserResponseDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    public async Task<UserResponseDTO> Create(CreateUserDTO userDTO)
    {
        await _userValidator.ValidateEmail(userDTO.Email);

        var user = new User
        {
            Name = userDTO.Name,
            Email = userDTO.Email,
            Password = _passwordHashService.HashPassword(userDTO.Password),
            PhoneNumber = userDTO.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _userRepository.Create(user);

        return UserResponse(user);
    }

    public async Task<List<UserResponseDTO>> GetAll()
    {
        var users = await _userRepository.GetAll();
        return users.Select(UserResponse).ToList();
    }

    public async Task<UserResponseDTO> GetById(int id)
    {
        await _userValidator.ValidateUser(id);
        var user = await _userRepository.GetById(id);

        return UserResponse(user);
    }

    public async Task<UserResponseDTO> Update(int id, UpdateUserDTO userDTO)
    {
        await _userValidator.ValidateUser(id);

        var user = await _userRepository.GetById(id);

        if (userDTO.Name != null)
        {
            user.Name = userDTO.Name;
        }

        if (userDTO.Email != null)
        {
            await _userValidator.ValidateEmail(userDTO.Email);
            user.Email = userDTO.Email;
        }

        if (userDTO.Password != null)
        {
            user.Password = _passwordHashService.HashPassword(userDTO.Password);
        }

        if (userDTO.PhoneNumber != null)
        {
            user.PhoneNumber = userDTO.PhoneNumber;
        }

        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.Update(user);

        return UserResponse(user);
    }

    public async Task<bool> Delete(int id)
    {
        await _userValidator.ValidateUser(id);
        var user = await _userRepository.GetById(id);

        return await _userRepository.Delete(user);
    }
}
