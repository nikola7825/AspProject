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
    public class EfGetRolesCommand : EfBaseCommand, IGetRolesCommand
    {
        public EfGetRolesCommand(EfContext context) : base(context)
        {
        }

        public PageResponses<ShowRoleDto> Execute(RoleQuery request)
        {
            var query = Context.Roles
                .Include(u => u.Users)
                .AsQueryable();

            if (request.Name != null)
                query = query.Where(r => r.Name.ToLower().Contains(request.Name.ToLower()));

            var totalCount = query.Count();

            
            var Data = query.Select(r => new ShowRoleDto
            {
                Id = r.Id,
                Name = r.Name,
                BasicUserInfoDtos = r.Users.Select(u => new BasicUserInfoDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.Username
                })
            });


            //Sorting logic
            var sortOrder = request.SortOrder;

            switch (sortOrder)
            {
                case "role_desc":
                    Data = Data.OrderByDescending(r => r.Name);
                    break;
                default:
                    Data = Data.OrderBy(r => r.Name);
                    break;
            }

            //Filtering logig
            if (!string.IsNullOrEmpty(request.SearchString))
            {
                Data = Data.Where(r => r.Name.Contains(request.SearchString));
                totalCount = Data.Count();
            }


            Data = Data.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);


            return new PageResponses<ShowRoleDto>
            {
                CurrentPage = request.PageNumber,
                PagesCount = pagesCount,
                TotalCount = totalCount,
                Data = Data
            };
        }
    }
}
