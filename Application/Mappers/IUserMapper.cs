using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers
{
    public interface IUserMapper
    {
        UserDto MapToDto(User user);
        User MapToEntity(CreateUserDto createUserDto);
        User MapToEntity(UpdateUserDto updateUserDto);
    }
}