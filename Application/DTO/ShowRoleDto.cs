using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ShowRoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<BasicUserInfoDto> BasicUserInfoDtos { get; set; }
    }
}
