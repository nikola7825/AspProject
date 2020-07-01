using Application.Commands;
using Application.DTO;
using Application.Queries;
using Application.Responses;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetCategoriesCommand : EfBaseCommand, IGetCategoriesCommand
    {
        public EfGetCategoriesCommand(EfContext context) : base(context)
        {
        }

        public PageResponses<ShowCategoryDto> Execute(CategoryQuery request)
        {
            var query = Context.Categories
                .Include(p => p.Posts)
                .ThenInclude(pt => pt.PostTags)
                .ThenInclude(t => t.Tag)
                .Include(p => p.Posts)
                .ThenInclude(u => u.User)
                .ThenInclude(r => r.Role)
                .AsQueryable();

            if (request.Name != null)
                query = query.Where(c => c.Name.ToLower().Contains(request.Name.ToLower()));

            var SortOrder = request.SortOrder;

            var totalCount = query.Count();

            var Data = query.Select(c => new ShowCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                ShowPostInCategoryDtos = c.Posts.Select(p => new ShowPostInCategoryDto
                {
                    Title = p.Title,
                    Summary = p.Summary,
                    Text = p.Text,
                    FirstName = p.User.FirstName,
                    LastName = p.User.LastName,
                    showTagDtos = p.PostTags.Select(pt => new ShowTagDto
                    {
                        Name = pt.Tag.Name
                    })
                })
            });

            //MVC filtering logic
            if (!string.IsNullOrEmpty(request.SearchString))
            {
                Data = Data.Where(c => c.Name.Contains(request.SearchString));
                totalCount = Data.Count();
            }

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            //Sorting logic
            switch (SortOrder)
            {
                case "category_desc":
                    Data = Data.OrderByDescending(c => c.Name);
                    break;
                default:
                    Data = Data.OrderBy(c => c.Name);
                    break;
            }

            Data = Data.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);


            return new PageResponses<ShowCategoryDto>
            {
                CurrentPage = request.PageNumber,
                PagesCount = pagesCount,
                TotalCount = totalCount,
                Data = Data
            };
        }

        
    }
}
