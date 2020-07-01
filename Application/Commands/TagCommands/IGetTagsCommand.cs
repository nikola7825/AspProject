using Application.DTO;
using Application.Interfaces;
using Application.Queries;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public interface IGetTagsCommand : ICommand<TagQuery, IEnumerable<ShowTagDto>>
    {
    }
}
