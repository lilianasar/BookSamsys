using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.Entities {
    public class Book {
        public int Id { get; set; }

        [Required(ErrorMessage = "Isbn é necessário")]
        public required string Isbn { get; set; }

        [Required(ErrorMessage = "Nome é necessário")]
        public required string Nome { get; set; }
        public string Autor { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; } = decimal.Zero;
    }
}
