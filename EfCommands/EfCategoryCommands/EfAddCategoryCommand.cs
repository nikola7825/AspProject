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
    public class EfAddCategoryCommand : EfBaseCommand, IAddCategoryCommand
    {
        public EfAddCategoryCommand(EfContext context) : base(context)
        {
        }

        public void Execute(CategoryDto request)
        {
            if (Context.Categories.Any(c => c.Name == request.Name))
                throw new EntityAlreadyExistsException();

            Context.Categories.Add(new Domain.Category
            {
                 Name = request.Name
            });

            Context.SaveChanges();
        }
    }
}
