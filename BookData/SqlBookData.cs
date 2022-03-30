using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreAPI.Models;

namespace BookStoreAPI.BookData
{
    /* this implements the interface and access data from the Database */
    public class SqlBookData : IBookData
    {
        private BookContext _bookContext;
        List<Book> books = new List<Book>();

        public SqlBookData( BookContext bookContext )
        {
            _bookContext = bookContext;
        }
        public Book AddBook(Book book)
        {
            _bookContext.Books.Add(book);
            _bookContext.SaveChanges();
            // books.Add(book);//throw new NotImplementedException();
            return book;
        }

        public void deleteBook(Book book)
        {
            _bookContext.Books.Remove(book);
            _bookContext.SaveChanges();
        }

        public Book EditBook(Book book)
        {
            var existingbook = GetBook(book.Id);
            if (existingbook != null)
            {
                ((BookStoreAPI.Models.Book)existingbook).title = book.title;
                ((BookStoreAPI.Models.Book)existingbook).description = book.description;
                ((BookStoreAPI.Models.Book)existingbook).author = book.author;
                ((BookStoreAPI.Models.Book)existingbook).title = book.title;
                ((BookStoreAPI.Models.Book)existingbook).cover_image = book.cover_image;
                ((BookStoreAPI.Models.Book)existingbook).price = book.price;
                _bookContext.Books.Update(existingbook );
                _bookContext.SaveChanges();
            }

            return (BookStoreAPI.Models.Book)(existingbook);
        }

        public Book GetBook(int id)
        {
            var book = _bookContext.Books.Find(id);
            return book;
        }

        public List<Book> GetBooks()
        {
            return _bookContext.Books.ToList() ;
        }
    }
}
