using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfDeleteTagCommand : EfBaseCommand, IDeleteTagCommand
    {
        public EfDeleteTagCommand(EfContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var tag = Context.Tags.Find(request);

            if (tag == null)
                throw new NotFoundException();

            Context.Tags.Remove(tag);

            Context.SaveChanges();
        }
    }
}
