using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.DTOs
{
    public class MessagingHelper<T> {
        public bool Success { get; set; }
        public string Message { get; set; } 
        public T Obj { get; set; } 
    }
}
