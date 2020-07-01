using Application.DTO;
using Application.Interfaces;
using Application.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.CategoryCommands
{
    public interface IGetCategoriesWithoutPaginationCommand : ICommand<GeneralSearchQuery, IEnumerable<CategoryDto>>
    {
    }
}
