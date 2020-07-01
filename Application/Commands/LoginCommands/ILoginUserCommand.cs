using Application.Interfaces;
using Application.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public interface ILoginUserCommand : ICommand<LoginUser, LoggedUser>
    {
    }
}
