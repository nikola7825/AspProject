using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ShowTagDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsChecked { get; set; }

        public IEnumerable<ShowPostInTagDto> ShowPostInTagDto { get; set; }
    }
}
