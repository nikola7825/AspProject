using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EfCommands
{
    public class EfBaseCommand
    {
        protected EfContext Context { get;  }

        public EfBaseCommand(EfContext context) => Context = context;

    }
}
