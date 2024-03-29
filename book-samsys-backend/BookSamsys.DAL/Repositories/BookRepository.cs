﻿using BookSamsys.DAL.Context;
using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Entities;
using BookSamsys.Infrastructure.Interfaces.Books;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.DAL.Repositories
{
    public class BookRepository : IBookRepository {
        private readonly BookContext _context;

        public BookRepository(BookContext context) {
            _context = context;
        }

        public async Task<PagedBookResult> GetAll(int pageNumber, int pageQuantity) {
            var totalCount = await _context.Livros.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageQuantity);
            var pagedBooks = await _context.Livros
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToListAsync();
            var pagedBookData = new PagedBookResult {
                Books = pagedBooks,
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            return pagedBookData;

        }
        /*       
        public async Task<IEnumerable<Book>> GetAll() {
            return await _context.Livros.ToListAsync();
        }*/
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

        public async Task<bool> AvailabilityIsbn(string isbn, int id) {
            //verifica se existe um isbn igual já atribuido - se corresponder, false
            var isbnAvailable = await _context.Livros.Where(x => x.Isbn == isbn && x.Id != id).AnyAsync();

            return !isbnAvailable;
        }

        public async Task<bool> RepeatedData(string isbn, string nome, string autor, decimal preco) {
            //verifica se existe um isbn igual já atribuido - se corresponder, false
            var dataRepeated = await _context.Livros.Where(x => x.Isbn == isbn)
                                                    .Where(x => x.Nome == nome)
                                                    .Where(x => x.Autor == autor)
                                                    .Where(x => x.Preco == preco)
                                                    .AnyAsync();

            return dataRepeated;
        }

        
        /*public Task<bool> ValidatePrice(decimal preco) {
            //verifica se o preço é negativo
            var priceValidate = preco < 0;
            
            return Task.FromResult(!priceValidate);
            //return preco < 0 ? !priceValidate : priceValidate;

        }*/
    }

}
