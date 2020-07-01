using Application.DTO;
using Application.Interfaces;
using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.RoleCommands
{
    public interface IGetRolesWithoutPaginationCommand : ICommand<GeneralSearchQuery, IEnumerable<ShowRoleDto>>
    {
    }
}
