using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.DTOs {
    public class BookPostDTO {

        [Required(ErrorMessage = "O ISBN é necessário")]
        public required string Isbn { get; set; }

        [Required(ErrorMessage = "O Nome é necessário")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "O Autor é necessário")]
        public required string Autor { get; set; }

        [Required(ErrorMessage = "O Preço é necessário")]
        public decimal Preco { get; set; } = decimal.Zero;
    }
}
