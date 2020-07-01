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
    public class EfEditTagCommand : EfBaseCommand, IEditTagCommand
    {
        public EfEditTagCommand(EfContext context) : base(context)
        {
        }

        public void Execute(TagDto request)
        {
            var tag = Context.Tags.Find(request.Id);

            if (tag == null)
                throw new NotFoundException();

            if (request.Name != tag.Name && Context.Tags.Any(t => t.Name == request.Name))
                throw new EntityAlreadyExistsException();

            tag.Name = request.Name;

            Context.SaveChanges();
        }
    }
}
