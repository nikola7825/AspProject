using Application.Commands;
using Application.Login;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfLoginUserCommand : EfBaseCommand, ILoginUserCommand
    {
        public EfLoginUserCommand(EfContext context) : base(context)
        {
        }

        public LoggedUser Execute(LoginUser request)
        {
            var user = Context.Users.Include(u => u.Role)
                .Where(u => u.Username == request.Username && u.Password == request.Password)
                .FirstOrDefault();

            if (user == null)
                throw new Exception("Invalid username or password");

            return new LoggedUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Role = user.Role.Name,
                Id = user.Id
            };
        }
    }
}
