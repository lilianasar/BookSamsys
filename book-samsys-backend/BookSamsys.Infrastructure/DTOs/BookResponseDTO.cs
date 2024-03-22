using BookSamsys.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.DTOs {
    public class BookResponse {
        public List<Book> Books { get; set; } = new List<Book>();
        public int Pages { get; set; }
        public int CurrentPage {  get; set; }
        public int Total { get; set; }
    }
}
