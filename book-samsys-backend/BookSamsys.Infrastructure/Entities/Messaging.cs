using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.Entities {
    public class Messaging {
        public bool Success {  get; set; }
        public string Message { get; set; }
        public string Obj { get; set; }
        public Object Logs { get; set; }
    }
}
