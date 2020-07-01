using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;

namespace EfCommands
{
    public class EfGetTagCommand : EfBaseCommand, IGetTagCommand
    {
        public EfGetTagCommand(EfContext context) : base(context)
        {
        }

        public ShowTagDto Execute(int request)
        {
            var tag = Context.Tags
                .Include(pt => pt.PostTags)
                .ThenInclude(p => p.Post)
                .ThenInclude(c => c.Category)
                .Include(pt => pt.PostTags)
                .ThenInclude(p => p.Post)
                .ThenInclude(u => u.User)
                .ThenInclude(r => r.Role)
                .Where(t => t.Id == request)
                .FirstOrDefault();

            if (tag == null)
                throw new NotFoundException();

            return new ShowTagDto
            {
                Name = tag.Name,
                ShowPostInTagDto = tag.PostTags.Select(pt => new ShowPostInTagDto
                {
                    Title = pt.Post.Title,
                    Summary = pt.Post.Summary,
                    Text = pt.Post.Text,
                    CategoryName = pt.Post.Category.Name,
                    FirstName = pt.Post.User.FirstName,
                    LastName = pt.Post.User.LastName,
                    RoleName = pt.Post.User.Role.Name
                })
            };
        }
    }
}
