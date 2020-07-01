using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string Summary { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int ImageId { get; set; }

        public Image Image { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
