using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Book code")]
        public string BookCode { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Author")]
        public string Author { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name ="Pieces")]
        public int NoPieces { get; set; }

        [Display(Name = "Publishing house")]
        public string PublishingHouse { get; set; }

        [Display(Name = "Image")]
        public byte[] File { get; set; }

        public string StringFile
        {
            get
            {
                string imageBase64 = Convert.ToBase64String(File);
                return string.Format("data:image/gif;base64,{0}", imageBase64);

            }
            set { }
        }

        public virtual ICollection<BookItem> Pieces { get; set; }

    }
}