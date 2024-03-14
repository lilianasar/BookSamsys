using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.Entities {
    public class Author {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do autor é necessário")]
        public required string Nome { get; set; }
    }
}
