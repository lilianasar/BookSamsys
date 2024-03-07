using BookSamsys.DAL.Context;
using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Entities;
using BookSamsys.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.DAL.Repositories {
    public class BookRepository : IBookRepository {
        private readonly BookContext _context;

        public BookRepository(BookContext context) {
            _context = context;
        }

        public async Task<Book> Update(Book book) {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }
   
        public Task<Book> Create(Book book) {
            throw new NotImplementedException();
        }

        public Task<Book> Delete(int id) {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetAll() {
            throw new NotImplementedException();
        }

        public Task<Book> GetById(int id) {
            throw new NotImplementedException();
        }

        
    }

}
