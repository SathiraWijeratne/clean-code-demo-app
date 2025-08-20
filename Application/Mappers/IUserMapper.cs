using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers
{
    /// <summary>
    /// User mapper interface for mapping between user entities and DTOs.
    /// </summary>
    public interface IUserMapper
    {
        UserDto MapToDto(User user);
        User MapToEntity(CreateUserDto createUserDto);
        User MapToEntity(UpdateUserDto updateUserDto);
    }
}