using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary.Controllers
{
    [Authorize]
    public class BookingAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: BookingAccount
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListBorrowedBooks(int? BookingAccountId)
        {
            if(BookingAccountId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.BookItems.Where(ba => ba.BookingAccountId == BookingAccountId));
        }
    }
}
