using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfAddTagCommand : EfBaseCommand, IAddTagCommand
    {
        public EfAddTagCommand(EfContext context) : base(context)
        {
        }

        public void Execute(TagDto request)
        {
            if (Context.Tags.Any(t => t.Name == request.Name))
                throw new EntityAlreadyExistsException();

            Context.Tags.Add(new Domain.Tag
            {
                Name = request.Name
            });

            Context.SaveChanges();
        }
    }
}
