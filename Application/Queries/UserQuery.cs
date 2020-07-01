using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public class UserQuery : BaseQuery
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string RoleName { get; set; }


    }
}
