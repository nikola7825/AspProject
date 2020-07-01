using Application.Commands;
using Application.DTO;
using Application.Interfaces;
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
    public class EfGetTagsCommand : EfBaseCommand, IGetTagsCommand
    {
        public EfGetTagsCommand(EfContext context) : base(context)
        {
        }

        public IEnumerable<ShowTagDto> Execute(TagQuery request)
        {
            var tags = Context.Tags
                .Include(pt => pt.PostTags)
                .ThenInclude(p => p.Post)
                .ThenInclude(u => u.User)
                .ThenInclude(r => r.Role)
                .Include(pt => pt.PostTags)
                .ThenInclude(p => p.Post)
                .ThenInclude(c => c.Category)
                .AsQueryable();

            if (request.Name != null)
                tags = tags.Where(t => t.Name.ToLower().Contains(request.Name.ToLower()));


            var dto = tags.Select(t => new ShowTagDto
            {
                Id = t.Id,
                Name = t.Name,
                ShowPostInTagDto = t.PostTags.Select(pt => new ShowPostInTagDto
                {
                    Title = pt.Post.Title,
                    Summary = pt.Post.Summary,
                    Text = pt.Post.Text,
                    CategoryName = pt.Post.Category.Name,
                    FirstName = pt.Post.User.FirstName,
                    LastName = pt.Post.User.LastName,
                    RoleName = pt.Post.User.Role.Name
                })
            });

            return dto;

        }

    }
}
