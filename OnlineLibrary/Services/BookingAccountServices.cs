using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Services
{
    public class BookingAccountServices
    {
        private ApplicationDbContext db;

        public BookingAccountServices(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void CreateBookingAccount(string firstName, string lastName, string userId)
        {
            var accountNumber = (0 + db.BookingAccounts.Count()).ToString().PadLeft(10, '0');
            var bookingAccount = new BookingAccount
            {
                FirstName = firstName,
                LastName = lastName,
                AccountNumber = accountNumber,
                ApplicationUserId = userId
            };
            db.BookingAccounts.Add(bookingAccount);
            db.SaveChanges();
        }
    }
}