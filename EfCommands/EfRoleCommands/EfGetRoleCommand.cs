using Application.Commands;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using EfDataAccess;
using Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EfCommands
{
    public class EfGetRoleCommand : EfBaseCommand, IGetRoleCommand
    {
        public EfGetRoleCommand(EfContext context) : base(context)
        {
        }

        public ShowRoleDto Execute(int request)
        {
            var role = Context.Roles
                .Include(u => u.Users)
                .Where(r => r.Id == request)
                .FirstOrDefault();

            if (role == null)
                throw new NotFoundException();

            return new ShowRoleDto
            {
                Id = role.Id,
                Name = role.Name,
                BasicUserInfoDtos = role.Users.Select(u => new BasicUserInfoDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.Username
                })
            };

        }
    }
}
