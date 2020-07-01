using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetUserCommand : EfBaseCommand, IGetUserCommand
    {
        public EfGetUserCommand(EfContext context) : base(context)
        {
        }

        public ShowUserDto Execute(int request)
        {
            var user = Context.Users
                .Include(p => p.Posts)
                .ThenInclude(pt => pt.PostTags)
                .ThenInclude(t => t.Tag)
                .Include(r => r.Role)
                .Include(p => p.Posts)
                .ThenInclude(c => c.Category)
                .Where(u => u.Id == request)
                .FirstOrDefault();

            if (user == null)
                throw new NotFoundException();

            var dto = new ShowUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                RoleName = user.Role.Name,
                showPostDtos = user.Posts.Select(p => new ShowPostDto
                {
                    Title = p.Title,
                    Summary = p.Summary,
                    Text = p.Text,
                    Category = p.Category.Name,
                    showTagDtos = p.PostTags.Select(t => new ShowTagDto
                    {
                        Name = t.Tag.Name
                    })
                })
            };

            return dto;
        }
    }
}
