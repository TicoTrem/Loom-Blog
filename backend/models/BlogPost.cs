using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace backend.models
{
    public class BlogPost : IModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public string Content { get; set; } = "";

        public DateTime? CreatedDateUtc { get; set; }
        public DateTime? LastUpdatedDateUtc { get; set; }
        
    }


}