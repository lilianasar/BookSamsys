using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.DTOs {
    public class BookDTO {
        [Required]//(ErrorMessage = "O identificador de livro é necessário")]
        public required int Id { get; set; }

        //[Required]//(ErrorMessage = "O ISBN é necessário")]
        public required string Isbn { get; set; } = string.Empty;

        //[Required]//(ErrorMessage = "O Nome é necessário")]
        public required string Nome { get; set; } = string.Empty;

        //[Required]//(ErrorMessage = "O Autor é necessário")]
        public required string Autor { get; set; } = string.Empty ;

        //[Required]//(ErrorMessage = "O Preço é necessário")]
        public decimal Preco { get; set; } = decimal.Zero;

    }
}

