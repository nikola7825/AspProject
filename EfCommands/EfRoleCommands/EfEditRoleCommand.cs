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
    public class EfEditRoleCommand : EfBaseCommand, IEditRoleCommand
    {
        public EfEditRoleCommand(EfContext context) : base(context)
        {
        }

        public void Execute(RoleDto request)
        {
            var role = Context.Roles.Find(request.Id);

            if (role == null)
                throw new NotFoundException();

            if (request.Name != role.Name && Context.Roles.Any(r => r.Name == request.Name))
                throw new EntityAlreadyExistsException();

            role.Name = request.Name;

            Context.SaveChanges();
        }
    }
}
