using System;
using System.Collections.Generic;

namespace Workintech02RestApiDemo.Domain.Entities
{
    public class Blog:BaseEntity
    {
        public string Url { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}


