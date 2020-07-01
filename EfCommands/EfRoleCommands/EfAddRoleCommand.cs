using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfAddRoleCommand : EfBaseCommand, IAddRoleCommand
    {
        public EfAddRoleCommand(EfContext context) : base(context)
        {
        }

        public void Execute(RoleDto request)
        {
            if (Context.Roles.Any(r => r.Name == request.Name))
                throw new EntityAlreadyExistsException();

            Context.Roles.Add(new Domain.Role
            {
                Name = request.Name
            });

            Context.SaveChanges();
        }
    }
}
