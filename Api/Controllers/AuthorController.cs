using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;


namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Author> Get()
        {
            return DataAccess.GetAllAuthors();
        }
        [HttpGet("{id}")]
        public ActionResult<Author> Get(int id)
        {
            var result = DataAccess.GetAuthor(id);
            if (result == null)
                return NotFound();

            return result;
        }
        [HttpPost]
        public IActionResult Create(Author author)
        {
            DataAccess.AddAuthor(author);
            return NoContent();
        }
    }
}