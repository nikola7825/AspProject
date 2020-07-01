using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands.RoleCommands;
using Application.DTO;
using Application.Queries;
using EfDataAccess;

namespace EfCommands.EfRoleCommands
{
    public class EfGetRolesWithoutPaginationCommand : EfBaseCommand, IGetRolesWithoutPaginationCommand
    {
        public EfGetRolesWithoutPaginationCommand(EfContext context) : base(context)
        {
        }

        public IEnumerable<ShowRoleDto> Execute(GeneralSearchQuery request)
        {
            var query = Context.Roles.AsQueryable();

            return query.Select(r => new ShowRoleDto
            {
                Id = r.Id,
                Name = r.Name
            });
        }
    }
}
