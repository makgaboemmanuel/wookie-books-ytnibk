using BookStoreAPI.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI.Models
{
    public class BookContext: DbContext
    {
        /* this is a DB Connection Class */
        public BookContext(DbContextOptions<BookContext> options): base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
