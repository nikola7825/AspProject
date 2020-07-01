using Application.DTO;
using Application.Interfaces;
using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.TagCommands
{
    public interface IGetTagsWithoutPaginationCommand : ICommand<GeneralSearchQuery, IEnumerable<ShowTagDto>>
    {
    }
}
