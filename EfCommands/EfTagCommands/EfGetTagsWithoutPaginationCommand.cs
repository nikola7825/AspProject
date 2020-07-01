using Application.Commands.TagCommands;
using Application.DTO;
using Application.Queries;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.EfTagCommands
{
    public class EfGetTagsWithoutPaginationCommand : EfBaseCommand, IGetTagsWithoutPaginationCommand
    {
        public EfGetTagsWithoutPaginationCommand(EfContext context) : base(context)
        {
        }

        public IEnumerable<ShowTagDto> Execute(GeneralSearchQuery request)
        {
            var query = Context.Tags.AsQueryable();

            return query.Select(t => new ShowTagDto
            {
                Id = t.Id,
                Name = t.Name
            });
        }
    }
}
