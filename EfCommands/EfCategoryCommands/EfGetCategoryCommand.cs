using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetCategoryCommand : EfBaseCommand, IGetCategoryCommand
    {
        public EfGetCategoryCommand(EfContext context) : base(context)
        {
        }

        public ShowCategoryDto Execute(int request)
        {
            var category = Context.Categories
                .Include(p => p.Posts)
                .ThenInclude(pt => pt.PostTags)
                .ThenInclude(t => t.Tag)
                .Include(p => p.Posts)
                .ThenInclude(u => u.User)
                .ThenInclude(r => r.Role)
                .Where(c => c.Id == request)
                .FirstOrDefault();

            if (category == null)
                throw new NotFoundException();

            return new ShowCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                ShowPostInCategoryDtos = category.Posts.Select(p => new ShowPostInCategoryDto
                {
                    Title = p.Title,
                    Summary = p.Summary,
                    FirstName = p.User.FirstName,
                    LastName = p.User.LastName,
                    Text = p.Text,
                    showTagDtos = p.PostTags.Select(pt => new ShowTagDto
                    {
                        Name = pt.Tag.Name
                    })
                })
            };
        }
    }
}
