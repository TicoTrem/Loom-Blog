using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend.models;

namespace backend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        public BlogController() { }

        [HttpGet]
        public ActionResult<BlogPost> GetAll()
        {
            throw new NotImplementedException();
        }

        
    }
}