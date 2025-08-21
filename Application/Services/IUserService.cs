using System;
using Application.Dtos;

namespace Application.Services;

/// <summary>
/// User service interface for managing user-related operations.
/// </summary>
public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(int id);
    Task CreateUserAsync(CreateUserDto createUserDto);
    Task UpdateUserAsync(UpdateUserDto updateUserDto);
    Task DeleteUserAsync(int id);
}
