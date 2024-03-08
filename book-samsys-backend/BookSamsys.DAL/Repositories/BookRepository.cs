﻿using BookSamsys.DAL.Context;
using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Entities;
using BookSamsys.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Book>> GetAll() {
            return await _context.Livros.ToListAsync();
        }

        public async Task<Book> GetById(int id) {
            return await _context.Livros.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book> Create(Book book) {
            _context.Livros.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> Update(Book book) {
            _context.Livros.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> Delete(int id) {
            var book = await _context.Livros.FindAsync(id);
            if (book != null){
                _context.Livros.Remove(book);
                await _context.SaveChangesAsync();
                return book;
            }
            return null;
        }
    }

}