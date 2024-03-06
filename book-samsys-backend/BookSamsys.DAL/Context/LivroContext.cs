using BookSamsys.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.DAL.Context {
    public class LivroContext : DbContext {
        public LivroContext(DbContextOptions<LivroContext> options) : base(options) {

        }

        //CRIAR A BD
        public DbSet<Livro> Livros { get; set; }

    }
}
