using ProductManagement.Features.Data.Models;
using ProductManagement.Features.Repositories.Interfaces;
using ProductManagement.Features.Services.Interfaces;

namespace ProductManagement.Features.Services.Implementations;

public class UserService(IUserRepository repo) : IUserService
{
    private readonly IUserRepository _repo = repo;

    public Task<List<User>> GetUsersAsync()
        => _repo.GetAllAsync();

    public Task<User?> GetUserByIdAsync(int id)
        => _repo.GetByIdAsync(id);

    public Task<User?> GetUserByUsernameAsync(string username)
        => _repo.GetByUsernameAsync(username);

    public Task CreateUserAsync(User user)
        => _repo.AddAsync(user);

    public Task UpdateUserAsync(User user)
        => _repo.UpdateAsync(user);

    public Task DeleteUserAsync(int id)
        => _repo.DeleteAsync(id);
}


