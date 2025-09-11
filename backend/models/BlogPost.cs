using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace backend.models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public Author Author { get; set; } = null!;
        public int Content { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? LastUpdatedDate { get; set; }
        

    }


}