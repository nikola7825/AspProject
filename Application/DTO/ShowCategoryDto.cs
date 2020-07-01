using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ShowCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<ShowPostInCategoryDto> ShowPostInCategoryDtos { get; set; }
    }
}
