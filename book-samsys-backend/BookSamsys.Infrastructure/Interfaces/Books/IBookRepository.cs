using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.Interfaces.Books
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<Book> Create(Book book);
        Task<Book> Update(Book book);
        Task<Book> Delete(int id);
        Task<bool> AvailabilityIsbn(string isbn, int id);
        //Task<bool> ValidatePrice(decimal preco);
    }
}
