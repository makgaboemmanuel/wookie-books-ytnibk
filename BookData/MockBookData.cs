using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreAPI.Models;

namespace BookStoreAPI.BookData
{
    public class MockBookData: IBookData
    {

        List<Book> books = new List<Book>()
        {
            new Book()
            {
                Id = 1,
                title = "Mr",
                author = "Makgabo Emmanuel",
                description = "The Book Of Life",
                cover_image = "",
                price = 5896.2
            },
             new Book()
            {
                Id = 2,
                title = "Mr",
                author = "Makgabo Mathekga",
                description = "The American Guy",
                cover_image = "",
                price = 86336.2
            },
            new Book()
            {
                Id = 3,
                title = "Miss",
                author = "Kyla McKenzie",
                description = "The Lion King",
                cover_image = "",
                price = 7849.6
            },
             new Book()
            {
                Id = 4,
                title = "Mrs",
                author = "Michael Martins",
                description = "Compton's Finest",
                cover_image = "",
                price = 96.2
            }


        };

        List<Book> IBookData.GetBooks()
        {
            return books; ;
        }

        Book IBookData.GetBook(int id)
        {
            return books.SingleOrDefault(x => x.Id == id);
        }

        Book IBookData.AddBook(Book book)
        {
            Random rand = new Random();
            // book.Id = rand.Next() + 9;
            books.Add(book) ;//throw new NotImplementedException();
            return book;
        }

        Book IBookData.EditBook(Book book)
        {
            var existingbook = GetBook(book.Id);
            ((BookStoreAPI.Models.Book)existingbook).title = book.title;
            ((BookStoreAPI.Models.Book)existingbook).description = book.description;
            ((BookStoreAPI.Models.Book)existingbook).author = book.author;
            ((BookStoreAPI.Models.Book)existingbook).title = book.title;
            ((BookStoreAPI.Models.Book)existingbook).cover_image = book.cover_image;
            ((BookStoreAPI.Models.Book)existingbook).price = book.price;

            return (BookStoreAPI.Models.Book)( existingbook);
            
        }

        private object GetBook(int id)
        {
            return books.SingleOrDefault(x => x.Id == id); // throw new NotImplementedException();
        }

        void IBookData.deleteBook(Book book)
        {
            books.Remove(book); // throw new NotImplementedException();
        }

 
    }
}
