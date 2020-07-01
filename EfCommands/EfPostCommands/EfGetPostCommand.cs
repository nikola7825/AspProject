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
    public class EfGetPostCommand : EfBaseCommand, IGetPostCommand
    {
        public EfGetPostCommand(EfContext context) : base(context)
        {
        }

        public GetPostDto Execute(int request)
        {
            var post = Context.Posts
                .Include(u => u.User)
                .ThenInclude(r => r.Role)
                .Include(c => c.Category)
                .Include(i => i.Image)
                .Include(pt => pt.PostTags)
                .ThenInclude(t => t.Tag)
                .Where(p => p.Id == request)
                .FirstOrDefault();
                

            if (post == null)
                throw new NotFoundException();

            var dto = new GetPostDto
            {
                Id = post.Id,
                Title = post.Title,
                Summary = post.Summary,
                Text = post.Text,
                CategoryId = post.CategoryId,
                Category = post.Category.Name,
                UserId = post.UserId,
                FirstName = post.User.FirstName,
                LastName = post.User.LastName,
                ImageId = post.ImageId,
                Image = post.Image.Path,
                ShowTagInPosts = post.PostTags.Select(t => new ShowTagInPosts
                {
                    TagName = t.Tag.Name
                })
            };

            return dto;
        }
    }
}
