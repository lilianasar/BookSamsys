﻿using BookSamsys.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.DTOs {
    public class PagedBookResult {
        public IEnumerable<Book> Books { get; set; } 
        public int TotalCount { get; set; }
        public int TotalPages {  get; set; }
    }
}
