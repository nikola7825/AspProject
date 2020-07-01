using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.UserCommands;
using Application.DTO;
using Application.Queries;
using EfDataAccess;

namespace EfCommands.EfUserCommands
{
    public class EfGetUsersWithoutPaginationCommand : EfBaseCommand, IGetUsersWithoutPaginationCommand
    {
        public EfGetUsersWithoutPaginationCommand(EfContext context) : base(context)
        {
        }

        public IEnumerable<UserDto> Execute(GeneralSearchQuery request)
        {
            var query = Context.Users.AsQueryable();

            return query.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName
            });
        }
    }
}
