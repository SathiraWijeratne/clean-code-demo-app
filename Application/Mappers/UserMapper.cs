using System;
using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

/// <summary>
/// User mapper implementation for mapping between user entities and DTOs.
/// </summary>
public class UserMapper : IUserMapper
{
    /// <summary>
    /// Maps a user entity to a user DTO.
    /// </summary>
    public UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name
        };
    }

    /// <summary>
    /// Maps a create user DTO to a user entity.
    /// </summary>
    public User MapToEntity(CreateUserDto createUserDto)
    {
        return new User
        {
            Name = createUserDto.Name
        };
    }

    /// <summary>
    /// Maps an update user DTO to a user entity.
    /// </summary>
    public User MapToEntity(UpdateUserDto updateUserDto)
    {
        return new User
        {
            Id = updateUserDto.Id,
            Name = updateUserDto.Name
        };
    }
}
