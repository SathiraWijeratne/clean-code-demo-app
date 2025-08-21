using System;
using Application.Dtos;
using Application.Mappers;
using Domain.Repositories;

namespace Application.Services;

/// <summary>
/// User service implementation for managing user-related operations.
/// </summary>
public class UserService(IUserRepository userRepository, IUserMapper userMapper) : IUserService
{
    public async Task CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = userMapper.MapToEntity(createUserDto);
        await userRepository.AddUserAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await userRepository.DeleteUserAsync(id);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllUsersAsync();
        return users.Select(user => userMapper.MapToDto(user)).ToList();
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        return user == null ? null : userMapper.MapToDto(user);
    }

    public async Task UpdateUserAsync(UpdateUserDto updateUserDto)
    {
        var user = userMapper.MapToEntity(updateUserDto);
        await userRepository.UpdateUserAsync(user);
    }
}
