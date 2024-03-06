using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.DTOs {
    public class LivroDTO {
        public required string Isbn { get; set; }
        public required string Nome {  get; set; }
        public string Autor { get; set; } = string.Empty;
        public decimal Preco { get; set; } = decimal.Zero;
    }
}
