using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Models
{
    public class BookingAccount
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Account #")]
        public string AccountNumber { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<BookItem> Books { get; set; }

        [Required]
        public string ApplicationUserId { set; get; }
    }
}