using System;
using Domain.Entities;

namespace Domain.Repositories;
/// <summary>
/// User repository interface for managing user data.
/// </summary>
public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
}
