using System;
using System.Collections.Generic;
using System.Text;
using EfDataAccess;
using Application.Commands.CategoryCommands;
using Application.DTO;
using Application.Queries;
using System.Linq;

namespace EfCommands.EfCategoryCommands
{
    public class EfGetCategoriesWithoutPaginationCommand : EfBaseCommand, IGetCategoriesWithoutPaginationCommand
    {
        public EfGetCategoriesWithoutPaginationCommand(EfContext context) : base(context)
        {
        }

        public IEnumerable<CategoryDto> Execute(GeneralSearchQuery request)
        {
            var query = Context.Categories.AsQueryable();

            return query.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }
    }
}
