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
    public class EfGetPostsCommand : EfBaseCommand, IGetPostsCommand
    {
        public EfGetPostsCommand(EfContext context) : base(context)
        {
        }

        public PageResponses<GetPostDto> Execute(PostQuery request)
        {
            var posts = Context.Posts
                .Include(u => u.User)
                .ThenInclude(r => r.Role)
                .Include(c => c.Category)
                .Include(pt => pt.PostTags)
                .ThenInclude(t => t.Tag)
                .Include(i => i.Image)
                .AsQueryable();

            if (request.Title != null)
                posts = posts.Where(p => p.Title.ToLower().Contains(request.Title.ToLower()));

            if (request.Text != null)
                posts = posts.Where(p => p.Text.ToLower().Contains(request.Text.ToLower()));

            if (request.Summary != null)
                posts = posts.Where(p => p.Summary.ToLower().Contains(request.Summary.ToLower()));

            var totalCount = posts.Count();

            var Data = posts.Select(p => new GetPostDto
            {
                Id = p.Id,
                Title = p.Title,
                Summary = p.Summary,
                Text = p.Text,
                FirstName = p.User.FirstName,
                LastName = p.User.LastName,
                Category = p.Category.Name,
                ImageId = p.Image.Id,
                Image = p.Image.Path,
                ShowTagInPosts = p.PostTags.Select(t => new ShowTagInPosts
                {
                    TagName = t.Tag.Name
                })
            });


            //Sorting logic
            var sortOrder = request.SortOrder;

            switch (sortOrder)
            {
                case "title_desc" :
                    Data = Data.OrderByDescending(p => p.Title);
                    break;
                case "author_desc":
                    Data = Data.OrderByDescending(p => p.FirstName);
                    break;
                case "author_asc":
                    Data = Data.OrderBy(p => p.FirstName);
                    break;
                case "category_desc":
                    Data = Data.OrderByDescending(c => c.Category);
                    break;
                case "category_asc":
                    Data = Data.OrderBy(p => p.Category);
                    break;
                default:
                    Data = Data.OrderBy(p => p.Title);
                    break;
            }

            //Filtering logic - MVC
            if (!string.IsNullOrEmpty(request.SearchString))
            {
                Data = Data.Where(p => p.Title.Contains(request.SearchString) ||
                p.FirstName.Contains(request.SearchString) ||
                p.LastName.Contains(request.SearchString) ||
                p.Category.Contains(request.SearchString) ||
                p.ShowTagInPosts.Any(t => t.TagName.Contains(request.SearchString)));

                totalCount = Data.Count();
            }

            Data = Data.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);


            return new PageResponses<GetPostDto>
            {
                CurrentPage = request.PageNumber,
                PagesCount = pagesCount,
                TotalCount = totalCount,
                Data = Data
            };
        }
    }
}
