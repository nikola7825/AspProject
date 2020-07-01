using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public class BaseQuery
    {
        public int PerPage { get; set; } = 3;

        public int PageNumber { get; set; } = 1;

        public string SortOrder { get; set; }

        public string SearchString { get; set; }
    }
}
