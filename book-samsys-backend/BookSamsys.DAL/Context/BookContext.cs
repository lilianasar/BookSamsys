using BookSamsys.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.DAL.Context {
    public class BookContext : DbContext {
       
        public BookContext(DbContextOptions<BookContext> options) : base(options) {

        }

        //CRIAR A BD
        public DbSet<Book> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(BookContext).Assembly);
        }
    }
}
