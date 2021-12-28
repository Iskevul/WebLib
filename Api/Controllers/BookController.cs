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
    public class BookController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return DataAccess.GetAllBooks();
        }
        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var result = DataAccess.GetBook(id);
            if (result == null)
                return NotFound();

            return result;
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            DataAccess.AddBook(book);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book book)
        {
            var result = DataAccess.GetBook(id);
            if (result == null)
                return NotFound();

            DataAccess.UpdateBook(id, book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = DataAccess.GetBook(id);
            if (result == null)
                return NotFound();

            DataAccess.DeleteBook(id);
            return NoContent();
        }
    }
}
