using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.models
{
    public class Author : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
    
}