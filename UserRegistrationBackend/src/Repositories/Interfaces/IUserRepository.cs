using UserRegistrationBackend.Models;

namespace UserRegistrationBackend.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> Create(User user);
    Task<List<User>> GetAll();
    Task<User?> GetById(int id);
    Task<bool> EmailExists(string email);
    Task<User> Update(User user);
    Task<bool> Delete(User user);
    Task<User> GetByEmail(string email);
}