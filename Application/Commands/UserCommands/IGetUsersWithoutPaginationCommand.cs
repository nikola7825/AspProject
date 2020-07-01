using Application.DTO;
using Application.Interfaces;
using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.UserCommands
{
    public interface IGetUsersWithoutPaginationCommand : ICommand<GeneralSearchQuery, IEnumerable<UserDto>>
    {
    }
}
