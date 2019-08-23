using Microsoft.AspNet.Identity;
using Microsoft.Owin.Diagnostics.Views;
using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="admin")]       
        public ActionResult AddBook()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            byte[] Image;
            if (Request.Files["bookCover"] != null)
            {
                using (var binaryReader = new BinaryReader(Request.Files["bookCover"].InputStream))
                {
                    Image = binaryReader.ReadBytes(Request.Files["bookCover"].ContentLength);
                }
                book.File = Image;
            }
            book.BookCode = (0 + db.Books.Count()).ToString().PadLeft(10, '0');
            db.Books.Add(book);
            db.SaveChanges();
            return View("~/Views/Home/Index.cshtml");
        }

        [Authorize(Roles = "admin")]
        public ActionResult UpdateBook(int? IdBook)
        {
            if(IdBook == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = db.Books.Find(IdBook);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBook([Bind(Include ="Id,BookCode,Title,Author,Price,NoPieces,PublishingHouse,File")] Book book)
        {
            if (Request.Files["bookCover"].FileName != "")
            {
                byte[] Image;
                using (var binaryReader = new BinaryReader(Request.Files["bookCover"].InputStream))
                {
                    Image = binaryReader.ReadBytes(Request.Files["bookCover"].ContentLength);
                }
                book.File = Image;
            }
            
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListBooks");
            }
            return View(book);
        }

        //GET: Book/RemoveBook/{ID}
        [Authorize(Roles = "admin")]
        public ActionResult RemoveBook(int? IdBook)
        {
            if(IdBook == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = db.Books.Find(IdBook);
            if(book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //POST: Book/RemoveBook/{ID}
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("RemoveBook")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveBookConfirmed(int IdBook)
        {
            if (ModelState.IsValid)
            {
                var book = db.Books.Find(IdBook);
                db.Books.Remove(book);
                db.SaveChanges();
                return RedirectToAction("ListBooks","Book");
            }
            return View();
        }

        //GET: Book/DetailsBook/{ID}
        public ActionResult DetailsBook(int? IdBook)
        {
            if(IdBook == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = db.Books.Find(IdBook);
            if(book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult ListBooks()
        { 
            return View(db.Books.ToList());
        }

        [Authorize]
        public ActionResult BorrowBook(int? IdBook)
        {
            if(IdBook == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = db.Books.Find(IdBook);
            if (book == null)
            {
                return HttpNotFound();
            }
            var userId = User.Identity.GetUserId();
            var bookItem = new BookItem
            {
                BookId = book.Id,
                Book = book,
                BookingAccountId = db.BookingAccounts.Where(ba => ba.ApplicationUserId == userId).First().Id,
                BookingAccount = db.BookingAccounts.Where(ba => ba.ApplicationUserId == userId).First(),
                DateOfReceip = DateTime.Now.Date,
                DateOfReturn = DateTime.Now.Date,
            };
            return View(bookItem);
        }

        [Authorize]
        [HttpPost, ActionName("BorrowBook")]
        public ActionResult BorrowBookConfirmed(BookItem item)
        {
            if(item == null)
            {
                return RedirectToAction("BorrowBook","Book");
            }
            var book = db.Books.Find(item.BookId);
            if(book.NoPieces - 1 >= 0)
            {
                book.NoPieces = book.NoPieces - 1;
                db.BookItems.Add(item);
                db.SaveChanges();
                return RedirectToAction("ListBooks");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        [HttpGet]
        public JsonResult GetBooks(string filterTitle)
        {
            return Json(new { success = true, books = List(filterTitle)},JsonRequestBehavior.AllowGet);   
        }

        public List<Book> List(string filter)
        {
            var list = new List<Book>();
            foreach(var item in db.Books)
            {
                var b = new Book {
                    Id = item.Id,
                    Title = item.Title,
                    Author = item.Author,
                    NoPieces = item.NoPieces,
                    Price = item.Price,
                    PublishingHouse = item.PublishingHouse,
                    File = item.File
                };
                if (b.Title.Contains(filter) || filter == "")
                {
                    list.Add(b);
                }
            }
            return list;
        }
    }
}