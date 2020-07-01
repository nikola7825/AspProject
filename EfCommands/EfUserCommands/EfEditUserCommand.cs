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
    public class EfEditUserCommand : EfBaseCommand, IEditUserCommand
    {
        public EfEditUserCommand(EfContext context) : base(context)
        {
        }

        public void Execute(ShowUserDto request)
        {
            var user = Context.Users.Find(request.Id);

            if (user == null)
                throw new NotFoundException();

            if (request.Username != user.Username && Context.Users.Any(u => u.Username == request.Username))
                throw new EntityAlreadyExistsException();

            //if (request.Password != null)
            //    user.Password = request.Password;

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Username = request.Username;
            // user.Password = request.Password;
            user.RoleId = request.RoleId;

            Context.SaveChanges();
        }

      
    }
}
