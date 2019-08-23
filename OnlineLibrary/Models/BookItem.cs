using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Models
{
    public class BookItem
    {
        public int Id { get; set; }    

        [Display(Name ="Date of receip")]
        public DateTime DateOfReceip { get; set; }

        [Display(Name = "Date of return")]
        public DateTime DateOfReturn { get; set; }

        [Required]
        public int BookId { get; set; }

        public virtual Book Book{ get; set; }

        [Required]
        public int BookingAccountId { get; set; }

        public virtual BookingAccount BookingAccount { get; set; }
    }
}