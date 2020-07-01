using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public class PostQuery : BaseQuery
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public string Text { get; set; }

    }
}
