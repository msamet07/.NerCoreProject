using System;
using System.Collections.Generic;

namespace Workintech02RestApiDemo.Domain.Entities
{
    public class Post:BaseEntity
    {
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;
        public string Description { get; set; } = null!;

        public int BlogId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual Blog Blog { get; set; }
    }
}


