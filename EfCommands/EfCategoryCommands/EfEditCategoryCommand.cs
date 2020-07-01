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
    public class EfEditCategoryCommand : EfBaseCommand, IEditCategoryCommand
    {
        public EfEditCategoryCommand(EfContext context) : base(context)
        {
        }

        public void Execute(CategoryDto request)
        {
            var category = Context.Categories.Find(request.Id);

            if (category == null)
                throw new NotFoundException();

            if (request.Name != category.Name && Context.Categories.Any(c => c.Name == request.Name))
                throw new EntityAlreadyExistsException();

            category.Name = request.Name;

            Context.SaveChanges();
        }
    }
}
