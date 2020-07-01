using Application.Commands;
using Application.DTO;
using Application.Queries;
using Application.Responses;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetUsersCommand : EfBaseCommand, IGetUsersCommand
    {

        public EfGetUsersCommand(EfContext context) : base(context)
        {
        }

        public PageResponses<ShowUserDto> Execute(UserQuery request)
        {

            var users = Context.Users
                .Include(r => r.Role)
                .Include(p => p.Posts)
                .ThenInclude(pt => pt.PostTags)
                .AsQueryable();

            if (request.FirstName != null)
                users = users.Where(u => u.FirstName.ToLower().Contains(request.FirstName.ToLower()));

            if (request.LastName != null)
                users = users.Where(u => u.LastName.ToLower().Contains(request.LastName.ToLower()));

            if (request.Username != null)
                users = users.Where(u => u.Username.ToLower().Contains(request.Username.ToLower()));

            if (request.RoleName != null)
                users = users.Where(u => u.Role.Name.ToLower().Contains(request.RoleName.ToLower()));

            var sortOrder = request.SortOrder;

            var totalCount = users.Count();

            var Data = users.Select(u => new ShowUserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.Username,
                RoleName = u.Role.Name,
                showPostDtos = u.Posts.Select(p => new ShowPostDto
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
            });


            if (!string.IsNullOrEmpty(request.SearchString))
            {
                Data = Data.Where(u => u.FirstName.Contains(request.SearchString) || u.LastName.Contains(request.SearchString));
                totalCount = Data.Count();
            }

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            switch (sortOrder)
            {
                case "name_desc":
                    Data = Data.OrderByDescending(u => u.FirstName);
                    break;
                case "name_asc":
                    Data = Data.OrderBy(u => u.FirstName);
                    break;
                case "last_name_desc":
                    Data = Data.OrderByDescending(u => u.LastName);
                    break;
                case "last_name_asc":
                    Data = Data.OrderBy(u => u.LastName);
                    break;
                case "username_desc":
                    Data = Data.OrderByDescending(u => u.Username);
                    break;
                case "username_asc":
                    Data = Data.OrderBy(u => u.Username);
                    break;
                case "role_desc":
                    Data = Data.OrderByDescending(u => u.RoleName);
                    break;
                case "role_asc":
                    Data = Data.OrderBy(u => u.RoleName);
                    break;
                default:
                    Data = Data.OrderBy(u => u.FirstName);
                    break;
            }

            Data = Data.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);


            return new PageResponses<ShowUserDto>
            {
                CurrentPage = request.PageNumber,
                PagesCount = pagesCount,
                TotalCount = totalCount,
                Data = Data
            };
        }
    }
}
