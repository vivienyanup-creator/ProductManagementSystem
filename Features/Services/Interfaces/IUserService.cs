using ProductManagement.Features.Data.Models;

namespace ProductManagement.Features.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
}