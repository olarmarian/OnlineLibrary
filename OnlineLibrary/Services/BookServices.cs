using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Services
{
    public class BookServices
    {
        private ApplicationDbContext db;
        public BookServices(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void CreateBook(string bookTitle, string bookAuthor, decimal bookPrice)
        {
            var book = new Book
            {
                BookCode = (0 + db.Books.Count()).ToString().PadLeft(10, '0'),
                Title = bookTitle,
                Author = bookAuthor,
                Price = bookPrice,
            };

        }

    }
}