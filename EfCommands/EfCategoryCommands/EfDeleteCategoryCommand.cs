using Application.Commands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfDeleteCategoryCommand : EfBaseCommand, IDeleteCategoryCommand
    {
        public EfDeleteCategoryCommand(EfContext context) : base(context)
        {
        }

        public void Execute(int request)
        {
            var category = Context.Categories.Find(request);

            if (category == null)
                throw new NotFoundException();

            Context.Categories.Remove(category);

            Context.SaveChanges();
        }
    }
}
