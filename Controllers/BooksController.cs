using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStoreAPI.BookData;
using BookStoreAPI.Models;

namespace BookStoreAPI.Controllers
{
    /* this allows the read of the API data from the DB */
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookData _bookData;
        public BooksController(IBookData bookData)
        {
            _bookData = bookData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetBooks()
        {
            return Ok(_bookData.GetBooks());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetBook(int id)
        {
            var abook = _bookData.GetBook(id);

            if(abook != null)
            {
                return Ok(abook);
            }
            return NotFound($"Book with Id: {id}");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult GetBook(Book book)
        {
            _bookData.AddBook(book);

            return Created(HttpContext.Request.Scheme + "//" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + book.Id, book);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookData.GetBook(id);
            if ( book != null)
            {
                _bookData.deleteBook(book);
                return Ok();
            }

            return NotFound($"Employee with id: {id} was not found" );
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult EditBook(int id, Book book)
        {
            var existingbook = _bookData.GetBook(id);
            if (existingbook != null)
            {
                book.Id = existingbook.Id;
                _bookData.EditBook(book);
                
            }

            return Ok(book);
        }
    }
}