using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreAPI.Models;

namespace BookStoreAPI.BookData
{
    public interface IBookData
    {
        /* this class defines methods for getting Book data from the Database */
        List<Book> GetBooks();
        Book GetBook(int id);
        Book AddBook(Book book);
        void deleteBook(Book book);
        Book EditBook(Book book);

    }
}
