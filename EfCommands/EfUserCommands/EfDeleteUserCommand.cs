using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfDeleteUserCommand : EfBaseCommand, IDeleteUserCommand
    {
        public EfDeleteUserCommand(EfContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var user = Context.Users.Find(request);

            if (user == null)
                throw new NotFoundException();

            Context.Users.Remove(user);
            Context.SaveChanges();
        }
    }
}
